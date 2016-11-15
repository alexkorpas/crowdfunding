using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class PaymentDTO
    {

        public int Payment_Id { get; set; }
        public int Project_Id { get; set; }
        public int User_Id { get; set; }
        public decimal Amount { get; set; }
        public int Rewards { get; set; }
        public DateTime Payment_Date { get; set; }
        public string Payment_Method { get; set; }
        public decimal Refunded_Amount { get; set; }
        public DateTime Refunded_Date{ get; set; }
    }
}
