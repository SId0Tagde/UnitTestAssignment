using AutoMapper;
using Employeemanagement.DatabaseBuild.EntityModels;


namespace Employeemanagement.ApiModels.ModelProfile
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeModel>()
                    .ForPath(dest => dest.DepartmentName,
                        opt => opt.MapFrom(src => src.Department.DepartmentName))
                    .ReverseMap()
                    .ForPath(dest => dest.Department.DepartmentName,
                                opt => opt.MapFrom(src => src.DepartmentName));

            CreateMap<Employee, EmployeeWithoutDepartmentModel>()
                    .ReverseMap();
        }
    }
}
