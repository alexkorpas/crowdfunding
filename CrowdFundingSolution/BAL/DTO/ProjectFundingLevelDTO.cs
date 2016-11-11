using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTO
{
    public class ProjectFundingLevelDTO
    {

        public int Funding_Level_Id { get; set; }
        public int Project_Id { get; set; }
        public string Title { get; set; }
        public int Amount { get; set; }
        public int Rewards { get; set; }
        public bool Is_Active { get; set; }


    }
}
