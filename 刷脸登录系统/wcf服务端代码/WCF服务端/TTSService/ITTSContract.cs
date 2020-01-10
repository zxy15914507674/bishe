using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF服务端
{
    [ServiceContract]
    public interface ITTSContract
    {
        [OperationContract]
        byte[] GetTTSBytes(string str);
    }
}
