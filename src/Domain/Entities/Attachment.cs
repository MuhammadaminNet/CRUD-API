using src.Domain.Commons;

namespace src.Domain.Entities
{
    public class Attachment : Auditable
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
