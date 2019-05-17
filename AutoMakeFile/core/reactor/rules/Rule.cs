namespace AutoMakeFile.core.reactor.rules {
	public class Rule {
		public string destinationFile { get; }
		public string[] operatorFiles { get; }
		public string[] bashCommands { get; }

		public Rule(string destinationFile, string[] operatorFiles, string[] bashCommands) {
			this.destinationFile = destinationFile;
			this.operatorFiles = operatorFiles;
			this.bashCommands = bashCommands;
		}
	}
}