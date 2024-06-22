using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EgyDynamicTask.Models;

public partial class Call
{
    public int Id { get; set; }
    public string CallTitle { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? CallDate { get; set; }
    public string? CallType { get; set; }
    public string? EnteredBy { get; set; }
    public DateTime? EntryDate { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public bool? IsCompleted { get; set; }
    public bool? IsIncoming { get; set; }
    public int? CustomerId { get; set; }
    public int? ProjectId { get; set; }
    public int? EmployeeId { get; set; }
    public virtual Customer? Customer { get; set; }
    public virtual Employee? Employee { get; set; }
    public virtual Project? Project { get; set; }
}
