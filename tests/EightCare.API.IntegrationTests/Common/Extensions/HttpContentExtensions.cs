using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EightCare.API.IntegrationTests.Common.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<string> GetIdAsync(this HttpContent content)
        {
            dynamic responseObject = await content.ReadFromJsonAsync<ExpandoObject>();

            return responseObject?.id.ToString();
        }
    }
}
