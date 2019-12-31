using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour {

    public Button btnConvert;
    public InputField InputStr;
    public AudioSource audioSource;
	void Start () {
        btnConvert.onClick.AddListener(BtnConvertClick);
	}

    void BtnConvertClick() {
        EndpointAddress address = new EndpointAddress("http://47.94.102.147:8000/TTSService/TTSInterface");
        WSHttpBinding binding = new WSHttpBinding();
        binding.Security.Mode = SecurityMode.None;
        ChannelFactory<TTSServiceConstract.ITTSContract> factory = new ChannelFactory<TTSServiceConstract.ITTSContract>(binding, address);
        TTSServiceConstract.ITTSContract channel = factory.CreateChannel();
        //TTSServiceNamespace.TTSContractClient tts = new TTSServiceNamespace.TTSContractClient(new BasicHttpBinding(), new EndpointAddress(new Uri("http://47.94.102.147:8000/TTSService")));
        string str = InputStr.text.Trim();
        byte[] buffer = channel.GetTTSBytes(str);
        //FileStream fs = new FileStream("Write.wav", FileMode.OpenOrCreate);
        //fs.Write(buffer, 0, buffer.Length);
        //fs.Close();
        GameObject.Find("MessageLog").GetComponent<Text>().text = "转换完成"+DateTime.Now.ToString();
        //AudioClip audioClip= WavUtility.ToAudioClip(buffer);
        //float[] samples = new float[buffer.Length];
        //Buffer.BlockCopy(buffer, 0, samples, 0, buffer.Length);
        //AudioClip audioClip = AudioClip.Create("RecordClip", samples.Length, 1, 11000, false);

        AudioClip audioClip = NAudioPlayer.FromWavData(buffer);

        audioSource.clip = audioClip;
        
        audioSource.Play();
    }



}
