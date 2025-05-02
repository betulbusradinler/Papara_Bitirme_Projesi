using ExpenseTracker.Base;
namespace ExpenseTracker.Schema;

public class PersonnelPhoneRequest
{
    public string CountryCode { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsDefault { get; set; }
}

public class PersonnelPhoneResponse:BaseResponse
{  
    public long PersonnelId { get; set; }
    public string PersonnelName { get; set; }
    public string CountryCode { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsDefault { get; set; }
}
