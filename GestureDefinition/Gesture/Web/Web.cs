using System;
using Microsoft.Kinect;

namespace GestureDefinition.Gesture.Web
{
    class Web
    {
        private int counter = 0;

        public GestureEnum.GestureName WebGesture(Skeleton[] allSkeletons)
        {
            foreach (Skeleton skeleton in allSkeletons)
            {
                if (SkeletonTrackingState.Tracked == skeleton.TrackingState)
                {
                    if ((Math.Abs(skeleton.Joints[JointType.HandLeft].Position.X) + Math.Abs(skeleton.Joints[JointType.HandRight].Position.X) > 1) &&
                        ((skeleton.Joints[JointType.HandLeft].Position.Z) > (skeleton.Joints[JointType.Spine].Position.Z)) &&
                        ((skeleton.Joints[JointType.HandRight].Position.Z) > (skeleton.Joints[JointType.Spine].Position.Z)))
                    {
                        counter++;
                        //MessageBox.Show("" +counter);
                        if (counter == 60)
                        {
                            return GestureEnum.GestureName.WEB;
                        }
                    }
                    else
                    {
                        counter = 0;
                    }
                }
            }
            return GestureEnum.GestureName.NULL;
        }
    }
}
