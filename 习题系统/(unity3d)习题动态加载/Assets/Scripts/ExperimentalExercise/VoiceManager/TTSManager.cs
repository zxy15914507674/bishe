using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;
using UnityEngine;


namespace fvc.exp.voice {
    public class TTSManager : System.IDisposable
    {
        EndpointAddress address;
        WSHttpBinding binding;
        TTSServiceConstract.ITTSContract channel;
        ChannelFactory<fvc.exp.voice.TTSServiceConstract.ITTSContract> factory;
        public TTSManager()
        {
            address=new EndpointAddress("http://47.94.102.147:8000/TTSService/TTSInterface");
            binding = new WSHttpBinding();
            binding.Security.Mode = SecurityMode.None;
            factory = new ChannelFactory<fvc.exp.voice.TTSServiceConstract.ITTSContract>(binding, address);
            channel = factory.CreateChannel();
         
        }

        public void ConvertAndPlay(AudioSource audioSource, string convertMsg)
        {
            if (audioSource == null || convertMsg == null || convertMsg.Trim().Length == 0)
            {
                Debug.LogError("TTSManager中的ConvertAndPlay方法参数不对");
                return;
            }
            
            byte[] buffer = channel.GetTTSBytes(convertMsg);

            AudioClip audioClip = NAudioPlayer.FromWavData(buffer);

            audioSource.clip = audioClip;
           
            audioSource.Play();

            
           
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            ((IClientChannel)channel).Close(); 
            Debug.Log("执行关闭操作");

            
            factory.Abort();
           
            factory.Close();
          
            
        }
        ~TTSManager() {
            Debug.Log("析构函数调用");
        }
    }
}


