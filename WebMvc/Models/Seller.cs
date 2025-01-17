using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace WebMvc.Models
{
    public class Seller
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public double BaseSalary { get; set; }
        public DateTime BirthDate { get; set; }
        public Department Department { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
            
        }

        public Seller(int id, string name, string email, double baseSalary, DateTime birthDate, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BaseSalary = baseSalary;
            BirthDate = birthDate;
            Department = department;
        }

        public void AddSales(SalesRecord sales)
        {
            Sales.Add(sales);
        }
        public void RemoveSales(SalesRecord sales)
        {
            Sales.Remove(sales);
        }

        public double TotalSales(DateTime inital, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= inital && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
