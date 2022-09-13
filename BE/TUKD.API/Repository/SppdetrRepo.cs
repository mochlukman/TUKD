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
    public class SppdetrRepo : Repo<Sppdetr>, ISppdetrRepo
    {
        public SppdetrRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<decimal?> TotalNilaiSpp(List<long> Idspp)
        {
            decimal? total = await _tukdContext.Sppdetr.Where(w => Idspp.Contains(w.Idspp)).SumAsync(s => s.Nilai);
            return total;
        }

        public async Task<List<SppdetrViewTreeRoot>> TreetableFromSubkegiatan(long Idspp, decimal? TotalSpp, decimal? TotalSpd)
        {
            List<SppdetrViewTreeRoot> data = new List<SppdetrViewTreeRoot> { };
            List<long> Idkegs = await _tukdContext.Sppdetr.Where(w => w.Idspp == Idspp).Select(s => s.Idkeg).Distinct().ToListAsync();
            if(Idkegs.Count() > 0)
            {
                for(var i = 0; i < Idkegs.Count(); i++)
                {
                    string kode_full = "";
                    
                    Mpgrm mpgrm = await _tukdContext.Mpgrm
                            .Join(_tukdContext.Mkegiatan.Where(w => w.Idkeg == Idkegs[i]),
                            program => program.Idprgrm,
                            kegiatan => kegiatan.Idprgrm,
                            (pro, keg) => new Mpgrm
                            {
                                Idprgrm = pro.Idprgrm,
                                Nuprgrm = pro.Nuprgrm,
                                Idurus = pro.Idurus
                            }).FirstOrDefaultAsync();
                    if(mpgrm != null)
                    {
                        Mkegiatan subkeg = await _tukdContext.Mkegiatan.Where(w => w.Idkeg == Idkegs[i]).Select(s => new Mkegiatan
                        {
                            Idkeg = s.Idkeg,
                            Nukeg = s.Nukeg
                        }).FirstOrDefaultAsync();
                        string nukeg_induk = await _tukdContext.Mkegiatan
                            .Where(w => w.Idprgrm == mpgrm.Idprgrm && w.Nukeg.Trim() == (subkeg.Nukeg.Substring(0, subkeg.Nukeg.Trim().Length - 3)) && w.Type.Trim() == "H")
                            .Select(s => s.Nukeg.Trim())
                            .FirstOrDefaultAsync();
                        if (String.IsNullOrEmpty(mpgrm.Idurus.ToString()))
                        {
                            kode_full = "0.00." + mpgrm.Nuprgrm.Trim() + "" + nukeg_induk;
                        } else
                        {
                            Dafturus dafturus = await _tukdContext.Dafturus.Where(w => w.Idurus == mpgrm.Idurus).FirstOrDefaultAsync();
                            if (dafturus != null) kode_full = dafturus.Kdurus.Trim() + "" + mpgrm.Nuprgrm.Trim() + "" + nukeg_induk;
                        }

                    }
                    data.Add(await _tukdContext.Mkegiatan.Where(w => w.Idkeg == Idkegs[i])
                    .Join(_tukdContext.Sppdetr.Where(w => w.Idspp == Idspp && w.Idkeg == Idkegs[i]).Distinct(),
                    kegiatan => kegiatan.Idkeg,
                    sppdetr => sppdetr.Idkeg,
                    (kegiatan, sppdetr) => new SppdetrViewTreeRoot
                    {
                        Data = new SppdetrViewTreeData
                        {
                            Rowid = kegiatan.Idkeg.ToString(),
                            Idkeg = kegiatan.Idkeg,
                            kode = kode_full + "" + kegiatan.Nukeg.Trim(),
                            uraian = kegiatan.Nmkegunit.Trim(),
                            Level = "sub_kegiatan"
                        },
                        Children = _tukdContext.Daftrekening
                            .Join(_tukdContext.Sppdetr.Where(w => w.Idspp == Idspp && w.Idkeg == kegiatan.Idkeg),
                            rekening => rekening.Idrek,
                            sppdetr2 => sppdetr2.Idrek,
                            (rekening, sppdetr2) => new SppdetrViewTreeRoot
                            {
                                Data = new SppdetrViewTreeData
                                {
                                    Rowid = kegiatan.Idkeg + "_" + sppdetr2.Idsppdetr,
                                    Idnojetra = sppdetr2.Idnojetra,
                                    Idrek = rekening.Idrek,
                                    kode = rekening.Kdper.Trim(),
                                    uraian = rekening.Nmper.Trim(),
                                    Level = "rekening",
                                    Nilai = sppdetr2.Nilai,
                                    Idspp = sppdetr2.Idspp,
                                    Idsppdetr = sppdetr2.Idsppdetr
                                }
                            }).ToList()
                    }).FirstOrDefaultAsync());
                }
            }
            
            return data;
        }

        public async Task<bool> Update(Sppdetr param)
        {
            Sppdetr data = await _tukdContext.Sppdetr.Where(w => w.Idsppdetr == param.Idsppdetr).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Nilai = param.Nilai;
                data.Updatedate = param.Updatedate;
                data.Updateby = param.Updateby;
                _tukdContext.Sppdetr.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateNilai(long Idsspdetr, decimal? Nilai, DateTime? Dateupdate, string Updateby)
        {
            Sppdetr data = await _tukdContext.Sppdetr.Where(w => w.Idsppdetr == Idsspdetr).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Nilai = Nilai;
                data.Updatedate = Dateupdate;
                data.Updateby = Updateby;
                _tukdContext.Sppdetr.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
