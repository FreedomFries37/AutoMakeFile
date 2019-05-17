using System.Collections.Generic;
using AutoMakeFile.core.structure;

namespace AutoMakeFile.core.reactor {
	public class MakeFileGenerator {
		public string ExecutableName { get; set; }
		public bool IncludeClean { get; set; } = true;
		public CStandard CStandard { get; set; } = CStandard.C99;
		private List<string> _flags;
		public string[] Flags => _flags?.ToArray();

		public RuleList RuleList { get; }
		public ExecutableProjectGraph ProjectGraph { get; set; }

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
			
		}
	}
}