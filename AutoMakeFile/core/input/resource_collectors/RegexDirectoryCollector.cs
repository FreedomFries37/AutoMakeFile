using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AutoMakeFile.core.input.resource_collectors {
	
	// TODO: Implement regex collection within a directory
	public class RegexDirectoryCollector : DirectoryCollector{
		public Regex pattern { get; }

		public RegexDirectoryCollector(string path, string pattern) : base(path) {
			this.pattern = new Regex(pattern);
		}

		public override List<FileInfo> GetFiles() {
			return base.GetFiles();
		}

		public override bool Verify() {
			return base.Verify();
		}
	}
}