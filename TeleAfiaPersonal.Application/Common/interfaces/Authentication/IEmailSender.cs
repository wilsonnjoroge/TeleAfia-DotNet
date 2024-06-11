

using TeleAfiaPersonal.Contracts.MessageDTO;

namespace TeleAfiaPersonal.Application.Common.interfaces.Authentication
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
