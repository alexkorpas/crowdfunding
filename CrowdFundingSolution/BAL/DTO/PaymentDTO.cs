﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class PaymentDTO
    {

        public int Id { get; set; }
        public int ProjectFK { get; set; }
        public string UserFK { get; set; }
        public decimal Amount { get; set; }
        public string Rewards { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public decimal? RefundedAmount { get; set; }
        public DateTime? RefundedDate{ get; set; }
        public string ProjectTitle { get; set; }
        public decimal? ProjectGathered { get; set; }
        public decimal ProjectGoal { get; set; }
        public string TransactionId { get; set; }

    }
}
