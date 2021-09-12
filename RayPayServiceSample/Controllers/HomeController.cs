using System;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RayPayServiceSample.Models;
using Newtonsoft.Json.Linq;
using System.Net;

namespace RayPayServiceSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            PaymentGetTokenModel model = new PaymentGetTokenModel
            {
                UserID = Constants.UserID,
                MarketingID = Constants.MarketingID,
                InvoiceID = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds().ToString(),
                RedirectUrl = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + "/RayPayResult/Index",
                Amount = "1000"
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(PaymentGetTokenModel model)
        {
                try
                {
                    var result = Pay(model).Result;
         
                PaymentTokenResponseModel Parameters = JsonConvert.DeserializeObject<PaymentTokenResponseModel>(result);
                var ParametersJson = JsonConvert.SerializeObject(Parameters);

                if (Parameters.IsSuccess == true)
                    {
                    string jwtToken= JObject.Parse(ParametersJson)["Data"].ToString();

                    _httpContextAccessor.HttpContext.Response.Redirect("https://my.raypay.ir/IPG?token=" + jwtToken);
                }
                    else {
                        throw new Exception(" - خطا در اتصال به سرویس پرداخت رای پی - " + Parameters.Message);
                    }

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<string> Pay(PaymentGetTokenModel model)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(model);
            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await client.PostAsync(
                "https://api.raypay.ir/raypay/api/v1/Payment/pay", requestContent);
                HttpContent responseContent = response.Content;
                return await responseContent.ReadAsStringAsync();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
