using System;
using System.Collections.Generic;

namespace PKMIAC.BARSFormStatus.Models
{
	public partial class FormBundle
	{
		public FormBundle()
		{
			ReportPeriodComponent = new HashSet<ReportPeriodComponent>();
		}

		public string Code { get; set; }
		public bool? IsDisabled { get; set; }
		public Guid? Id { get; set; }
		public string Name { get; set; }

		public virtual ICollection<ReportPeriodComponent> ReportPeriodComponent { get; set; }
	}
}
