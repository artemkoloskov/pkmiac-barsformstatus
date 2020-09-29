using System;
using System.Collections.Generic;

namespace PKMIAC.BARSFormStatus.Models
{
	public partial class ReportSubmitChain
	{
		public ReportSubmitChain()
		{
			Komponentotchetnogop = new HashSet<ReportPeriodComponent>();
		}

		public string Code { get; set; }
		public bool? Neispolzuetsya { get; set; }
		public Guid? Id { get; set; }
		public string Name { get; set; }

		public virtual ICollection<ReportPeriodComponent> Komponentotchetnogop { get; set; }
	}
}
