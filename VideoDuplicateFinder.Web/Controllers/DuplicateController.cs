using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using DuplicateFinderEngine;

namespace DuplicateFinderWeb.Controllers {
	[ApiController]
	[Route("api/[controller]")]
	public class DuplicateController : ControllerBase {
		private ScanEngine Scanner;
		private Session session = Session.Instance;

		public Dictionary<string, List<Data.DuplicateItemBody>> Duplicates {
			get => new Data.DuplicateListBody(session.Duplicates, Url).Duplicates;
		}

		private readonly ILogger<DuplicateController> _logger;

        public DuplicateController(ILogger<DuplicateController> logger)
        {
            _logger = logger;
			Scanner = session.Scanner;
        }


		[HttpGet("{gid}")]
		public IEnumerable<Data.DuplicateItemBody> GetDuplicateGroup(string gid)
		{
			return Duplicates[gid];
		}
		
		[HttpGet("{gid}/{id}/")]
		public async Task<ActionResult<Data.DuplicateItemBody>> GetDuplicateItem(string gid, uint id)
		{
			var group = Duplicates[gid];
			Data.DuplicateItemBody item = group.Find(i => i.IdInGroup == id);

			if (item == null)
			{
				_logger.LogInformation("Unable to find {ItemId} in {Group} with {GroupId}", id, group, gid);
				return NotFound();
			}
			return item;
		}

        [HttpGet("")]
        public Dictionary<string, List<Data.DuplicateItemBody>> GetDuplicates()
        {
			return Duplicates;
        }

		[HttpGet("{gid}/{id}/thumbs/{idx}.jpg", Name = "GetDuplicateThumbnail")]
		public async Task<ActionResult<System.Drawing.Image>> GetDuplicateThumbnail(string gid, uint id, int idx)
		{
			ActionResult<Data.DuplicateItemBody> response = await GetDuplicateItem(gid, id);

			if (response.Result != null && (response.Result as StatusCodeResult).StatusCode == (int) System.Net.HttpStatusCode.NotFound)
			{
				_logger.LogInformation("Item {id} not found, cant get thumbs", id);
				return NotFound(new { gid = gid, id = id });
			}

			if (idx < 0) {
				_logger.LogInformation("Negative thumbnail index {idx} given", idx);
				return BadRequest(new { idx = idx });
			}

			Data.DuplicateItemBody item = response.Value;
			if (item.Entry.Thumbnail.Count > idx) return item.Entry.Thumbnail[idx];

			return NotFound(new { idx = idx });
		}
	}
}
