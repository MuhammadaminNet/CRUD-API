using src.Data.IRepositories;
using src.Domain.Entities;
using src.Services.Extentions;
using src.Services.Helpers;
using src.Services.IServices;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace src.Services.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IUnitOfWork unitOfWork;

        public AttachmentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> DeleteAsync(Expression<Func<Attachment, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public Task<Attachment> UpdateAsync(int id, Stream stream)
        {

            throw new NotImplementedException();
        }

        /// <summary>
        /// uploading file to wwwroot and create attachment
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<Attachment> UploadAsync(Stream stream, string filename)
        {
            // store to wwwroot
            filename = Guid.NewGuid().ToString("N") + "-" + filename;
            string filePath = Path.Combine(EnvironmentHelper.AttachmentPath, filename);

            FileStream fileStream = File.Create(filePath);
            await stream.CopyToAsync(fileStream);

            await fileStream.FlushAsync();
            fileStream.Close();

            // store to database
            var attachment = new Attachment()
            {
                Name = Path.GetFileName(filePath),
                Path = Path.Combine(EnvironmentHelper.FilePath, Path.GetFileName(filePath)),
            };

            attachment.Create();

            return await unitOfWork.Attachments.CreateAsync(attachment);
        }
    }
}
