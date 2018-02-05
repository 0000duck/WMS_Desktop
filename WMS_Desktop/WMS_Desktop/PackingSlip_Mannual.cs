using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMS_DAL;
using WMS_Models;
using System.IO;
using ZXing;
using ZXing.QrCode;
using System.Net;
using Microsoft.Reporting.WinForms;
using System.Drawing.Imaging;
using System.Web;
using System.Diagnostics;
using System.Configuration;


namespace WMS_Desktop
{
    public partial class PackingSlip_Mannual : Form
    {
        PackinglipManual_dal _PackinglipManual_dal; 

        public PackingSlip_Mannual()
        {
            InitializeComponent();
            _PackinglipManual_dal = new PackinglipManual_dal();
            GetlientList();
        }
        #region commit
        private void PackingSlip_Mannual_Load(object sender, EventArgs e)
        {

        }

        private void dataGridViewPicklist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (string.Compare(dataGridViewPicklist.CurrentCell.OwningColumn.Name, "Select") == 0)
            {
                bool checkBoxStatus = Convert.ToBoolean(dataGridViewPicklist.CurrentCell.EditedFormattedValue);
                if (checkBoxStatus==true)
                {
                    //DataTable dtpicklistDetails = _PackinglipManual_dal. (Convert.ToInt32(cmbClient.SelectedValue));
                    //if (dtpicklist.Rows.Count > 0)
                    ////{
                    //txtManualCaseNo.Text = dtpicklistDetails.Rows[0][""].ToString();
                    //txtTotalNoBox.Text = dtpicklistDetails.Rows[0][""].ToString();
                    //txtTotalQty.Text = dtpicklistDetails.Rows[0][""].ToString();
                    //txtTotalWeight.Text = dtpicklistDetails.Rows[0][""].ToString(); 
                    //txtUOM.Text = dtpicklistDetails.Rows[0][""].ToString(); 
                    //// dataGridView1.DataSource = dtpicklist;
                    ////}
                    ////else 
                    ////{    MessageBox.Show("Not Record Found");
                    ////    dataGridView1.DataSource = null;
                    ////}

                }
                else
                {
                   
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                
        }


      
        #endregion

        public void GetlientList()
        {
            DataTable dtClient = new DataTable();
            string userId = LoginInfo.UserID;
            int ClientId = _PackinglipManual_dal.GetClientId(userId);
            dtClient = _PackinglipManual_dal.GetClientList(ClientId);
            if (dtClient.Rows.Count > 0)
            {
                DataRow dr = dtClient.NewRow();
                dr.ItemArray = new object[] { 0, "-- Select Client --", "-- Select Client --" };
                dtClient.Rows.InsertAt(dr, 0);
                cmbClient.DisplayMember = "Name";
                cmbClient.ValueMember = "Id";
                cmbClient.DataSource = dtClient;
            }
        }
        private void cmbClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtShiftToCode = new DataTable();
            grpCartonDetail.Visible = true;
            dtShiftToCode = _PackinglipManual_dal.GetShitToIds(Convert.ToInt32(cmbClient.SelectedValue));
            if (dtShiftToCode.Rows.Count > 0)
            {
                    cmbShipTo.DisplayMember = "Name";
                    cmbShipTo.ValueMember = "Id";
                    cmbShipTo.DataSource = dtShiftToCode;
            }
        }

        private void cmbShipTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dataGridViewPicklist.Visible = true;
            //grpCartonDetail.Visible = true;
            DataTable dtpicklist = new DataTable();
            dtpicklist = _PackinglipManual_dal.GetPicklistNo(Convert.ToInt32(cmbShipTo.SelectedValue));
            if (dtpicklist.Rows.Count > 0)
            {
                    CmbPicklistNo.DisplayMember = "ManualCaseNumber";
                    CmbPicklistNo.ValueMember = "ManualCaseNumber";
                    CmbPicklistNo.DataSource = dtpicklist;
            }
            else
            {
                CmbPicklistNo.DataSource = null;
                dataGridViewPicklist.DataSource = null;
            }

        }





        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int cell8 = Convert.ToInt32(dataGridView1.CurrentRow.Cells[8].Value);

            int cell9 = Convert.ToInt32(dataGridView1.CurrentRow.Cells[9].Value);

            dataGridView1.CurrentRow.Cells[10].Value = cell8 - cell9;

            }

        private void CmbPicklistNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtPicklistDetail = new DataTable();
            string userId = LoginInfo.UserID;
            int ClientId = _PackinglipManual_dal.GetClientId(userId);
            dtPicklistDetail = _PackinglipManual_dal.GetPicklistDetail(CmbPicklistNo.SelectedValue.ToString(), ClientId);
            if (dtPicklistDetail.Rows.Count > 0)
            {
                dataGridView1.DataSource = dtPicklistDetail;
                //DataGridViewComboBoxCell ComboColumn = (DataGridViewComboBoxCell)(dataGridView1.Rows[12].Cells[0]);
                //ComboColumn.DataSource = dtPicklistDetail.Rows[0]["ItemDescription"];
                //ComboColumn.DisplayMember = "ItemDescription";
                //dataGridView1.Columns.Add(ComboColumn);
            }
            else 
            {
                dataGridView1.DataSource = null;
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //DataTable dtPicklistDetail = new DataTable();
            //string userId = LoginInfo.UserID;
            //int ClientId = _PackinglipManual_dal.GetClientId(userId);
            //dtPicklistDetail = _PackinglipManual_dal.GetPicklistDetail(CmbPicklistNo.SelectedValue.ToString(), ClientId);
            ////DataGridViewComboBoxCell box = dataGridView1.Rows[e.RowIndex].Cells[12] as DataGridViewComboBoxCell;
            ////box.DataSource = dtPicklistDetail;
            //try
            //{
            //    DataGridViewComboBoxCell ComboColumn = (DataGridViewComboBoxCell)(dataGridView1.Rows[12].Cells[0]);

            //    ComboColumn.DataSource = dtPicklistDetail.Rows[0]["ItemDescription"];
            //    ComboColumn.DisplayMember = "ItemDescription";
            //}
            //catch { }

        }

        }
    
}
