using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyWebLibrary
{
    public class UploadHelper
    {
        public static SaveImageInfo CreateNewImage(HttpPostedFile file, ImageInfo imageInfo)
        {
            SaveImageInfo saveImageInfo = new SaveImageInfo();
            if (IsImageType(file))
            {
                string extName = Path.GetExtension(file.FileName);
                string fileName = imageInfo.fileName == null ? Guid.NewGuid().ToString() : imageInfo.fileName;
                string folderPath = imageInfo.folderPath == null ? "" : imageInfo.folderPath + "/";
                using (System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream))
                {
                    int oldWidth = img.Width;
                    int oldHeight = img.Height;
                    int imgWidth = imageInfo.imgWidth == null ? oldWidth : Convert.ToInt16(imageInfo.imgWidth);
                    int imgHeight = imageInfo.imgHeight == null ? oldHeight : Convert.ToInt16(imageInfo.imgHeight);


                    using (Bitmap bmp = new Bitmap(imgWidth, imgHeight))
                    {
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.Clear(Color.Transparent);
                            g.InterpolationMode = InterpolationMode.High;
                            g.CompositingQuality = CompositingQuality.HighQuality;
                            g.SmoothingMode = SmoothingMode.HighQuality;
                            g.DrawImage(img, new Rectangle(0, 0, imgWidth, imgHeight), new Rectangle(0, 0, oldWidth, oldHeight), GraphicsUnit.Pixel);

                            //添加文字模块----------------------------
                            if (imageInfo.text != null)
                            {
                                Font font = new Font(imageInfo.fontFamily, imageInfo.fontSize);
                                SolidBrush sbrush = new SolidBrush(Color.FromArgb(imageInfo.opacity, 255, 255, 255)); //Color.White
                                g.DrawString(imageInfo.text, font, sbrush, imageInfo.textX, imageInfo.textY);
                            }
                            //添加文字模块----------------------------

                            //添加水印--------------------------------
                            if (imageInfo.waterFilePath != null)
                            {
                                System.Drawing.Image water = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath("~/" + imageInfo.waterFilePath));
                                g.DrawImage(water, new Rectangle(0, 0, water.Width, water.Height), 0, 0, water.Width, water.Height, GraphicsUnit.Pixel);
                                water.Dispose();
                            }
                            //添加水印---------------------------------

                            string newFileName = Path.GetFileNameWithoutExtension(fileName) + extName;
                            saveImageInfo.fileName = newFileName;
                            saveImageInfo.fullPath = imageInfo.path == null ? HttpContext.Current.Server.MapPath("~/" + folderPath) + newFileName : imageInfo.path;
                            bmp.Save(saveImageInfo.fullPath, ImageFormat.Jpeg);
                            saveImageInfo.isSave = true;
                            return saveImageInfo;
                        }
                    }
                }
            }
            else
            {
                saveImageInfo.isSave = false;
                return saveImageInfo;
            }
        }

        /// <summary>
        /// 系统会自动生成guid文件名,保存到当前根目录下,如果非图片类型则返回false,当前紧支持jpg、png、gif格式
        /// </summary>
        /// <param name="file">一般为post的参数, ashx文件示例: context.Request.Files[0]</param>
        /// <param name="folderName">存放的目录名称,比如image文件夹,如果存在多级目录则 image/product,前后无需"/"斜杠</param>
        /// <returns>MyWebLibrary SaveImageInfo对象</returns>
        public static SaveImageInfo SaveNewImage(HttpPostedFile file)
        {
            SaveImageInfo img = new SaveImageInfo();
            if (IsImageType(file))
            {
                string extName = Path.GetExtension(file.FileName);
                string fileName = Guid.NewGuid().ToString();
                img.fileName = fileName + extName;
                img.fullPath = HttpContext.Current.Server.MapPath("~/" + img.fileName);
                file.SaveAs(img.fullPath);
                img.isSave = true;
                return img;
            }
            else
            {
                img.isSave = false;
                return img;
            }
        }

        /// <summary>
        /// 系统会自动生成guid文件名,如果非图片类型则返回false,当前紧支持jpg、png、gif格式
        /// </summary>
        /// <param name="file">一般为post的参数, ashx文件示例: context.Request.Files[0]</param>
        /// <param name="folderName">存放的目录名称,比如image文件夹,如果存在多级目录则 image/product,前后无需"/"斜杠</param>
        /// <returns>MyWebLibrary SaveImageInfo对象</returns>
        public static SaveImageInfo SaveNewImage(HttpPostedFile file, string folderName)
        {
            SaveImageInfo img = new SaveImageInfo();
            if (IsImageType(file))
            {
                string extName = Path.GetExtension(file.FileName);
                string fileName = Guid.NewGuid().ToString();
                img.fileName = fileName + extName;
                img.fullPath = HttpContext.Current.Server.MapPath("~/" + folderName + "/") + img.fileName;
                file.SaveAs(img.fullPath);
                img.isSave = true;
                return img;
            }
            else
            {
                img.isSave = false;
                return img;
            }
        }

        /// <summary>
        /// 此方法一般用来生成缩略图 系统会自动生成guid文件名,如果非图片类型则返回false,当前紧支持jpg、png、gif格式
        /// </summary>
        /// <param name="file">一般为post的参数, ashx文件示例: context.Request.Files[0]</param>
        /// <param name="folderName">存放的目录名称,比如image文件夹,如果存在多级目录则 image/product,前后无需"/"斜杠</param>
        /// <param name="imgWidth">图片宽度</param>
        /// <param name="imgHeight">图片高度</param>
        /// <returns>MyWebLibrary SaveImageInfo对象</returns>
        public static SaveImageInfo SaveNewImage(HttpPostedFile file, string folderName, int imgWidth, int imgHeight)
        {
            SaveImageInfo imageInfo = new SaveImageInfo();
            if (IsImageType(file))
            {
                string extName = Path.GetExtension(file.FileName);
                string fileName = Guid.NewGuid().ToString();
                using (System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream))
                {
                    int oldWidth = img.Width;
                    int oldHeight = img.Height;
                    using (Bitmap bmp = new Bitmap(imgWidth, imgHeight))
                    {
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.Clear(Color.Transparent);
                            g.InterpolationMode = InterpolationMode.High;
                            g.CompositingQuality = CompositingQuality.HighQuality;
                            g.SmoothingMode = SmoothingMode.HighQuality;
                            g.DrawImage(img, new Rectangle(0, 0, imgWidth, imgHeight), new Rectangle(0, 0, oldWidth, oldHeight), GraphicsUnit.Pixel);

                            string newFileName = Path.GetFileNameWithoutExtension(fileName) + extName;
                            imageInfo.fileName = newFileName;
                            imageInfo.fullPath = HttpContext.Current.Server.MapPath("~/" + folderName + "/") + newFileName;
                            bmp.Save(imageInfo.fullPath, ImageFormat.Jpeg);
                        }
                    }
                }
                imageInfo.isSave = true;
                return imageInfo;
            }
            else
            {
                imageInfo.isSave = false;
                return imageInfo;
            }
        }

        //public static SaveImageInfo SaveNewImage(HttpPostedFile file, ImageInfo imageInfo)
        //{
        //    // && imageInfo.fileName==null && imageInfo.folderName==null && imageInfo.path==null && imageInfo.text == null && imageInfo.opacity== null && imageInfo.fontSize== null
        //    if (imageInfo.imgHeight==null && imageInfo.imgWidth == null)
        //    {

        //    }
        //    else
        //    {

        //    }
        //}

        /// <summary>
        /// 如果非图片类型则返回false,当前紧支持jpg、png、gif格式
        /// </summary>
        /// <param name = "file" > 一般为post的参数, ashx文件示例: context.Request.Files[0]</param>
        /// <param name = "fileName" >文件名称,不需要加文件格式后缀,如.jpg</param>
        /// <param name="folderName">存放的目录名称,比如image文件夹,如果存在多级目录则 image/product,前后无需"/"斜杠</param>
        /// <returns></returns>
        public static bool SaveImage(HttpPostedFile file, string folderName, string fileName)
        {
            if (IsImageType(file))
            {
                string extName = Path.GetExtension(file.FileName);
                string phyFilePath = HttpContext.Current.Server.MapPath("~/" + folderName + "/") + fileName + extName;
                file.SaveAs(phyFilePath);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 重载1 如果非图片类型则返回false,当前紧支持jpg、png、gif格式
        /// </summary>
        /// <param name = "file" > 一般为post的参数, ashx文件示例: context.Request.Files[0]</param>
        /// <param name="path">目录路径+文件名称+文件后缀的完整字符串,示例:image/product/image1.png,字符串开头不能包含"/"左斜杠</param>
        public static bool SaveImage(HttpPostedFile file, string path)
        {
            if (IsImageType(file))
            {
                string phyFilePath = HttpContext.Current.Server.MapPath("~/" + path);
                file.SaveAs(phyFilePath);
                return true;
            }
            else
            {
                return false;
            }
        }



        public static bool IsImageType(HttpPostedFile file)
        {
            string fileName = file.FileName.ToLower();
            bool isAllowType = file.ContentType == "image/x-png" || file.ContentType == "image/gif" || file.ContentType == "image/pjpeg" || file.ContentType == "image/jpeg" || file.ContentType == "image/png";
            bool isRealImageType = !fileName.Contains(".dll") && !fileName.Contains(".txt") && !fileName.Contains(".html") && !fileName.Contains(".php") && !fileName.Contains(".exe") && !fileName.Contains(".ashx") && !fileName.Contains(".aspx") && !fileName.Contains(".jsp"); //不包含这些类型则为true
            if (file.ContentLength > 0 && isAllowType && isRealImageType)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


}
