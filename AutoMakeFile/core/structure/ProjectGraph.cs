using System.Collections.Generic;
using System.IO;
using AutoMakeFile.core.input;

namespace AutoMakeFile.core.structure {
	public class ProjectGraph {
		private HashSet<FileNode> nodes { get; }
		private HashSet<Dependency> edges { get; }

		private Dictionary<FileNode, Dependency> dependencies;

		private ProjectGraph() {
			nodes = new HashSet<FileNode>();
			edges = new HashSet<Dependency>();
			dependencies = new Dictionary<FileNode, Dependency>();
		}

		public static ProjectGraph GenerateProjectGraph(FileTracker t) {
			List<FileInfo> fileInfos = t.GetFiles();
			ProjectGraph output = new ProjectGraph();
			foreach (FileInfo fileInfo in fileInfos) {
				output.nodes.Add(new FileNode(fileInfo));
			}







			return output;
		}
	}
}