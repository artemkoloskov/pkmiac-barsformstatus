using Microsoft.EntityFrameworkCore;
using PKMIAC.BARSFormStatus.Data;
using PKMIAC.BARSFormStatus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace PKMIAC.BARSFormStatus.Controllers
{
	[RoutePrefix("api/FormBundles")]
	public class FormBundlesController : ApiController
	{
		private readonly BFSContext _db = new BFSContext();

		/// <summary>
		/// Получить полный список доступных пакетов форм
		/// 
		/// GET api/FormBundles
		/// </summary>
		/// <returns>Списиок пакетов форм</returns>
		// GET api/FormBundles
		public IQueryable<FormBundle> GetAllFormBundles()
		{
			return from s in _db.FormBundles
				   select s;
		}

		/// <summary>
		/// Получить конкретный пакет форм
		/// 
		/// GET api/FormBundles/03b15001-87cf-45ba-be9b-093c3de47388
		/// </summary>
		/// <param name="id">Уникальный идентификатор пакета форм</param>
		/// <returns>Пакет форм с загруженым списком компонентов отчетных периодов,
		/// которые его используют</returns>
		// GET api/FormBundles/03b15001-87cf-45ba-be9b-093c3de47388
		[ResponseType(typeof(FormBundle))]
		public async Task<IHttpActionResult> GetFormBundle(Guid id)
		{
			FormBundle formBundle = await _db.FormBundles
				.Where(b => b.Id == id)
				.Include(b => b.ReportPeriodComponents)
				.FirstOrDefaultAsync();

			if (formBundle == null)
			{
				return NotFound();
			}

			return Ok(formBundle);
		}
	}
}
