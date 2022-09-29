using Domain.Commons;

namespace Domain.Entities
{
    public class Attachment : Auditable
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
