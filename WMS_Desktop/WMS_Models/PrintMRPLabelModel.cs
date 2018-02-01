using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WMS_Models
{
    class PrintMRPLabelModel
    {
        public int Client_Id { get; set; }
        public String ClientName { get; set; }
        public int OrderId { get; set; }
        public string MRNNo { get; set; }
        public string PONumber { get; set; }
        public int ItemId { get; set; }
        public string itemCode { get; set; }
        public int NoOfStricker { get; set; }
        public string CurrencyName { get; set; }
        public int CurrencyId  { get; set; }
        public int UOM { get; set; }
        public string UOMName { get; set; }
    }
}
