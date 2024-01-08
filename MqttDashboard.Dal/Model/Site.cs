using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MqttDashboard.Dal.Model;

public class Site : BaseEntity
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public required int ApplicationId { get; set; }

    public required Application Application { get; set; }

    public virtual ICollection<Sensor> Sensors { get; } = [];
}