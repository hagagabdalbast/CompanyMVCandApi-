using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Models
{
    [ModelMetadataType(typeof(EmployeeMetaData))]
    public class Employee
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public int Salary { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }
        [ForeignKey("Department")]
        public int Dept_Id { get; set; }
        public virtual Department? Department { get; set; }
    }
}
