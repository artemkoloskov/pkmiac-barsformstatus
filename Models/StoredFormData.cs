using System;
using System.Collections.Generic;

namespace PKMIAC.BARSFormStatus.Models
{
	public partial class StoredFormData
	{
		public Guid? OrganizationId { get; set; }
		public string MetaFormCode { get; set; }
		public byte StatusNumber { get; set; }
		public byte InternalConstraintsStatusNumber { get; set; }
		public byte ExternalConstraintsStatusNumber { get; set; }
		public Guid? ReportPeriodComponentId { get; set; }
		public byte SubmitChainElementType { get; set; }
		public byte ExpertStatusNumber { get; set; }
		public Guid? Id { get; set; }

		public virtual ReportPeriodComponent ReportPeriodComponent { get; set; }
		public virtual Organization Organization { get; set; }
	}
}
