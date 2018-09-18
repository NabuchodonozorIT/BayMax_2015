using System;
using Microsoft.Kinect;

namespace GestureDefinition.Gesture.ZoomIn
{
    class ZoomIn
    {
        public GestureEnum.GestureName ZoomInGesture(Skeleton[] allSkeletons)
        {
            foreach (Skeleton skeleton in allSkeletons)
            {
                if (SkeletonTrackingState.Tracked == skeleton.TrackingState)
                {
                    if ((skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.Head].Position.Y) &&
                        (skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.Head].Position.Y) &&
                        (skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ShoulderRight].Position.X) &&
                        (skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ShoulderLeft].Position.X) &&
                        (Math.Abs(skeleton.Joints[JointType.HandLeft].Position.Y - skeleton.Joints[JointType.Head].Position.Y)) > 0.1)
                    {
                        if ((skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ElbowRight].Position.X) &&
                           (skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ElbowLeft].Position.X) &&
                           (0.1 > (Math.Abs(skeleton.Joints[JointType.HandLeft].Position.Z) - (skeleton.Joints[JointType.HandRight].Position.Z))))
                        {
                            return GestureEnum.GestureName.MAXYMALIZE;
                        }
                    }
                }
            }
            return GestureEnum.GestureName.NULL;
        }
    }
}
