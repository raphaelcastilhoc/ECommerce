using MediatR;
using System;
using System.Collections.Generic;

namespace ECommerce.Location.Api.Application.Queries
{
    public class GetLocationByZipCodeQuery : IRequest<IEnumerable<GetLocationByZipCodeQueryResult>>
    {
        public GetLocationByZipCodeQuery(int zipCode)
        {
            ZipCode = zipCode;
        }

        public int ZipCode { get; set; }
    }

    public class GetLocationByZipCodeQueryResult
    {
        public int LocationId { get; set; }

        public string StreetName { get; set; }

        public string Number { get; set; }

        public int ZipCode { get; set; }

        public string CityName { get; set; }

        public string StateName { get; set; }

        public string CountryName { get; set; }

        internal bool Any()
        {
            throw new NotImplementedException();
        }
    }
}
