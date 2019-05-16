using System;
using AutoMakeFile.core.input;
using AutoMakeFile.core.input.sorting_methods;
using AutoMakeFile.core.structure;

namespace AutoMakeFile {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine("Hello World!");
			
			FileTracker fileTracker = new FileTracker();


			fileTracker.AddDirectory("test_project");
			fileTracker.Update();
			fileTracker.FileComparer = new ModifiedSort();
			fileTracker.Sort();
			fileTracker.PrintFiles();
			ExecutableProjectGraph executableProjectGraph = ExecutableProjectGraph.GenerateProjectGraph(fileTracker);
			
			
			
			
			
			
		}
	}
}