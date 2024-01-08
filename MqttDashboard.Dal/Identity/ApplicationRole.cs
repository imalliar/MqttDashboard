using Microsoft.AspNetCore.Identity;

namespace MqttDashboard.Dal.Identity;

public class ApplicationRole :  IdentityRole<int>
{
    public string? Description { get; set; }    
}