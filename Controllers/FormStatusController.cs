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

		/// <summary>
		/// Получить конкретные хранимые данные формы
		/// </summary>
		/// <param name="id">Уникальный идентификатор хранимых данных формы</param>
		/// <returns>Хранимые данные формы с загруженной организацией, к которой
		/// прикреплены данные формы</returns>
		// GET api/FormStatus/711f7a03-4237-4b04-ad7e-64dac0471e61
		public async Task<IHttpActionResult> GetFormStatus(Guid id)
		{
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
		/// по коду организации
		/// </summary>
		/// <param name="periodComponentId">Уникальный идентификатор компонента отчетного периода
		/// которому принадлежат загружаемые хранимые данные форм</param>
		/// <param name="organizationCode">Код организации, по которой нужно отфильтровать список</param>
		/// <returns></returns>
		// GET api/FormStatus?periodComponentId=cf8989b2-c120-400b-9ccf-4488d482c47b&organizationCode=030683508
		public async Task<IHttpActionResult> GetFormStatus(
			Guid? periodComponentId,
			string organizationCode = null)
		{
			List<StoredFormData> storedFormData = await _db.StoredFormDatas
				.Where(sd => !periodComponentId.HasValue || sd.ReportPeriodComponentId == periodComponentId)
				.Where(sd => organizationCode == null || sd.Organization.Code == organizationCode)
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
