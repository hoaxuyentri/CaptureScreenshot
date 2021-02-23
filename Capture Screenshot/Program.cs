using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace Capture_Screenshot
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string filename = "D:\\Temp\\Fuel_Rate_Capture_Screenshot\\Fuel price " + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + ".jpg";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            WebsiteScreenshot("https://www.petrolimex.com.vn/", filename);
        }

        static void WebsiteScreenshot(string url, string file)
        {
            int SizeWidth = 1360;
            int SizeHeight = 800;
            //Create a webbrwoser object
            WebBrowser browser = new WebBrowser();

            //Deactivate scrollbars, unless you want them to
            //appear on your screenshot
            browser.ScrollBarsEnabled = false;

            //Suppress (java-)script error popups
            browser.ScriptErrorsSuppressed = true;

            //Open the given url in webbrowser
            browser.Navigate(new Uri(url));

            //Wait until the page is fully loaded
            //while (browser.Document == null || browser.Document.Body == null)
            //   Application.DoEvents();

            wait(3000); //wait 3 second to load full content

            //Resize the webbrowser object to the same size as the
            //webpage
            Rectangle websiteSize = browser.Document.Body.ScrollRectangle;
            browser.Size = new Size(SizeWidth, SizeHeight);

            //Create a bitmap object with the same dimensions as the website
            Bitmap bmp = new Bitmap(SizeWidth, SizeHeight);

            //Paint the website contents to the bitmap
            browser.DrawToBitmap(bmp,
            new Rectangle(0, 0, SizeWidth, SizeHeight));

            browser.Dispose();

            //Save the bitmap at the given filepath
            bmp.Save(file, ImageFormat.Jpeg);
        }

        static void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }
    }

}
