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
    public class SpmRepo : Repo<Spm>, ISpmRepo
    {
        public SpmRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;
        public async Task<string> GenerateNoReg(long Idunit)
        {
            string newno = "";
            string lastno = await _tukdContext.Spm.Where(w => w.Idunit == Idunit).OrderBy(o => o.Noreg.Trim()).Select(s => s.Noreg).LastOrDefaultAsync();
            if (string.IsNullOrEmpty(lastno))
            {
                newno = "00001";
            }
            else
            {
                var toNumber = Int32.Parse(lastno);
                var PlusNumber = toNumber + 1;
                if (PlusNumber.ToString().Length == 1) newno = "0000" + PlusNumber.ToString();
                if (PlusNumber.ToString().Length == 2) newno = "000" + PlusNumber.ToString();
                if (PlusNumber.ToString().Length == 3) newno = "00" + PlusNumber.ToString();
                if (PlusNumber.ToString().Length == 4) newno = "0" + PlusNumber.ToString();
                if (PlusNumber.ToString().Length == 5) newno = PlusNumber.ToString();
            }
            return newno;
        }

        public async Task<string> GenerateNoReg(long Idunit, long Idbend, int Idxkode, string Kdstatus)
        {
            string newno = "";
            string lastno = await _tukdContext.Spm.Where(w => w.Idunit == Idunit && w.Idbend == Idbend && w.Idxkode == Idxkode && w.Kdstatus.Trim() == Kdstatus.Trim()).OrderBy(o => o.Noreg.Trim()).Select(s => s.Noreg).LastOrDefaultAsync();
            if (string.IsNullOrEmpty(lastno))
            {
                newno = "00001";
            }
            else
            {
                var toNumber = Int32.Parse(lastno);
                var PlusNumber = toNumber + 1;
                if (PlusNumber.ToString().Length == 1) newno = "0000" + PlusNumber.ToString();
                if (PlusNumber.ToString().Length == 2) newno = "000" + PlusNumber.ToString();
                if (PlusNumber.ToString().Length == 3) newno = "00" + PlusNumber.ToString();
                if (PlusNumber.ToString().Length == 4) newno = "0" + PlusNumber.ToString();
                if (PlusNumber.ToString().Length == 5) newno = PlusNumber.ToString();
            }
            return newno;
        }

        public async Task<List<long>> GetIds(long Idunit, int Idxkode, string Kdstatus, long Idspp)
        {
            List<long> Ids = await _tukdContext.Spm
                .Where(w => w.Idunit == Idunit && w.Idxkode == Idxkode && w.Kdstatus.Trim() == Kdstatus.Trim() && w.Idspp == Idspp)
                .Select(s => s.Idspp).ToListAsync();
            return Ids;
        }

        public async Task<bool> Update(Spm param)
        {
            Spm data = await _tukdContext.Spm.Where(w => w.Idspm == param.Idspm).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Tglspm = param.Tglspm;
                data.Nospm = param.Nospm;
                data.Idbend = param.Idbend;
                data.Idspd = param.Idspd;
                data.Idspp = param.Idspp;
                data.Ketotor = param.Ketotor;
                data.Updatedate = param.Updatedate;
                data.Updateby = param.Updateby;
                data.Idphk3 = param.Idphk3;
                data.Idkontrak = param.Idkontrak;
                _tukdContext.Spm.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }

        public async Task<bool> Pengesahan(Spm param)
        {
            Spm data = await _tukdContext.Spm.Where(w => w.Idspm == param.Idspm).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Tglvalid = param.Tglvalid;
                data.Valid = param.Valid;
                data.Updateby = param.Updateby;
                data.Updatedate = param.Updatedate;
                data.Validby = param.Validby;
                data.Validasi = param.Validasi;
                _tukdContext.Spm.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }

        public async Task<Spm> ViewData(long Idspm)
        {
            Spm Result = await (
                from data in _tukdContext.Spm
                join spp in _tukdContext.Spp on data.Idspp equals spp.Idspp into sppMatch
                from spp_data in sppMatch.DefaultIfEmpty()
                join kontrak in _tukdContext.Kontrak on data.Idkontrak equals kontrak.Idkontrak into kontrakMatch
                from kontrak_data in kontrakMatch.DefaultIfEmpty()
                join phk3 in _tukdContext.Daftphk3 on data.Idphk3 equals phk3.Idphk3 into phk3Match
                from phk3_data in phk3Match.DefaultIfEmpty()
                join bendahara in _tukdContext.Bend on data.Idbend equals bendahara.Idbend into bendaharaMatch
                from bendahara_data in bendaharaMatch.DefaultIfEmpty()
                where data.Idspm == Idspm
                select new Spm
                {
                    Createdate = data.Createdate,
                    Createby = data.Createby,
                    Updateby = data.Updateby,
                    Updatedate = data.Updatedate,
                    Idunit = data.Idunit,
                    Idbend = data.Idbend,
                    Idkontrak = data.Idkontrak,
                    Idphk3 = data.Idphk3,
                    Idspd = data.Idspd,
                    Idspm = data.Idspm,
                    Idspp = data.Idspp,
                    Idxkode = data.Idxkode,
                    Kdstatus = data.Kdstatus,
                    Keperluan = data.Keperluan,
                    Ketotor = data.Ketotor,
                    Noreg = data.Noreg,
                    Nilaiup = data.Nilaiup,
                    Nospm = data.Nospm,
                    Penolakan = data.Penolakan,
                    Tglvalid = data.Tglvalid,
                    Tglspp = data.Tglspp,
                    Tglspm = data.Tglspm,
                    Tglaprove = data.Tglaprove,
                    Validby = data.Validby,
                    Valid = data.Valid,
                    Approveby = data.Approveby,
                    Status = data.Status,
                    Verifikasi = data.Verifikasi,
                    IdsppNavigation = spp_data ?? null,
                    IdkontrakNavigation = kontrak_data ?? null,
                    Idphk3Navigation = phk3_data ?? null,
                    IdbendNavigation = bendahara_data ?? null,
                    Idkeg = data.Idkeg,
                    Validasi = data.Validasi
                }
                ).FirstOrDefaultAsync();
            if(Result != null)
            {
                if (Result.IdsppNavigation != null)
                {
                    Result.IdsppNavigation.IdspdNavigation = await _tukdContext.Spd.Where(w => w.Idspd == Result.IdsppNavigation.Idspd).FirstOrDefaultAsync();
                    if (!String.IsNullOrEmpty(Result.IdsppNavigation.Idkontrak.ToString()) || Result.IdsppNavigation.Idkontrak != 0)
                    {
                        Result.IdsppNavigation.IdkontrakNavigation = await _tukdContext.Kontrak.Where(w => w.Idkontrak == Result.IdsppNavigation.Idkontrak).FirstOrDefaultAsync();
                    }
                    if (!String.IsNullOrEmpty(Result.IdsppNavigation.Idphk3.ToString()) || Result.IdsppNavigation.Idphk3 != 0)
                    {
                        Result.IdsppNavigation.Idphk3Navigation = await _tukdContext.Daftphk3.Where(w => w.Idphk3 == Result.IdsppNavigation.Idphk3).FirstOrDefaultAsync();
                    }
                }
                if (Result.IdbendNavigation != null)
                {
                    Result.IdbendNavigation.IdpegNavigation = await _tukdContext.Pegawai.Where(w => w.Idpeg == Result.IdbendNavigation.Idpeg).FirstOrDefaultAsync();
                    if (!String.IsNullOrEmpty(Result.IdbendNavigation.Jnsbend))
                    {
                        Result.IdbendNavigation.JnsbendNavigation = await _tukdContext.Jbend.Where(w => w.Jnsbend.Trim() == Result.IdbendNavigation.Jnsbend.Trim()).FirstOrDefaultAsync();
                    }
                }
            }
            return Result;
        }

        public async Task<List<Spm>> ViewDatas(SpmGet param)
        {
            List<Spm> Result = new List<Spm>();
            IQueryable<Spm> query = (
                from data in _tukdContext.Spm
                join spp in _tukdContext.Spp on data.Idspp equals spp.Idspp into sppMatch from spp_data in sppMatch.DefaultIfEmpty()
                join kontrak in _tukdContext.Kontrak on data.Idkontrak equals kontrak.Idkontrak into kontrakMatch
                from kontrak_data in kontrakMatch.DefaultIfEmpty()
                join phk3 in _tukdContext.Daftphk3 on data.Idphk3 equals phk3.Idphk3 into phk3Match
                from phk3_data in phk3Match.DefaultIfEmpty()
                join bendahara in _tukdContext.Bend on data.Idbend equals bendahara.Idbend into bendaharaMatch
                from bendahara_data in bendaharaMatch.DefaultIfEmpty()
                select new Spm {
                    Createdate = data.Createdate,
                    Createby = data.Createby,
                    Updateby = data.Updateby,
                    Updatedate = data.Updatedate,
                    Idunit = data.Idunit,
                    Idbend = data.Idbend,
                    Idkontrak = data.Idkontrak,
                    Idphk3 = data.Idphk3,
                    Idspd = data.Idspd,
                    Idspm = data.Idspm,
                    Idspp = data.Idspp,
                    Idxkode = data.Idxkode,
                    Kdstatus = data.Kdstatus,
                    Keperluan = data.Keperluan,
                    Ketotor = data.Ketotor,
                    Noreg = data.Noreg,
                    Nilaiup = data.Nilaiup,
                    Nospm = data.Nospm,
                    Penolakan = data.Penolakan,
                    Tglvalid = data.Tglvalid,
                    Tglspp = data.Tglspp,
                    Tglspm = data.Tglspm,
                    Tglaprove = data.Tglaprove,
                    Validby = data.Validby,
                    Valid = data.Valid,
                    Approveby = data.Approveby,
                    Status = data.Status,
                    Verifikasi = data.Verifikasi,
                    IdsppNavigation = spp_data ?? null,
                    IdkontrakNavigation = kontrak_data ?? null,
                    Idphk3Navigation = phk3_data ?? null,
                    IdbendNavigation = bendahara_data ?? null,
                    Idkeg = data.Idkeg,
                    Validasi = data.Validasi
                }
                ).AsQueryable();
            if(param.Idunit.ToString() != "0")
            {
                query = query.Where(w => w.Idunit == param.Idunit).AsQueryable();
            }
            if(param.Kdstatus.Trim() != "x")
            {
                query = query.Where(w => w.Kdstatus.Trim() == param.Kdstatus.Trim()).AsQueryable();
            }
            if(param.Idxkode.ToString() != "0")
            {
                query = query.Where(w => w.Idxkode == param.Idxkode).AsQueryable();
            }
            if(param.Idbend.ToString() != "0")
            {
                query = query.Where(w => w.Idbend == param.Idbend).AsQueryable();
            }
            if(param.Idkeg.ToString() != "0")
            {
                query = query.Where(w => w.Idkeg == param.Idkeg).AsQueryable();
            }
            Result = await query.ToListAsync();
            if (Result.Count() > 0)
            {
                foreach (var f in Result)
                {
                    if (f.IdsppNavigation != null)
                    {
                        f.IdsppNavigation.IdspdNavigation = await _tukdContext.Spd.Where(w => w.Idspd == f.IdsppNavigation.Idspd).FirstOrDefaultAsync();
                        if (!String.IsNullOrEmpty(f.IdsppNavigation.Idkontrak.ToString()) || f.IdsppNavigation.Idkontrak != 0)
                        {
                            f.IdsppNavigation.IdkontrakNavigation = await _tukdContext.Kontrak.Where(w => w.Idkontrak == f.IdsppNavigation.Idkontrak).FirstOrDefaultAsync();
                        }
                        if (!String.IsNullOrEmpty(f.IdsppNavigation.Idphk3.ToString()) || f.IdsppNavigation.Idphk3 != 0)
                        {
                            f.IdsppNavigation.Idphk3Navigation = await _tukdContext.Daftphk3.Where(w => w.Idphk3 == f.IdsppNavigation.Idphk3).FirstOrDefaultAsync();
                        }
                    }
                    if (f.IdbendNavigation != null)
                    {
                        f.IdbendNavigation.IdpegNavigation = await _tukdContext.Pegawai.Where(w => w.Idpeg == f.IdbendNavigation.Idpeg).FirstOrDefaultAsync();
                        if (!String.IsNullOrEmpty(f.IdbendNavigation.Jnsbend))
                        {
                            f.IdbendNavigation.JnsbendNavigation = await _tukdContext.Jbend.Where(w => w.Jnsbend.Trim() == f.IdbendNavigation.Jnsbend.Trim()).FirstOrDefaultAsync();
                        }
                    }
                }
            }
            return Result;
        }

        public async Task<bool> Penolakan(Spm param)
        {
            Spm data = await _tukdContext.Spm.Where(w => w.Idspm == param.Idspm).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Tglaprove = param.Tglaprove;
                data.Status = param.Status;
                data.Updateby = param.Updateby;
                data.Updatedate = param.Updatedate;
                data.Verifikasi = param.Verifikasi;
                _tukdContext.Spm.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }

        public async Task<List<DataTracking>> TrackingData(long Idspm)
        {
            List<DataTracking> Result = new List<DataTracking>();
            Result.Add(new DataTracking
            {
                Title = "SPP",
                Active = 0,
                Canenter = false,
                Desc = "SPP Sudah Disahkan Dan Belum Diverifikasi",
                Done = 3
            });
            Spm spm = null;
            Sp2d sp2d = null;
            Bkuk bkuk = null;
            spm = await _tukdContext.Spm.Where(w => w.Idspm == Idspm).FirstOrDefaultAsync();
            if (spm != null)
            {
                sp2d = await _tukdContext.Sp2d.Where(w => w.Idspm == spm.Idspm).FirstOrDefaultAsync();
            }
            if (sp2d != null)
            {
                bkuk = await _tukdContext.Bkuk.Where(w => w.Idsp2d == sp2d.Idsp2d).FirstOrDefaultAsync();
            }
            if (spm != null)
            {
                DataTracking temp = new DataTracking
                {
                    Title = "SPM",
                    Active = sp2d != null ? 0 : 1,
                    Canenter = sp2d != null ? false : true,
                    Desc = "SPM Dalam Tahap Pengajuan",
                    Done = 2
                };
                if (!String.IsNullOrEmpty(spm.Status.ToString()))
                {
                    if (spm.Status == true)
                    {
                        temp.Desc = "SPM Sudah Disahkan Dan Diverifikasi";
                    }
                    else
                    {
                        temp.Desc = "SPM Sudah Disahkan Dan Belum Diverifikasi";
                    }
                    temp.Done = 3;
                }
                else
                {
                    if (!String.IsNullOrEmpty(spm.Valid.ToString()))
                    {
                        if (spm.Valid == true)
                        {
                            temp.Desc = "SPM Sudah Disahkan";
                        }
                        else
                        {
                            temp.Desc = "SPM Belum Disahkan";
                        }
                    }
                    temp.Done = 2;
                }
                Result.Add(temp);
            }
            if (sp2d != null)
            {
                DataTracking temp = new DataTracking
                {
                    Title = "SP2D",
                    Active = bkuk != null ? 0 : 1,
                    Canenter = bkuk != null ? false : true,
                    Desc = "SP2D Dalam Tahap Pengajuan",
                    Done = 2,
                };
                if (!String.IsNullOrEmpty(sp2d.Tglvalid.ToString()))
                {
                    temp.Desc = "SP2D Sudah Disahkan Dan Diverifikasi";
                    temp.Done = 3;
                }
                Result.Add(temp);
            }
            else
            {
                DataTracking temp = new DataTracking
                {
                    Title = "SP2D",
                    Active = 0,
                    Canenter = false,
                    Desc = "SP2D Belum Diajukan",
                    Done = 1
                };
                Result.Add(temp);
            }
            if (bkuk != null)
            {
                DataTracking temp = new DataTracking
                {
                    Title = "CAIR",
                    Active = !String.IsNullOrEmpty(bkuk.Tglvalid.ToString()) ? 1 : 0,
                    Canenter = !String.IsNullOrEmpty(bkuk.Tglvalid.ToString()) ? true : false,
                    Desc = "SP2D Belum Cair",
                    Done = 2
                };
                if (!String.IsNullOrEmpty(bkuk.Tglvalid.ToString()))
                {
                    temp.Desc = "SP2D Sudah Cair";
                    temp.Done = 3;
                }
                Result.Add(temp);
            }
            else
            {
                DataTracking temp = new DataTracking
                {
                    Title = "CAIR",
                    Active = 0,
                    Canenter = false,
                    Desc = "Pencairan Belum Diajukan",
                    Done = 1
                };
                Result.Add(temp);
            }

            return Result;
        }
    }
}
