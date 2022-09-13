using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class TbpdettkegRepo : Repo<Tbpdettkeg>, ITbpdettkegRepo
    {
        public TbpdettkegRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Tbpdettkeg param)
        {
            Tbpdettkeg data = await _tukdContext.Tbpdettkeg.Where(w => w.Idtbpdettkeg == param.Idtbpdettkeg).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Nilai = param.Nilai;
            data.Dateupdate = param.Dateupdate;
            _tukdContext.Tbpdettkeg.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0) return true;
            return false;
        }

        public async Task<Tbpdettkeg> ViewData(long Idtbpdettkeg)
        {
            Tbpdettkeg data = await (
                from tbp_keg in _tukdContext.Tbpdettkeg
                join keg in _tukdContext.Mkegiatan on tbp_keg.Idkeg equals keg.Idkeg
                where tbp_keg.Idtbpdettkeg == Idtbpdettkeg
                select new Tbpdettkeg
                {
                    Datecreate = tbp_keg.Datecreate,
                    Dateupdate = tbp_keg.Dateupdate,
                    Idkeg = tbp_keg.Idkeg,
                    IdkegNavigation = keg ?? null,
                    Nilai = tbp_keg.Nilai,
                    Idtbpdett = tbp_keg.Idtbpdett,
                    Idtbpdettkeg = tbp_keg.Idtbpdettkeg
                }
                ).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<Tbpdettkeg>> ViewDatas(long Idtbpdett)
        {
            List<Tbpdettkeg> datas = await (
                from tbp_keg in _tukdContext.Tbpdettkeg
                join keg in _tukdContext.Mkegiatan on tbp_keg.Idkeg equals keg.Idkeg
                where tbp_keg.Idtbpdett == Idtbpdett
                select new Tbpdettkeg
                {
                    Datecreate = tbp_keg.Datecreate,
                    Dateupdate = tbp_keg.Dateupdate,
                    Idkeg = tbp_keg.Idkeg,
                    IdkegNavigation = keg ?? null,
                    Nilai = tbp_keg.Nilai,
                    Idtbpdett = tbp_keg.Idtbpdett,
                    Idtbpdettkeg = tbp_keg.Idtbpdettkeg
                }
                ).ToListAsync();
            return datas;
        }
    }
}
