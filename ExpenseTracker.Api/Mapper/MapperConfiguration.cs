using AutoMapper;
using ExpenseTracker.Api.Domain;
using ExpenseTracker.Schema;
using ExpenseTracker.Base;

namespace ExpenseTracker.Api.Mapper;

public class MapperConfiguration:Profile
{
    public MapperConfiguration()
    {
        CreateMap<PaymentCategoryRequest, PaymentCategory>();
        CreateMap<PaymentCategory, PaymentCategoryResponse>();
        CreateMap<PersonnelRequest, Personnel>()
            .ForMember(dest => dest.PersonnelPassword, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                HashHelper.CreatePasswordHash(src.Password, out string hash, out string salt);
                dest.PersonnelPassword = new PersonnelPassword
                {
                    Password = hash,
                    Secret = salt
                };
            })   
            .ForMember(dest => dest.PersonnelAddresses, opt => opt.MapFrom(src =>
                src.PersonnelAddressRequest != null
                    ? new List<PersonnelAddress> { new PersonnelAddress
                        {
                            CountryCode = src.PersonnelAddressRequest.CountryCode,
                            City = src.PersonnelAddressRequest.City,
                            District = src.PersonnelAddressRequest.District,
                            Street = src.PersonnelAddressRequest.Street,
                            ZipCode = src.PersonnelAddressRequest.ZipCode,
                            IsDefault = src.PersonnelAddressRequest.IsDefault
                        } }
                    : new List<PersonnelAddress>()
            ))
            .ForMember(dest => dest.PersonnelPhones, opt => opt.MapFrom(src =>
                src.PersonnelPhoneRequest != null
                    ? new List<PersonnelPhone> { new PersonnelPhone
                        {
                            CountryCode = src.PersonnelPhoneRequest.CountryCode,
                            PhoneNumber = src.PersonnelPhoneRequest.PhoneNumber,
                            IsDefault = src.PersonnelPhoneRequest.IsDefault
                        } }
                    : new List<PersonnelPhone>()
            ));      

        CreateMap<PersonnelAddressRequest, PersonnelAddress>();
        CreateMap<PersonnelAddress, PersonnelAddressResponse>();

        CreateMap<PersonnelPhoneRequest, PersonnelPhone>();
        CreateMap<PersonnelPhone, PersonnelPhoneRequest>();

        CreateMap<Personnel, PersonnelResponse>();

    }
}