using System.Collections.Generic;

namespace EasyQuizApi.Share.Dto
{
    public class DeThiListDto
    {
        public int DeThiRootId { get; set; }
        public string MonHoc { get; set; }
        public string KyThi { get; set; }
        public string LopThi { get; set; }
        public string SoDe { get; set; }
        public int DeThiId { get; set; }
    }

    public class DeThiListResponseDto
    {
        public int TotalRow { get; set; }
        public List<DeThiListDto> Data { get; set; }
    }
}