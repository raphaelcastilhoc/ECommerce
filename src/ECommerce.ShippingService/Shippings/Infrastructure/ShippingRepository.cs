namespace ECommerce.ShippingService.Shippings.Infrastructure
{
    public class ShippingRepository
    {
        public async Task AddAsync(Shipping shipping)
        {
        }

        public async Task<Shipping> GetByOrderIdAsync(int orderId)
        {
            //TODO: Just for test. Replace for query on storage
            return Shipping.Create(0, DateTime.Now, DateTime.Now, 10, 10).Value;
        }
    }
}
