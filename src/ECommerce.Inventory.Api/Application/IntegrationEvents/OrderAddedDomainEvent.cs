namespace ECommerce.Inventory.Api.Application.IntegrationEvents
{
    public class OrderAddedDomainEvent
    {
        public int ProductId { get; set; }

        public int Quantity { get; set;  }
    }
}
