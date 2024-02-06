using AutoMapper;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;
using WhereToDataAccess.Interfaces;
using WhereToServices.DTOs;
using WhereToServices.Interfaces;

namespace WhereToServices
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IBlobService blobService;
        private static readonly FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();

        public FeedbackService(IUnitOfWork uow, IMapper mapper, IBlobService blobService)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.blobService = blobService;
        }

        public async Task CreateFeedback(FeedbackDto tourFeedback)
        {
            // add feedback to db table
            var feedback = mapper.Map<FeedbackDto, TourFeedback>(tourFeedback);
            uow.TourFeedbacks.Create(feedback);
            uow.Save();

            var blobStorageModel = new List<BlobStorageModel>();
            foreach (var item in tourFeedback.FilePaths)
            {
                BlobStorageModel model = new BlobStorageModel();
                model.FilePath = item;
                model.FileContent = provider.TryGetContentType(item, out var contentType) ? contentType : "application/octet-stream";
                model.FileName = Guid.NewGuid().ToString();
                blobStorageModel.Add(model);
            }

            //send to blob storage
            foreach (var item in blobStorageModel)
            {
                await blobService.UploadFileBlobAsync(item.FilePath, item.FileName, item.FileContent);
            }            

            //add attachments to db table
            var blobStorageAttachments = new List<BlobAttachments_feedbackphotos>();
            foreach (var item in blobStorageModel)
            {
                BlobAttachments_feedbackphotos model = new BlobAttachments_feedbackphotos();
                model.TourFeedbackId = feedback.Id;
                model.FileName = item.FileName;
                blobStorageAttachments.Add(model);
            }

            uow.BlobAttachments.CreateRange(blobStorageAttachments);
            uow.Save();
        }

        public TourFeedback GetTourFeedbackByUserId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
