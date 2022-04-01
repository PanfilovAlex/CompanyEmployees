using AutoMapper;
using Contracts;
using Entities.DataTrancferObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CompanyEmployees.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private ILoggerManager _loger;
        private IRepositoryManager _repository;
        private IMapper _mapper;

        public CompaniesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _loger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            try
            {
                var companies = _repository.Company.GetAllCompanies(trackChanges: false);
                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

                return Ok(companiesDto);
            }
            catch (Exception ex)
            {
                _loger.LogError($"Something went wrong in the {nameof(GetCompanies)} action { ex}");
                return StatusCode(500, "Internal server error");
            }
        }



    }
}
