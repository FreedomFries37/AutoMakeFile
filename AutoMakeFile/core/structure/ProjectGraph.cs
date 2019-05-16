using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using AutoMakeFile.core.input;

namespace AutoMakeFile.core.structure {
	public class ExecutableProjectGraph {
		private HashSet<FileNode> nodes { get; }



		private HashSet<Dependency> edges { get; }

		private Dictionary<FileNode, List<Dependency>> dependencies;

		private FileNode _MainFile;
		public string MainFile => _MainFile.Name;



		private ExecutableProjectGraph() {
			nodes = new HashSet<FileNode>();
			edges = new HashSet<Dependency>();
			dependencies = new Dictionary<FileNode, List<Dependency>>();
		}

		public static ExecutableProjectGraph GenerateProjectGraph(FileTracker t) {
			List<FileInfo> fileInfos = t.GetFiles();
			ExecutableProjectGraph output = new ExecutableProjectGraph();
			foreach (FileInfo fileInfo in fileInfos) {
				output.nodes.Add(new FileNode(fileInfo));
			}

			if (!output.FindMain()) return null;

			var nodeStack = new Stack<FileNode>();
			var visited = new HashSet<FileNode>();
			nodeStack.Push(output._MainFile);

			while (nodeStack.Count > 0) {
				var current = nodeStack.Pop();
				visited.Add(current);

				foreach (var dependency in GetDependencies(current.FullName)) {
					var fileNode = output[dependency];
					output.AddDependency(fileNode, current);
					if (!visited.Contains(fileNode)) {
						nodeStack.Push(fileNode);
					}
				}
			}


			return output;
		}

		protected bool AddDependency(FileNode dependency, FileNode dependent) {
			var dep = new Dependency(dependency, dependent);
			if (dependencies[dependent].Contains(dep)) return false;
			edges.Add(dep);
			dependencies[dependent].Add(dep);
			return true;
		}

		protected FileNode this[string fullName] {
			get {
				foreach (FileNode node in nodes) {
					if (node.FullName.Equals(fullName)) return node;
				}
				throw new FileNotFoundException();
			}
		}

		public List<string> Files() => new List<string>(from f in nodes select f.FullName);
		public List<string> Files(string extension) => new List<string>(from f in nodes where f.Extension.Equals(extension) select f.FullName);
		/// <summary>
		/// Gets the list of files that this files requires to compile
		/// </summary>
		/// <returns>the list</returns>
		public static List<string> GetDependencies(string FullName) {
			List<string> output = new List<string>();
			
			
			return output;
		}

		private bool FindMain() {
			Regex mainRegex = new Regex(@"int\s+main\s*\(\s*((int\s+\w+\s*,\s*char\s*\*\s*\w+\s*)|void)?\){");

			FileNode main = null;
			
			foreach (string file in Files(".c")) {
				string allText = File.ReadAllText(file);
				if (mainRegex.Match(allText).Success) {
					if (main == null) {
						main = this[file];
					} else {
						throw new MultipleFilesWithMainException();
					}
				}
			}


			_MainFile = main;

			return main != null;
		}

		[Serializable]
		public class MultipleFilesWithMainException : Exception {
			//
			// For guidelines regarding the creation of new exception types, see
			//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
			// and
			//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
			//

			public MultipleFilesWithMainException() : base("Multiple files where main functions are declared."){ }

			protected MultipleFilesWithMainException(
				SerializationInfo info,
				StreamingContext context) : base(info, context) { }
		}
	}
}