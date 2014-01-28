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
using System.Threading;

namespace OpenCVTest
{
    public partial class Form1 : Form//, ICvProcessorForm
    {
        private CvProcessor cvProcessor = null;
        public Form1()
        {
            InitializeComponent();
            cvProcessor = new CvProcessor();

            cvProcessor.CaptureStateChanged += new CvProcessor.CaptureStateChangedEventHandler(cvProcessor_CaptureStateChanged);
            cvProcessor.Verbose += new CvProcessor.CaptureVerboseEventHandler(cvProcessor_CaptureVerbose);
            cvProcessor.ImageProcessed += new CvProcessor.ImageProcessedHandler(cvProcessor_ImageProcessed);
            cvProcessor.LEDStatusChanged+=new CvProcessor.LEDStatusChangedEventHandler(cvProcessor_LEDStatusChanged);


            this.ThresholdSpinBox.DataBindings.Add("Value", cvProcessor.Parameters, "ThresholdVal", false, DataSourceUpdateMode.OnPropertyChanged);
            this.MaxThresholdSpinBox.DataBindings.Add("Value", cvProcessor.Parameters, "MaxThresholdVal", false, DataSourceUpdateMode.OnPropertyChanged);
            this.SmoothBlurSpinBox.DataBindings.Add("Value", cvProcessor.Parameters, "SmoothBlur", false, DataSourceUpdateMode.OnPropertyChanged);

        }

        private void cvProcessor_CaptureStateChanged(bool CaptureState)
        {
            this.StratButton.Text = (CaptureState == true) ? "Start Capture" : "Stop";
        }

        private void cvProcessor_LEDStatusChanged(bool CurrentLEDStatus)
        {            
            LEDStatusLabel.Text = CurrentLEDStatus==true?"LED ON":"LED OFF";
        }

        private void cvProcessor_CaptureVerbose(string message)
        {
            string result = string.Format("{0:HH:mm:ss.fff}>>{1}{2}", DateTime.Now, message, Environment.NewLine);

            this.LoggerTextBox.AppendText(result);

        }

        private void cvProcessor_ImageProcessed(Image<Bgr, Byte> frame, Image<Gray, Byte> smooth, Image<Gray, Byte> threshold, Image<Gray, Byte> canny)
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

    }

    class CvParameters : INotifyPropertyChanged
    {
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

                NotifyPropertyChanged("SmoothBlur");
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
                }

                NotifyPropertyChanged("MaxThresholdVal");
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
                }

                NotifyPropertyChanged("ThresholdVal");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    class CvProcessor
    {
        private readonly SynchronizationContext context = SynchronizationContext.Current;

        private Capture capture;
        private bool inProgress;

        private CvParameters parameters = new CvParameters();

        internal CvParameters Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        public delegate void LEDStatusChangedEventHandler(bool LEDStatus);
        public event LEDStatusChangedEventHandler LEDStatusChanged;

        public delegate void CaptureStateChangedEventHandler(bool CaptureState);
        public event CaptureStateChangedEventHandler CaptureStateChanged;

        public delegate void CaptureVerboseEventHandler(string message);
        public event CaptureVerboseEventHandler Verbose;

        public delegate void ImageProcessedHandler(Image<Bgr, Byte> frame, Image<Gray, Byte> smooth, Image<Gray, Byte> threshold, Image<Gray, Byte> canny);
        public event ImageProcessedHandler ImageProcessed;

        private void OnCaptureStateChanged(bool state)
        {
            if (CaptureStateChanged != null)
            {
                if (context != null)
                    context.Post((o) => { CaptureStateChanged(state); }, null);
                else
                    CaptureStateChanged(state);
            }
        }

        private void OnVerbose(string message)
        {
            if (Verbose != null)
            {
                if (context != null)
                    context.Post((o) => { Verbose(message); }, null);
                else
                    Verbose(message);
            }
        }

        private void OnLEDStatusChanged(bool status)
        {
            if (Verbose != null)
            {
                if (context != null)
                    context.Post((o) => { LEDStatusChanged(status); }, null);
                else
                    LEDStatusChanged(status);
            }
        }

        private void OnImageProcessed(Image<Bgr, Byte> frame, Image<Gray, Byte> smooth, Image<Gray, Byte> threshold, Image<Gray, Byte> canny)
        {
            if (ImageProcessed != null)
            {
                //if (context != null)
                //    context.Post((o) => { ImageProcessed(frame, smooth, threshold, canny); }, null);
                //else
                    ImageProcessed(frame, smooth, threshold, canny);
            }
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
                    Application.Idle -= ProcessFrame;
                }
                else
                {

                    Application.Idle += ProcessFrame;
                }

                OnCaptureStateChanged(inProgress);
                inProgress = !inProgress;
            }
        }

        private bool prevLEDStatus = false;
        private bool currLEDStatus = false;

        private void ProcessFrame(object sender, EventArgs arg)
        {
            Image<Bgr, Byte> frame = capture.QueryFrame();//RetrieveBgrFrame();

            if (frame == null) return;

            //Image<Gray, Byte> smallGrayFrame = grayFrame.PyrDown();
            //Image<Gray, Byte> smoothedGrayFrame = smallGrayFrame.PyrUp();
            //Image<Gray, Byte> cannyFrame = smoothedGrayFrame.Canny(new Gray(100), new Gray(60));

            Image<Gray, Byte> smooth = frame.SmoothBlur(parameters.SmoothBlur, parameters.SmoothBlur).Convert<Gray, Byte>();

            //Image<Gray, Byte> smoothImage = frame.Convert<Gray, Byte>();

            //smoothImage._EqualizeHist();
            //smoothImage._GammaCorrect(10.0d);

            Image<Gray, Byte> threshold = smooth.ThresholdBinary(new Gray((int)parameters.ThresholdVal), new Gray((int)parameters.MaxThresholdVal));

            Image<Gray, Byte> canny = threshold.Canny(new Gray(255), new Gray(255));

            Contour<Point> largestContour = null;
            double largestArea = 0;
            currLEDStatus = false;

            for (Contour<System.Drawing.Point> contours = canny.FindContours(
                      Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
                      Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_EXTERNAL); contours != null; contours = contours.HNext)
            {
                Point pt = new Point(contours.BoundingRectangle.X, contours.BoundingRectangle.Y);
                //draw.Draw(new CircleF(pt, 5), new Gray(255), 0);

                if (contours.Area > largestArea)
                {
                    largestArea = contours.Area;
                    largestContour = contours;
                }

                canny.Draw(contours.BoundingRectangle, new Gray(255), 1);
                //frame.Draw(contours.BoundingRectangle, new Bgr(Color.Red), 3);

                if (largestContour != null)
                {
                    frame.Draw(largestContour.BoundingRectangle, new Bgr(Color.Red), 2);

                    currLEDStatus = true;

                    string message = string.Format("{0}, Area={1}", "LargestContour", largestContour.Area);

                    OnVerbose(message);

                }
            }

            if (currLEDStatus != prevLEDStatus)
            {
                string message = String.Format("LED Status = {0}", currLEDStatus ? "ON" : "OFF");
                OnVerbose(message);

                OnLEDStatusChanged(currLEDStatus);
            }

            prevLEDStatus = currLEDStatus;

            OnImageProcessed(frame, smooth, threshold, canny);

            //form.DrawFrame(frame);

        }

        private void ReleaseData()
        {
            if (capture != null)
                capture.Dispose();
        }


    }
}
