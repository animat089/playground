using Microsoft.EntityFrameworkCore;

namespace AnimatLabs.OutboxPattern.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OutboxEvent> Outbox => Set<OutboxEvent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(e =>
        {
            e.ToTable("orders");
            e.HasKey(o => o.Id);
            e.Property(o => o.Id).HasColumnName("id");
            e.Property(o => o.Customer).HasColumnName("customer");
            e.Property(o => o.Product).HasColumnName("product");
            e.Property(o => o.Quantity).HasColumnName("quantity");
            e.Property(o => o.TotalAmount).HasColumnName("total_amount").HasColumnType("decimal(10,2)");
            e.Property(o => o.Status).HasColumnName("status").HasDefaultValue("pending");
            e.Property(o => o.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<OutboxEvent>(e =>
        {
            e.ToTable("outbox");
            e.HasKey(o => o.Id);
            e.Property(o => o.Id).HasColumnName("id");
            e.Property(o => o.AggregateType).HasColumnName("aggregate_type");
            e.Property(o => o.AggregateId).HasColumnName("aggregate_id");
            e.Property(o => o.EventType).HasColumnName("event_type");
            e.Property(o => o.Payload).HasColumnName("payload");
            e.Property(o => o.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()");
        });
    }
}

public class Order
{
    public int Id { get; set; }
    public string Customer { get; set; } = "";
    public string Product { get; set; } = "";
    public int Quantity { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "pending";
    public DateTime CreatedAt { get; set; }
}

public class OutboxEvent
{
    public Guid Id { get; set; }
    public string AggregateType { get; set; } = "";
    public string AggregateId { get; set; } = "";
    public string EventType { get; set; } = "";
    public string Payload { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}
