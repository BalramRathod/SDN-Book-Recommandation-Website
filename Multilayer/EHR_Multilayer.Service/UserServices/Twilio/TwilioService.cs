using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace EHR_Multilayer.Service.UserServices.Twilio
{
    public  class TwilioService
    {
        public void SendOtpOnRegisterdMoileNo(int Otp)
        {


            // var accountSid = "AC545e89c133041fbad5eba78545e5c8b1";
            // authToken ="35f9cec0abd33383eaf6c4a4ea4e09af";
            // above my crediancial

            var accountSid = "AC545e89c133041fbad5eba78545e5c8b1";
            var authToken = "35f9cec0abd33383eaf6c4a4ea4e09af";
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
             new PhoneNumber("+91" + 918103761917));

            messageOptions.From = new PhoneNumber("+12512202503");
            messageOptions.Body = "Two Factor Authentication OTP is:" + Otp;

            var message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body);
        }
    }
}
