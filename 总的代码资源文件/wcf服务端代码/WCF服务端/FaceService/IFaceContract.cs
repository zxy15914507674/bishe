using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF服务端
{
    [ServiceContract]
    public interface IFaceContract
    {
        /// <summary>
        /// 人脸注册
        /// </summary>
        /// <param name="faceBuffer1">人脸数据1</param>
        /// <param name="faceBuffer2">人脸数据2</param>
        /// <param name="faceBuffer3">人脸数据3</param>
        /// <param name="face_id">人脸id</param>
        /// <returns>注册结果  0:失败  1：成功</returns>
        [OperationContract]
        int FaceRegistration(byte[] faceBuffer1, byte[] faceBuffer2, byte[] faceBuffer3,string face_id);

        /// <summary>
        /// 人脸登录
        /// </summary>
        /// <param name="faceBuffer">输入的人脸数据</param>
        /// <returns>返回识别出的face_id</returns>
        [OperationContract]
        string FaceLogin(byte[] faceBuffer);
    }
}
