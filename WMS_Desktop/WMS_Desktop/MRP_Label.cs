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
    public partial class MRP_Label : Form
    {

        private DAL_MRPLabelPrint _dal_MrpPrint;
        private MRPPrintModel MrpModel;
        public DataTable MRNLabelItem = new DataTable();
        public DataTable MRNLabelItem1 = new DataTable();
        public string MRPValueMRP = string.Empty;

        public MRP_Label()
        {
            InitializeComponent();
            _dal_MrpPrint = new DAL_MRPLabelPrint();
            MrpModel = new MRPPrintModel();
        }

        private void MRP_Label_Load(object sender, EventArgs e)
        {
            FillClient();
            //this.viewer.RefreshReport();
        }

        private void FillClient()
        {
            DataTable dt = _dal_MrpPrint.GetClientList();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "-- Select Client --", "-- Select Client --" };
                dt.Rows.InsertAt(dr, 0);
                cmbBoxClient.DisplayMember = "Name";
                cmbBoxClient.ValueMember = "Id";
                cmbBoxClient.DataSource = dt;
            }
        }

        private void cmbBoxClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Client_Id = Convert.ToInt32(cmbBoxClient.SelectedValue);
            DataTable dt = _dal_MrpPrint.GetClientMRNNO(Client_Id);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                //dr.ItemArray = new object[] { 0, "-- Select MRN NO --", "-- Select MRN NO --" };
                //dt.Rows.InsertAt(dr, 0);
                cmbBoxMRNNO.DisplayMember = "MRNNo";
                cmbBoxMRNNO.ValueMember = "MRNNo";
                cmbBoxMRNNO.DataSource = dt;

            }
        }

        private void cmbBoxMRNNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Mrnno = cmbBoxMRNNO.SelectedValue.ToString();
            DataTable dt = _dal_MrpPrint.GetPONO(Mrnno);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                cmbPONO.DisplayMember = "PONumber";
                cmbPONO.ValueMember = "OrderId";
                cmbPONO.DataSource = dt;

            }
        }


        private void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string MRPVALUE = _dal_MrpPrint.ItemMrpValue(Convert.ToInt32(cmbItem.SelectedValue));
        }

        private void cmbPONO_SelectedIndexChanged(object sender, EventArgs e)
        {
            int OrderId = Convert.ToInt32(cmbPONO.SelectedValue);
            DataTable dt = _dal_MrpPrint.GetItems(OrderId);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                //dr.ItemArray = new object[] { 0, "-- Select Item --", "-- Select Item --" };
                //dt.Rows.InsertAt(dr, 0);

                cmbItem.DisplayMember = "itemCode";
                cmbItem.ValueMember = "id";
                cmbItem.DataSource = dt;
            }
        }

        public static byte[] GetBytesFromFile(string fullFilePath)
        {
            // this method is limited to 2^32 byte files (4.2 GB)

            FileStream fs = null;
            try
            {
                fs = System.IO.File.OpenRead(fullFilePath);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                return bytes;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }

        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            string Itemcode = _dal_MrpPrint.GetItemsCode(Convert.ToInt32(cmbItem.SelectedValue));
            if (txtDescription.Text.Trim() == Itemcode.ToString().Trim())
            {
                if (rdbtnBarcode.Checked == true)
                {
                    PrintBarcode();
                }
                else
                {
                    //Qr code
                    PrintLabelForQRCode();
                }
            }
            else
            {
                MessageBox.Show("Item Code are not match");
                // no match scanner code
            }

        }

        private void LocalReportLabel_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            string strParameter = e.Parameters["FID"].Values[0].ToString();
            DataTable dt = MRNLabelItem;
            DataTable dts = dt.Select("FID = '" + strParameter + "'").CopyToDataTable();
            ReportDataSource rds = new ReportDataSource("dsWMS", dts);
            e.DataSources.Add(rds);
        }

        //void LocalReportLabel_SubreportProcessing1(object sender, Microsoft.Reporting.WinForms.SubreportProcessingEventArgs e)
        //{
        //    string strParameter = e.Parameters["FID"].Values[0].ToString();
        //    DataTable dt = MRNLabelItem1;
        //    DataTable dts = dt.Select("FID = '" + strParameter + "'").CopyToDataTable();
        //    ReportDataSource rds = new ReportDataSource("dsWMS1", dts);
        //    e.DataSources.Add(rds);

        //}

        public void PrintBarcode()
        {
            int clientid = Convert.ToInt32(cmbBoxClient.SelectedValue);
            string MRNNo = cmbBoxMRNNO.SelectedValue.ToString();
            int OrderId = Convert.ToInt32(cmbPONO.SelectedValue);
            int Itemid = Convert.ToInt32(cmbItem.SelectedValue);
            string Itemdesc = txtDescription.Text;
            int noOfStickers = Convert.ToInt32(txtNoOfStricker.Text);
            int barcodeCombinationid = 1;
            //int SelectMRPOrIndustrailuseValue;
            //int IFMRPIsZero;
            WMSData ws = new WMSData();
            WhsData WMSDs1 = new WhsData();
            DataTable dt = new DataTable();

            try
            {
                // var user = WMSWebSession.GetInstance().User;
                dt = _dal_MrpPrint.GetPreferenceById(clientid);
                string labelColumnId = dt.Rows[0][0].ToString();
                int[] labelColumnIds1= new int[10];
                int a=0;
                foreach (var i in labelColumnId.Split(','))
                {
                    labelColumnIds1[a] = Convert.ToInt32 (i);
                    a++;
                }

                // Get picking strategy of Item 

                int ItemId=Convert.ToInt32(cmbItem.SelectedValue);
                var PickingStrategy = _dal_MrpPrint.GetPickingStrategyOfItem(ItemId);

                if (PickingStrategy == 1)
                {
                    barcodeCombinationid = 3;
                }
                else if (PickingStrategy == 2)
                {
                    barcodeCombinationid = 4;
                }

                string barcodeType = barcodeCombinationid == 1 ? "OnlyItemCode" : (barcodeCombinationid == 2 ? "ItemCodeMRNNo" : (barcodeCombinationid == 3 ? "ItemCodePONo" : (barcodeCombinationid == 4 ? "ItemCodeBatchNo" : "ItemCodeSerialNo")));

                //DataSet dsmrnItems = new DataSet(); 
                DataTable dtmrnItems = new DataTable();

                MrpModel.Client_Id = clientid;
                MrpModel.MRNNo = MRNNo.ToString();
                string itemCode = _dal_MrpPrint.GetItemsCode(Convert.ToInt32(cmbItem.SelectedValue));
                MrpModel.itemCode = itemCode.ToString().Trim();
                MrpModel.OrderId = OrderId;

                dtmrnItems = _dal_MrpPrint.GetItemDetailsForLablePrinting(MrpModel);

                if (dtmrnItems != null && dtmrnItems.Rows.Count > 0)
                {
                    int fid = 1;
                    for (int i = 0; i < dtmrnItems.Rows.Count;i++)
                    {
                        for (int Noofst = 0; Noofst < noOfStickers; Noofst++)
                        {
                            string directoryPath = string.Format("{0}\\{1}", System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Descripancy_Barcode");
                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory((directoryPath));
                            }
                            var writer = new ZXing.BarcodeWriter();
                            writer.Options.PureBarcode = true;
                            writer.Format = BarcodeFormat.CODE_128;

                            string barcodeString = dtmrnItems.Rows[i]["ItemCode"].ToString();
                            if (barcodeCombinationid == 2)
                                barcodeString = barcodeString + "_" + MRNNo;
                            else if (barcodeCombinationid == 3)
                                barcodeString = barcodeString + "_" + dtmrnItems.Rows[i]["ItemCode"].ToString();
                            else if (barcodeCombinationid == 4)
                                barcodeString = barcodeString + "-" + dtmrnItems.Rows[i]["BatchNo"];
                            else if (barcodeCombinationid == 5)
                            {
                                //if (mrnItem.SerialNo != null && qty < mrnItem.SerialNo.Length)
                                //    barcodeString = barcodeString + "-" + mrnItem.SerialNo[qty];
                                //else
                                barcodeString = barcodeString + "-" + "NA";
                            }

                            var result = writer.Write(barcodeString);
                            string imgPath = @"~/Descripancy_Barcode/" + MRNNo + "_" + barcodeString + ".jpg";
                            var barcodeBitmap = new Bitmap(result);
                            using (MemoryStream memory = new MemoryStream())
                            {
                                using (FileStream fs = new FileStream(imgPath, FileMode.Create, FileAccess.ReadWrite))
                                {
                                    barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                                    byte[] imgbytes = memory.ToArray();
                                    fs.Write(imgbytes, 0, imgbytes.Length);
                                }
                            }
                            //string path = AppDomain.CurrentDomain.BaseDirectory + imgPath;
                            string path = string.Format("{0}\\{1}", System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), imgPath);
                            path = "File:///" + path;

                            ws.tblLabelBarcode.Rows.Add(path, fid);

                            //if (labelColumnIds.Contains(1))
                            ws.tblLabelPrinting.Rows.Add("Description", dtmrnItems.Rows[i]["ItemDescription"], fid);
                            //if (labelColumnIds.Contains(2))
                            ws.tblLabelPrinting.Rows.Add("ItemCode", dtmrnItems.Rows[i]["ClientItemDesc"], fid);
                            if (labelColumnIds1.Contains(2))
                                ws.tblLabelPrinting.Rows.Add("PONo", "PO No.: " + dtmrnItems.Rows[i]["PONumber"], fid);
                            if (labelColumnIds1.Contains(3))
                                ws.tblLabelPrinting.Rows.Add("SONo", "SO No.: " + dtmrnItems.Rows[i]["SONumber"], fid);
                            if (labelColumnIds1.Contains(1))
                                ws.tblLabelPrinting.Rows.Add("MRNNo", "MRN No.: " + dtmrnItems.Rows[i]["MRNNo"], fid);
                            if (labelColumnIds1.Contains(6))
                                ws.tblLabelPrinting.Rows.Add("BatchNo", "Batch No.: " + dtmrnItems.Rows[i]["BatchNo"], fid);
                            if (labelColumnIds1.Contains(11))
                            {
                                //if (mrnItem.SerialNo != null && qty < mrnItem.SerialNo.Length)
                                //    wmsDs.tblLabelPrinting.Rows.Add(@ReportRes.SerialNo, mrnItem.SerialNo[qty], fid);
                                //else
                                //  wmsDs.tblLabelPrinting.Rows.Add(@ReportRes.SerialNo, "NA", fid);
                            }
                            if (labelColumnIds1.Contains(7))
                                ws.tblLabelPrinting.Rows.Add("ExpDate", "Expiry Date: " + dtmrnItems.Rows[i]["ExpiryDate"], fid);
                            if (labelColumnIds1.Contains(8))
                                ws.tblLabelPrinting.Rows.Add("MfgDate", "Mfg. Date: " + dtmrnItems.Rows[i]["ProductionDate"], fid);
                            //if ((labelColumnIds.Contains(9) && SelectMRPOrIndustrailuseValue != 2) || SelectMRPOrIndustrailuseValue == 1)
                            //    if (mrnItem.MRP == 0)
                            //    {
                            //        if (IFMRPIsZero == 2)
                            //        {
                            //            SelectMRPOrIndustrailuseValue = 3;
                            //            // wmsDs.tblLabelPrinting.Rows.Add(@ReportRes.MRP, "For Industrail use only", fid);
                            //        }
                            //        else if (IFMRPIsZero == 3)
                            //        {
                            //            SelectMRPOrIndustrailuseValue = 4;
                            //            // wmsDs.tblLabelPrinting.Rows.Add(@ReportRes.MRP, "Not for Retail Sale", fid);
                            //        }

                            //    }
                            //    else
                            //    {
                            //        wmsDs.tblLabelPrinting.Rows.Add(@ReportRes.MRP, mrnItem.MRP, fid);
                            //    }

                            if (labelColumnIds1.Contains(10))
                                ws.tblLabelPrinting.Rows.Add("Quantity", "Unit Qty.: " + dtmrnItems.Rows[i]["UnitQuantity"] + " " + dtmrnItems.Rows[i]["UOM_Name"], fid);

                            if (Convert.ToDouble(dtmrnItems.Rows[i]["MRP"]) == 0.0)
                            {
                                if (dtmrnItems.Rows[i]["MRPValue"] != null || dtmrnItems.Rows[i]["MRPValue"] != " ")
                                {
                                    if (dtmrnItems.Rows[i]["MRPValue"] == "I")
                                    {
                                        ws.tblLabelPrinting.Rows.Add("MRP", "Industrial", fid);
                                    }
                                    else if (dtmrnItems.Rows[i]["MRPValue"] == "N")
                                    {
                                        ws.tblLabelPrinting.Rows.Add("MRP", "Not For sale", fid);
                                    }
                                }
                            }
                            else
                            {
                                ws.tblLabelPrinting.Rows.Add("MRP", Convert.ToDouble(dtmrnItems.Rows[i]["MRP"]), fid);
                            }
                            fid = fid + 1;
                        }
                        //}
                    }

                    MRNLabelItem = ws.tblLabelPrinting;
                    // MRNLabelItem1 = WMSDs1.tblLabelPrinting1;
                }
                // Variables
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                // Setup the report viewer object and get the array of bytes
                ReportViewer viewer = new ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                //string reportPath = Server.MapPath(@"~\Reports\rptMRNLabels.rdlc");
                string reportPath = "~/Report/rptAtlasCopco_Barcode.rdlc";
                viewer.LocalReport.ReportPath = reportPath;

                ReportDataSource rds = new ReportDataSource("dsWMS", (DataTable)ws.tblLabelBarcode);
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.EnableExternalImages = true;

                ReportParameter[] param = new ReportParameter[3];

                string strPrintString = "";
                string CompanyName = ConfigurationManager.AppSettings["CompanyName"];
                string Address = ConfigurationManager.AppSettings["Address"];

                param[0] = new ReportParameter("CompanyName", CompanyName);
                param[1] = new ReportParameter("Address", Address);
                param[2] = new ReportParameter("PrintString", strPrintString);
                viewer.LocalReport.SetParameters(param);

                //string strPrintString = SelectMRPOrIndustrailuseValue == 3 ? ReportRes.ForIndustrialUseOnly : (SelectMRPOrIndustrailuseValue == 4 ? ReportRes.NotForRetailSale : string.Empty);
                //param[0] = new ReportParameter("PrintString", strPrintString);
                //viewer.LocalReport.SetParameters(param);
                // viewer.LocalReport.Refresh();

                string subreportPath = "~/Report/rptAtlasCopcoItemsBarcode.rdlc";
                using (System.IO.Stream report = System.IO.File.OpenRead(subreportPath))
                {
                    viewer.LocalReport.LoadSubreportDefinition("rptAtlasCopcoItemsBarcode", report);
                }
                viewer.LocalReport.SubreportProcessing +=
                   new Microsoft.Reporting.WinForms.SubreportProcessingEventHandler(LocalReportLabel_SubreportProcessing);

                byte[]bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);


                string FilePath = string.Format("{0}\\{1}", System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Reports_PDF");
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }
                string file_name = MRNNo + "_LabelPrint.pdf"; //save the file in unique name 

                if (System.IO.File.Exists(FilePath + "/" + file_name))
                    System.IO.File.Delete(FilePath + "/" + file_name);

                //After that use file stream to write from bytes to pdf file on your server path
                FileStream file = new FileStream(FilePath + "/" + file_name, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                file.Write(bytes, 0, bytes.Length);
                file.Dispose();

                //string filePath = FilePath + "/" + file_name;

                // Create Copy of SAME pdf ON Local Machine 

                //string sourcePath = FilePath;
                //string targetPath = @"C:/Reports_PDF";

                //// Use Path class to manipulate file and directory paths.
                //string sourceFile = System.IO.Path.Combine(sourcePath, file_name);
                //string destFile = System.IO.Path.Combine(targetPath, file_name);

                //// To copy a folder's contents to a new location:
                //// Create a new target folder, if necessary.
                //if (!System.IO.Directory.Exists(targetPath))
                //{
                //    System.IO.Directory.CreateDirectory(targetPath);
                //}

                //// To copy a file to another location and 
                //// overwrite the destination file if it already exists.
                //System.IO.File.Copy(sourceFile, destFile, true);
                //string NewFilePath = targetPath + "/" + file_name;
                string AdobeReaderExePath = @"C:\Program Files (x86)\Adobe\Acrobat Reader DC\Reader\AcroRd32.exe";
                string DirName = AppDomain.CurrentDomain.BaseDirectory;
                string dir1 = DirName + @"\Reports_PDF\";

                string[] dirs = Directory.GetFiles(dir1, "" + "*.*", SearchOption.AllDirectories);

                foreach (string dir in dirs)
                {
                    if (File.Exists(dir))
                    {
                        Process proc = new Process();
                        proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        proc.StartInfo.Verb = "print";
                        proc.StartInfo.FileName = AdobeReaderExePath;
                        proc.StartInfo.Arguments = String.Format(@"/p /h {0}", dir);
                        proc.StartInfo.UseShellExecute = false;
                        proc.StartInfo.CreateNoWindow = true;

                        proc.Start();
                        proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        if (proc.HasExited == false)
                        {
                            proc.WaitForExit(Convert.ToInt32(1000));
                        }

                        proc.EnableRaisingEvents = true;

                        proc.Close();
                        KillAdobe("AcroRd32");
                        File.Delete(dir);
                    }
                }
                string message = "Sticker are genrated";
                MessageBox.Show(message);
                clear();
                viewer.LocalReport.Refresh();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void clear()
        {
            txtCharLeftShift.Text = string.Empty;
            txtCharRightShift.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtNoOfStricker.Text = string.Empty;
        }

        public void PrintLabelForQRCode()
        {

            int clientid = Convert.ToInt32(cmbBoxClient.SelectedValue);
            string MRNNo = cmbBoxMRNNO.SelectedValue.ToString();

            int OrderId = Convert.ToInt32(cmbPONO.SelectedValue);
            int Itemid = Convert.ToInt32(cmbItem.SelectedValue);
            string Itemdesc = txtDescription.Text;
            int noOfStickers = Convert.ToInt32(txtNoOfStricker.Text);
            int barcodeCombinationid = 1;
            //int SelectMRPOrIndustrailuseValue;
            //int IFMRPIsZero;
            WMSData ws = new WMSData();
            WhsData WMSDs1 = new WhsData();
            DataTable dt = new DataTable();

            try
            {

                // _dal_MrpPrint;


                // var user = WMSWebSession.GetInstance().User;
                dt = _dal_MrpPrint.GetPreferenceById(clientid);
                string labelColumnId = dt.Rows[0][0].ToString();
                int[] labelColumnIds1 = new int[10];
                int a = 0;
                foreach (var i in labelColumnId.Split(','))
                {
                    labelColumnIds1[a] = Convert.ToInt32(i);
                    a++;
                }

                // Get picking strategy of Item 

                int ItemId = Convert.ToInt32(cmbItem.SelectedValue);
                var PickingStrategy = _dal_MrpPrint.GetPickingStrategyOfItem(ItemId);

                if (PickingStrategy == 1)
                {
                    barcodeCombinationid = 3;
                }
                else if (PickingStrategy == 2)
                {
                    barcodeCombinationid = 4;
                }

                string barcodeType = barcodeCombinationid == 1 ? "OnlyItemCode" : (barcodeCombinationid == 2 ? "ItemCodeMRNNo" : (barcodeCombinationid == 3 ? "ItemCodePONo" : (barcodeCombinationid == 4 ? "ItemCodeBatchNo" : "ItemCodeSerialNo")));

                //DataSet dsmrnItems = new DataSet(); 
                DataTable dtmrnItems = new DataTable();

                MrpModel.Client_Id = clientid;
                MrpModel.MRNNo = MRNNo.ToString();
                string itemCode = _dal_MrpPrint.GetItemsCode(Convert.ToInt32(cmbItem.SelectedValue));
                MrpModel.itemCode = itemCode.ToString().Trim();
                MrpModel.OrderId = OrderId;

                dtmrnItems = _dal_MrpPrint.GetItemDetailsForLablePrinting(MrpModel);

                if (dtmrnItems != null && dtmrnItems.Rows.Count > 0)
                {
                    int fid = 1;
                    for (int i = 0; i < dtmrnItems.Rows.Count; i++)
                    {
                        for (int Noofst = 0; Noofst < noOfStickers; Noofst++)
                        {
                            string directoryPath = string.Format("{0}\\{1}", System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Descripancy_Barcode");
                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }
                            var writer = new ZXing.BarcodeWriter();
                            writer.Options.PureBarcode = true;
                            writer.Format = BarcodeFormat.CODE_128;

                            string barcodeString = dtmrnItems.Rows[i]["ItemCode"].ToString();
                            if (barcodeCombinationid == 2)
                                barcodeString = barcodeString + "_" + MRNNo;
                            else if (barcodeCombinationid == 3)
                                barcodeString = barcodeString + "_" + dtmrnItems.Rows[i]["ItemCode"].ToString();
                            else if (barcodeCombinationid == 4)
                                barcodeString = barcodeString + "-" + dtmrnItems.Rows[i]["BatchNo"];
                            else if (barcodeCombinationid == 5)
                            {
                                barcodeString = barcodeString + "-" + "NA";
                            }

                            //QR code
                            string imgPath1 = @"~\Descripancy_Barcode\QR_" + MRNNo + "_" + barcodeString + ".jpg";
                            //  string FilePath = string.Format("{0}\\{1}", System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Reports_PDF");

                            var qrCodeWriter = new ZXing.BarcodeWriterPixelData
                            {
                                Format = ZXing.BarcodeFormat.QR_CODE,
                                Options = new QrCodeEncodingOptions
                                {
                                    Height = 100,
                                    Width = 100,
                                    Margin = 0
                                }
                            };
                            var pixelData = qrCodeWriter.Write(barcodeString);
                            using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
                            {
                                using (var ms = new MemoryStream())
                                {
                                    var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                                    try
                                    {
                                        // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image    
                                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                                    }
                                    finally
                                    {
                                        bitmap.UnlockBits(bitmapData);
                                    }
                                    using (FileStream fs = new FileStream(imgPath1, FileMode.Create, FileAccess.ReadWrite))
                                    {
                                        // save to stream as PNG    
                                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                        byte[] imgbytes = ms.ToArray();
                                        fs.Write(imgbytes, 0, imgbytes.Length);
                                    }

                                }
                            }

                            string path = string.Format("{0}\\{1}", System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), imgPath1);
                            path = "File:///" + path;


                            ws.tblLabelBarcode.Rows.Add(path, fid);
                            //MRN Number, PO Number, SO Number, Item Code, Description, Batch Number, Exp. Date, Mfg. Date, MRP, Quantity
                            //if (labelColumnIds.Contains(1))
                            ws.tblLabelPrinting.Rows.Add("Description", dtmrnItems.Rows[i]["ItemDescription"], fid);
                            //if (labelColumnIds.Contains(2))
                            ws.tblLabelPrinting.Rows.Add("ItemCode", dtmrnItems.Rows[i]["ClientItemDesc"], fid);
                            if (labelColumnIds1.Contains(2))
                                ws.tblLabelPrinting.Rows.Add("PONo", "PO No.: " + dtmrnItems.Rows[i]["PONumber"], fid);
                            if (labelColumnIds1.Contains(3))
                                ws.tblLabelPrinting.Rows.Add("SONo", "SO No.: " + dtmrnItems.Rows[i]["SONumber"], fid);
                            if (labelColumnIds1.Contains(1))
                                ws.tblLabelPrinting.Rows.Add("MRNNo", "MRN No.: " + dtmrnItems.Rows[i]["MRNNo"], fid);
                            if (labelColumnIds1.Contains(6))
                                ws.tblLabelPrinting.Rows.Add("BatchNo", "Batch No.: " + dtmrnItems.Rows[i]["BatchNo"], fid);
                            if (labelColumnIds1.Contains(11))
                            {
                                //if (mrnItem.SerialNo != null && qty < mrnItem.SerialNo.Length)
                                //    wmsDs.tblLabelPrinting.Rows.Add(@ReportRes.SerialNo, mrnItem.SerialNo[qty], fid);
                                //else
                                //  wmsDs.tblLabelPrinting.Rows.Add(@ReportRes.SerialNo, "NA", fid);
                            }
                            if (labelColumnIds1.Contains(7))
                                ws.tblLabelPrinting.Rows.Add("ExpDate", "Expiry Date: " + dtmrnItems.Rows[i]["ExpiryDate"], fid);
                            if (labelColumnIds1.Contains(8))
                                ws.tblLabelPrinting.Rows.Add("MfgDate", "Mfg. Date: " + dtmrnItems.Rows[i]["ProductionDate"], fid);
                            //if ((labelColumnIds.Contains(9) && SelectMRPOrIndustrailuseValue != 2) || SelectMRPOrIndustrailuseValue == 1)
                            //    if (mrnItem.MRP == 0)
                            //    {
                            //        if (IFMRPIsZero == 2)
                            //        {
                            //            SelectMRPOrIndustrailuseValue = 3;
                            //            // wmsDs.tblLabelPrinting.Rows.Add(@ReportRes.MRP, "For Industrail use only", fid);
                            //        }
                            //        else if (IFMRPIsZero == 3)
                            //        {
                            //            SelectMRPOrIndustrailuseValue = 4;
                            //            // wmsDs.tblLabelPrinting.Rows.Add(@ReportRes.MRP, "Not for Retail Sale", fid);
                            //        }

                            //    }
                            //    else
                            //    {
                            //        wmsDs.tblLabelPrinting.Rows.Add(@ReportRes.MRP, mrnItem.MRP, fid);
                            //    }

                            if (labelColumnIds1.Contains(10))
                                ws.tblLabelPrinting.Rows.Add("Quantity", "Unit Qty.: " + dtmrnItems.Rows[i]["UnitQuantity"] + " " + dtmrnItems.Rows[i]["UOM_Name"], fid);
                            if (dtmrnItems.Rows[i]["MRPValue"]!= null)
                            {
                                if (dtmrnItems.Rows[i]["MRPValue"] == "I")
                                {
                                    ws.tblLabelPrinting.Rows.Add("MRP", "Industrial", fid);
                                }
                                else if(dtmrnItems.Rows[i]["MRPValue"] == "N")
                                {
                                    ws.tblLabelPrinting.Rows.Add("MRP", "Not For sale", fid);
                                }
                            }
                            // WMSDs1.tblLabelPrinting1.Rows.Add("ItemCode", "Item No.: " + dtmrnItems.Rows[i]["ItemCode1"], fid);
                            fid = fid + 1;
                        }
                        //}
                    }

                    MRNLabelItem = ws.tblLabelPrinting;
                    // MRNLabelItem1 = WMSDs1.tblLabelPrinting1;
                }


                // Variables
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                // Setup the report viewer object and get the array of bytes
                ReportViewer viewer = new ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;
                //string reportPath = Server.MapPath(@"~\Reports\rptMRNLabels.rdlc");
                string reportPath = "~/Report/rptAltasCopco_QR.rdlc";
                viewer.LocalReport.ReportPath = reportPath;

                ReportDataSource rds = new ReportDataSource("dsWMS", (DataTable)ws.tblLabelBarcode);
                viewer.LocalReport.DataSources.Clear();
                viewer.LocalReport.DataSources.Add(rds);
                viewer.LocalReport.EnableExternalImages = true;

                ReportParameter[] param = new ReportParameter[3];
                string strPrintString = "ForIndustrialUseOnly";
                string CompanyName = ConfigurationManager.AppSettings["CompanyName"];
                string Address = ConfigurationManager.AppSettings["Address"];

                param[0] = new ReportParameter("CompanyName", CompanyName);
                param[1] = new ReportParameter("Address", Address);
                param[2] = new ReportParameter("PrintString", strPrintString);
                viewer.LocalReport.SetParameters(param);

                // viewer.LocalReport.Refresh();
                // string strPrintString = SelectMRPOrIndustrailuseValue == 3 ? ReportRes.ForIndustrialUseOnly : (SelectMRPOrIndustrailuseValue == 4 ? ReportRes.NotForRetailSale : string.Empty);
                //param[0] = new ReportParameter("PrintString", strPrintString);
                //viewer.LocalReport.SetParameters(param);

                // viewer.LocalReport.Refresh();


                //using (System.IO.Stream report = System.IO.File.OpenRead(Server.MapPath(@"~\Reports\rptMRNLabelItems.rdlc")))
                string subreportPath = "~/Report/rptAtlasCopcoItems.rdlc";
                using (System.IO.Stream report = System.IO.File.OpenRead(subreportPath))
                {
                    viewer.LocalReport.LoadSubreportDefinition("rptAtlasCopcoItemsBarcode", report);
                }
                viewer.LocalReport.SubreportProcessing +=
                   new Microsoft.Reporting.WinForms.SubreportProcessingEventHandler(LocalReportLabel_SubreportProcessing);

                //using (System.IO.Stream report = System.IO.File.OpenRead("~/Report/rptAtlasCopco_Barcode.rdlc"))
                //{
                //    viewer.LocalReport.LoadSubreportDefinition("rptAtlasCopco_Barcode", report);
                //}
                //viewer.LocalReport.SubreportProcessing += new Microsoft.Reporting.WinForms.SubreportProcessingEventHandler(LocalReportLabel_SubreportProcessing1);
                //   string deviceInfo = string.Format("<DeviceInfo><PageHeight>{0}</PageHeight><PageWidth>{1}</PageWidth></DeviceInfo>", "7.6cm", "9.0cm");

                //   viewer.RefreshReport();


                byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);



                //Server.MapPath("~/App_Data/
                // string path1 = "~/Reports_PDF";



                string FilePath = string.Format("{0}\\{1}", System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Reports_PDF");
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }
                string file_name = MRNNo + "_LabelWithQRPrint.pdf"; //save the file in unique name 

                if (System.IO.File.Exists(FilePath + "/" + file_name))
                    System.IO.File.Delete(FilePath + "/" + file_name);

                //if (System.IO.File.Exists(path1 + "/" + file_name))
                //    System.IO.File.Delete(path1 + "/" + file_name);

                //After that use file stream to write from bytes to pdf file on your server path


                FileStream file = new FileStream(FilePath + "/" + file_name, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                file.Write(bytes, 0, bytes.Length);
                file.Dispose();

                // string filePath = path1 + "/" + file_name;

                // Create Copy of SAME pdf ON Local Machine 

                //string sourcePath = FilePath;
                //string targetPath = @"C:/Reports_PDF";

                //// Use Path class to manipulate file and directory paths.
                //string sourceFile = System.IO.Path.Combine(sourcePath, file_name);
                //string destFile = System.IO.Path.Combine(targetPath, file_name);

                //// To copy a folder's contents to a new location:
                //// Create a new target folder, if necessary.
                //if (!System.IO.Directory.Exists(targetPath))
                //{
                //    System.IO.Directory.CreateDirectory(targetPath);
                //}

                //// To copy a file to another location and 
                //// overwrite the destination file if it already exists.
                //System.IO.File.Copy(sourceFile, destFile, true);
                //string NewFilePath = targetPath + "/" + file_name;

                string AdobeReaderExePath = @"C:\Program Files (x86)\Adobe\Acrobat Reader DC\Reader\AcroRd32.exe";
                string DirName = AppDomain.CurrentDomain.BaseDirectory;
                string dir1 = DirName + @"\Reports_PDF\";
                string[] dirs = Directory.GetFiles(dir1, "" + "*.*", SearchOption.AllDirectories);

                foreach (string dir in dirs)
                {
                    if (File.Exists(dir))
                    {
                        Process proc = new Process();
                        proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        proc.StartInfo.Verb = "print";
                        proc.StartInfo.FileName = AdobeReaderExePath;
                        proc.StartInfo.Arguments = String.Format(@"/p /h {0}", dir);
                        proc.StartInfo.UseShellExecute = false;
                        proc.StartInfo.CreateNoWindow = true;

                        proc.Start();
                        proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        if (proc.HasExited == false)
                        {
                            proc.WaitForExit(Convert.ToInt32(1000));
                        }

                        proc.EnableRaisingEvents = true;

                        proc.Close();
                        KillAdobe("AcroRd32");
                        File.Delete(dir);
                    }
                }
                string message = "Sticker are genrated";
                MessageBox.Show(message);
                viewer.LocalReport.Refresh();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private static bool KillAdobe(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses().Where(
                         clsProcess => clsProcess.ProcessName.StartsWith(name)))
            {
                clsProcess.Kill();
                return true;
            }
            return false;
        }


        // code for Scanning on click enter

        private void txtDescription_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)Keys.Return)
            //{
            //    // the enter key was pressed
            //    string Itemcode = _dal_MrpPrint.GetItemsCode(Convert.ToInt32(cmbItem.SelectedValue));
            //    if (txtDescription.Text.Trim() == Itemcode.ToString().Trim())
            //    {
            //        if (rdbtnBarcode.Checked == true)
            //        {
            //            PrintBarcode();
            //        }
            //        else
            //        {
            //            //Qr code
            //            PrintLabelForQRCode();
            //        }
            //    }
            //}
        }


        private void txtDescription_KeyUp(object sender, KeyEventArgs e)
        {

        }



    }
}
