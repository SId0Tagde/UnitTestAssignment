using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employeemanagement.DatabaseBuild.EntityModels
{
    public class Employee
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        public Department Department { get; set; } = null!;

    }
}
