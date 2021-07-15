using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;

using DuplicateFinderEngine.Data;

namespace DuplicateFinderWeb.Data {

	public class DuplicateListBody
	{
		private IEnumerable<DuplicateItem> _duplicates;
		private IUrlHelper _urlHelper;

		public Dictionary<string, List<DuplicateItemBody>> Duplicates {
			get {
				return _duplicates
					.Select(o => new DuplicateItemBody(o, _urlHelper))
					.GroupBy(o => o.Entry.GroupId.ToString())
					.ToDictionary(g => g.Key, g => g.ToList());
			}
		}
		public DuplicateListBody(IEnumerable<DuplicateItem> duplicates, IUrlHelper urlHelper) {
			_duplicates = duplicates;
			_urlHelper = urlHelper;
		}
	}


	public class DuplicateItemBody
	{
		private DuplicateItem _entry;
		private IUrlHelper _urlHelper;
		public  DuplicateItem Entry { get => _entry ; }

		public DuplicateItemBody(DuplicateItem item, IUrlHelper urlHelper)
		{
			_entry = item;
			_urlHelper = urlHelper;
		}

		public List<string> ThumbnailLinks {
			get {
				List<string> imgLinks =_entry.Thumbnail.Select<Image, string>(
					(Image img, int idx) => {
						return _urlHelper.Action(
							"GetDuplicateThumbnail", "duplicate",
							new {gid = _entry.GroupId.ToString(), id = IdInGroup, idx = idx });
					}).ToList();

				return imgLinks;
			} 
		} 

		public uint IdInGroup {
			get {
				// .GetHashCode() is considered unstable,
				// Consider a different hash
				return (uint) _entry.Path.GetHashCode();
			}
		}

	}
}
