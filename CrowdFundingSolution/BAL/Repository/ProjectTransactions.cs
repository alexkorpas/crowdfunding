using System.Collections.Generic;
using System.Linq;
using DAL;
using BAL.DTO;
using System;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BAL
{
    public partial class CrowdFundingTransactions
    {
        /// <summary>
        /// Returns a list of all available Projects
        /// </summary>  
        /// <returns>List<ProjectDTO></returns>
        public async Task<List<ProjectDTO>> ReadProjects(int ?id=null)
        {
            if (id.Equals(null))
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
            else
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
                }).Where(s => s.Project_Id==id).ToList();

                return resultList;
            }
            
        }

        public async Task<List<ProjectDTO>> ReadProjectsByUserId(int id)
        {
            var db = new CrowdFundingViva1Entities();
            List<ProjectDTO> resultList = new List<ProjectDTO>();
            resultList = await db.project.Select(s => new ProjectDTO
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
            }).Where(s => s.User_Id == id).ToListAsync();

            return resultList;
        }

        public async Task<List<ProjectDTO>> ReadProjectByState(int id)
        {
            var db = new CrowdFundingViva1Entities();
            List<ProjectDTO> resultList = new List<ProjectDTO>();
            resultList = await db.project.Select(s => new ProjectDTO
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
            }).Where(s => s.State_Id.Equals(id)).ToListAsync();

            return resultList;
        }

        public async Task<List<ProjectPhotoDTO>> ReadProjectPhotoById(int id)
        {
            var db = new CrowdFundingViva1Entities();
            List<ProjectPhotoDTO> resultList = new List<ProjectPhotoDTO>();
            resultList = await db.project_photo.Select(s => new ProjectPhotoDTO
            {
                Photo_Id = s.photo_id,
                Photo = s.photo
            }).Where(s=> s.Photo_Id==id).ToListAsync();

            return resultList;
        }

        public async Task<List<ProjectStateDTO>> ReadProjectStates()
        {
            var db = new CrowdFundingViva1Entities();
            List<ProjectStateDTO> resultList = new List<ProjectStateDTO>();
            resultList = await db.project_state.Select(s => new ProjectStateDTO
            {
                State_Id = s.state_id,
                Title = s.title,
                Description = s.description
            }).ToListAsync();

            return resultList;
        }

        public async Task<List<ProjectDTO>> ReadProjectByCategory(int id)
        {
            var db = new CrowdFundingViva1Entities();
            List<ProjectDTO> resultList = new List<ProjectDTO>();
            resultList = await db.project.Select(s => new ProjectDTO
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
            }).Where(s => s.Category_Id.Equals(id)).ToListAsync();

            return resultList;
        }

        public async Task<List<ProjectCategoryDTO>> ReadProjectCategories()
        {
            var db = new CrowdFundingViva1Entities();
            List<ProjectCategoryDTO> resultList = new List<ProjectCategoryDTO>();
            resultList = await db.project_category.Select(s => new ProjectCategoryDTO
            {
                Category_Id = s.category_id,
                Title = s.title,
                Description = s.description
            }).ToListAsync();

            return resultList;
        }
    } // End class
} // End namespace
