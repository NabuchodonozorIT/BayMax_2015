using Microsoft.Kinect;
using System.Threading.Tasks;

namespace GestureDefinition.Gesture.SwipeLeft
{
    class SwipeLeft
    {
        bool SwipeLeftGestureSequence = false;
        bool SwipeRightGestureSequence = false;

        public GestureEnum.GestureName SwipeLeftGesture(Skeleton[] allSkeletons)
        {
            bool SwipeLeft = false;
            //foreach (Skeleton skeleton in allSkeletons)
            /* Plinq Parallel.ForEach */
            Parallel.ForEach(allSkeletons, skeleton =>
            {
                if (SkeletonTrackingState.Tracked == skeleton.TrackingState)
                {
                    //SWIP PREV witch HandLeft

                    if (((skeleton.Joints[JointType.HandLeft].Position.Z < skeleton.Joints[JointType.Spine].Position.Z)) &&
                                ((skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.Spine].Position.X)) &&
                                ((skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.Spine].Position.Y)) &&
                                ((skeleton.Joints[JointType.HandLeft].Position.X > skeleton.Joints[JointType.ElbowLeft].Position.X)))
                        SwipeLeftGestureSequence = true;

                    if ((skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.Spine].Position.Y))
                        SwipeLeftGestureSequence = false;

                    if (SwipeLeftGestureSequence == true)
                    {
                        if (((skeleton.Joints[JointType.HandLeft].Position.Z < skeleton.Joints[JointType.Spine].Position.Z)) &&
                                ((skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.Spine].Position.X)) &&
                                ((skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.Spine].Position.Y)) &&
                                ((skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ElbowLeft].Position.X)) &&
                                (((skeleton.Joints[JointType.HandLeft].Position.X) - (skeleton.Joints[JointType.ElbowLeft].Position.X))) < -0.25)
                        {
                            SwipeLeftGestureSequence = false;
                            SwipeRightGestureSequence = false;
                            SwipeLeft = true;
                        }
                    }

                    //SWIP PREV witch HandRight

                    if (((skeleton.Joints[JointType.HandRight].Position.Z < skeleton.Joints[JointType.Spine].Position.Z)) &&
                                ((skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.Spine].Position.X)) &&
                                ((skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.Spine].Position.Y)) &&
                                ((skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.ElbowRight].Position.X)))
                        SwipeRightGestureSequence = true;

                    if ((skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.Spine].Position.Y))
                        SwipeRightGestureSequence = false;

                    if (SwipeRightGestureSequence == true)
                    {
                        if (((skeleton.Joints[JointType.HandRight].Position.Z < skeleton.Joints[JointType.Spine].Position.Z)) &&
                                ((skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.Spine].Position.X)) &&
                                ((skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.Spine].Position.Y)) &&
                                ((skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.WristRight].Position.X)))
                        {
                            SwipeLeftGestureSequence = false;
                            SwipeRightGestureSequence = false;
                            SwipeLeft = true;
                        }
                    }
                }
            });
            if (SwipeLeft == true)
            {
                SwipeLeft = false;
                SwipeLeftGestureSequence = false;
                SwipeRightGestureSequence = false;
                return GestureEnum.GestureName.PRIEV;
            }
            else
            {
                return GestureEnum.GestureName.NULL;
            }
        }
    }
}
