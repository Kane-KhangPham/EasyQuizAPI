﻿using EasyQuizApi.Share.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyQuizApi.Data.RepositoryBase
{
    public interface IDeThiRepository
    {
        Task<List<ObjectReference>> GetListLopHoc();
        Task<List<ObjectReference>> GetListKiThi();
        Task<List<ObjectReference>> GetListMonHoc(string filter);
        int CreateDeThi(DeThiNewDto data);
        DeThiListResponseDto GetListDeThi(DeThiFilterDto data);
        DeThiDetailDto GetDeThiDetail(int id);
        int UpdateDeThi(DeThiNewDto data);
    }
}
