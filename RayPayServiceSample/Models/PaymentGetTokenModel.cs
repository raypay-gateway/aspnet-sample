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
        [Required(ErrorMessage = "کد پذیرنده الزامی است")]
        public string AcceptorCode { get; set; }


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
        public string FullName { get; set; }

        /// <summary>
        /// شماره تلفن پرداخت کننده
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// ایمیل پرداخت کننده
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// شماره فاکتور
        /// </summary>
        public string FactorNumber { get; set; }

        /// <summary>
        /// توضیحات
        /// </summary>
        public string Comment { get; set; }

    }


}