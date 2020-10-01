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
		private readonly BARSContext _db = new BARSContext();

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

		// GET api/FormStatus/GetFormStatus?organizationCode=33322211100&formCode=ДЗПК_М_Мониторинг&periodStart=01.12.2020&periodEnd=31.12.2020
		[Route("GetFormStatus")]
		public async Task<IHttpActionResult> GetFormStatus(string organizationCode, string formCode, string periodStart, string periodEnd)
		{
			DateTime startDate = DateTime.Parse(periodStart);

			DateTime endDate = DateTime.Parse(periodEnd);

			List<StoredFormData> storedFormData = await
				_db.StoredFormDatas.Where(
					sd => sd.OrganizationNavigation.Code == organizationCode && 
					sd.MetaFormCode == formCode &&
					sd.ReportPeriodComponent.ReportPeriod.StartDate == startDate).ToListAsync();

			if (storedFormData == null)
			{
				return NotFound();
			}

			return Ok(storedFormData);
		}
	}
}
