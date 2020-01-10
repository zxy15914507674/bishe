import cv2
import os
import numpy as np
from PIL import Image
import datetime
import csv

Path = r"C:\AnacondaInstall\Lib\site-packages\cv2\data\haarcascade_frontalface_default.xml"
face_detector = cv2.CascadeClassifier(Path)
 
#图像采集,为了保证准确性，需要一次性采集多张图片
def data_collection(img,face_id):
        # 转为灰度图片
        gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
 
        # 检测人脸
        faces = face_detector.detectMultiScale(gray, 1.3, 5)
 
        for (x, y, w, h) in faces:
            cv2.rectangle(img, (x, y), (x + w, y + w), (255, 0, 0))
            # 保存图像
            cv2.imwrite("facedata/Member." + face_id +'.jpg', gray[y: y + h, x: x + w])
            #cv2.imshow('data collection', img)

#人脸训练  
def face_training():
    # 人脸数据路径
    path = './facedata'

    recognizer = cv2.face.LBPHFaceRecognizer_create()
 
    def getImagesAndLabels(path):
        imagePaths = [os.path.join(path, f) for f in os.listdir(path)]  # join函数将多个路径组合后返回
        faceSamples = []
        ids = []
        for imagePath in imagePaths:
            if imagePath[-3:]=='jpg' or imagePath[-3:]=='png':
                PIL_img = Image.open(imagePath).convert('L')  # convert it to grayscale
                img_numpy = np.array(PIL_img, 'uint8')
                id_number = int(os.path.split(imagePath)[-1].split(".")[1])
                faceSamples.append(img_numpy)
                ids.append(id_number)
         
        return faceSamples, ids
 
    print('数据训练中')
    faces, ids = getImagesAndLabels(path)
    recognizer.train(faces, np.array(ids))
 
    recognizer.write(r'.\trainer.yml')
    # print("{0} faces trained. Exiting Program".format(len(np.unique(ids))))
    print("训练完成")

def face_ientification(img):
    recognizer = cv2.face.LBPHFaceRecognizer_create()
    recognizer.read('./trainer.yml')
    faceCascade = cv2.CascadeClassifier(Path)
    font = cv2.FONT_HERSHEY_SIMPLEX
 
    idnum = 0
    global namess
  
  
    #图像灰度处理
    gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
    grayInfo=gray.shape
    #设置大小
    minW = grayInfo[1]*0.01
    minH =grayInfo[0]*0.01
    
    # 将人脸用vector保存各个人脸的坐标、大小（用矩形表示）
    faces = faceCascade.detectMultiScale(
            gray,
            scaleFactor=1.2,#表示在前后两次相继的扫描中，搜索窗口的比例系数
            minNeighbors=5,#表示构成检测目标的相邻矩形的最小个数(默认为3个)
            minSize=(int(minW), int(minH))#minSize和maxSize用来限制得到的目标区域的范围
        )
    
    for (x, y, w, h) in faces:
            cv2.rectangle(img, (x, y), (x + w, y + h), (0, 255, 0), 2)
            idnum, confidence = recognizer.predict(gray[y:y + h, x:x + w])
 
            if confidence < 100:
                confidence =round(100 - confidence)
                #打印可信度
                print(confidence)
                #识别出来的id号
                print(idnum)
                return str(idnum)
            else:
                return 'unknown'
