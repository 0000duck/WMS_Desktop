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
    public class MRP_LabelPrint_Dal
    {
        private dbConnection conn;


        public MRP_LabelPrint_Dal()
        {
            conn = new dbConnection();
        }

        public DataTable GetItemList(int Client_Id)
        {
            string query = string.Format("SpGetitemListClientWise");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ClientId", SqlDbType.Int);
            sqlParameters[0].Value = Client_Id;
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetUOMList()
        {
            string query = string.Format("SpGetUOMList");
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetCurrencyList()
        {
            string query = string.Format("SpGetCurrencyList");
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetItemListbyId(int ItemId,int ClientId)
        {
            string query = string.Format("spGetItemById");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@ItemId", SqlDbType.Int);
            sqlParameters[0].Value = ItemId;
            sqlParameters[1] = new SqlParameter("@ClientId", SqlDbType.Int);
            sqlParameters[1].Value = ClientId;
            return conn.executeSelectQuery(query, sqlParameters);
        }


        public string GetUOMUnit(int UOMId)
        {
            string Unitname = string.Empty;
            DataTable dt = new DataTable();
            string query = string.Format("spUomNamebyId");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@UOMId", SqlDbType.Int);
            sqlParameters[0].Value = UOMId;

            dt = conn.executeSelectQuery(query, sqlParameters);
            if (dt.Rows.Count > 0)
            {
                Unitname = (dt.Rows[0]["Unit"].ToString());
            }
            return Unitname;
        }



    }
}
