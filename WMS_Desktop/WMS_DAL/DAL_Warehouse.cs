using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_Core;

namespace WMS_DAL
{
    public class DAL_Warehouse
    {
        private dbConnection conn;

        /// <constructor>
        /// Constructor
        /// </constructor>
        public DAL_Warehouse()
        {
            conn = new dbConnection();
        }

        /// <method>
        /// Get list of warehouse
        /// </method>
        public DataTable GetWarehouseList()
        {
            string query = string.Format("GetWarehouseList");
            SqlParameter[] sqlParameters = new SqlParameter[0];

            return conn.executeSelectQuery(query, sqlParameters);
        }

    }
}
