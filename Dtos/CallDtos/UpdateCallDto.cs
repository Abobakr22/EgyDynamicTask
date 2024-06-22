namespace EgyDynamicTask.Dtos.CallDtos
{
    public class UpdateCallDto
    {
        public string CallTitle { get; set; }
        public string? Description { get; set; }
        public DateTime? CallDate { get; set; }
        public string? CallType { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsIncoming { get; set; }

        public string? EnteredBy { get; set; }
        public DateTime? EntryDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public int? CustomerId { get; set; }
        public int? ProjectId { get; set; }
        public int? EmployeeId { get; set; }

    }
}
