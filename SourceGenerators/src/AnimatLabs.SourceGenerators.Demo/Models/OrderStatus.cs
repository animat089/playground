using System.ComponentModel.DataAnnotations;
using AnimatLabs.SourceGenerators.Attributes;

namespace AnimatLabs.SourceGenerators.Demo.Models;

[GenerateEnumExtensions]
public enum OrderStatus
{
    [Display(Name = "Pending Approval")]
    Pending,
    [Display(Name = "In Progress")]
    Processing,
    Shipped,
    Delivered,
    Cancelled
}
