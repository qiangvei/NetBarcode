﻿namespace NetBarcodeDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.txtStr = new System.Windows.Forms.TextBox();
            this.picBarCodeImg = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBarCodeImg)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(402, 411);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(395, 36);
            this.button1.TabIndex = 0;
            this.button1.Text = "生成条码";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtStr
            // 
            this.txtStr.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStr.Location = new System.Drawing.Point(402, 384);
            this.txtStr.Name = "txtStr";
            this.txtStr.Size = new System.Drawing.Size(395, 22);
            this.txtStr.TabIndex = 1;
            this.txtStr.Text = "X002F6OZRR";
            // 
            // picBarCodeImg
            // 
            this.picBarCodeImg.Location = new System.Drawing.Point(12, 12);
            this.picBarCodeImg.Name = "picBarCodeImg";
            this.picBarCodeImg.Size = new System.Drawing.Size(776, 357);
            this.picBarCodeImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picBarCodeImg.TabIndex = 2;
            this.picBarCodeImg.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 411);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 36);
            this.button2.TabIndex = 3;
            this.button2.Text = "打印";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.picBarCodeImg);
            this.Controls.Add(this.txtStr);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picBarCodeImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtStr;
        private System.Windows.Forms.PictureBox picBarCodeImg;
        private System.Windows.Forms.Button button2;
    }
}

