using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechnicalTask.Models;
using TechnicalTask.Services.Interfaces;

namespace TechnicalTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        readonly IPersonService service;
        public PersonController(IPersonService personService)
        {
            service = personService;
        }

        // GET: api/Person
        [HttpGet]
        public async Task<IEnumerable<Person>> Get()
        {
            return await service.GetAllPeson();
        }

        // GET: api/Person/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Person> Get(int id)
        {
            return await service.GetPerson(id);
        }

        // POST: api/Person
        [HttpPost]
        public async Task<Person> Post([FromBody] Person newPerson)
        {
            return await service.CreatePerson(newPerson);
        }

        // PUT: api/Person/5
        [HttpPut("{id}")]
        public async Task<Person> Put(int id, [FromBody] Person person)
        {
            return await service.UpdatePerson(id, person);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await service.DeletePerson(id);
        }
    }
}
