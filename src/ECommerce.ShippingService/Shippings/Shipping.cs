using CSharpFunctionalExtensions;
using ECommerce.SeedWork;
using ECommerce.ShippingService.Shippings.DomainEvents;

namespace ECommerce.ShippingService.Shippings
{
    public class Shipping : SeedWork.Entity<Guid>, IAggregateRoot
    {
        private const decimal _costPerMile = 0.50m;

        private Shipping(int orderId,
            DateTime sendDate, 
            DateTime expectedDeliveryDate, 
            decimal loadWeight,
            decimal distance)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            SendDate = sendDate;
            ExpectedDeliveryDate = expectedDeliveryDate;
            LoadWeight = loadWeight;
            Distance = distance;
            Cost = CalculateCost(distance);
        }

        public int OrderId { get; }

        public DateTime SendDate { get; }

        public DateTime ExpectedDeliveryDate { get; }

        public decimal LoadWeight { get; }

        public decimal Distance { get; }

        public decimal Cost { get; }

        public static Result<Shipping> Create(int orderId,
            DateTime sendDate,
            DateTime expectedDeliveryDate,
            decimal loadWeight,
            decimal distance)
        {
            var validation = Validate(sendDate, expectedDeliveryDate, loadWeight, distance);
            if (validation.IsFailure)
            {
                return Result.Failure<Shipping>(validation.Error);
            }

            var shipping = new Shipping(orderId, sendDate, expectedDeliveryDate, loadWeight, distance);
            shipping.AddDomainEvent(new ShippingAddedDomainEvent(shipping.Id));

            return shipping;
        }

        private static Result Validate(DateTime sendDate,
            DateTime expectedDeliveryDate,
            decimal loadWeight,
            decimal distance)
        {
            var validationResults = new List<Result>();

            if(expectedDeliveryDate < DateTime.Now || expectedDeliveryDate < sendDate)
            {
                validationResults.Add(Result.Failure("Expected delivery date cannot be less than date now or send date"));
            }

            if(loadWeight <= 0)
            {
                validationResults.Add(Result.Failure("Load weight cannot be less or equal 0"));
            }

            if (distance <= 0)
            {
                validationResults.Add(Result.Failure("Distance cannot be less or equal 0"));
            }

            return Result.Combine(validationResults);
        }

        private decimal CalculateCost(decimal distance)
        {
            return distance * _costPerMile;
        }

        public bool IsLate()
        {
            return DateTime.Now > ExpectedDeliveryDate;
        }
    }
}
