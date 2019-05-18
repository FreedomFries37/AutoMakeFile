using System.Collections.Generic;

namespace AutoMakeFile.core.reactor.comp_settings {
	public class CompilationSettings {
		/// <summary>
		/// All File endings
		/// </summary>
		public List<string> FileEndings { get; }

		/// <summary>
		/// File endings to create rules for
		/// </summary>
		public List<string> RuleFileEnding { get; }

		/// <summary>
		/// Compiler to use
		/// </summary>
		public string Compiler { get; }


		private CompilationSettings(List<string> fileEndings, List<string> ruleFileEnding, string compiler) {
			FileEndings = fileEndings;
			RuleFileEnding = ruleFileEnding;
			Compiler = compiler;
		}

		public static readonly CompilationSettings CCompiler;


		static CompilationSettings() {
			CCompiler = new CompilationSettings(new List<string> {".c", ".h"}, new List<string> {".c"}, "gcc");
			
		}
	}
}