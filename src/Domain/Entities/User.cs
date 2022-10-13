using src.Domain.Commons;
using src.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Domain.Entities
{
    public class User : Auditable
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender? Gender { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRole? Role { get; set; }

        public long? FileId { get; set; }
        [ForeignKey(nameof(FileId))]
        public Attachment File { get; set; }
    }
}

