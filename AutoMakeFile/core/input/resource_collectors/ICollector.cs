using System.Collections.Generic;
using System.IO;

namespace AutoMakeFile.core.input.resource_collectors {
	public interface ICollector {
		/// <summary>
		/// Gets the file(s) that the collector has been designated to obtain
		/// </summary>
		/// <returns>A list of files</returns>
		List<FileInfo> GetFiles();
		
		/// <summary>
		/// Verifies that the collector is attached to the correct type of file
		/// </summary>
		/// <returns>the verification</returns>
		bool Verify();
	}
}