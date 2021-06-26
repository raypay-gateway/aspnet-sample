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
using Newtonsoft.Json.Linq;
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

        public async Task<IActionResult> IndexAsync()
        {
            PaymentResultModel model = new PaymentResultModel();
            try
            {
                if (_httpContextAccessor.HttpContext.Request.Query["invoiceID"] != string.Empty)
                {
                    string invoiceID = _httpContextAccessor.HttpContext.Request.Query["invoiceID"];

                    string result = await Verify(invoiceID);

                    int paymentStatus = Convert.ToInt32(JObject.Parse(result)["Data"]["State"]);
                    if (paymentStatus == 1)
                        model.IsSuccess = true;
                    else
                        model.IsSuccess = false;

                    model.FactorNumber = JObject.Parse(result)["Data"]["FactorNumber"].ToString();
                   
                    model.InvoiceID = JObject.Parse(result)["Data"]["InvoiceID"].ToString();
                    model.Comments = JObject.Parse(result)["Data"]["Comments"].ToString();
                    model.Mobile = JObject.Parse(result)["Data"]["Mobile"].ToString();
                    model.Amount = JObject.Parse(result)["Data"]["Amount"].ToString();
                }
                else
                    Response.Redirect("~/", false);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,"خطا رخ داده است");
                return View();
            }
            return View(model);
        }
        public async Task<string> Verify(string invoiceID)
        {
            var client = new HttpClient();
            var requestContent = new StringContent("data", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(
                "http://5.182.44.228:14000/raypay/api/v1/Payment/checkInvoice?pInvoiceID=" + invoiceID, requestContent);
            HttpContent responseContent = response.Content;

            var jsonString = await responseContent.ReadAsStringAsync();

            return jsonString;
        }
    }
}
