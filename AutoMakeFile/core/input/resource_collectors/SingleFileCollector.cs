using System.IO;

namespace AutoMakeFile.core.input.resource_collectors {
	
	// TODO: Implement single file collecting
	public class SingleFileCollector : SingleCollector {
		
		
		public SingleFileCollector(string path) : base(path) { }
		protected override FileInfo GetFile() {
			throw new System.NotImplementedException();
		}

		public override bool Verify() {
			throw new System.NotImplementedException();
		}
	}
}