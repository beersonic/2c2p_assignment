using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using CustomerService.Models;
using System.Text.RegularExpressions;

namespace CustomerService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DB2C2PContext _context;

        public CustomersController(DB2C2PContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetCustomerById/{id}")]
        private async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            var customer = await _context.Customer.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            // transaction
            var transactions = await _context.Transaction.Where(t => t.CustomerId == customer.CustomerId).ToListAsync();
            customer.Transactions = transactions;

            return customer;
        }

        [HttpGet]
        [Route("GetCustomerByEmail/{email}")]
        private async Task<ActionResult<Customer>> GetCustomerByEmail(string email)
        {
            if (!Util.IsValidEmail(email))
            {
                return BadRequest("Invalid email");
            }
            var customer = await _context.Customer.FirstAsync(p => p.ContactEmail == email);
            
            if (customer == null)
            {
                return NotFound();
            }

            // transaction
            var transactions = await _context.Transaction.Where(t => t.CustomerId == customer.CustomerId).ToListAsync();
            customer.Transactions = transactions;

            return customer;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(CustomerRequest request)
        {
            if (request.CustomerId.HasValue)
            {
                return await GetCustomerById(request.CustomerId.Value);
            }
            else if (request.Email != null)
            {
                return await GetCustomerByEmail(request.Email);
            }
            return BadRequest("No inquiry criteria");
        }

        // GET: api/Customers
        [HttpGet]
        private async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
             var customers = await _context.Customer.ToListAsync();
            foreach (var c in customers)
            {
                var transactions = await (from t in _context.Transaction
                                          where t.CustomerId == c.CustomerId
                                          select t).ToListAsync();
                c.Transactions = transactions;
            }
            return customers;
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        private async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        [HttpPost]
        [Route("CreateCustomer/{email}")]
        private async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        private async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustomerId == id);
        }
    }
}
