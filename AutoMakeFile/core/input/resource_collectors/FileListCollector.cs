using System.Collections.Generic;
using System.IO;

namespace AutoMakeFile.core.input.resource_collectors {
	
	// TODO: Implement file list collector
	public class FileListCollector : Collector{
		public FileListCollector(string path) : base(path) { }
		public override List<FileInfo> GetFiles() {
			throw new System.NotImplementedException();
		}

		public override bool Verify() {
			throw new System.NotImplementedException();
		}
	}
}