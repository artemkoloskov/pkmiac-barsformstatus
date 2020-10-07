using System;
using System.Collections.Generic;

namespace PKMIAC.BARSFormStatus.Models
{
	public partial class ReportSubmitChainElement
	{
		public ReportSubmitChainElement()
		{
			ChildrenElemts = new HashSet<ReportSubmitChainElement>();
		}

		public Guid? OrganizationId { get; set; }
		public byte Type { get; set; }
		public Guid? ParentId { get; set; }
		public Guid? ReportSubmitChainId { get; set; }
		public Guid? Id { get; set; }
		public string Name { get; set; }

		public virtual ReportSubmitChainElement ParentChainElement { get; set; }
		public virtual ICollection<ReportSubmitChainElement> ChildrenElemts { get; set; }
		public virtual Organization Organization { get; set; }
		public virtual ReportSubmitChain ReportSubmitChain { get; set; }
	}
}
