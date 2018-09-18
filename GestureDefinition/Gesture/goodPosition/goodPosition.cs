using Microsoft.Kinect;

namespace GestureDefinition.Gesture.goodPosition
{
    class GoodPosition
    {
        public bool goodPosition(Skeleton[] allSkeletons)
        {
            foreach (Skeleton skeleton in allSkeletons)
            {
                if (SkeletonTrackingState.Tracked == skeleton.TrackingState)
                {
                    if ((0.09 > (skeleton.Joints[JointType.ShoulderLeft].Position.Z) - (skeleton.Joints[JointType.ShoulderRight].Position.Z)) &&
                       (-0.09 < (skeleton.Joints[JointType.ShoulderLeft].Position.Z) - (skeleton.Joints[JointType.ShoulderRight].Position.Z)))
                        return true;
                }
            }
            return false;
        }
    }
}

