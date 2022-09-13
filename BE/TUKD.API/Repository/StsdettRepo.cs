using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class StsdettRepo : Repo<Stsdett>, IStsdettRepo
    {
        public StsdettRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;
        public async Task<bool> Update(Stsdett param)
        {
            Stsdett data = await _tukdContext.Stsdett.Where(w => w.Idstsdett == param.Idstsdett).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Nilai = param.Nilai;
            data.Dateupdate = param.Dateupdate;
            _tukdContext.Stsdett.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }
        public async Task<Stsdett> ViewData(long IdStsdett)
        {
            Stsdett data = await (
                    from det in _tukdContext.Stsdett
                    join bkbkas in _tukdContext.Bkbkas on det.Nobbantu.Trim() equals bkbkas.Nobbantu.Trim()
                    where det.Idstsdett == IdStsdett
                    select new Stsdett
                    {
                        Idstsdett = det.Idstsdett,
                        Idsts = det.Idsts,
                        Nobbantu = det.Nobbantu,
                        Idnojetra = det.Idnojetra,
                        Nilai = det.Nilai,
                        NobbantuNavigation = bkbkas ?? null
                    }
                ).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<Stsdett>> ViewDatas(long Idsts)
        {
            List<Stsdett> datas = await (
                    from det in _tukdContext.Stsdett
                    join bkbkas in _tukdContext.Bkbkas on det.Nobbantu.Trim() equals bkbkas.Nobbantu.Trim()
                    where det.Idsts == Idsts
                    select new Stsdett
                    {
                        Idstsdett = det.Idstsdett,
                        NobbantuNavigation = bkbkas ?? null,
                        Idsts = det.Idsts,
                        Nobbantu = det.Nobbantu,
                        Idnojetra = det.Idnojetra,
                        Nilai = det.Nilai
                    }
                ).ToListAsync();
            return datas;
        }
    }
}
