using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Exam
{
    public static class GetExchangeRateClass
    {

        public static async Task<string> GetExchangeRateGEL_USD()
        {
            var url =
                "https://nbg.gov.ge/gw/api/ct/monetarypolicy/currencies/calculator/?codeFrom=GEL&codeTo=USD&quantity=1";

            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var responseBody = await response.Content.ReadAsStringAsync();
            //var exchangeRate = decimal.Parse(responseBody.Trim('.')).ToString("0.00");

            return responseBody;
        }
        
        public static async Task<string> GetExchangeRateGEL_EUR()
        {
            var url =
                "https://nbg.gov.ge/gw/api/ct/monetarypolicy/currencies/calculator/?codeFrom=GEL&codeTo=EUR&quantity=1";

            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var responseBody = await response.Content.ReadAsStringAsync();
            //var exchangeRate = decimal.Parse(responseBody.Trim('.')).ToString("0.00");

            return responseBody;
        }
        
        public static async Task<string> GetExchangeRateUSD_EUR()
        {
            var url =
                "https://nbg.gov.ge/gw/api/ct/monetarypolicy/currencies/calculator/?codeFrom=USD&codeTo=EUR&quantity=1";

            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var responseBody = await response.Content.ReadAsStringAsync();
            //var exchangeRate = decimal.Parse(responseBody.Trim('.')).ToString("0.00");

            return responseBody;
        }
        
        
    }
    
}