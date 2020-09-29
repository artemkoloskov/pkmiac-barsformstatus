using PKMIAC.BARSFormStatus.Models;
using PKMIAC.BARSFormStatus.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PKMIAC.BARSFormStatus.Controllers
{
	public class OrganizationsController : ApiController
	{
        private readonly BARSContext _context = new BARSContext();

        public List<string> GetAllOrganizations()
        {
            IQueryable<Organization> organizations = from s in _context.Organizations
                                                     select s;
			return (organizations.Select(org => org.Code)).ToList();
        }

        /*public IHttpActionResult GetOrganization(string id)
        {
            var organization = organizations.FirstOrDefault((p) => p.Id == id);
            
            if (organization == null)
            {
                return NotFound();
            }

            return Ok(organization);
        }*/
    }
}
