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
    public class SppRepo : Repo<Spp>, ISppRepo
    {
        public SppRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<string> GenerateNoReg(long Idunit)
        {
            string newno = "";
            string lastno = await _tukdContext.Spp.Where(w => w.Idunit == Idunit).OrderBy(o => o.Noreg.Trim()).Select(s => s.Noreg).LastOrDefaultAsync();
            if (string.IsNullOrEmpty(lastno))
            {
                newno = "00001";
            } else {
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

        public async Task<List<long>> GetIds(long Idunit, int Idxkode, string Kdstatus, long Idspd)
        {
            List<long> Ids = await _tukdContext.Spp
                .Where(w => w.Idunit == Idunit && w.Idxkode == Idxkode && w.Kdstatus.Trim() == Kdstatus.Trim() && w.Idspd == Idspd)
                .Select(s => s.Idspp).ToListAsync();
            return Ids;
        }

        public async Task<bool> Update(Spp param)
        {
            Spp data = await _tukdContext.Spp.Where(w => w.Idspp == param.Idspp).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Nospp = param.Nospp;
                data.Tglspp = param.Tglspp;
                data.Idspd = param.Idspd;
                data.Idbulan = param.Idbulan;
                data.Ketotor = param.Ketotor;
                data.Keperluan = param.Keperluan;
                data.Noreg = param.Noreg;
                data.Idkontrak = param.Idkontrak;
                data.Idphk3 = param.Idphk3;
                data.Updatedate = param.Updatedate;
                data.Updateby = param.Updateby;
                data.Idbend = param.Idbend;
                _tukdContext.Spp.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }

        public async Task<bool> Pengesahan(Spp param)
        {
            Spp data = await _tukdContext.Spp.Where(w => w.Idspp == param.Idspp).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Tglvalid = param.Tglvalid;
                data.Valid = param.Valid;
                data.Validby = param.Validby;
                data.Updateby = param.Updateby;
                data.Updatedate = param.Updatedate;
                data.Validasi = param.Validasi;
                _tukdContext.Spp.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
        public async Task<bool> Penolakan(Spp param)
        {
            Spp data = await _tukdContext.Spp.Where(w => w.Idspp == param.Idspp).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Tglaprove = param.Tglaprove;
                data.Status = param.Status;
                data.Approveby = param.Approveby;
                data.Verifikasi = param.Verifikasi;
                data.Updateby = param.Updateby;
                data.Updatedate = param.Updatedate;
                _tukdContext.Spp.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }

        public async Task<Spp> ViewData(long Idspp)
        {
            Spp Result = await (
                    from data in _tukdContext.Spp
                    join spd in _tukdContext.Spd on data.Idspd equals spd.Idspd into spdMatch
                    from spd_data in spdMatch.DefaultIfEmpty()
                    join kontrak in _tukdContext.Kontrak on data.Idkontrak equals kontrak.Idkontrak into kontrakMatch
                    from kontrak_data in kontrakMatch.DefaultIfEmpty()
                    join phk3 in _tukdContext.Daftphk3 on data.Idphk3 equals phk3.Idphk3 into phk3Match
                    from phk3_data in phk3Match.DefaultIfEmpty()
                    join bendahara in _tukdContext.Bend on data.Idbend equals bendahara.Idbend into bendaharaMatch
                    from bendahara_data in bendaharaMatch.DefaultIfEmpty()
                    join bulan in _tukdContext.Bulan on data.Idbulan equals bulan.Idbulan into bulanMatch from bulan_data in bulanMatch.DefaultIfEmpty()
                    where data.Idspp == Idspp
                    select new Spp
                    {
                        Idbend = data.Idbend,
                        Idbulan = data.Idbulan,
                        Idkontrak = data.Idkontrak,
                        Idphk3 = data.Idphk3,
                        Idspd = data.Idspd,
                        Idspp = data.Idspp,
                        Idunit = data.Idunit,
                        Idxkode = data.Idxkode,
                        Kdstatus = data.Kdstatus,
                        Keperluan = data.Keperluan,
                        Ketotor = data.Ketotor,
                        Nilaiup = data.Nilaiup,
                        Noreg = data.Noreg,
                        Nospp = data.Nospp,
                        Penolakan = data.Penolakan,
                        Status = data.Status,
                        Tglspp = data.Tglspp,
                        Valid = data.Valid,
                        Verifikasi = data.Verifikasi,
                        Tglvalid = data.Tglvalid,
                        IdspdNavigation = spd_data ?? null,
                        IdkontrakNavigation = kontrak_data ?? null,
                        Idphk3Navigation = phk3_data ?? null,
                        IdbendNavigation = bendahara_data ?? null,
                        IdbulanNavigation = bulan_data ?? null,
                        Createby = data.Createby,
                        Createdate = data.Createdate,
                        Updateby = data.Updateby,
                        Updatedate = data.Updatedate,
                        Validby = data.Validby,
                        Approveby = data.Approveby,
                        Tglaprove = data.Tglaprove,
                        Idkeg = data.Idkeg,
                        Validasi = data.Validasi
                    }
                ).FirstOrDefaultAsync();
            if (Result != null)
            {
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

        public async Task<List<Spp>> ViewDatas(SppGet param)
        {
            List<Spp> Result = new List<Spp>();
            IQueryable<Spp> query = (
                    from data in _tukdContext.Spp
                    join spd in _tukdContext.Spd on data.Idspd equals spd.Idspd into spdMatch from spd_data in spdMatch.DefaultIfEmpty()
                    join kontrak in _tukdContext.Kontrak on data.Idkontrak equals kontrak.Idkontrak into kontrakMatch from kontrak_data in kontrakMatch.DefaultIfEmpty()
                    join phk3 in _tukdContext.Daftphk3 on data.Idphk3 equals phk3.Idphk3 into phk3Match from phk3_data in phk3Match.DefaultIfEmpty()
                    join bendahara in _tukdContext.Bend on data.Idbend equals bendahara.Idbend into bendaharaMatch from bendahara_data in bendaharaMatch.DefaultIfEmpty()
                    join bulan in _tukdContext.Bulan on data.Idbulan equals bulan.Idbulan into bulanMatch
                    from bulan_data in bulanMatch.DefaultIfEmpty()
                    select new Spp
                    {
                        Idbend = data.Idbend,
                        Idbulan = data.Idbulan,
                        Idkontrak = data.Idkontrak,
                        Idphk3 = data.Idphk3,
                        Idspd = data.Idspd,
                        Idspp = data.Idspp,
                        Idunit = data.Idunit,
                        Idxkode = data.Idxkode,
                        Kdstatus = data.Kdstatus,
                        Keperluan = data.Keperluan,
                        Ketotor = data.Ketotor,
                        Nilaiup = data.Nilaiup,
                        Noreg = data.Noreg,
                        Nospp = data.Nospp,
                        Penolakan = data.Penolakan,
                        Status = data.Status,
                        Tglspp = data.Tglspp,
                        Valid = data.Valid,
                        Verifikasi = data.Verifikasi,
                        Tglvalid = data.Tglvalid,
                        IdspdNavigation = spd_data ?? null,
                        IdkontrakNavigation = kontrak_data ?? null,
                        Idphk3Navigation = phk3_data ?? null,
                        IdbendNavigation = bendahara_data ?? null,
                        IdbulanNavigation = bulan_data ?? null,
                        Createby = data.Createby,
                        Createdate = data.Createdate,
                        Updateby = data.Updateby,
                        Updatedate = data.Updatedate,
                        Validby = data.Validby,
                        Approveby = data.Approveby,
                        Tglaprove = data.Tglaprove,
                        Idkeg = data.Idkeg,
                        Validasi = data.Validasi
                    }
                ).AsQueryable();
            if(param.Kdstatus != "x")
            {
                query = query.Where(w => w.Kdstatus.Trim() == param.Kdstatus.Trim()).AsQueryable();
            }
            if(param.Idxkode.ToString() != "0")
            {
                query = query.Where(w => w.Idxkode == param.Idxkode).AsQueryable();
            }
            if(param.Idunit.ToString() != "0")
            {
                query = query.Where(w => w.Idunit == param.Idunit).AsQueryable();
            }
            if(param.Idbend.ToString() != "0")
            {
                query = query.Where(w => w.Idbend == param.Idbend).AsQueryable();
            }
            if (param.Idkeg.ToString() != "0")
            {
                query = query.Where(w => w.Idkeg == param.Idkeg).AsQueryable();
            }
            Result.AddRange(await query.ToListAsync());
            if(Result.Count() > 0)
            {
                foreach(var f in Result)
                {
                    if(f.IdbendNavigation !=  null)
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

        public async Task<List<DataTracking>> TrackingData(long Idspp)
        {
            List<DataTracking> Result = new List<DataTracking>();
            Spp spp = new Spp();
            Spm spm = null;
            Sp2d sp2d = null;
            Bkuk bkuk = null;
            spp = await _tukdContext.Spp.Where(w => w.Idspp == Idspp).FirstOrDefaultAsync();
            if(spp != null)
            {
                spm = await _tukdContext.Spm.Where(w => w.Idspp == spp.Idspp).FirstOrDefaultAsync();
            }
            if(spm != null)
            {
                sp2d = await _tukdContext.Sp2d.Where(w => w.Idspm == spm.Idspm).FirstOrDefaultAsync();
            }
            if(sp2d != null)
            {
                bkuk = await _tukdContext.Bkuk.Where(w => w.Idsp2d == sp2d.Idsp2d).FirstOrDefaultAsync();
            }
            if(spp != null)
            {
                DataTracking temp = new DataTracking
                {
                    Title = "SPP",
                    Active = spm != null ? 0 : 1,
                    Canenter = spm != null ? false : true,
                    Desc = "SPP Dalam Tahap Pengajuan",
                    Done = 2
                };
                if (!String.IsNullOrEmpty(spp.Status.ToString()))
                {
                    if (spp.Status == true)
                    {
                        temp.Desc = "SPP Sudah Disahkan Dan Diverifikasi";
                    } else
                    {
                        temp.Desc = "SPP Sudah Disahkan Dan Belum Diverifikasi";
                    }
                    temp.Done = 3;
                } else
                {
                    if (!String.IsNullOrEmpty(spp.Valid.ToString()))
                    {
                        if (spp.Valid == true)
                        {
                            temp.Desc = "SPP Sudah Disahkan";
                        }
                        else
                        {
                            temp.Desc = "SPP Belum Disahkan";
                        }
                        temp.Done = 2;
                    }
                }
                Result.Add(temp);
            }
            if(spm != null)
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
                    } else
                    {
                        temp.Desc = "SPM Sudah Disahkan Dan Belum Diverifikasi";
                    }
                    temp.Done = 3;
                } else
                {
                    if (!String.IsNullOrEmpty(spm.Valid.ToString()))
                    {
                        if (spm.Valid == true)
                        {
                            temp.Desc = "SPM Sudah Disahkan";
                        } else
                        {
                            temp.Desc = "SPM Belum Disahkan";
                        }
                    }
                    temp.Done = 2;
                }
                Result.Add(temp);
            } else
            {
                DataTracking temp = new DataTracking
                {
                    Title = "SPM",
                    Active = 0,
                    Canenter = false,
                    Desc = "SPM Belum Diajukan",
                    Done = 1
                };
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
                    Done = 2
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
