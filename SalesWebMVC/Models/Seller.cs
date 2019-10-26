using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} obrigatorio!")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Tamanho do {0} deve ser entre {2} e {1} caracteres!")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} obrigatorio!")]
        [EmailAddress(ErrorMessage = "Entre com um E-Mail valido!")]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} obrigatorio!")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} obrigatorio!")]
        [Range(100.00, 50000.00, ErrorMessage = "{0} deve ser entre {1} e {2}")]
        [Display(Name = "Salario")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }

        public Department Department { get; set; }

        [Display(Name = "Departamento")]
        public int DepartmentId { get; set; } // Para o EF garantir que a chava nao seja nula
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
