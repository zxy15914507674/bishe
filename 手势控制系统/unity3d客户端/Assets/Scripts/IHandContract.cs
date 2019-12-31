using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;


namespace HandServiceConstract
{
    [ServiceContract]
    interface IHandContract
    {
        [OperationContract]
       string GetGestureInfo(byte[] buffer);
    }
}
