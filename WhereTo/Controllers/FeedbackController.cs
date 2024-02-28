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

        [HttpGet("{feedbackId}")]
        public async Task<ActionResult<FeddbackResponseModel>> GetFeedback(int feedbackId)
        {
            var data = await feedbackService.GetFeedbackAsync(feedbackId);
            return Ok(data);
        }

        [HttpPost("uploadfeedback")]
        public async Task<IActionResult> UploadFeedback([FromBody] FeedbackDto model)
        {
            var response = await feedbackService.CreateFeedback(model);
            return Ok(response);
        }

        [HttpPost("uploadphoto")]
        public async Task<IActionResult> UploadPhotoBySas(string sasUrlWithToken, [FromBody] byte[] content)
        {
            await feedbackService.UploadPhotoToBlob(sasUrlWithToken, content);
            return Ok();
        }

        [HttpPost("streamphoto")]
        public async Task<IActionResult> StreamPhoto(string blobName)
        {
            await feedbackService.StreamPhotoToBlob(blobName, Request.Body);
            return Ok();
        }
    }
}
