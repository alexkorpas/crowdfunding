using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BAL.DTO;
using System.Data.Entity;

namespace BAL
{
    public partial class CrowdFundingTransactions
    {
        public async Task<List<ProjectPhotoDTO>> ReadProjectPhotoById(int id)
        {
            var db = new CrowdFundingViva1Entities();
            List<ProjectPhotoDTO> resultList = new List<ProjectPhotoDTO>();
            resultList = await db.project_photo
                .Where(s => s.photo_id == id)
                .Select(s => new ProjectPhotoDTO
                {
                    Photo_Id = s.photo_id,
                    Photo = s.photo
                }).ToListAsync();

            return resultList;
        }

        // To be rewritten. It should save the result on the database for 20 mins so that it will not
        // be recalculated every time projects are loaded.
        public async Task<ProjectDTO> GetProjectAmountAndProgress(ProjectDTO item)
        {
            var db = new CrowdFundingViva1Entities();
            List<PaymentDTO> resultList = new List<PaymentDTO>();
            resultList = await db.payment
                .Where(s => s.project_id == item.Project_Id)
                .Select(s => new PaymentDTO
                {
                    Amount = s.amount,
                    Refunded_Amount = s.refunded_amount.Equals(null) ? 0 : s.refunded_amount
                }).ToListAsync();

            decimal sum = 0;
            int backers = 0;
            foreach (var s in resultList) {
                sum += s.Amount - s.Refunded_Amount;
                if (s.Refunded_Amount != s.Amount) backers += 1;
            }

            item.Amount_Gathered = sum;
            item.Progress = Math.Round((sum / item.Goal) * 100, 2);
            item.Backers = backers;

            return item;
        }
        // Need to remove hardcoded strings
        public ProjectDTO GetRemainingTime(ProjectDTO item)
        {
            if ( ((DateTime)item.Due_Date).CompareTo(DateTime.Now) > 0)
            {
                TimeSpan time_remaining = ((DateTime)item.Due_Date).Subtract(DateTime.Now);

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
