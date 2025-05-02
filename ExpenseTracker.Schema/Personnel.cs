using ExpenseTracker.Base;
namespace ExpenseTracker.Schema;

public class PersonnelRequest
{
    public string UserName {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public string Role {get; set;}
    public string Email {get; set;}
    public string Password {get; set;}
    public string PasswordConfirm {get; set;}
    public PersonnelAddressRequest? PersonnelAddressRequest {get; set;}
    public PersonnelPhoneRequest? PersonnelPhoneRequest {get; set;}
}

public class PersonnelResponse:BaseResponse
{  
    public string UserName {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public string Role {get; set;}
    public string Email {get; set;}
    public List<PersonnelAddressRequest>? PersonnelAddressRequest {get; set;} 
    public List<PersonnelPhoneRequest>? PersonnelPhoneRequest {get; set;}
}
