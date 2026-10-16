using Microsoft.EntityFrameworkCore;
using MiniRobot.Domain.Entities;
using MiniRobot.Domain.Enums;

namespace MiniRobot.Infrastructure.Persistence;

public class MiniRobotDbContext(DbContextOptions<MiniRobotDbContext> options) : DbContext(options)
{
    // Каждый DbSet — таблица. Через них пишутся запросы: db.Suppliers.Where(...)
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
    public DbSet<PurchaseOrderLine> PurchaseOrderLines => Set<PurchaseOrderLine>();
    public DbSet<IncomingDocument> IncomingDocuments => Set<IncomingDocument>();
    public DbSet<IncomingDocumentLine> IncomingDocumentLines => Set<IncomingDocumentLine>();
    public DbSet<OperatorQuestion> OperatorQuestions => Set<OperatorQuestion>();

    /// <summary>Общие правила для всех свойств определённого типа</summary>
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // Enum'ы храним строками: в базе будет 'Received', а не 1.
        // Так база читается глазами, и порядок вариантов в enum можно менять без порчи данных
        configurationBuilder.Properties<CommunicationChannel>().HaveConversion<string>().HaveMaxLength(32);
        configurationBuilder.Properties<IncomingDocumentStatus>().HaveConversion<string>().HaveMaxLength(32);
        configurationBuilder.Properties<OperatorQuestionStatus>().HaveConversion<string>().HaveMaxLength(32);
        configurationBuilder.Properties<PurchaseOrderStatus>().HaveConversion<string>().HaveMaxLength(32);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Supplier>(e =>
        {
            e.Property(x => x.Name).HasMaxLength(256);
            e.Property(x => x.Email).HasMaxLength(256);
        });

        modelBuilder.Entity<PurchaseOrder>(e =>
        {
            e.Property(x => x.Number).HasMaxLength(64);
            e.HasIndex(x => x.Number).IsUnique();             // номер заказа не повторяется

            e.HasOne(x => x.Supplier)
             .WithMany(s => s.PurchaseOrders)
             .HasForeignKey(x => x.SupplierId)
             .OnDelete(DeleteBehavior.Restrict);              // нельзя удалить поставщика, у которого есть заказы

            e.HasMany(x => x.Lines)
             .WithOne(l => l.PurchaseOrder)
             .HasForeignKey(l => l.PurchaseOrderId)
             .OnDelete(DeleteBehavior.Cascade);               // удалили заказ — удалились его строки
        });

        modelBuilder.Entity<PurchaseOrderLine>(e =>
        {
            e.Property(x => x.Sku).HasMaxLength(64);
            e.Property(x => x.Name).HasMaxLength(512);
            e.Property(x => x.Quantity).HasPrecision(18, 3);  // до 3 знаков после запятой: кг, литры
            e.Property(x => x.UnitPrice).HasPrecision(18, 2); // рубли и копейки
        });

        modelBuilder.Entity<IncomingDocument>(e =>
        {
            e.Property(x => x.ExternalMessageId).HasMaxLength(512);

            // Одно и то же сообщение из одного канала не попадёт в базу дважды:
            // при повторной вставке база сама выдаст ошибку
            e.HasIndex(x => new { x.Channel, x.ExternalMessageId }).IsUnique();

            e.HasIndex(x => x.Status);                        // робот часто ищет документы по статусу

            e.HasOne(x => x.Supplier)
             .WithMany()
             .HasForeignKey(x => x.SupplierId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.PurchaseOrder)
             .WithMany()
             .HasForeignKey(x => x.PurchaseOrderId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasMany(x => x.Lines)
             .WithOne(l => l.IncomingDocument)
             .HasForeignKey(l => l.IncomingDocumentId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(x => x.Questions)
             .WithOne(q => q.IncomingDocument)
             .HasForeignKey(q => q.IncomingDocumentId)
             .OnDelete(DeleteBehavior.Cascade);               // удалили документ — удалились вопросы по нему
        });

        modelBuilder.Entity<IncomingDocumentLine>(e =>
        {
            e.Property(x => x.Sku).HasMaxLength(64);
            e.Property(x => x.Name).HasMaxLength(512);
            e.Property(x => x.Quantity).HasPrecision(18, 3);
            e.Property(x => x.UnitPrice).HasPrecision(18, 2);
        });

        modelBuilder.Entity<OperatorQuestion>(e =>
        {
            e.Property(x => x.Text).HasMaxLength(2000);
            e.HasIndex(x => x.Status);
        });
    }
}