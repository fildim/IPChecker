using IPChecker.DTOS;
using IPChecker.Exceptions;
using IPChecker.Models;
using System.Net.Http.Headers;

namespace IPChecker.Services
{
    public class IP2CService
    {
        

        public async Task<IP2CDTO> Get (string ipAddress)
        {
            try
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
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        throw new IP2CCallException(errorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
