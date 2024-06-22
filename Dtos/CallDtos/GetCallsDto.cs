namespace EgyDynamicTask.Dtos.CallDtos
{
    public class GetCallsDto
    {
        public int CallId { get; set; }
        public string? Description { get; set; }
        public string CallTitle { get; set; }
        public DateTime? CallDate { get; set; }
        public string? CallType { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsIncoming { get; set; }

        public string? EnteredBy { get; set; }
        public DateTime? EntryDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public string CustomerName { get; set; }
        public string ProjectName { get; set; }
        public string EmployeeName { get; set; }
    }
}
