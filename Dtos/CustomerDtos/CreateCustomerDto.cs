using EgyDynamicTask.Models;

namespace EgyDynamicTask.Dtos.CustomerDtos
{
    public class CreateCustomerDto
    {
        public string CustomerName { get; set; }
        public string? Residence { get; set; }
        public string? Description { get; set; }
        public string? Job { get; set; }
        public string? CustomerSource { get; set; }
        public string? CustomerClassification { get; set; }
        public string? CustomerAddress { get; set; }
        public string? FirstPhone { get; set; }
        public string? SecondaryPhone { get; set; }
        public string? WhatsApp { get; set; }
        public string? Email { get; set; }
        public string? CustomerCode { get; set; }
        public string? Nationality { get; set; }

        public string? EnteredBy { get; set; }
        public DateTime EnteredDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public int? SalesmanId { get; set; }
    }
}
