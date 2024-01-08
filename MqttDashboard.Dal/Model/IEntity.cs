namespace MqttDashboard.Dal.Model;

public interface IEntity
{
    public int Id { get; set; }
    public DateTime DateAdded { get; set; }
}