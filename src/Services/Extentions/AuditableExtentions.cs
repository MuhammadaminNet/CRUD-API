using src.Domain.Commons;
using src.Domain.Enums;
using System;

namespace src.Services.Extentions
{
    public static class AuditableExtentions
    {
        public static void Create(this Auditable auditable)
        {
            auditable.CreatedAt = DateTime.UtcNow;
            auditable.State = ItemState.Created;
        }

        public static void Update(this Auditable auditable)
        {
            auditable.UpdatedAt = DateTime.UtcNow;
            auditable.State = ItemState.Updated;
        }
    }
}
