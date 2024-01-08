using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MqttDashboard.Dal.Model;

public class Sensor : BaseEntity
{
    public required string Name { get; set; }    

    public string? Description { get; set; }  
    
    public int SiteId { get; set; }

    public virtual required Site Site { get; set; }

    public virtual SensorStatus? SensorStatus { get; set; }

    public virtual ICollection<AlarmEvent> AlarmEvents { get; } = [];
}