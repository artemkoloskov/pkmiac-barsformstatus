using Microsoft.EntityFrameworkCore;
using PKMIAC.BARSFormStatus.Data;
using PKMIAC.BARSFormStatus.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace PKMIAC.BARSFormStatus.Controllers
{
	[RoutePrefix("api/Organizations")]
	public class OrganizationsController : ApiController
	{
		private readonly BFSContext _db = new BFSContext();

		// GET api/Organizations
		public IQueryable<Organization> GetAllOrganizations()
		{
			return from s in _db.Organizations
				   select s;
		}

		// GET api/Organizations/qdjn43-ekndjwe-2323nj-2323njn?includeStoredFormData=true
		public async Task<IHttpActionResult> GetOrganization(Guid id, bool includeStoredFormData = false)
		{
			IQueryable<Organization> request =
				_db.Organizations
				.Where(o => o.Id == id);

			Organization organization;

			if (includeStoredFormData)
			{
				organization = await request.Include(o => o.StoredFormData).FirstOrDefaultAsync();
			}
			else
			{
				organization = await request.FirstOrDefaultAsync();
			}

			if (organization == null)
			{
				return NotFound();
			}

			return Ok(organization);
		}

		// GET api/Organizations?code=33322211100&includeStoredFormData=true
		public async Task<IHttpActionResult> GetOrganizationByCode(string code, bool includeStoredFormData = false)
		{
			IQueryable<Organization> request =
				_db.Organizations
				.Where(o => o.Code == code);

			Organization organization;

			if (includeStoredFormData)
			{
				organization = await request.Include(o => o.StoredFormData).FirstOrDefaultAsync();
			}
			else
			{
				organization = await request.FirstOrDefaultAsync();
			}

			if (organization == null)
			{
				return NotFound();
			}

			return Ok(organization);
		}
	}
}
