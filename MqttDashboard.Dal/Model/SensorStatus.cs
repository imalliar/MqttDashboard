using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MqttDashboard.Dal.Model;


public class SensorStatus : IEntity
{
    public int Id { get; set; }

    public DateTime DateAdded { get; set; }

    public int EventId { get; set; }
    
    public virtual required AlarmEvent AlarmEvent { get; set; }

    public virtual required Sensor Sensor { get; set; }
}