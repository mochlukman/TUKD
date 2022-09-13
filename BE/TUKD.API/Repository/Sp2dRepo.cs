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
    public class Sp2dRepo : Repo<Sp2d>, ISp2dRepo
    {
        public Sp2dRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<PrimengTableResult<Sp2d>> ByDp(PrimengTableParam<Sp2dGet> param)
        {
            PrimengTableResult<Sp2d> Result = new PrimengTableResult<Sp2d>();
            List<long> Idsp2d = await _tukdContext.Dpdet.Where(w => w.Iddp == param.Parameters.Iddp).Select(s => s.Idsp2d).Distinct().ToListAsync();
            IQueryable<Sp2d> query = (
                from data in _tukdContext.Sp2d
                join bend in _tukdContext.Bend on data.Idbend equals bend.Idbend
                join spm in _tukdContext.Spm on data.Idspm equals spm.Idspm
                join spd in _tukdContext.Spd on data.Idspd equals spd.Idspd
                select new Sp2d
                {
                    Idbend = data.Idbend,
                    Idunit = data.Idunit,
                    Idxkode = data.Idxkode,
                    Kdstatus = data.Kdstatus,
                    Tglvalid = data.Tglvalid,
                    Createdate = data.Createdate,
                    Createby = data.Createby,
                    Updatedate = data.Updatedate,
                    Updateby = data.Updateby,
                    Valid = data.Valid,
                    Validby = data.Validby,
                    Verifikasi = data.Verifikasi,
                    Idsp2d = data.Idsp2d,
                    Nobbantu = data.Nobbantu,
                    Tglsp2d = data.Tglsp2d,
                    Nospm = !String.IsNullOrEmpty(data.Nospm) ? data.Nospm : spm.Nospm,
                    Nosp2d = data.Nosp2d,
                    Idkontrak = data.Idkontrak,
                    Idphk3 = data.Idphk3,
                    Idttd = data.Idttd,
                    Tglspm = data.Tglspm,
                    Penolakan = data.Penolakan,
                    Noreg = data.Noreg,
                    Ketotor = data.Ketotor,
                    Keperluan = data.Keperluan,
                    Idspd = data.Idspd,
                    Idspm = data.Idspm,
                    IdspmNavigation = spm ?? null,
                    IdspdNavigation = spd ?? null,
                    Validasi = data.Validasi
                }
                ).AsQueryable();
            if (param.Parameters != null)
            {
                if(param.Parameters.Iddp.ToString() != "0")
                {
                    query = query.Where(w => Idsp2d.Contains(w.Idsp2d)).AsQueryable();
                }
            }
            if (!String.IsNullOrEmpty(param.GlobalFilter))
            {
                query = query.Where(w =>
                    EF.Functions.Like(w.Nosp2d.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Keperluan.Trim(), "%" + param.GlobalFilter + "%")
                ).AsQueryable();
            }
            Result.Data = await query.Skip(param.Start).Take(param.Rows).ToListAsync();
            Result.Totalrecords = await query.CountAsync();
            return Result;
        }

        public async Task<PrimengTableResult<Sp2d>> ForBkuBud(PrimengTableParam<Sp2dGetForBkuBud> param)
        {
            PrimengTableResult<Sp2d> Result = new PrimengTableResult<Sp2d>();
            IQueryable<Sp2d> query = (
                from data in _tukdContext.Sp2d
                where !String.IsNullOrEmpty(data.Tglvalid.ToString())
                select new Sp2d
                {
                    Idbend = data.Idbend,
                    Idunit = data.Idunit,
                    Idxkode = data.Idxkode,
                    Kdstatus = data.Kdstatus,
                    Tglvalid = data.Tglvalid,
                    Createdate = data.Createdate,
                    Createby = data.Createby,
                    Updatedate = data.Updatedate,
                    Updateby = data.Updateby,
                    Valid = data.Valid,
                    Validby = data.Validby,
                    Verifikasi = data.Verifikasi,
                    Idsp2d = data.Idsp2d,
                    Nobbantu = data.Nobbantu,
                    Tglsp2d = data.Tglsp2d,
                    Nospm = data.Nospm,
                    Nosp2d = data.Nosp2d,
                    Idkontrak = data.Idkontrak,
                    Idphk3 = data.Idphk3,
                    Idttd = data.Idttd,
                    Tglspm = data.Tglspm,
                    Penolakan = data.Penolakan,
                    Noreg = data.Noreg,
                    Ketotor = data.Ketotor,
                    Keperluan = data.Keperluan,
                    Idspd = data.Idspd,
                    Idspm = data.Idspm,
                    Validasi = data.Validasi
                }
               ).AsQueryable();
            List<long> sp2dbkudk = await (from bkuk in _tukdContext.Bkuk select bkuk.Idsp2d).ToListAsync();
            if (sp2dbkudk.Count() > 0)
            {
                query = query.Where(w => !sp2dbkudk.Contains(w.Idsp2d)).AsQueryable();
            }
            if (param.Parameters.Idunit.ToString() != "0")
            {
                query = query.Where(w => w.Idunit == param.Parameters.Idunit).AsQueryable();
            }
            if (param.Parameters.Idbend.ToString() != "0")
            {
                query = query.Where(w => w.Idbend == param.Parameters.Idbend).AsQueryable();
            }
            if (param.Parameters.Idxkode.ToString() != "0")
            {
                query = query.Where(w => w.Idxkode == param.Parameters.Idxkode).AsQueryable();
            }
            if (param.Parameters.Kdstatus != "x")
            {
                query = query.Where(w => w.Kdstatus.Trim() == param.Parameters.Kdstatus).AsQueryable();
            }
            if (!String.IsNullOrEmpty(param.GlobalFilter))
            {
                query = query.Where(w =>
                    EF.Functions.Like(w.Nosp2d.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Keperluan.Trim(), "%" + param.GlobalFilter + "%")
                ).AsQueryable();
            }
            if (!String.IsNullOrEmpty(param.SortField))
            {
                if (param.SortField == "nosp2d")
                {
                    if (param.SortOrder > 0)
                    {
                        query = query.OrderBy(o => o.Nosp2d).AsQueryable();
                    }
                    else
                    {
                        query = query.OrderByDescending(o => o.Nosp2d).AsQueryable();
                    }
                }
                else if (param.SortField == "keperluan")
                {
                    if (param.SortOrder > 0)
                    {
                        query = query.OrderBy(o => o.Keperluan).AsQueryable();
                    }
                    else
                    {
                        query = query.OrderByDescending(o => o.Keperluan).AsQueryable();
                    }
                }
            }
            Result.Data = await query.Skip(param.Start).Take(param.Rows).ToListAsync();
            Result.Totalrecords = await query.CountAsync();
            return Result;
        }

        public async Task<List<Sp2d>> ForDp(long Iddp, int? Idxkode)
        {
            List<Sp2d> sp2d = new List<Sp2d>();
            IQueryable<Sp2d> query = (
                from data in _tukdContext.Sp2d
                where data.Idxkode == Idxkode && !String.IsNullOrEmpty(data.Tglvalid.ToString())
                select new Sp2d
                {
                    Idbend = data.Idbend,
                    Idunit = data.Idunit,
                    Idxkode = data.Idxkode,
                    Kdstatus = data.Kdstatus,
                    Tglvalid = data.Tglvalid,
                    Createdate = data.Createdate,
                    Createby = data.Createby,
                    Updatedate = data.Updatedate,
                    Updateby = data.Updateby,
                    Valid = data.Valid,
                    Validby = data.Validby,
                    Verifikasi = data.Verifikasi,
                    Idsp2d = data.Idsp2d,
                    Nobbantu = data.Nobbantu,
                    Tglsp2d = data.Tglsp2d,
                    Nospm = data.Nospm,
                    Nosp2d = data.Nosp2d,
                    Idkontrak = data.Idkontrak,
                    Idphk3 = data.Idphk3,
                    Idttd = data.Idttd,
                    Tglspm = data.Tglspm,
                    Penolakan = data.Penolakan,
                    Noreg = data.Noreg,
                    Ketotor = data.Ketotor,
                    Keperluan = data.Keperluan,
                    Idspd = data.Idspd,
                    Idspm = data.Idspm,
                    Validasi = data.Validasi
                }
                ).AsQueryable();
            List<long> idsp2dused = await (from dpdet in _tukdContext.Dpdet select dpdet.Idsp2d).ToListAsync();
            if(idsp2dused.Count() > 0)
            {
                query = query.Where(w => !idsp2dused.Contains(w.Idsp2d)).AsQueryable();
            }
            sp2d = await query.ToListAsync();
            return sp2d;
        }

        public async Task<string> GenerateNoReg(long Idunit)
        {
            string newno = "";
            string lastno = await _tukdContext.Sp2d.Where(w => w.Idunit == Idunit).OrderBy(o => o.Noreg.Trim()).Select(s => s.Noreg).LastOrDefaultAsync();
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

        public async Task<PrimengTableResult<Sp2d>> Paging(PrimengTableParam<Sp2dGet> param)
        {
            PrimengTableResult<Sp2d> Result = new PrimengTableResult<Sp2d>();
            IQueryable<Sp2d> query = (
                from data in _tukdContext.Sp2d
                join bend in _tukdContext.Bend on data.Idbend equals bend.Idbend into bendMatch
                from bend_data in bendMatch.DefaultIfEmpty()
                join kontrak in _tukdContext.Kontrak on data.Idkontrak equals kontrak.Idkontrak into kontrakMatch
                from kontrak_data in kontrakMatch.DefaultIfEmpty()
                join spd in _tukdContext.Spd on data.Idspd equals spd.Idspd into spdMatch
                from spd_data in spdMatch.DefaultIfEmpty()
                join spm in _tukdContext.Spm on data.Idspm equals spm.Idspm into spmMatch
                from spm_data in spmMatch.DefaultIfEmpty()
                join jabttd in _tukdContext.Jabttd on data.Idttd equals jabttd.Idttd into ttdMatch
                from ttd_data in ttdMatch.DefaultIfEmpty()
                join zkode in _tukdContext.Zkode on data.Idxkode equals zkode.Idxkode into zkodeMatch
                from zkode_data in zkodeMatch.DefaultIfEmpty()
                select new Sp2d
                {
                    Idbend = data.Idbend,
                    Idunit = data.Idunit,
                    Idxkode = data.Idxkode,
                    Kdstatus = data.Kdstatus,
                    Tglvalid = data.Tglvalid,
                    Createdate = data.Createdate,
                    Createby = data.Createby,
                    Updatedate = data.Updatedate,
                    Updateby = data.Updateby,
                    Valid = data.Valid,
                    Validby = data.Validby,
                    Verifikasi = data.Verifikasi,
                    Idsp2d = data.Idsp2d,
                    Nobbantu = data.Nobbantu,
                    Tglsp2d = data.Tglsp2d,
                    Nospm = !String.IsNullOrEmpty(data.Nospm) ? data.Nospm : spm_data.Nospm,
                    Nosp2d = data.Nosp2d,
                    Idkontrak = data.Idkontrak,
                    Idphk3 = data.Idphk3,
                    Idttd = data.Idttd,
                    Tglspm = data.Tglspm,
                    Penolakan = data.Penolakan,
                    Noreg = data.Noreg,
                    Ketotor = data.Ketotor,
                    Keperluan = data.Keperluan,
                    Idspd = data.Idspd,
                    Idspm = data.Idspm,
                    IdspmNavigation = spm_data ?? null,
                    IdspdNavigation = spd_data ?? null,
                    IdbendNavigation = bend_data ?? null,
                    IdkontrakNavigation = kontrak_data ?? null,
                    IdttdNavigation = ttd_data ?? null,
                    IdxkodeNavigation = zkode_data ?? null,
                    Validasi = data.Validasi
                }
                ).AsQueryable();
            if(param.Parameters.Idunit.ToString() != "0")
            {
                query = query.Where(w => w.Idunit == param.Parameters.Idunit).AsQueryable();
            }
            if (param.Parameters.Kdstatus != "x")
            {
                query = query.Where(w => w.Kdstatus.Trim() == param.Parameters.Kdstatus.Trim()).AsQueryable();
            }
            if (param.Parameters.Idxkode.ToString() != "0")
            {
                query = query.Where(w => w.Idxkode == param.Parameters.Idxkode).AsQueryable();
            }
            if (!String.IsNullOrEmpty(param.GlobalFilter))
            {
                query = query.Where(w =>
                    EF.Functions.Like(w.Nosp2d.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Keperluan.Trim(), "%" + param.GlobalFilter + "%")
                ).AsQueryable();
            }
            Result.Data = await query.Skip(param.Start).Take(param.Rows).ToListAsync();
            Result.Totalrecords = await query.CountAsync();
            return Result;
        }

        public async Task<bool> Pengesahan(Sp2d param)
        {
            Sp2d data = await _tukdContext.Sp2d.Where(w => w.Idsp2d == param.Idsp2d).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Tglvalid = param.Tglvalid;
                data.Validby = param.Validby;
                data.Valid = param.Valid;
                data.Penolakan = param.Penolakan;
                data.Validasi = param.Validasi;
                data.Verifikasi = param.Verifikasi;
                data.Updateby = param.Updateby;
                data.Updatedate = param.Updatedate;
                _tukdContext.Sp2d.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }

        public async Task<bool> Update(Sp2d param)
        {
            Sp2d data = await _tukdContext.Sp2d.Where(w => w.Idsp2d == param.Idsp2d).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Tglspm = param.Tglspm;
                data.Nospm = param.Nospm;
                data.Idbend = param.Idbend;
                data.Idspd = param.Idspd;
                data.Idspm = param.Idspm;
                data.Ketotor = param.Ketotor;
                data.Tglsp2d = param.Tglsp2d;
                data.Updatedate = param.Updatedate;
                data.Updateby = param.Updateby;
                data.Idphk3 = param.Idphk3;
                data.Idkontrak = param.Idkontrak;
                data.Idttd = param.Idttd;
                data.Keperluan = param.Keperluan;
                _tukdContext.Sp2d.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }

        public async Task<Sp2d> ViewData(long Idsp2d)
        {
            Sp2d Result = await (
                from data in _tukdContext.Sp2d
                join bend in _tukdContext.Bend on data.Idbend equals bend.Idbend into bendMatch
                from bend_data in bendMatch.DefaultIfEmpty()
                join kontrak in _tukdContext.Kontrak on data.Idkontrak equals kontrak.Idkontrak into kontrakMatch
                from kontrak_data in kontrakMatch.DefaultIfEmpty()
                join spd in _tukdContext.Spd on data.Idspd equals spd.Idspd into spdMatch
                from spd_data in spdMatch.DefaultIfEmpty()
                join spm in _tukdContext.Spm on data.Idspm equals spm.Idspm into spmMatch
                from spm_data in spmMatch.DefaultIfEmpty()
                join jabttd in _tukdContext.Jabttd on data.Idttd equals jabttd.Idttd into ttdMatch
                from ttd_data in ttdMatch.DefaultIfEmpty()
                join zkode in _tukdContext.Zkode on data.Idxkode equals zkode.Idxkode into zkodeMatch
                from zkode_data in zkodeMatch.DefaultIfEmpty()
                where data.Idsp2d == Idsp2d
                select new Sp2d
                {
                    Idbend = data.Idbend,
                    Idunit = data.Idunit,
                    Idxkode = data.Idxkode,
                    Kdstatus = data.Kdstatus,
                    Tglvalid = data.Tglvalid,
                    Createdate = data.Createdate,
                    Createby = data.Createby,
                    Updatedate = data.Updatedate,
                    Updateby = data.Updateby,
                    Valid = data.Valid,
                    Validby = data.Validby,
                    Verifikasi = data.Verifikasi,
                    Idsp2d = data.Idsp2d,
                    Nobbantu = data.Nobbantu,
                    Tglsp2d = data.Tglsp2d,
                    Nospm = !String.IsNullOrEmpty(data.Nospm) ? data.Nospm : spm_data.Nospm,
                    Nosp2d = data.Nosp2d,
                    Idkontrak = data.Idkontrak,
                    Idphk3 = data.Idphk3,
                    Idttd = data.Idttd,
                    Tglspm = data.Tglspm,
                    Penolakan = data.Penolakan,
                    Noreg = data.Noreg,
                    Ketotor = data.Ketotor,
                    Keperluan = data.Keperluan,
                    Idspd = data.Idspd,
                    Idspm = data.Idspm,
                    IdspmNavigation = spm_data ?? null,
                    IdspdNavigation = spd_data ?? null,
                    IdbendNavigation = bend_data ?? null,
                    IdkontrakNavigation = kontrak_data ?? null,
                    IdttdNavigation = ttd_data ?? null,
                    IdxkodeNavigation = zkode_data ?? null,
                    Idkeg = data.Idkeg,
                    Validasi = data.Validasi
                }
                ).FirstOrDefaultAsync();
            if(Result != null)
            {
                if(Result.IdspmNavigation != null)
                {
                    if (!String.IsNullOrEmpty(Result.IdspmNavigation.Idphk3.ToString()) || Result.IdspmNavigation.Idphk3 != 0)
                    {
                        Result.IdspmNavigation.Idphk3Navigation = await _tukdContext.Daftphk3.Where(w => w.Idphk3 == Result.IdspmNavigation.Idphk3).FirstOrDefaultAsync();
                    }
                    Result.IdspmNavigation.IdsppNavigation = await _tukdContext.Spp.Where(w => w.Idspp == Result.IdspmNavigation.Idspp).FirstOrDefaultAsync();
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

        public async Task<List<Sp2d>> ViewDatas(Sp2dGet param, List<long> Sp2dIds)
        {
            List<Sp2d> Result = new List<Sp2d>();
            IQueryable<Sp2d> query = (
                from data in _tukdContext.Sp2d
                join bend in _tukdContext.Bend on data.Idbend equals bend.Idbend into bendMatch
                from bend_data in bendMatch.DefaultIfEmpty()
                join kontrak in _tukdContext.Kontrak on data.Idkontrak equals kontrak.Idkontrak into kontrakMatch
                from kontrak_data in kontrakMatch.DefaultIfEmpty()
                join spd in _tukdContext.Spd on data.Idspd equals spd.Idspd into spdMatch
                from spd_data in spdMatch.DefaultIfEmpty()
                join spm in _tukdContext.Spm on data.Idspm equals spm.Idspm into spmMatch
                from spm_data in spmMatch.DefaultIfEmpty()
                join jabttd in _tukdContext.Jabttd on data.Idttd equals jabttd.Idttd into ttdMatch
                from ttd_data in ttdMatch.DefaultIfEmpty()
                join zkode in _tukdContext.Zkode on data.Idxkode equals zkode.Idxkode into zkodeMatch
                from zkode_data in zkodeMatch.DefaultIfEmpty()
                select new Sp2d
                {
                    Idbend = data.Idbend,
                    Idunit = data.Idunit,
                    Idxkode = data.Idxkode,
                    Kdstatus = data.Kdstatus,
                    Tglvalid = data.Tglvalid,
                    Createdate = data.Createdate,
                    Createby = data.Createby,
                    Updatedate = data.Updatedate,
                    Updateby = data.Updateby,
                    Valid = data.Valid,
                    Validby = data.Validby,
                    Verifikasi = data.Verifikasi,
                    Idsp2d = data.Idsp2d,
                    Nobbantu = data.Nobbantu,
                    Tglsp2d = data.Tglsp2d,
                    Nospm = !String.IsNullOrEmpty(data.Nospm) ? data.Nospm : spm_data.Nospm,
                    Nosp2d = data.Nosp2d,
                    Idkontrak = data.Idkontrak,
                    Idphk3 = data.Idphk3,
                    Idttd = data.Idttd,
                    Tglspm = data.Tglspm,
                    Penolakan = data.Penolakan,
                    Noreg = data.Noreg,
                    Ketotor = data.Ketotor,
                    Keperluan = data.Keperluan,
                    Idspd = data.Idspd,
                    Idspm = data.Idspm,
                    IdspmNavigation = spm_data ?? null,
                    IdspdNavigation = spd_data ?? null,
                    IdbendNavigation = bend_data ?? null,
                    IdkontrakNavigation = kontrak_data ?? null,
                    IdttdNavigation = ttd_data ?? null,
                    IdxkodeNavigation = zkode_data ?? null,
                    Idkeg = data.Idkeg,
                    Validasi = data.Validasi
                }
                ).AsQueryable();
            if(param.Idunit.ToString() != "0")
            {
                query = query.Where(w => w.Idunit == param.Idunit).AsQueryable();
            }
            if(param.Idxkode.ToString() != "0")
            {
                query = query.Where(w => w.Idxkode == param.Idxkode).AsQueryable();
            }
            if(param.Kdstatus.Trim() != "x")
            {
                query = query.Where(w => w.Kdstatus.Trim() == param.Kdstatus.Trim()).AsQueryable();
            }
            if(param.Penolakan != "x")
            {
                query = query.Where(w => w.Penolakan.Trim() == param.Penolakan.Trim()).AsQueryable();
            }
            if(Sp2dIds.Count() > 0)
            {
                query = query.Where(w => !Sp2dIds.Contains(w.Idsp2d)).AsQueryable();
            }
            if(param.Idkeg.ToString() != "0")
            {
                query = query.Where(w => w.Idkeg == param.Idkeg).AsQueryable();
            }
            Result = await query.ToListAsync();
            if (Result.Count() > 0)
            {
                foreach(var f in Result)
                {
                    if (f.IdspmNavigation != null)
                    {
                        if (!String.IsNullOrEmpty(f.IdspmNavigation.Idphk3.ToString()) || f.IdspmNavigation.Idphk3 != 0)
                        {
                            f.IdspmNavigation.Idphk3Navigation = await _tukdContext.Daftphk3.Where(w => w.Idphk3 == f.IdspmNavigation.Idphk3).FirstOrDefaultAsync();
                        }
                        f.IdspmNavigation.IdsppNavigation = await _tukdContext.Spp.Where(w => w.Idspp == f.IdspmNavigation.Idspp).FirstOrDefaultAsync();
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
    }
}
