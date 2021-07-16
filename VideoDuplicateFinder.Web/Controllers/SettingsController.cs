using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DuplicateFinderEngine;

namespace DuplicateFinderWeb.Controllers {
	[ApiController]
	[Route("api/[controller]")]
	public class SettingsController : ControllerBase {
		private ScanEngine Scanner;
		private readonly ILogger<SettingsController> _logger;
		private Session session = Session.Instance;

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
			Scanner = session.Scanner;
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
}
