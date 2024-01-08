using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MqttDashboard.Dal.Model;

public class AlarmEvent : BaseEntity
{
    public int SensorId { get; set; }

    public DateTime TimeOccurred { get; set; }

    public int AlarmStatusId { get; set; }

    public float Temperature { get; set; }

    public float Humidity { get; set; }

    public float AirPressure { get; set; }

    public float Latitude { get; set; }

    public float Longitude { get; set; }

    public virtual required AlarmStatus AlarmStatus { get; set; }

    public virtual required Sensor Sensor { get; set; }

    public virtual SensorStatus? SensorStatus { get; set; }
}