namespace JWT.Helper
{
    public class APIResponse<type>
    {
        public string Code { get; set; }
        public string State { get; set; }
        public string Message { get; set; }
        public type Data { get; set; }
        public string Error { get; set; }
    }
}
