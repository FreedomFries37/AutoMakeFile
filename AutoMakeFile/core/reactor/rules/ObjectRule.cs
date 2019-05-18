using System.IO;
using System.Linq;

namespace AutoMakeFile.core.reactor.rules {
	public class ObjectRule : IRuleGenerator{
		private string destinationFile { get; }
		private string sourceFile { get; }
		private string[] operants { get; }

		private MakeFileGenerator _makeFileGenerator;

		public ObjectRule(MakeFileGenerator makeFileGenerator, string destinationFile, string sourceFile, string[] operants) {
			_makeFileGenerator = makeFileGenerator;
			PathFixer f = new PathFixer(_makeFileGenerator.BaseDirectory);
			
			this.destinationFile = destinationFile;
			this.sourceFile = f.CreateRelativePath(sourceFile);
			this.operants = (from operant in operants select f.CreateRelativePath(operant)).ToArray();
		}


		
		public Rule GetRule() {
			string bashCommand = $"{Program.CompilationSettings.Compiler} -c " +
								$"{_makeFileGenerator.CStandardString()} " +
								$"{string.Join(" ", _makeFileGenerator.Flags.ToList())} " +
								$"{sourceFile}";
								
			
			
			return new Rule(destinationFile, operants, new []{bashCommand}, Rule.RuleType.OBJ);
		}
	}
}