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
        public async Task<List<ProjectPhotoDTO>> ReadProjectPhotoById(int id)
        {
            var db = new backup_CrowdFundingViva1Entities();
            List<ProjectPhotoDTO> resultList = new List<ProjectPhotoDTO>();
            resultList = await db.ProjectPhoto
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
        public async Task<ProjectDTO> GetProjectAmountAndProgress(ProjectDTO item)
        {
            var db = new backup_CrowdFundingViva1Entities();
            List<PaymentDTO> resultList = new List<PaymentDTO>();
            resultList = await db.Payment
                .Where(s => s.ProjectFK == item.Id)
                .Select(s => new PaymentDTO
                {
                    Amount = s.Amount,
                    RefundedAmount = s.RefundedAmount.Equals(null) ? 0 : (decimal)s.RefundedAmount
                }).ToListAsync();

            decimal sum = 0;
            int backers = 0;
            foreach (var s in resultList) {
                sum += s.Amount - s.RefundedAmount;
                if (s.RefundedAmount != s.Amount) backers += 1;
            }

            item.AmountGathered = sum;
            item.Progress = Math.Round((sum / item.Goal) * 100, 2);
            item.Backers = backers;

            return item;
        }
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
