using System.Collections.Generic;

namespace DuplicateFinderWeb.Data {

	public class SettingsRequestBody {
		public bool? IncludeReadOnlyFolders { get; set; }
		public bool? IncludeSubDirectories { get; set; }
		public bool? IncludeImages { get; set; }
		public byte? Threshhold { get; set; }
		public float? Percent { get; set; }
		public int? ThumbnailCount { get; set; }


		public SettingsRequestBody() {

		}
		public SettingsRequestBody(DuplicateFinderEngine.Settings s) {
			IncludeReadOnlyFolders = !s.IgnoreReadOnlyFolders;
			IncludeSubDirectories = s.IncludeSubDirectories;
			IncludeImages = s.IncludeImages;
			Threshhold = s.Threshhold;
			Percent = s.Percent;
			ThumbnailCount = s.ThumbnailCount;
		}

		public static void CopyToSettings(SettingsRequestBody body, DuplicateFinderEngine.Settings s) {
			if (body.IncludeReadOnlyFolders != null) s.IgnoreReadOnlyFolders = !body.IncludeReadOnlyFolders ?? true;
			if (body.IncludeImages != null) s.IncludeImages = body.IncludeImages ?? true;
			if (body.IncludeSubDirectories != null) s.IncludeSubDirectories = body.IncludeSubDirectories ?? true;
			if (body.Threshhold != null) s.Threshhold = body.Threshhold ?? 5;
			if (body.Percent != null) s.Percent = body.Percent ?? 96f;
			if (body.ThumbnailCount != null) s.ThumbnailCount = body.ThumbnailCount ?? 1;
		}
	}

	public class DeleteBody {
		public List<string> Paths { get; set; }
		public bool ClearAll { get; set; }
	}
}
