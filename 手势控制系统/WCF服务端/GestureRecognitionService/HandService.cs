using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF服务端
{
    public class HandService:IHandContract
    {
        public string GetGestureInfo(byte[] buffer)
        {
            ProxyInterface proxy = (ProxyInterface)XmlRpcProxyGen.Create(typeof(ProxyInterface));
            string ImageString = Convert.ToBase64String(buffer);
            try
            {
                return proxy.getHelloWorld(ImageString).ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("发生异常");
                return "";
            }
           
            
        }
    }



    [XmlRpcUrl("http://47.94.102.147:666")]
    public interface ProxyInterface : IXmlRpcProxy
    {
        [XmlRpcMethod("getHelloWorld")]
        string getHelloWorld(string ImageString);

    }
}
