using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Customers;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly AplicationDbContext _context;

        public CustomerRepository(AplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task add(Customer customer) => await _context.Customers.AddAsync(customer);

        public async Task<Customer> GetByIdAsync(CustomerId id) => await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
        
    }
}