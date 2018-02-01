using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS_Core;
using WMS_Models;

namespace WMS_DAL
{
   public class DAL_Login
    {

         private dbConnection conn;
         private CommonFunctions commonFunctions;
        /// <constructor>
        /// Constructor
        /// </constructor>
        public DAL_Login()
        {
            conn = new dbConnection();
            commonFunctions = new CommonFunctions();
        }

        /// <method>
        /// Get User By username and password
        /// </method>
        public DataTable GetLoginDetails(LoginVO loginVO)
        {
            string query = string.Format("spGetUserDetailsForLogin");
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@WarehouseId", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(loginVO.WarehouseId);
            sqlParameters[1] = new SqlParameter("@Username", SqlDbType.NVarChar);
            sqlParameters[1].Value = Convert.ToString(loginVO.Username);
            sqlParameters[2] = new SqlParameter("@Password", SqlDbType.NVarChar);
            sqlParameters[2].Value = Convert.ToString(commonFunctions.EncryptPassword(loginVO.Password));
            return conn.executeSelectQuery(query, sqlParameters);
        }

    }
}
