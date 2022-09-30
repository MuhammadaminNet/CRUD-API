using Domain.Commons;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extentions
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
