using System.ComponentModel.DataAnnotations;

namespace Employeemanagement.ApiModels
{
    public class DepartmentModel
    {

        [Required]
        public string DepartmentName { get; set; } = null!;
    }
}
