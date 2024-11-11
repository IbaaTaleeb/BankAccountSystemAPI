using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankAccountAssignment.API.Data;
using BankAccountAssignment.API.Models;

namespace BankAccountAssignment.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountsController : ControllerBase
    {
        private readonly BankAccountDbContext _context;

        public BankAccountsController(BankAccountDbContext context)
        {
            _context = context;
        }

        // POST: api/BankAccounts/OpenAccount
        [HttpPost("OpenAccount")]
        public async Task<ActionResult<BankAccount>> OpenAccount([FromBody] OpenAccountRequest request)
        {
            var bankAccount = new BankAccount
            {
                HolderName = request.HolderName,
                AssociatedPhoneNumber = request.AssociatedPhoneNumber,
                DateOfBirth = request.DateOfBirth
            };

            _context.BankAccounts.Add(bankAccount);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BankAccountExists(bankAccount.Number))
                {
                    return Conflict("Account number already exists.");
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetAccountDetails), new { AccountNumber = bankAccount.Number }, bankAccount);
        }

        // GET: api/BankAccounts/GetAccountDetails/{AccountNumber}
        [HttpGet("GetAccountDetails/{AccountNumber}")]
        public async Task<ActionResult<BankAccount>> GetAccountDetails(string AccountNumber)
        {
            var bankAccount = await _context.BankAccounts.FindAsync(AccountNumber);

            if (bankAccount == null)
            {
                return NotFound("Account not found.");
            }

            return bankAccount;
        }

        // PUT: api/BankAccounts/ModifyAccountDetails/{AccountNumber}
        [HttpPut("ModifyAccountDetails/{AccountNumber}")]
        public async Task<IActionResult> ModifyAccountDetails(string AccountNumber, [FromBody] ModifyAccountDetailsRequest request)
        {
            var bankAccount = await _context.BankAccounts.FindAsync(AccountNumber);
            if (bankAccount == null)
            {
                return NotFound("Account not found.");
            }

            bankAccount.HolderName = request.HolderName;
            bankAccount.AssociatedPhoneNumber = request.AssociatedPhoneNumber;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankAccountExists(AccountNumber))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // PATCH: api/BankAccounts/Deposit/{AccountNumber}
        [HttpPatch("Deposit/{AccountNumber}")]
        public async Task<IActionResult> Deposit(string AccountNumber, [FromBody] TransactionRequest request)
        {
            var bankAccount = await _context.BankAccounts.FindAsync(AccountNumber);
            if (bankAccount == null)
            {
                return NotFound("Account not found.");
            }

            bankAccount.Balance += request.Amount;
            await _context.SaveChangesAsync();

            return Ok($"Deposited {request.Amount}KD to account {AccountNumber}. New balance: {bankAccount.Balance}KD");
        }

        // PATCH: api/BankAccounts/Withdraw/{AccountNumber}
        [HttpPatch("Withdraw/{AccountNumber}")]
        public async Task<IActionResult> Withdraw(string AccountNumber, [FromBody] WithdrawRequest request)
        {
            var bankAccount = await _context.BankAccounts.FindAsync(AccountNumber);
            if (bankAccount == null)
            {
                return NotFound("Account not found.");
            }

            if (bankAccount.Balance < request.Amount)
            {
                return BadRequest("Insufficient funds.");
            }

            bankAccount.Balance -= request.Amount;
            await _context.SaveChangesAsync();

            return Ok($"Withdrew {request.Amount}KD from account {AccountNumber}. New balance: {bankAccount.Balance}KD");
        }

        private bool BankAccountExists(string accountNumber)
        {
            return _context.BankAccounts.Any(e => e.Number == accountNumber);
        }
    }
}
