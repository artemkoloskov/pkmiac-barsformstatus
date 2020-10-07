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
	[RoutePrefix("api/FormStatus")]
	public class FormStatusController : ApiController
	{
		private readonly BFSContext _db = new BFSContext();

		// GET api/FormStatus/qdjn43-ekndjwe-2323nj-2323njn
		public async Task<IHttpActionResult> GetFormStatus(Guid id)
		{
			StoredFormData storedFormData = await _db.StoredFormDatas
				.Where(sd => sd.Id == id).FirstOrDefaultAsync();

			if (storedFormData == null)
			{
				return NotFound();
			}

			return Ok(storedFormData);
		}

		// GET api/FormStatus?periodComponentId=asfd23erf-sa12-11ds-12ad-24fsdffs&organizationCode=33322211100&formCode=ДЗПК_М_Мониторинг&periodCode=Месячные_Июнь_2020&periodComponentCode=010&periodStart=01.12.2020&periodEnd=31.12.2020
		public async Task<IHttpActionResult> GetFormStatus(
			Guid? periodComponentId,
			string organizationCode = null,
			string formCode = null,
			string periodStart = null,
			string periodEnd = null,
			string periodCode = null,
			string periodComponentCode = null)
		{
			DateTime.TryParse(periodStart, out DateTime startDate);

			DateTime.TryParse(periodEnd, out DateTime endDate);

			List<StoredFormData> storedFormData = await _db.StoredFormDatas
				.Where(sd => sd.ReportPeriodComponentId == periodComponentId)
				.Where(sd => organizationCode == null || sd.Organization.Code == organizationCode)
				.Where(sd => formCode == null || sd.MetaFormCode == formCode)
				.Where(sd => periodComponentCode == null || sd.ReportPeriodComponent.Code == periodComponentCode)
				.Where(sd => periodCode == null || sd.ReportPeriodComponent.ReportPeriod.Code == periodCode)
				.Where(sd => startDate == DateTime.MinValue || sd.ReportPeriodComponent.ReportPeriod.StartDate == startDate)
				.Where(sd => endDate == DateTime.MinValue || sd.ReportPeriodComponent.ReportPeriod.EndDate == endDate)
				.Include(sd => sd.Organization)
				.ToListAsync();

			if (storedFormData == null)
			{
				return NotFound();
			}

			return Ok(storedFormData);
		}
	}
}
