using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TUKD.API.Dto;
using TUKD.API.Interface;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Controllers.SPP
{
    [Route("api/[controller]")]
    [ApiController]
    public class SppdetrController : ControllerBase
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly DbConnection _dbConnection;
        public SppdetrController(IUow uow, IMapper mapper, DbConnection dbConnection)
        {
            _uow = uow;
            _mapper = mapper;
            _dbConnection = dbConnection;
        }
        [HttpGet]
        public async Task<IActionResult> Gets(
            [FromQuery][Required]long Idspp,
            [FromQuery][Required]long Idkeg
            )
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                Spp spp = await _uow.SppRepo.Get(w => w.Idspp == Idspp);
                decimal? totspd = await _uow.SpddetrRepo.TotalNilaiSpd(spp.Idspd);
                decimal? totalSpp = 0;
                List<long> Ids = new List<long> { };
                Ids.AddRange(await _uow.SppRepo.GetIds(spp.Idunit, spp.Idxkode, spp.Kdstatus, spp.Idspd));
                if (Ids.Count() > 0)
                {
                    totalSpp = await _uow.SppdetrRepo.TotalNilaiSpp(Ids);
                }
                List<Sppdetr> datas = new List<Sppdetr> { };
                if (Idkeg != 0)
                {
                    datas.AddRange(await _uow.SppdetrRepo.Gets(w => w.Idspp == Idspp && w.Idkeg == Idkeg));
                } else
                {
                    datas.AddRange(await _uow.SppdetrRepo.Gets(w => w.Idspp == Idspp));
                }

                List<SppdetrView> views = _mapper.Map<List<SppdetrView>>(datas);
                if (views.Count() > 0)
                {
                    foreach (var d in views)
                    {
                        if (!String.IsNullOrEmpty(d.Idrek.ToString()) || d.Idrek != 0)
                        {
                            d.IdrekNavigation = await _uow.DaftrekeningRepo.Get(w => w.Idrek == d.Idrek);
                        }
                        d.Totspd = totspd;
                        d.Sisa = totalSpp - d.Nilai;
                    }
                }
                return Ok(views);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpGet("treetable-from-subkegiatan")]
        public async Task<IActionResult> TreetableFromSubkegiatan(
            [FromQuery][Required]long Idspp
            )
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Spp spp = await _uow.SppRepo.Get(w => w.Idspp == Idspp);
            decimal? totspd = await _uow.SpddetrRepo.TotalNilaiSpd(spp.Idspd);
            decimal? totalSpp = 0;
            List<long> Ids = new List<long> { };
            Ids.AddRange(await _uow.SppRepo.GetIds(spp.Idunit, spp.Idxkode, spp.Kdstatus, spp.Idspd));
            if (Ids.Count() > 0)
            {
                totalSpp = await _uow.SppdetrRepo.TotalNilaiSpp(Ids);
            }
            try
            {
                List<SppdetrViewTreeRoot> data = await _uow.SppdetrRepo.TreetableFromSubkegiatan(Idspp, totalSpp, totalSpp);
                return Ok(data);
            }catch(Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpGet("{Idsppdetr}")]
        public async Task<IActionResult> Get(int Idsppdetr)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                Sppdetr data = await _uow.SppdetrRepo.Get(w => w.Idsppdetr == Idsppdetr);
                SppdetrView view = _mapper.Map<SppdetrView>(data);
                if (view != null)
                {
                    if (!String.IsNullOrEmpty(view.Idrek.ToString()) || view.Idrek != 0)
                    {
                        view.IdrekNavigation = await _uow.DaftrekeningRepo.Get(w => w.Idrek == view.Idrek);
                    }
                }
                return Ok(view);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SppdetrPost param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                Spp spp = await _uow.SppRepo.Get(w => w.Idspp == param.Idspp);
                decimal? totspd = await _uow.SpddetrRepo.TotalNilaiSpd(spp.Idspd);
                decimal? totalSpp = 0;
                List<long> Ids = new List<long> { };
                Ids.AddRange(await _uow.SppRepo.GetIds(spp.Idunit, spp.Idxkode, spp.Kdstatus, spp.Idspd));
                if (Ids.Count() > 0)
                {
                    totalSpp = await _uow.SppdetrRepo.TotalNilaiSpp(Ids);
                }
                List<SppdetrView> views = new List<SppdetrView> { };
                if(param.Idrek.Count() > 0)
                {
                    for (var i = 0; i < param.Idrek.Count(); i++){
                        Sppdetr insert = await _uow.SppdetrRepo.Add(new Sppdetr
                        {
                            Idnojetra = 21,
                            Createdate = DateTime.Now,
                            Createby = User.Claims.FirstOrDefault().Value,
                            Idkeg = param.Idkeg,
                            Idrek = param.Idrek[i],
                            Idspp = param.Idspp,
                            Nilai = 0
                        });
                        if(insert != null)
                        {
                            views.Add(_mapper.Map<SppdetrView>(insert));
                        }
                    }
                }
                if (views.Count() > 0)
                {
                    foreach(var v in views)
                    {
                        if (!String.IsNullOrEmpty(v.Idrek.ToString()) || v.Idrek != 0)
                        {
                            v.IdrekNavigation = await _uow.DaftrekeningRepo.Get(w => w.Idrek == v.Idrek);
                        }
                        v.Totspd = totspd;
                        v.Sisa = totalSpp - v.Nilai;
                    }
                    
                }
                return Ok(views);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpPost("multi-keg")]
        public async Task<IActionResult> PostMultiKeg([FromBody] List<SppdetrPostMultiKeg> param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            List<Sppdetr> posts = _mapper.Map<List<Sppdetr>>(param);
            foreach(var p in posts)
            {
                p.Createdate = DateTime.Now;
                p.Createby = User.Claims.FirstOrDefault().Value;
            }
            try
            {
                _uow.SppdetrRepo.AddRange(posts);
                if (await _uow.Complete())
                    return Ok();
                return BadRequest("Input Gagal");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] SppdetrUpdate param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Sppdetr post = _mapper.Map<Sppdetr>(param);
            post.Updatedate = DateTime.Now;
            post.Updateby = User.Claims.FirstOrDefault().Value;
            try
            {
                Spp spp = await _uow.SppRepo.Get(w => w.Idspp == param.Idspp);
                Spptag spptag = await _uow.SpptagRepo.Get(w => w.Idspp == param.Idspp);
                Daftrekening daftrek = await _uow.DaftrekeningRepo.Get(w => w.Idrek == post.Idrek);

                decimal? totspd = await _uow.SpddetrRepo.TotalNilaiSpd(spp.Idspd);
                decimal? totalSpp = 0;


                List<long> Ids = new List<long> { };
                Ids.AddRange(await _uow.SppRepo.GetIds(spp.Idunit, spp.Idxkode, spp.Kdstatus, spp.Idspd));
                if (Ids.Count() > 0)
                {
                    totalSpp = await _uow.SppdetrRepo.TotalNilaiSpp(Ids);
                }
                List<ValidationValue> validation1 = new List<ValidationValue>();
                List<ValidationValue> validation2 = new List<ValidationValue>();

                long currentTotal1 = 0;
                long currentTotal2 = 0;

                long Nildpa = 0;
                long RealDPA = 0;
                long Sisa1 = 0;

                long Niltag = 0;
                long RealTagihan = 0;
                long Sisa2 = 0;

                Sppdetr current_data = await _uow.SppdetrRepo.Get(w => w.Idsppdetr == post.Idsppdetr);
                using (IDbConnection dbConnection = _dbConnection)
                {
                    dbConnection.Open();
                    var SpName = "WSP_VALIDATIONSPP_REK_LS";
                    var parameters = new DynamicParameters();
                    parameters.Add("@IDUNIT", spp.Idunit.ToString());
                    parameters.Add("@IDREK", current_data.Idrek.ToString());
                    parameters.Add("@IDKEG", current_data.Idkeg.ToString());
                    parameters.Add("@TGLSPP", spp.Tglspp);
                    validation1.AddRange(dbConnection.QueryAsync<ValidationValue>(SpName, parameters, commandType: CommandType.StoredProcedure).Result.ToList());
                }
                if (validation1.Count() > 0)
                {
                    currentTotal1 = (long)(validation1[0].Tot - param.Nilai);
                    Nildpa = (long)(validation1[0].Penambah);
                    RealDPA = (long)(validation1[0].Pengurang);
                    Sisa1 = (long)(validation1[0].Tot);

                    if (currentTotal1 < 0)
                    {
                        return BadRequest("Nilai DPA " + param.Nilai.ToString() + ", Nilai Realisasi DPA " + RealTagihan.ToString() + ", Nilai Rekening SPP" + daftrek.Kdper.ToString() + "Sebesar " + post.Nilai.ToString() + ", melebihi sisa DPA yang belum direalisasikan " + Sisa1.ToString());
                    }
                }
                if (spptag.Idtagihan != 0)
                {
                    using (IDbConnection dbConnection = _dbConnection)
                    {
                        dbConnection.Open();
                        var SpName = "WSP_VALIDATIONSPP_TAGIHAN";
                        var parameters = new DynamicParameters();
                        parameters.Add("@IDUNIT", spp.Idunit.ToString());
                        parameters.Add("@IDSPP", spp.Idspp.ToString());
                        parameters.Add("@IDTAGIHAN", spptag.Idtagihan.ToString());
                        parameters.Add("@IDREK", current_data.Idrek.ToString());
                        parameters.Add("@IDKEG", current_data.Idkeg.ToString());
                        parameters.Add("@TGLSPP", spp.Tglspp);
                        validation2.AddRange(dbConnection.QueryAsync<ValidationValue>(SpName, parameters, commandType: CommandType.StoredProcedure).Result.ToList());
                    }
                    if (validation2.Count() > 0)
                    {

                        currentTotal2 = (long)(validation2[0].Tot - param.Nilai);
                        Niltag = (long)(validation2[0].Penambah);
                        RealTagihan = (long)(validation2[0].Pengurang);
                        Sisa2 = (long)(validation2[0].Tot);

                        if (currentTotal2 < 0)
                        {
                            return BadRequest("Nilai Tagihan " + Niltag.ToString() + ", Nilai Realisasi Tagihan " + RealTagihan.ToString() + ", Nilai Rekening " + daftrek.Kdper.ToString() + "Sebesar " + post.Nilai.ToString() + ", melebihi sisa Tagihan yang belum direalisasikan " + Sisa2.ToString());
                        }
                    }
                }
                bool update = await _uow.SppdetrRepo.Update(post);
                if (update)
                {
                    Sppdetr data = await _uow.SppdetrRepo.Get(w => w.Idsppdetr == post.Idsppdetr);
                    SppdetrView view = _mapper.Map<SppdetrView>(data);
                    if (!String.IsNullOrEmpty(view.Idrek.ToString()) || view.Idrek != 0)
                    {
                        view.IdrekNavigation = await _uow.DaftrekeningRepo.Get(w => w.Idrek == view.Idrek);
                    }
                    view.Totspd = totspd;
                    view.Sisa = totalSpp - view.Nilai;
                    return Ok(view);
                }
                return BadRequest("Update Gagal");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpPut("update-nilai")]
        public async Task<IActionResult> PutNilai([FromBody]SppdetrUpdate param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                bool update = await _uow.SppdetrRepo.UpdateNilai(param.Idsppdetr, param.Nilai, DateTime.Now, User.Claims.FirstOrDefault().Value);
                if (update)
                {
                    Sppdetr data = await _uow.SppdetrRepo.Get(w => w.Idsppdetr == param.Idsppdetr);
                    return Ok(data);
                }
                return BadRequest("Gagal Update Nilai");
            }catch(Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpDelete("{Idsppdetr}")]
        public async Task<IActionResult> Delete(long Idsppdetr)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                Sppdetr data = await _uow.SppdetrRepo.Get(w => w.Idsppdetr == Idsppdetr);
                if (data == null) return BadRequest("Data Tidak Ditemukan");
                _uow.SppdetrRepo.Remove(data);
                if (await _uow.Complete())
                    return Ok();
                return BadRequest("Hapus Gagal");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpGet("total-nilai")]
        public async Task<IActionResult> GetTotalNilai(
            [FromQuery][Required]long Idunit,
            [FromQuery][Required]int Idxkode,
            [FromQuery][Required]string Kdstatus,
            [FromQuery][Required]long Idspd
            )
        {
            try
            {
                decimal? TotalSpp = 0;
                List<long> Ids = new List<long> { };
                Ids.AddRange(await _uow.SppRepo.GetIds(Idunit, Idxkode, Kdstatus, Idspd));
                if (Ids.Count() > 0)
                {
                    TotalSpp = await _uow.SppdetrRepo.TotalNilaiSpp(Ids);
                }
                return Ok(new { TotalSpp });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
    }
}