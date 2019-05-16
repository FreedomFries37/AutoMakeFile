using System;
using System.IO;

namespace AutoMakeFile.core.input.sorting_methods {
	public class LexogragicalSort : FileSorter {
		
		public int Compare(FileInfo x, FileInfo y) {
			return string.Compare(x?.Name, y?.Name, StringComparison.CurrentCulture);
		}
	}
}