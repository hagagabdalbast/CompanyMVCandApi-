using System.ComponentModel.DataAnnotations;

namespace Demo.ViewModel
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
