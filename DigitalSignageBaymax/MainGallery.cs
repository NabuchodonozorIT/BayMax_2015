using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WpfScreenHelper;

namespace DigitalSignageBaymax
{
    public class MainGallery
    {
        private List<string> IMAGES = new List<string>();
        private int TakeNextJPG, countImagesConfig = 0;
        private double IMAGE_WIDTH = Screen.PrimaryScreen.Bounds.Width / 10;
        private double IMAGE_HEIGHT = Screen.PrimaryScreen.Bounds.Height / 10;
        private static double SPRINESS = 0.4;
        private static double DECAY = 0.5;
        private static double SCALE_DOWN_FACTOR = 0.5;
        private static double OFFSET_FACTOR = 400;
        private static double OPACITY_DOWN_FACTOR = 0.4;
        private static double MAX_SCALE = 2;
        private double xCenter;
        private bool switchIA = false;
        private double yCenter;
        private double target, current, spring = 0;
        private List<System.Windows.Controls.Image> images = new List<System.Windows.Controls.Image>();
        private static int FPS = 24;
        private DispatcherTimer timerGallery = new DispatcherTimer();
        public DispatcherTimer timer;
        private int counterNextGallery, counterPrevGallery, counterNext, counterPrev, counterJpg = 0;
        private System.Windows.Controls.Label Next, Prev, NextGallery, PrevGallery;
        private Canvas GalleryShow;
        private System.Windows.Controls.Image PresentationJPG;

        public void SetWhiteHeightCanvasGallery(double Width, double Height, System.Windows.Controls.Label NextSource,
            System.Windows.Controls.Label PrevSource, System.Windows.Controls.Label NextGallerySource, System.Windows.Controls.Label PrevGallerySource,
            Canvas GalleryShowSource, System.Windows.Controls.Image PresentationJPGSource)
        {
            xCenter = Width / 2;
            yCenter = Height / 2;
            Next = NextSource;
            Prev = PrevSource;
            NextGallery = NextGallerySource;
            PrevGallery = PrevGallerySource;
            GalleryShow = GalleryShowSource;
            PresentationJPG = PresentationJPGSource;
        }

        public void InitializeImage(int next)
        {
            int takenext = 0;

            foreach (var image in IMAGES)
            {
                if ((TakeNextJPG + next) == takenext)
                {
                    switch (next)
                    {
                        case -1:
                            Next.Content = (++counterNext);
                            Prev.Content = (--counterPrev);
                            break;
                        case 1:
                            Next.Content = (--counterNext);
                            Prev.Content = (++counterPrev);
                            break;
                    }
                    TakeNextJPG += next;
                    PresentationJPG.Source = new BitmapImage(new Uri((image), UriKind.RelativeOrAbsolute));
                    break;
                }
                takenext++;
            }
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < images.Count; i++)
            {
                System.Windows.Controls.Image image = images[i];
                posImage(image, i);
            }
            if (target == images.Count)
                target = 0;
            spring = (target - current) * SPRINESS + spring * DECAY;
            current += spring;
        }

        public void GetImageFiles(string directory, string format, bool value)
        {

            List<string> IMAGESMirror = new List<string>();
            if (Directory.Exists(directory))
            {
                foreach (var file in Directory.GetFiles(directory, string.Format("*.{0}", format)))
                {
                    IMAGESMirror.Add(file);
                }
                IMAGES = null;
                IMAGES = IMAGESMirror;
                if (value == true)
                {
                    counterPrev = 0;
                    counterNext = IMAGESMirror.Count - 1;
                    counterJpg = IMAGESMirror.Count;
                    Next.Content = (counterNext);
                    Prev.Content = (counterPrev);
                    InitializeImage(0);
                }
                else
                {
                    timer.Tick += new EventHandler(GaruseleStarted);
                    for (int i = 0; i < counterPrevGallery; i++)
                    {
                        moveIndex(-1);
                    }
                    counterPrevGallery = 0;
                    counterNextGallery = IMAGESMirror.Count() - 1;
                    counterJpg = IMAGESMirror.Count;
                    addImages();
                    NextGallery.Content = (counterNextGallery);
                    PrevGallery.Content = (counterPrevGallery);
                }
            }
        }

        private void addImages()
        {
            GalleryShow.Children.RemoveRange(0, GalleryShow.Children.Count);
            images.RemoveRange(0, images.Count);
            foreach (string imageSource in IMAGES)
            {
                System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                image.Source = new BitmapImage(new Uri(imageSource, UriKind.Absolute));
                image.Width = IMAGE_WIDTH * 2.2;
                GalleryShow.Children.Add(image);
                posImage(image, countImagesConfig);
                images.Add(image);
                countImagesConfig++;
            }
            countImagesConfig = 0;
        }

        public void moveIndex(int value)
        {
            if (value == -1 && counterPrevGallery > 0)
            {
                NextGallery.Content = (++counterNextGallery);
                PrevGallery.Content = (--counterPrevGallery);
            }
            else if (value == 1 && (counterPrevGallery + 1) < counterJpg)
            {
                NextGallery.Content = (--counterNextGallery);
                PrevGallery.Content = (++counterPrevGallery);
            }
            target += value;
            target = Math.Max(0, target);
            target = Math.Min(images.Count - 1, target);
        }

        private void posImage(System.Windows.Controls.Image image, int index)
        {
            double diffFactor = index - current;
            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = MAX_SCALE - Math.Abs(diffFactor) * SCALE_DOWN_FACTOR;
            scaleTransform.ScaleY = MAX_SCALE - Math.Abs(diffFactor) * SCALE_DOWN_FACTOR;
            image.RenderTransform = scaleTransform;

            double left = xCenter - (IMAGE_WIDTH * scaleTransform.ScaleX) / 2 + diffFactor * OFFSET_FACTOR;
            double top = yCenter - (IMAGE_HEIGHT * scaleTransform.ScaleY) / 2;
            image.Opacity = 1 - Math.Abs(diffFactor) * OPACITY_DOWN_FACTOR;

            image.SetValue(Canvas.LeftProperty, left);
            image.SetValue(Canvas.TopProperty, top);
            image.SetValue(Canvas.ZIndexProperty, (int)Math.Abs(scaleTransform.ScaleX * 100));
        }

        public void Start()
        {
            timerGallery = new DispatcherTimer();
            timerGallery.Interval = new TimeSpan(0, 0, 0, 0, 1000 / FPS);
            timerGallery.Tick += new EventHandler(_timer_Tick);
            timerGallery.Start();
        }

        public void InitTimerCarusele(int speed)
        {
            speed = speed * 1000;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, speed);
            timer.Start();
        }

        public void GaruseleStarted(object sender, EventArgs e)
        {
            if (counterNextGallery > 0 && switchIA == false)
            {
                moveIndex(1);
            }
            else
            {
                if (counterPrevGallery == 0)
                {
                    switchIA = false;
                }
                else
                {
                    switchIA = true;
                    moveIndex(-1);
                }
            }
        }

    }
}
