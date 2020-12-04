using System;
using System.Drawing;
using System.IO;

public partial class Captcha : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ContentType = "text/plain";
        Random random = new Random(DateTime.Now.Millisecond);

        int number = random.Next(1000000);

        Session.Add("Captcha",number.ToString());

        System.Drawing.Bitmap bmpOut = new Bitmap(100, 25);

        Graphics graphics = Graphics.FromImage(bmpOut);

        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

        graphics.FillRectangle(Brushes.Silver, 0, 0, 100, 25);

        graphics.DrawString(number.ToString(), new Font("Chiller", 20), new SolidBrush(Color.Black), 0, 0);

        MemoryStream memorystream = new MemoryStream();

        bmpOut.Save(memorystream, System.Drawing.Imaging.ImageFormat.Png);

        byte[] bmpBytes = memorystream.GetBuffer();

        bmpOut.Dispose();

        memorystream.Close();

        Response.BinaryWrite(bmpBytes);

        Response.End();

    }
}
