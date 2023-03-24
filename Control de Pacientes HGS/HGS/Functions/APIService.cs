using HGS.Models;
using Newtonsoft.Json;

namespace HGS.Functions
{
    public class APIService
    {
        private static readonly int timeout = 30;
        private static readonly string baseurl = "https://localhost/HGSAPI/";

        // Devuelve lista de Pacientes
        public static async Task<IEnumerable<Patient>> PatientGetList()
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "Patient/GetList");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<Patient>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
    }
}