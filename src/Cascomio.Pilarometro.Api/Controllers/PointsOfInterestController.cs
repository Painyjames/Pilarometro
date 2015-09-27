using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Cascomio.Pilarometro.DataContract;
using Cascomio.Pilarometro.Common.Commands;

namespace Cascomio.Pilarometro.Api.Controllers
{
	[Route("api")]
	public class PointsOfInterestController : Controller
	{

		private readonly Command<FindPointsOfInterestRequest, FindPointsOfInterestResponse> _findPointsOfInterestCommand;
		private readonly Command<FindPointOfInterestRequest, FindPointOfInterestResponse> _findPointOfInterestCommand;
		private readonly Command<UpdateUserPointsOfInterestRequest, UpdateUserPointsOfInterestResponse> _updateUserPointsOfInterestCommand;
		private readonly Command<FindUserPointsOfInterestRequest, FindUserPointsOfInterestResponse> _findUserPointsOfInterestCommand;

		public PointsOfInterestController(Command<FindPointsOfInterestRequest, FindPointsOfInterestResponse> findPointsOfInterestCommand,
								Command<FindPointOfInterestRequest, FindPointOfInterestResponse> findPointOfInterestCommand,
								Command<UpdateUserPointsOfInterestRequest, UpdateUserPointsOfInterestResponse> updateUserPointsOfInterestCommand,
								Command<FindUserPointsOfInterestRequest, FindUserPointsOfInterestResponse> findUserPointsOfInterestCommand)
		{
			_findPointOfInterestCommand = findPointOfInterestCommand;
			_findPointsOfInterestCommand = findPointsOfInterestCommand;
			_updateUserPointsOfInterestCommand = updateUserPointsOfInterestCommand;
			_findUserPointsOfInterestCommand = findUserPointsOfInterestCommand;
		}

		[HttpGet("[controller]")]
		public async Task<IActionResult> Get(FindPointsOfInterestRequest request)
		{
			return Json(await _findPointsOfInterestCommand.Execute(request));
		}

		[HttpGet("[controller]/{id}")]
		public async Task<IActionResult> Get(FindPointOfInterestRequest request)
		{
			return Json(await _findPointOfInterestCommand.Execute(request));
		}

		[HttpGet("User/{id}/[controller]")]
		public async Task<IActionResult> GetUserPointsOfInterest(int id, FindUserPointsOfInterestRequest request)
		{
			return Json(await _findUserPointsOfInterestCommand.Execute(request));
		}

		[HttpPut("User/{id}/[controller]")]
		public async Task<IActionResult> UpdateUserPointsOfInterest(int id, [FromBody]UpdateUserPointsOfInterestRequest request)
		{
			return Json(await _updateUserPointsOfInterestCommand.Execute(request));
		}

	}
}
