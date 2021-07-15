using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DuplicateFinderEngine;

namespace DuplicateFinderWeb.Controllers {
	[ApiController]
	[Route("api/[controller]")]
	public class ScanController : ControllerBase {
		private ScanEngine Scanner;
		private readonly ILogger<ScanController> _logger;
		private ILogger<ScanController> _L { get => _logger ; }

		public ScanEngine.OwnScanProgress Progress {
			get => Session.Progress;
			set => Session.Progress = value;
		}

		public ScanController(ILogger<ScanController> logger) {
			_logger = logger;
			Scanner = Session.Scanner;
		}

		[HttpGet("")]
		public async Task<ActionResult<ScanEngine.OwnScanProgress>> GetScanProgress() {
			return Progress;
		}

		[HttpPost("start")]
		public async Task<ActionResult<string>> ScanStart() {
			_L.LogDebug("Starting search now!");
			Scanner.StartSearch();
			return Ok();
		}
		[HttpPost("stop")]
		public async Task<ActionResult<string>> ScanStop() {
			Scanner.Stop();
			_L.LogDebug("Stopping Search Now!");
			return Ok();
		}

	}
}
