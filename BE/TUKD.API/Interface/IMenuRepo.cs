using RKPD.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IMenuRepo : IRepo<Webrole>
    {
        Task<List<Menu>> GetMenuAdmin(long Idapp);
        Task<List<Menu>> GetMenuByGroupId(long groupid, long Idapp);
        Task<List<MenuTreeDto>> TreeMenuWeRole(long groupid);
    }
}
