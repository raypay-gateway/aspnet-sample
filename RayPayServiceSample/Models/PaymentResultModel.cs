namespace RayPayServiceSample.Models
{
    public class PaymentResultModel
    {
        /// <summary>
        /// مبلغ
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// شناسه پرداخت
        /// </summary>
        public string InvoiceID { get; set; }

        /// <summary>
        /// شماره پیگیری
        /// </summary>
        public string WritHeaderID { get; set; }


        /// <summary>
        /// شماره فاکتور
        /// </summary>
        public string FactorNumber { get; set; }



        /// <summary>
        /// شماره تلفن پرداخت کننده
        /// </summary>
        public string Mobile { get; set; }

        /// ایمیل پرداخت کننده
        public string Email { get; set; }

        /// نام پرداخت کننده
        public string FullName { get; set; }


        /// <summary>
        /// توضیحات
        /// </summary>
        public string Comments { get; set; }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}