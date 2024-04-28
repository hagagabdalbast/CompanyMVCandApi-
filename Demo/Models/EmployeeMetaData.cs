using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Models
{
    public class EmployeeMetaData
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Name is required and the length from 3 to 20 char")]
        [MinLength(3)]
        [MaxLength(20)]
        [UniqueName]
        public string? Name { get; set; }
        [Required]
        [RegularExpression(@"[0-9]{4}")]
        //[Range(2000,10000,ErrorMessage ="the salary is required and must be from 2000$ to 10000$")]
        [DataType(DataType.Currency)]
        //in remote get to things the first is the action and the second is the controller
        //to use the remote validation the must be understand client side validation
        [Remote("CheckSalary", "Employee", ErrorMessage = "the salary must be greater than 2000")]
        public int Salary { get; set; }
        [Required]
        [RegularExpression("Alex|Menia|Assuit|Sohage", ErrorMessage = "the Address is required and must be alex or menia or assuit or sohag")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Image is required")]
       // [RegularExpression(@"[a-z]+\.(jpg|png)", ErrorMessage = "Image is required and must be jpg and png")]
        public string? Image { get; set; }
        [Display(Name = "Department Name")]
        [ForeignKey("Department")]
        public int Dept_Id { get; set; }
        public virtual Department? Department { get; set; }
    }
}
