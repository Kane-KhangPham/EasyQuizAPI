using System;
using System.Collections.Generic;
using EasyQuizApi.Model;
using EasyQuizApi.Model.Entities;
using EasyQuizApi.Share.Dto;

namespace EasyQuizApi.Share.ModelExtention
{
    public static class QuestionExtention
    {
        public static void MappingData(this CauHoi cauHoi, QuestionCreateModel source)
        {
            cauHoi.Id = source.Id;
            cauHoi.Content = source.Question;
            cauHoi.Options = new List<Option>()
            {
                new Option()
                {
                    Content = source.OptionA,
                    IsAnswer = source.DapAn == "A",
                    Id = 0
                },
                new Option()
                {
                    Content = source.OptionB,
                    IsAnswer = source.DapAn == "B",
                    Id = 0
                },
                new Option()
                {
                    Content = source.OptionC,
                    IsAnswer = source.DapAn == "C",
                    Id = 0
                },
                new Option()
                {
                    Content = source.OptionD,
                    IsAnswer = source.DapAn == "D",
                    Id = 0
                }
            };
            cauHoi.CreatedDate = DateTime.Now;
            cauHoi.GiaoVienId = 1;
            cauHoi.MonHocId = source.MonHocId;
            cauHoi.Status = Status.GuiDuyet;
        }
    }
}