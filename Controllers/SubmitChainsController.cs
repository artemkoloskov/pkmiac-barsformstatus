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
	[RoutePrefix("api/SubmitChains")]
	public class SubmitChainsController : ApiController
	{
		private readonly BFSContext _db = new BFSContext();

		/// <summary>
		/// Получить список цепочек сдачи отчетности, с возможностью фильтрации по
		/// коду цепочки. Фильтрованый список содержит загруженые компоненты отчетных периодов,
		/// к которым относится цепочка.
		/// 
		/// GET api/SubmitChains
		/// </summary>
		/// <param name="code">Код цепочки сдачи отчтености</param>
		/// <returns>Список цепочек сдачи отчетности</returns>
		// GET api/SubmitChains
		[ResponseType(typeof(List<ReportSubmitChain>))]
		public async Task<IHttpActionResult> GetAllSubmitChains(string code = null)
		{
			var request = 
				_db.ReportSubmitChains
				.Where(sc => code == null || sc.Code == code);

			if (code != null)
			{
				request = request
					.Include(sc => sc.ReportPeriodComponents);
			}

			List<ReportSubmitChain> submitChains = 
				await request.ToListAsync();

			if (submitChains == null)
			{
				return NotFound();
			}

			return Ok(submitChains);
		}

		/// <summary>
		/// Получить конкретную цепочку сдачи отчетности с загружеными компонентами отчетных периодов,
		/// к которым относится цепочка.
		/// 
		/// GET api/SubmitChains/d3f29683-75ab-46da-844e-a5416508482b
		/// </summary>
		/// <param name="id">Уникальный идентификтор цепочки</param>
		/// <returns>Цепочка сдачи отчетности</returns>
		// GET api/SubmitChains/d3f29683-75ab-46da-844e-a5416508482b
		[ResponseType(typeof(ReportSubmitChain))]
		public async Task<IHttpActionResult> GetSubmitChain(Guid id)
		{
			ReportSubmitChain submitChain = await _db.ReportSubmitChains
				.Where(sc => sc.Id == id)
				.Include(sc => sc.ReportPeriodComponents)
				.FirstOrDefaultAsync();

			if (submitChain == null)
			{
				return NotFound();
			}

			return Ok(submitChain);
		}
	}
}
