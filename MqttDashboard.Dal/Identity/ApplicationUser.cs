using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MqttDashboard.Dal.Identity;

public class ApplicationUser : IdentityUser<int>
{
    [PersonalData]
    [Column(TypeName = "nvarchar(256)")]
    public string? FirstName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(256)")]
    public string? LastName { get; set; }
}