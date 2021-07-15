using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using DuplicateFinderEngine;

namespace DuplicateFinderWeb.Controllers {
	[ApiController]
	[Route("api/[controller]")]
	public class DuplicateController : ControllerBase {
		private ScanEngine Scanner;
		private Session session = Session.Instance;

		public List<DuplicateFinderEngine.Data.DuplicateItem> Duplicates {
			get => session.Duplicates;
			set => session.Duplicates = value;
		}

		private readonly ILogger<DuplicateController> _logger;

        public DuplicateController(ILogger<DuplicateController> logger)
        {
            _logger = logger;
			Scanner = session.Scanner;
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
