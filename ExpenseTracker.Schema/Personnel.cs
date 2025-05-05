using ExpenseTracker.Base;
namespace ExpenseTracker.Schema;

public class PersonnelRequest
{
    public string UserName {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public string Role {get; set;}
    public string Email {get; set;}
    public string Iban { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm {get; set;}
    public PersonnelAddressRequest? PersonnelAddressRequest {get; set;}
    public PersonnelPhoneRequest? PersonnelPhoneRequest {get; set;}
}

public class PersonnelResponse:BaseResponse
{  
    public string UserName {get; set;}
    public string FirstName {get; set;}
    public string Iban { get; set; }
    public string LastName { get; set; }
    public string Role {get; set;}
    public string Email {get; set;}
    public List<PersonnelAddressResponse>? PersonnelAddressResponses { get; set; }
    public List<PersonnelPhoneResponse>? PersonnelPhoneResponses { get; set; }
}
