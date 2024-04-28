using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class UniqueNameAttribute:ValidationAttribute
    {
        
        protected override ValidationResult? IsValid(object? value, 
            ValidationContext validationContext)

        {
            ITIEntity context = new ITIEntity();

            if (value != null)
            {
                string name = value.ToString();//value is an employee name

              

                Employee emp = context.Employees.FirstOrDefault(e => e.Name == name);

                if (emp == null)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("name is already found");
                //return base.IsValid(value, validationContext);
            }

            return new ValidationResult("name is required");
        }
    }
}
