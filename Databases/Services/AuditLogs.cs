using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Databases.Models
{
    [Table("AuditLogs")]
    public class AuditLog
    {
        [Key]
        public int Id { get; set; }

        public string? EntityName { get; set; }
        public int? EntityId { get; set; }

        public string? FieldName { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }

        public DateTime ChangeDate { get; set; } = DateTime.UtcNow;
        public string? ChangedBy { get; set; }
        public string? ActionType { get; set; } = "Update";  // Default: Update

        public Guid TransactionId { get; set; } = Guid.NewGuid(); // NEW!
    }
}
