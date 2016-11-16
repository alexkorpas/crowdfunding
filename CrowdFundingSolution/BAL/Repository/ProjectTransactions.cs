using System.Collections.Generic;
using System.Linq;
using DAL;
using System.Threading.Tasks;
using System.Data.Entity;
using System;

namespace BAL
{
    public partial class CrowdFundingTransactions
    {
        public async Task<TransactionResult> ReadPageCount()
        {
            try
            {
                var db = new backup_CrowdFundingViva1Entities();
                return new TransactionResult(TransResult.Success, string.Empty, await db.Project.CountAsync());
            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }
        public async Task<TransactionResult> ReadProjects(TransactionCriteria criteria)
        {
            try
            {
                var db = new backup_CrowdFundingViva1Entities();
                var result = new List<ProjectDTO>();
                if (criteria.Id != null)
                {
                    var s = await db.Project.FindAsync(criteria.Id);
                    result.Add(new ProjectDTO
                    {
                        Id = s.Id,
                        Description = s.Description,
                        UserFK = s.UserFK,
                        Title = s.Title,
                        ShortDescription = s.ShortDescription,
                        Goal = s.Goal,
                        GoalMin = s.GoalMin,
                        MainPhotoFK = s.MainPhotoFK,
                        Video = s.Video,
                        CategoryFK = s.CategoryFK,
                        CategoryDesc = s.ProjectCategory != null ? s.ProjectCategory.Description : null,
                        CategoryName = s.ProjectCategory.Title,
                        DueDate = s.DueDate,
                        IsActive = s.IsActive,
                        CreatedDate = s.CreatedDate,
                        UpdatedDate = s.UpdatedDate,
                        DeletedDate = s.DeletedDate,
                        BlockedDate = s.BlockedDate,
                        StateFK = s.StateFK,
                        Website = s.Website
                    });
                    return new TransactionResult(TransResult.Success, string.Empty, result);
                }
                var res = db.Project.AsQueryable();
                if (criteria.UserId != null)
                    res = res.Where(s => s.UserFK == criteria.UserId.ToString());
                if (criteria.StateId != null)
                    res = res.Where(s => s.StateFK == criteria.UserId);
                if (criteria.CategoryId != null)
                    res = res.Where(s => s.CategoryFK == criteria.CategoryId);
                if (criteria.Search != null && criteria.Search != "")
                    res = res.Where(s => s.Title.Contains(criteria.Search) || s.ShortDescription.Contains(criteria.Search));
                if (criteria.Page != null)
                    res = res.Skip((int)criteria.Page * 3).Take(3);
                result = await res.Where(a => a.IsActive == true).Select(s => new ProjectDTO
                {
                    Id = s.Id,
                    Description = s.Description,
                    UserFK = s.UserFK,
                    Title = s.Title,
                    ShortDescription = s.ShortDescription,
                    Goal = s.Goal,
                    GoalMin = s.GoalMin,
                    MainPhotoFK = s.MainPhotoFK,
                    Video = s.Video,
                    CategoryFK = s.CategoryFK,
                    DueDate = s.DueDate,
                    IsActive = s.IsActive,
                    CreatedDate = s.CreatedDate,
                    UpdatedDate = s.UpdatedDate,
                    DeletedDate = s.DeletedDate,
                    BlockedDate = s.BlockedDate,
                    StateFK = s.StateFK,
                    Website = s.Website
                }).ToListAsync(); // Query Execute __________________________________            
                return new TransactionResult(TransResult.Success, string.Empty, result);
            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }

        public async Task<TransactionResult> ReadProjectStates()
        {
            try
            {
                var db = new backup_CrowdFundingViva1Entities();
                List<ProjectStateDTO> result = new List<ProjectStateDTO>();
                result = await db.ProjectState.Select(s => new ProjectStateDTO
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description
                }).ToListAsync();

                return new TransactionResult(TransResult.Success, string.Empty, result);
            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }

        public async Task<TransactionResult> ReadProjectCategories()
        {
            try
            { 
                var db = new backup_CrowdFundingViva1Entities();
                List<ProjectCategoryDTO> result = new List<ProjectCategoryDTO>();
                result = await db.ProjectCategory.Select(s => new ProjectCategoryDTO
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description
                }).ToListAsync();

                return new TransactionResult(TransResult.Success, string.Empty, result);
            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }

        public async Task<List<ProjectDTO>> PrepareProjects(List<ProjectDTO> list)
        {
            foreach (var i in list)
            {
                await GetProjectAmountAndProgress(i);
                GetRemainingTime(i);
            }

            return list;
        }
    } // End class
} // End namespace
