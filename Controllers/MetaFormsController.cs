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
using System.Web.Http.Description;

namespace PKMIAC.BARSFormStatus.Controllers
{
	[RoutePrefix("api/MetaForms")]
	public class MetaFormsController : ApiController
	{
		private readonly BFSContext _db = new BFSContext();

		/// <summary>
		/// Получить список метаописаний отчетных форм. Опционально
		/// фильтровать по код или наименованию формы
		/// 
		/// GET api/MetaForms?code=ДЗПК_М_ИнфоОФедералИКраевДоплатахРаботникам&name=Информация%20о%20федеральных%20и%20краевых%20стимулирующих%20доплатах%20медицинским%20и%20иным%20работникам
		/// </summary>
		/// <param name="name">Намиенование формы</param>
		/// <param name="code">Код формы</param>
		/// <returns>Список метаописаний отчетных форм</returns>
		// GET api/MetaForms?code=ДЗПК_М_ИнфоОФедералИКраевДоплатахРаботникам&name=Информация%20о%20федеральных%20и%20краевых%20стимулирующих%20доплатах%20медицинским%20и%20иным%20работникам
		[ResponseType(typeof(List<MetaForm>))]
		public async Task<IHttpActionResult> GetMetaForms(string name = null, string code = null)
		{
			if (name != null || code != null)
			{
				MetaForm metaForm =
					await _db.MetaForms
					.Where(mf => name == null || mf.Name == name)
					.Where(mf => code == null || mf.Code == code)
					.FirstOrDefaultAsync();

				if (metaForm != null)
				{
					return Ok(metaForm);
				}
			}
			else
			{
				List<MetaForm> metaForms =
					await _db.MetaForms.ToListAsync();

				return Ok(metaForms);
			}

			return NotFound();
		}

		/// <summary>
		/// Получить конктреное метаописание отчетной формы
		/// 
		/// GET api/MetaForms/1bf727cb-da31-4c2b-a2c8-57ecb99e5373
		/// </summary>
		/// <param name="id">Уникальный идентификатор отчетной формы</param>
		/// <returns>Метаописание отчетной формы</returns>
		// GET api/MetaForms/1bf727cb-da31-4c2b-a2c8-57ecb99e5373
		[ResponseType(typeof(MetaForm))]
		public async Task<IHttpActionResult> GetMetaForm(Guid id)
		{
			MetaForm metaForm =
				await _db.MetaForms
				.Where(mf => mf.Id == id)
				.FirstOrDefaultAsync();

			if (metaForm == null)
			{
				return NotFound();
			}

			return Ok(metaForm);
		}
	}
}
