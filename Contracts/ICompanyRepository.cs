using Entities.Models;
using System.Collections.Generic;
using System;


namespace Contracts
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompanies(bool trackChanges);
        IEnumerable<Company> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        Company GetCompany(Guid id, bool trackChanges);
        void CreateCompany(Company company);
        void DeleteCompany(Company company);
        void UpdateCompany(Company company);

    }
}
