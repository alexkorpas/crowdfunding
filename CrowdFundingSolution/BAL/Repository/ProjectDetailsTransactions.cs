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
                    Amount = s.amount
                }).ToListAsync();

            decimal sum = 0;
            foreach (var s in resultList) {
                sum += s.Amount - s.Refunded_Amount;
            }

            item.Amount_Gathered = sum;
            item.Progress = Math.Round((sum / item.Goal) * 100, 2);

            return item;
        }

        public ProjectDTO GetRemainingTime(ProjectDTO item)
        {
            if ( ((DateTime)item.Due_Date).CompareTo(DateTime.Now) > 0)
            {
                item.Days_Remaining = item.Due_Date - DateTime.Now;
            } else
            {
                item.Days_Remaining = TimeSpan.Zero;
            }
            return item;
        }
    }
}
