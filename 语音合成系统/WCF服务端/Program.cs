using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace WCF服务端
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建一个基地址,MyService名称自己起
            Uri uri = new Uri("http://47.94.102.147:8000/TTSService");
            //创建一个Servicehost对象
            ServiceHost host = new ServiceHost(typeof(TTSService), uri);
            WSHttpBinding bind = new WSHttpBinding();
            bind.Security.Mode = SecurityMode.None;
            //为一个服务添加终结点,myHello名称自己起
            host.AddServiceEndpoint(typeof(ITTSContract), bind, "TTSInterface");
            //建立元数据行为对象
            ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
            //设置httpgetEnabled属性
            behavior.HttpGetEnabled = true;
            //把这个服务元数据行为对象添加到宿主ServiceHost对象的行为集合中去
            host.Description.Behaviors.Add(behavior);
            //启动宿主，开启服务
            host.Open();
            Console.WriteLine("host is ready");
            Console.ReadLine();
            //关闭服务
            host.Close();

        }
    }
}
