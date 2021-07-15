using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DuplicateFinderEngine;
using System.Text.Json;

namespace DuplicateFinderWeb.Controllers
{
	// This is an anti-pattern for creating persistence, I just wanted a fast PoC though.
	public static class Session {
		private static ScanEngine _scanner;
		public static ScanEngine Scanner { get => _scanner; }

		private static ILogger _L { get; set; } = NullLogger.Instance;

		public static void SetLogger(ILogger logger) {
			_L = logger;
			_L.LogInformation("Logger set to {logger}", logger);
		}
		private static List<DuplicateFinderEngine.Data.DuplicateItem> _duplicates;
		public static List<DuplicateFinderEngine.Data.DuplicateItem> Duplicates {
			get => _duplicates;
			set {
				_duplicates = value;
			}
		}

		private static ScanEngine.OwnScanProgress _progress;
		public static ScanEngine.OwnScanProgress Progress { get => _progress; set => _progress = value; }

		static Session()
		{
			_scanner = new ScanEngine();

			Scanner.ScanDone += Scanner_ScanDone;
			Scanner.Progress += Scanner_Progress;
			Scanner.FilesEnumerated += Scanner_FilesEnumerated;

			Progress = new ScanEngine.OwnScanProgress {
				CurrentFile = "",
				CurrentPosition = 0,
				Elapsed = new TimeSpan(0),
				Remaining = new TimeSpan(0)
			};

			Logger.Instance.LogItemAdded += Instance_LogItemAdded;

		}
		private static void Instance_LogItemAdded(object sender, EventArgs e)  {
			while (Logger.Instance.LogEntries.Count > 0) {
				if (Logger.Instance.LogEntries.TryTake(out var item))
					_L.LogInformation("{LogItem}", item);
			}
		}

		private static void Scanner_ScanDone(object sender, EventArgs e)
		{
			_L.LogInformation("Scan Finished");
			Duplicates = new List<DuplicateFinderEngine.Data.DuplicateItem>(Scanner.Duplicates);
			Session.Scanner.PopulateDuplicateThumbnails();
		}

		private static void Scanner_Progress(object sender, ScanEngine.OwnScanProgress e)
		{
			_L.LogInformation(
				"Progress Update: {CurrentPosition}\t{Elapsed} / {Remaining}\t{CurrentFile}", 
				                   e.CurrentPosition,  e.Elapsed,  e.Remaining, e.CurrentFile
								   );
			Progress = e;
		}

		private static void Scanner_FilesEnumerated(object sender, EventArgs e)
		{
			_L.LogInformation("Files Enumerated");
		}

	}
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

	[ApiController]
	[Route("api/[controller]")]
	public class SettingsController : ControllerBase {
		private ScanEngine Scanner;
		private readonly ILogger<SettingsController> _logger;
		private ILogger<SettingsController> _L { get => _logger; }

		public static T GetPropertyValue<T>(object obj, string propName) { return (T)obj.GetType().GetProperty(propName).GetValue(obj, null); }

		public List<string> IncludeList {
			get {
				List<string> sorted = Scanner.Settings.IncludeList.ToList();
				sorted.Sort();
				return sorted;
			}
		}

		public List<string> BlackList {
			get {
				List<string> sorted = Scanner.Settings.BlackList.ToList();
				sorted.Sort();
				return sorted;
			}
		}

		public SettingsController(ILogger<SettingsController> logger) {
			_logger = logger;
			Scanner = Session.Scanner;
		}

		// Get Condition Paths
		[HttpGet("include/path")]
		public IEnumerable<string> GetIncludeList() => IncludeList;

		[HttpGet("exclude/path")]
		public IEnumerable<string> GetExcludeList() => BlackList;

		// Get Item From Condition Paths
		[HttpGet("include/path/{id}", Name = "GetIncludeListItem")]
		public async Task<ActionResult<string>> GetIncludeListItem(int idx) {
			return getListItem(idx, IncludeList);
		}

		[HttpGet("exclude/path/{id}", Name = "GetExcludeListItem")]
		public async Task<ActionResult<string>> GetExcludeListItem(int idx) {
			return getListItem(idx, BlackList);
		}

		private ActionResult<string> getListItem(int idx, List<string> ls) {
			string element = ls.ElementAtOrDefault(idx);
			
			_L.LogDebug("I got: {element}", element);

			if (element == null) return NotFound();
			return element;
		}

		// Add Condition Paths

		[HttpPost("include/path")]
		public async Task<ActionResult<string>> AddIncludeDir([FromBody] string path) {
			return await addConditionPath(path, nameof(IncludeList), nameof(GetIncludeListItem));
		}
		[HttpPost("exclude/path")]
		public async Task<ActionResult<string>> AddExcludeDir([FromBody] string path) {
			return await addConditionPath(path, nameof(BlackList), nameof(GetExcludeListItem));
		}

		private async Task<ActionResult<string>> addConditionPath(string path, string listName, string actionName) {
			GetPropertyValue<HashSet<string>>(Scanner.Settings, listName).Add(path);

			int idx = GetPropertyValue<List<string>>(this, listName).IndexOf(path);
			return CreatedAtAction(actionName, new { id = idx }, path);
		}

		// Delete Condition paths




		[HttpDelete("include/path")]
		public async Task<IActionResult> DeleteIncludePaths([FromBody] Data.DeleteBody req) {
			return await deleteConditionPaths(req, Scanner.Settings.IncludeList);
		}

		[HttpDelete("exclude/path")]
		public async Task<IActionResult> DeleteExcludePaths([FromBody] Data.DeleteBody req) {
			return await deleteConditionPaths(req, Scanner.Settings.BlackList);
		}

		private async Task<IActionResult> deleteConditionPaths(Data.DeleteBody req, HashSet<string> conditionList) {
			if (req.ClearAll) {
				conditionList.Clear();
				return NoContent();
			}
			req.Paths.ForEach(delegate (string path) { conditionList.Remove(path); });
			return NoContent();
		}
		


		[HttpPost("")]
		public async Task<ActionResult<Data.SettingsRequestBody>> AddSettings([FromBody] Data.SettingsRequestBody body)
		{
			Data.SettingsRequestBody.CopyToSettings(body, Scanner.Settings);

			return await GetSettings();
		}

		[HttpGet("")]
		public async Task<ActionResult<Data.SettingsRequestBody>> GetSettings() => new Data.SettingsRequestBody(Scanner.Settings);


	}


	[ApiController]
	[Route("api/[controller]")]
	public class DuplicateController : ControllerBase {
		private ScanEngine Scanner;

		public List<DuplicateFinderEngine.Data.DuplicateItem> Duplicates {
			get => Session.Duplicates;
			set => Session.Duplicates = value;
		}

		private readonly ILogger<DuplicateController> _logger;

        public DuplicateController(ILogger<DuplicateController> logger)
        {
            _logger = logger;
			Scanner = Session.Scanner;
        }


		[HttpGet("{idx}")]
		public DuplicateFinderEngine.Data.DuplicateItem GetDuplicate(int idx)
		{
			return Duplicates[idx];
		}
		

        [HttpGet("")]
        public IEnumerable<DuplicateFinderEngine.Data.DuplicateItem> GetDuplicates()
        {
			return Duplicates;
        }

	}
}
