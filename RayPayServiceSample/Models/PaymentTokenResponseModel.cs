using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RayPayServiceSample.Models
{
    public class PaymentTokenResponseModel
    {
        public dynamic Data { get; set; }

        public int StatusCode { get; set; }
        public Boolean IsSuccess { get; set; }
        public string ValidationErrors { get; set; }


        public string Message { get; set; }


        public PaymentTokenResponseModel()
        {
        }
    }
}
