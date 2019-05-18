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
			string bashCommand = $"{Program.CompilationSettings.Compiler} -o " +
								$"{outputName} " +
								$"{string.Join(' ', objectFiles)} -lm";
			
			return new Rule(outputName, objectFiles, new []{bashCommand}, Rule.RuleType.EXE);
		}
	}
}