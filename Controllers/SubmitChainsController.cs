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
	[RoutePrefix("api/SubmitChains")]
	public class SubmitChainsController : ApiController
	{
		private readonly BFSContext _db = new BFSContext();

		// GET api/SubmitChains
		public IQueryable<ReportSubmitChain> GetAllSubmitChains()
		{
			return from s in _db.ReportSubmitChains
				   select s;
		}

		// GET api/SubmitChains/d3f29683-75ab-46da-844e-a5416508482b
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

		// GET api/SubmitChains?code=0971%2001
		public async Task<IHttpActionResult> GetSubmitChainByCode(string code)
		{
			ReportSubmitChain submitChain = await _db.ReportSubmitChains
				.Where(sc => sc.Code == code)
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
