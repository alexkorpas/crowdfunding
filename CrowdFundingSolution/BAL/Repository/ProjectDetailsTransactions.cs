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

        // To be rewritten. It should save the result on the database for 20 mins so that it will not
        // be recalculated every time projects are loaded.
        //public async Task<ProjectDTO> GetProjectAmountAndProgress(ProjectDTO item)
        //{
        //    var db = new CrowdFundingVivaTeam1Entities();
        //    List<PaymentDTO> resultList = new List<PaymentDTO>();
        //    resultList = await db.Payment
        //        .Where(s => s.ProjectFK == item.Id)
        //        .Select(s => new PaymentDTO
        //        {
        //            Amount = s.Amount,
        //            RefundedAmount = s.RefundedAmount
        //        }).ToListAsync();

        //    decimal sum = 0;
        //    int backers = 0;
        //    foreach (var s in resultList) {
        //        if (s.RefundedAmount != null)
        //        {
        //            sum += s.Amount - (decimal)(s.RefundedAmount);
        //        } else
        //        {
        //            sum += s.Amount;
        //        }

        //        if (s.RefundedAmount != s.Amount) backers += 1;
        //    }

        //    item.AmountGathered = sum;
        //    item.Progress = Math.Round((sum / item.Goal) * 100, 2);
        //    item.Backers = backers;

        //    return item;
        //}
        // Need to remove hardcoded strings
        public ProjectDTO GetRemainingTime(ProjectDTO item)
        {
            if ( ((DateTime)item.DueDate).CompareTo(DateTime.Now) > 0)
            {
                TimeSpan time_remaining = ((DateTime)item.DueDate).Subtract(DateTime.Now);

                if ( (int) time_remaining.TotalDays > 0 )
                {
                    item.Left = (int)time_remaining.TotalDays + " day" + ((time_remaining.TotalDays > 1) ? "s" : "") + " left";
                } else if ((int)time_remaining.Hours > 0)
                {
                    item.Left = time_remaining.Hours + " hour" + ((time_remaining.Hours > 1) ? "s" : "") + " left";
                } else {
                    item.Left = time_remaining.Minutes + " minute" + ((time_remaining.Minutes>1)?"s":"") + " left";
                }

            } else
            {
                item.Left = "EXPIRED";
            }
            return item;
        }
    }
}
