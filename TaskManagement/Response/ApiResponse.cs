namespace TaskManagement.Response
{
    public class ApiResponse
    {
        public object Response { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public bool Status { get; set; }
        public int StatusCode { get; set; }
    }
}
