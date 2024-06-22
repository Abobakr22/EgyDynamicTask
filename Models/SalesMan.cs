using System;
using System.Collections.Generic;

using System.Text.Json.Serialization;

namespace EgyDynamicTask.Models;

public partial class SalesMan
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
