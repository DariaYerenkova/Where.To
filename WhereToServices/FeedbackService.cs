using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            var blobStorageModel = new List<EmptyBlobForSasGenerationModel>();
            foreach (var item in tourFeedback.FilePaths)
            {
                EmptyBlobForSasGenerationModel model = new EmptyBlobForSasGenerationModel();
                model.FilePath = item;
                // guid will be stored in the db 
                model.FileName = Guid.NewGuid().ToString();
                blobStorageModel.Add(model);
            }

            //Generate sas(write) key for file uploading
            var sasFilePathModel = new List<SasFilePathResponseModel>();
            foreach (var item in blobStorageModel)
            {
                SasFilePathResponseModel model = new SasFilePathResponseModel();
                model.FilePatht = item.FilePath;
                model.SasToken = blobService.GenerateSasTokenForUserFileName(item.FileName);
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

        public async Task UploadPhotoToBlob(string urlWithSasToken, byte[] content)
        {
            await blobService.UploadPhotoBySas(urlWithSasToken, content);
        }

        public async Task<FeddbackResponseModel> GetFeedbackAsync(int feedbackId)
        {
            var feedback = uow.TourFeedbacks.Get(feedbackId);

            if (feedback == null)
            {
                throw new HttpRequestException(null, null, HttpStatusCode.NotFound);
            }

            var feedbackAttachmentsSasUrls = new List<string>();

            foreach (var item in feedback.FeedbackPhotos) 
            {
                feedbackAttachmentsSasUrls.Add(blobService.GetBlobSasUrl(item.FileName));
            }

            var responseModel = new FeddbackResponseModel();
            responseModel.FeedbackId = feedbackId;
            responseModel.Comment = feedback.Comment;
            responseModel.AttachmentsSasUrls = feedbackAttachmentsSasUrls;

            return responseModel;
        }

        public async Task StreamPhotoToBlob(string blobName, Stream content)
        {
            await blobService.StreamPhotoToBlob(blobName, content);
        }
    }
}
