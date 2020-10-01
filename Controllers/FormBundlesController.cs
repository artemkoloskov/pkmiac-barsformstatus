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

namespace PKMIAC.BARSFormStatus.Controllers
{
    [RoutePrefix("api/FormBundles")]
    public class FormBundlesController : ApiController
    {
		private readonly BARSContext _db = new BARSContext();

		// GET api/FormBundles
		public IQueryable<FormBundle> GetAllFormBundles()
		{
			return from s in _db.FormBundles.Include(b => b.ReportPeriodComponent)
				   select s;
		}

		// GET api/FormBundles/qdjn43-ekndjwe-2323nj-2323njn
		public async Task<IHttpActionResult> GetFormBundles(Guid id)
		{
			FormBundle formBundle = await _db.FormBundles
				.Where(b => b.Id == id).Include(b => b.ReportPeriodComponent).FirstOrDefaultAsync();

			if (formBundle == null)
			{
				return NotFound();
			}

			return Ok(formBundle);
		}
	}
}
