using ProcurementManagementSystemServices.DTOs;
using ProcurementManagementSystemServices.Models;
using System.Linq;

namespace ProcurementManagementSystemServices.Business
{
    public interface CostApprover
    {

        public const int SITE_USER = 1;
        public const int SITE_SUPERVISOR = 2;
        public const int SITE_MANAGER = 3;

        public bool canApprove(ProcurementManagmentContext context, PurchaseRequisitionDTO purchReq);
    }
}
