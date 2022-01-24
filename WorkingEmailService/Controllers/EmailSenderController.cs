using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkingEmailService.Services;

namespace WorkingEmailService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailSenderController : ControllerBase
    {
       
        private readonly ILogger<EmailSenderController> _logger;
        private readonly IEmailService _emailService;

        public EmailSenderController(ILogger<EmailSenderController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string emailAddress, string subject, string content)
        {
           
            try
            {
                var message = new Message(new string[] { emailAddress }, subject, content);
                await _emailService.SendEmailAsync(message);
                return Ok("An email has been sent to you. Please check and verify that it's you");
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
