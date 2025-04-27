using AutoMapper;
using ExpenseTracker.Domain;
using ExpenseTracker.Schema;

namespace ExpenseTracker.Mapper;

public class MapperConfiguration:Profile
{
    public MapperConfiguration()
    {
        CreateMap<PaymentCategoryRequest, PaymentCategory>();
        CreateMap<PaymentCategory, PaymentCategoryResponse>();
    }
}