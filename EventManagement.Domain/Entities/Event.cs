using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Domain.Entities;
public class Event : BaseEntity
{
    public string Name { get; set; }
    public string Location { get; set; }
    public int Capacity { get; set; }
    public int BookedSeats { get; set; }
}
