using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) :
            base(repositoryContext)
        { }

        public Employee GetEmployee(Guid companyId, Guid Id, bool trackChanges) =>
            FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(Id), trackChanges)
            .SingleOrDefault();

        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackCganges) =>
            FindByCondition(e => e.CompanyId.Equals(companyId), trackCganges).OrderBy(e => e.Name);
    }
}
