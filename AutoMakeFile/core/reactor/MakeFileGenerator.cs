using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMakeFile.core.reactor.rules;
using AutoMakeFile.core.structure;
using Rule = AutoMakeFile.core.reactor.rules.Rule;

namespace AutoMakeFile.core.reactor {
	public class MakeFileGenerator {
		public string ExecutableName { get; set; }
		public bool IncludeClean { get; set; } = true;
		public CStandard CStandard { get; set; } = CStandard.C99;
		private List<string> _flags;
		public string[] Flags => _flags?.ToArray();
		public string BaseDirectory { get; set; } = Directory.GetCurrentDirectory();
		

		public RuleList RuleList { get; }
		public ExecutableProjectGraph ProjectGraph { get; set; }

		private List<string> _ruleFileEndings => Program.CompilationSettings.RuleFileEnding;

		public MakeFileGenerator(string executableName, params string[] flags) {
			ExecutableName = executableName;
			_flags = new List<string>();
			_flags.AddRange(flags);
			RuleList = new RuleList();
		}


		public void AddFlag(string flag) {
			_flags.Add(flag);
		}

		public void GenerateRules() {
			if (ProjectGraph == null) return;
			// generate object rules
			foreach (string ruleFileEnding in _ruleFileEndings) {
				List<string> files = ProjectGraph.Files(ruleFileEnding);

				foreach (string file in files) {
					var fileInfos =ProjectGraph.GetFullDependencies(file);
					FileInfo basefileInfo = new FileInfo(file);
					fileInfos.Append(file);
					string[] fullDependencies = fileInfos.ToArray();

					string objectFileName = basefileInfo.Name.Replace(basefileInfo.Extension, "") + ".o";
					RuleList.Add(new ObjectRule(this, objectFileName, file, fullDependencies).GetRule());
				}
			}

			string[] objectNames = RuleList.GetObjectNames();
			Rule exeRule = new ExecutableRule(ExecutableName, objectNames).GetRule();
			RuleList.Insert(0, exeRule);

			if (IncludeClean) {
				Rule cleanRule = new CleanRule(objectNames).GetRule();
				RuleList.Add(cleanRule);
			}
		}

		public bool OutputFile() {
			string makefileTxt = Path.Combine(BaseDirectory, "makefile");
			var fileStream = File.Create(makefileTxt);
			var info = new FileInfo(makefileTxt);
			if (!info.Exists) return false;
			
			fileStream.Close();
			File.WriteAllText(info.FullName, RuleList.ToString());
			return true;
		}

		public string CStandardString() {
			string output = "-std=";

			switch (CStandard) {
				case CStandard.C89:
					output += "c89";
					break;
				case CStandard.C99:
					output += "c99";
					break;
				case CStandard.C11:
					output += "c11";
					break;
				case CStandard.C18:
					output += "c18";
					break;
			}



			return output;
		}
	}
}