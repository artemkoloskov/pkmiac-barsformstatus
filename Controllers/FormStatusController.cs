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

		// GET api/FormStatus?organizationCode=33322211100&formCode=ДЗПК_М_Мониторинг&periodCode=Месячные_Июнь_2020&periodComponentCode=010&periodStart=01.12.2020&periodEnd=31.12.2020
		public async Task<IHttpActionResult> GetFormStatus(
			string organizationCode,
			string formCode = null,
			string periodStart = null,
			string periodEnd = null,
			string periodCode = null,
			string periodComponentCode = null)
		{
			DateTime.TryParse(periodStart, out DateTime startDate);

			DateTime.TryParse(periodEnd, out DateTime endDate);

			List<StoredFormData> storedFormData = await _db.StoredFormDatas
				.Where(sd => sd.OrganizationNavigation.Code == organizationCode &&
					formCode == null ? true : sd.MetaFormCode == formCode &&
					periodComponentCode == null ? true : sd.ReportPeriodComponent.Code == periodComponentCode &&
					periodCode == null ? true : sd.ReportPeriodComponent.ReportPeriod.Code == periodCode &&
					startDate == DateTime.MinValue ? true : sd.ReportPeriodComponent.ReportPeriod.StartDate == startDate &&
					endDate == DateTime.MinValue ? true : sd.ReportPeriodComponent.ReportPeriod.EndDate == endDate)
				.ToListAsync();

			if (storedFormData == null)
			{
				return NotFound();
			}

			return Ok(storedFormData);
		}
	}
}
