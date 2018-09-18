using Microsoft.Kinect;

namespace GestureDefinition.Gesture.Play
{
    class Play
    {
        public GestureEnum.GestureName PlayGesture(Skeleton[] allSkeletons)
        {
            foreach (Skeleton skeleton in allSkeletons)
            {
                if (SkeletonTrackingState.Tracked == skeleton.TrackingState)
                {
                    if (((skeleton.Joints[JointType.HandLeft].Position.Z - skeleton.Joints[JointType.HandRight].Position.Z) > 0.45) &&
                        ((skeleton.Joints[JointType.HandRight].Position.Z + 0.40) < skeleton.Joints[JointType.ShoulderCenter].Position.Z) &&
                        (skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.HipCenter].Position.Y))
                        return GestureEnum.GestureName.PLAY;
                }
            }
            return GestureEnum.GestureName.NULL;
        }
    }
}

