using IPChecker.DTOS.IP2CDTOS;
using IPChecker.Exceptions;
using IPChecker.Models;
using Microsoft.Net.Http.Headers;

using System.Net.Http.Headers;
using System.Text.Json;

namespace IPChecker.Services
{
    public interface IIP2CService
    {
        /*Task<IP2CDTO> Get(string ipAddress);*/
        Task<IP2CDTO> OnGet(string ipAddress);
    }

    public class IP2CService : IIP2CService
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public IP2CService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IP2CDTO> OnGet(string ipAddress)
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://ip2c.org/{ipAddress}");

            using var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var content = await httpResponseMessage.Content.ReadAsStringAsync();

                var values = content.Split(';');

                var ip = new IP2CDTO() { TwoLetterCode = values[1], ThreeLetterCode = values[2], CountryName = values[3] };

                return ip;
            }
            else
            {
                throw new IP2CCallException();
            }

        }




        /*public async Task<IP2CDTO> Get(string ipAddress)
        {

            var url = $"https://ip2c.org/{ipAddress}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    IP2CDTO result = await response.Content.ReadAsAsync<IP2CDTO>();
                    return result;
                }
                else
                {
                    throw new IP2CCallException();
                }

            }
        }*/

    }
}