using System;
using System.Collections.Generic;
using System.Linq;
using AutoMakeFile.core.reactor.rules;

namespace AutoMakeFile.core.reactor {
	public class RuleList : List<Rule>{
		public RuleList() { }
		public RuleList(IEnumerable<Rule> collection) : base(collection) { }

		public string[] GetObjectNames() {
			return (from f in this 
				where f.ruleType == Rule.RuleType.OBJ 
				select f.ruleName).ToArray();
		}

		public override string ToString() {
			return string.Join("", this);
		}

		public new void Add(Rule r) {
			Console.WriteLine("Adding Rule " + r.ruleName);
			base.Add(r);
		}
	}
}