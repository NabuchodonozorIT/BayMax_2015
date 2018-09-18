using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;


namespace DigitalSignageBaymax
{
    class Caruzele
    {
        private List<ContentText> contentCarusele;
        private List<System.Windows.Controls.Label> TextCentralCarusele = new List<System.Windows.Controls.Label>();
        private int RandomTextSwitch;
        private DispatcherTimer timer;
        private int speed = 250;
        private int ListElement, NextTabCarusele = 0;
        private List<string> tempolaryContentCarusele = new List<string>();

        Random rnd = new Random();
        ReadConfig readConfig = new ReadConfig();

        public void GetCopyLable(List<System.Windows.Controls.Label> source)
        {
            TextCentralCarusele = source;
        }

        public void SetCaruzeleContentPath()
        {
            contentCarusele = readConfig.GetCaruseleTextConfig();

            for (int i = 0; i < contentCarusele.Count(); i++)
            {
                TextCentralCarusele[i].Content = contentCarusele[i].Text;
                tempolaryContentCarusele.Add(contentCarusele[i].Text);
                ListElement++;
                if (ListElement >= 15)
                    break;
            }
        }

        public void CaruzeleContent(object sender, EventArgs e)
        {
            NextTabCarusele++;

            if (NextTabCarusele >= ListElement)
                NextTabCarusele = 0;

            RandomTextSwitch = rnd.Next(0, tempolaryContentCarusele.Count());
            TextCentralCarusele[RandomTextSwitch].Content = tempolaryContentCarusele[NextTabCarusele];
        }

        public void InitTimerCarusele()
        {
            if (ListElement > 0)
            {
                timer = new DispatcherTimer();
                timer.Tick += new EventHandler(CaruzeleContent);
                timer.Interval = new TimeSpan(0, 0, 0, 0, speed);
                timer.Start();
            }
        }
    }
}
