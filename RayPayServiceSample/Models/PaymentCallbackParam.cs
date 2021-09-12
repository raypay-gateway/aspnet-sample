namespace RayPayServiceSample.Model
{
    public class PaymentCallbackParam
    {
        //public int respcode { get; set; }
        public string respcode { get; set; }
        public string respmsg { get; set; }
        //public long amount { get; set; }
        public string amount { get; set; }
        public string invoiceid { get; set; }
        public string payload { get; set; }
        //public long terminalid { get; set; }
        //public long tracenumber { get; set; }
        //public long rrn { get; set; }
        public string terminalid { get; set; }
        public string tracenumber { get; set; }
        public string rrn { get; set; }
        public string datePaid { get; set; }
        public string digitalreceipt { get; set; }

        public string hash { get; set; }
        public string issuerbank { get; set; }
        //public long billid { get; set; }
        //public long payid { get; set; }
        public string cardnumber { get; set; }
        //public string pincharge { get; set; }
        //public string refcharge { get; set; }
        //public string serialcharge { get; set; }
    }

}
