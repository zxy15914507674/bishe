using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 语音合成;

namespace WCF服务端
{
    public class TTSService:ITTSContract
    {
        
        public byte[] GetTTSBytes(string str)
        {
            try
            {
                SpVoiceUtil SpVoiceUtilObj = new SpVoiceUtil(0, 100);
                return SpVoiceUtilObj.GetByteByString(str, DotNetSpeech.SpeechAudioFormatType.SAFTCCITT_uLaw_11kHzMono);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
