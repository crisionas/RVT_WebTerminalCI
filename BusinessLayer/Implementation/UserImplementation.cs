using Newtonsoft.Json;
using RVTLibrary.Models.AuthUser;
using RVTLibrary.Models.UserIdentity;
using RVTLibrary.Models.Vote;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLayer.Implementation
{
    public class UserImplementation
    {

        /// <summary>
        /// Registration Implementation
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        internal async Task<RegistrationResponse> RegistrationAction(RegistrationMessage registration)
        {
            return await Task.Run(() =>
            {
                var data_req = JsonConvert.SerializeObject(registration);
                var content = new StringContent(data_req, Encoding.UTF8, "application/json");

                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                handler.AllowAutoRedirect = true;
                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri("https://localhost:44380/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var request_api = new HttpRequestMessage()
                    {
                        RequestUri = new Uri("https://localhost:44380/api/Identity/Registration"),
                        Method = HttpMethod.Post,
                    };

                    var response = client.PostAsync("api/Identity/Registration", content);
                    var regresp = new RegistrationResponse();

                    try
                    {
                        var data_resp = response.Result.Content.ReadAsStringAsync().Result;
                        regresp = JsonConvert.DeserializeObject<RegistrationResponse>(data_resp);

                    }
                    catch
                    {
                    }

                    return regresp;
                }
            });
        }


        /// <summary>
        /// Auth Implementation
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        internal async Task<AuthResponse> AuthAction(AuthMessage auth)
        {
            return await Task.Run(() =>
            {

                var data_req = JsonConvert.SerializeObject(auth);
                var content = new StringContent(data_req, Encoding.UTF8, "application/json");

                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                handler.AllowAutoRedirect = true;
                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri("https://localhost:44380/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var request_api = new HttpRequestMessage()
                    {
                        RequestUri = new Uri("https://localhost:44380/api/Identity/Auth"),
                        Method = HttpMethod.Post,
                    };

                    var response = client.PostAsync("api/Identity/Auth", content);
                    var logresponse = new AuthResponse();
                    try
                    {
                        var data_resp =  response.Result.Content.ReadAsStringAsync().Result;

                        logresponse = JsonConvert.DeserializeObject<AuthResponse>(data_resp);

                    }
                    catch
                    {
                    }

                    return logresponse;
                }
            });

        }


        /// <summary>
        /// Vote Implementation
        /// </summary>
        /// <param name="vote"></param>
        /// <returns></returns>
        internal async Task<VoteResponse> VoteAction(VoteMessage vote)
        {
            return await Task.Run(() =>
            {

                var data_req = JsonConvert.SerializeObject(vote);
                var content = new StringContent(data_req, Encoding.UTF8, "application/json");

                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                handler.AllowAutoRedirect = true;
                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri("https://localhost:44380/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.PostAsync("api/Identity/Vote", content);
                    var voteresponse = new VoteResponse();
                    try
                    {
                        var data_resp =  response.Result.Content.ReadAsStringAsync().Result;

                        voteresponse = JsonConvert.DeserializeObject<VoteResponse>(data_resp);

                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }

                    return voteresponse;
                }
            });
        }
    }
}
