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
using ZXing.Common;

namespace WMS_Desktop
{
    public partial class MRPLabelPrint : Form
    {
        DAL_MRPLabelPrint _dal_MrpPrint;
        MRP_LabelPrint_Dal _MRP_LabelPrint_Dal;
        DataTable MRNLabelItem = new DataTable();
        public MRPLabelPrint()
        {
            InitializeComponent();
            _dal_MrpPrint = new DAL_MRPLabelPrint();
            _MRP_LabelPrint_Dal = new MRP_LabelPrint_Dal();
            GetClientList();
            UOMList();
            CurrencyList();
        }

        public void GetClientList()
        {
            DataTable dt = _dal_MrpPrint.GetClientList();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "-- Select Client --", "-- Select Client --" };
                dt.Rows.InsertAt(dr, 0);
                cmbClient.DisplayMember = "Name";
                cmbClient.ValueMember = "Id";
                cmbClient.DataSource = dt;
            }
        }

        private void cmbClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = _MRP_LabelPrint_Dal.GetItemList(Convert.ToInt32(cmbClient.SelectedValue));
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                cmbItem.DisplayMember = "ItemCode";
                cmbItem.ValueMember = "Id";
                cmbItem.DataSource = dt;
            }
        }

        private void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = _MRP_LabelPrint_Dal.GetItemListbyId(Convert.ToInt32(cmbItem.SelectedValue), Convert.ToInt32(cmbClient.SelectedValue));
            if (dt != null && dt.Rows.Count > 0)
            {
                txtItemDesc.Text = dt.Rows[0]["ItemDescription"].ToString();


                if (Convert.ToDouble(dt.Rows[0]["MRP"]) == 0.0)
                {
                    if (dt.Rows[0]["MRPValue"] != null || dt.Rows[0]["MRPValue"] != " ")
                    {
                        if (dt.Rows[0]["MRPValue"] == "I")
                        {
                            txtMrp.Text = "Industrial";
                        }
                        else if (dt.Rows[0]["MRPValue"] == "N")
                        {
                            txtMrp.Text = "Not For sale";
                        }
                        else 
                        {
                            txtMrp.Text = "0.0";
                        }
                    }
                }
                else
                {
                  txtMrp.Text=(Convert.ToDouble(dt.Rows[0]["MRP"]).ToString());
                }

            }
        }

        public void UOMList()
        {
            DataTable dt = _MRP_LabelPrint_Dal.GetUOMList();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                cmbUOM.DisplayMember = "Unit";
                cmbUOM.ValueMember = "Id";
                cmbUOM.DataSource = dt;
            }
        }

        public void CurrencyList()
        {
            DataTable dt = _MRP_LabelPrint_Dal.GetCurrencyList();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                cmbCurrency.DisplayMember = "CurrencyName";
                cmbCurrency.ValueMember = "Id";
                cmbCurrency.DataSource = dt;
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

        private void BtnPrintLable_Click(object sender, EventArgs e)
        {
            if (rdbtnBarcode.Checked == true)
            {
                MRPPrintLabels();
            }
            else
            {
                //Qr code
                MRPPrintQRLabels();
            }
            

        }

        public void MRPPrintLabels()
        {
            int clientid = Convert.ToInt32(cmbClient.SelectedValue);
            int Itemid = Convert.ToInt32(cmbItem.SelectedValue);
            string Itemdesc = txtItemDesc.Text;
            int noOfStickers = Convert.ToInt32(txtNoOfStrickers.Text);
            string Itemcode = _dal_MrpPrint.GetItemsCode(Convert.ToInt32(cmbItem.SelectedValue));
            int stockKeepingqty = Convert.ToInt32(txtStockQty.Text);
            string StockkeepingUnit = _MRP_LabelPrint_Dal.GetUOMUnit(Convert.ToInt32(cmbUOM.SelectedValue));
            string Mrp = txtMrp.Text.ToString();
            
            int barcodeCombinationid = 1;

            //int SelectMRPOrIndustrailuseValue;
            //int IFMRPIsZero;
            WMSData ws = new WMSData();
            WhsData WMSDs1 = new WhsData();
            DataTable dt = new DataTable();
           
            try
            {
                int fid = 1;
                for (int Noofst = 0; Noofst < noOfStickers; Noofst++)
                {
                    string directoryPath = string.Format("{0}\\{1}", System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Descripancy_Barcode");
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory((directoryPath));
                    }
                    var writer = new ZXing.BarcodeWriter
                    {
                        Format = BarcodeFormat.CODE_128,
                        Options = new EncodingOptions
                        {
                            PureBarcode = true,
                            Height = 100,
                            Width = 300
                        }
                    };

                    string barcodeString = Itemdesc;

                    var result = writer.Write(barcodeString);
                    string imgPath = @"~\Descripancy_Barcode\" + clientid + "_" + Itemdesc + ".jpg";
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
                    string path = string.Format("{0}\\{1}", System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), imgPath);
                    path = "File:///" + path;
                    //string path = "File:///" + imgPath;
    
                    ws.tblLabelBarcode.Rows.Add(path, fid);


                    ws.tblLabelPrinting.Rows.Add("ItemCode", "Item No.: " + Itemcode, fid);
                    string desc = string.IsNullOrEmpty(Itemdesc) ? string.Empty : (Itemdesc.Length > 35 ? Itemdesc.Substring(0, 35) : Itemdesc);
                    ws.tblLabelPrinting.Rows.Add("Description","Item Name: " + desc, fid);
                    ws.tblLabelPrinting.Rows.Add("Quantity: " + stockKeepingqty + " " + StockkeepingUnit, fid);

                    if (Convert.ToDouble(Mrp) == 0.0 || Convert.ToDouble(Mrp)== 0)
                    {
                        string message = txtMrp.Text;
                        ws.tblLabelPrinting.Rows.Add("Quantity", message, fid);
                    }
                    else 
                    {
                        ws.tblLabelPrinting.Rows.Add("MRP", "MRP (Rs.): " + Convert.ToDouble(Mrp) * stockKeepingqty + " ( Inclusive of all taxes )", fid);
                    }

                    string currentMonthYear = DateTime.Now.ToString("MMM-yyyy");
                    ws.tblLabelPrinting.Rows.Add("Quantity", "Month & Year of Packing: " + currentMonthYear, fid);

                    fid = fid + 1;
                }

                MRNLabelItem = ws.tblLabelPrinting;

                // Variables
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;


                // Setup the report viewer object and get the array of bytes
                ReportViewer viewer = new ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;

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

                viewer.LocalReport.Refresh();

                string subreportPath = "~/Report/rptAtlasCopcoItemsBarcode.rdlc";
                using (System.IO.Stream report = System.IO.File.OpenRead(subreportPath))
                {
                    viewer.LocalReport.LoadSubreportDefinition("rptAtlasCopcoItemsBarcode", report);
                }
                viewer.LocalReport.SubreportProcessing +=
                     new Microsoft.Reporting.WinForms.SubreportProcessingEventHandler(LocalReportLabel_SubreportProcessing);

                byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                string FilePath = string.Format("{0}\\{1}", System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Reports_PDF");
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }

               // string path1 = Server.MapPath("~/Reports_PDF");
                string file_name = Itemcode + "_LabelPrint.pdf";
                if (System.IO.File.Exists(FilePath + "/" + file_name))
                    System.IO.File.Delete(FilePath + "/" + file_name);

                //After that use file stream to write from bytes to pdf file on your server path
                FileStream file = new FileStream(FilePath + "/" + file_name, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                file.Write(bytes, 0, bytes.Length);
                file.Dispose();

                string filePath = FilePath + "/" + file_name;
                // Create Copy of SAME pdf ON Local Machine 

                string sourcePath = FilePath;
                string targetPath = @"C:/Reports_PDF";

                // Use Path class to manipulate file and directory paths.
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
                            proc.WaitForExit(Convert.ToInt32(10000));
                        }

                        proc.EnableRaisingEvents = true;

                        proc.Close();
                        KillAdobe("AcroRd32");
                        File.Delete(dir);
                    }
                }
                string message1 = "Sticker are genrated";
                MessageBox.Show(message1);
              //  clear();
                viewer.LocalReport.Refresh();

                
            }
            catch (Exception ex)
            {
                // _labelprintingService.SaveLog(ex.Message);
              MessageBox.Show(ex.Message);
              
            }
        }

        public void MRPPrintQRLabels()
        {
            int clientid = Convert.ToInt32(cmbClient.SelectedValue);
            int Itemid = Convert.ToInt32(cmbItem.SelectedValue);
            string Itemdesc = txtItemDesc.Text;
            int noOfStickers = Convert.ToInt32(txtNoOfStrickers.Text);
            string Itemcode = _dal_MrpPrint.GetItemsCode(Convert.ToInt32(cmbItem.SelectedValue));
            int stockKeepingqty = Convert.ToInt32(txtStockQty.Text);
            string StockkeepingUnit = _MRP_LabelPrint_Dal.GetUOMUnit(Convert.ToInt32(cmbUOM.SelectedValue));
            int barcodeCombinationid = 1;
            string Mrp = txtMrp.Text.ToString();
            //int SelectMRPOrIndustrailuseValue;
            //int IFMRPIsZero;
            WMSData ws = new WMSData();
            WhsData WMSDs1 = new WhsData();
            DataTable dt = new DataTable();

            try
            {
                int fid = 1;
                for (int Noofst = 0; Noofst < noOfStickers; Noofst++)
                {
                    string directoryPath = string.Format("{0}\\{1}", System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Descripancy_Barcode");
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory((directoryPath));
                    }
                    var writer = new ZXing.BarcodeWriter();
                    writer.Format = BarcodeFormat.CODE_128;
                    string barcodeString = Itemdesc;

                    string imgPath1 = @"~\Descripancy_Barcode\QR_" + clientid + "_" + Itemcode + ".jpg";
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


                    ws.tblLabelPrinting.Rows.Add("ItemCode", "Item No.: " + Itemcode, fid);
                    string desc = string.IsNullOrEmpty(Itemdesc) ? string.Empty : (Itemdesc.Length > 35 ? Itemdesc.Substring(0, 35) : Itemdesc);
                    ws.tblLabelPrinting.Rows.Add("Description", "Item Name: " + desc, fid);
                    ws.tblLabelPrinting.Rows.Add("Quantity: " + stockKeepingqty + " " + StockkeepingUnit, fid);
                   
                    if (Convert.ToDouble(Mrp) == 0.0 || Convert.ToDouble(Mrp) == 0)
                    {
                        string msg = txtMrp.Text;
                        ws.tblLabelPrinting.Rows.Add("Quantity", msg, fid);
                    }
                    else
                    {
                        ws.tblLabelPrinting.Rows.Add("MRP", "MRP (Rs.): " + Convert.ToDouble(Mrp) * stockKeepingqty + " ( Inclusive of all taxes )", fid);
                    }



                    //if (SelectedMRPString == 1)
                    //    if (MRP == 0)
                    //    {
                    //        if (IfMRPIsZeroValue == 2)
                    //        {
                    //            SelectedMRPString = 3;
                    //        }
                    //        else if (IfMRPIsZeroValue == 3)
                    //        {
                    //            SelectedMRPString = 4;
                    //        }
                    //        else if (IfMRPIsZeroValue == 1)
                    //        {
                    //            SelectedMRPString = 2;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        wmsDs.tblLabelPrinting.Rows.Add(@ReportRes.MRP, "MRP (Rs.): " + MRP * StockKeepingqty + " ( Inclusive of all taxes )", fid);
                    //        // wmsDs.tblLabelPrinting.Rows.Add(@ReportRes.MRP, "MRP (" + "" + " ): " + MRP * StockKeepingqty + " ( Inclusive of all taxes )", fid);
                    //    }

                    //if (SelectedMRPString == 2)
                    //    wmsDs.tblLabelPrinting.Rows.Add(@ReportRes.Quantity, string.Empty, fid);
                    //if (SelectedMRPString == 3)
                    //    wmsDs.tblLabelPrinting.Rows.Add(@ReportRes.Quantity, ReportRes.ForIndustrialUseOnly, fid);
                    //if (SelectedMRPString == 4)
                    //    wmsDs.tblLabelPrinting.Rows.Add(@ReportRes.Quantity, ReportRes.NotForRetailSale, fid);

                    string currentMonthYear = DateTime.Now.ToString("MMM-yyyy");
                    ws.tblLabelPrinting.Rows.Add("Quantity", "Month & Year of Packing: " + currentMonthYear, fid);

                    fid = fid + 1;
                }

                MRNLabelItem = ws.tblLabelPrinting;

                // Variables
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;


                // Setup the report viewer object and get the array of bytes
                ReportViewer viewer = new ReportViewer();
                viewer.ProcessingMode = ProcessingMode.Local;

                string reportPath = "~/Report/rptAltasCopco_QR.rdlc";
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

                viewer.LocalReport.Refresh();

                string subreportPath = "~/Report/rptAtlasCopcoItems.rdlc";
                using (System.IO.Stream report = System.IO.File.OpenRead(subreportPath))
                {
                    viewer.LocalReport.LoadSubreportDefinition("rptAtlasCopcoItems", report);
                }
                viewer.LocalReport.SubreportProcessing +=
                     new Microsoft.Reporting.WinForms.SubreportProcessingEventHandler(LocalReportLabel_SubreportProcessing);

                byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                string FilePath = string.Format("{0}\\{1}", System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Reports_PDF");
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }

                // string path1 = Server.MapPath("~/Reports_PDF");
                string file_name = Itemcode + "_LabelWithQRPrint.pdf";
                if (System.IO.File.Exists(FilePath + "/" + file_name))
                    System.IO.File.Delete(FilePath + "/" + file_name);

                //After that use file stream to write from bytes to pdf file on your server path
                FileStream file = new FileStream(FilePath + "/" + file_name, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                file.Write(bytes, 0, bytes.Length);
                file.Dispose();

                string filePath = FilePath + "/" + file_name;
                // Create Copy of SAME pdf ON Local Machine 

                string sourcePath = FilePath;
                string targetPath = @"C:/Reports_PDF";

                // Use Path class to manipulate file and directory paths.
                string sourceFile = System.IO.Path.Combine(sourcePath, file_name);
                string destFile = System.IO.Path.Combine(targetPath, file_name);

                // To copy a folder's contents to a new location:
                // Create a new target folder, if necessary.
                if (!System.IO.Directory.Exists(targetPath))
                {
                    System.IO.Directory.CreateDirectory(targetPath);
                }

                // To copy a file to another location and 
                // overwrite the destination file if it already exists.
                System.IO.File.Copy(sourceFile, destFile, true);
                string NewFilePath = targetPath + "/" + file_name;
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
                            proc.WaitForExit(Convert.ToInt32(10000));
                        }

                        proc.EnableRaisingEvents = true;

                        proc.Close();
                        KillAdobe("AcroRd32");
                        File.Delete(dir);
                    }
                }
                string message = "Sticker are genrated";
                MessageBox.Show(message);
               // clear();
                viewer.LocalReport.Refresh();


            }
            catch (Exception ex)
            {
                // _labelprintingService.SaveLog(ex.Message);
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

        public void clear() 
        {
            txtItemDesc.Text=string.Empty;
            txtMrp.Text=string.Empty;
            txtNoOfStrickers.Text=string.Empty;
            txtStockQty.Text=string.Empty;

        }
    }
}
