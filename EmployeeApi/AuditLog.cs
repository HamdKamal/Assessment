using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Databases.Models;
using Microsoft.EntityFrameworkCore;

namespace Databases.Services
{
    public interface IAuditService
    {
        Task LogChangesAsync<T>(T oldEntity, T newEntity, int entityId, string changedBy) where T : class;
    }

    public class AuditService : IAuditService
    {
        private readonly DbContext _context;

        public AuditService(DbContext context)
        {
            _context = context;
        }

        public async Task LogChangesAsync<T>(T oldEntity, T newEntity, int entityId, string changedBy) where T : class
        {
            if (oldEntity == null || newEntity == null) return;

            var entityName = typeof(T).Name;
            var transactionId = Guid.NewGuid(); // 🎯 One TransactionId for all changes
            var auditLogs = new List<AuditLog>();

            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                var oldValue = prop.GetValue(oldEntity)?.ToString();
                var newValue = prop.GetValue(newEntity)?.ToString();

                if (oldValue != newValue)
                {
                    auditLogs.Add(new AuditLog
                    {
                        EntityName = entityName,
                        EntityId = entityId,
                        FieldName = prop.Name,
                        OldValue = oldValue,
                        NewValue = newValue,
                        ChangeDate = DateTime.UtcNow,
                        ChangedBy = changedBy,
                        ActionType = "Update",
                        TransactionId = transactionId
                    });
                }
            }

            if (auditLogs.Count > 0)
            {
                await _context.Set<AuditLog>().AddRangeAsync(auditLogs);
                await _context.SaveChangesAsync();
            }
        }
    }
}
