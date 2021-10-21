using ProcurementManagementSystemServices.DTOs;
using ProcurementManagementSystemServices.Models;
using System.Linq;

namespace ProcurementManagementSystemServices.Business
{
    public class CostApprover
    {
        private readonly ProcurementManagmentContext context;
        private string username;

        public CostApprover(ProcurementManagmentContext context, string username)
        {
            this.context = context;
            this.username = username;
        }

        public bool canApprove(PurchaseRequisitionDTO purchReq) {
            //Things I have approved so far
            var innerJoinQueryOldEntires =
                from header in this.context.PurchaseRequisitions
                where header.SiteId == purchReq.SiteId
                join items in this.context.PurchaseRequisitionItems.Where(item => item.Status == "Approved" && item.ApprovedBy == this.username) on header.Id equals items.PurchaseRequisitionId
                join materials in this.context.Materials on items.MaterialId equals materials.Id
                select new { items.ApprovedQuantity, materials.UnitPrice };

            //From current PR
            var innerJoinQueryCurrent =
                from header in this.context.PurchaseRequisitions
                where ( header.SiteId == purchReq.SiteId && header.Id == purchReq.Id )
                join items in this.context.PurchaseRequisitionItems on header.Id equals items.PurchaseRequisitionId
                join materials in this.context.Materials on items.MaterialId equals materials.Id
                select new { items.ItemId, materials.UnitPrice };

            //My budget
            SiteBudget myBudget = this.context.SiteBudgets.Where(budget => budget.CostApprover == this.username && budget.SiteId == purchReq.SiteId).ToArray<SiteBudget>()[0];

            //Deduct already approved
            double availableAmount = (double)myBudget.Amount;
            foreach (var row in innerJoinQueryOldEntires)
            {
                availableAmount -= (double)(row.UnitPrice * row.ApprovedQuantity);
            }

            //Deduct current
            foreach (var row in innerJoinQueryCurrent)
            {
                double qty = purchReq.items.Where(edited=> edited.ItemId == row.ItemId).First<PurchaseRequisitionItemDTO>().ApprovedQuantity;
                availableAmount -= (double)(row.UnitPrice * qty);
            }

            return (availableAmount >= 0);
        }
    }
}
