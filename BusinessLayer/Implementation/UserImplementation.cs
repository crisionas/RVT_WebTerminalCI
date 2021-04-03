using Newtonsoft.Json;
using NLog;
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
        private static Logger logger = LogManager.GetLogger("UserLog");
        /// <summary>
        /// Registration Implementation
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        internal async Task<RegistrationResponse> RegistrationAction(RegistrationMessage registration)
        {
            return await Task.Run(async () =>
            {
                var regresp = new RegistrationResponse();
                var data_req = JsonConvert.SerializeObject(registration);
                var content = new StringContent(data_req, Encoding.UTF8, "application/json");

                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                handler.AllowAutoRedirect = true;
                using (var client = new HttpClient(handler))
                {
                    try
                    {
                        client.BaseAddress = new Uri("https://localhost:44380/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response =await client.PostAsync("api/Identity/Registration", content);
                    
                        var data_resp = await response.Content.ReadAsStringAsync();
                        regresp = JsonConvert.DeserializeObject<RegistrationResponse>(data_resp);
                    }
                    catch(Exception e)
                    {
                        logger.Error(e,"Registration error");
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
            return await Task.Run(async() =>
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

                    var response = await client.PostAsync("api/Identity/Auth", content);
                    var logresponse = new AuthResponse();
                    try
                    {
                        var data_resp = await response.Content.ReadAsStringAsync();

                        logresponse = JsonConvert.DeserializeObject<AuthResponse>(data_resp);

                    }
                    catch(Exception e)
                    {
                        logger.Error(e, "Auth error");
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
            return await Task.Run(async() =>
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

                    var response = await client.PostAsync("api/Identity/Vote", content);
                    var voteresponse = new VoteResponse();
                    try
                    {
                        var data_resp = await response.Content.ReadAsStringAsync();

                        voteresponse = JsonConvert.DeserializeObject<VoteResponse>(data_resp);

                    }
                    catch (Exception e)
                    {
                        logger.Error(e, "Vote error");
                    }

                    return voteresponse;
                }
            });
        }
    }
}
