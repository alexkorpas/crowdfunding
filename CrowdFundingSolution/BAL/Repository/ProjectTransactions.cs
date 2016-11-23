﻿using System.Collections.Generic;
using System.Linq;
using DAL;
using System.Threading.Tasks;
using System.Data.Entity;
using System;

namespace BAL
{
    public partial class CrowdFundingTransactions
    {
        public async Task<TransactionResult> ReadPageCount(string keyword)
        {
            try
            {
                var db = new backup_CrowdFundingViva1Entities();
                int result;
                //var transaction = await ReadProjects(new TransactionCriteria { Search = keyword });
                if (keyword != null) {
                    result = await db.Project.Where(s => (s.Title.Contains(keyword) || s.ShortDescription.Contains(keyword)) && s.IsActive == true ).CountAsync();
                }
                else
                {
                    result = await db.Project.Where(s => s.IsActive == true).CountAsync();
                }
                
                //return new TransactionResult(TransResult.Success, string.Empty, ((List<ProjectDTO>)transaction.ReturnObject).Count());

                return new TransactionResult(TransResult.Success, string.Empty, result);
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
                    res = res.Where(s => s.UserFK == criteria.UserId);
                if (criteria.StateId != null)
                    res = res.Where(s => s.StateFK == criteria.StateId);
                if (criteria.CategoryId != null)
                    res = res.Where(s => s.CategoryFK == criteria.CategoryId);
                if (criteria.Search != null && criteria.Search != "")
                    res = res.Where(s => s.Title.Contains(criteria.Search) || s.ShortDescription.Contains(criteria.Search));
                if (criteria.Page != null)
                    res = res.OrderBy(s => s.Id).Skip((int)criteria.Page * 6).Take(6);
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
                result = await db.ProjectCategory.OrderBy(s => s.Title).Select(s => new ProjectCategoryDTO
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

        public async Task<TransactionResult> SaveProjectTransaction(ProjectDTO projectDTO, string user)
        {
            try
            {
                using (var db = new backup_CrowdFundingViva1Entities())
                {
                    AspNetUsers _user = await db.AspNetUsers.Where(u => u.UserName == user).FirstOrDefaultAsync();

                    ProjectCategory _projectCategory = null;
                    if (projectDTO.CategoryFK != null)
                        _projectCategory = await db.ProjectCategory.FindAsync(projectDTO.CategoryFK);
                    ProjectState _projectState = null;
                    if (projectDTO.StateFK != null)
                        _projectState = await db.ProjectState.FindAsync(projectDTO.StateFK);

                    if (projectDTO.Id == null || projectDTO.Id == 0)
                    {
                        Project project = new Project()
                        {
                            AspNetUsers = _user,
                            Title = projectDTO.Title,
                            Description = projectDTO.Description,
                            ShortDescription = projectDTO.ShortDescription,
                            Goal = projectDTO.Goal,
                            Video = projectDTO.Video,
                            ProjectCategory = _projectCategory,
                            DueDate = projectDTO.DueDate,
                            IsActive = true,
                            ProjectState = _projectState,
                            CreatedDate = DateTime.Now
                        
                        };
                        db.Project.Add(project);
                        await db.SaveChangesAsync();

                        return new TransactionResult(TransResult.Success, "Success", null, project.Id);
                    }
                    else
                    {
                        Project project = await db.Project.FindAsync(projectDTO.Id);
                        if (project.AspNetUsers != _user)
                            return new TransactionResult(TransResult.Fail, "This is not your project", null);
                        project.Title = projectDTO.Title;
                        project.Description = projectDTO.Description;
                        project.ShortDescription = projectDTO.ShortDescription;
                        project.Goal = projectDTO.Goal;
                        project.Video = projectDTO.Video;
                        project.ProjectCategory = _projectCategory;
                        project.DueDate = projectDTO.DueDate;
                        project.IsActive = true;
                        project.ProjectState = _projectState;
                        await db.SaveChangesAsync();

                        return new TransactionResult(TransResult.Success, "Success", null);
                    }
                }
            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }

        public async Task<TransactionResult> ReadTrendingProjects()
        {
            try
            {
                var db = new backup_CrowdFundingViva1Entities();
                double days = 7;
                int projsToReturn = 5;
                DateTime fromDay = DateTime.Now.AddDays(-days);

                var result = await db.Project.Where(
                    x => (
                        from p in db.Payment
                        where (fromDay.CompareTo(p.PaymentDate) <= 0 && (p.Amount - p.RefundedAmount != 0))
                        group p by p.ProjectFK into ProjectIDs
                        let countPayments = ProjectIDs.Count()
                        orderby countPayments descending
                        select ProjectIDs.Key).Take(projsToReturn
                        ).Contains(x.Id)
                    ).Select(s => new ProjectDTO
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
                    }).ToListAsync();

                return new TransactionResult(TransResult.Success, string.Empty, result);
            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }
    } // End class
} // End namespace
