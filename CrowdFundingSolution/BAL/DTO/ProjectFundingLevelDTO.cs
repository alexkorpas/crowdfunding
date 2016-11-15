using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class ProjectFundingLevelDTO
    {

        public int Id { get; set; }
        public int ProjectFK { get; set; }
        public string Title { get; set; }
        public int Amount { get; set; }
        public int Rewards { get; set; }
        public bool IsActive { get; set; }


    }
}
