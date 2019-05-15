using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutoMakeFile.core.input.resource_collectors {
	public class DirectoryCollector : Collector{
		public DirectoryCollector(string path) : base(path) { }
		
		
		public override List<FileInfo> GetFiles() {
			List<FileInfo> output = new List<FileInfo>();
			var cFiles = from f in Directory.EnumerateFiles(path, "*.c", SearchOption.AllDirectories)
				where !ignores.Contains(new FileInfo(f).Name)
				select new FileInfo(f);
			output.AddRange(cFiles);
			var hFiles = from f in Directory.EnumerateFiles(path, "*.h", SearchOption.AllDirectories)
				where !ignores.Contains(new FileInfo(f).Name)
				select new FileInfo(f);
			
			return output;
		}
	}
}