using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF服务端
{
    public class FaceService:IFaceContract
    {

        FaceProxyInterface proxy = (FaceProxyInterface)XmlRpcProxyGen.Create(typeof(FaceProxyInterface));

        /// <summary>
        /// 人脸注册
        /// </summary>
        /// <param name="faceBuffer1">人脸数据1</param>
        /// <param name="faceBuffer2">人脸数据2</param>
        /// <param name="faceBuffer3">人脸数据3</param>
        /// <param name="face_id">人脸id</param>
        /// <returns>返回是否成功  0：失败  1：成功</returns>
        public int FaceRegistration(byte[] faceBuffer1, byte[] faceBuffer2, byte[] faceBuffer3, string face_id)
        {
            string face1 = Convert.ToBase64String(faceBuffer1);
            string face2 = Convert.ToBase64String(faceBuffer2);
            string face3 = Convert.ToBase64String(faceBuffer3);

            string FaceAndFaceId = "['{0}','{1}','{2}','{3}']";
            FaceAndFaceId = string.Format(FaceAndFaceId, face1, face2, face3, face_id);
            try
            {
                proxy.FaceRegistration(FaceAndFaceId);
                return 1;
            }
            catch (Exception ex)
            {

                Console.WriteLine("FaceRegistration发生异常:"+ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// 人脸登录
        /// </summary>
        /// <param name="faceBuffer">人脸数据</param>
        /// <returns>识别出的id号</returns>
        public string FaceLogin(byte[] faceBuffer)
        {
            string face = Convert.ToBase64String(faceBuffer);
            try
            {
                return proxy.FaceLogin(face);
            }
            catch (Exception ex)
            {

                Console.WriteLine("FaceLogin发生异常:"+ex.Message);
            }
            return "unknown";
        }
    }


    [XmlRpcUrl("http://47.94.102.147:666")]
    public interface FaceProxyInterface : IXmlRpcProxy
    {
        [XmlRpcMethod("FaceRegistration")]
        void FaceRegistration(string imgAndface_id);

        [XmlRpcMethod("FaceLogin")]
        string FaceLogin(string img);
    }
}
