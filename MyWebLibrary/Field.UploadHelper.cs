using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebLibrary
{
    /// <summary>
    /// 创建一个新的图片信息
    /// </summary>
    public class ImageInfo
    {
        public string fileName { get; set; }
        public string folderPath { get; set; }
        public string path { get; set; }
        public int? imgWidth { get; set; }
        public int? imgHeight { get; set; }
        public string text { get; set; }
        public string fontFamily { get; set; }
        public int fontSize { get; set; }
        public int opacity { get; set; }
        public int textX { get; set; }
        public int textY { get; set; }
        /// <summary>
        /// 添加水印图片的路径,该路径为根目录下的文件路径加文件名包含后缀
        /// </summary>
        public string waterFilePath { get; set; }

        public ImageInfo()
        {
            this.fontSize = 10;
            this.fontFamily = "微软雅黑";
            this.opacity = 255;
            this.textX = 0;
            this.textY = 0;
        }
    }

    /// <summary>
    /// 用于保存的图片返回信息
    /// </summary>
    public class SaveImageInfo
    {
        /// <summary>
        /// 文件名称包含后缀
        /// </summary>
        public string fileName { get; set; }
        /// <summary>
        /// 完整物理路径,包含文件名称
        /// </summary>
        public string fullPath { get; set; }
        /// <summary>
        /// 是否保存成功
        /// </summary>
        public bool isSave { get; set; }
    }


    public class TextInfo
    {

        public string text { get; set; }
        public int fontSize { get; set; }
        /// <summary>
        /// 水印透明度
        /// </summary>
        public string opacity { get; set; }

    }
}
