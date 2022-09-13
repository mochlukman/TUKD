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
    public class SpddetrRepo : Repo<Spddetr>, ISpddetrRepo
    {
        public SpddetrRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<List<long>> GetIdReks(long Idspd, long Idkeg)
        {
            List<long> IdReks = new List<long> { };
            if(Idkeg.ToString() != "0")
            {
                IdReks.AddRange(await _tukdContext.Spddetr.Where(w => w.Idspd == Idspd && w.Idkeg == Idkeg).Select(s => s.Idrek).Distinct().ToListAsync());
            } else
            {
                IdReks.AddRange(await _tukdContext.Spddetr.Where(w => w.Idspd == Idspd).Select(s => s.Idrek).Distinct().ToListAsync());
            }
            return IdReks;
        }
        public async Task<bool> UpdateNilai(long Idssddetr, decimal? Nilai, DateTime? Dateupdate)
        {
            Spddetr data = await _tukdContext.Spddetr.Where(w => w.Idspddetr == Idssddetr).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Nilai = Nilai;
                data.Dateupdate = Dateupdate;
                _tukdContext.Spddetr.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
        public async Task<decimal?> TotalNilaiSpd(long Idspd)
        {
            decimal? Total = await _tukdContext.Spddetr.Where(w => w.Idspd == Idspd).SumAsync(s => s.Nilai);
            return Total;
        }
        public async Task<List<SpddetrViewTreeRoot>> TreetableFromSubkegiatan(long Idspd)
        {
            List<SpddetrViewTreeRoot> data = new List<SpddetrViewTreeRoot> { };
            List<long> Idkegs = await _tukdContext.Spddetr.Where(w => w.Idspd == Idspd).Select(s => s.Idkeg).Distinct().ToListAsync();
            if (Idkegs.Count() > 0)
            {
                for (var i = 0; i < Idkegs.Count(); i++)
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
                    if (mpgrm != null)
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
                        }
                        else
                        {
                            Dafturus dafturus = await _tukdContext.Dafturus.Where(w => w.Idurus == mpgrm.Idurus).FirstOrDefaultAsync();
                            if (dafturus != null) kode_full = dafturus.Kdurus.Trim() + "" + mpgrm.Nuprgrm.Trim() + "" + nukeg_induk;
                        }

                    }
                    data.Add(await _tukdContext.Mkegiatan.Where(w => w.Idkeg == Idkegs[i])
                    .Join(_tukdContext.Spddetr.Where(w => w.Idspd == Idspd && w.Idkeg == Idkegs[i]).Distinct(),
                    kegiatan => kegiatan.Idkeg,
                    spddetr => spddetr.Idkeg,
                    (kegiatan, spddetr) => new SpddetrViewTreeRoot
                    {
                        Data = new SpddetrViewTreeData
                        {
                            Rowid = kegiatan.Idkeg.ToString(),
                            Idkeg = kegiatan.Idkeg,
                            kode = kode_full + "" + kegiatan.Nukeg.Trim(),
                            uraian = kegiatan.Nmkegunit.Trim(),
                            Level = "sub_kegiatan"
                        },
                        Children = _tukdContext.Daftrekening
                            .Join(_tukdContext.Spddetr.Where(w => w.Idspd == Idspd && w.Idkeg == kegiatan.Idkeg),
                            rekening => rekening.Idrek,
                            spddetr2 => spddetr2.Idrek,
                            (rekening, spddetr2) => new SpddetrViewTreeRoot
                            {
                                Data = new SpddetrViewTreeData
                                {
                                    Rowid = kegiatan.Idkeg + "_" + spddetr2.Idspddetr,
                                    Idrek = rekening.Idrek,
                                    kode = rekening.Kdper.Trim(),
                                    uraian = rekening.Nmper.Trim(),
                                    Level = "rekening",
                                    Nilai = spddetr2.Nilai,
                                    Idspd = spddetr2.Idspd,
                                    Idspddetr = spddetr2.Idspddetr
                                }
                            }).ToList()
                    }).FirstOrDefaultAsync());
                }
            }

            return data;
        }
    }
}
