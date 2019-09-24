using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalTask.Models;

namespace TechnicalTask.Services.Interfaces
{
    public interface IPersonService
    {
        Task<Person> GetPerson(int id);
        Task<IEnumerable<Person>> GetAllPeson();
        Task<Person> CreatePerson(Person newPerson);
        Task<Person> UpdatePerson(int id, Person person);
        Task<bool> DeletePerson(int id);
    }
}
