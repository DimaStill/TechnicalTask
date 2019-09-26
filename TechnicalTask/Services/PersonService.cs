using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.Models;
using TechnicalTask.Services.Interfaces;

namespace TechnicalTask.Services
{
    public class PersonService: IPersonService
    {
        readonly string FHIRPersonUrl;
        readonly HttpClient httpClient;
        public PersonService(string url)
        {
            FHIRPersonUrl = url;
            httpClient = new HttpClient();
        }

        public async Task<IEnumerable<Person>> GetAllPeson()
        {
            var response = await httpClient.GetAsync($"{FHIRPersonUrl}?_pretty=true");
            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic persons = JsonConvert.DeserializeObject<dynamic>(responseContent);
            List<Person> resultPerson = new List<Person>();
            try
            {
                foreach (var person in persons.entry)
                {
                    resultPerson.Add(JsonConvert.DeserializeObject<Person>(person["resource"].ToString()));
                }
            }
            catch
            {
                return resultPerson;
            }

            return resultPerson;
        }

        public async Task<Person> GetPerson(int id)
        {
            var response = await httpClient.GetAsync($"{FHIRPersonUrl}/{id}?_pretty=true");
            var responseContent = await response.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<Person>(responseContent);

            return person;
        }

        public async Task<Person> CreatePerson(Person newPerson)
        {
            var content = new StringContent(JsonConvert.SerializeObject(newPerson), Encoding.UTF8, "application/json");
            Console.WriteLine($"\n\n{await content.ReadAsStringAsync()}\n\n");
            var response = await httpClient.PostAsync($"{FHIRPersonUrl}", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            var person = JsonConvert.DeserializeObject<Person>(responseContent);

           return person;
        }

        public async Task<Person> UpdatePerson(int id, Person person)
        {
            var content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"{FHIRPersonUrl}/{id}?_format=json&_pretty=true", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var resultPerson = JsonConvert.DeserializeObject<Person>(responseContent);

            return resultPerson;
        }

        public async Task<bool> DeletePerson(int id)
        {
            var response = await httpClient.DeleteAsync($"{FHIRPersonUrl}/{id}?_pretty=true");

            return response.IsSuccessStatusCode;
        }
    }
}
