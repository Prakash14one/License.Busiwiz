using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;


/// <summary>
/// Summary description for csImageResize
/// </summary>
public class csImageResize
{

    public string ImageWidthHeightDecrisce(Stream sourcePath, Int16 MinWidth, Int16 MinHeight, Int16 RquireWidth, Int16 RquireHeight, string targetPathWithFileName, bool elseImageSaveORNot)
    {
        var targatFile = targetPathWithFileName;
        var targetPath = targetPathWithFileName;
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
            if (image.Width >= MinWidth && image.Height >= MinHeight)
            {
                // can given width of image as we want
                var newWidth = (int)RquireWidth;
                // can given height of image as we want
                var newHeight = (int)RquireHeight;
                var thumbnailImg = new System.Drawing.Bitmap(newWidth, newHeight);
                var thumbGraph = System.Drawing.Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath, image.RawFormat);
                ///return "Image Save Successfully";
            }
            else
            {
                if (elseImageSaveORNot == true)
                {
                    image.Save(targetPath, image.RawFormat);
                    return "Image Save Successfully";
                }
                else
                {
                    return "<b>Failed to upload<br/>Reason:-</b>Minimum Width : " + MinWidth + "px and Height : " + MinHeight + "px";
                }
            }
        }
        if (File.Exists(targetPath))
        {
           return "Image Save Successfully";
        }
        else
        {
            return "Image not Save Successfully";
        }
    }




    public bool ThumbnailCallback()
    {
        return true;
    }

    // This function is for reducing the size when the dimensions are specified 
    public static void GenerateThumbnailsWithheight(int reqHeight, int reqWidth, Stream sourcePath, string targetPathWithFileName)
    {
        var targetPath = targetPathWithFileName;
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
            // can given width of image as we want
            var newWidth = (int)reqWidth;
            // can given height of image as we want
            var newHeight = (int)reqHeight;
            var thumbnailImg = new System.Drawing.Bitmap(newWidth, newHeight);
            var thumbGraph = System.Drawing.Graphics.FromImage(thumbnailImg);
            thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imageRectangle);
            thumbnailImg.Save(targetPath, image.RawFormat);
        }
    }
    public string GenerateThumbnails(double scaleFactor, Stream sourcePath, string targetPathWithFileName)
    {
        var targetPath = targetPathWithFileName;
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
            // can given width of image as we want
            var newWidth = (int)(image.Width * scaleFactor);
            //var newWidth = (int)(image.Width * scaleFactor);
            // can given height of image as we want
            var newHeight = (int)(image.Height * scaleFactor);
            //var newHeight = (int)(image.Height * scaleFactor);
            var thumbnailImg = new System.Drawing.Bitmap(newWidth, newHeight);
            var thumbGraph = System.Drawing.Graphics.FromImage(thumbnailImg);
            thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imageRectangle);
            thumbnailImg.Save(targetPath, image.RawFormat);
            if (File.Exists(targetPath))
            {
                return "Image Save Successfully";
            }
            else
            {
                return "Image not Save Successfully";
            }
        }
    }

    public void AutoImage(string strCompanyName)
    {
        Int16 swidth = Convert.ToInt16(strCompanyName.Length * 13);
        System.Drawing.Bitmap objBMP = new System.Drawing.Bitmap(swidth, 30);
        System.Drawing.Graphics objGraphics = System.Drawing.Graphics.FromImage(objBMP);
        objGraphics.Clear(System.Drawing.Color.White);
        objGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        //' Configure font to use for text
        System.Drawing.Font objFont = new System.Drawing.Font("Arial", 15, System.Drawing.FontStyle.Bold);

        string randomStr = strCompanyName;
        int[] myIntArray = new int[6];
        int x;
        //That is to create the random # and add it to our string
        //Random autoRand = new Random();
        //for (x = 0; x < 6; x++)
        //{
        //    myIntArray[x] = System.Convert.ToInt32(autoRand.Next(0, 9));
        //    randomStr += (myIntArray[x].ToString());
        //}
        //This is to add the string to session cookie, to be compared later


        //' Write out the text
        objGraphics.DrawString(strCompanyName, objFont, System.Drawing.Brushes.Black, 3, 3);

        //' Set the content type and return the image
        //Response.ContentType = "image/GIF";
        File.Delete(HttpContext.Current.Server.MapPath("~\\provider\\LogoImages\\Default.jpg"));
        objBMP.Save(HttpContext.Current.Server.MapPath("~\\provider\\LogoImages\\Default.jpg"), objBMP.RawFormat);
        objFont.Dispose();
        objGraphics.Dispose();
        objBMP.Dispose();
    }
    public csImageResize()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    // This function is for reducing the size when the dimensions are specified 
    public static void GenerateThumbnails(int reqHeight, int reqWidth, Stream sourcePath, string targetPathWithFileName)
    {
        var targetPath = targetPathWithFileName;
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
            // can given width of image as we want
            var newWidth = (int)reqWidth;
            // can given height of image as we want
            var newHeight = (int)reqHeight;
            var thumbnailImg = new System.Drawing.Bitmap(newWidth, newHeight);
            var thumbGraph = System.Drawing.Graphics.FromImage(thumbnailImg);
            thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imageRectangle);
            thumbnailImg.Save(targetPath, image.RawFormat);
           
        }
    }

    public string ImageWidthHeightDecrisceImageOnly(Stream sourcePath, Int16 MinWidth, Int16 MinHeight, Int16 RquireWidth, Int16 RquireHeight, string targetPathWithFileName, bool elseImageSaveORNot)
    {
        var targatFile = targetPathWithFileName;
        var targetPath = targetPathWithFileName;
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
            if (image.Width >= RquireWidth && image.Height >= RquireHeight)
            {
                // can given width of image as we want
                var newWidth = (int)RquireWidth;
                // can given height of image as we want
                var newHeight = (int)RquireHeight;
                var thumbnailImg = new System.Drawing.Bitmap(newWidth, newHeight);
                var thumbGraph = System.Drawing.Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath, image.RawFormat);
                ///return "Image Save Successfully";
            }
            else
            {
                if (image.Width >= MinWidth && image.Height >= MinHeight)
                {
                    GenerateThumbnails(0.5, sourcePath, targetPathWithFileName);
                }
                else if (elseImageSaveORNot == true)
                {
                    image.Save(targetPath, image.RawFormat);
                    return "Image Save Successfully";
                }
                else
                {
                    return "<b>Failed to upload<br/>Reason:-</b>Minimum Width : " + MinWidth + "px and Height : " + MinHeight + "px";
                }

            }
        }
        if (File.Exists(targetPath))
        {
            return "Image Save Successfully";
        }
        else
        {
            return "Image not Save Successfully";
        }
    }

    public string ImageSizeDescrisOnly(Stream sourcePath, string targetPathWithFileName)
    {
        var targatFile = targetPathWithFileName;
        var targetPath = targetPathWithFileName;
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
                // can given width of image as we want
                var newWidth = (int)image.Width;
                // can given height of image as we want
                var newHeight = (int)image.Height;
                var thumbnailImg = new System.Drawing.Bitmap(newWidth, newHeight);
                var thumbGraph = System.Drawing.Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath, image.RawFormat);
                ///return "Image Save Successfully";
                
        }
        if (File.Exists(targetPath))
        {
            return "Image Save Successfully";
        }
        else
        {
            return "Image not Save Successfully";
        }
    }
}