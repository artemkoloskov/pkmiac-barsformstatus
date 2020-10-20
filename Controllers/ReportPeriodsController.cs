using PKMIAC.BARSFormStatus.Models;
using PKMIAC.BARSFormStatus.Data;
using System;
using System.Linq;
using System.Web.Http;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System.Configuration;
using System.Web.Http.Tracing;

namespace PKMIAC.BARSFormStatus.Controllers
{
	[RoutePrefix("api/ReportPeriods")]
	public class ReportPeriodsController : ApiController
	{
		private readonly BFSConfig bfsConfig = (BFSConfig)ConfigurationManager.GetSection("bfsConfigs");

		private readonly BFSContext _db = new BFSContext();

		/// <summary>
		/// Получить список всех отчетных периодов
		/// 
		/// GET api/ReportPeriods
		/// </summary>
		/// <returns>Полный список отчетных периодов</returns>
		// GET api/ReportPeriods
		public IQueryable<ReportPeriod> GetAllReportPeriods()
		{
			if (bfsConfig.Logging.TraceEnabled)
			{
				Configuration.Services.GetTraceWriter().Info(Request, "Контроллер " + GetType().Name, MethodBase.GetCurrentMethod().Name);
			}

			return from s in _db.ReportPeriods
				   select s;
		}

		/// <summary>
		/// Получить конкретный отчетный период, с загруженными компонентами
		/// отчетного периода
		/// 
		/// GET api/ReportPeriods/4811d2e9-34bc-4aa3-939f-dedefa475d68
		/// </summary>
		/// <param name="id">Уникальный идентификатор отчетного периода</param>
		/// <returns>Отчетный период</returns>
		// GET api/ReportPeriods/4811d2e9-34bc-4aa3-939f-dedefa475d68
		[ResponseType(typeof(ReportPeriod))]
		public async Task<IHttpActionResult> GetReportPeriod(Guid id)
		{
			if (bfsConfig.Logging.TraceEnabled)
			{
				Configuration.Services.GetTraceWriter().Info(Request, "Контроллер " + GetType().Name, MethodBase.GetCurrentMethod().Name);
			}

			ReportPeriod reportPeriod =
				await _db.ReportPeriods
				.Where(rp => rp.Id == id)
				.Include(rp => rp.ReportPeriodComponents)
				.FirstOrDefaultAsync();

			if (reportPeriod == null)
			{
				return NotFound();
			}

			return Ok(reportPeriod);
		}

		/// <summary>
		/// Получить конкретный отчетный период, с загруженными компонентами
		/// отчетного периода
		/// 
		/// GET api/ReportPeriod?code=ДЗПК_М_ИнфоОФедералИКраевДоплатахРаботникам
		/// </summary>
		/// <param name="code">Код отчетного периода</param>
		/// <returns>>Отчетный период</returns>
		// GET api/ReportPeriod?code=ДЗПК_М_ИнфоОФедералИКраевДоплатахРаботникам
		[ResponseType(typeof(ReportPeriod))]
		public async Task<IHttpActionResult> GetReportPeriodByCode(string code)
		{
			if (bfsConfig.Logging.TraceEnabled)
			{
				Configuration.Services.GetTraceWriter().Info(Request, "Контроллер " + GetType().Name, MethodBase.GetCurrentMethod().Name);
			}

			ReportPeriod reportPeriod =
				await _db.ReportPeriods
				.Where(rp => rp.Code == code)
				.Include(rp => rp.ReportPeriodComponents)
				.FirstOrDefaultAsync();

			if (reportPeriod == null)
			{
				return NotFound();
			}

			return Ok(reportPeriod);
		}
	}
}
