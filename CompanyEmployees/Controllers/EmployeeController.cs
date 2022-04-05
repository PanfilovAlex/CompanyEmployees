using AutoMapper;
using Contracts;
using Entities.DataTrancferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CompanyEmployees.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private ILoggerManager _logger;
        private IMapper _mapper;
        private IRepositoryManager _repository;

        public EmployeeController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetEmployeesForCompany(Guid companyId)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");

                return NotFound();
            }

            var employeesFromDb = _repository.Employee.GetEmployees(companyId, trackCganges: false);
            var employeeDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);

            return Ok(employeeDto);
        }

        [HttpGet("{id}", Name = "GetEmployeeForCompany")]
        public IActionResult GetEmployee(Guid companyId, Guid Id)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);

            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }

            var employee = _repository.Employee.GetEmployee(companyId, Id, trackChanges: false);

            if (employee == null)
            {
                _logger.LogInfo($"Employee with id: {Id} doesn't exist in the database.");
                return NotFound();
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return Ok(employeeDto);
        }

        [HttpPost]
        public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee)
        {
            if (employee == null)
            {
                _logger.LogError("EmployeeForCreationDto object sent from client is null.");
                return BadRequest("EmployeeForCreationDto object is null");
            }

            var company = _repository.Company.GetCompany(companyId, trackChanges: false);

            if (company == null)
            {
                _logger.LogError($"Company with id: {companyId} doesn't exist");
                return NotFound();
            }

            var employeeEntity = _mapper.Map<Employee>(employee);
            _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            _repository.Save();

            var employeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeToReturn.Id }, employeToReturn);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteEmployeeForCompany(Guid companyId, Guid id)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);

            if (company == null)
            {
                _logger.LogError($"Company with id: {companyId} doesn't exist in database.");
                return NotFound();
            }

            var employeeForCompany = _repository.Employee.GetEmployee(companyId, id, trackChanges: false);

            if (employeeForCompany == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.Employee.DeleteEmployee(employeeForCompany);
            _repository.Save();

            return NoContent();
        }
    }
}
