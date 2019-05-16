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
		
		public string Name => FileInfo.Name;

		protected bool Equals(FileNode other) {
			return FullName.Equals(other.FullName);
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((FileNode) obj);
		}

		public override int GetHashCode() {
			return FullName.GetHashCode();
		}
	}
}