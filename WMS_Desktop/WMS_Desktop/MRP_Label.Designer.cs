namespace WMS_Desktop
{
    partial class MRP_Label
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MRP_Label));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtCharRightShift = new System.Windows.Forms.TextBox();
            this.cmbItem = new System.Windows.Forms.ComboBox();
            this.cmbBoxMRNNO = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbPONO = new System.Windows.Forms.ComboBox();
            this.cmbBoxClient = new System.Windows.Forms.ComboBox();
            this.txtNoOfStricker = new System.Windows.Forms.TextBox();
            this.txtCharLeftShift = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdBtnItemCodeBatchCode = new System.Windows.Forms.RadioButton();
            this.rdBtnItemCodePONO = new System.Windows.Forms.RadioButton();
            this.rdBtnItemCodeMRNNO = new System.Windows.Forms.RadioButton();
            this.rdBtnItemCode = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdbtnQRCode = new System.Windows.Forms.RadioButton();
            this.rdbtnBarcode = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.txtCharRightShift);
            this.groupBox1.Controls.Add(this.cmbItem);
            this.groupBox1.Controls.Add(this.cmbBoxMRNNO);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbPONO);
            this.groupBox1.Controls.Add(this.cmbBoxClient);
            this.groupBox1.Controls.Add(this.txtNoOfStricker);
            this.groupBox1.Controls.Add(this.txtCharLeftShift);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(26, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(904, 255);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select MRP / Industrial Use / Retail Sale";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(51, 146);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 13);
            this.label9.TabIndex = 18;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(513, 171);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(176, 20);
            this.txtDescription.TabIndex = 17;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            this.txtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescription_KeyPress);
            this.txtDescription.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDescription_KeyUp);
            // 
            // txtCharRightShift
            // 
            this.txtCharRightShift.Location = new System.Drawing.Point(513, 121);
            this.txtCharRightShift.Name = "txtCharRightShift";
            this.txtCharRightShift.Size = new System.Drawing.Size(176, 20);
            this.txtCharRightShift.TabIndex = 16;
            this.txtCharRightShift.Text = "0";
            this.txtCharRightShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbItem
            // 
            this.cmbItem.FormattingEnabled = true;
            this.cmbItem.Location = new System.Drawing.Point(513, 77);
            this.cmbItem.Name = "cmbItem";
            this.cmbItem.Size = new System.Drawing.Size(176, 21);
            this.cmbItem.TabIndex = 15;
            this.cmbItem.SelectedIndexChanged += new System.EventHandler(this.cmbItem_SelectedIndexChanged);
            // 
            // cmbBoxMRNNO
            // 
            this.cmbBoxMRNNO.FormattingEnabled = true;
            this.cmbBoxMRNNO.Location = new System.Drawing.Point(513, 41);
            this.cmbBoxMRNNO.Name = "cmbBoxMRNNO";
            this.cmbBoxMRNNO.Size = new System.Drawing.Size(176, 21);
            this.cmbBoxMRNNO.TabIndex = 14;
            this.cmbBoxMRNNO.SelectedIndexChanged += new System.EventHandler(this.cmbBoxMRNNO_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(421, 178);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Description";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(421, 132);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 26);
            this.label7.TabIndex = 12;
            this.label7.Text = "No. of char \r\nskip from Right";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(418, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Item";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(418, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "MRN NO";
            // 
            // cmbPONO
            // 
            this.cmbPONO.FormattingEnabled = true;
            this.cmbPONO.Location = new System.Drawing.Point(135, 82);
            this.cmbPONO.Name = "cmbPONO";
            this.cmbPONO.Size = new System.Drawing.Size(167, 21);
            this.cmbPONO.TabIndex = 9;
            this.cmbPONO.SelectedIndexChanged += new System.EventHandler(this.cmbPONO_SelectedIndexChanged);
            // 
            // cmbBoxClient
            // 
            this.cmbBoxClient.FormattingEnabled = true;
            this.cmbBoxClient.Location = new System.Drawing.Point(135, 42);
            this.cmbBoxClient.Name = "cmbBoxClient";
            this.cmbBoxClient.Size = new System.Drawing.Size(167, 21);
            this.cmbBoxClient.TabIndex = 8;
            this.cmbBoxClient.SelectedIndexChanged += new System.EventHandler(this.cmbBoxClient_SelectedIndexChanged);
            // 
            // txtNoOfStricker
            // 
            this.txtNoOfStricker.Location = new System.Drawing.Point(135, 176);
            this.txtNoOfStricker.Name = "txtNoOfStricker";
            this.txtNoOfStricker.Size = new System.Drawing.Size(167, 20);
            this.txtNoOfStricker.TabIndex = 7;
            this.txtNoOfStricker.Text = "1";
            this.txtNoOfStricker.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCharLeftShift
            // 
            this.txtCharLeftShift.Location = new System.Drawing.Point(135, 129);
            this.txtCharLeftShift.Name = "txtCharLeftShift";
            this.txtCharLeftShift.Size = new System.Drawing.Size(167, 20);
            this.txtCharLeftShift.TabIndex = 6;
            this.txtCharLeftShift.Text = "0";
            this.txtCharLeftShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "No Of Strickers";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "No. of char \r\nskip from left";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "PO Number";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Client";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdBtnItemCodeBatchCode);
            this.groupBox2.Controls.Add(this.rdBtnItemCodePONO);
            this.groupBox2.Controls.Add(this.rdBtnItemCodeMRNNO);
            this.groupBox2.Controls.Add(this.rdBtnItemCode);
            this.groupBox2.Location = new System.Drawing.Point(26, 302);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(904, 78);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select Barcode combination";
            // 
            // rdBtnItemCodeBatchCode
            // 
            this.rdBtnItemCodeBatchCode.AutoSize = true;
            this.rdBtnItemCodeBatchCode.Location = new System.Drawing.Point(549, 32);
            this.rdBtnItemCodeBatchCode.Name = "rdBtnItemCodeBatchCode";
            this.rdBtnItemCodeBatchCode.Size = new System.Drawing.Size(140, 17);
            this.rdBtnItemCodeBatchCode.TabIndex = 3;
            this.rdBtnItemCodeBatchCode.Text = "Item code + Batch Code";
            this.rdBtnItemCodeBatchCode.UseVisualStyleBackColor = true;
            // 
            // rdBtnItemCodePONO
            // 
            this.rdBtnItemCodePONO.AutoSize = true;
            this.rdBtnItemCodePONO.Location = new System.Drawing.Point(391, 32);
            this.rdBtnItemCodePONO.Name = "rdBtnItemCodePONO";
            this.rdBtnItemCodePONO.Size = new System.Drawing.Size(116, 17);
            this.rdBtnItemCodePONO.TabIndex = 2;
            this.rdBtnItemCodePONO.Text = "Item Code +PO NO";
            this.rdBtnItemCodePONO.UseVisualStyleBackColor = true;
            // 
            // rdBtnItemCodeMRNNO
            // 
            this.rdBtnItemCodeMRNNO.AutoSize = true;
            this.rdBtnItemCodeMRNNO.Location = new System.Drawing.Point(199, 32);
            this.rdBtnItemCodeMRNNO.Name = "rdBtnItemCodeMRNNO";
            this.rdBtnItemCodeMRNNO.Size = new System.Drawing.Size(138, 17);
            this.rdBtnItemCodeMRNNO.TabIndex = 1;
            this.rdBtnItemCodeMRNNO.Text = "Item Code + MRN Code";
            this.rdBtnItemCodeMRNNO.UseVisualStyleBackColor = true;
            // 
            // rdBtnItemCode
            // 
            this.rdBtnItemCode.AutoSize = true;
            this.rdBtnItemCode.Checked = true;
            this.rdBtnItemCode.Location = new System.Drawing.Point(51, 32);
            this.rdBtnItemCode.Name = "rdBtnItemCode";
            this.rdBtnItemCode.Size = new System.Drawing.Size(97, 17);
            this.rdBtnItemCode.TabIndex = 0;
            this.rdBtnItemCode.TabStop = true;
            this.rdBtnItemCode.Text = "Only Item Code";
            this.rdBtnItemCode.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdbtnQRCode);
            this.groupBox3.Controls.Add(this.rdbtnBarcode);
            this.groupBox3.Location = new System.Drawing.Point(26, 406);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(906, 254);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Select Format Of Print";
            // 
            // rdbtnQRCode
            // 
            this.rdbtnQRCode.AutoSize = true;
            this.rdbtnQRCode.Checked = true;
            this.rdbtnQRCode.Image = ((System.Drawing.Image)(resources.GetObject("rdbtnQRCode.Image")));
            this.rdbtnQRCode.Location = new System.Drawing.Point(426, 34);
            this.rdbtnQRCode.Name = "rdbtnQRCode";
            this.rdbtnQRCode.Size = new System.Drawing.Size(480, 131);
            this.rdbtnQRCode.TabIndex = 1;
            this.rdbtnQRCode.TabStop = true;
            this.rdbtnQRCode.UseVisualStyleBackColor = true;
            // 
            // rdbtnBarcode
            // 
            this.rdbtnBarcode.AutoSize = true;
            this.rdbtnBarcode.Image = ((System.Drawing.Image)(resources.GetObject("rdbtnBarcode.Image")));
            this.rdbtnBarcode.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rdbtnBarcode.Location = new System.Drawing.Point(30, 34);
            this.rdbtnBarcode.Name = "rdbtnBarcode";
            this.rdbtnBarcode.Size = new System.Drawing.Size(379, 184);
            this.rdbtnBarcode.TabIndex = 0;
            this.rdbtnBarcode.UseVisualStyleBackColor = true;
            // 
            // MRP_Label
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 669);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MRP_Label";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MRP_Label";
            this.Load += new System.EventHandler(this.MRP_Label_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtCharRightShift;
        private System.Windows.Forms.ComboBox cmbItem;
        private System.Windows.Forms.ComboBox cmbBoxMRNNO;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbPONO;
        private System.Windows.Forms.ComboBox cmbBoxClient;
        private System.Windows.Forms.TextBox txtNoOfStricker;
        private System.Windows.Forms.TextBox txtCharLeftShift;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdBtnItemCodeBatchCode;
        private System.Windows.Forms.RadioButton rdBtnItemCodePONO;
        private System.Windows.Forms.RadioButton rdBtnItemCodeMRNNO;
        private System.Windows.Forms.RadioButton rdBtnItemCode;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdbtnQRCode;
        private System.Windows.Forms.RadioButton rdbtnBarcode;
    }
}