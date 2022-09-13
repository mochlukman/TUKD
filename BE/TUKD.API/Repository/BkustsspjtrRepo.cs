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
    public class BkustsspjtrRepo : Repo<Bkustsspjtr>, IBkustsspjtrRepo
    {
        public BkustsspjtrRepo(DbContext context) : base(context)
        {
        }
        TukdContext _tukdContext => _context as TukdContext;
        public async Task<BkustsspjtrView> ViewData(long Idbkustsspjtr)
        {
            BkustsspjtrView data = await (
                from src in _tukdContext.Bkustsspjtr
                join spjtr in _tukdContext.Spjtr on src.Idspjtr equals spjtr.Idspjtr
                join bkusts in _tukdContext.Bkusts on src.Idbkusts equals bkusts.Idbkusts
                where src.Idbkustsspjtr == Idbkustsspjtr
                select new BkustsspjtrView
                {
                    Idspjtr = src.Idspjtr,
                    Idbkusts = src.Idbkusts,
                    Datecreate = src.Datecreate,
                    Dateupdate = src.Dateupdate,
                    Idbkustsspjtr = src.Idbkustsspjtr,
                    IdspjtrNavigation = spjtr ?? null,
                    IdbkustsNavigation = bkusts ?? null,
                    Nilai = _tukdContext.Stsdetd.Where(w => w.Idsts == bkusts.Idsts).Select(s => s.Nilai).Sum()
                }
                ).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<BkustsspjtrView>> ViewDatas(long Idspjtr)
        {
            List<BkustsspjtrView> data = await (
                from src in _tukdContext.Bkustsspjtr
                join spjtr in _tukdContext.Spjtr on src.Idspjtr equals spjtr.Idspjtr
                join bkusts in _tukdContext.Bkusts on src.Idbkusts equals bkusts.Idbkusts
                where src.Idspjtr == Idspjtr
                select new BkustsspjtrView
                {
                    Idspjtr = src.Idspjtr,
                    Idbkusts = src.Idbkusts,
                    Datecreate = src.Datecreate,
                    Dateupdate = src.Dateupdate,
                    Idbkustsspjtr = src.Idbkustsspjtr,
                    IdspjtrNavigation = spjtr ?? null,
                    IdbkustsNavigation = bkusts ?? null,
                    Nilai = _tukdContext.Stsdetd.Where(w => w.Idsts == bkusts.Idsts).Select(s => s.Nilai).Sum()
                }
                ).ToListAsync();
            return data;
        }
    }
}
