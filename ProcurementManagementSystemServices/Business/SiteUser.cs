using ProcurementManagementSystemServices.DTOs;
using ProcurementManagementSystemServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcurementManagementSystemServices.Business
{
    public class SiteUser : CostApprover
    {
        public bool canApprove(ProcurementManagmentContext context,PurchaseRequisitionDTO purchReq)
        {
            //Things I have approved so far
            var innerJoinQueryOldEntires =
                from header in context.PurchaseRequisitions
                where header.SiteId == purchReq.SiteId
                join items in context.PurchaseRequisitionItems.Where(item => item.Status == "Approved") on header.Id equals items.PurchaseRequisitionId
                join materials in context.Materials on items.MaterialId equals materials.Id
                select new { items.ApprovedQuantity, materials.UnitPrice };

            //From current PR
            var innerJoinQueryCurrent =
                from header in context.PurchaseRequisitions
                where (header.SiteId == purchReq.SiteId && header.Id == purchReq.Id)
                join items in context.PurchaseRequisitionItems on header.Id equals items.PurchaseRequisitionId
                join materials in context.Materials on items.MaterialId equals materials.Id
                select new { items.ItemId, materials.UnitPrice };

            //Site user
            SiteBudget myBudget = context.SiteBudgets.Where(budget => budget.UserType == CostApprover.SITE_USER && budget.SiteId == purchReq.SiteId).ToArray<SiteBudget>()[0];

            //Deduct already approved
            double availableAmount = (double)myBudget.Amount;
            foreach (var row in innerJoinQueryOldEntires)
            {
                availableAmount -= (double)(row.UnitPrice * row.ApprovedQuantity);
            }

            //Deduct current
            foreach (var row in innerJoinQueryCurrent)
            {
                double qty = purchReq.items.Where(edited => edited.ItemId == row.ItemId).First<PurchaseRequisitionItemDTO>().ApprovedQuantity;
                availableAmount -= (double)(row.UnitPrice * qty);
            }

            return (availableAmount >= 0);
        }
    }
}
