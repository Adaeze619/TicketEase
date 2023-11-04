using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketEase.Application.Interfaces.Services;
using TicketEase.Domain.Entities;

namespace TicketEase.Controllers
{
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]

    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailController> _logger;

        public EmailController(IEmailService emailService, ILogger<EmailController> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        [HttpPost("SendMail")]
       
        public async Task<IActionResult> SendMail()
        {
            try
            {//"jemmimahabdul@gmail.com";
                //"chuksinnocent1@gmail.com"
                MailRequest mailRequest = new MailRequest();
                mailRequest.ToEmail = "kely2000sgm@gmail.com";
                mailRequest.Subject = "Welcome to Tech world";
                mailRequest.Body = GetHtmlContent();
                await _emailService.SendEmailAsync(mailRequest);
                return Ok(mailRequest);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Email sending failed."); //logger not fully implemented

                // Return an error response to the client
                return StatusCode(500, "Email sending failed. Please try again later.");
            }


        }
        private string GetHtmlContent()
        {
            string response = "<div style=\"width:100%; background-color: lightblue; text-align:center;\">";
            response += "<h1>Welcome To SQD 17 Ticketing app</h1>";
            response += "<img src=\"https://www.google.com/imgres?imgurl=https%3A%2F%2Fusa.bootcampcdn.com%2Fwp-content%2Fuploads%2Fsites%2F108%2F2021%2F12%2Ftes_gen_blog_post_071921_1233182206-1-800x412.jpg&tbnid=zOY9WNHB7uLPnM&vet=12ahUKEwis_Irgw6iCAxVSpycCHSXNDWsQMygLegUIARCEAQ..i&imgrefurl=https%3A%2F%2Fbootcamp.cvn.columbia.edu%2Fblog%2Fsoftware-engineer-vs-developer%2F&docid=iVux8Sn2xBjURM&w=800&h=412&q=developers&ved=2ahUKEwis_Irgw6iCAxVSpycCHSXNDWsQMygLegUIARCEAQ\" alt=\"Your Image\">";
            response += "<h2>Thanks for Registering with us</h2>";
            response += "<a href=\"https://www.youtube.com/watch?v=VkrKNXscoto&list=PL6n9fhu94yhWi8K02Eqxp3Xyh_OmQ0Rp6&index=3\">Please Join us by clicking this link</a>";

            response += "<div><h1>Contact us: everythingtech@gmail.com</h1></div>";
            response += "</div>";

            return response;
        }

    }
}
