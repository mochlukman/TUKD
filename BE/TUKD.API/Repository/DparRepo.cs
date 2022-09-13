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
    public class DparRepo : Repo<Dpar>, IDparRepo
    {
        public DparRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;
        public async Task<bool> UpdateNilai(Dpadetr param, decimal? newTotal)
        {
            Dpar data = await _tukdContext.Dpar.Where(w =>
                                 w.Iddpar == param.Iddpar).FirstOrDefaultAsync();
            data.Nilai = newTotal;
            data.Dateupdate = param.Dateupdate;
            _tukdContext.Dpar.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }
        public async Task<decimal?> GetNilai(long Iddpar)
        {
            decimal? Nilai = await _tukdContext.Dpar.Where(w => w.Iddpar == Iddpar)
                    .Select(s => s.Nilai).SumAsync();
            return Nilai;
        }

        public async Task<bool> UpdateULT(Dpar param)
        {
            Dpar data = await _tukdContext.Dpar.Where(w => w.Iddpar == param.Iddpar).FirstOrDefaultAsync();
            if(data != null)
            {
                data.UpGu = param.UpGu;
                data.Ls = param.Ls;
                data.Tu = param.Tu;
                data.Dateupdate = param.Dateupdate;
                _tukdContext.Dpar.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }

        public async Task<List<DparView>> GetByBpkdetr(long Idunit, long Idkeg, long Idbpk)
        {
            List<long> rekInBpkdetr = await _tukdContext.Bpkdetr.Where(w => w.Idbpk == Idbpk).Select(s => s.Idrek).Distinct().ToListAsync();
            List<long> listTahapDpar = await _tukdContext.Dpar.Select(s => Int64.Parse(s.Kdtahap.Trim())).Distinct().ToListAsync();
            long maxTahap = listTahapDpar[listTahapDpar.Count() - 1];
            List<DparView> datas = await (
                  from dpar in _tukdContext.Dpar
                  join dpa in _tukdContext.Dpa on dpar.Iddpa equals dpa.Iddpa
                  join kegiatan in _tukdContext.Mkegiatan on dpar.Idkeg equals kegiatan.Idkeg
                  join rekening in _tukdContext.Daftrekening on dpar.Idrek equals rekening.Idrek
                  where dpa.Idunit == Idunit && dpar.Idkeg == Idkeg && !rekInBpkdetr.Contains(dpar.Idrek) && dpar.Kdtahap.Trim() == maxTahap.ToString()
                  select new DparView
                  {
                      Iddpa = dpar.Iddpa,
                      Idkeg = dpar.Idkeg,
                      Iddpar = dpar.Iddpar,
                      Datecreate = dpar.Datecreate,
                      Dateupdate = dpar.Dateupdate,
                      Idrek = dpar.Idrek,
                      Ls = dpar.Ls,
                      UpGu = dpar.UpGu,
                      Kdtahap = dpar.Kdtahap,
                      Nilai = dpar.Nilai,
                      Tu = dpar.Tu,
                      Kegiatan = kegiatan,
                      Rekening = rekening
                  }
                ).ToListAsync();
            return datas;
        }
    }
}
