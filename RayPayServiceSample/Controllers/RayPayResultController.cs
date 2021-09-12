using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RayPayServiceSample.Model;
using RayPayServiceSample.Models;

namespace RayPayServiceSample.Controllers
{
    public class RayPayResultController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RayPayResultController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
           
        }

        [HttpPost]
        public async Task<IActionResult> Index(PaymentCallbackParam param)
        {
            PaymentResultModel model = new PaymentResultModel();
            try
            {
                if (param != null)
                {
                    string result = await Verify(param);
                    int paymentStatus = Convert.ToInt32(JObject.Parse(result)["Data"]["Status"]);
                    if (paymentStatus == 1)
                        model.IsSuccess = true;
                    else
                        model.IsSuccess = false;

                    model.FactorNumber = JObject.Parse(result)["Data"]["FactorNumber"].ToString();
                    model.InvoiceID = JObject.Parse(result)["Data"]["InvoiceID"].ToString();
                    model.WritHeaderID = JObject.Parse(result)["Data"]["WritheaderID"].ToString();
                    model.Comments = JObject.Parse(result)["Data"]["comment"].ToString();
                    model.Mobile = JObject.Parse(result)["Data"]["mobile"].ToString();
                    model.Email = JObject.Parse(result)["Data"]["email"].ToString();
                    model.FullName = JObject.Parse(result)["Data"]["fullName"].ToString();
                    model.Message = JObject.Parse(result)["Message"].ToString();
                    model.Amount = JObject.Parse(result)["Data"]["Amount"].ToString();
                }
                else
                {
                    //Response.Redirect("~/", false);
                    ModelState.AddModelError(string.Empty, "خطا در بازگشت از درگاه رخ داده است");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "خطا رخ داده است");
                return View();
            }
            return View(model);
        }
        public async Task<string> Verify(PaymentCallbackParam sepehrData)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(sepehrData);
            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await client.PostAsync(
              "https://api.raypay.ir/raypay/api/v1/Payment/verify", requestContent);
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
