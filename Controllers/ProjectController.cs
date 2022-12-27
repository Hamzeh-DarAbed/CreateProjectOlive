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
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetProjects()
        {
            try
            {
                return Ok(_unitOfWork.ProjectService.FindAll());
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
                Project? project =  _unitOfWork.ProjectService.FindByCondition(x => x.Id.Equals(id)).FirstOrDefault();

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
                Project project = _mapper.Map<Project>(projectDto);

                _unitOfWork.ProjectService.Create(project);

                return CreatedAtRoute("GetProject", new { id = project.Id.ToString() }, project);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        [HttpPut]
        [Route("~/UpdateProject/{id}")]
        public async Task<IActionResult> UpdateProject(string id, UpdateProjectDto projectIn)
        {
            try
            {

                Project? project = await _unitOfWork.ProjectService.FindByCondition(x => x.Id.Equals(id)).FirstOrDefaultAsync();

                if (project == null)
                {
                    return NotFound();
                }

               _mapper.Map(projectIn, project);

                 _unitOfWork.ProjectService.Update(project);

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

                Project? project = await _unitOfWork.ProjectService.FindByCondition(x => x.Id.Equals(id)).FirstOrDefaultAsync();

                if (project == null)
                {
                    return NotFound();
                }

                 _unitOfWork.ProjectService.Delete(project);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}