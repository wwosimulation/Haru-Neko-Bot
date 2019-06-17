using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Neko_Test.Endpoints;
using Neko_Test.Responses;
using Newtonsoft.Json;

namespace Neko_Test.Modules.services
{
    public class NekosClient
    {

        private const string HostUrl = "https://nekos.life/api/v2";
        /// <summary>
        ///     Get an image from random SFW endpoints.
        /// </summary>
        /// <returns>The infomation about the image returned from random SFW endpoints.</returns>
        public static async Task<NekosImage> GetSfwAsync()
        {
            Random r = new Random();
            int index = r.Next(0, SfwEndpoints.Endpoints.Length - 1);
            return await GetSfwAsync2(SfwEndpoints.Endpoints[index]);
        }

        /// <summary>
        ///     Get an image from random NSFW endpoints.
        /// </summary>
        /// <returns>The infomation about the image returned from random SFW endpoints.</returns>
        public static async Task<NekosImage> GetNsfwAsync()
        {
            Random r = new Random();
            int index = r.Next(0, NsfwEndpoints.Endpoints.Length - 1);
            return await GetNsfwAsync2(NsfwEndpoints.Endpoints[index]);
        }

        /// <summary>
        ///     Get an image from given SFW endpoint.
        /// </summary>
        /// <param name="urlEndpoint">The endpoint string.</param>
        /// <returns>The infomation about the image returned from given SFW endpoint.</returns>
        /// <exception cref="ArgumentException">When urlEndpoint is null or empty</exception>
        /// <exception cref="ArgumentException">When urlEndpoint is not in <see cref="SfwEndpoints" /></exception>
        // ReSharper disable once MemberCanBePrivate.Global
        public static async Task<NekosImage> GetSfwAsync2(string urlEndpoint)
        {
            if (string.IsNullOrEmpty(urlEndpoint.Trim()))
                throw new ArgumentException("urlEndpoint cannot be null or whitespace");

            if (NsfwEndpoints.Endpoints.Contains(urlEndpoint))
                throw new ArgumentException("urlEndpoint is not in endpoints list !");

            return await GetResponseFromHost<NekosImage>($"/img/{urlEndpoint}");
        }

        /// <summary>
        ///     Get an image from given NSFW endpoint.
        /// </summary>
        /// <param name="urlEndpoint">The endpoint string.</param>
        /// <returns>The infomation about the image returned from given NSFW endpoint.</returns>
        /// <exception cref="ArgumentException">When urlEndpoint is null or empty</exception>
        /// <exception cref="ArgumentException">When urlEndpoint is not in <see cref="NsfwEndpoints" /></exception>
        // ReSharper disable once MemberCanBePrivate.Global
        public static async Task<NekosImage> GetNsfwAsync2(string urlEndpoint)
        {
            if (string.IsNullOrEmpty(urlEndpoint.Trim()))
                throw new ArgumentException("urlEndpoint cannot be null or whitespace");

            if (NsfwEndpoints.Endpoints.Contains(urlEndpoint))
                throw new ArgumentException("urlEndpoint is not in endpoints list !");

            return await GetResponseFromHost<NekosImage>($"/img/{urlEndpoint}");
        }

        private static async Task<T> GetResponseFromHost<T>(string endpoint)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, $"{HostUrl}{endpoint}");
                HttpResponseMessage res = await httpClient.SendAsync(req);

                if (
                    res.StatusCode.Equals(HttpStatusCode.NoContent) ||
                    res.StatusCode.Equals(HttpStatusCode.GatewayTimeout) ||
                    res.StatusCode.Equals(HttpStatusCode.BadGateway) ||
                    res.StatusCode.Equals(HttpStatusCode.BadRequest) ||
                    res.StatusCode.Equals(HttpStatusCode.Unauthorized) ||
                    res.StatusCode.Equals(HttpStatusCode.Forbidden) ||
                    res.StatusCode.Equals(HttpStatusCode.MethodNotAllowed) ||
                    res.StatusCode.Equals(HttpStatusCode.NotAcceptable) ||
                    res.StatusCode.Equals(HttpStatusCode.PaymentRequired) /* oof */
                )
                    throw new HttpRequestException($"Unwanted status code : {res.StatusCode}");

                string response = await res.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(response);
            }
        }


    }
}
