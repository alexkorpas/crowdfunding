﻿using System;

namespace BAL
{
    public class TransactionCriteria
    {
        public int? Id { get; set; }
        public int? ProjectId { get; set; }
        public string UserId { get; set; }
        public int? StateId { get; set; }
        public int? CategoryId { get; set; }
        public string Search { get; set; }
        public int? Page { get; set; }
        public int? NewestProject { get; set; }
        public DateTime? AfterDate { get; set; }
        public int? TrendingProjects { get; set; }

    }//End Class
}//End Namespace
