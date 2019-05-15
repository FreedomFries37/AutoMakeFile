using System;
using System.Collections.Generic;
using System.IO;

namespace AutoMakeFile.core.input {
	public class LexogragicalSort : IComparer<FileInfo> {
		
		public int Compare(FileInfo x, FileInfo y) {
			return string.Compare(x?.Name, y?.Name, StringComparison.CurrentCulture);
		}
	}
}