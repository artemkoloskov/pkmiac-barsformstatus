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
    [RoutePrefix("api/ReportPeriods")]
    public class ReportPeriodsController : ApiController
    {
		private readonly BARSContext _db = new BARSContext();

		// GET api/ReportPeriods
		public IQueryable<ReportPeriod> GetAllReportPeriods()
		{
			return from s in _db.ReportPeriods
				   select s;
		}

		// GET api/ReportPeriods/qdjn43-ekndjwe-2323nj-2323njn
		public async Task<IHttpActionResult> GetReportPeriod(Guid id)
		{
			ReportPeriod reportPeriod =
				await _db.ReportPeriods.Where(rp => rp.Id == id).Include(rp => rp.ReportPeriodComponent).FirstOrDefaultAsync();

			if (reportPeriod == null)
			{
				return NotFound();
			}

			return Ok(reportPeriod);
		}

		// GET api/ReportPeriods/ReportPeriod?code=Месячные_Октябрь_2020
		[Route("ReportPeriod")]
		public async Task<IHttpActionResult> GetReportPeriodByCode(string code)
		{
			ReportPeriod reportPeriod =
				await _db.ReportPeriods.Where(rp => rp.Code == code).Include(rp => rp.ReportPeriodComponent).FirstOrDefaultAsync();

			if (reportPeriod == null)
			{
				return NotFound();
			}

			return Ok(reportPeriod);
		}
	}
}
