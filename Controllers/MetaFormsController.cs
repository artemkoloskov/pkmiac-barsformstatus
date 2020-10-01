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
		private readonly BARSContext _db = new BARSContext();

		// GET api/MetaForms
		public IQueryable<MetaForm> GetAllMetaForms()
		{
			return from s in _db.MetaForms
				   select s;
		}

		// GET api/MetaForms/qdjn43-ekndjwe-2323nj-2323njn
		public async Task<IHttpActionResult> GetMetaForm(Guid id)
		{
			MetaForm metaForm =
				await _db.MetaForms.Where(mf => mf.Id == id).FirstOrDefaultAsync();

			if (metaForm == null)
			{
				return NotFound();
			}

			return Ok(metaForm);
		}

		// GET api/MetaForms/MetaForm?code=ДЗПК_М_Мониторинг&name=Мониторинг%20отчета
		[Route("MetaForm")]
		public async Task<IHttpActionResult> GetMetaFormByCode(string code)
		{
			MetaForm metaForm = await _db.MetaForms.Where(mf => mf.Code == code).FirstOrDefaultAsync();
			
			if (metaForm == null)
			{
				return NotFound();
			}

			return Ok(metaForm);
		}
	}
}
