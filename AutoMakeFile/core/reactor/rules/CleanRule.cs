using System.Linq;

namespace AutoMakeFile.core.reactor.rules {
	public class CleanRule : IRuleGenerator{
		
		private string[] removeFiles { get; }

		public CleanRule(string[] removeFiles) {
			
			this.removeFiles = removeFiles;
		}


		public Rule GetRule() {
			string bashCommand = $"rm {string.Join(' ', removeFiles)}";
			
			return new Rule("clean", new string[0], new []{bashCommand}, Rule.RuleType.CLEAN);
		}
	}
}