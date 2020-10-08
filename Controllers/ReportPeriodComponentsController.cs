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
	[RoutePrefix("api/ReportPeriodComponents")]
	public class ReportPeriodComponentsController : ApiController
	{
		private readonly BFSContext _db = new BFSContext();

		// GET api/ReportPeriodComponents
		public IQueryable<ReportPeriodComponent> GetAllReportPeriodComponents()
		{
			return from s in _db.ReportPeriodComponents
				   select s;
		}

		// GET api/ReportPeriodComponents/cf8989b2-c120-400b-9ccf-4488d482c47b
		public async Task<IHttpActionResult> GetReportPeriodComponent(Guid id)
		{
			ReportPeriodComponent periodComponent =
				await _db.ReportPeriodComponents
				.Where(rpc => rpc.Id == id)
				.Include(rpc => rpc.StoredFormData)
					.ThenInclude(sf => sf.Organization)
				.Include(rpc => rpc.FormsBundle)
				.Include(rpc => rpc.ReportPeriod)
				.Include(rpc => rpc.ReportSubmitChain)
				.FirstOrDefaultAsync();

			if (periodComponent == null)
			{
				return NotFound();
			}

			return Ok(periodComponent);
		}

		// GET api/ReportPeriodComponents?periodId=4811d2e9-34bc-4aa3-939f-dedefa475d68&name=18.%2002.10.2020&code=018
		public async Task<IHttpActionResult> GetReportPeriodComponentByCode(
			Guid? periodId,
			string name = null,
			string code = null)
		{
			var request =
				_db.ReportPeriodComponents
				.Where(rpc => rpc.ReportPeriodId == periodId);

			if (code != null || name != null)
			{
				ReportPeriodComponent periodComponent =
					await request
					.Where(rpc => name == null || rpc.Name == name)
					.Where(rpc => code == null || rpc.Code == code)
					.Include(rpc => rpc.StoredFormData)
					.Include(rpc => rpc.FormsBundle)
					.Include(rpc => rpc.ReportPeriod)
					.Include(rpc => rpc.ReportSubmitChain)
					.FirstOrDefaultAsync();

				if (periodComponent != null)
				{
					return Ok(periodComponent);
				}
			}
			else
			{
				List<ReportPeriodComponent> periodComponents =
					await request
					.OrderBy(rpc => rpc.Code)
					.ToListAsync();

				if (periodComponents != null)
				{
					return Ok(periodComponents);
				}
			}

			return NotFound();
			
			
		}
	}
}
