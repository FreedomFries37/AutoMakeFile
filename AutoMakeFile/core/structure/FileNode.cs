using System;
using System.Collections.Generic;
using System.IO;

namespace AutoMakeFile.core.structure {
	public class FileNode {

		private FileInfo FileInfo { get; }

		public FileNode(FileInfo fileInfo) {
			FileInfo = fileInfo;
		}

		public override string ToString() {
			return $"[{nameof(FileInfo)}: {FileInfo}]";
		}

		public void Refresh() {
			FileInfo.Refresh();
		}

		public string Extension => FileInfo.Extension;

		public string FullName => FileInfo.FullName;

		public DateTime LastWriteTimeUtc {
			get => FileInfo.LastWriteTimeUtc;
			set => FileInfo.LastWriteTimeUtc = value;
		}

		public string Name => FileInfo.Name;

		
		/// <summary>
		/// Gets the list of files that this files requires to compile
		/// </summary>
		/// <returns>the list</returns>
		public List<string> GetDependencies() {
			
			
			return new List<string>();
		}
	}
}