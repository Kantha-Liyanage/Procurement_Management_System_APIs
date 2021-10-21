namespace ProcurementManagementSystemServices.DTOs
{
    public class ApprovalResult
    {
        public ApprovalResult(string message)
        {
            this.Message = message;
        }
        public string Message { get; set; }
    }
}
