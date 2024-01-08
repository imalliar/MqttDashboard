using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MqttDashboard.Dal.Model;


public class Application : BaseEntity
{
    public required string Name { get; set; }    

    public string? Description { get; set; }

    public virtual ICollection<Site> Sites { get; } = [];
}