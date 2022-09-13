using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Dto;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class RkadanarRepo : Repo<Rkadanar>, IRkadanarRepo
    {
        public RkadanarRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Rkadanar param)
        {
            Rkadanar data = await _tukdContext.Rkadanar.Where(w => w.Idrkadanar == param.Idrkadanar).FirstOrDefaultAsync();
            if (data == null)
                return false;
            data.Nilai = param.Nilai;
            data.Updateby = param.Updateby;
            data.Updatetime = param.Updatetime;
            _tukdContext.Rkadanar.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<RkadanarView> ViewData(long Idrkadana)
        {
           RkadanarView Result = await (
               from data in _tukdContext.Rkadanar
               join rka in _tukdContext.Rkar on data.Idrkar equals rka.Idrkar
               join jdana in _tukdContext.Jdana on data.Idjdana equals jdana.Idjdana
               where data.Idrkadanar == Idrkadana
               select new RkadanarView
               {
                   Createddate = data.Createddate,
                   Updatetime = data.Updatetime,
                   Createdby = data.Createdby,
                   Updateby = data.Updateby,
                   Idjdana = data.Idjdana,
                   Nilai = data.Nilai,
                   IdrkarNavigation = rka ?? null,
                   IdjdanaNavigation = jdana ?? null,
                   Rkadanarx = !String.IsNullOrEmpty(data.Idrkadanarx.ToString()) ? _tukdContext.Rkadanar.Where(w => w.Idrkadanar == data.Idrkadanarx).FirstOrDefault() : null,
                   Idrkadanarx = data.Idrkadanarx,
                   Idrkadanar = data.Idrkadanar,
                   Idrkar = data.Idrkar
               }
               ).FirstOrDefaultAsync();
            return Result;
        }

        public async Task<List<RkadanarView>> ViewDatas(long Idrka)
        {
            List<RkadanarView> Result = await (
                from data in _tukdContext.Rkadanar
                join rka in _tukdContext.Rkar on data.Idrkar equals rka.Idrkar
                join jdana in _tukdContext.Jdana on data.Idjdana equals jdana.Idjdana
                where data.Idrkar == Idrka
                select new RkadanarView
                {
                    Createddate = data.Createddate,
                    Updatetime = data.Updatetime,
                    Createdby = data.Createdby,
                    Updateby = data.Updateby,
                    Idjdana = data.Idjdana,
                    Nilai = data.Nilai,
                    IdrkarNavigation = rka ?? null,
                    IdjdanaNavigation = jdana ?? null,
                    Rkadanarx = !String.IsNullOrEmpty(data.Idrkadanarx.ToString()) ? _tukdContext.Rkadanar.Where(w => w.Idrkadanar == data.Idrkadanarx).FirstOrDefault() : null,
                    Idrkadanarx = data.Idrkadanarx,
                    Idrkadanar = data.Idrkadanar,
                    Idrkar = data.Idrkar
                }
                ).ToListAsync();
            return Result;
        }
    }
}
