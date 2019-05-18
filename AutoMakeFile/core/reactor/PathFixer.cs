using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoMakeFile.core.reactor {
	public class PathFixer {

		public DirectoryInfo BaseDirectory { get; } = new DirectoryInfo(Directory.GetCurrentDirectory());

		public PathFixer(string basePath) {
			BaseDirectory = new DirectoryInfo(basePath);
		}

		public PathFixer() { }

		/// <summary>
		/// Creates a relative path in POSIX format to a file
		/// </summary>
		/// <param name="otherPath">A path to a file or directory</param>
		/// <returns></returns>
		public string CreateRelativePath(string otherPath) {
			DirectoryInfo directoryInfo = new DirectoryInfo(otherPath);
			

			FileInfo fileInfo = null;
			if (!directoryInfo.Exists) {
				fileInfo = new FileInfo(otherPath);
				if (!fileInfo.Exists) {
					//return null;
					return fileInfo.Name;
				}
				directoryInfo = fileInfo.Directory;
				if (directoryInfo == null) return null;
			}
			string path = "";

		
			string firstSameParentPath = otherPath.Substring(0, GetLengthSame(BaseDirectory.FullName, otherPath));
			DirectoryInfo firstSameParent = new DirectoryInfo(firstSameParentPath);
			
			DirectoryInfo follow = new DirectoryInfo(BaseDirectory.FullName);
			while (!follow.FullName.Equals(firstSameParent.FullName)) {
				path += "../";
				follow = follow.Parent;
				if (follow == null) return null;
			}
			
			var upDirs = new Stack<string>();
			follow = directoryInfo;
			while (!follow.FullName.Equals(firstSameParent.FullName)) {
				upDirs.Push(follow.Name);
				follow = follow.Parent;
				if (follow == null) return null;
			}


			while (upDirs.Count > 0) {
				path += upDirs.Pop() + "/";
			}

			
			if (fileInfo != null) {
				path += fileInfo.Name;
			}
			
			
			return path;
		}

		private int GetLengthSame(string a, string b) {
			int index;
			int min = a.Length < b.Length ? a.Length : b.Length;
			
			for (index = 0; index < min && a[index] == b[index]; index++) { }
			
			return index;
		}
	}
}