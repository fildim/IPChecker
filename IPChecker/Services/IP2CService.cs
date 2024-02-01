using IPChecker.DTOS.IP2CDTOS;
using IPChecker.Exceptions;
using IPChecker.Models;
using System.Net.Http.Headers;

namespace IPChecker.Services
{
    public interface IIP2CService
    {
        Task<IP2CDTO> Get(string ipAddress);
    }

    public class IP2CService : IIP2CService
    {


        public async Task<IP2CDTO> Get(string ipAddress)
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


        }
    }
}