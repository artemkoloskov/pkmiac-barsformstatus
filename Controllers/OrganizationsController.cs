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

		// GET api/Organizations/06caa0d2-3efd-47d9-85fe-1d1ff5899b48?includeStoredFormData=true
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

		// GET api/Organizations?code=030010106&includeStoredFormData=true
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

		// GET api/Organizations?name=КГБУЗ%20"Владивостокская%20клиническая%20больница%20№%201"&includeStoredFormData=true
		public async Task<IHttpActionResult> GetOrganizationByName(string name, bool includeStoredFormData = false)
		{
			IQueryable<Organization> request =
				_db.Organizations
				.Where(o => o.Name == name);

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
