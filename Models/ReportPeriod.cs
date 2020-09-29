﻿using System;
using System.Collections.Generic;

namespace PKMIAC.BARSFormStatus.Models
{
	public partial class ReportPeriod
	{
		public ReportPeriod()
		{
			ReportPeriodComponent = new HashSet<ReportPeriodComponent>();
		}

		public string Code { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public bool? Neispolzuetsya { get; set; }
		public bool? Zablokirovan { get; set; }
		public Guid? Id { get; set; }
		public string Name { get; set; }

		public virtual ICollection<ReportPeriodComponent> ReportPeriodComponent { get; set; }
	}
}
