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
	[RoutePrefix("api/SubmitChainElements")]
	public class SubmitChainElementsController : ApiController
	{
		private readonly BFSContext _db = new BFSContext();

		/// <summary>
		/// Получить конкретный элемент цепочки сдачи отчетности, с загруженной
		/// цепочкой, родительским элементом цепочки, элементами - потомками,
		/// организацие, котороая является данным элементом цепочки.
		/// 
		/// GET api/SubmitChainElements/2804d900-3477-443a-9aa9-4f74b7f924f7
		/// </summary>
		/// <param name="id">Уникальный идентификатор элемента цепочки сдачи отчетности</param>
		/// <returns>Элемент цепочки отчетности</returns>
		// GET api/SubmitChainElements/2804d900-3477-443a-9aa9-4f74b7f924f7
		[ResponseType(typeof(ReportSubmitChainElement))]
		public async Task<IHttpActionResult> GetReportSubmitChainElement(Guid id)
		{
			ReportSubmitChainElement chainElement = await _db.ReportSubmitChainElements
				.Where(ce => ce.Id == id)
				.Include(ce => ce.ReportSubmitChain)
				.Include(ce => ce.ParentChainElement)
				.Include(ce => ce.Organization)
				.Include(ce => ce.ChildrenElements)
				.FirstOrDefaultAsync();

			if (chainElement == null)
			{
				return NotFound();
			}

			return Ok(chainElement);
		}

		/// <summary>
		/// Получить список элементов конкретной цепочки отчетности.
		/// 
		/// GET api/SubmitChainElements?chainId=d3f29683-75ab-46da-844e-a5416508482b
		/// </summary>
		/// <param name="chainId">Уникальный идентификтор цепочки отчетности</param>
		/// <returns>Список элементов цеопчки отчетности</returns>
		// GET api/SubmitChainElements?chainId=d3f29683-75ab-46da-844e-a5416508482b
		[ResponseType(typeof(List<ReportSubmitChainElement>))]
		public async Task<IHttpActionResult> GetReportSubmitChainElementByChainCode(Guid? chainId)
		{
			List<ReportSubmitChainElement> chainElements = await _db.ReportSubmitChainElements
				.Where(ce => ce.ReportSubmitChain.Id == chainId)
				.ToListAsync();

			if (chainElements == null)
			{
				return NotFound();
			}

			return Ok(chainElements);
		}
	}
}
