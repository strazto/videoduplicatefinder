﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DuplicateFinderWeb.Controllers {
	[ApiController]
	[Route("api/[controller]")]
	public class LoggingController : ControllerBase {
		private readonly ILogger<LoggingController> _logger;
		private Session session = Session.Instance;

		public LoggingController(ILogger<LoggingController> logger) {
			_logger = logger;
		}

		[HttpPost("start")]
		public async Task<ActionResult> EnableLogging() {
			session.SetLogger(_logger);
			return Ok();
		}
	}
}
