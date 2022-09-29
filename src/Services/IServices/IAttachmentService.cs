using Domain.Entities;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IAttachmentService
    {
        Task<Attachment> UploadAsync(Stream stream, string fileName);
        Task<Attachment> UpdateAsync(int id, Stream stream);
        Task<bool> DeleteAsync(Expression<Func<Attachment, bool>> expression = null);

    }
}
