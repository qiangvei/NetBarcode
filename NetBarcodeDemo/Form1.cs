using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetBarcode;

namespace NetBarcodeDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String data = txtStr.Text.Trim().ToUpper();
            
            int ww = picBarCodeImg.Width/4;
            int hh = picBarCodeImg.Height/4;
            string otherText = "some text will show here draw datastring under the barcode image some text will show here draw datastring under the barcode image";
            //BarcodeForImg nb = new BarcodeForImg(data, otherText, NetBarcode.Type.Code128A, true, ww, hh, new Font("Consolas", 12));
            //picBarCodeImg.Image = nb.GetImage(); 
            try
            {
                FbaLable itlab = new FbaLable(data, "some text will show here draw datastring under the barcode image some text will show here draw datastring under the barcode image", NetBarcode.Type.Code128A);
                picBarCodeImg.Image = itlab.GetItemLable();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.DefaultPageSettings.Margins.Left = 0;
            pd.PrinterSettings.DefaultPageSettings.Margins.Top = 0;
            pd.PrinterSettings.DefaultPageSettings.Margins.Right = 0;
            pd.PrinterSettings.DefaultPageSettings.Margins.Bottom = 0;
            //设置尺寸大小，如不设置默认是A4纸
            //A4纸的尺寸是210mm×297mm，
            //当你设定的分辨率是72像素/英寸时，A4纸的尺寸的图像的像素是595×842
            //当你设定的分辨率是150像素/英寸时，A4纸的尺寸的图像的像素是1240×1754
            //当你设定的分辨率是300像素/英寸时，A4纸的尺寸的图像的像素是2479×3508，
            //pd.DefaultPageSettings.PaperSize = new PaperSize("A4", 595, 842);
            pd.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);//【非常重要】在这个方法里画出要打印的内容。
            try
            {
                pd.Print();
            }
            catch (InvalidPrinterException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                pd.Dispose();
            }
        }

        /// <summary>
        /// 打印的内容,这相当于打印机内，在里面画出的内容全部会打印出来
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            String data = txtStr.Text.Trim().ToUpper();
            try
            {

                FbaLable itlab = new FbaLable(data, "测试标签打印some text will show here draw datastring under the barcode image some text will show here draw datastring under the barcode image", NetBarcode.Type.Code128A);
                Image image = itlab.GetItemLable();
                e.Graphics.DrawImage(image,0,0);

                //画更多的图
                //Font printFont = new Font("宋体", 12, System.Drawing.FontStyle.Regular);
                //var printColor = System.Drawing.Brushes.Black;
                ////画字符串
                //e.Graphics.DrawString(data, printFont, Brushes.Black,0, image.Height+50);


                e.HasMorePages = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
