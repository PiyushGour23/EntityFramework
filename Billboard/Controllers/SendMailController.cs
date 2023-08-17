using Billboard.Container;
using Billboard.Data;
using Billboard.Helper;
using Billboard.Models;
using Billboard.Service;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Billboard.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IEmailService _emailService;
        public SendMailController(UserDbContext context, IEmailService emailService)  // this way we will get object of DbContext at runtime
        {
            _context = context;
            _emailService = emailService;
        }
        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail()
        {
            try
            {
                MailRequest mailRequest = new MailRequest();
                mailRequest.ToEmail = "piyushgour2301@gmail.com";
                mailRequest.Subject = "Welcome to Billboard";
                mailRequest.Body = GetHtmlContent();
                await _emailService.SendEmail(mailRequest);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetHtmlContent()
        {
            string Response = "<div style=\"width:100%;background-color:red;text-align:center;margin:10px\">";
            Response += "<h1>Welcome to Billboard Application</h1>";
            Response += "<img src=\"https://th.bing.com/th/id/OIP.j1Um8Nl-5tkrKLtGP1SBvQAAAA?pid=ImgDet&rs=1\" />";
            Response += "<h2>Thanks for using our application</h2>";
            Response += "<a href=\"https://www.google.com\">Please visit our application</a>";
            Response += "<div><h1> Contact us : piyushgour2301@gmail.com</h1></div>";
            Response += "</div>";
            return Response;
        }

    }
}
