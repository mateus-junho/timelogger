using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timelogger.Application.Dtos;
using Timelogger.Application.Interfaces;
using Timelogger.Domain.DomainObjects;

namespace Timelogger.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectsController : Controller
	{
		private readonly IProjectAppService projectAppService;

		public ProjectsController(IProjectAppService projectAppService)
		{
			this.projectAppService = projectAppService;
		}

		[HttpGet(@"{projectId}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<ProjectDto>> GetProjectTimeRegister(int projectId)
		{
			var project = await projectAppService.GetProjectTimeRegister(projectId);

			if (project == null)
			{
				return NotFound();
			}

			return Ok(project);
		}

		[HttpPost(@"{projectId}/register-time")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<TimeRegisterDto>> AddTimeRegister(int projectId, NewTimeRegisterDto newTimeRegisterDto)
		{
			if (projectId != newTimeRegisterDto.ProjectId)
			{
				return BadRequest();
			}

			try
			{
				var savedTimeRegister = await projectAppService.RegisterTimeInProject(newTimeRegisterDto);
				return CreatedAtAction(nameof(GetProjectTimeRegister), new { projectId = savedTimeRegister.ProjectId }, savedTimeRegister);
			}
			catch (DomainException de)
            {
				return BadRequest(de.Message);
            }
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAll([FromQuery] bool sortDeadline = false)
		{
			var projects = await projectAppService.GetAllProjects(sortDeadline);

			return Ok(projects);
		}
	}
}
