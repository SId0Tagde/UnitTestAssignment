using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employeemanagement.DatabaseBuild.EntityModels
{
    public class Department
    {
        [Key]
        [Required]
        public string DepartmentName { get; set; } = null!;

    }
}
