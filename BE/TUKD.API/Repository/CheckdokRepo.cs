﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Repository
{
    public class CheckdokRepo : Repo<Checkdok>, ICheckdokRepo
    {
        public CheckdokRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Checkdok param)
        {
            Checkdok data = await _tukdContext.Checkdok.Where(w => w.Idcheck == param.Idcheck).FirstOrDefaultAsync();
            if (data == null)
                return false;
            data.Uraian = param.Uraian;
            _tukdContext.Checkdok.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<Checkdok> ViewData(long Idcheck)
        {
            Checkdok Result = await (
                from data in _tukdContext.Checkdok
                join kode in _tukdContext.Zkode on data.Idxkode equals kode.Idxkode
                where data.Idcheck == Idcheck
                select new Checkdok
                {
                    Idcheck = data.Idcheck,
                    Idxkode = data.Idxkode,
                    Uraian = data.Uraian,
                    IdxkodeNavigation = kode ?? null
                }
                ).FirstOrDefaultAsync();
            return Result;
        }

        public async Task<List<Checkdok>> ViewDatas(CheckdokGet param)
        {
            List<Checkdok> Result = new List<Checkdok>();
            IQueryable<Checkdok> query = (
                from data in _tukdContext.Checkdok
                join kode in _tukdContext.Zkode on data.Idxkode equals kode.Idxkode
                select new Checkdok
                {
                    Idcheck = data.Idcheck,
                    Idxkode = data.Idxkode,
                    Uraian = data.Uraian,
                    IdxkodeNavigation = kode ?? null
                }
                ).AsQueryable();
            if(param.Idxkode.ToString() != "0")
            {
                query = query.Where(w => w.Idxkode == param.Idxkode).AsQueryable();
            }
            if(param.Idspp.ToString() != "0")
            {
                List<long> Idchecks = await (
                    from sppcheckdox in _tukdContext.Sppcheckdok
                    join spp in _tukdContext.Spp on sppcheckdox.Idspp equals spp.Idspp
                    where spp.Idspp == param.Idspp && spp.Idxkode == param.Idxkode
                    select sppcheckdox.Idcheck
                    ).ToListAsync();
                if(Idchecks.Count() > 0)
                {
                    query = query.Where(w => !Idchecks.Contains(w.Idcheck)).AsQueryable();
                }
            }
            if (param.Idsp2d.ToString() != "0")
            {
                List<long> Idchecks = await (
                    from sp2dcheckdox in _tukdContext.Sp2dcheckdok
                    join sp2d in _tukdContext.Sp2d on sp2dcheckdox.Idsp2d equals sp2d.Idsp2d
                    where sp2d.Idsp2d == param.Idsp2d && sp2d.Idxkode == param.Idxkode
                    select sp2dcheckdox.Idcheck
                    ).ToListAsync();
                if (Idchecks.Count() > 0)
                {
                    query = query.Where(w => !Idchecks.Contains(w.Idcheck)).AsQueryable();
                }
            }
            Result = await query.ToListAsync();
            return Result;
        }
    }
}
