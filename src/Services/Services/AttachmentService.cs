﻿using Data.IRepositories;
using Domain.Entities;
using Domain.Enums;
using Services.Helpers;
using Services.IServices;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.Services
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
            var attachment = await unitOfWork.Attachments.CreateAsync(new Attachment()
            {
                Name = Path.GetFileName(filePath),
                Path = Path.Combine(EnvironmentHelper.FilePath, Path.GetFileName(filePath)),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                State = State.Created
            });

            return attachment;
        }
    }
}