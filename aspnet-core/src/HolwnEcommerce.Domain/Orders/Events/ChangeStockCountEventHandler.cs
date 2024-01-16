using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace HolwnEcommerce.Orders.Events
{
    public class ChangeStockCountEventHandler : ILocalEventHandler<NewOrderCreatedEvent>,
          ITransientDependency
    {
        public async Task HandleEventAsync(NewOrderCreatedEvent eventData)
        {
            // Handler when create new order ---> Change current product stock
        }
    }
}