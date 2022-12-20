using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CreateProjectOlive.Dtos;
using CreateProjectOlive.Models;
using CreateProjectOlive.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;


namespace CreateProjectOlive.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectController(IMapper mapper,
        IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("~/GetProjects")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetProjects()
        {
            try
            {
                System.Console.WriteLine(await _unitOfWork.ProjectService.GetAll());
                return Ok(await _unitOfWork.ProjectService.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("~/ProjectDetails/{id}", Name = "GetProject")]
        public async Task<IActionResult> ProjectDetails(string id)
        {
            try
            {
                Project project = await _unitOfWork.ProjectService.GetById(id);

                if (project == null)
                {
                    return NotFound();
                }

                return Ok(project);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("~/CreateProject")]
        public async Task<IActionResult> CreateProject(CreateProjectDto projectDto)
        {
            try
            {
                var project = new Project
                {
                    ProjectName = projectDto.ProjectName,
                    ProjectDescription = projectDto.ProjectDescription,
                    BusinessType = projectDto.BusinessType,
                    CreatedBy = projectDto.CreatedBy,
                    Domain = projectDto.Domain,

                };

                Project project = _mapper.Map<Project>(projectDto);
                await _unitOfWork.ProjectService.Create(project);

                return CreatedAtRoute("GetProject", new { id = project.Id.ToString() }, project);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("~/UpdateProject/{id}")]
        public async Task<IActionResult> UpdateProject(string id, UpdateProjectDto projectIn)
        {
            try
            {

                Project project = await _unitOfWork.ProjectService.GetById(id);

                if (project == null)
                {
                    return NotFound();
                }

                _mapper.Map(projectIn, project);

                await _unitOfWork.ProjectService.Update(id, project);

                return Ok(project);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("~/DeleteProject/{id}")]
        public async Task<IActionResult> DeleteProject(string id)
        {
            try
            {

                Project project = await _unitOfWork.ProjectService.GetById(id);

                if (project == null)
                {
                    return NotFound();
                }

                await _unitOfWork.ProjectService.Delete(id);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}