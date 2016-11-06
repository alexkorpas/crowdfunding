using System.Collections.Generic;
using System.Linq;
using DAL;

namespace BAL
{
    public partial class CrowdFundingTransactions
    {
        /// <summary>
        /// Returns a list of all available Projects
        /// </summary>
        /// <returns>List<ProjectDTO></returns>
        public List<ProjectDTO> ReadProjects()
        {
            var db = new CrowdFundingViva1Entities();
            List<ProjectDTO> resultList = new List<ProjectDTO>();
            resultList = db.Projects.Select(s => new ProjectDTO
            {
               Id = s.Id,
               Description = s.Description
            }).ToList();

            return resultList;
        }
    } // End class
} // End namespace
