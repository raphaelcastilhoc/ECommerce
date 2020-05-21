namespace ECommerce.Location.IntegrationTests.SqlCommands
{
    public class AddressSqlCommands
    {
        public const string Insert = @"INSERT INTO [dbo].[Address] 
                                                    ([StreetName],
                                                     [Number],
                                                     [ZipCode],
                                                     [CityId])
                                              VALUES (@StreetName,
                                                      @Number,
                                                      @ZipCode,
                                                      @CityId);

                                               SELECT CAST(SCOPE_IDENTITY() as int)";
    }
}
