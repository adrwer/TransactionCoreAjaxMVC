using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TransactionCoreAjaxMVC.Helpers;
using TransactionCoreAjaxMVC.Models;

namespace TransactionCoreAjaxMVC.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly TransactionDbContext _context;

        public TransactionsController(TransactionDbContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transactions.ToListAsync());
        }

        // GET: Transactions/AddOrEdit
        // GET: Transactions/AddOrEdit/1
        public async Task<IActionResult> AddOrEdit(int id=0)
        {
            if (id == 0)
                return View(new Transaction());
            else
            {
                var transaction = await _context.Transactions.FindAsync(id);
                if (transaction == null)
                {
                    return NotFound();
                }
                return View(transaction);
            }
        }

        // POST: Transactions/Save/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(int id, [Bind("TransctionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,DateCreated")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    transaction.DateCreated = DateTime.Now;
                    _context.Add(transaction);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    try
                    {
                        transaction.TransactionId = id;
                        _context.Update(transaction);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TransactionExists(transaction.TransactionId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                }
                return Json(new { isValid =true, html = HtmlHelper.RenderRazorViewToString(this, "_ViewAll", _context.Transactions.ToList()) });
            }
            return Json(new { isValid = false, html = HtmlHelper.RenderRazorViewToString(this, "AddOrEdit", transaction) });
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return Json(new { html = HtmlHelper.RenderRazorViewToString(this, "_ViewAll", _context.Transactions.ToList()) });
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }
    }
}
