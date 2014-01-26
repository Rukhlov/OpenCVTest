using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.VideoSurveillance;
using Emgu.Util;

namespace OpenCVTest
{
    public partial class Form1 : Form, ICvProcessorForm
    {
        private CvProcessor cvProcessor = null;
        public Form1()
        {
            InitializeComponent();
            cvProcessor = new CvProcessor(this);
            cvProcessor.Init();
        }

        public void SetCaptureText(string value)
        {
            this.StratButton.Text = value;
        }
        public void SetThreshold(int value) 
        { 
            this.ThresholdSpinBox.Value = (int)cvProcessor.ThresholdVal;
        }
        public void SetMaxThreshold(int value) 
        {
            this.MaxThresholdSpinBox.Value = (int)cvProcessor.MaxThresholdVal;
        }

        public void SetSmoothBlur(int value)
        {
            this.SmoothBlurSpinBox.Value = (int)cvProcessor.SmoothBlur;
        }

        public void Echo(string message, bool title = false)
        {
            string result=string.Format("{0:HH:mm:ss.fff}>>{1}{2}",DateTime.Now, message, Environment.NewLine); 
            if (this.InvokeRequired == true)
            {
                this.Invoke((Action)(() => 
                {
                    if (title == false)
                        this.LoggerTextBox.AppendText(result);
                    else
                        this.Text = message; 
                }));
            }
            else
            {
                if (title == false)
                    this.LoggerTextBox.AppendText(result);
                else
                    this.Text = message; 
            }
        }

        public void Draw(Image<Bgr, Byte> frame, Image<Gray, Byte> smooth, Image<Gray, Byte> threshold, Image<Gray, Byte> canny)
        {
            this.OriginalImageBox.Image = frame;
            this.SmoothImageBox.Image = smooth;
            this.ThresholdImageBox.Image = threshold;
            this.CannyImageBox.Image = canny;
        }



        private void StratButton_Click(object sender, EventArgs e)
        {
            cvProcessor.Start();
        }

        private void MaxThresholdSpinBox_ValueChanged(object sender, EventArgs e)
        {
            cvProcessor.MaxThresholdVal= (double)MaxThresholdSpinBox.Value;
        }

        private void ThresholdSpinBox_ValueChanged(object sender, EventArgs e)
        {
            cvProcessor.ThresholdVal= (double)ThresholdSpinBox.Value;
        }

        private void SmoothBlurSpinBox_ValueChanged(object sender, EventArgs e)
        {
            cvProcessor.SmoothBlur = (int)SmoothBlurSpinBox.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (_capture != null)
            //{
            //    //double property = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_AUTO_EXPOSURE);

            //    _capture.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_BRIGHTNESS, 1.0);

            //    //this.Text = String.Format("CV_CAP_PROP_BRIGHTNESS={0}", property);
            //}
        }



    }

    class CvProcessor
    {
        private Capture capture;
        private bool inProgress;

        private double maxThresholdVal = 255;
        private double thresholdVal = 254;

        private int smoothBlur = 3;

        public int SmoothBlur
        {
            get { return smoothBlur; }
            set 
            {
                if (value > 0 && value < 50)
                {
                    smoothBlur = value;
                }

                if (form != null)
                    form.SetSmoothBlur(smoothBlur);
            }
        }

        public double MaxThresholdVal
        {
            get { return maxThresholdVal; }
            set 
            {
                if (value > ThresholdVal)
                {
                    maxThresholdVal = value;
                }
                else
                {
                    maxThresholdVal = ThresholdVal;

                    if(form!=null)
                        form.SetMaxThreshold((int)MaxThresholdVal);
                }
            }
        }


        public double ThresholdVal
        {
            get { return thresholdVal; }
            set 
            {
                if (value < MaxThresholdVal)
                {
                    thresholdVal = value;
                }
                else
                {
                    thresholdVal = MaxThresholdVal;
                    if(form!=null)
                        form.SetThreshold((int)ThresholdVal);
                }
            }
        }

        private ICvProcessorForm form = null;

        public CvProcessor(ICvProcessorForm _form)
        {
            form = _form;

        }

        public void Init()
        {
            form.SetCaptureText("Start Capture");
            form.SetMaxThreshold((int)MaxThresholdVal);
            form.SetThreshold((int)ThresholdVal);
            form.SetSmoothBlur(SmoothBlur);
        }

        public void Start()
        {
            if (capture == null)
            {
                try
                {
                    //capture = new Capture("test.wmv");
                    capture = new Capture();
                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }

            if (capture != null)
            {
                if (inProgress == true)
                {
                    form.SetCaptureText("Start Capture");
                    Application.Idle -= ProcessFrame;
                }
                else
                {
                    form.SetCaptureText("Stop");
                    Application.Idle += ProcessFrame;
                }

                inProgress = !inProgress;
            }
        }

        private bool prevLEDStatus= false;
        private bool currLEDStatus = false;

        private void ProcessFrame(object sender, EventArgs arg)
        {
            Image<Bgr, Byte> frame = capture.QueryFrame();//RetrieveBgrFrame();

            if (frame == null) return;

            //Image<Gray, Byte> smallGrayFrame = grayFrame.PyrDown();
            //Image<Gray, Byte> smoothedGrayFrame = smallGrayFrame.PyrUp();
            //Image<Gray, Byte> cannyFrame = smoothedGrayFrame.Canny(new Gray(100), new Gray(60));

            Image<Gray, Byte> smooth = frame.SmoothBlur(SmoothBlur, SmoothBlur).Convert<Gray, Byte>();

            //Image<Gray, Byte> smoothImage = frame.Convert<Gray, Byte>();

            //smoothImage._EqualizeHist();
            //smoothImage._GammaCorrect(10.0d);

            Image<Gray, Byte> threshold = smooth.ThresholdBinary(new Gray((int)thresholdVal), new Gray((int)maxThresholdVal));

            Image<Gray, Byte> canny = threshold.Canny(new Gray(255), new Gray(255));

            Contour<Point> largestContour = null;
            double largestarea = 0;
            currLEDStatus = false;

            for (Contour<System.Drawing.Point> contours = canny.FindContours(
                      Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
                      Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_EXTERNAL); contours != null; contours = contours.HNext)
            {
                Point pt = new Point(contours.BoundingRectangle.X, contours.BoundingRectangle.Y);
                //draw.Draw(new CircleF(pt, 5), new Gray(255), 0);

                if (contours.Area > largestarea)
                {
                    largestarea = contours.Area;
                    largestContour = contours;
                }

                canny.Draw(contours.BoundingRectangle, new Gray(255), 1);
                //frame.Draw(contours.BoundingRectangle, new Bgr(Color.Red), 3);

                if (largestContour != null)
                {
                    frame.Draw(largestContour.BoundingRectangle, new Bgr(Color.Red), 2);

                    currLEDStatus = true;

                    string message = string.Format("{0}, Area={1}", "LargestContour", largestContour.Area);
                    form.Echo(message);

                }
            }

            if (currLEDStatus != prevLEDStatus)
            {
                string message = String.Format("LED Status = {0}", currLEDStatus ? "ON" : "OFF"); ;
                form.Echo(message, true);
            }

            prevLEDStatus = currLEDStatus;

            form.Draw(frame, smooth, threshold, canny);

            //form.DrawFrame(frame);

        }

        private void ReleaseData()
        {
            if (capture != null)
                capture.Dispose();
        }


    }

    interface ICvProcessorForm
    {
        void SetCaptureText(string value);
        void SetThreshold(int value);
        void SetMaxThreshold(int value);
        void SetSmoothBlur(int value);

        void Echo(string message, bool title=false);
        //void DrawFrame(Image<Bgr, Byte> frame);
        void Draw(Image<Bgr, Byte> frame, Image<Gray, Byte> smooth, Image<Gray, Byte> threshold, Image<Gray, Byte> canny);

    }

}
