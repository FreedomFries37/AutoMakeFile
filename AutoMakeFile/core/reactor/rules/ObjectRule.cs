using System.IO;

namespace AutoMakeFile.core.reactor.rules {
	public class ObjectRule : IRuleGenerator{
		private string destinationFile { get; }
		private FileInfo[] operants { get; }

		public ObjectRule(string destinationFile, params FileInfo[] operants) {
			this.destinationFile = destinationFile;
			this.operants = operants;
		}

		public Rule GetRule() {
			throw new System.NotImplementedException();
		}
	}
}