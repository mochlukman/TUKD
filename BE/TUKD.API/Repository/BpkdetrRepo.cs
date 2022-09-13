using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Dto;
using TUKD.API.Interface;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Repository
{
    public class BpkdetrRepo : Repo<Bpkdetr>, IBpkdetrRepo
    {
        public BpkdetrRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<PrimengTableResult<Bpkdetr>> Paging(PrimengTableParam<BpkdetrGet> param)
        {
            PrimengTableResult<Bpkdetr> Result = new PrimengTableResult<Bpkdetr>();
            IQueryable<Bpkdetr> query = (
                    from data in _tukdContext.Bpkdetr
                    join bpk in _tukdContext.Bpk on data.Idbpk equals bpk.Idbpk
                    join jtrnlkas in _tukdContext.Jtrnlkas on data.Idnojetra equals jtrnlkas.Idnojetra into jtrnlkasMatch
                    from jtrnlkas_data in jtrnlkasMatch.DefaultIfEmpty()
                    join rekening in _tukdContext.Daftrekening on data.Idrek equals rekening.Idrek
                    select new Bpkdetr
                    {
                        Idbpkdetr = data.Idbpkdetr,
                        Idbpk = data.Idbpk,
                        Idnojetra = data.Idnojetra,
                        IdnojetraNavigation = jtrnlkas_data ?? null,
                        Idrek = data.Idrek,
                        IdrekNavigation = rekening ?? null,
                        Datecreate = data.Datecreate,
                        Nilai = data.Nilai,
                        Dateupdate = data.Dateupdate,
                        Idkeg = data.Idkeg,
                        IdbpkNavigation = bpk ?? null
                    }
                ).AsQueryable();
            if (param.Parameters.Idbpk.ToString() != "0")
            {
                query = query.Where(w => w.Idbpk == param.Parameters.Idbpk).AsQueryable();
            }
            if (param.Parameters.Idkeg.ToString() != "0")
            {
                query = query.Where(w => w.Idkeg == param.Parameters.Idkeg).AsQueryable();
            }
            Result.Data = await query.Skip(param.Start).Take(param.Rows).ToListAsync();
            Result.Totalrecords = await query.CountAsync();
            return Result;
        }

        public async Task<bool> Update(Bpkdetr param)
        {
            Bpkdetr data = await _tukdContext.Bpkdetr.Where(w => w.Idbpkdetr == param.Idbpkdetr).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Dateupdate = param.Dateupdate;
            data.Nilai = param.Nilai;
            _tukdContext.Bpkdetr.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<Bpkdetr> ViewData(long Idbpkdetr)
        {
            Bpkdetr Result = await (
                    from data in _tukdContext.Bpkdetr
                    join jtrnlkas in _tukdContext.Jtrnlkas on data.Idnojetra equals jtrnlkas.Idnojetra into jtrnlkasMatch from jtrnlkas_data in jtrnlkasMatch.DefaultIfEmpty()
                    join rekening in _tukdContext.Daftrekening on data.Idrek equals rekening.Idrek
                    where data.Idbpkdetr == Idbpkdetr
                    select new Bpkdetr
                    {
                        Idbpkdetr = data.Idbpkdetr,
                        Idbpk = data.Idbpk,
                        Idnojetra = data.Idnojetra,
                        IdnojetraNavigation = jtrnlkas_data ?? null,
                        Idrek = data.Idrek,
                        IdrekNavigation = rekening ?? null,
                        Datecreate = data.Datecreate,
                        Nilai = data.Nilai,
                        Dateupdate = data.Dateupdate,
                        Idkeg = data.Idkeg
                    }
                ).FirstOrDefaultAsync();
            return Result;
        }

        public async Task<List<Bpkdetr>> ViewDatas(BpkdetrGet param)
        {
            List<Bpkdetr> Result = new List<Bpkdetr>();
            IQueryable<Bpkdetr> query = (
                    from data in _tukdContext.Bpkdetr
                    join jtrnlkas in _tukdContext.Jtrnlkas on data.Idnojetra equals jtrnlkas.Idnojetra into jtrnlkasMatch
                    from jtrnlkas_data in jtrnlkasMatch.DefaultIfEmpty()
                    join rekening in _tukdContext.Daftrekening on data.Idrek equals rekening.Idrek
                    select new Bpkdetr
                    {
                        Idbpkdetr = data.Idbpkdetr,
                        Idbpk = data.Idbpk,
                        Idnojetra = data.Idnojetra,
                        IdnojetraNavigation = jtrnlkas_data ?? null,
                        Idrek = data.Idrek,
                        IdrekNavigation = rekening ?? null,
                        Datecreate = data.Datecreate,
                        Nilai = data.Nilai,
                        Dateupdate = data.Dateupdate,
                        Idkeg = data.Idkeg
                    }
                ).AsQueryable();
            if(param.Idbpk.ToString() != "0")
            {
                query = query.Where(w => w.Idbpk == param.Idbpk).AsQueryable();
            }
            if(param.Idkeg.ToString() != "0")
            {
                query = query.Where(w => w.Idkeg == param.Idkeg).AsQueryable();
            }
            Result = await query.ToListAsync();
            return Result;
        }
    }
}
