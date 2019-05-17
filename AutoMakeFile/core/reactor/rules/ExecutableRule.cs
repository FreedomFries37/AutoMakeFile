using System.IO;

namespace AutoMakeFile.core.reactor.rules {
	public class ExecutableRule : IRuleGenerator{
		private string outputName { get; }
		private string[] objectFiles { get; }

		public ExecutableRule(string outputName, params string[] objectFiles) {
			this.outputName = outputName;
			this.objectFiles = objectFiles;
		}

		public Rule GetRule() {
			throw new System.NotImplementedException();
		}
	}
}