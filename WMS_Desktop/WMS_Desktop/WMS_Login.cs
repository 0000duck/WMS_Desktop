using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMS_DAL;
using WMS_Models;

namespace WMS_Desktop
{
    public partial class WMS_Login : Form
    {
        private DAL_Login _dal_Login;
        private DAL_Warehouse _dal_Warehouse;
        public string UserId = string.Empty;

        public WMS_Login()
        {
            InitializeComponent();
            _dal_Login = new DAL_Login();
            _dal_Warehouse = new DAL_Warehouse();
        }

        private void WMS_Login_Load(object sender, EventArgs e)
        {
            DataTable dt = _dal_Warehouse.GetWarehouseList();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "-- Select Warehouse --", "-- Select Warehouse --" };
                dt.Rows.InsertAt(dr, 0);
                cmbWarehouse.DataSource = dt;
                cmbWarehouse.DisplayMember = "Name";
                cmbWarehouse.ValueMember = "Id";
            }

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //WMS_Master obj = new WMS_Master();
            //obj.Show();


            //if (Convert.ToInt32(cmbWarehouse.SelectedValue) <= 0)
            //{
            //    MessageBox.Show("Please select warehouse.", "Warning");
            //}
            //else if (string.IsNullOrEmpty(txtPassword.Text.Trim()) || string.IsNullOrEmpty(txtUsername.Text.Trim()))
            //{
            //    MessageBox.Show("Please enter username and password.", "Warning");
            //}
            //else
            //{
            //    LoginVO loginVO = new LoginVO();
            //    loginVO.WarehouseId = Convert.ToInt32(cmbWarehouse.SelectedValue);
            //    loginVO.Username = txtUsername.Text.Trim();
            //    loginVO.Password = txtPassword.Text.Trim();

            //    DataTable dt = _dal_Login.GetLoginDetails(loginVO);
            //    if (dt != null && dt.Rows.Count > 0)
            //    {

            //        this.Hide();
            //        UserId = dt.Rows[0]["Id"].ToString();
            //        LoginInfo.UserID = dt.Rows[0]["Id"].ToString();
            //        WMS_Master master = new WMS_Master();
            //        master.Show();

            //    }
            //    else
            //    {
            //        txtUsername.Text = string.Empty;
            //        txtPassword.Text = string.Empty;
            //        MessageBox.Show("Invalid username and password.", "Warning");
            //    }

            //}

            this.Hide();
            //UserId = dt.Rows[0]["Id"].ToString();
           // LoginInfo.UserID = dt.Rows[0]["Id"].ToString();
            WMS_Master master = new WMS_Master();
            master.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnForgotPassword_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please contact to administrator.", "Information");
        }

        public string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        //public bool VerifyHashedPassword(string hashedPassword, string password)
        //{
        //    byte[] buffer4;
        //    if (hashedPassword == null)
        //    {
        //        return false;
        //    }
        //    if (password == null)
        //    {
        //        throw new ArgumentNullException("password");
        //    }
        //    byte[] src = Convert.FromBase64String(hashedPassword);
        //    if ((src.Length != 0x31) || (src[0] != 0))
        //    {
        //        return false;
        //    }
        //    byte[] dst = new byte[0x10];
        //    Buffer.BlockCopy(src, 1, dst, 0, 0x10);
        //    byte[] buffer3 = new byte[0x20];
        //    Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
        //    using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
        //    {
        //        buffer4 = bytes.GetBytes(0x20);
        //    }
        //    return ByteArraysEqual(buffer3, buffer4);
        //}

    }

    public static class LoginInfo
    {
        public static string UserID;
    }
}
