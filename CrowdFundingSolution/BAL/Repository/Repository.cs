using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
   public partial class CrowdFundingTransactions : IDisposable
    {
        protected backup_CrowdFundingViva1Entities context;

        public CrowdFundingTransactions() { context = new backup_CrowdFundingViva1Entities(); }

        public void Dispose() { context.Dispose(); }
    }
}
