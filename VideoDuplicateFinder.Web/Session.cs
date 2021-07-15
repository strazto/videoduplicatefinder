using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using DuplicateFinderEngine;
using System.Text.Json;

namespace DuplicateFinderWeb.Controllers {
	// This is an anti-pattern for creating persistence, I just wanted a fast PoC though.
	public class Session {
		public static Session Instance { get; } = new Session();
		
		private ScanEngine _scanner;
		public ScanEngine Scanner { get => _scanner; }

		private ILogger _L { get; set; } = NullLogger.Instance;

		public void SetLogger(ILogger logger) {
			_L = logger;
			_L.LogInformation("Logger set to {logger}", logger);
		}
		private  List<DuplicateFinderEngine.Data.DuplicateItem> _duplicates;
		public  List<DuplicateFinderEngine.Data.DuplicateItem> Duplicates {
			get => _duplicates;
			set {
				_duplicates = value;
			}
		}

		private  ScanEngine.OwnScanProgress _progress;
		public  ScanEngine.OwnScanProgress Progress { get => _progress; set => _progress = value; }

		public Session()
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
		private void Instance_LogItemAdded(object sender, EventArgs e)  {
			while (Logger.Instance.LogEntries.Count > 0) {
				if (Logger.Instance.LogEntries.TryTake(out var item))
					_L.LogInformation("{LogItem}", item);
			}
		}

		private void Scanner_ScanDone(object sender, EventArgs e)
		{
			_L.LogInformation("Scan Finished");
			Duplicates = new List<DuplicateFinderEngine.Data.DuplicateItem>(Scanner.Duplicates);
			Scanner.PopulateDuplicateThumbnails();
		}

		private void Scanner_Progress(object sender, ScanEngine.OwnScanProgress e)
		{
			_L.LogInformation(
				"Progress Update: {CurrentPosition}\t{Elapsed} / {Remaining}\t{CurrentFile}", 
				                   e.CurrentPosition,  e.Elapsed,  e.Remaining, e.CurrentFile
								   );
			Progress = e;
		}

		private void Scanner_FilesEnumerated(object sender, EventArgs e)
		{
			_L.LogInformation("Files Enumerated");
		}

	}
}
