using Domain.Enums;
using System;

namespace Domain.Commons
{
    public class Auditable
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public State State { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long? DeletedBy { get; set; }
    }
}
