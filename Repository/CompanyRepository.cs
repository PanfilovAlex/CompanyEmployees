using Contracts;
using Entities;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext) :
            base(repositoryContext)
        { }

        public async Task<IEnumerable<Company>> GetAllCompanies(bool trackChanges) =>
             await FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToListAsync();
        public async Task<Company> GetCompany(Guid companyId, bool trackChanges) =>
           await FindByCondition(c => c.Id.Equals(companyId), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateCompany(Company company)
         => Create(company);

        public async Task<IEnumerable<Company>> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(c => ids.Contains(c.Id), trackChanges)
            .ToListAsync();

        public void DeleteCompany(Company company) => Delete(company);

        public void UpdateCompany(Company company) => Update(company);

    }
}
