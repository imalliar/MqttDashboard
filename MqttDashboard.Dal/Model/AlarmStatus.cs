using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MqttDashboard.Dal.Model;

public class AlarmStatus : BaseEntity
{
    public int Value { get; set; }

    public required string Name { get; set; }

    public ICollection<AlarmEvent> AlarmEvents { get; set; } = [];

}