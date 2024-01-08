namespace MqttDashboard.Dal.Settings;

public class MqttSettings
{
    public string? Server { get; set; }

    public int Port { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Topic { get; set; }
}