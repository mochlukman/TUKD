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
    public class DaftrekeningRepo : Repo<Daftrekening>, IDaftrekeningRepo
    {
        public DaftrekeningRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<PrimengTableResult<Daftrekening>> ByForRkad(PrimengTableParam<RekGlobalParam> param)
        {
            PrimengTableResult<Daftrekening> Result = new PrimengTableResult<Daftrekening>();
            IQueryable<Daftrekening> query = _tukdContext.Daftrekening.AsQueryable();
            IQueryable<Rkad> query_ref = _tukdContext.Rkad.AsQueryable();
            if (param.Parameters.Idunit.ToString() != "0")
            {
                query_ref = query_ref.Where(w => w.Idunit == param.Parameters.Idunit).AsQueryable();
            }
            if(param.Parameters.Kdtahap != "x")
            {
                query_ref = query_ref.Where(w => w.Kdtahap.Trim() == param.Parameters.Kdtahap.Trim()).AsQueryable();
            }
            List<long> Idresk = await query_ref.Select(s => s.Idrek).Distinct().ToListAsync();
            query = query.Where(w => !Idresk.Contains(w.Idrek) && w.Type.Trim() == "D").AsQueryable();
            if (param.Parameters.Idjnsakun.ToString() != "0")
            {
                query = query.Where(w => w.Idjnsakun == param.Parameters.Idjnsakun).AsQueryable();
            }
            if (!String.IsNullOrEmpty(param.GlobalFilter))
            {
                query = query.Where(w =>
                    EF.Functions.Like(w.Kdper.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Nmper.Trim(), "%" + param.GlobalFilter + "%")
                ).AsQueryable();
            }
            Result.Data = await query.Skip(param.Start).Take(param.Rows).ToListAsync();
            Result.Totalrecords = await query.CountAsync();
            return Result;
        }

        public async Task<PrimengTableResult<Daftrekening>> ByForRkar(PrimengTableParam<RekGlobalParam> param)
        {
            PrimengTableResult<Daftrekening> Result = new PrimengTableResult<Daftrekening>();
            IQueryable<Daftrekening> query = _tukdContext.Daftrekening.AsQueryable();
            IQueryable<Rkar> query_ref = _tukdContext.Rkar.AsQueryable();
            if (param.Parameters.Idunit.ToString() != "0")
            {
                query_ref = query_ref.Where(w => w.Idunit == param.Parameters.Idunit).AsQueryable();
            }
            if (param.Parameters.Kdtahap != "x")
            {
                query_ref = query_ref.Where(w => w.Kdtahap.Trim() == param.Parameters.Kdtahap.Trim()).AsQueryable();
            }
            if (param.Parameters.Idkeg.ToString() != "0")
            {
                query_ref = query_ref.Where(w => w.Idkeg == param.Parameters.Idkeg);
            }
            List<long> Idresk = await query_ref.Select(s => s.Idrek).Distinct().ToListAsync();
            query = query.Where(w => !Idresk.Contains(w.Idrek) && w.Type.Trim() == "D").AsQueryable();
            if (param.Parameters.Idjnsakun.ToString() != "0")
            {
                query = query.Where(w => w.Idjnsakun == param.Parameters.Idjnsakun).AsQueryable();
            }
            if (!String.IsNullOrEmpty(param.GlobalFilter))
            {
                query = query.Where(w =>
                    EF.Functions.Like(w.Kdper.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Nmper.Trim(), "%" + param.GlobalFilter + "%")
                ).AsQueryable();
            }
            Result.Data = await query.Skip(param.Start).Take(param.Rows).ToListAsync();
            Result.Totalrecords = await query.CountAsync();
            return Result;
        }
        public async Task<PrimengTableResult<Daftrekening>> ByForRkab(PrimengTableParam<RekGlobalParam> param)
        {
            PrimengTableResult<Daftrekening> Result = new PrimengTableResult<Daftrekening>();
            IQueryable<Daftrekening> query = _tukdContext.Daftrekening.AsQueryable();
            IQueryable<Rkab> query_ref = _tukdContext.Rkab.AsQueryable();
            if (param.Parameters.Idunit.ToString() != "0")
            {
                query_ref = query_ref.Where(w => w.Idunit == param.Parameters.Idunit).AsQueryable();
            }
            if (param.Parameters.Kdtahap != "x")
            {
                query_ref = query_ref.Where(w => w.Kdtahap.Trim() == param.Parameters.Kdtahap.Trim()).AsQueryable();
            }
            if (param.Parameters.Trkr.ToString() != "0")
            {
                query_ref = query_ref.Where(w => w.Trkr == param.Parameters.Trkr).AsQueryable();
            }
            List<long> Idresk = await query_ref.Select(s => s.Idrek).Distinct().ToListAsync();
            query = query.Where(w => !Idresk.Contains(w.Idrek) && w.Type.Trim() == "D").AsQueryable();
            if (param.Parameters.Idjnsakun.ToString() != "0")
            {
                query = query.Where(w => w.Idjnsakun == param.Parameters.Idjnsakun).AsQueryable();
            }
            if(param.Parameters.KdperStartwith.Trim() != "x")
            {
                query = query.Where(w => w.Kdper.Trim().StartsWith(param.Parameters.KdperStartwith.Trim())).AsQueryable();
            }
            if (!String.IsNullOrEmpty(param.GlobalFilter))
            {
                query = query.Where(w =>
                    EF.Functions.Like(w.Kdper.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Nmper.Trim(), "%" + param.GlobalFilter + "%")
                ).AsQueryable();
            }
            Result.Data = await query.Skip(param.Start).Take(param.Rows).ToListAsync();
            Result.Totalrecords = await query.CountAsync();
            return Result;
        }

        public async Task<List<Daftrekening>> ByJenisAkun(List<long?> Idjnsakun)
        {
            List<Daftrekening> datas = await _tukdContext.Daftrekening.Where(w => Idjnsakun.Contains(w.Idjnsakun)).OrderBy(o => o.Kdper.Trim()).ToListAsync();
            return datas;
        }

        public async Task<PrimengTableResult<Daftrekening>> BySpmdetb(PrimengTableParam<RekGlobalParam> param)
        {
            PrimengTableResult<Daftrekening> Result = new PrimengTableResult<Daftrekening>();
            IQueryable<Daftrekening> query = _tukdContext.Daftrekening.AsQueryable();
            IQueryable<Spm> spm_query = _tukdContext.Spm.AsQueryable();
            if (param.Parameters.Idunit.ToString() != "0")
            {
                spm_query = spm_query.Where(w => w.Idunit == param.Parameters.Idunit);
            }
            if (param.Parameters.Idbend.ToString() != "0")
            {
                spm_query = spm_query.Where(w => w.Idbend == param.Parameters.Idbend);
            }
            if (param.Parameters.Idxkode.ToString() != "0")
            {
                spm_query = spm_query.Where(w => w.Idxkode == param.Parameters.Idxkode);
            }
            if (param.Parameters.Kdstatus != "x")
            {
                spm_query = spm_query.Where(w => w.Kdstatus.Trim() == param.Parameters.Kdstatus.Trim());
            }
            List<long> Idspm = await spm_query.Select(s => s.Idspm).ToListAsync();
            List<long> Idresk = await _tukdContext.Spmdetb.Where(w => Idspm.Contains(w.Idspm)).Distinct().Select(s => s.Idrek).ToListAsync();
            query = query.Where(w => !Idresk.Contains(w.Idrek) && w.Type.Trim() == "D").AsQueryable();
            if(param.Parameters.Idjnsakun.ToString() != "0")
            {
                query = query.Where(w => w.Idjnsakun == param.Parameters.Idjnsakun).AsQueryable();
            }
            if (!String.IsNullOrEmpty(param.GlobalFilter))
            {
                query = query.Where(w =>
                    EF.Functions.Like(w.Kdper.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Nmper.Trim(), "%" + param.GlobalFilter + "%")
                ).AsQueryable();
            }
            Result.Data = await query.Skip(param.Start).Take(param.Rows).ToListAsync();
            Result.Totalrecords = await query.CountAsync();
            return Result;
        }

        public async Task<PrimengTableResult<Daftrekening>> BySpmdetd(PrimengTableParam<RekGlobalParam> param)
        {
            PrimengTableResult<Daftrekening> Result = new PrimengTableResult<Daftrekening>();
            IQueryable<Daftrekening> query = _tukdContext.Daftrekening.AsQueryable();
            IQueryable<Spm> spm_query = _tukdContext.Spm.AsQueryable();
            if (param.Parameters.Idunit.ToString() != "0")
            {
                spm_query = spm_query.Where(w => w.Idunit == param.Parameters.Idunit);
            }
            if (param.Parameters.Idbend.ToString() != "0")
            {
                spm_query = spm_query.Where(w => w.Idbend == param.Parameters.Idbend);
            }
            if (param.Parameters.Idxkode.ToString() != "0")
            {
                spm_query = spm_query.Where(w => w.Idxkode == param.Parameters.Idxkode);
            }
            if (!String.IsNullOrEmpty(param.Parameters.Kdstatus))
            {
                spm_query = spm_query.Where(w => w.Kdstatus.Trim() == param.Parameters.Kdstatus.Trim());
            }
            List<long> Idspm = await spm_query.Select(s => s.Idspm).ToListAsync();
            List<long> Idresk = await _tukdContext.Spmdetd.Where(w => Idspm.Contains(w.Idspm)).Distinct().Select(s => s.Idrek).ToListAsync();
            query = query.Where(w => !Idresk.Contains(w.Idrek) && w.Type.Trim() == "D").AsQueryable();
            if (!String.IsNullOrEmpty(param.GlobalFilter))
            {
                query = query.Where(w =>
                    EF.Functions.Like(w.Kdper.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Nmper.Trim(), "%" + param.GlobalFilter + "%")
                ).AsQueryable();
            }
            Result.Data = await query.Skip(param.Start).Take(param.Rows).ToListAsync();
            Result.Totalrecords = await query.CountAsync();
            return Result;
        }

        public async Task<PrimengTableResult<Daftrekening>> ByStsdetd(PrimengTableParam<RekGlobalParam> param)
        {
            PrimengTableResult<Daftrekening> Result = new PrimengTableResult<Daftrekening>();
            IQueryable<Daftrekening> query = _tukdContext.Daftrekening.AsQueryable();
            IQueryable<Sts> sts_query = _tukdContext.Sts.AsQueryable();
            if (param.Parameters.Idunit.ToString() != "0")
            {
                sts_query = sts_query.Where(w => w.Idunit == param.Parameters.Idunit);
            }
            if(param.Parameters.Idbend.ToString() != "0")
            {
                sts_query = sts_query.Where(w => w.Idbend == param.Parameters.Idbend);
            }
            if(param.Parameters.Idxkode.ToString() != "0")
            {
                sts_query = sts_query.Where(w => w.Idxkode == param.Parameters.Idxkode);
            }
            if (!String.IsNullOrEmpty(param.Parameters.Kdstatus))
            {
                sts_query = sts_query.Where(w => w.Kdstatus.Trim() == param.Parameters.Kdstatus.Trim());
            }
            List<long> Idsts = await sts_query.Select(s => s.Idsts).ToListAsync();
            List<long> Idresk = await _tukdContext.Stsdetd.Where(w => Idsts.Contains(w.Idsts)).Distinct().Select(s => s.Idrek).ToListAsync();
            query = query.Where(w => Idresk.Contains(w.Idrek)).AsQueryable();
            if (!String.IsNullOrEmpty(param.GlobalFilter))
            {
                query = query.Where(w =>
                    EF.Functions.Like(w.Kdper.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Nmper.Trim(), "%" + param.GlobalFilter + "%")
                ).AsQueryable();
            }
            Result.Data = await query.Skip(param.Start).Take(param.Rows).ToListAsync();
            Result.Totalrecords = await query.CountAsync();
            return Result;
        }

        public async Task<List<Daftrekening>> GetByRkar(long Idunit, long Idkeg)
        {
            List<Daftrekening> datas = await _tukdContext.Daftrekening
                .Join(_tukdContext.Rkar.Where(w => w.Idunit == Idunit && w.Idkeg == Idkeg),
                rekening => rekening.Idrek,
                rkar => rkar.Idrek,
                (rekening, rkar) => new Daftrekening
                {
                    Idrek = rekening.Idrek,
                    Kdper = rekening.Kdper,
                    Nmper = rekening.Nmper,
                    Mtglevel = rekening.Mtglevel,
                    Kdkhusus = rekening.Kdkhusus,
                    Jnsrek = rekening.Jnsrek,
                    Idjnsakun = rekening.Idjnsakun,
                    Type = rekening.Type,
                    Staktif = rekening.Staktif,
                    Datecreate = rekening.Datecreate
                }
                ).ToListAsync().ConfigureAwait(false);
            return datas;
        }

        public async Task<List<Daftrekening>> Search(RekeningSearchParam param)
        {
            string[] mtgSplit = param.Mtglevel.Split(',');
            int[] mtglevels = mtgSplit.Select(int.Parse).ToArray();
            List<Daftrekening> datas = await _tukdContext.Daftrekening.Where(w =>
                            w.Kdper.StartsWith(param.Startwith) &&
                            (w.Nmper.Contains(param.Keyword) || w.Kdper.Contains(param.Keyword)) &&
                            mtglevels.Contains(w.Mtglevel)).Take(20).ToListAsync();
            return datas;
        }

        public async Task<List<Daftrekening>> StartKode(string startkode)
        {
            string[] split_kode = startkode.Split(",");
            List<Daftrekening> datas = new List<Daftrekening> { };
            for(var i = 0; i < split_kode.Length; i++)
            {
                datas.AddRange(await _tukdContext.Daftrekening.Where(w => w.Kdper.Trim().StartsWith(split_kode[i])).ToListAsync());
            }
            return datas;
        }

        public async Task<PrimengTableResult<Daftrekening>> StartKodePaging(PrimengTableParam<RekeningStartKodeParam> param)
        {
            PrimengTableResult<Daftrekening> Result = new PrimengTableResult<Daftrekening>();
            IQueryable<Daftrekening> entity = _tukdContext.Daftrekening.AsQueryable();
            if(param.Parameters != null)
            {
                if (!String.IsNullOrEmpty(param.Parameters.Kode))
                {
                    List<string> split_kode = param.Parameters.Kode.Split(",").ToList();
                    entity = entity.Where(w => split_kode.Any(f => w.Kdper.Trim().StartsWith(f))).AsQueryable();
                }
            }
            if (!String.IsNullOrEmpty(param.GlobalFilter))
            {
                entity = entity.Where(w =>
                    EF.Functions.Like(w.Kdper.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Nmper.Trim(), "%" + param.GlobalFilter + "%")
                ).AsQueryable();
            }
            Result.Data = await entity.Skip(param.Start).Take(param.Rows).ToListAsync();
            Result.Totalrecords = await entity.CountAsync();
            return Result;
        }

        public async Task<List<LookupTreeDto>> TreeCheckboxByDpa(long Idunit, long Idspp, string Kdtahap, int Idnojetra, long Idxkode, string Kdstatus)
        {
            List<long> rekUsed = await _tukdContext.Spp.Where(w => w.Idunit == Idunit && w.Kdstatus.Trim() == Kdstatus && w.Idxkode == Idxkode && w.Idspp == Idspp)
                .Join(_tukdContext.Sppdetr.Where(w => w.Idnojetra == Idnojetra),
                spp => spp.Idspp,
                sppdetr => sppdetr.Idspp,
                (spp, sppdetr) => sppdetr.Idrek).ToListAsync();
            List<long> Iddpas = await _tukdContext.Dpa.Where(w => w.Idunit == Idunit && w.Kdtahap.Trim() == Kdtahap.Trim()).Select(s => s.Iddpa).ToListAsync();
            List<long> Idkegdpar = await _tukdContext.Dpar.Where(w => Iddpas.Contains(w.Iddpa) && w.Kdtahap.Trim() == Kdtahap && !rekUsed.Contains(w.Idrek)).Select(s => s.Idkeg).ToListAsync();
            List<long> Idrekdpar = await _tukdContext.Dpar.Where(w => Iddpas.Contains(w.Iddpa) && w.Kdtahap.Trim() == Kdtahap && !rekUsed.Contains(w.Idrek)).Select(s => s.Idrek).ToListAsync();
            List<LookupTreeDto> model = new List<LookupTreeDto> { };
            //ambil Non Urusan START
            LookupTreeDto temp_non_urusan = new LookupTreeDto
            {
                label = "0.00. - Non Urusan",
                expandedIcon = "fa fa-folder-open",
                collapsedIcon = "fa fa-folder",
                data_id = 0,
                this_header = true,
                this_level = "Non Urusan",
            };
            List<long> idpgrm_non_urusan = await _tukdContext.Mpgrm.Where(w => w.Idurus == null).Select(s => s.Idprgrm).ToListAsync();
            List<long?> IdkegDpa = await (
                    from dpakeg in _tukdContext.Dpakegiatan
                    join mkeg in _tukdContext.Mkegiatan on dpakeg.Idkeg equals mkeg.Idkeg
                    join dpapgrm in _tukdContext.Dpaprogram on dpakeg.Iddpapgrm equals dpapgrm.Iddpapgrm
                    where dpapgrm.Idunit == Idunit && dpapgrm.Kdtahap.Trim() == Kdtahap.Trim()
                    select mkeg.Idkeginduk
                ).Distinct().ToListAsync();
            temp_non_urusan.children = await (
                from dpapgrm in _tukdContext.Dpaprogram
                join mpgrm in _tukdContext.Mpgrm on dpapgrm.Idprgrm equals mpgrm.Idprgrm
                where idpgrm_non_urusan.Contains(dpapgrm.Idprgrm) && dpapgrm.Idunit == Idunit && dpapgrm.Kdtahap.Trim() == Kdtahap.Trim()
                select new LookupTreeDto
                {
                    label = "00." + mpgrm.Nuprgrm.Trim() + " - " + mpgrm.Nmprgrm.Trim(),
                    expandedIcon = "fa fa-folder-open",
                    collapsedIcon = "fa fa-folder",
                    data_id = mpgrm.Idprgrm,
                    data_id_parent = 0,
                    this_header = true,
                    this_level = "program",
                    children = (
                        from mkeg in _tukdContext.Mkegiatan
                        where mkeg.Idprgrm == dpapgrm.Idprgrm && IdkegDpa.Contains(mkeg.Idkeg) && mkeg.Idkeginduk == 0 && mkeg.Type.Trim() == "H" && mkeg.Levelkeg == 1
                        select new LookupTreeDto
                        {
                            label = "00." + mpgrm.Nuprgrm.Trim() + "" + mkeg.Nukeg.Trim() + " - " + mkeg.Nmkegunit.Trim(),
                            expandedIcon = "fa fa-folder-open",
                            collapsedIcon = "fa fa-folder",
                            data_id = mkeg.Idkeg,
                            data_id_parent = mpgrm.Idprgrm,
                            this_header = true,
                            this_level = "kegiatan",
                            children =
                            (
                                from dpakegsub in _tukdContext.Dpakegiatan
                                join mkegsub in _tukdContext.Mkegiatan on dpakegsub.Idkeg equals mkegsub.Idkeg
                                where dpakegsub.Iddpapgrm == dpapgrm.Iddpapgrm && mkegsub.Idkeginduk == mkeg.Idkeg && mkegsub.Type.Trim() == "D" && mkegsub.Levelkeg == 2
                                select new LookupTreeDto
                                {
                                    label = "00." + mpgrm.Nuprgrm.Trim() + "" + mkegsub.Nukeg.Trim() + " - " + mkegsub.Nmkegunit.Trim(),
                                    expandedIcon = "fa fa-folder-open",
                                    collapsedIcon = "fa fa-folder",
                                    data_id = mkegsub.Idkeg,
                                    data_id_parent = mkeg.Idkeg,
                                    this_header = true,
                                    this_level = "sub_kegiatan",
                                    children = (
                                        from rekening in _tukdContext.Daftrekening
                                        join dpar in _tukdContext.Dpar on rekening.Idrek equals dpar.Idrek
                                        where Idrekdpar.Contains(rekening.Idrek) && !rekUsed.Contains(rekening.Idrek) && Iddpas.Contains(dpar.Iddpa) && Idkegdpar.Contains(dpar.Idkeg) && dpar.Idkeg == dpakegsub.Idkeg
                                        select new LookupTreeDto
                                        {
                                            label = "00." + mpgrm.Nuprgrm.Trim() + "" + "" + mkegsub.Nukeg.Trim() + "" + mkegsub.Nukeg.Trim() + "" + rekening.Kdper.Trim() + " - " + rekening.Nmper.Trim(),
                                            expandedIcon = "fa fa-folder-open",
                                            collapsedIcon = "fa fa-folder",
                                            data_id = rekening.Idrek,
                                            data_id_parent = mkegsub.Idkeg,
                                            this_header = false,
                                            this_level = "rekening",
                                            idrek = rekening.Idrek
                                        }
                                    ).ToList()
                                }
                            ).ToList()
                        }
                    ).ToList()
                }
                ).ToListAsync();
            model.Add(temp_non_urusan);
            //Non Urusan END
            Daftunit daftunit = await _tukdContext.Daftunit
                .Where(w => w.Idunit == Idunit).FirstOrDefaultAsync();
            if (daftunit != null)
            {
                Dafturus dafturus = await _tukdContext.Dafturus.Where(w => w.Idurus == daftunit.Idurus).FirstOrDefaultAsync();
                if (dafturus != null)
                {
                    LookupTreeDto temp_urusan = new LookupTreeDto
                    {
                        label = dafturus.Kdurus.Trim() + " - " + dafturus.Nmurus.Trim(),
                        expandedIcon = "fa fa-folder-open",
                        collapsedIcon = "fa fa-folder",
                        data_id = dafturus.Idurus,
                        this_header = true,
                        this_level = "Urusan",
                    };
                    List<long> idpgrm_urusan = await _tukdContext.Mpgrm.Where(w => w.Idurus == dafturus.Idurus).Select(s => s.Idprgrm).ToListAsync();
                    temp_urusan.children = await (
                        from dpapgrm in _tukdContext.Dpaprogram
                        join mpgrm in _tukdContext.Mpgrm on dpapgrm.Idprgrm equals mpgrm.Idprgrm
                        where idpgrm_urusan.Contains(dpapgrm.Idprgrm) && dpapgrm.Idunit == Idunit
                        select new LookupTreeDto
                        {
                            label = "00." + mpgrm.Nuprgrm.Trim() + " - " + mpgrm.Nmprgrm.Trim(),
                            expandedIcon = "fa fa-folder-open",
                            collapsedIcon = "fa fa-folder",
                            data_id = mpgrm.Idprgrm,
                            data_id_parent = 0,
                            this_header = true,
                            this_level = "program",
                            children = (
                                from mkeg in _tukdContext.Mkegiatan
                                where mkeg.Idprgrm == dpapgrm.Idprgrm && IdkegDpa.Contains(mkeg.Idkeg) && mkeg.Idkeginduk == 0 && mkeg.Type.Trim() == "H" && mkeg.Levelkeg == 1
                                select new LookupTreeDto
                                {
                                    label = "00." + mpgrm.Nuprgrm.Trim() + "" + mkeg.Nukeg.Trim() + " - " + mkeg.Nmkegunit.Trim(),
                                    expandedIcon = "fa fa-folder-open",
                                    collapsedIcon = "fa fa-folder",
                                    data_id = mkeg.Idkeg,
                                    data_id_parent = mpgrm.Idprgrm,
                                    this_header = true,
                                    this_level = "kegiatan",
                                    children =
                                    (
                                        from dpakegsub in _tukdContext.Dpakegiatan
                                        join mkegsub in _tukdContext.Mkegiatan on dpakegsub.Idkeg equals mkegsub.Idkeg
                                        where dpakegsub.Iddpapgrm == dpapgrm.Iddpapgrm && mkegsub.Idkeginduk == mkeg.Idkeg && mkegsub.Type.Trim() == "D" && mkegsub.Levelkeg == 2
                                        select new LookupTreeDto
                                        {
                                            label = "00." + mpgrm.Nuprgrm.Trim() + "" + mkegsub.Nukeg.Trim() + " - " + mkegsub.Nmkegunit.Trim(),
                                            expandedIcon = "fa fa-folder-open",
                                            collapsedIcon = "fa fa-folder",
                                            data_id = mkegsub.Idkeg,
                                            data_id_parent = mkeg.Idkeg,
                                            this_header = true,
                                            this_level = "sub_kegiatan",
                                            children = (
                                                from rekening in _tukdContext.Daftrekening
                                                join dpar in _tukdContext.Dpar on rekening.Idrek equals dpar.Idrek
                                                where Idrekdpar.Contains(rekening.Idrek) && !rekUsed.Contains(rekening.Idrek) && Iddpas.Contains(dpar.Iddpa) && Idkegdpar.Contains(dpar.Idkeg) && dpar.Idkeg == dpakegsub.Idkeg
                                                select new LookupTreeDto
                                                {
                                                    label = "00." + mpgrm.Nuprgrm.Trim() + "" + "" + mkegsub.Nukeg.Trim() + "" + mkegsub.Nukeg.Trim() + "" + rekening.Kdper.Trim() + " - " + rekening.Nmper.Trim(),
                                                    expandedIcon = "fa fa-folder-open",
                                                    collapsedIcon = "fa fa-folder",
                                                    data_id = rekening.Idrek,
                                                    data_id_parent = mkegsub.Idkeg,
                                                    this_header = false,
                                                    this_level = "rekening",
                                                    idrek = rekening.Idrek
                                                }
                                            ).ToList()
                                        }
                                    ).ToList()
                                }
                            ).ToList()
                        }
                        ).ToListAsync();
                    model.Add(temp_urusan);
                }
            }
            return model;
        }

        public async Task<List<TreeTableRekeningRoot>> TreeTableRekeningByBpk(long Idbpk)
        {
            List<TreeTableRekeningRoot> models = new List<TreeTableRekeningRoot> { };
            List<long> Idkegs = await _tukdContext.Bpkdetr.Where(w => w.Idbpk == Idbpk).Select(s => s.Idkeg).Distinct().ToListAsync();
            List<long> Idprog = new List<long> { };
            if (Idkegs.Count() > 0) {
                Idprog.AddRange(await _tukdContext.Mkegiatan.Where(w => Idkegs.Contains(w.Idkeg)).Select(s => s.Idprgrm).Distinct().ToListAsync());
            }
            if(Idprog.Count() > 0)
            {
                List<long?> Idurus = await _tukdContext.Mpgrm.Where(w => Idprog.Contains(w.Idprgrm)).Select(s => s.Idurus).Distinct().ToListAsync();
                if((Idurus.Count() > 0))
                {
                    for(var i = 0; i < Idurus.Count(); i++)
                    {
                        if (String.IsNullOrEmpty(Idurus[i].ToString()))
                        {
                            // Non Urusan Start
                            List<long> Idpgrm = await _tukdContext.Mpgrm.Where(w => w.Idurus == null && Idprog.Contains(w.Idprgrm)).Select(s => s.Idprgrm).ToListAsync();
                            List<Mkegiatan> Subkeg = await _tukdContext.Mkegiatan.Where(w => Idpgrm.Contains(w.Idprgrm) && Idkegs.Contains(w.Idkeg)).ToListAsync();
                            List<long> Idkeg = new List<long>();
                            if(Subkeg.Count() > 0)
                            {
                                Subkeg.ForEach(f =>
                                {
                                    Idkeg.AddRange(_tukdContext.Mkegiatan.Where(w => w.Idkeg == f.Idkeginduk).Select(s => s.Idkeg).ToList());
                                });
                            }
                            TreeTableRekeningRoot temp_non_urusan = new TreeTableRekeningRoot() {
                                Data = new TreeTableRekeningData
                                {
                                    kode = "0.00.",
                                    row_id = "0.00.",
                                    this_level = "urusan",
                                    uraian = "Non Urusan"
                                },
                                Children = (
                                    from program in _tukdContext.Mpgrm
                                    where Idpgrm.Contains(program.Idprgrm) && program.Idurus == null
                                    select new TreeTableRekeningRoot
                                    {
                                        Data = new TreeTableRekeningData
                                        {
                                            kode = "00." + program.Nuprgrm.Trim(),
                                            row_id = "00." + program.Nuprgrm.Trim(),
                                            this_level = "program",
                                            uraian = program.Nmprgrm.Trim()
                                        },
                                        Children = (
                                            from kegiatan in _tukdContext.Mkegiatan
                                            join program2 in _tukdContext.Mpgrm on kegiatan.Idprgrm equals program2.Idprgrm
                                            where Idkeg.Contains(kegiatan.Idkeg) && Idpgrm.Contains(kegiatan.Idprgrm)
                                            select new TreeTableRekeningRoot
                                            {
                                                Data = new TreeTableRekeningData
                                                {
                                                    kode = "00." + program.Nuprgrm.Trim() + kegiatan.Nukeg.Trim(),
                                                    row_id = "00." + program.Nuprgrm.Trim() + kegiatan.Nukeg.Trim(),
                                                    this_level = "kegiatan",
                                                    uraian = kegiatan.Nmkegunit.Trim()
                                                },
                                                Children = (
                                                    from subkeg in _tukdContext.Mkegiatan
                                                    join program3 in _tukdContext.Mpgrm on subkeg.Idprgrm equals program3.Idprgrm
                                                    where subkeg.Idkeginduk == kegiatan.Idkeg && Idkegs.Contains(subkeg.Idkeg)
                                                    select new TreeTableRekeningRoot
                                                    {
                                                        Data = new TreeTableRekeningData
                                                        {
                                                            kode = "00." + program.Nuprgrm.Trim() + kegiatan.Nukeg.Trim() + subkeg.Nukeg.Trim(),
                                                            row_id = "00." + program.Nuprgrm.Trim() + kegiatan.Nukeg.Trim() + subkeg.Nukeg.Trim(),
                                                            this_level = "sub_kegiatan",
                                                            uraian = subkeg.Nmkegunit.Trim()
                                                        },
                                                        Children = (
                                                        from bpkdetr in _tukdContext.Bpkdetr
                                                        join rekening in _tukdContext.Daftrekening on bpkdetr.Idrek equals rekening.Idrek
                                                        join subkeg2 in _tukdContext.Mkegiatan on bpkdetr.Idkeg equals subkeg2.Idkeg
                                                        where bpkdetr.Idbpk == Idbpk && Idkegs.Contains(bpkdetr.Idkeg)
                                                        select new TreeTableRekeningRoot
                                                        {
                                                            Data = new TreeTableRekeningData
                                                            {
                                                                kode = rekening.Kdper.Trim(),
                                                                row_id = "00." + program.Nuprgrm.Trim() + kegiatan.Nukeg.Trim() + subkeg.Nukeg.Trim() + rekening.Kdper.Trim(),
                                                                this_level = "rekening",
                                                                uraian = rekening.Nmper.Trim(),
                                                                nilai = bpkdetr.Nilai,
                                                                idbpkdetr = bpkdetr.Idbpkdetr,
                                                                idbpk = bpkdetr.Idbpk
                                                            }
                                                        }
                                                        ).ToList()

                                                    }
                                                ).ToList()
                                            }
                                        ).ToList()
                                    }
                                ).ToList()
                            };
                            models.Add(temp_non_urusan);
                        } else
                        {
                            // Urusan Start
                            List<long> Idpgrm = await _tukdContext.Mpgrm.Where(w => w.Idurus == Idurus[i] && Idprog.Contains(w.Idprgrm)).Select(s => s.Idprgrm).ToListAsync();
                            List<Mkegiatan> Subkeg = await _tukdContext.Mkegiatan.Where(w => Idpgrm.Contains(w.Idprgrm) && Idkegs.Contains(w.Idkeg)).ToListAsync();
                            List<long> Idkeg = new List<long>();
                            if (Subkeg.Count() > 0)
                            {
                                Subkeg.ForEach(f =>
                                {
                                    Idkeg.AddRange(_tukdContext.Mkegiatan.Where(w => w.Idkeg == f.Idkeginduk).Select(s => s.Idkeg).ToList());
                                });
                            }
                            Dafturus urusan = await _tukdContext.Dafturus.Where(w => w.Idurus == Idurus[i]).FirstOrDefaultAsync();
                            if(urusan != null)
                            {
                                TreeTableRekeningRoot temp_root = new TreeTableRekeningRoot()
                                {
                                    Data = new TreeTableRekeningData
                                    {
                                        kode = urusan.Kdurus.Trim(),
                                        row_id = urusan.Kdurus.Trim(),
                                        this_level = "urusan",
                                        uraian = urusan.Nmurus.Trim()
                                    },
                                    Children = (
                                    from program in _tukdContext.Mpgrm
                                    where Idpgrm.Contains(program.Idprgrm) && program.Idurus == urusan.Idurus
                                    select new TreeTableRekeningRoot
                                    {
                                        Data = new TreeTableRekeningData
                                        {
                                            kode = urusan.Kdurus.Trim() + program.Nuprgrm.Trim(),
                                            row_id = urusan.Kdurus.Trim() + program.Nuprgrm.Trim(),
                                            this_level = "program",
                                            uraian = program.Nmprgrm.Trim()
                                        },
                                        Children = (
                                            from kegiatan in _tukdContext.Mkegiatan
                                            join program2 in _tukdContext.Mpgrm on kegiatan.Idprgrm equals program2.Idprgrm
                                            where Idkeg.Contains(kegiatan.Idkeg) && Idpgrm.Contains(kegiatan.Idprgrm)
                                            select new TreeTableRekeningRoot
                                            {
                                                Data = new TreeTableRekeningData
                                                {
                                                    kode = urusan.Kdurus.Trim() + program.Nuprgrm.Trim() + kegiatan.Nukeg.Trim(),
                                                    row_id = urusan.Kdurus.Trim() + program.Nuprgrm.Trim() + kegiatan.Nukeg.Trim(),
                                                    this_level = "kegiatan",
                                                    uraian = kegiatan.Nmkegunit.Trim()
                                                },
                                                Children = (
                                                    from subkeg in _tukdContext.Mkegiatan
                                                    join program3 in _tukdContext.Mpgrm on subkeg.Idprgrm equals program3.Idprgrm
                                                    where subkeg.Idkeginduk == kegiatan.Idkeg && Idkegs.Contains(subkeg.Idkeg)
                                                    select new TreeTableRekeningRoot
                                                    {
                                                        Data = new TreeTableRekeningData
                                                        {
                                                            kode = urusan.Kdurus.Trim() + program.Nuprgrm.Trim() + kegiatan.Nukeg.Trim() + subkeg.Nukeg.Trim(),
                                                            row_id = urusan.Kdurus.Trim() + program.Nuprgrm.Trim() + kegiatan.Nukeg.Trim() + subkeg.Nukeg.Trim(),
                                                            this_level = "sub_kegiatan",
                                                            uraian = subkeg.Nmkegunit.Trim()
                                                        },
                                                        Children = (
                                                        from bpkdetr in _tukdContext.Bpkdetr
                                                        join rekening in _tukdContext.Daftrekening on bpkdetr.Idrek equals rekening.Idrek
                                                        join subkeg2 in _tukdContext.Mkegiatan on bpkdetr.Idkeg equals subkeg2.Idkeg
                                                        where bpkdetr.Idbpk == Idbpk && Idkegs.Contains(bpkdetr.Idkeg)
                                                        select new TreeTableRekeningRoot
                                                        {
                                                            Data = new TreeTableRekeningData
                                                            {
                                                                kode = rekening.Kdper.Trim(),
                                                                row_id = urusan.Kdurus.Trim() + program.Nuprgrm.Trim() + kegiatan.Nukeg.Trim() + subkeg.Nukeg.Trim() + rekening.Kdper.Trim(),
                                                                this_level = "rekening",
                                                                uraian = rekening.Nmper.Trim(),
                                                                nilai = bpkdetr.Nilai,
                                                                idbpkdetr = bpkdetr.Idbpkdetr,
                                                                idbpk = bpkdetr.Idbpk
                                                            }
                                                        }
                                                        ).ToList()

                                                    }
                                                ).ToList()
                                            }
                                        ).ToList()
                                    }
                                ).ToList()
                                };
                                models.Add(temp_root);
                            }
                        }
                    }
                }
            }
            //if(dafturus.Count() > 0)
            //{
            //    dafturus.ForEach(f =>
            //    {
            //        models.Add(new TreeTableRekeningRoot
            //        {
            //            Data = new TreeTableRekeningData
            //            {
            //                kode = f.Kdurus.Trim(),
            //                row_id = f.Kdurus.Trim(),
            //                this_level = "urusan",
            //                uraian = f.Nmurus.Trim(),
            //            },
            //            Children = (
            //            from program in _tukdContext.Mpgrm
            //            join urusan in _tukdContext.Dafturus on program.Idurus equals urusan.Idurus
            //            where urusan.Idurus == f.Idurus && Idprog.Contains(program.Idprgrm)
            //            select new TreeTableRekeningRoot
            //            {
            //                Data = new TreeTableRekeningData
            //                {
            //                    kode = f.Kdurus.Trim() + "" + program.Nuprgrm.Trim(),
            //                    row_id = f.Kdurus.Trim() + "" + program.Nuprgrm.Trim(),
            //                    this_level = "program",
            //                    uraian = program.Nmprgrm.Trim()
            //                }
            //            }
            //            ).ToList()
            //        });
            //    });
            //}
            return models;
        }

        public async Task<Daftrekening> ViewData(long Idrek)
        {
            return await _tukdContext.Daftrekening.Where(w => w.Idrek == Idrek).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Daftrekening param)
        {
            Daftrekening data = await _tukdContext.Daftrekening.Where(w => w.Idrek == param.Idrek).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Dateupdate = DateTime.Now;
            data.Kdper = param.Kdper;
            data.Nmper = param.Nmper;
            data.Type = param.Type;
            _tukdContext.Daftrekening.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<PrimengTableResult<Daftrekening>> ByDpa(PrimengTableParam<RekGlobalParam> param)
        {
            PrimengTableResult<Daftrekening> Result = new PrimengTableResult<Daftrekening>();
            IQueryable<Daftrekening> query = _tukdContext.Daftrekening.AsQueryable();
            if(param.Parameters.Mtglevel != "x")
            {
                List<int> mtglevels = param.Parameters.Mtglevel.Split(",").Select(int.Parse).ToList();
                query = query.Where(w => mtglevels.Contains(w.Mtglevel)).AsQueryable();
            }
            if (param.Parameters.KdperStartwith != "x")
            {
                if (param.Parameters.KdperStartwith == "non-modal")
                {
                    query = query.Where(w => w.Kdper.Trim().StartsWith("1") ||
                                            w.Kdper.Trim().StartsWith("2") ||
                                            w.Kdper.Trim().StartsWith("3") ||
                                            w.Kdper.Trim().StartsWith("4") ||
                                            w.Kdper.Trim().StartsWith("6") ||
                                            w.Kdper.Trim().StartsWith("7") ||
                                            w.Kdper.Trim().StartsWith("8")).AsQueryable();
                } else
                {
                    query = query.Where(w => w.Kdper.Trim().StartsWith(param.Parameters.KdperStartwith)).AsQueryable();
                }
            }
            if (!String.IsNullOrEmpty(param.GlobalFilter))
            {
                query = query.Where(w =>
                    EF.Functions.Like(w.Kdper.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Nmper.Trim(), "%" + param.GlobalFilter + "%")
                ).AsQueryable();
            }
            if (param.Parameters.Idunit.ToString() != "0")
            {
                List<long> Iddpas = new List<long>();
                List<long> Idresk = new List<long>();
                Iddpas = await _tukdContext.Dpa.Where(w => w.Idunit == param.Parameters.Idunit && w.Kdtahap.Trim() == "341" && !String.IsNullOrEmpty(w.Tglsah.ToString())).Select(s => s.Iddpa).ToListAsync();
                if (Iddpas.Count() == 0)
                {
                    Iddpas = await _tukdContext.Dpa.Where(w => w.Idunit == param.Parameters.Idunit && w.Kdtahap.Trim() == "321" && !String.IsNullOrEmpty(w.Tglsah.ToString())).Select(s => s.Iddpa).ToListAsync();
                }
                if (Iddpas.Count() > 0 && param.Parameters.Idkeg.ToString() != "0")
                {
                    Idresk = await _tukdContext.Dpar.Where(w => Iddpas.Contains(w.Iddpa) && w.Idkeg == param.Parameters.Idkeg).Select(s => s.Idrek).Distinct().ToListAsync();
                }
                if (Idresk.Count() > 0)
                {
                    query = query.Where(w => Idresk.Contains(w.Idrek)).AsQueryable();
                }
            }
            Result.Data = await query.Skip(param.Start).Take(param.Rows).OrderBy(o => o.Kdper.Trim()).ToListAsync();
            Result.Totalrecords = await query.CountAsync();
            return Result;
        }

        public async Task<PrimengTableResult<Daftrekening>> ByDpaB(PrimengTableParam<RekGlobalParam> param)
        {
            PrimengTableResult<Daftrekening> Result = new PrimengTableResult<Daftrekening>();
            IQueryable<Daftrekening> query = _tukdContext.Daftrekening.AsQueryable();
            if (param.Parameters.Mtglevel != "x")
            {
                List<int> mtglevels = param.Parameters.Mtglevel.Split(",").Select(int.Parse).ToList();
                query = query.Where(w => mtglevels.Contains(w.Mtglevel)).AsQueryable();
            }
            if (param.Parameters.KdperStartwith != "x")
            {
                if (param.Parameters.KdperStartwith == "non-modal")
                {
                    query = query.Where(w => w.Kdper.Trim().StartsWith("1") ||
                                            w.Kdper.Trim().StartsWith("2") ||
                                            w.Kdper.Trim().StartsWith("3") ||
                                            w.Kdper.Trim().StartsWith("4") ||
                                            w.Kdper.Trim().StartsWith("6") ||
                                            w.Kdper.Trim().StartsWith("7") ||
                                            w.Kdper.Trim().StartsWith("8")).AsQueryable();
                }
                else
                {
                    query = query.Where(w => w.Kdper.Trim().StartsWith(param.Parameters.KdperStartwith)).AsQueryable();
                }
            }
            if (!String.IsNullOrEmpty(param.GlobalFilter))
            {
                query = query.Where(w =>
                    EF.Functions.Like(w.Kdper.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Nmper.Trim(), "%" + param.GlobalFilter + "%")
                ).AsQueryable();
            }
            if (param.Parameters.Idunit.ToString() != "0")
            {
                List<long> Iddpas = new List<long>();
                List<long> Idresk = new List<long>();
                string lastTahap = await _tukdContext.Dpa.OrderByDescending(o => o.Kdtahap.Trim()).Select(s => s.Kdtahap.Trim()).FirstOrDefaultAsync();
                Iddpas = await _tukdContext.Dpa.Where(w => w.Idunit == param.Parameters.Idunit && w.Kdtahap.Trim() == lastTahap && !String.IsNullOrEmpty(w.Tglsah.ToString())).Select(s => s.Iddpa).ToListAsync();
                if (Iddpas.Count() > 0)
                {
                    Idresk = await _tukdContext.Dpab.Where(w => Iddpas.Contains(w.Iddpa)).Select(s => s.Idrek).Distinct().ToListAsync();
                }
                if (Idresk.Count() > 0)
                {
                    query = query.Where(w => Idresk.Contains(w.Idrek)).AsQueryable();
                } else
                {
                    query = query.Where(w => w.Idrek == 0).AsQueryable();
                }
            }
            Result.Data = await query.Skip(param.Start).Take(param.Rows).OrderBy(o => o.Kdper.Trim()).ToListAsync();
            Result.Totalrecords = await query.CountAsync();
            return Result;
        }

        public async Task<PrimengTableResult<Daftrekening>> ForStsdetr(PrimengTableParam<RekGlobalParam> param)
        {
            PrimengTableResult<Daftrekening> Result = new PrimengTableResult<Daftrekening>();
            IQueryable<Daftrekening> query = _tukdContext.Daftrekening.AsQueryable();
            IQueryable<Sts> sts_query = _tukdContext.Sts.AsQueryable();
            if (param.Parameters.Idunit.ToString() != "0")
            {
                sts_query = sts_query.Where(w => w.Idunit == param.Parameters.Idunit);
            }
            if (param.Parameters.Idbend.ToString() != "0")
            {
                sts_query = sts_query.Where(w => w.Idbend == param.Parameters.Idbend);
            }
            if (param.Parameters.Idxkode.ToString() != "0")
            {
                sts_query = sts_query.Where(w => w.Idxkode == param.Parameters.Idxkode);
            }
            if (!String.IsNullOrEmpty(param.Parameters.Kdstatus))
            {
                sts_query = sts_query.Where(w => w.Kdstatus.Trim() == param.Parameters.Kdstatus.Trim());
            }
            if (!String.IsNullOrEmpty(param.Parameters.Mtglevel))
            {
                query = query.Where(w => w.Mtglevel == Int32.Parse(param.Parameters.Mtglevel)).AsQueryable();
            }
            if (!String.IsNullOrEmpty(param.Parameters.KdperStartwith))
            {
                query = query.Where(w => w.Kdper.Trim().StartsWith(param.Parameters.KdperStartwith)).AsQueryable();
            }
            List<long> Idsts = await sts_query.Select(s => s.Idsts).ToListAsync();
            List<long> Idresk = await _tukdContext.Stsdetr.Where(w => Idsts.Contains(w.Idsts)).Distinct().Select(s => s.Idrek).ToListAsync();
            query = query.Where(w => !Idresk.Contains(w.Idrek)).AsQueryable();
            if (!String.IsNullOrEmpty(param.GlobalFilter))
            {
                query = query.Where(w =>
                    EF.Functions.Like(w.Kdper.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Nmper.Trim(), "%" + param.GlobalFilter + "%")
                ).AsQueryable();
            }
            Result.Data = await query.Skip(param.Start).Take(param.Rows).ToListAsync();
            Result.Totalrecords = await query.CountAsync();
            return Result;
        }
    }
}
