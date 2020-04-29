using System.Collections.Generic;

namespace EasyQuizApi.Share.Dto
{
    public class MonHocDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MonHocResponseDto
    {
        public int TotalRow { get; set; }
        public List<MonHocDto> Data { get; set; }
    }
}