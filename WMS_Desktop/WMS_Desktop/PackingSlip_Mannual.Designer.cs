namespace WMS_Desktop
{
    partial class PackingSlip_Mannual
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnreset = new System.Windows.Forms.Button();
            this.cmbShipTo = new System.Windows.Forms.ComboBox();
            this.lblShipTo = new System.Windows.Forms.Label();
            this.lblClient = new System.Windows.Forms.Label();
            this.cmbClient = new System.Windows.Forms.ComboBox();
            this.dataGridViewPicklist = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PicklistNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpCartonDetail = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PONumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SONumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PickedQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackedQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BalanceQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoxNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Carton = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Grossweight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Netweight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTotalQty = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUOM = new System.Windows.Forms.TextBox();
            this.txtTotalWeight = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotalNoBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtManualCaseNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCartoncode = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPicklist)).BeginInit();
            this.grpCartonDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnreset);
            this.groupBox1.Controls.Add(this.cmbShipTo);
            this.groupBox1.Controls.Add(this.lblShipTo);
            this.groupBox1.Controls.Add(this.lblClient);
            this.groupBox1.Controls.Add(this.cmbClient);
            this.groupBox1.Location = new System.Drawing.Point(26, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1008, 85);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(743, 30);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnreset
            // 
            this.btnreset.Location = new System.Drawing.Point(646, 30);
            this.btnreset.Name = "btnreset";
            this.btnreset.Size = new System.Drawing.Size(75, 23);
            this.btnreset.TabIndex = 4;
            this.btnreset.Text = "Reset";
            this.btnreset.UseVisualStyleBackColor = true;
            // 
            // cmbShipTo
            // 
            this.cmbShipTo.FormattingEnabled = true;
            this.cmbShipTo.Location = new System.Drawing.Point(381, 32);
            this.cmbShipTo.Name = "cmbShipTo";
            this.cmbShipTo.Size = new System.Drawing.Size(209, 21);
            this.cmbShipTo.TabIndex = 3;
            this.cmbShipTo.SelectedIndexChanged += new System.EventHandler(this.cmbShipTo_SelectedIndexChanged);
            // 
            // lblShipTo
            // 
            this.lblShipTo.AutoSize = true;
            this.lblShipTo.Location = new System.Drawing.Point(340, 37);
            this.lblShipTo.Name = "lblShipTo";
            this.lblShipTo.Size = new System.Drawing.Size(44, 13);
            this.lblShipTo.TabIndex = 2;
            this.lblShipTo.Text = "Ship To";
            // 
            // lblClient
            // 
            this.lblClient.AutoSize = true;
            this.lblClient.Location = new System.Drawing.Point(44, 37);
            this.lblClient.Name = "lblClient";
            this.lblClient.Size = new System.Drawing.Size(33, 13);
            this.lblClient.TabIndex = 1;
            this.lblClient.Text = "Client";
            // 
            // cmbClient
            // 
            this.cmbClient.FormattingEnabled = true;
            this.cmbClient.Location = new System.Drawing.Point(97, 34);
            this.cmbClient.Name = "cmbClient";
            this.cmbClient.Size = new System.Drawing.Size(209, 21);
            this.cmbClient.TabIndex = 0;
            this.cmbClient.SelectedIndexChanged += new System.EventHandler(this.cmbClient_SelectedIndexChanged);
            // 
            // dataGridViewPicklist
            // 
            this.dataGridViewPicklist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPicklist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.PicklistNo,
            this.TotalItems,
            this.TotalQuantity,
            this.TotalAmount});
            this.dataGridViewPicklist.Location = new System.Drawing.Point(26, 113);
            this.dataGridViewPicklist.Name = "dataGridViewPicklist";
            this.dataGridViewPicklist.Size = new System.Drawing.Size(1008, 67);
            this.dataGridViewPicklist.TabIndex = 0;
            this.dataGridViewPicklist.Visible = false;
            this.dataGridViewPicklist.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPicklist_CellContentClick);
            // 
            // Select
            // 
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            // 
            // PicklistNo
            // 
            this.PicklistNo.HeaderText = "Picklist No.";
            this.PicklistNo.Name = "PicklistNo";
            this.PicklistNo.Width = 200;
            // 
            // TotalItems
            // 
            this.TotalItems.HeaderText = "Total Items";
            this.TotalItems.Name = "TotalItems";
            this.TotalItems.Width = 200;
            // 
            // TotalQuantity
            // 
            this.TotalQuantity.HeaderText = "Total Quantity";
            this.TotalQuantity.Name = "TotalQuantity";
            this.TotalQuantity.Width = 200;
            // 
            // TotalAmount
            // 
            this.TotalAmount.HeaderText = "Total Amount";
            this.TotalAmount.Name = "TotalAmount";
            this.TotalAmount.Width = 200;
            // 
            // grpCartonDetail
            // 
            this.grpCartonDetail.Controls.Add(this.dataGridView1);
            this.grpCartonDetail.Controls.Add(this.txtTotalQty);
            this.grpCartonDetail.Controls.Add(this.label5);
            this.grpCartonDetail.Controls.Add(this.txtUOM);
            this.grpCartonDetail.Controls.Add(this.txtTotalWeight);
            this.grpCartonDetail.Controls.Add(this.label4);
            this.grpCartonDetail.Controls.Add(this.txtTotalNoBox);
            this.grpCartonDetail.Controls.Add(this.label3);
            this.grpCartonDetail.Controls.Add(this.txtManualCaseNo);
            this.grpCartonDetail.Controls.Add(this.label2);
            this.grpCartonDetail.Controls.Add(this.lblCartoncode);
            this.grpCartonDetail.Controls.Add(this.label1);
            this.grpCartonDetail.Location = new System.Drawing.Point(26, 186);
            this.grpCartonDetail.Name = "grpCartonDetail";
            this.grpCartonDetail.Size = new System.Drawing.Size(1008, 464);
            this.grpCartonDetail.TabIndex = 2;
            this.grpCartonDetail.TabStop = false;
            this.grpCartonDetail.Text = "groupBox2";
            this.grpCartonDetail.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeight = 35;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemCode,
            this.ItemDescription,
            this.PONumber,
            this.SONumber,
            this.BatchNumber,
            this.TransType,
            this.LocationCode,
            this.PickedQuantity,
            this.PackedQty,
            this.BalanceQty,
            this.BoxNo,
            this.Carton,
            this.Grossweight,
            this.Netweight});
            this.dataGridView1.Location = new System.Drawing.Point(22, 159);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.Size = new System.Drawing.Size(964, 158);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // ItemCode
            // 
            this.ItemCode.HeaderText = "Item Code";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.Width = 80;
            // 
            // ItemDescription
            // 
            this.ItemDescription.HeaderText = "Item Description";
            this.ItemDescription.Name = "ItemDescription";
            this.ItemDescription.Width = 80;
            // 
            // PONumber
            // 
            this.PONumber.HeaderText = "PO Number";
            this.PONumber.Name = "PONumber";
            this.PONumber.Width = 60;
            // 
            // SONumber
            // 
            this.SONumber.HeaderText = "SO Number";
            this.SONumber.Name = "SONumber";
            this.SONumber.Width = 60;
            // 
            // BatchNumber
            // 
            this.BatchNumber.HeaderText = "Batch Number";
            this.BatchNumber.Name = "BatchNumber";
            this.BatchNumber.Width = 60;
            // 
            // TransType
            // 
            this.TransType.HeaderText = "Trans Type";
            this.TransType.Name = "TransType";
            this.TransType.Width = 60;
            // 
            // LocationCode
            // 
            this.LocationCode.HeaderText = "Location Code";
            this.LocationCode.Name = "LocationCode";
            this.LocationCode.Width = 60;
            // 
            // PickedQuantity
            // 
            this.PickedQuantity.HeaderText = "Picked Quantity";
            this.PickedQuantity.Name = "PickedQuantity";
            this.PickedQuantity.Width = 60;
            // 
            // PackedQty
            // 
            this.PackedQty.HeaderText = "Packed Qty";
            this.PackedQty.Name = "PackedQty";
            this.PackedQty.Width = 60;
            // 
            // BalanceQty
            // 
            this.BalanceQty.HeaderText = "BalanceQty";
            this.BalanceQty.Name = "BalanceQty";
            this.BalanceQty.Width = 70;
            // 
            // BoxNo
            // 
            this.BoxNo.HeaderText = "Box No";
            this.BoxNo.Name = "BoxNo";
            this.BoxNo.Width = 65;
            // 
            // Carton
            // 
            this.Carton.HeaderText = "Carton";
            this.Carton.Name = "Carton";
            this.Carton.Width = 90;
            // 
            // Grossweight
            // 
            this.Grossweight.HeaderText = "Gross weight";
            this.Grossweight.Name = "Grossweight";
            this.Grossweight.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Grossweight.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Grossweight.Width = 60;
            // 
            // Netweight
            // 
            this.Netweight.HeaderText = "Net weight";
            this.Netweight.Name = "Netweight";
            this.Netweight.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Netweight.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Netweight.Width = 60;
            // 
            // txtTotalQty
            // 
            this.txtTotalQty.Enabled = false;
            this.txtTotalQty.Location = new System.Drawing.Point(114, 112);
            this.txtTotalQty.Name = "txtTotalQty";
            this.txtTotalQty.Size = new System.Drawing.Size(156, 20);
            this.txtTotalQty.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Total Quantity.";
            // 
            // txtUOM
            // 
            this.txtUOM.Enabled = false;
            this.txtUOM.Location = new System.Drawing.Point(802, 61);
            this.txtUOM.Name = "txtUOM";
            this.txtUOM.Size = new System.Drawing.Size(52, 20);
            this.txtUOM.TabIndex = 8;
            // 
            // txtTotalWeight
            // 
            this.txtTotalWeight.Enabled = false;
            this.txtTotalWeight.Location = new System.Drawing.Point(630, 61);
            this.txtTotalWeight.Name = "txtTotalWeight";
            this.txtTotalWeight.Size = new System.Drawing.Size(156, 20);
            this.txtTotalWeight.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(556, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Total Weight";
            // 
            // txtTotalNoBox
            // 
            this.txtTotalNoBox.Enabled = false;
            this.txtTotalNoBox.Location = new System.Drawing.Point(372, 61);
            this.txtTotalNoBox.Name = "txtTotalNoBox";
            this.txtTotalNoBox.Size = new System.Drawing.Size(156, 20);
            this.txtTotalNoBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(292, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Total Boxes";
            // 
            // txtManualCaseNo
            // 
            this.txtManualCaseNo.Enabled = false;
            this.txtManualCaseNo.Location = new System.Drawing.Point(114, 61);
            this.txtManualCaseNo.Name = "txtManualCaseNo";
            this.txtManualCaseNo.Size = new System.Drawing.Size(156, 20);
            this.txtManualCaseNo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Manual Case No.";
            // 
            // lblCartoncode
            // 
            this.lblCartoncode.AutoSize = true;
            this.lblCartoncode.Location = new System.Drawing.Point(97, 20);
            this.lblCartoncode.Name = "lblCartoncode";
            this.lblCartoncode.Size = new System.Drawing.Size(0, 13);
            this.lblCartoncode.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Carton code :-";
            // 
            // PackingSlip_Mannual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 644);
            this.Controls.Add(this.dataGridViewPicklist);
            this.Controls.Add(this.grpCartonDetail);
            this.Controls.Add(this.groupBox1);
            this.Name = "PackingSlip_Mannual";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PackingSlip_Mannual";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPicklist)).EndInit();
            this.grpCartonDetail.ResumeLayout(false);
            this.grpCartonDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnreset;
        private System.Windows.Forms.ComboBox cmbShipTo;
        private System.Windows.Forms.Label lblShipTo;
        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.ComboBox cmbClient;
        private System.Windows.Forms.DataGridView dataGridViewPicklist;
        private System.Windows.Forms.GroupBox grpCartonDetail;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtTotalQty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUOM;
        private System.Windows.Forms.TextBox txtTotalWeight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTotalNoBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtManualCaseNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCartoncode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn PicklistNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn PONumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn SONumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransType;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn PickedQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackedQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn BalanceQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn BoxNo;
        private System.Windows.Forms.DataGridViewComboBoxColumn Carton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Grossweight;
        private System.Windows.Forms.DataGridViewTextBoxColumn Netweight;
    }
}