using HolwnEcommerce.Emailing;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.EventBus;
using Volo.Abp.TextTemplating;

namespace HolwnEcommerce.Orders.Events
{
    public class SendMailToCustomerEventHandler : ILocalEventHandler<NewOrderCreatedEvent>,
          ITransientDependency
    {
        private readonly IEmailSender _emailSender;
        private readonly ITemplateRenderer _templateRenderer;

        public SendMailToCustomerEventHandler(IEmailSender emailSender, ITemplateRenderer templateRenderer)
        {
            _emailSender = emailSender;
            _templateRenderer = templateRenderer;
        }

        public async Task HandleEventAsync(NewOrderCreatedEvent eventData)
        {
            var emailBody = await _templateRenderer.RenderAsync(
                        EmailTemplates.CreateOrderEmail,
                        new
                        {
                            message = eventData.Message,
                        });
            await _emailSender.SendAsync(eventData.CustomerEmail, "Tạo đơn hàng thành công", emailBody);
        }
    }
}