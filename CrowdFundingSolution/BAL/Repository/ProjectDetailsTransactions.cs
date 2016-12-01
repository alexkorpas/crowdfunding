using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;

namespace BAL
{
    public partial class CrowdFundingTransactions
    {
        public async Task<TransactionResult> ReadProjectFundingLevels(int Id)
        {
            try
            {
                List<ProjectFundingLevelDTO> result = await context.ProjectFundingLevel
                    .Where(w=> w.IsActive && w.ProjectFK == Id).Select(s => new ProjectFundingLevelDTO
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Amount = (int)s.Amount,
                        Rewards = s.Rewards
                    }).OrderBy(s=>s.Amount).ToListAsync();
                return new TransactionResult(TransResult.Success, string.Empty, result);                
            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }

        public async Task<TransactionResult> DeleteProjectFundingLevel(ProjectFundingLevelDTO projectFundingLevelDTO, string user)
        {
            try
            {
                AspNetUsers _user = await context.AspNetUsers.Where(u => u.UserName == user).FirstOrDefaultAsync();

                Project _project = null;
                if (projectFundingLevelDTO.ProjectFK != null)
                    _project = await context.Project.FindAsync(projectFundingLevelDTO.ProjectFK);

                if (_project.AspNetUsers != _user)
                    return new TransactionResult(TransResult.Fail, "This is not your project", null);

                ProjectFundingLevel projectFundingLevel = await context.ProjectFundingLevel.FindAsync(projectFundingLevelDTO.Id); 

                projectFundingLevel.IsActive = false;
                await context.SaveChangesAsync();

                return new TransactionResult(TransResult.Success, "Success", null);
            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }

        public async Task<TransactionResult> SaveProjectFundingTransaction(ProjectFundingLevelDTO projectFundingLevelDTO, string user)
        {
            try
            {
                AspNetUsers _user = await context.AspNetUsers.Where(u => u.UserName == user).FirstOrDefaultAsync();                    

                Project _project = null;
                if (projectFundingLevelDTO.ProjectFK != null)
                    _project = await context.Project.FindAsync(projectFundingLevelDTO.ProjectFK);

                if (_project.AspNetUsers != _user)
                    return new TransactionResult(TransResult.Fail, "This is not your project", null);

                if (projectFundingLevelDTO.Id == null || projectFundingLevelDTO.Id == 0)
                {
                    ProjectFundingLevel projectFundingLevel = new ProjectFundingLevel()
                    {
                        Project = _project,
                        Title = projectFundingLevelDTO.Title,
                        Amount = projectFundingLevelDTO.Amount != null ? Convert.ToDecimal(projectFundingLevelDTO.Amount) : 0,
                        Rewards = projectFundingLevelDTO.Rewards,
                        IsActive = projectFundingLevelDTO.IsActive != null ? (bool)projectFundingLevelDTO.IsActive : true
                    };
                    context.ProjectFundingLevel.Add(projectFundingLevel);
                    await context.SaveChangesAsync();

                    return new TransactionResult(TransResult.Success, "Success", null, projectFundingLevel.Id);
                }
                else
                {
                    ProjectFundingLevel projectFundingLevel = await context.ProjectFundingLevel.FindAsync(projectFundingLevelDTO.Id);
                    projectFundingLevel.Project = _project;
                    projectFundingLevel.Title = projectFundingLevelDTO.Title;
                    projectFundingLevel.Amount = projectFundingLevelDTO.Amount != null ? (decimal)projectFundingLevelDTO.Amount : 0;
                    projectFundingLevel.Rewards = projectFundingLevelDTO.Rewards;
                    projectFundingLevel.IsActive = projectFundingLevelDTO.IsActive != null ? (bool)projectFundingLevelDTO.IsActive : true;
                    await context.SaveChangesAsync();

                    return new TransactionResult(TransResult.Success, "Success", null);
                }
                
            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }

        public async Task<List<ProjectPhotoDTO>> ReadProjectPhotoById(int id)
        {
            List<ProjectPhotoDTO> resultList = await context.ProjectPhoto
                .Where(s => s.Id == id)
                .Select(s => new ProjectPhotoDTO
                {
                    Id = s.Id,
                    Photo = s.Photo
                }).ToListAsync();

            return resultList;
        }

        public async Task<TransactionResult> SaveProjectDescTransaction(ProjectCampaingDTO projectCampaingDTO, string user)
        {
            try
            {
                AspNetUsers _user = await context.AspNetUsers.Where(u => u.UserName == user).FirstOrDefaultAsync();

                Project _project = await context.Project.FindAsync(projectCampaingDTO.Id);

                if (_project.AspNetUsers != _user)
                    return new TransactionResult(TransResult.Fail, "This is not your project", null);

                _project.Description = projectCampaingDTO.Description;
                await context.SaveChangesAsync();

                return new TransactionResult(TransResult.Success, "Success", null);
            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }

        public async Task<TransactionResult> ReadProjectUpdates(int Id)
        {
            try
            {
                List<ProjectUpdateDTO> result = await context.ProjectUpdate
                    .Where(w => w.ProjectFK == Id).Select(s => new ProjectUpdateDTO
                    {
                        Id = s.Id,
                        Message = s.Message,
                        Date = s.Date
                    }).OrderByDescending(s => s.Date).ToListAsync();
                return new TransactionResult(TransResult.Success, string.Empty, result);
            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }

        public async Task<TransactionResult> SaveProjectUpdateTransaction(ProjectUpdateDTO projectUpdateDTO, string user)
        {
            try
            {
                AspNetUsers _user = await context.AspNetUsers.Where(u => u.UserName == user).FirstOrDefaultAsync();

                Project _project = null;
                if (projectUpdateDTO.ProjectFK != null)
                    _project = await context.Project.FindAsync(projectUpdateDTO.ProjectFK);

                if (_project.AspNetUsers != _user)
                    return new TransactionResult(TransResult.Fail, "This is not your project", null);

                if (projectUpdateDTO.Id == null || projectUpdateDTO.Id == 0)
                {
                    ProjectUpdate projectUpdate = new ProjectUpdate()
                    {
                        Project = _project,
                        Message = projectUpdateDTO.Message,
                        Date = DateTime.Now
                    };
                    context.ProjectUpdate.Add(projectUpdate);
                    await context.SaveChangesAsync();

                    return new TransactionResult(TransResult.Success, "Success", null, projectUpdate.Id);
                }
                else
                {
                    ProjectUpdate projectUpdate = await context.ProjectUpdate.FindAsync(projectUpdateDTO.Id);
                    projectUpdate.Project = _project;
                    projectUpdate.Message = projectUpdateDTO.Message;
                    projectUpdate.Date = DateTime.Now;
                    await context.SaveChangesAsync();

                    return new TransactionResult(TransResult.Success, "Success", null);
                }

            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }


    }
}
