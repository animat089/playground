using System.ComponentModel.DataAnnotations;

namespace AnimatLabs.SourceGenerators.T4.Generated;

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
