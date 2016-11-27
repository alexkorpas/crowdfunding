using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public partial class CrowdFundingTransactions
    {
        public async Task<TransactionResult> ReadPayments(TransactionCriteria criteria)
        {
            try
            {
                var result = new List<PaymentDTO>();

                // Checking if an Id, ProjectId or UserId has been given
                // so that it will not return all payments.
                if (criteria.Id == null &&
                    criteria.ProjectId == null &&
                    (criteria.UserId == "" || criteria.UserId == null) )
                {
                    return new TransactionResult(TransResult.Fail, string.Empty, null);
                }

                if (criteria.Id != null)
                {
                    var s = await context.Payment.FindAsync(criteria.Id);
                    result.Add(new PaymentDTO
                    {
                        Id = s.Id,
                        ProjectFK = s.ProjectFK,
                        UserFK = s.UserFK,
                        Amount = s.Amount,
                        Rewards = s.Rewards,
                        PaymentDate = s.PaymentDate,
                        PaymentMethod = s.PaymentMethod,
                        RefundedAmount = s.RefundedAmount,
                        RefundedDate = s.RefundedDate,
                        ProjectTitle = s.Project != null ? s.Project.Title : null,
                        ProjectGathered = s.Project != null ? s.Project.Gathered : 0,
                        ProjectGoal = s.Project != null ? s.Project.Goal : 0
                    });
                    return new TransactionResult(TransResult.Success, string.Empty, result);
                }
                var res = context.Payment.AsQueryable();
                if (criteria.ProjectId != null)
                    res = res.Where(s => s.ProjectFK == criteria.ProjectId);
                if (criteria.UserId != null)
                    res = res.Where(s => s.UserFK == criteria.UserId);
                if (criteria.Page != null)
                    res = res.OrderBy(s => s.Id).Skip((int)criteria.Page * 3).Take(3);
                result = res.Select(s => new PaymentDTO
                {
                    Id = s.Id,
                    ProjectFK = s.ProjectFK,
                    UserFK = s.UserFK,
                    Amount = s.Amount,
                    Rewards = s.Rewards,
                    PaymentDate = s.PaymentDate,
                    PaymentMethod = s.PaymentMethod,
                    RefundedAmount = s.RefundedAmount,
                    RefundedDate = s.RefundedDate,
                    ProjectTitle = s.Project != null ? s.Project.Title : null,
                    ProjectGathered = s.Project != null ? s.Project.Gathered : 0,
                    ProjectGoal = s.Project != null ? s.Project.Goal : 0
                }).ToList(); // Query Execute __________________________________            
                return new TransactionResult(TransResult.Success, string.Empty, result);
            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }
    }
}
