using Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;

namespace ConsoleApp1.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;

        public ApiService(HttpClient httpClient, string baseAddress)
        {
            _httpClient = httpClient;
            _baseAddress = baseAddress;
        }

        public async Task GetPatientsAsync(string endpointSuffix)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_baseAddress + endpointSuffix);
            string content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content + '\n');
        }

        public async Task<Patient> GetPatientByIdAsync(string endpointSuffix, Guid id)
        {
            var jsonContent = $"\"{id}\"";
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{_baseAddress}{endpointSuffix}", content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);

            try
            {
                Patient patient = JsonConvert.DeserializeObject<Patient>(result);
                return patient;
            }
            catch (JsonException ex)
            {
                Console.WriteLine("Error deserializing response: " + ex.Message);
                return null;
            }
        }



        public async Task GetPatientByNameAsync(string endpointSuffix, string name)
        {
            var jsonContent = $"\"{name}\""; // Обратите внимание на двойные кавычки вокруг имени
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync($"{_baseAddress}{endpointSuffix}", content);
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result + '\n');

        }

        public async Task<Patient> AddPatientAsync(string endpointSuffix, Patient patient)
        {
            var patientJson = JsonConvert.SerializeObject(patient);
            var content = new StringContent(patientJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(_baseAddress + endpointSuffix, content);
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result + '\n');
            return patient;
        }

        public async Task UpdatePatientAsync(string endpointSuffix, Patient patient)
        {
            var json = JsonConvert.SerializeObject(patient);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(_baseAddress + endpointSuffix, content);
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result + '\n');
        }

        public async Task DeletePatientAsync(string endpointSuffix, Guid id)
        {
            var json = JsonConvert.SerializeObject(id);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(_baseAddress + endpointSuffix, content);
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result + '\n');
        }
    }
}
