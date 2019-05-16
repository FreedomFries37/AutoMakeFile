using System;
using AutoMakeFile.core.input;

namespace AutoMakeFile {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine("Hello World!");
			
			FileTracker fileTracker = new FileTracker();


			fileTracker.AddDirectory("test_project");
			fileTracker.Update();
			fileTracker.Sort(new LexogragicalSort());
			
		}
	}
}