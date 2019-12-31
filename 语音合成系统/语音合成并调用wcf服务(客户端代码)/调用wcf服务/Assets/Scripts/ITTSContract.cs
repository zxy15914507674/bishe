using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;


namespace TTSServiceConstract
{
    [ServiceContract]
    public interface ITTSContract
    {
        [OperationContract]
        byte[] GetTTSBytes(string str);
    }
}
