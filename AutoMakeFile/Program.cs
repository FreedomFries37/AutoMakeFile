using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutoMakeFile.core.input;
using AutoMakeFile.core.input.sorting_methods;
using AutoMakeFile.core.reactor;
using AutoMakeFile.core.reactor.comp_settings;
using AutoMakeFile.core.structure;

namespace AutoMakeFile {
	
	class Program {

		public static List<string> FileEndings => CompilationSettings.FileEndings;
		public static CompilationSettings CompilationSettings { get; set; } = CompilationSettings.CCompiler;

		static void Main(string[] args) {
			
			
			FileTracker fileTracker = new FileTracker();


			fileTracker.AddDirectory(args[0]);
			fileTracker.Update();
			fileTracker.FileComparer = new ModifiedSort();
			fileTracker.Sort();
			fileTracker.PrintFiles();
			ExecutableProjectGraph executableProjectGraph = ExecutableProjectGraph.GenerateProjectGraph(fileTracker);
			
			
			executableProjectGraph.PrintStructure();
			
			MakeFileGenerator generator = new MakeFileGenerator(args[1]);
			generator.BaseDirectory =
				@"C:\Users\Joshua\Documents\ProgrammingWorkspace\AutoMakeFile\workingdirectory\test_project2";
			generator.ProjectGraph = executableProjectGraph;
			generator.GenerateRules();
			generator.OutputFile();
		}
	}
}