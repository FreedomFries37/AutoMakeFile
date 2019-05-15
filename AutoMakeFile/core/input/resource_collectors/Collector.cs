using System.Collections.Generic;
using System.IO;

namespace AutoMakeFile.core.input.resource_collectors {
	public abstract class Collector {
		public string path { get; }
		public List<string> ignores { get; }

		protected Collector(string path) {
			this.path = path;
			ignores = new List<string>();
		}

		public abstract List<FileInfo> GetFiles();

		protected bool Equals(Collector other) {
			return string.Equals(path, other.path);
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Collector) obj);
		}

		public override int GetHashCode() {
			return (path != null ? path.GetHashCode() : 0);
		}
	}

	public abstract class SingleCollector : Collector {
		protected abstract FileInfo GetFile();

		protected SingleCollector(string path) : base(path) { }

		public override List<FileInfo> GetFiles() {
			return new List<FileInfo> {GetFile()};
		}
	}
}