using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class SkpRepo : Repo<Skp>, ISkpRepo
    {
        public SkpRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;
        public async Task<List<Skp>> ViewDatas(long Idunit, long? Idbend, long Idxkode, string Kdstatus, bool istglvalid)
        {
            List<Skp> Result = new List<Skp>();
            IQueryable<Skp> data =  (
                from skp in _tukdContext.Skp
                join unit in _tukdContext.Daftunit on skp.Idunit equals unit.Idunit
                join bend in _tukdContext.Bend on skp.Idbend equals bend.Idbend into bendMacth from bendData in bendMacth.DefaultIfEmpty()
                where skp.Idunit == Idunit
                select new Skp
                {
                    Alamat = skp.Alamat,
                    Bunga = skp.Bunga,
                    Idbend = skp.Idbend,
                    IdbendNavigation = bendData ?? null,
                    Idskp = skp.Idskp,
                    Idunit = skp.Idunit,
                    IdunitNavigation = unit ?? null,
                    Idxkode = skp.Idxkode,
                    Kdstatus = skp.Kdstatus,
                    Kenaikan = skp.Kenaikan,
                    Noskp = skp.Noskp,
                    Npwpd = skp.Npwpd,
                    Penyetor = skp.Penyetor,
                    Tglskp = skp.Tglskp,
                    Tgltempo = skp.Tgltempo,
                    Tglvalid = skp.Tglvalid,
                    Uraiskp = skp.Uraiskp
                }
                ).AsQueryable();
            if(Idbend.ToString() != "0")
            {
                data = data.Where(w => w.Idbend == Idbend).AsQueryable();
            }
            if(Idxkode.ToString() != "0")
            {
                data = data.Where(w => w.Idxkode == Idxkode).AsQueryable();
            }
            if(Kdstatus.Trim() != "x")
            {
                List<string> status = Kdstatus.Split(",").ToList();
                data = data.Where(w => status.Contains(w.Kdstatus.Trim())).AsQueryable();
            }
            if(istglvalid == true)
            {
                data = data.Where(w => !String.IsNullOrEmpty(w.Tglvalid.ToString())).AsQueryable();
            }
            Result = await data.ToListAsync();
            return Result;
        }
        public async Task<Skp> ViewData(long Idskp)
        {
            Skp data = await (
                from skp in _tukdContext.Skp
                join unit in _tukdContext.Daftunit on skp.Idunit equals unit.Idunit
                join bend in _tukdContext.Bend on skp.Idbend equals bend.Idbend
                where skp.Idskp == Idskp
                select new Skp
                {
                    Alamat = skp.Alamat,
                    Bunga = skp.Bunga,
                    Idbend = skp.Idbend,
                    IdbendNavigation = bend ?? null,
                    Idskp = skp.Idskp,
                    Idunit = skp.Idunit,
                    IdunitNavigation = unit ?? null,
                    Idxkode = skp.Idxkode,
                    Kdstatus = skp.Kdstatus,
                    Kenaikan = skp.Kenaikan,
                    Noskp = skp.Noskp,
                    Npwpd = skp.Npwpd,
                    Penyetor = skp.Penyetor,
                    Tglskp = skp.Tglskp,
                    Tgltempo = skp.Tgltempo,
                    Tglvalid = skp.Tglvalid,
                    Uraiskp = skp.Uraiskp
                }
                ).FirstOrDefaultAsync();
            return data;
        }
        public async Task<bool> Update(Skp param)
        {
            Skp data = await _tukdContext.Skp.Where(w => w.Idskp == param.Idskp).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Noskp = param.Noskp;
            data.Npwpd = param.Npwpd;
            data.Tglskp = param.Tglskp;
            data.Tgltempo = param.Tgltempo;
            data.Uraiskp = param.Uraiskp;
            data.Alamat = param.Alamat;
            data.Penyetor = param.Penyetor;
            data.Bunga = param.Bunga;
            data.Kenaikan = param.Kenaikan;
            _tukdContext.Skp.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

    }
}
