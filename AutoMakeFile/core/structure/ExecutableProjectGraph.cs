using System;
using System.Collections.Generic;
using System.Globalization;
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

			do {
				while (nodeStack.Count > 0) {
					var current = nodeStack.Pop();
					visited.Add(current);

					foreach (var dependency in GetDependencies(current.FullName)) {
						var fileNode = output[dependency];
						if (!output.AddDependency(fileNode, current)) return null;
						if (!visited.Contains(fileNode)) {
							nodeStack.Push(fileNode);
						}
					}
				}

				var fileNodes = from filenode in output.nodes where !visited.Contains(filenode) select filenode;
				foreach (var fileNode in fileNodes) {
					if(!visited.Contains(fileNode)) nodeStack.Push(fileNode);
				}
			} while (visited.Count < fileInfos.Count);


			return output;
		}

		protected bool AddDependency(FileNode dependency, FileNode dependent) {
			var dep = new Dependency(dependency, dependent);
			if (!dependencies.ContainsKey(dependent)) {
				dependencies.Add(dependent, new List<Dependency>());
			} else if (dependencies[dependent].Contains(dep)) return false;
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
		public static List<string> GetDependencies(string fullName) {
			var output = new List<string>();
			
			var includeRegex = new Regex("#\\s*include\\s+\"(?<up>(\\.\\.\\\\)+)?(?<dir>(\\w+\\\\)+)?(?<file_name>\\w+(.h|.c))\"");
			var allText = File.ReadAllText(fullName);

			DirectoryInfo dirInfo = new FileInfo(fullName).Directory;
		
			
			
			var matchCollection = includeRegex.Matches(allText);
			foreach (Match o in matchCollection) {

				string path = "";
				
				var included = o.Groups["file_name"];
				var upDirsGroup = o.Groups["up"];
				
				
				if (upDirsGroup.Success) {
					string upDirsString = upDirsGroup.Value;
					while (upDirsString.Contains(@"..\")) {
						upDirsString = upDirsString.Remove(0, 3);

						dirInfo = dirInfo?.Parent;
					}
					
				}

				path += dirInfo.FullName;
				
				var dirGroup = o.Groups["dir"];
				if (dirGroup.Success) {
					path += dirGroup.Value;
				} else {
					path += "\\";
				}

				path += included.Value;

				output.Add(path);
			}

			return output;
		}

		public void PrintStructure() {
			var order = new List<FileNode> {_MainFile};
			order.AddRange(from f in nodes where !f.Equals(_MainFile) select f);
			
			foreach (FileNode fileNode in order) {
				var nodeStack = new Stack<FileNode>();
				var indentStack = new Stack<int>();
				
				nodeStack.Push(fileNode);
				indentStack.Push(0);
				
				Console.WriteLine("Dependency Tree for " + fileNode.Name + ":");
				
				while (nodeStack.Count > 0) {
					var cNode = nodeStack.Pop();
					var cIndent = indentStack.Pop();
					
					for (int i = 0; i < cIndent; i++) {
						Console.Write("\t");
					}
					Console.WriteLine(cNode.Name);

					foreach (var dependency in GetDependencies(cNode.FullName)) {
						var f = this[dependency];
						nodeStack.Push(f);
						indentStack.Push(cIndent + 1);
					}
				}
				
				Console.WriteLine();
			}
			
		}

		public override string ToString() {
			return $"[ProjectGraph  File Count: {nodes.Count}]";
		}

		private bool FindMain() {
			Regex mainRegex = new Regex(@"int\s+main\s*\(\s*((int\s+\w+\s*,\s*char\s*\*\s*\w+\s*\[\s*\]\s*)|void)?\)\s*{");

			FileNode main = null;
			List<string> files = Files(".c");
			foreach (string file in files) {
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