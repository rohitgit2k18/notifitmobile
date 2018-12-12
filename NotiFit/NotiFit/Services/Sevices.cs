using Newtonsoft.Json;
using NotiFit.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NotiFit.Services
{
    public class Sevices
    {
        public async Task<ResponseGetPaymentPayPal> GetPaymentPayPalAsync(string uri, GetPaymentPayPal _objPaymentRequest)
        {

            ResponseGetPaymentPayPal objPaymentResponse;
            string s = JsonConvert.SerializeObject(_objPaymentRequest);
            HttpResponseMessage response = null;
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {
                HttpClient client = new HttpClient();
                response = await client.PostAsync(uri, stringContent);


                if (response.IsSuccessStatusCode)
                {
                    var SucessResponse = await response.Content.ReadAsStringAsync();
                    objPaymentResponse = JsonConvert.DeserializeObject<ResponseGetPaymentPayPal>(SucessResponse);
                    return objPaymentResponse;
                }
                else
                {
                    var ErrorResponse = await response.Content.ReadAsStringAsync();
                    objPaymentResponse = JsonConvert.DeserializeObject<ResponseGetPaymentPayPal>(ErrorResponse);
                    return objPaymentResponse;
                }
            }
        }
    }
}
