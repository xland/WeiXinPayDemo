
**项目描述：**

> 本项目是DCloud微信支付C#.NET服务端的SDK和DEMO
 
> WXPaySDK目录下是服务端的SDK（与微信服务交互的核心代码都在这里）
> WXPaySDKDemo目录下服务端的DEMO
> H5目录下是客户端的DEMO

**使用方法：**
```
var payment = new Payment("******",
                "******", 
                "******************", 
                "http://www.yourdomain.com/Notify.aspx");
var orderId = "TS" + DateTime.Now.ToString("yyyyMMddhhmmssffff");
var jsonStr = payment.Pay(100, orderId, "卖肉钱", "127.0.0.1");
```
>构造函数的四个参数：
>MchId：微信支付商户号（从微信发给你的邮件中获得的）
>AppId：应用的APPID（微信发给你的邮件中也有这项内容，一般以wx开头，微信开放平台-管理中心-应用详情也可以看到这项内容）
>ApiKey：这里是API密钥，不是Appsecret，这里最容易出错了！请务必注意！设置方法：微信商户平台(pay.weixin.qq.com)-->账户设置-->API安全-->密钥设置
>NotifyUrl：支付成功之后微信回调页面地址

>Pay方法的五个参数：
>TotalFee：总金额，单位：分，不能为空
>TradeNo：订单号，你自己定就好了，不要重复，不能为空
>Des：订单描述，不能为空
>ClientIp：客户端的IP地址，不能为空
>FeeType：货币类型，默认是CNY，人民币
>Pay方法返回的是一个JSON字符串，把这个JSON字符串返回给DCLOUD客户端即可

>客户端的关键代码：
```
var payChanel;
function getPayChanel() {
	//设备信息加载完成，先获取微信的支付通道
	plus.payment.getChannels(function(channels) {
		for (var i in channels) {
			if (channels[i].id == "wxpay") {
				payChanel = channels[i];
			}
		}
	}, function(e) {
		alert("获取支付通道失败：" + e.message);
	});
}
function payClick() {
	//支付按钮点击事件
	//为了演示这里并没有发送数据给服务端，订单所需数据都是在服务端生成的
	mui.post("http://www.yourdomain.net/OrderService.aspx", {}, function(data) {
		console.log(JSON.stringify(data));
		plus.payment.request(payChanel, data, function(result) {
			alert(JSON.stringify(result));
			alert("付费成功");
		}, function(e) {
			alert(JSON.stringify(e));
			alert("付费失败");
		});
	});
}
mui.init();
mui.plusReady(function() {
	getPayChanel();
	document.getElementById("payBtn").addEventListener("tap", payClick)
});
```
>其中mui.post回调函数的返回值，就是服务端返回的json字符串

**其他说明：**
>开发者：liulun（blog：http://www.cnblogs.com/liulun）
>PHP的DEMO：https://github.com/dcloudio/H5P.Server
>有什么问题，欢迎在这里开issue，我会尽快回复