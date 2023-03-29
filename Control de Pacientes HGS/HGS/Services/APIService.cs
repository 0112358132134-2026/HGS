using HGS.Models;
using Newtonsoft.Json;
using System.Text;

namespace HGS.Services
{
    public class APIService
    {
        private static readonly int timeout = 30;
        private static readonly string baseurl = "https://localhost/HGSAPI/";

        #region Patient
        // Devuelve lista de Pacientes
        public static async Task<IEnumerable<Models.Patient>> PatientGetList()
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
                return JsonConvert.DeserializeObject<IEnumerable<Models.Patient>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        // Inserta un Paciente
        public static async Task<HGSModel.GeneralResult> PatientSet(Models.Patient object_to_serialize)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            HttpResponseMessage response = await httpClient.PostAsync(baseurl + "Patient/Set", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<HGSModel.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            return new()
            {
                Message = "Error"
            };
        }

        // Devuelve un Paciente
        public static async Task<Patient> PatientGet(int id)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            var response = await httpClient.GetAsync(baseurl + "Patient/Get/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Patient>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        // Edita un Paciente
        public static async Task<HGSModel.GeneralResult> PatientUpdate(Models.Patient object_to_serialize)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            var response = await httpClient.PutAsync(baseurl + "Patient/Update", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<HGSModel.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            return new()
            {
                Message = "Error"
            };
        }
        #endregion

        #region Branch
        // Devuelve lista de Municipios
        public static async Task<IEnumerable<Models.Branch>> BranchGetList()
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "Branch/GetList");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.Branch>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        // Inserta un Municipio
        public static async Task<HGSModel.GeneralResult> BranchSet(Models.Branch object_to_serialize)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            HttpResponseMessage response = await httpClient.PostAsync(baseurl + "Branch/Set", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<HGSModel.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            return new()
            {
                Message = "Error"
            };
        }

        // Devuelve un Municipio
        public static async Task<Branch> BranchGet(int id)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            var response = await httpClient.GetAsync(baseurl + "Branch/Get/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Branch>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        // Edita un Municipio
        public static async Task<HGSModel.GeneralResult> BranchUpdate(Models.Branch object_to_serialize)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            var response = await httpClient.PutAsync(baseurl + "Branch/Update", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<HGSModel.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            return new()
            {
                Message = "Error"
            };
        }
        #endregion

        #region Area
        // Devuelve lista de Áreas
        public static async Task<IEnumerable<Models.Area>> AreaGetList()
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "Area/GetList");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.Area>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        // Inserta un Área
        public static async Task<HGSModel.GeneralResult> AreaSet(Models.Area object_to_serialize)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            HttpResponseMessage response = await httpClient.PostAsync(baseurl + "Area/Set", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<HGSModel.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            return new()
            {
                Message = "Error"
            };
        }

        // Devuelve un Área
        public static async Task<Area> AreaGet(int id)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            var response = await httpClient.GetAsync(baseurl + "Area/Get/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Area>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        // Edita un Área
        public static async Task<HGSModel.GeneralResult> AreaUpdate(Models.Area object_to_serialize)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            var response = await httpClient.PutAsync(baseurl + "Area/Update", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<HGSModel.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            return new()
            {
                Message = "Error"
            };
        }
        #endregion

        #region Speciality
        // Devuelve lista de Especialidades
        public static async Task<IEnumerable<Models.Speciality>> SpecialityGetList()
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "Speciality/GetList");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<Models.Speciality>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        // Inserta una Especialidad
        public static async Task<HGSModel.GeneralResult> SpecialitySet(Models.Speciality object_to_serialize)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            HttpResponseMessage response = await httpClient.PostAsync(baseurl + "Speciality/Set", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<HGSModel.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            return new()
            {
                Message = "Error"
            };
        }

        // Devuelve una Especialidad
        public static async Task<Speciality> SpecialityGet(int id)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            var response = await httpClient.GetAsync(baseurl + "Speciality/Get/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Speciality>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        // Edita una Especialidad
        public static async Task<HGSModel.GeneralResult> SpecialityUpdate(Models.Speciality object_to_serialize)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            var response = await httpClient.PutAsync(baseurl + "Speciality/Update", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<HGSModel.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            return new()
            {
                Message = "Error"
            };
        }
        #endregion

        #region Areasucursal
        // Devuelve lista de Áreas Sucursales
        public static async Task<IEnumerable<HGSModel.Areasucursal>> AreasucursalGetList()
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "Areasucursal/GetList");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<HGSModel.Areasucursal>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        // Devuelve un Área Sucursal con el listado de Áreas y Sucursales existentes
        public static async Task<HGSModel.Areasucursal> AreasucursalGetAB()
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "Areasucursal/GetAB");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<HGSModel.Areasucursal>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        // Inserta un Área Sucursal
        public static async Task<HGSModel.GeneralResult> AreasucursalSet(Models.Areasucursal object_to_serialize)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            HttpResponseMessage response = await httpClient.PostAsync(baseurl + "Areasucursal/Set", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<HGSModel.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            return new()
            {
                Message = "Error"
            };
        }

        // Devuelve un Área Sucursal
        public static async Task<HGSModel.Areasucursal> AreasucursalGet(int id)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            var response = await httpClient.GetAsync(baseurl + "Areasucursal/Get/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<HGSModel.Areasucursal>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        // Edita un Área Sucursal
        public static async Task<HGSModel.GeneralResult> AreasucursalUpdate(Models.Areasucursal object_to_serialize)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            var response = await httpClient.PutAsync(baseurl + "Areasucursal/Update", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<HGSModel.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            return new()
            {
                Message = "Error"
            };
        }
        #endregion

        #region Doctor
        // Devuelve lista de Doctores
        public static async Task<IEnumerable<HGSModel.Doctor>> DoctorGetList()
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "Doctor/GetList");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<HGSModel.Doctor>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        // Devuelve un Doctor con el listado Especialidades existentes
        public static async Task<HGSModel.Doctor> DoctorGetS()
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "Doctor/GetS");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<HGSModel.Doctor>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        // Inserta un Doctor
        public static async Task<HGSModel.GeneralResult> DoctorSet(HGSModel.Doctor object_to_serialize)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            HttpResponseMessage response = await httpClient.PostAsync(baseurl + "Doctor/Set", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<HGSModel.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            return new()
            {
                Message = "Error"
            };
        }

        // Devuelve un Doctor
        public static async Task<HGSModel.Doctor> DoctorGet(int id)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            var response = await httpClient.GetAsync(baseurl + "Doctor/Get/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<HGSModel.Doctor>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        // Edita un Doctor
        public static async Task<HGSModel.GeneralResult> DoctorUpdate(HGSModel.Doctor object_to_serialize)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            var response = await httpClient.PutAsync(baseurl + "Doctor/Update", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<HGSModel.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            return new()
            {
                Message = "Error"
            };
        }
        #endregion

        #region Home
        public static async Task<HGSModel.GeneralResult> DoctorExists(HGSModel.Doctor object_to_serialize)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            HttpResponseMessage response = await httpClient.PostAsync(baseurl + "Home/DoctorExists", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<HGSModel.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            return new()
            {
                Message = "Error"
            };
        }

        #endregion
    }
}