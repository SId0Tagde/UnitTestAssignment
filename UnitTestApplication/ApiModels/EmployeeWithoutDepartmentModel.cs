using System.ComponentModel.DataAnnotations;

namespace Employeemanagement.ApiModels
{
    public class EmployeeWithoutDepartmentModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string SurName { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public string Qualification { get; set; } = null!;

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = null!;

    }
}
