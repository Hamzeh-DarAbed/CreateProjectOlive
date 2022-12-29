using AutoMapper;
using CreateProjectOlive.Dtos;
using CreateProjectOlive.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CreateProjectOlive.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using MongoOlive.Models;

namespace CreateProjectOlive.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private UserManager<User> _userManager;

        private RoleManager<IdentityRole> _roleManager;

        public ProjectController(IMapper mapper,
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("~/GetProjects")]
        public IActionResult GetProjects()
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
        public IActionResult ProjectDetails(Guid id)
        {
            try
            {
                Project? project = _unitOfWork.ProjectService.FindByCondition(x => x.Id == id).FirstOrDefault();


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
        public IActionResult CreateProject(CreateProjectDto projectDto)
        {
            try
            {
                Project project = _mapper.Map<Project>(projectDto);
                User user = _userManager.GetUserAsync(HttpContext.User).Result;
                
                _unitOfWork.ProjectService.Create(project, user);

                return CreatedAtRoute("GetProject", new { id = project.Id }, project);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        [HttpPut]
        [Route("~/UpdateProject/{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, CreateProjectDto projectIn)
        {
            try
            {

                Project? project = await _unitOfWork.ProjectService.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();

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
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            try
            {

                Project? project = await _unitOfWork.ProjectService.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();

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