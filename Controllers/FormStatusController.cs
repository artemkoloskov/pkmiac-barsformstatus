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
using System.Web.Http.Tracing;
using System.Configuration;

namespace PKMIAC.BARSFormStatus.Controllers
{
	/// <summary>
	/// Контроллер получения хранимых данных форм
	/// </summary>
	[RoutePrefix("api/FormStatus")]
	public class FormStatusController : ApiController
	{
		private readonly BFSConfig bfsConfig = (BFSConfig)ConfigurationManager.GetSection("bfsConfigs");

		private readonly BFSContext _db = new BFSContext();

		/// <summary>
		/// Получить конкретные хранимые данные формы
		/// 
		/// GET api/FormStatus/711f7a03-4237-4b04-ad7e-64dac0471e61
		/// </summary>
		/// <param name="id">Уникальный идентификатор хранимых данных формы</param>
		/// <returns>Хранимые данные формы с загруженной организацией, к которой
		/// прикреплены данные формы</returns>
		// GET api/FormStatus/711f7a03-4237-4b04-ad7e-64dac0471e61
		[ResponseType(typeof(StoredFormData))]
		public async Task<IHttpActionResult> GetFormStatus(Guid id)
		{
			if (bfsConfig.Logging.TraceEnabled)
			{
				Configuration.Services.GetTraceWriter().Info(Request, "Контроллер " + GetType().Name, MethodBase.GetCurrentMethod().Name);
			}

			StoredFormData storedFormData =
				await _db.StoredFormDatas
				.Where(sd => sd.Id == id)
				.Include(sd => sd.Organization)
				.FirstOrDefaultAsync();

			if (storedFormData == null)
			{
				return NotFound();
			}

			return Ok(storedFormData);
		}

		/// <summary>
		/// Получить список хранимых данных форм, принадлежащих
		/// определенному компоненту отчного периода, с возможностью фильтрации списка
		/// по коду организации. Если организация из цепочки сдачи отчетности
		/// не заполняла отчет, у нее нет хранимых данных, соответственно они не выгружаются
		/// ответом на запрос. Поэтому метод пробегает по цепочке отчетности,
		/// сверяет ее со списком хранимых данных и для тех организаций,
		/// у которых их нет, заполняет список "болванками" хранимых данных, со статусами 0
		/// (не заполнено/не проверено).
		/// 
		/// GET api/FormStatus?periodComponentId=cf8989b2-c120-400b-9ccf-4488d482c47b&organizationCode=030683508
		/// </summary>
		/// <param name="periodComponentId">Уникальный идентификатор компонента отчетного периода
		/// которому принадлежат загружаемые хранимые данные форм</param>
		/// <param name="organizationCode">Код организации, по которой нужно отфильтровать список</param>
		/// <returns></returns>
		// GET api/FormStatus?periodComponentId=cf8989b2-c120-400b-9ccf-4488d482c47b&organizationCode=030683508
		[ResponseType(typeof(List<StoredFormData>))]
		public async Task<IHttpActionResult> GetFormStatus(
			Guid? periodComponentId,
			string organizationCode = null)
		{
			if (bfsConfig.Logging.TraceEnabled)
			{
				Configuration.Services.GetTraceWriter().Info(Request, "Контроллер " + GetType().Name, MethodBase.GetCurrentMethod().Name);
			}

			ReportPeriodComponent reportPeriodComponent = await _db.ReportPeriodComponents.Where(c => c.Id == periodComponentId).FirstOrDefaultAsync();

			ReportSubmitChain reportSubmitChain = await _db.ReportSubmitChains
				.Where(c => c.Id == reportPeriodComponent.ReportSubmitChainId)
				.Include(c => c.ChainElements)
					.ThenInclude(e => e.Organization)
				.FirstOrDefaultAsync();

			List<StoredFormData> storedFormData = await _db.StoredFormDatas
				.Where(sd => !periodComponentId.HasValue || sd.ReportPeriodComponentId == periodComponentId)
				.Where(sd => organizationCode == null || sd.Organization.Code == organizationCode)
				.Include(sd => sd.Organization)
				.ToListAsync();

			foreach (ReportSubmitChainElement chainElement in reportSubmitChain.ChainElements)
			{
				string metaFormCode = storedFormData.Count == 0 ? "" : storedFormData.FirstOrDefault().MetaFormCode;

				if (storedFormData.Where(s => s.OrganizationId == chainElement.OrganizationId).Count() == 0)
				{
					storedFormData.Add(new StoredFormData()
					{
						StatusNumber = 0,
						Organization = chainElement.Organization,
						ExpertStatusNumber = 0,
						ExternalConstraintsStatusNumber = 0,
						InternalConstraintsStatusNumber = 0,
						ReportPeriodComponent = reportPeriodComponent,
						ReportPeriodComponentId = reportPeriodComponent.Id,
						OrganizationId = chainElement.OrganizationId,
						SubmitChainElementType = chainElement.Type,
						MetaFormCode = metaFormCode,
						Id = null,
					});
				}
			}

			if (storedFormData == null)
			{
				return NotFound();
			}

			return Ok(storedFormData);
		}
	}
}
