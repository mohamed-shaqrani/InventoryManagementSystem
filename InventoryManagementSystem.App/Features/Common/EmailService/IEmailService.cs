namespace InventoryManagementSystem.App.Features.Common.EmailService
{
    public interface IEmailServices
    {
        void SendEmail(string to, string subject, string body, bool isBodyHtml = false);
    }
}
