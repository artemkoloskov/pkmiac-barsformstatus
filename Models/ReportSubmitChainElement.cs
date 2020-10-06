using System;
using System.Collections.Generic;

namespace PKMIAC.BARSFormStatus.Models
{
	public partial class ReportSubmitChainElement
	{
		public ReportSubmitChainElement()
		{
			ChildrenElemtsNavigation = new HashSet<ReportSubmitChainElement>();
		}

		public Guid? OrganizationId { get; set; }
		public byte Type { get; set; }
		public Guid? ParentId { get; set; }
		public Guid? ReportSubmitChainId { get; set; }
		public Guid? Id { get; set; }
		public string Name { get; set; }

		public virtual ReportSubmitChainElement ParentChainElementNavigation { get; set; }
		public virtual ICollection<ReportSubmitChainElement> ChildrenElemtsNavigation { get; set; }
		public virtual Organization OrganizationNavigation { get; set; }
		public virtual ReportSubmitChain ReportSubmitChain { get; set; }
	}
}
