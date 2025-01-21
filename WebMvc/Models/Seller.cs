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
        [Required(ErrorMessage ="Name Required")]
        [StringLength(60, MinimumLength =3, ErrorMessage ="{0} size should be between {2} and {1}")]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} Required")]
        [EmailAddress(ErrorMessage ="Enter a valid Email")]
        public string Email { get; set; }
        [Display(Name="Base Salary")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        [Required(ErrorMessage ="{0} Required")]
        [Range(1500.00,50000.00, ErrorMessage = "The {0} should be between {1} and {2}")]
        public double BaseSalary { get; set; }
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        [Required(ErrorMessage ="{0} Required")]
        public DateTime BirthDate { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
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
