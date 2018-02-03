using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_Core;
using System.Data.SqlClient;
using WMS_Models;
using System.Data;


namespace WMS_DAL
{
   public class PackinglipManual_dal
    {
        private dbConnection conn;
        public PackinglipManual_dal()
        {
            conn = new dbConnection();
        }
        public int GetClientId(string UserId)
        {
            int ClientId = 0;
            DataTable dt = new DataTable();
            string query = string.Format("spClientIdbyUserId");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@UserId", SqlDbType.VarChar);
            sqlParameters[0].Value = UserId;
            dt = conn.executeSelectQuery(query, sqlParameters);
            if (dt.Rows.Count > 0)
            {
                ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
            }
            return ClientId;
        }

        public DataTable GetClientList(int ClientId)
        {
            string query = string.Format("spGetClientById");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ClientId", SqlDbType.Int);
            sqlParameters[0].Value = ClientId;
            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetShitToIds(int ClientId)
        {
            string query = string.Format("spGetShiftToId");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ClientId", SqlDbType.Int);
            sqlParameters[0].Value = ClientId;
            return conn.executeSelectQuery(query, sqlParameters);
        }
    }
}
