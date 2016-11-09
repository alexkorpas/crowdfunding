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
        public List<ProjectDTO> ReadProjects(int ?id=null)
        {
            var db = new CrowdFundingViva1Entities();
            List<ProjectDTO> resultList = new List<ProjectDTO>();
            resultList = db.project.Select(s => new ProjectDTO
            {
               Id = s.project_id,
               Description = s.description
            }).ToList();

            return resultList;
        }
    } // End class
} // End namespace
