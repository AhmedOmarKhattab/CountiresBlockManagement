namespace BlockCountriesTask.Response
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
      
        public ApiResponse(int statuscode, string? message = null)
        {

            StatusCode = statuscode;
            Message = message ?? GetDefaultMesaageForStatusCode(StatusCode);

        }

        private string? GetDefaultMesaageForStatusCode(int state)
        {
            return state switch
            {
             //   201=>"Created Successfully",
                200=>"Successfully",
                400 => "A BadRequest, You have made",
                401 => "Authourized, you are not",
                404 => "Resource was not found",
                505 => "Errors are got tos the dark side",
                _ => null
            };
        }
    }
}
