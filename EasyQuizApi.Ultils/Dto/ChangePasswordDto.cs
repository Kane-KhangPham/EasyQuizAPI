namespace EasyQuizApi.Share.Dto
{
    public class ChangePasswordDto
    {
        public string AccountName { get; set; }
        public string OldPass { get; set; }
        public string NewPass { get; set; }
    }
}