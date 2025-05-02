using ExpenseTracker.Base;
namespace ExpenseTracker.Schema;

public class PersonnelAddressRequest
{
    public string? CountryCode { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? Street { get; set; }
    public string? ZipCode { get; set; }
    public bool IsDefault { get; set; }
}

public class PersonnelAddressResponse:BaseResponse
{  
    public long PersonnelId { get; set; }
    public string PersonnelName { get; set; }
    public string? CountryCode { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? Street { get; set; }
    public string? ZipCode { get; set; }
    public bool IsDefault { get; set; }
}
