namespace ECommerce.Location.IntegrationTests.DTOs
{
    public class AddressDTO
    {
        public AddressDTO(string streetName,
            int number,
            int zipCode,
            int cityId)
        {
            StreetName = streetName;
            Number = number;
            ZipCode = zipCode;
            CityId = cityId;
        }

        public int Id { get; set; }

        public string StreetName { get; set; }

        public int Number { get; set; }

        public int ZipCode { get; set; }

        public int CityId { get; set; }
    }
}