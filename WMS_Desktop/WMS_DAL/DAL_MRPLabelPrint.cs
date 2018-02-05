using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WMS_Core;
using System.Data.SqlClient;
using WMS_Models;


namespace WMS_DAL
{
    public class DAL_MRPLabelPrint
    {

        private dbConnection conn;

        public DAL_MRPLabelPrint()
        {
            conn = new dbConnection();
        }

        public DataTable GetClientList()
        {
            string query = string.Format("SpGetClientList");
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetClientMRNNO( int Client_Id)
        {
            string query = string.Format("SpGetMRNNO");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ClientId", Client_Id);
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetPONO(string MRNNo)
        {
            string query = string.Format("SpGetPONo");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@MRNNo", MRNNo);
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public string GetItemsCode(int ItemId)
        {
            string ItemCode = string.Empty;
            DataTable dt = new DataTable();
            string query = string.Format("spGetItemsCode");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ItemId", ItemId);
            dt= conn.executeSelectQuery(query, sqlParameters);
            if (dt.Rows.Count > 0)
            {
                ItemCode = (dt.Rows[0]["ItemCode"].ToString());
            }

            return ItemCode;
        }

        public DataTable GetItems(int OrderId)
        {
            string query = string.Format("spGetItems");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@OrderId", OrderId);
            return conn.executeSelectQuery(query, sqlParameters);
        }


        public DataTable GetPreferenceById(int clientId)
        {
            string query = string.Format("SpGetPreferenceById");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ClientId", clientId);
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public int GetPickingStrategyOfItem(int ItemId)
        {
            int result = 0;
            DataTable dt = new DataTable();
            string query = string.Format("spGetPickingStrategyOfItem");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ItemId", ItemId);
           dt = conn.executeSelectQuery(query, sqlParameters);

           if (dt.Rows.Count > 0)
           {
               int PickingStrategyId = Convert.ToInt32(dt.Rows[0]["PickingStrategyId"]);
               int PickingStrategyById = Convert.ToInt32(dt.Rows[0]["PickingStrategyById"]);

               if (PickingStrategyById == 2)
               {
                   result = 1; // Picking Strategy is FIFO +PO
               }
               else if (PickingStrategyById == 4)
               {
                   result = 2; // Picking Strategy is FIFO + Batch
               }
               //else if (PickingStrategyById == 1)
               //{
               //    //serial
               //}
               else
               {
                   result = 0;
               }

           }
           return result;
        }


        public DataTable GetItemDetailsForLablePrinting(MRPPrintModel m)
        {
            string query = string.Format("SpGetItemDetailsForLablePrinting");
            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@ClientId", m.Client_Id);
            sqlParameters[1] = new SqlParameter("@MRNNo", m.MRNNo);
            sqlParameters[2] = new SqlParameter("@itemCode", m.itemCode);
            sqlParameters[3] = new SqlParameter("@OrderId", m.OrderId);
            return conn.executeSelectQuery(query, sqlParameters);
        }
        
    }


}
