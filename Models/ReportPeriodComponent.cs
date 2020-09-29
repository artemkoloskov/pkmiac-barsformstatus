using System;
using System.Collections.Generic;

namespace PKMIAC.BARSFormStatus.Models
{
	public partial class ReportPeriodComponent
	{
		public ReportPeriodComponent()
		{
			StoredFormData = new HashSet<StoredFormData>();
		}

		public string Code { get; set; }
		public Guid? FormsBundleId { get; set; }
		public Guid? ReportSubmitChainId { get; set; }
		public Guid? ReportPeriodId { get; set; }
		public string EqualGroup { get; set; }
		public Guid? Id { get; set; }
		public string Name { get; set; }
		public bool? Disabled { get; set; }

		public virtual ReportSubmitChain ReportSubmitChain { get; set; }
		public virtual ReportPeriod ReportPeriod { get; set; }
		public virtual FormBundle FormsBundleNavigation { get; set; }
		public virtual ICollection<StoredFormData> StoredFormData { get; set; }
	}
}
