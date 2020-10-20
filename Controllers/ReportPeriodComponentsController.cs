using PKMIAC.BARSFormStatus.Models;
using PKMIAC.BARSFormStatus.Data;
using System;
using System.Collections.Generic;
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
	[RoutePrefix("api/ReportPeriodComponents")]
	public class ReportPeriodComponentsController : ApiController
	{
		private readonly BFSConfig bfsConfig = (BFSConfig)ConfigurationManager.GetSection("bfsConfigs");

		private readonly BFSContext _db = new BFSContext();

		/// <summary>
		/// Получить список всех компонентов отчетных периодов
		/// 
		/// GET api/ReportPeriodComponents
		/// </summary>
		/// <returns>Полный список компонентов отчетных периодов</returns>
		// GET api/ReportPeriodComponents
		public IQueryable<ReportPeriodComponent> GetAllReportPeriodComponents()
		{
			if (bfsConfig.Logging.TraceEnabled)
			{
				Configuration.Services.GetTraceWriter().Info(Request, "Контроллер " + GetType().Name, MethodBase.GetCurrentMethod().Name);
			}

			return from s in _db.ReportPeriodComponents
				   select s;
		}

		/// <summary>
		/// Получить конкретный компонент отчетного периода, с загруженными хранимыми данныи форм (с 
		/// загруженной организацие, которой принадлежать хранимые данные), загруженным пкетом форм,
		/// загруженным отчетным периодом, которому принадлежит компонент, загруженной цеочкой
		/// сдачи отчетности.
		/// 
		/// GET api/ReportPeriodComponents/cf8989b2-c120-400b-9ccf-4488d482c47b
		/// </summary>
		/// <param name="id">Уникальный идентификатор компонента отчетного периода</param>
		/// <returns>Компонент отчетного периода</returns>
		// GET api/ReportPeriodComponents/cf8989b2-c120-400b-9ccf-4488d482c47b
		[ResponseType(typeof(ReportPeriodComponent))]
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

		/// <summary>
		/// Получить список компонентов конкретного отчетного периода, с возможностью фильтрации по
		/// наименованию компонента или его кода. С загруженными хранимыми данныи форм (с 
		/// загруженной организацие, которой принадлежать хранимые данные), загруженным пкетом форм,
		/// загруженным отчетным периодом, которому принадлежит компонент, загруженной цеочкой
		/// сдачи отчетности. Список отсортирован по коду компонента очтетного периода, по возрастанию.
		/// 
		/// GET api/ReportPeriodComponents?periodId=4811d2e9-34bc-4aa3-939f-dedefa475d68&name=18.%2002.10.2020&code=018
		/// </summary>
		/// <param name="periodId">Уникальный идентификатор отчетного периода</param>
		/// <param name="name">Наименование компонента отчетного периода</param>
		/// <param name="code">Код компонента отчетного периода</param>
		/// <returns>Список компонентов отчетного периода</returns>
		// GET api/ReportPeriodComponents?periodId=4811d2e9-34bc-4aa3-939f-dedefa475d68&name=18.%2002.10.2020&code=018
		[ResponseType(typeof(ReportPeriodComponent))]
		public async Task<IHttpActionResult> GetReportPeriodComponentByCode(
			Guid? periodId,
			string name = null,
			string code = null)
		{
			if (bfsConfig.Logging.TraceEnabled)
			{
				Configuration.Services.GetTraceWriter().Info(Request, "Контроллер " + GetType().Name, MethodBase.GetCurrentMethod().Name);
			}

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
