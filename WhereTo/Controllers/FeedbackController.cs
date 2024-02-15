using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.DotNet.Scaffolding.Shared;
using WhereToServices.DTOs;
using WhereToServices.Interfaces;

namespace WhereTo.Controllers
{
    [Route("api/feedback")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IBlobService blobService;
        private readonly IFeedbackService feedbackService;
        private static readonly FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();


        public FeedbackController(IBlobService blobService, IFeedbackService feedbackService)
        {
            this.blobService = blobService;
            this.feedbackService = feedbackService;

        }

        [HttpGet("{blobName}")]
        public async Task<IActionResult> GetBlob(string blobName)
        {
            var data = await blobService.GetBlobAsync(blobName);
            return File(data.Content.ToStream(), data.Details.ContentType);
        }

        //[HttpPost("uploadfile")]
        //public async Task<IActionResult> UploadFile()
        //{
        //    BlobStorageModel model = new BlobStorageModel();
        //    model.FilePath = @"C:\\Users\\dnazare\\Downloads\\IMG_4027.jpg";
        //    model.FileName = "IMG_9";
        //    model.FileContent = provider.TryGetContentType(model.FilePath, out var contentType) ? contentType : "application/octet-stream";
        //    await blobService.UploadFileBlobAsync(model.FilePath, model.FileName, model.FileContent);
        //    return Ok();
        //}

        [HttpPost("uploadfeedback")]
        public async Task<IActionResult> UploadFeedback([FromBody] FeedbackDto model)
        {
            var response = await feedbackService.CreateFeedback(model);
            return Ok(response);
        }

        [HttpPost("uploadphoto{sasToken}")]
        public async Task<IActionResult> UploadPhotoBySas(string sasToken, [FromBody] UploadPhotoUsingSasModel model)
        {
            await feedbackService.UploadPhotoToBlob(sasToken, model);
            return Ok();
        }
    }
}
