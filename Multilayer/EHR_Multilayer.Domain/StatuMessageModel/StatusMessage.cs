using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.Domain.StatuMessageModel
{
    public static class StatusMessage
    {


        #region Request
        public const string BadRequest = "Bad Request, Null parameter requested";
        #endregion
        // new code
        public const string Success = "Book has been inserted successfully ";
        public const string SuccessReview = "Review has been inserted successfully ";
        public const string NotFound = "Data Not Found";
        public const string EmailNotFound = "Email Not Found";
        public const string FetchMessage = "Fetching data from db";
        public const string UpdatedSuccessfully = "Data has been updated successfully";
        public const string LoginSuccess = "User Loggedin successfully ";
        public const string ChangeMessage = "Password send on you registered email";
        public const string ChangePasswordMessage = "Password has been updated";
        public const string SpecialityMessage = "Fetching Specialisations From DB";
















        public const string EmailAlreadyExist = "Email Already Exist";
        public const string UpdateEntityPermission = "Permissions has been updated successfully";
        public const string ChatConnectedEstablished = "Chat connection established";
        public const string VerifiedBusinessName = "Domain verified";
        public const string BusinessnameNotActivate = "This is not activated as of now,please contact with your system administrator";

        public const string UnVerifiedBusinessName = "Business name is invalid.";
        public const string InvalidUserOrPassword = "Invalid username or password.";
        public const string UnknownError = "Sorry, we have encountered an error";
       
        public const string UpdateEntity = "Data has been successfully updated";
        public const string UserCustomFieldSaved = "User's custom fields has been saved successfully";
        public const string AgencySaved = "Agency has been saved successfully.";
        public const string AgencyUpdatedSuccessfully = "Agency has been updated successfully.";
        public const string AgencyAlredyExist = "Agency name already in use";
        public const string SoapSuccess = "Client encounter has been saved successfully.";
        public const string UserCustomFieldUpdated = "User's custom fields has been updated successfully";
        public const string UserCustomFieldDeleted = "User's custom fields has been deleted successfully";
        public const string Delete = "Data has been deleted successfully";
        public const string InvalidData = "Please enter valid user name";
        public const string VaildData = "Please enter vaild data.";
        public const string ModelState = "Model state is not valid";
        public const string InvalidToken = "Please enter valid token";

 
        public enum HttpStatusCodes
        {
            // http://www.w3.org/Protocols/rfc2616/rfc2616-sec6.html#sec6.1.1

            Continue = 100,        // Section 10.1.1: Continue
            SwitchingProtocols = 101,        // Section 10.1.2: Switching Protocols

            OK = 200,                // Section 10.2.1: OK
            Created = 201,        // Section 10.2.2: Created
            Accepted = 202,        // Section 10.2.3: Accepted
            NonAuthoritativeInformation = 203,    // Section 10.2.4: Non-Authoritative Information
            NoContent = 204,        // Section 10.2.5: No Content
            ResetContent = 205,    // Section 10.2.6: Reset Content
            PartialContent = 206,    // Section 10.2.7: Partial Content
            MultipleChoices = 300,        // Section 10.3.1: Multiple Choices
            MovedPermanently = 301,        // Section 10.3.2: Moved Permanently
            Found = 302,            // Section 10.3.3: Found
            SeeOther = 303,        // Section 10.3.4: See Other
            NotModified = 304,    // Section 10.3.5: Not Modified
            UseProxy = 305,        // Section 10.3.6: Use Proxy
            TemporaryRedirect = 307,        // Section 10.3.8: Temporary Redirect
            NotFound = 404,
        
        }
    }
}
