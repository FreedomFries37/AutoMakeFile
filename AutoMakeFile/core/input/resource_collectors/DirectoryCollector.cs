using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutoMakeFile.core.input.resource_collectors {
	public class DirectoryCollector : Collector{
		public DirectoryCollector(string path) : base(path) { }
		
		
		public override List<FileInfo> GetFiles() {
			List<FileInfo> output = new List<FileInfo>();
			
			foreach (string fileEnding in Program.FileEndings) {
				var cFiles = from f in Directory.EnumerateFiles(path, "*" + fileEnding, SearchOption.AllDirectories)
					where !ignores.Contains(new FileInfo(f).Name)
					select new FileInfo(f);
				output.AddRange(cFiles);
			}
			
			return output;
		}

		public override bool Verify() {
			return Directory.Exists(path);
		}
	}
}