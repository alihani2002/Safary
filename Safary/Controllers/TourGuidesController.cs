using Domain.Consts;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Safary.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TourGuidesController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;

		public TourGuidesController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult GetAll(string duration)
		{
			// !(specificStartDate <= user.EndDate && specificEndDate >= user.StartDate)

			if (!string.IsNullOrEmpty(duration))
			{
				if (!DateTime.TryParse(duration.Split(separator: " - ")[0], out DateTime from))
				{
					ModelState.AddModelError("Duration", Errors.InvalidStartDate);
					return BadRequest(ModelState);
				}

				if (!DateTime.TryParse(duration.Split(" - ")[1], out DateTime to))
				{
					ModelState.AddModelError("Duration", Errors.InvalidEndDate);
					return BadRequest(ModelState);
				}

			}

		}

	}
}
