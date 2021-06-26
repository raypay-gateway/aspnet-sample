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
                AcceptorCode = Constants.AcceptorCode,
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
                    string token = JObject.Parse(ParametersJson)["Data"]["Accesstoken"].ToString();
                    string terminalID = JObject.Parse(ParametersJson)["Data"]["TerminalID"].ToString();
                   
                        Dictionary<string, string> PostData = new Dictionary<string, string>
                    {
                        { "token",token},
                        { "TerminalID", terminalID}
                    };

                        var shaparakUrl =  " https://mabna.shaparak.ir:8080/Pay ";

                         SendDataToShaparak(shaparakUrl, PostData);
                        return View();
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
                   "http://5.182.44.228:14000/raypay/api/v1/Payment/getPaymentTokenWithUserID", requestContent);
                HttpContent responseContent = response.Content;
                return await responseContent.ReadAsStringAsync();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void SendDataToShaparak(string varURL, Dictionary<string, string> varData)
        {
            string FormName = "raypaySubmitForm";
            string Method = "post";
            var sb = new StringBuilder();
            sb.Append("<html><head>");
            sb.Append($"</head><body onload=\"document.{FormName}.submit()\">");
            sb.Append($"<form name=\"{FormName}\" method=\"{Method}\" action=\"{varURL}\" >");
                foreach (var key in varData)
                        {
                            sb.Append(
                                $"<input name=\"{WebUtility.HtmlEncode(key.Key)}\" type=\"hidden\" value=\"{WebUtility.HtmlEncode(key.Value)}\">");
                        }
            sb.Append("</form>");
            sb.Append("</body></html>");

            var data = Encoding.UTF8.GetBytes(sb.ToString());

            //modify the response
            var httpContext = _httpContextAccessor.HttpContext;
            var response = httpContext.Response;

            //change headers before the content is written to body
            response.OnStarting(() =>
            {
                response.ContentType = "text/html; charset=utf-8";
                response.ContentLength = data.Length;

                return Task.CompletedTask;
            });

            response.Clear();
            response.Body
                .WriteAsync(data, 0, data.Length)
                .Wait();
        }
    }
}
