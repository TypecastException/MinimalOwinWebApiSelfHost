using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using MinimalOwinWebApiSelfHost.Models;

// Add these usings:
using System.Data.Entity;

namespace MinimalOwinWebApiSelfHost.Controllers
{
    [Authorize(Roles="Admin")]
    public class CompaniesController : ApiController
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        public IEnumerable<Company> Get()
        {
            return dbContext.Companies;
        }


        public async Task<Company> Get(int id)
        {
            var company = await dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id);
            if (company == null)
            {
                throw new HttpResponseException(
                    System.Net.HttpStatusCode.NotFound);
            }
            return company;
        }


        public async Task<IHttpActionResult> Post(Company company)
        {
            if (company == null)
            {
                return BadRequest("Argument Null");
            }
            var companyExists = await dbContext.Companies.AnyAsync(c => c.Id == company.Id);

            if (companyExists)
            {
                return BadRequest("Exists");
            }

            dbContext.Companies.Add(company);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        public async Task<IHttpActionResult> Put(Company company)
        {
            if (company == null)
            {
                return BadRequest("Argument Null");
            }
            var existing = await dbContext.Companies.FirstOrDefaultAsync(c => c.Id == company.Id);

            if (existing == null)
            {
                return NotFound();
            }

            existing.Name = company.Name;
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        public async Task<IHttpActionResult> Delete(int id)
        {
            var company = await dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            dbContext.Companies.Remove(company);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
