using System.ComponentModel.DataAnnotations;

namespace RayPayServiceSample.Models
{
    public class PaymentGetTokenModel
    {
        /// <summary>
        /// شناسه کاربری
        /// </summary>
        [Required(ErrorMessage = "شناسه کاربری الزامی است")]
        public string UserID { get; set; }

        /// <summary>
        /// کد پذیرنده
        /// </summary>
        [Required(ErrorMessage = "شناسه کسب و کار الزامی است")]
        public string MarketingID { get; set; }


        /// <summary>
        /// آدرس بازگشت
        /// </summary>
        [Required(ErrorMessage = "آدرس بازگشت الزامی است")]
        public string RedirectUrl { get; set; }


        /// <summary>
        /// مبلغ
        /// </summary>
        [Required(ErrorMessage = "مبلغ الزامی است")]
        public string Amount { get; set; }


        /// <summary>
        /// شناسه پرداخت
        /// </summary>
        [Required(ErrorMessage = "شناسه پرداخت الزامی است و باید به صورت یکتا تولید شود.")]
        public string InvoiceID { get; set; }


        /// <summary>
        /// نام پرداخت کننده
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FullName { get; set; }

        /// <summary>
        /// شماره تلفن پرداخت کننده
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Mobile { get; set; }

        /// <summary>
        /// ایمیل پرداخت کننده
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Email { get; set; }

        /// <summary>
        /// شماره فاکتور
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FactorNumber { get; set; }

        /// <summary>
        /// توضیحات
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Comment { get; set; }

        public bool EnableSandBox { get; set; }

    }


}