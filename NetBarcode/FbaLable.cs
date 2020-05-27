using NetBarcode.Types;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Security.Cryptography;

namespace NetBarcode
{
    public class FbaLable
    {
        private readonly string _data;
        private readonly string _otherData;
        private readonly Type _type = Type.Code128A;
        private string _encodedData;
        private readonly Font _labelFont = new Font("Consolas", 8);
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="barcodeData"></param>
        /// <param name="otherData"></param>
        /// <param name="barcodeType"></param>
        public FbaLable(string barcodeData,string otherData,Type barcodeType)
        {
            _data = barcodeData;
            _otherData = otherData;
            _type = barcodeType;
            InitializeType();
        }
        private void InitializeType()
        {
            IBarcode barcode;

            switch (_type)
            {
                case Type.Code128:
                    barcode = new Code128(_data);
                    break;
                case Type.Code128A:
                    barcode = new Code128(_data, Code128.Code128Type.A);
                    break;
                case Type.Code128B:
                    barcode = new Code128(_data, Code128.Code128Type.B);
                    break;
                case Type.Code128C:
                    barcode = new Code128(_data, Code128.Code128Type.C);
                    break;
                case Type.Code11:
                    barcode = new Code11(_data);
                    break;
                case Type.Code39:
                    barcode = new Code39(_data);
                    break;
                case Type.Code93:
                    barcode = new Code93(_data);
                    break;
                case Type.EAN8:
                    barcode = new EAN8(_data);
                    break;
                case Type.EAN13:
                    barcode = new EAN13(_data);
                    break;
                default:
                    barcode = new Code128(_data);
                    break;
            }
            _encodedData = barcode.GetEncoding();
        }

        /// <summary>
        /// 当前显示器英寸转像素
        /// </summary>
        /// <param name="inches"></param>
        /// <param name="TrueToHeightOrFalseToWidth"></param>
        /// <returns></returns>
        public double InchToPixels(float inches , bool TrueToHeightOrFalseToWidth)
        {
            float pixels;
            float dpiX = 0;
            float dpiY = 0;
            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                dpiX = graphics.DpiX;
                dpiY = graphics.DpiY;
            }
            if (TrueToHeightOrFalseToWidth)
            {
                pixels = inches * dpiY;
            }
            else
            {
                pixels = inches * dpiX;
            }
            return pixels;
        }
        /// <summary>
        /// 毫米转像素
        /// </summary>
        /// <param name="mm"></param>
        /// <param name="TrueToHeightOrFalseToWidth"></param>
        /// <returns></returns>
        public double MmToPixels(float mm,bool TrueToHeightOrFalseToWidth)
        {
            return InchToPixels((mm*10 / 254), TrueToHeightOrFalseToWidth);
        }
        /// <summary>
        /// 获取SKU条码
        /// </summary>
        /// <returns></returns>
        public Image GetItemLable()
        {
            return GenerateItemLableImage();
        }
        private Image GenerateItemLableImage()
        {
            //背景尺寸
            int width = (int)MmToPixels(75,false);
            int height = (int)MmToPixels(50, true);
            //两边空白区
            int blankSides = (int)MmToPixels((float)6.5, false);
            //上下空白区
            int blankTop = (int)MmToPixels((float)3.3, true);

            int iBarWidth = (width-blankSides*2) / _encodedData.Length;
            if (iBarWidth <= 0)
                throw new Exception(
                    "EGENERATE_IMAGE-2: Image size specified not large enough to draw image. (Bar size determined to be less than 1 pixel)");
            
            //条码高度
            int barcodeHeight = height / 3;
            //条码居中的起始X
            int barcodeBeginX = (width - _encodedData.Length - blankSides * 2) / 2;

            Bitmap bitmap = new Bitmap(width,height);
            //draw image
            var pos = 0;
            var halfBarWidth = (int)(iBarWidth * 0.5);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                //clears the image and colors the entire background
                graphics.Clear(Color.White);

                //lines are fBarWidth wide so draw the appropriate color line vertically
                using (var backpen = new Pen(Color.White, iBarWidth))
                {
                    using (var pen = new Pen(Color.Black, iBarWidth))
                    {
                        while (pos < _encodedData.Length)
                        {
                            if (_encodedData[pos] == '1')
                            {
                                graphics.DrawLine(pen,
                                    new Point(pos * iBarWidth + halfBarWidth+blankSides+ barcodeBeginX,  _labelFont.Height+blankTop),
                                    new Point(pos * iBarWidth +  halfBarWidth+blankSides+ barcodeBeginX,
                                        barcodeHeight + blankTop));
                            }
                            pos++;
                        }
                    }
                }
            }

            Image image = (Image)bitmap;

            //写入文字
            try
            {
                using (var g = Graphics.FromImage(image))
                {
                    g.DrawImage(image, 0, 0);

                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                    var stringFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,//文字居中对齐
                        LineAlignment = StringAlignment.Near
                    };
                    int labelY = barcodeHeight + blankTop * 3;
                    //color a background color box at the bottom of the barcode to hold the string of data
                    g.FillRectangle(new SolidBrush(Color.White),
                        new RectangleF(0, labelY, image.Width, image.Height));
                    //draw datastring under the barcode image
                    g.DrawString(_data, _labelFont, new SolidBrush(Color.Black),
                        new RectangleF(0, labelY , image.Width, _labelFont.Height), stringFormat);

                    if (!String.IsNullOrWhiteSpace(_otherData))
                    {
                        stringFormat.Alignment = StringAlignment.Near;
                        stringFormat.Trimming = StringTrimming.EllipsisCharacter;//换行
                        RectangleF descRect = new RectangleF();
                        descRect.X = blankSides;
                        descRect.Y = labelY + _labelFont.Height * 2;
                        descRect.Width = (width - blankSides * 2);
                        descRect.Height = _labelFont.Height*3;// height * 2 / 3 - labelY-blankTop;
                        g.DrawString(_otherData, _labelFont, new SolidBrush(Color.Black),descRect, stringFormat);
                    }

                    g.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ELABEL_GENERIC-1: " + ex.Message);
            }
            return image;
        }
    }
}
