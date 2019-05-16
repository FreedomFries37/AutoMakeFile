using System;
using System.IO;

namespace AutoMakeFile.core.input.sorting_methods {
	public class ModifiedSort : FileSorter{
		public int Compare(FileInfo x, FileInfo y) {
			if (x == null &&
				y == null) return 0;
			if (x == null) return 1;
			if (y == null) return -1;
			return DateTime.Compare(x.LastWriteTime, y.LastWriteTime);
		}
	}
}