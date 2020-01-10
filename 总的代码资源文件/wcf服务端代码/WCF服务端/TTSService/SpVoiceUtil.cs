using DotNetSpeech;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 语音合成
{
    class SpVoiceUtil
    {
        SpVoice voice= new DotNetSpeech.SpVoiceClass();

        public SpVoiceUtil(int Rate,int Volume)
        {
            voice.Rate = Rate;
            voice.Volume = Volume;
        }
        
        /// <summary>
        /// 设置语速
        /// </summary>
        /// <param name="n"></param>
        public void setRate(int n)
        {
            voice.Rate = n;
        }

        /// <summary>
        /// 设置声音大小
        /// </summary>
        /// <param name="n"></param>
        public void setVolume(int n)
        {
            voice.Volume = n;
        }



        /// <summary>
        /// 输出WAV
        /// </summary>
        /// <param name="path">保存路径</param>
        /// <param name="str">要转换的文本内容</param>
        /// <returns></returns>
        public bool WreiteToWAV(string path, string str, SpeechAudioFormatType SpAudioType)
        {
            SpeechStreamFileMode SpFileMode = SpeechStreamFileMode.SSFMCreateForWrite;
            SpFileStream SpFileStream = new SpFileStream();
            SpeechVoiceSpeakFlags SpFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
            SpAudioFormat SpAudio = new DotNetSpeech.SpAudioFormat();
            SpAudio.Type = SpAudioType;
            SpFileStream.Format = SpAudio;
            SpFileStream.Open(path, SpFileMode, false);
         
            voice.AudioOutputStream = SpFileStream;
            voice.Speak(str, SpFlags);//阅读函数
            voice.WaitUntilDone(Timeout.Infinite);
            SpFileStream.Close();
            return File.Exists(path);
        }


        /// <summary>
        /// 把字节数组写入wav文件中
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="buffer"></param>
        public void WriteByteToWav(string filePath,byte []buffer,int Length) {
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
            fs.Write(buffer, 0, Length);
            fs.Close();
        }

        /// <summary>
        /// 文本转语音并转byte数组
        /// </summary>
        /// <param name="str">要写入的文本</param>
        /// <param name="SpAudioType">类型</param>
        /// <returns></returns>
        public byte[] GetByteByString(string str, SpeechAudioFormatType SpAudioType)
        {
            string path = DateTime.Now.ToString("yyyyMMddHHmmssms") + ".wav";

            SpeechStreamFileMode SpFileMode = SpeechStreamFileMode.SSFMCreateForWrite;
            SpFileStream SpFileStream = new SpFileStream();
            SpeechVoiceSpeakFlags SpFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
            SpAudioFormat SpAudio = new DotNetSpeech.SpAudioFormat();
            SpAudio.Type = SpAudioType;
            SpFileStream.Format = SpAudio;
            SpFileStream.Open(path, SpFileMode, false);

            voice.AudioOutputStream = SpFileStream;
            voice.Speak(str, SpFlags);//阅读函数
            voice.WaitUntilDone(Timeout.Infinite);
            SpFileStream.Close();
            
            FileStream fs = new FileStream(path, FileMode.Open);
            byte [] buffer=new byte[fs.Length];
            //txtBoxLength.Text = "长度为:" + Length;
           int Length=fs.Read(buffer, 0, buffer.Length);
           fs.Close();
           if (File.Exists(path)) {
               File.Delete(path);
           }
           return buffer;
        }
    }
}
