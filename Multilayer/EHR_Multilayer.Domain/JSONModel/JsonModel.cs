using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHR_Multilayer.Domain.JSONModel
{
    public class JsonModel
    {

        public JsonModel()
        {

        }
        // parameterized const..
        public JsonModel(object responseData,string token, string message, int statusCode, string appError = "")
        {
            data = responseData;
            AccessToken = token;
            Message = message;
            StatusCode = statusCode;
            AppError = appError;
        }

        public object data { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public string AppError { get; set; }
    }
}
