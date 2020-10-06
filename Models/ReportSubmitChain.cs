using System;
using System.Collections.Generic;

namespace PKMIAC.BARSFormStatus.Models
{
	public partial class ReportSubmitChain
	{
		public ReportSubmitChain()
		{
			ReportPeriodComponents = new HashSet<ReportPeriodComponent>();

			ChainElements = new HashSet<ReportSubmitChainElement>();
		}

		public string Code { get; set; }
		public bool? IsDisabled { get; set; }
		public Guid? Id { get; set; }
		public string Name { get; set; }

		public virtual ICollection<ReportPeriodComponent> ReportPeriodComponents { get; set; }
		public virtual ICollection<ReportSubmitChainElement> ChainElements { get; set; }
	}
}
