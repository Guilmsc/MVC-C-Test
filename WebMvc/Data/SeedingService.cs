using System;
using WebMvc.Models.Enums;
using WebMvc.Models;
using System.Linq;

namespace WebMvc.Data
{
    public class SeedingService
    {
        private WebMvcContext _context;

        public SeedingService(WebMvcContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Department.Any() || _context.Seller.Any() || _context.SalesRecord.Any())
            {
                return; //bd ja populado
            }
            Department d4 = new Department{Name="Eletronics"};

            Seller s1 = new Seller{Name = "Bob",Email = "Bob@gmail.com", BaseSalary = 1000, BirthDate = new DateTime(1990, 10, 20),Department = d4};

            SalesRecord r1 = new SalesRecord{Date = new DateTime(2020, 12, 01), Amount = 2000,Status = SaleStatus.Billed,Seller = s1};

            _context.Department.Add(d4);
            _context.Seller.Add(s1);
            _context.SalesRecord.Add(r1);

            _context.SaveChanges();
        }
    }
}
