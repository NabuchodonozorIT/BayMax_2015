using System;
using Microsoft.Kinect;

namespace GestureDefinition.Gesture.Volume
{
    class Volume
    {
        double volumeUp = 0;
        public double volume = 0.1;
        bool oFFoN = false;

        public GestureEnum.GestureName VolumeGesture(Skeleton[] allSkeletons)
        {
            foreach (Skeleton skeleton in allSkeletons)
            {
                if (SkeletonTrackingState.Tracked == skeleton.TrackingState)
                {
                    if ((Math.Abs((skeleton.Joints[JointType.HandRight].Position.X) - (skeleton.Joints[JointType.HandLeft].Position.X)) < 0.12) &&
                        (Math.Abs(skeleton.Joints[JointType.HandRight].Position.Z - skeleton.Joints[JointType.HandLeft].Position.Z) < 0.04) &&
                        (Math.Abs(skeleton.Joints[JointType.HandLeft].Position.Z - skeleton.Joints[JointType.HandRight].Position.Z) < 0.04) &&
                        (Math.Abs(skeleton.Joints[JointType.HandLeft].Position.Y - skeleton.Joints[JointType.HandRight].Position.Y) < 0.1) &&
                        (Math.Abs(skeleton.Joints[JointType.HandRight].Position.Y - skeleton.Joints[JointType.HandLeft].Position.Y) < 0.1) &&
                        (Math.Abs(skeleton.Joints[JointType.HandLeft].Position.Z - skeleton.Joints[JointType.ShoulderCenter].Position.Z) < 0.38))
                        oFFoN = true;

                    if (((skeleton.Joints[JointType.HandLeft].Position.Y) < (skeleton.Joints[JointType.ElbowLeft].Position.Y)) ||
                        ((skeleton.Joints[JointType.HandRight].Position.Y) < (skeleton.Joints[JointType.ElbowRight].Position.Y)) ||
                        ((skeleton.Joints[JointType.HandLeft].Position.Y) > (skeleton.Joints[JointType.Head].Position.Y)) ||
                        ((skeleton.Joints[JointType.HandRight].Position.Y) > (skeleton.Joints[JointType.Head].Position.Y)) ||
                        ((skeleton.Joints[JointType.HandRight].Position.Y) < (skeleton.Joints[JointType.Spine].Position.Y)) ||
                        ((skeleton.Joints[JointType.HandLeft].Position.Y) < (skeleton.Joints[JointType.Spine].Position.Y)))
                        oFFoN = false;

                    if ((oFFoN == true) &&
                        ((skeleton.Joints[JointType.HandLeft].Position.Y) > (skeleton.Joints[JointType.Spine].Position.Y)) &&
                        ((skeleton.Joints[JointType.HandRight].Position.Y) > (skeleton.Joints[JointType.Spine].Position.Y)) &&
                        ((skeleton.Joints[JointType.HandRight].Position.Y) < (skeleton.Joints[JointType.Head].Position.Y)) &&
                        ((skeleton.Joints[JointType.HandLeft].Position.Y) < (skeleton.Joints[JointType.Head].Position.Y)))
                    {
                        volumeUp = (Math.Abs(skeleton.Joints[JointType.HandLeft].Position.X) + Math.Abs(skeleton.Joints[JointType.HandRight].Position.X));
                        if (((volumeUp) > 0.2) && ((volumeUp) < 0.3))
                        {
                            volume = 0.0;
                            return GestureEnum.GestureName.VOLUME;
                        }
                        if (((volumeUp) > 0.3) && ((volumeUp) < 0.4))
                        {
                            volume = 0.1;
                            return GestureEnum.GestureName.VOLUME;
                        }
                        if (((volumeUp) > 0.4) && ((volumeUp) < 0.5))
                        {
                            volume = 0.2;
                            return GestureEnum.GestureName.VOLUME;
                        }
                        if (((volumeUp) > 0.5) && ((volumeUp) < 0.6))
                        {
                            volume = 0.3;
                            return GestureEnum.GestureName.VOLUME;
                        }
                        if (((volumeUp) > 0.6) && ((volumeUp) < 0.7))
                        {
                            volume = 0.4;
                            return GestureEnum.GestureName.VOLUME;
                        }
                        if (((volumeUp) > 0.7) && ((volumeUp) < 0.8))
                        {
                            volume = 0.5;
                            return GestureEnum.GestureName.VOLUME;
                        }
                        if (((volumeUp) > 0.8) && ((volumeUp) < 0.9))
                        {
                            volume = 0.6;
                            return GestureEnum.GestureName.VOLUME;
                        }
                        if (((volumeUp) > 0.9) && ((volumeUp) < 1))
                        {
                            volume = 0.7;
                            return GestureEnum.GestureName.VOLUME;
                        }
                        if (((volumeUp) > 1) && ((volumeUp) < 1.1))
                        {
                            volume = 0.8;
                            return GestureEnum.GestureName.VOLUME;
                        }
                        if (((volumeUp) > 1.1) && ((volumeUp) < 1.2))
                        {
                            volume = 0.9;
                            return GestureEnum.GestureName.VOLUME;
                        }
                        if (((volumeUp) > 1.2))
                        {
                            volume = 1;
                            return GestureEnum.GestureName.VOLUME;
                        }

                    }
                }
            }
            return GestureEnum.GestureName.NULL;
        }
    }
}
