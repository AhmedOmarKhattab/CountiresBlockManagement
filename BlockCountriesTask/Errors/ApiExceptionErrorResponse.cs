
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockCountriesTask.Response;

namespace BlockCountriesTask.Errors
{
    public class ApiExceptionErrorResponse : ApiResponse
    {
        public string? Details { get; set; }
        public ApiExceptionErrorResponse(int statuscode, string errormessage = null
            , string? details = null) : base(statuscode, errormessage)
        {

            Details = details;

        }
    }
}
