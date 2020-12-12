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
            var data_req = JsonConvert.SerializeObject(registration);
            var content = new StringContent(data_req, Encoding.UTF8, "application/json");

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            handler.AllowAutoRedirect = true;
            var client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44380/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request_api = new HttpRequestMessage()
            {
                RequestUri = new Uri("https://localhost:44380/api/Identity/Registrationr"),
                Method = HttpMethod.Post,
            };

            var response = client.PostAsync("api/Identity/Registration", content);
            var regresp = new RegistrationResponse();

            try
            {
                var data_resp = await response.Result.Content.ReadAsStringAsync();
                regresp = JsonConvert.DeserializeObject<RegistrationResponse>(data_resp);

            }
            catch
            {
            }

            return new RegistrationResponse
            {
                Status = regresp.Status,
                Message = regresp.Message
            };
        }


        /// <summary>
        /// Auth Implementation
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        internal async Task<AuthResponse> AuthAction(AuthMessage auth)
        {

            var data_req = JsonConvert.SerializeObject(auth);
            var content = new StringContent(data_req, Encoding.UTF8, "application/json");

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            handler.AllowAutoRedirect = true;
            var client = new HttpClient(handler);
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
                var data_resp = await response.Result.Content.ReadAsStringAsync();

                logresponse = JsonConvert.DeserializeObject<AuthResponse>(data_resp);

            }
            catch
            {
            }
            return new AuthResponse
            {
                Status = logresponse.Status,
                Message = logresponse.Message
            };
        }


        /// <summary>
        /// Vote Implementation
        /// </summary>
        /// <param name="vote"></param>
        /// <returns></returns>
        internal async Task<VoteResponse> VoteAction(VoteMessage vote)
        {

            var data_req = JsonConvert.SerializeObject(vote);
            var content = new StringContent(data_req, Encoding.UTF8, "application/json");

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            handler.AllowAutoRedirect = true;
            var client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44380/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request_api = new HttpRequestMessage()
            {
                RequestUri = new Uri("https://localhost:44380/api/Vote"),
                Method = HttpMethod.Post,
            };

            var response = client.PostAsync("api/Vote", content);
            var voteresponse = new VoteResponse();
            // if (response.IsCompletedSuccessfully)
            // {
            try
            {
                var data_resp = await response.Result.Content.ReadAsStringAsync();

                voteresponse = JsonConvert.DeserializeObject<VoteResponse>(data_resp);

            }
            catch
            {
            }
            return new VoteResponse()
            {
                VoteStatus = voteresponse.VoteStatus,
                Message = voteresponse.Message,
                ProcessedTime = voteresponse.ProcessedTime
            };
        }
    }
}
