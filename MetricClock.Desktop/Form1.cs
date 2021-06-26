using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetricClock.Desktop
{
    public partial class Form1 : Form
    {
        private const float secRatio = 25.0F / 18.0F;


        Timer myTimer;
        public Form1()
        {
            InitializeComponent();
            myTimer = new Timer();
            myTimer.Tick += myTimer_Tick;
        }

        private void myTimer_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            var stringTime = GetMetricTime(now);
            label1.Text = stringTime + "\r\n\r\n" + now.ToString("h:mm:ss tt");
        }

        protected override void OnLoad(EventArgs e)
        {
            myTimer.Start();
        }



        private static string GetMetricTime(DateTime now)
        {
            var midnight = new DateTime(now.Year, now.Month, now.Day);
            var start = new DateTime(now.Year, now.Month, now.Day, 6, 0, 0);
            float metricSecsSinceStart;
            if (now >= midnight && now < start)
            {
                //this means we are between 10 and 1 (10,11,12,1)
                metricSecsSinceStart = 90000 + (float)(now.TimeOfDay.TotalSeconds * secRatio);
            }
            else
            {
                var standardSecs = now.TimeOfDay.TotalSeconds - 21600;// start.TimeOfDay.TotalSeconds;
                metricSecsSinceStart = (float)(standardSecs * secRatio);
            }
            var currentMetricHours = Math.Floor(metricSecsSinceStart / 10000);

            var currentMetricMinutes = Math.Floor((metricSecsSinceStart - (currentMetricHours * 10000)) / 100);
            var currentMetricSeconds = Math.Floor(metricSecsSinceStart - (currentMetricHours * 10000) - (currentMetricMinutes * 100));
            if (currentMetricHours == 12)
                currentMetricHours = 0;
            var stringTime = String.Format("{0}:{1}:{2} MT", (1 + currentMetricHours).ToString(), ((int)currentMetricMinutes).ToString("D2"), ((int)currentMetricSeconds).ToString("D2"));

            return stringTime;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myTimer.Start();
        }

    }
}
