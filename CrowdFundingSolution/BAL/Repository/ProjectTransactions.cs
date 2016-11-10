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
                Project_Id = s.project_id,
                Description = s.description,
                User_Id = s.user_id,
                Title = s.title,
                Short_Description = s.short_description,
                Goal = s.goal,
                Goal_Min = s.goal_min,
                Photo_Id_Main = s.photo_id_main,
                Video = s.video,
                Category_Id = s.category_id,
                Due_Date = s.due_date,
                Is_Active = s.is_active,
                Created_Date = s.created_date,
                Updated_Date = s.updated_date,
                Deleted_Date = s.deleted_date,
                Blocked_Date = s.blocked_date,
                State_Id = s.state_id,
                Website = s.website
            }).ToList();

            return resultList;
        }
    } // End class
} // End namespace
