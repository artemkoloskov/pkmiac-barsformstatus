using PKMIAC.BARSFormStatus.Models;
using PKMIAC.BARSFormStatus.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace PKMIAC.BARSFormStatus.Controllers
{
	[RoutePrefix("api/Organizations")]
	public class OrganizationsController : ApiController
	{
		private readonly BARSContext _db = new BARSContext();

		// GET api/Organizations
		public IQueryable<Organization> GetAllOrganizations()
		{
			return from s in _db.Organizations
				   select s;
		}

		// GET api/Organizations/qdjn43-ekndjwe-2323nj-2323njn
		public async Task<IHttpActionResult> GetOrganization(Guid id)
		{
			Organization organization =
				await _db.Organizations.Where(o => o.Id == id).Include(o => o.StoredFormData).FirstOrDefaultAsync();

			if (organization == null)
			{
				return NotFound();
			}

			return Ok(organization);
		}

		// GET api/Organizations/Organization?code=33322211100
		[Route("Organization")]
		public async Task<IHttpActionResult> GetOrganizationByCode(string code)
		{
			Organization organization =
				await _db.Organizations.Where(o => o.Code == code).Include(o => o.StoredFormData).FirstOrDefaultAsync();

			if (organization == null)
			{
				return NotFound();
			}

			return Ok(organization);
		}
	}
}
