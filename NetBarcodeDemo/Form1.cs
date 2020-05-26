using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            FbaLable itlab = new FbaLable(data, "some text here", NetBarcode.Type.Code128A);
            picBarCodeImg.Image = itlab.GetItemLable();
        }
    }
}
