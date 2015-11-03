using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WXPaySDK;

namespace WXPaySDKDemo
{
    public partial class OrderService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var payment = new Payment("******",
                "******", 
                "******************", 
                "http://www.yourdomain.com/Notify.aspx");
            var orderId = "TS" + DateTime.Now.ToString("yyyyMMddhhmmssffff");
            var jsonStr = payment.Pay(100, orderId, "卖肉钱", "127.0.0.1");
        }
    }
}