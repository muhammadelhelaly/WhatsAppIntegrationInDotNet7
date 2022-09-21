using System.ComponentModel;
using WhatsAppIntegration.Models;

namespace WhatsAppIntegration.Services
{
    public interface IWhatsAppService
    {
        Task<bool> SendMessage(string mobile, string language, string template, List<WhatsAppComponent>? components = null);
    }
}