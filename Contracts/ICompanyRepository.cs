using Entities.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompanies(bool trackChanges);
        Task<IEnumerable<Company>> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        Task<Company> GetCompanyAsync(Guid id, bool trackChanges);
        void CreateCompany(Company company);
        void DeleteCompany(Company company);
        void UpdateCompany(Company company);

    }
}
