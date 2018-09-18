
#region DLL
using System;
using System.Windows;
using Microsoft.Kinect;
using GestureDefinition.Gesture.Stop;
using GestureDefinition.Gesture.Play;
using GestureDefinition.Gesture.SwipeLeft;
using GestureDefinition.Gesture.Web;
using GestureDefinition.Gesture.ZoomIn;
using GestureDefinition.Gesture.ZoomOut;
using GestureDefinition.Gesture.Volume;
using GestureDefinition.Gesture.SwipeRight;
using GestureDefinition.Gesture.goodPosition;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.AudioFormat;
using System.Collections.Generic;

#endregion
namespace GestureDefinition
{
    public class GestureDefinition
    {

        #region Properties
        public bool VoiceOn { set { slaveIsOn = !slaveIsOn; } }
        public double volume = 0.1;
        private string tempolaryWord = string.Empty;
        public bool GoodLocationForGestures = false;
        public bool slaveIsOn;
        public bool gamerIsOn;

        GestureEnum GestureEnum = new GestureEnum();

        public delegate void GestureDelegate(GestureEnum.GestureName eventNumber);
        public event GestureDelegate OnGestrue;
        GrammarBuilder gb = new GrammarBuilder();
        KinectSensor _sensor;
        RecognizerInfo voiceOrder;
        const int skeletonCount = 6;
        Skeleton[] allSkeletons = new Skeleton[skeletonCount];

        Choices world = new Choices();

        Stop stopGesture = new Stop();
        Play playGesture = new Play();
        SwipeLeft swipeLeftGesture = new SwipeLeft();
        SwipeRight swipeRightGesture = new SwipeRight();
        Web webGesture = new Web();
        ZoomIn zoomInGesture = new ZoomIn();
        ZoomOut zoomOutGesture = new ZoomOut();
        Volume volumeGesture = new Volume();
        GoodPosition goodPositionGesture = new GoodPosition();

        private SpeechRecognitionEngine speechEngine;

        #endregion

        #region Main Window Method

        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (KinectSensor.KinectSensors.Count > 0)
            {
                _sensor = KinectSensor.KinectSensors[0];

                try
                {
                    if (_sensor.Status == KinectStatus.Connected)
                    {
                        _sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                        _sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                        _sensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(_sensor_AllFramesReady);
                        _sensor.SkeletonStream.Enable();
                        _sensor.Start();
                    }

                    voiceOrder = GetKinectRecognizer();

                    if (null != voiceOrder)
                    {
                        speechEngine = new SpeechRecognitionEngine(voiceOrder.Id);
                        world.Add(new string[] { "BAYMAX", "PLAY", "EXIT", "STOP", "NEXT", "PREVIOUS", "HIDE", "SHOW",
                        "ORDER", "CORTANA", "BACK", "MOVIE", "MAXIMIZE", "MINIMIZE", "POSSIBLE.COM", "RIGHT", "LEFT",
                        "DOWN", "UP", "GAMER", "AGAIN" });
                        gb.Append(world);
                        Grammar g = new Grammar(gb);

                        speechEngine.LoadGrammar(g);
                        speechEngine.SpeechRecognized += SpeechRecognized;
                        //speechEngine.SpeechRecognitionRejected += SpeechRejected;
                        speechEngine.SetInputToAudioStream(_sensor.AudioSource.Start(), new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
                        speechEngine.RecognizeAsync(RecognizeMode.Multiple);
                    }
                    else
                    {
                        MessageBox.Show("No speech recognition engine installed!");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chack your sensor Kinect");
                }
            }
        }


        public void MainWindow_Closed(object sender, EventArgs e)
        {
            if (_sensor != null)
            {
                _sensor.Stop();
                _sensor.AudioSource.Stop();
            }
        }

        #endregion

        #region EventDelegate

        public void raiseEventDelegateGesture(GestureEnum.GestureName msg)
        {
            if (OnGestrue != null)
                OnGestrue(msg);
        }

        #endregion

        #region Gesture Detected

        public void _sensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrameData = e.OpenSkeletonFrame())
            {
                if (skeletonFrameData == null)
                    return;

                skeletonFrameData.CopySkeletonDataTo(allSkeletons);

                #region user detection

                foreach (Skeleton data in allSkeletons)
                {
                    if (SkeletonTrackingState.Tracked == data.TrackingState)
                    {
                        raiseEventDelegateGesture(GestureEnum.GestureName.DISABLESCREENSAVER);
                        break;
                    }
                    else
                        raiseEventDelegateGesture(GestureEnum.GestureName.ENABLESCREENSAVER);
                }

                #endregion

                #region gesture detection

                GoodLocationForGestures = goodPositionGesture.goodPosition(allSkeletons);

                if (goodPositionGesture.goodPosition(allSkeletons) == true)
                {
                    //Gesture STOP
                    raiseEventDelegateGesture(stopGesture.StopGesture(allSkeletons));
                    //Gesture SwipeRight
                    raiseEventDelegateGesture(swipeRightGesture.SwipeRightGesture(allSkeletons));
                    //Gesture SwipeLeft
                    raiseEventDelegateGesture(swipeLeftGesture.SwipeLeftGesture(allSkeletons));
                    //Gesture PLAY
                    raiseEventDelegateGesture(playGesture.PlayGesture(allSkeletons));
                    //Gesture OpenWebPage
                    //raiseEventDelegateGesture(webGesture.WebGesture(allSkeletons));
                    //Gesture ZoomIn
                    raiseEventDelegateGesture(zoomInGesture.ZoomInGesture(allSkeletons));
                    //Gesture ZoomOut
                    raiseEventDelegateGesture(zoomOutGesture.ZoomOutGesture(allSkeletons));
                    //Gesture Volume
                    raiseEventDelegateGesture(volumeGesture.VolumeGesture(allSkeletons));
                    volume = volumeGesture.volume;
                }

                #region Old part code
                //if (goodPositionGesture.goodPosition(allSkeletons) == true)
                //{
                //    Parallel.Invoke(() =>
                //    {
                //        //Gesture STOP
                //        raiseEventDelegateGesture(stopGesture.StopGesture(allSkeletons));
                //    },
                //        () =>
                //        {
                //            //Gesture SwipeRight
                //            raiseEventDelegateGesture(swipeRightGesture.SwipeRightGesture(allSkeletons));
                //        },
                //        () =>
                //        {
                //            //Gesture SwipeLeft
                //            raiseEventDelegateGesture(swipeLeftGesture.SwipeLeftGesture(allSkeletons));
                //        },
                //        () =>
                //        {
                //            //Gesture PLAY
                //            raiseEventDelegateGesture(playGesture.PlayGesture(allSkeletons));
                //        },
                //        () =>
                //        {
                //            //Gesture OpenWebPage
                //            //raiseEventDelegateGesture(webGesture.WebGesture(allSkeletons));
                //        },
                //        () =>
                //        {
                //            //Gesture ZoomIn
                //            raiseEventDelegateGesture(zoomInGesture.ZoomInGesture(allSkeletons));
                //        },
                //        () =>
                //        {
                //            //Gesture ZoomOut
                //            raiseEventDelegateGesture(zoomOutGesture.ZoomOutGesture(allSkeletons));
                //        },
                //        () =>
                //        {
                //            //Gesture Volume
                //            raiseEventDelegateGesture(volumeGesture.VolumeGesture(allSkeletons));
                //        },
                //        () =>
                //        {
                //            volume = volumeGesture.volume;
                //        }
                //    );
                //}

                //// GESTURE analyze X Y Z 
                //foreach (Skeleton skeleton in allSkeletons)
                //{
                //    if (SkeletonTrackingState.Tracked == skeleton.TrackingState)
                //    {
                //        //ShowXYZ = " HandRight X=" + (skeleton.Joints[JointType.HandRight].Position.X);
                //        //ShowXYZ1 = " HandRight Y=" + (skeleton.Joints[JointType.HandRight].Position.Y);
                //        //ShowXYZ2 = " ElbowLeft=" + Math.Abs((skeleton.Joints[JointType.HandRight].Position.X) - (skeleton.Joints[JointType.HandLeft].Position.X));
                //    }
                //}
                #endregion

                #endregion
            }
        }

        #endregion

        #region Speech methods

        private static RecognizerInfo GetKinectRecognizer()
        {
            foreach (RecognizerInfo recognizer in SpeechRecognitionEngine.InstalledRecognizers())
            {
                if (recognizer.Culture.TwoLetterISOLanguageName.Equals("en"))
                {
                    return recognizer;
                }
            }
            return null;
        }

        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            const double ConfidenceThreshold = 0.7;

            if (e.Result.Confidence >= ConfidenceThreshold)
            {
                //if (e.Result.Text.Equals("CORTANA"))
                //    slaveIsOn = !slaveIsOn;

                //else if (slaveIsOn == true)
                //{
                //    switch (e.Result.Text)
                //    {
                //        case "STOP":
                //            raiseEventDelegateGesture(GestureEnum.GestureName.STOP);
                //            break;
                //        case "EXIT":
                //            raiseEventDelegateGesture(GestureEnum.GestureName.STOP);
                //            break;
                //        case "PLAY":
                //            raiseEventDelegateGesture(GestureEnum.GestureName.PLAY);
                //            break;
                //        case "MOVIE":
                //            raiseEventDelegateGesture(GestureEnum.GestureName.PLAY);
                //            break;
                //        case "NEXT":
                //            raiseEventDelegateGesture(GestureEnum.GestureName.PRIEV);
                //            break;
                //        case "PREVIOUS":
                //            raiseEventDelegateGesture(GestureEnum.GestureName.NEXT);
                //            break;
                //        case "BACK":
                //            raiseEventDelegateGesture(GestureEnum.GestureName.NEXT);
                //            break;
                //        case "MINIMIZE":
                //            raiseEventDelegateGesture(GestureEnum.GestureName.ZOOMMINUS);
                //            break;
                //        case "HIDE":
                //            raiseEventDelegateGesture(GestureEnum.GestureName.ZOOMMINUS);
                //            break;
                //        case "MAXIMIZE":
                //            raiseEventDelegateGesture(GestureEnum.GestureName.ZOOMPLUS);
                //            break;
                //        case "SHOW":
                //            raiseEventDelegateGesture(GestureEnum.GestureName.ZOOMPLUS);
                //            break;
                //        case "POSSIBLE.COM":
                //            raiseEventDelegateGesture(GestureEnum.GestureName.POSSIBLE);
                //            break;
                //    }
                //}

                // if (e.Result.Text.Equals("GAMER"))
                //    gamerIsOn = !gamerIsOn;

                //else if (gamerIsOn == true)
                //{
                //    switch (e.Result.Text)
                //    {
                //        case "RIGHT":
                //            raiseEventDelegateGesture(GestureEnum.GestureName.ZOOMPLUS);
                //            break;
                //        case "LEFT":
                //            raiseEventDelegateGesture(GestureEnum.GestureName.LEFT);
                //            break;
                //        case "DOWN":
                //            raiseEventDelegateGesture(GestureEnum.GestureName.DOWN);
                //            break;
                //        case "UP":
                //            raiseEventDelegateGesture(GestureEnum.GestureName.UP);
                //            break;
                //        case "AGAIN":
                //            raiseEventDelegateGesture(GestureEnum.GestureName.AGAIN);
                //            break;
                //    }
                //}

                if (e.Result.Text.Equals("CORTANA") || e.Result.Text.Equals("BAYMAX"))
                    slaveIsOn = !slaveIsOn;
                else if ((e.Result.Text.Equals("STOP") || e.Result.Text.Equals("EXIT")) && slaveIsOn == true)
                    raiseEventDelegateGesture(GestureEnum.GestureName.STOP);
                else if ((e.Result.Text.Equals("PLAY") || e.Result.Text.Equals("MOVIE")) && slaveIsOn == true)
                    raiseEventDelegateGesture(GestureEnum.GestureName.PLAY);
                else if (e.Result.Text.Equals("NEXT") && slaveIsOn == true)
                    raiseEventDelegateGesture(GestureEnum.GestureName.PRIEV);
                else if ((e.Result.Text.Equals("BACK") || e.Result.Text.Equals("PREVIOUS")) && slaveIsOn == true)
                    raiseEventDelegateGesture(GestureEnum.GestureName.NEXT);
                else if ((e.Result.Text.Equals("HIDE") || e.Result.Text.Equals("MINIMIZE")) && slaveIsOn == true)
                    raiseEventDelegateGesture(GestureEnum.GestureName.MINIMALIZE);
                else if ((e.Result.Text.Equals("SHOW") || e.Result.Text.Equals("MAXIMIZE")) && slaveIsOn == true)
                    raiseEventDelegateGesture(GestureEnum.GestureName.MAXYMALIZE);
                else if (e.Result.Text.Equals("POSSIBLE.COM") && slaveIsOn == true)
                    raiseEventDelegateGesture(GestureEnum.GestureName.POSSIBLE);

                if (e.Result.Text.Equals("GAMER"))
                    gamerIsOn = !gamerIsOn;
                else if (e.Result.Text.Equals("RIGHT") && gamerIsOn == true)
                    raiseEventDelegateGesture(GestureEnum.GestureName.RIGHT);
                else if (e.Result.Text.Equals("LEFT") && gamerIsOn == true)
                    raiseEventDelegateGesture(GestureEnum.GestureName.LEFT);
                else if (e.Result.Text.Equals("DOWN") && gamerIsOn == true)
                    raiseEventDelegateGesture(GestureEnum.GestureName.DOWN);
                else if (e.Result.Text.Equals("UP") && gamerIsOn == true)
                    raiseEventDelegateGesture(GestureEnum.GestureName.UP);
                else if (e.Result.Text.Equals("AGAIN") && gamerIsOn == true)
                    raiseEventDelegateGesture(GestureEnum.GestureName.AGAIN);
            }
        }

        private void SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            MessageBox.Show("Something unrecognized= " + e.Result.Text);
        }

        #endregion
    }
}
