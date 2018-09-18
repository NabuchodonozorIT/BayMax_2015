using Microsoft.Kinect;

namespace GestureDefinition.Gesture.Stop
{
    class Stop
    {
        public GestureEnum.GestureName StopGesture(Skeleton[] allSkeletons)
        {
            foreach (Skeleton skeleton in allSkeletons)
            {
                if (SkeletonTrackingState.Tracked == skeleton.TrackingState)
                {
                    if (((skeleton.Joints[JointType.HandLeft].Position.Z + 0.52) < skeleton.Joints[JointType.ShoulderCenter].Position.Z) &&
                        ((skeleton.Joints[JointType.HandRight].Position.Z + 0.52) < skeleton.Joints[JointType.ShoulderCenter].Position.Z) &&
                        (skeleton.Joints[JointType.HandLeft].Position.X > skeleton.Joints[JointType.ShoulderLeft].Position.X) &&
                        (skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.ShoulderRight].Position.X))
                        return GestureEnum.GestureName.STOP;
                }
            }
            return GestureEnum.GestureName.NULL;
        }
    }
}
