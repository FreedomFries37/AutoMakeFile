using System.Collections.Generic;
using AutoMakeFile.core.reactor.rules;

namespace AutoMakeFile.core.reactor {
	public class RuleList : List<Rule>{
		public RuleList() { }
		public RuleList(IEnumerable<Rule> collection) : base(collection) { }
		
		
	}
}