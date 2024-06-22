using System;
using System.Collections.Generic;

namespace EgyDynamicTask.Models;

public partial class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<Call> Calls { get; set; } = new List<Call>();
}
