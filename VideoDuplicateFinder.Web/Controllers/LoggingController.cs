using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DuplicateFinderWeb.Controllers {
	[ApiController]
	[Route("api/[controller]")]
	public class LoggingController : ControllerBase {
		private readonly ILogger<LoggingController> _logger;

		public LoggingController(ILogger<LoggingController> logger) {
			_logger = logger;
		}

		[HttpPost("start")]
		public async Task<ActionResult> EnableLogging() {
			Session.SetLogger(_logger);
			return Ok();
		}
	}
}
