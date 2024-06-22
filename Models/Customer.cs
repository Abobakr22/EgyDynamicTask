using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EgyDynamicTask.Models;

public partial class Customer
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;
    public string? Residence { get; set; }
    public string? Description { get; set; }
    public string? Job { get; set; }
    public string? EnteredBy { get; set; }
    public DateTime? EntryDate { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public string? CustomerSource { get; set; }
    public string? CustomerClassification { get; set; }
    public string? CustomerAddress { get; set; }
    public string? Phone1 { get; set; }
    public string? Phone2 { get; set; }
    [StringLength(25)]
    public string? Whatsapp { get; set; }
    public string? Email { get; set; }
    public string? CustomerCode { get; set; }
    public string? Nationality { get; set; }
    public int? SalesManId { get; set; }
    public virtual SalesMan? SalesMan { get; set; }
    public virtual ICollection<Call>? Calls { get; set; } = new List<Call>();
}
