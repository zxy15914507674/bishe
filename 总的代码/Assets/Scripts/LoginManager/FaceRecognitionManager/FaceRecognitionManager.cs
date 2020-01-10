using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;
using UnityEngine;

public class FaceRecognitionManager{


    FaceServiceConstract.IFaceContract channel;
    public FaceRecognitionManager()
    {
        EndpointAddress address = new EndpointAddress("http://47.94.102.147:8000/FaceService/FaceInterface");
        WSHttpBinding binding = new WSHttpBinding();
        binding.Security.Mode = SecurityMode.None;
        ChannelFactory<FaceServiceConstract.IFaceContract> factory = new ChannelFactory<FaceServiceConstract.IFaceContract>(binding, address);
        channel = factory.CreateChannel();
    }

    /// <summary>
    /// 人脸注册
    /// </summary>
    /// <param name="faceBuffer1">人脸数据1</param>
    /// <param name="faceBuffer2">人脸数据2</param>
    /// <param name="faceBuffer3">人脸数据3</param>
    /// <param name="face_id">人脸id</param>
    /// <returns>0：代表注册失败  1代表注册成功</returns>
    public int FaceRegistration(byte[] faceBuffer1, byte[] faceBuffer2, byte[] faceBuffer3, string face_id) {
        return channel.FaceRegistration(faceBuffer1,faceBuffer2,faceBuffer3,face_id);
    }


    /// <summary>
    /// 人脸登录
    /// </summary>
    /// <param name="faceBuffer">人脸数据</param>
    /// <returns>返回的是识别出的face_id,如果返回null或者是unknown，则表示识别不出</returns>
    public string FaceLogin(byte[] faceBuffer) {

        return  channel.FaceLogin(faceBuffer);
    }

}
