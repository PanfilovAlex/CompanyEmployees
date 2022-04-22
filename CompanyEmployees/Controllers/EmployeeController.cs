using AutoMapper;
using CompanyEmployees.ActionFilters;
using CompanyEmployees.Utility;
using Contracts;
using Entities.DataTrancferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository.DataShaping;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyEmployees.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private ILoggerManager _logger;
        private IMapper _mapper;
        private IRepositoryManager _repository;
        private EmployeeLinks _employeeLinks;

        public EmployeeController(IRepositoryManager repository, ILoggerManager logger,
            IMapper mapper, EmployeeLinks employeeLinks)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _employeeLinks = employeeLinks;
        }

        [HttpGet(Name = "GetEmployees")]
        [HttpHead]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetEmployeesForCompany(Guid companyId, [FromQuery] EmployeeParameters employeeParameters)
        {
            if (!employeeParameters.ValidAgeRange)
            {
                return BadRequest("Max age can't be less than min");
            }

            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");

                return NotFound();
            }

            var employeesFromDb = await _repository.Employee.GetEmployeesAsync(companyId, employeeParameters, trackCganges: false);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(employeesFromDb.MetaData));

            var employeeDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
            var links = _employeeLinks.TryGenerateLinks(employeeDto, employeeParameters.Fields, companyId, HttpContext);

            var linkEnt = links.LinkedEntities;
            var shapedEnt = links.ShapedEntities;


            return links.HasLinks ? Ok(links.LinkedEntities) : Ok(links.ShapedEntities);
        }

        [HttpGet("{id}", Name = "GetEmployeeForCompany")]
        public async Task<IActionResult> GetEmployee(Guid companyId, Guid Id)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges: false);

            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }

            var employee = await _repository.Employee.GetEmployeeAsync(companyId, Id, trackChanges: false);

            if (employee == null)
            {
                _logger.LogInfo($"Employee with id: {Id} doesn't exist in the database.");
                return NotFound();
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return Ok(employeeDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges: false);

            if (company == null)
            {
                _logger.LogError($"Company with id: {companyId} doesn't exist");
                return NotFound();
            }

            var employeeEntity = _mapper.Map<Employee>(employee);
            _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            await _repository.SaveAsync();

            var employeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeToReturn.Id }, employeToReturn);
        }


        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEmployeeForCompanyExistsAttribute))]
        public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid id)
        {
            var employee = HttpContext.Items["employee"] as Employee;

            _repository.Employee.DeleteEmployee(employee);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEmployeeForCompanyExistsAttribute))]
        public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid id,
            [FromBody] EmployeeForUpdateDto employee)
        {
            var employeFofCompany = HttpContext.Items["employee"] as Employee;

            _mapper.Map(employee, employeFofCompany);
            await _repository.SaveAsync();

            return Ok($"{employee.Name} was updated");
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(Guid companyId, Guid id,
            [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
        {

            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var employeeEntity = HttpContext.Items["employee"];

            var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);

            patchDoc.ApplyTo(employeeToPatch, ModelState);
            TryValidateModel(employeeToPatch);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(employeeToPatch, employeeEntity);

            await _repository.SaveAsync();

            return Ok($"{employeeToPatch.Name} was updated.");
        }
    }
}
