using API.DTOs;
using Core.Entities;

namespace API.Extensions
{

    public static class AddressMappingExtensions
    {
        public static AddressDto? ToDto(this Address? adress)
        {
            if (adress == null) return null;
            return new AddressDto
            {
                Line1 = adress.Line1,
                Line2 = adress.Line2,
                City = adress.City,
                State = adress.State,
                Country = adress.Country,
                PostalCode = adress.PostalCode,
            };
        }
        public static Address ToEntity(this AddressDto addressDto)
        {
            if (addressDto == null) throw new ArgumentNullException(nameof(addressDto));
            return new Address
            {
                Line1 = addressDto.Line1,
                Line2 = addressDto.Line2,
                City = addressDto.City,
                State = addressDto.State,
                Country = addressDto.Country,
                PostalCode = addressDto.PostalCode,
            };
        }
        public static void UpdateFromDto(this Address adress, AddressDto addressDto)
        {
            if (addressDto == null) throw new ArgumentNullException(nameof(addressDto));
            if (adress == null) throw new ArgumentNullException(nameof(adress));

            adress.Line1 = addressDto.Line1;
            adress.Line2 = addressDto.Line2;
            adress.City = addressDto.City;
            adress.State = addressDto.State;
            adress.Country = addressDto.Country;
            adress.PostalCode = addressDto.PostalCode;
        }
    }
}