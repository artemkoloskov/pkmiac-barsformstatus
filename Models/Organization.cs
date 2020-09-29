using System;
using System.Collections.Generic;

namespace PKMIAC.BARSFormStatus.Models
{
	public partial class Organization
	{
		public Organization()
		{
			ChildOrganizations = new HashSet<Organization>();
			StoredFormData = new HashSet<StoredFormData>();
		}

		public string Name { get; set; }
		public string Code { get; set; }
		public Guid? ParentOrganizationId { get; set; }
		public string Region { get; set; }
		public string LocalityType { get; set; }
		public string LocalityName { get; set; }
		public bool? Terminated { get; set; }
		public Guid? Id { get; set; }

		public virtual Organization ParentOrganization { get; set; }
		public virtual ICollection<Organization> ChildOrganizations { get; set; }
		public virtual ICollection<StoredFormData> StoredFormData { get; set; }
	}
}
