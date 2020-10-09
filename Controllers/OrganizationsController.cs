using Microsoft.EntityFrameworkCore;
using PKMIAC.BARSFormStatus.Data;
using PKMIAC.BARSFormStatus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace PKMIAC.BARSFormStatus.Controllers
{
	[RoutePrefix("api/Organizations")]
	public class OrganizationsController : ApiController
	{
		private readonly BFSContext _db = new BFSContext();

		/// <summary>
		/// Получить полный список всех организаций с возможностью фильтрации
		/// по коду или наименованию организации. Опционально загрузить все хранимые данные формы
		/// принадлежащие организации
		/// 
		/// GET api/Organizations
		/// </summary>
		/// <param name="name">Наименование организации</param>
		/// <param name="code">Код организации</param>
		/// <param name="includeStoredFormData">Включать/не включать загрузку 
		/// хранимых данных формы</param>
		/// <returns>Список организаций</returns>
		// GET api/Organizations
		[ResponseType(typeof(List<Organization>))]
		public async Task<IHttpActionResult> GetOrganizations(
			string name = null, 
			string code = null, 
			bool includeStoredFormData = false)
		{
			IQueryable<Organization> request =
				_db.Organizations
				.Where(o => name == null || o.Name == name)
				.Where(o => code == null || o.Code == code);

			List<Organization> organizations;

			if (includeStoredFormData)
			{
				request = request.Include(o => o.StoredFormData);
			}

			organizations =
				await request				
				.ToListAsync();

			return organizations == null || organizations.Count() == 0 ? NotFound() : (IHttpActionResult)Ok(organizations);
		}

		/// <summary>
		/// Получить конкретную организацию.
		/// 
		/// GET api/Organizations/06caa0d2-3efd-47d9-85fe-1d1ff5899b48?includeStoredFormData=true
		/// </summary>
		/// <param name="id">Уникальный идентификатор организации</param>
		/// <param name="includeStoredFormData">Включать/не включать загрузку 
		/// хранимых данных формы</param>
		/// <returns>Организация</returns>
		// GET api/Organizations/06caa0d2-3efd-47d9-85fe-1d1ff5899b48?includeStoredFormData=true
		[ResponseType(typeof(Organization))]
		public async Task<IHttpActionResult> GetOrganization(Guid id, bool includeStoredFormData = false)
		{
			IQueryable<Organization> request =
				_db.Organizations
				.Where(o => o.Id == id);

			Organization organization;

			if (includeStoredFormData)
			{
				organization = 
					await request
					.Include(o => o.StoredFormData)
					.FirstOrDefaultAsync();
			}
			else
			{
				organization = 
					await request.
					FirstOrDefaultAsync();
			}

			if (organization == null)
			{
				return NotFound();
			}

			return Ok(organization);
		}
	}
}
