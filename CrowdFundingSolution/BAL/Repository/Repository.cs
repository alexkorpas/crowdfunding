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
        protected CrowdFundingVivaTeam1Entities context;

        public CrowdFundingTransactions() { context = new CrowdFundingVivaTeam1Entities(); }

        public void Dispose() { context.Dispose(); }
    }
}
