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
    public partial class Packing_Mannual : Form
    {
        PackinglipManual_dal _PackinglipManual_dal; 
        public Packing_Mannual()
        {
            _PackinglipManual_dal = new PackinglipManual_dal();
            GetlientList();
            InitializeComponent();
        }

        public void GetlientList()
        {
            DataTable dtClient = new DataTable();
            string userId = LoginInfo.UserID;
            int ClientId = _PackinglipManual_dal.GetClientId(userId);
            dtClient = _PackinglipManual_dal.GetClientList(ClientId);
            if (dtClient.Rows.Count > 0)
            {
                //DataRow dr = dtClient.NewRow();
                //dr.ItemArray = new object[] { 0, "-- Select Client --", "-- Select Client --" };
                //dtClient.Rows.InsertAt(dr, 0);
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
         
            DataTable dtpicklistNo = new DataTable();
            grpCartonDetail.Visible = true;
            dtpicklistNo = _PackinglipManual_dal.GetPicklistNo(Convert.ToInt32(cmbShipTo.SelectedValue));
            if (dtpicklistNo.Rows.Count > 0)
            {
                cmbPicklistNo.DisplayMember = "ManualCaseNumber";
                cmbPicklistNo.ValueMember = "ManualCaseNumber";
                cmbPicklistNo.DataSource = dtpicklistNo;
            }



            grpCartonDetail.Visible = true;
            DataTable dtpicklist = new DataTable();
            //dtpicklist = _PackinglipManual_dal.GetShitToIds(Convert.ToInt32(cmbClient.SelectedValue));
            //if (dtpicklist.Rows.Count > 0)
            //{
            //    dataGridViewPicklist.DataSource = dtpicklist;
            //}
            //else 
            //{
            //    dataGridViewPicklist.DataSource = null;
            //}
        }

          private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Qty")
            {
             dataGridView1.CurrentRow.Cells[2].Value = (System.Convert.ToDecimal(dataGridView1.CurrentRow.Cells[1].Value) * System.Convert.ToDecimal("0.01"));
            }
            }

        }



    }

