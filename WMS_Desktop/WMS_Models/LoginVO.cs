using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS_Models
{
    public class LoginVO
    {
        public LoginVO()
        {

        }
        public string UserId { get; set; }
        public int WarehouseId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
