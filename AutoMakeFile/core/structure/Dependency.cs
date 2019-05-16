using System.IO;

namespace AutoMakeFile.core.structure {
	public class Dependency {
		
		public FileNode dependency { get; }
		public FileNode dependent { get; }

		public Dependency(FileNode dependency, FileNode dependent) {
			this.dependency = dependency;
			this.dependent = dependent;
		}

		protected bool Equals(Dependency other) {
			return Equals(dependency, other.dependency) && Equals(dependent, other.dependent);
		}

		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Dependency) obj);
		}

		public override int GetHashCode() {
			unchecked {
				return ((dependency != null ? dependency.GetHashCode() : 0) * 397) ^ (dependent != null ? dependent.GetHashCode() : 0);
			}
		}

		public override string ToString() {
			return $"{nameof(dependency)}: {dependency}, {nameof(dependent)}: {dependent}";
		}
	}
}