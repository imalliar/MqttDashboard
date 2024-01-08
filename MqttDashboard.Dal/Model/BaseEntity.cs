using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MqttDashboard.Dal.Model;

public abstract class BaseEntity : IEntity
{
    public int Id { get; set; }

    public DateTime DateAdded { get; set; } = DateTime.UtcNow;
}