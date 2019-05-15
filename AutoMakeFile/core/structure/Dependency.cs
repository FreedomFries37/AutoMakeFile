using System.IO;

namespace AutoMakeFile.core.structure {
	public class Dependency {
		
		public FileNode dependency { get; }
		public FileNode dependent { get; }

		public Dependency(FileNode dependency, FileNode dependent) {
			this.dependency = dependency;
			this.dependent = dependent;
		}

		public bool UpdateNeeded() {
			dependency.Refresh();
			dependent.Refresh();
			return dependency.LastWriteTimeUtc > dependent.LastWriteTimeUtc;
		}
	}
}