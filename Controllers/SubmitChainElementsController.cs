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
	[RoutePrefix("api/SubmitChainElements")]
	public class SubmitChainElementsController : ApiController
	{
		private readonly BFSContext _db = new BFSContext();

		// GET api/SubmitChainElements/qdjn43-ekndjwe-2323nj-2323njn
		public async Task<IHttpActionResult> GetReportSubmitChainElement(Guid id)
		{
			ReportSubmitChainElement chainElement = await _db.ReportSubmitChainElements
				.Where(ce => ce.Id == id)
				.Include(ce => ce.ReportSubmitChain)
				.Include(ce => ce.ParentChainElement)
				.Include(ce => ce.Organization)
				.Include(ce => ce.ChildrenElemts)
				.FirstOrDefaultAsync();

			if (chainElement == null)
			{
				return NotFound();
			}

			return Ok(chainElement);
		}

		// GET api/SubmitChainElements?chainCode=0812%2005
		public async Task<IHttpActionResult> GetReportSubmitChainElementByChainCode(string chainCode)
		{
			ReportSubmitChainElement chainElement = await _db.ReportSubmitChainElements
				.Where(ce => ce.ReportSubmitChain.Code == chainCode)
				.Include(ce => ce.ReportSubmitChain)
				.Include(ce => ce.ParentChainElement)
				.Include(ce => ce.Organization)
				.Include(ce => ce.ChildrenElemts)
				.FirstOrDefaultAsync();

			if (chainElement == null)
			{
				return NotFound();
			}

			return Ok(chainElement);
		}
	}
}
