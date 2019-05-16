using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using AutoMakeFile.core.input.resource_collectors;
using AutoMakeFile.core.input.sorting_methods;

namespace AutoMakeFile.core.input {
	public class FileTracker {
		private Collection<Collector> collectors { get; }
		private Dictionary<Collector, List<FileInfo>> collectorToFiles;
		private List<FileInfo> files;
		
		public FileSorter FileComparer { protected get; set; } = new LexogragicalSort();

		public FileTracker() {
			collectors = new Collection<Collector>();
			files = new List<FileInfo>();
			collectorToFiles = new Dictionary<Collector, List<FileInfo>>();
		}

		public void Update() {
			foreach (Collector collector in collectors) {
				List<FileInfo> lastList = collectorToFiles[collector];
				List<FileInfo> fileInfos = collector.GetFiles();
				
				// get new files
				IEnumerable<FileInfo> newFileInfos = from file in fileInfos where !lastList.Contains(file) select file;
				// get removed files
				IEnumerable<FileInfo> removeFileInfos = from file in lastList where !fileInfos.Contains(file) select file;
				
				foreach (FileInfo newFileInfo in newFileInfos) {
					Console.WriteLine($"Added file {newFileInfo}");
					lastList.Add(newFileInfo);
					files.Add(newFileInfo);
				}
				
				foreach (FileInfo removeFileInfo in removeFileInfos) {
					Console.WriteLine($"Removed file {removeFileInfo}");
					lastList.Remove(removeFileInfo);
					files.Remove(removeFileInfo);
				}
			}
		}

		public void Sort() {
			files.Sort(FileComparer);
		}

		public void PrintFiles() {
			Console.WriteLine("Files included:");
			foreach (var fileInfo in files) {
				Console.WriteLine("\t" + fileInfo.FullName);
			}
		}

		public void Sort(FileSorter comparer) {
			var oldFileSorter = FileComparer;
			FileComparer = comparer;
			Sort();
			FileComparer = oldFileSorter;
		}

		public bool AddCollector(Collector c) {
			if (collectors.Contains(c)) return false;
			collectors.Add(c);
			collectorToFiles.Add(c, new List<FileInfo>());
			return true;
		}

		public bool AddDirectory(string path, params string[] ignores) {
			var directoryCollector = new DirectoryCollector(path);
			directoryCollector.ignores.AddRange(ignores);
			return AddCollector(directoryCollector);
		}

		public List<FileInfo> GetFiles() {
			return new List<FileInfo>(files);
		}
		
	}
}