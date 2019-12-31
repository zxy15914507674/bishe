﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OMCS.Passive;
using System.Configuration;

/*
* 本demo采用的是OMCS的免费版本。若想获取OMCS其它版本，请联系 www.oraycn.com 。
* 
*/
namespace AudioChatRoom
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                string[] args = Environment.GetCommandLineArgs();
                if (args == null || args.Length == 0)
                {
                    return;
                }

                string CurrentUserID = DateTime.Now.ToString("hhmmssffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                string RoomId = args[0];
                IMultimediaManager multimediaManager = MultimediaManagerFactory.GetSingleton();
                multimediaManager.CameraVideoSize = new System.Drawing.Size(320, 240);
                multimediaManager.AutoAdjustCameraEncodeQuality = false;
                multimediaManager.ChannelMode = ChannelMode.P2PDisabled;
                multimediaManager.SecurityLogEnabled = false;
                multimediaManager.CameraDeviceIndex = 0;
                multimediaManager.MicrophoneDeviceIndex = int.Parse(ConfigurationManager.AppSettings["MicrophoneIndex"]);
                multimediaManager.SpeakerIndex = int.Parse(ConfigurationManager.AppSettings["SpeakerIndex"]);
                multimediaManager.DesktopEncodeQuality = 3;
                multimediaManager.Initialize(CurrentUserID, "", ConfigurationManager.AppSettings["ServerIP"], int.Parse(ConfigurationManager.AppSettings["ServerPort"]));

                MainForm mainForm = new MainForm(multimediaManager, RoomId);
                Application.Run(mainForm);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}
