namespace AutoMakeFile.core.reactor.rules {
	public class Rule {

		public enum RuleType {
			EXE,
			OBJ,
			CLEAN
		}
		public string ruleName { get; }
		public string[] operatorFiles { get; }
		public string[] bashCommands { get; }

		public RuleType ruleType { get; }

		

		public Rule(string ruleName, string[] operatorFiles, string[] bashCommands, RuleType ruleType) {
			this.ruleName = ruleName;
			this.operatorFiles = operatorFiles;
			this.bashCommands = bashCommands;
			this.ruleType = ruleType;
		}

		public override string ToString() {
			return $@"{ruleName}: {string.Join(" ", operatorFiles)}
	{string.Join('\n', bashCommands)}

";
		}
	}
}