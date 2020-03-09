namespace EasyQuizApi.Share.Dto
{
    public class ResponseBase
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}