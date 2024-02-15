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

        public async Task<List<SasFilePathResponseModel>> CreateFeedback(FeedbackDto tourFeedback)
        {
            // add feedback to db table
            var feedback = mapper.Map<FeedbackDto, TourFeedback>(tourFeedback);
            uow.TourFeedbacks.Create(feedback);
            await uow.SaveAsync();

            var blobStorageModel = new List<BlobStorageModel>();
            foreach (var item in tourFeedback.FilePaths)
            {
                BlobStorageModel model = new BlobStorageModel();
                model.FilePath = item;
                model.FileContent = provider.TryGetContentType(item, out var contentType) ? contentType : "application/octet-stream";
                // guid will be stored in the db 
                model.FileName = Guid.NewGuid().ToString();
                blobStorageModel.Add(model);
            }

            //send to blob storage
            //foreach (var item in blobStorageModel)
            //{
            //    await blobService.UploadFileBlobAsync(item.FilePath, item.FileName, item.FileContent);
            //}

            //Generate sas(write) key for file uploading
            var sasFilePathModel = new List<SasFilePathResponseModel>();
            foreach (var item in blobStorageModel)
            {
                SasFilePathResponseModel model = new SasFilePathResponseModel();
                model.FilePatht = item.FilePath;
                model.SasToken = await blobService.GenerateSasTokenForUserFileName(item.FileName);
                model.GuidFileName = item.FileName;
                sasFilePathModel.Add(model);
            }

            //add attachments Guid to db table
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

            return sasFilePathModel;
        }

        public async Task UploadPhotoToBlob(string token, UploadPhotoUsingSasModel content)
        {
            await blobService.UploadPhotoBySas(token, content);
        }

        public TourFeedback GetTourFeedbackByUserId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
