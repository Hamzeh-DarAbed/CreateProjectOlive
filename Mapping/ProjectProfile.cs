using AutoMapper;
using CreateProjectOlive.Dtos;
using CreateProjectOlive.Models;

namespace CreateProjectOlive.Mapping
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<CreateProjectDto, Project>();

            CreateMap<UpdateProjectDto, Project>();

        }
    }

}