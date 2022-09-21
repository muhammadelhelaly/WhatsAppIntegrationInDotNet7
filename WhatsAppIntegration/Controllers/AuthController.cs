using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhatsAppIntegration.Dtos;
using System.Net.Http.Headers;
using WhatsAppIntegration.Settings;
using Microsoft.Extensions.Options;
using WhatsAppIntegration.Models;
using WhatsAppIntegration.Services;

namespace WhatsAppIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IWhatsAppService _whatsAppService;

        public AuthController(IWhatsAppService whatsAppService)
        {
            _whatsAppService = whatsAppService;
        }


        [HttpPost("send-welcome-message")]
        public async Task<IActionResult> SendWelcomeMessage(SendMessageDto dto)
        {
            var language = Request.Headers["language"].ToString();

            var result = await _whatsAppService.SendMessage(dto.Mobile, language, "hello_world");

            if (!result)
                throw new Exception("Something went wrong!");

            return Ok("Sent successfully");
        }

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOTP(SendOTPDto dto)
        {
            var language = Request.Headers["language"].ToString();
            Random random = new();
            var otp = random.Next(0, 999999);

            var components = new List<WhatsAppComponent>
            {
                new WhatsAppComponent
                {
                    type = "body",
                    parameters = new List<object>
                    {
                        new { type = "text", text = dto.Name },
                        new { type = "text", text = otp.ToString("000000") },
                    }
                }
            };

            var result = await _whatsAppService.SendMessage(dto.Mobile, language, "send_otp_new", components);
            
            if (!result)
                throw new Exception("Something went wrong!");

            return Ok("Sent successfully");
        }
    }
}