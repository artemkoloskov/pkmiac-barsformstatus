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
	[RoutePrefix("api/MetaForms")]
	public class MetaFormsController : ApiController
	{
		private readonly BFSContext _db = new BFSContext();

		/// <summary>
		/// Получить весь список метаописаний отчетных форм
		/// </summary>
		/// <returns>Список метаописаний отчетных форм</returns>
		// GET api/MetaForms
		public IQueryable<MetaForm> GetAllMetaForms()
		{
			return from s in _db.MetaForms
				   select s;
		}

		/// <summary>
		/// Получить конктреное метаописание отчетной формы
		/// </summary>
		/// <param name="id">Уникальный идентификатор отчетной формы</param>
		/// <returns>Метаописание отчетной формы</returns>
		// GET api/MetaForms/1bf727cb-da31-4c2b-a2c8-57ecb99e5373
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

		/// <summary>
		/// Получить конктреное метаописание отчетной формы
		/// </summary>
		/// <param name="code">Код отчетной формы</param>
		/// <returns>Метаописание отчетной формы</returns>
		// GET api/MetaForms?code=ДЗПК_М_ИнфоОФедералИКраевДоплатахРаботникам
		public async Task<IHttpActionResult> GetMetaFormByCode(string code)
		{
			MetaForm metaForm = 
				await _db.MetaForms
				.Where(mf => mf.Code == code)
				.FirstOrDefaultAsync();

			if (metaForm == null)
			{
				return NotFound();
			}

			return Ok(metaForm);
		}

		/// <summary>
		/// Получить конктреное метаописание отчетной формы
		/// </summary>
		/// <param name="name">Наименование отчетной формы</param>
		/// <returns>Метаописание отчетной формы</returns>
		// GET api/MetaForms?name=Информация%20о%20федеральных%20и%20краевых%20стимулирующих%20доплатах%20медицинским%20и%20иным%20работникам
		public async Task<IHttpActionResult> GetMetaFormByName(string name)
		{
			MetaForm metaForm = 
				await _db.MetaForms
				.Where(mf => mf.Name == name)
				.FirstOrDefaultAsync();

			if (metaForm == null)
			{
				return NotFound();
			}

			return Ok(metaForm);
		}
	}
}
