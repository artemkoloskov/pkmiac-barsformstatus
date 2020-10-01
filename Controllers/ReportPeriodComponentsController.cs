﻿using PKMIAC.BARSFormStatus.Models;
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
    [RoutePrefix("api/ReportPeriodComponents")]
    public class ReportPeriodComponentsController : ApiController
    {
		private readonly BARSContext _db = new BARSContext();

		// GET api/ReportPeriodComponents
		public IQueryable<ReportPeriodComponent> GetAllReportPeriodComponents()
		{
			return from s in _db.ReportPeriodComponents
				   select s;
		}

		// GET api/ReportPeriodComponents/qdjn43-ekndjwe-2323nj-2323njn
		public async Task<IHttpActionResult> GetReportPeriodComponent(Guid id)
		{
			ReportPeriodComponent periodComponent =
				await _db.ReportPeriodComponents
				.Where(rpc => rpc.Id == id)
				.Include(rpc => rpc.StoredFormData)
				.Include(rpc => rpc.FormsBundleNavigation)
				.Include(rpc => rpc.ReportPeriod)
				.Include(rpc => rpc.ReportSubmitChain)
				.FirstOrDefaultAsync();

			if (periodComponent == null)
			{
				return NotFound();
			}

			return Ok(periodComponent);
		}

		// GET api/ReportPeriodComponents/ReportPeriodComponent?code=015
		[Route("ReportPeriodComponent")]
		public async Task<IHttpActionResult> GetReportPeriodComponentByCode(string code)
		{
			ReportPeriodComponent periodComponent =
				await _db.ReportPeriodComponents
				.Where(o => o.Code == code)
				.Include(rpc => rpc.StoredFormData)
				.Include(rpc => rpc.FormsBundleNavigation)
				.Include(rpc => rpc.ReportPeriod)
				.Include(rpc => rpc.ReportSubmitChain)
				.FirstOrDefaultAsync();

			if (periodComponent == null)
			{
				return NotFound();
			}

			return Ok(periodComponent);
		}
	}
}