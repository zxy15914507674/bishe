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
            Uri uri1 = new Uri("http://47.94.102.147:8000/TTSService");
            Uri uri2 = new Uri("http://47.94.102.147:8000/HandService");
            Uri uri3 = new Uri("http://47.94.102.147:8000/QAService");



            //创建一个Servicehost对象
            ServiceHost host1 = new ServiceHost(typeof(TTSService), uri1);
            ServiceHost host2 = new ServiceHost(typeof(HandService), uri2);
            ServiceHost host3 = new ServiceHost(typeof(QAService), uri3);

            WSHttpBinding bind1 = new WSHttpBinding();
            bind1.Security.Mode = SecurityMode.None;

            WSHttpBinding bind2 = new WSHttpBinding();
            bind2.Security.Mode = SecurityMode.None;

            WSHttpBinding bind3 = new WSHttpBinding();
            bind3.Security.Mode = SecurityMode.None;


            //为一个服务添加终结点,myHello名称自己起
            host1.AddServiceEndpoint(typeof(ITTSContract), bind1, "TTSInterface");
            host2.AddServiceEndpoint(typeof(IHandContract), bind2, "HandGestureInterface");
            host3.AddServiceEndpoint(typeof(IQAContract), bind3, "QAInterface");
            
            //建立元数据行为对象
            ServiceMetadataBehavior behavior1 = new ServiceMetadataBehavior();
            //设置httpgetEnabled属性
            behavior1.HttpGetEnabled = true;
            //把这个服务元数据行为对象添加到宿主ServiceHost对象的行为集合中去
            host1.Description.Behaviors.Add(behavior1);


            //建立元数据行为对象
            ServiceMetadataBehavior behavior2 = new ServiceMetadataBehavior();
            //设置httpgetEnabled属性
            behavior2.HttpGetEnabled = true;
            //把这个服务元数据行为对象添加到宿主ServiceHost对象的行为集合中去
            host2.Description.Behaviors.Add(behavior2);


            //建立元数据行为对象
            ServiceMetadataBehavior behavior3 = new ServiceMetadataBehavior();
            //设置httpgetEnabled属性
            behavior3.HttpGetEnabled = true;
            //把这个服务元数据行为对象添加到宿主ServiceHost对象的行为集合中去
            host3.Description.Behaviors.Add(behavior3);



            //启动宿主，开启服务
            host1.Open();
            host2.Open();
            host3.Open();


            Console.WriteLine("host is ready");
            Console.ReadLine();
            //关闭服务
            host1.Close();
            host2.Close();
            host3.Close();
        }
    }
}
