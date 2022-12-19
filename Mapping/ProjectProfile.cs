using AutoMapper;
using CreateProjectOlive.Dtos;
using CreateProjectOlive.Models;

namespace CreateProjectOlive.Mapping
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, CreateProjectDto>();

            CreateMap<Project,UpdateProjectDto>();

        }
    }

}