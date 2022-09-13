using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TUKD.API.Dto;
using TUKD.API.Interface;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Controllers.STS
{
    [Route("api/[controller]")]
    [ApiController]
    public class StsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        private readonly TukdContext _ctx;
        private readonly DbConnection _dbConnection;
        public StsController(IMapper mapper, IUow uow, TukdContext tukdContext, DbConnection dbConnection)
        {
            _uow = uow;
            _mapper = mapper;
            _ctx = tukdContext;
            _dbConnection = dbConnection;
        }
        [HttpGet("noreg")]
        public async Task<IActionResult> GenerateNoReg([FromQuery][Required]long Idunit)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                string Noreg = await _uow.StsRepo.GenerateNoReg(Idunit);
                return Ok(new { Noreg });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpGet("for-bku-bud")]
        public async Task<IActionResult> GetsBkuBud([FromQuery][Required] PrimengTableParam<StsGetForBkuBud> param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                PrimengTableResult<Sts> data = await _uow.StsRepo.ForBkuBud(param);
                return Ok(data);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Gets([FromQuery] StsGet param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                if (param.Kdstatus == "x")
                {
                    List<Sts> datas = await _uow.StsRepo.ViewDatas(param.Idunit, param.Idxkode);
                    return Ok(datas);
                }
                else
                {
                    List<string> status = param.Kdstatus.Split(",").ToList();
                    List<Sts> datas = await _uow.StsRepo.ViewDatas(param.Idunit, param.Idbend, param.Idxkode, status);
                    return Ok(datas);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpGet("paging")]
        public async Task<IActionResult> Paging([FromQuery][Required] PrimengTableParam<StsGet> param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                PrimengTableResult<Sts> data = await _uow.StsRepo.Paging(param);
                return Ok(data);
            }
            catch(Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpGet("For-Bku")]
        public async Task<IActionResult> ForBku([FromQuery] StsGet param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                List<Bkusts> Bkusts = await _uow.BkustsRepo.Gets(w => w.Idunit == param.Idunit && w.Idbend == param.Idbend);
                List<long> ids = new List<long>();
                if (Bkusts.Count() > 0)
                {
                    Bkusts.ForEach(f =>
                    {
                        ids.Add(f.Idsts);
                    });
                }
                List<string> status = param.Kdstatus.Split(",").ToList();
                List<Sts> datas = await _uow.StsRepo.ViewDatasForBku(ids, param.Idunit, param.Idbend, param.Idxkode, status);
                return Ok(datas);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpGet("{Idsts}")]
        public async Task<IActionResult> Get(long Idsts)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                Sts data = await _uow.StsRepo.ViewData(Idsts);
                return Ok(data);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StsPost param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Sts post = _mapper.Map<Sts>(param);
            post.Datecreate = DateTime.Now;
            bool checkNo = await _uow.StsRepo.isExist(w => w.Nosts.Trim() == param.Nosts.Trim() && w.Kdstatus.Trim() == post.Kdstatus.Trim() && w.Idxkode == post.Idxkode && w.Idbend == post.Idbend);
            if (checkNo) return BadRequest("No STS Telah Digunakan");
            try
            {
                using (var trans = await _ctx.Database.BeginTransactionAsync())
                {
                    try
                    {
                        if ((param.Kdstatus.Trim() == "13") || (param.Kdstatus.Trim() == "17"))
                        {
                            List<ValidationValue> validation1 = new List<ValidationValue>();

                            using (IDbConnection dbConnection = _dbConnection)
                            {
                                dbConnection.Open();
                                var SpName = "WSP_VALIDATIONSTS_UP";
                                var parameters = new DynamicParameters();
                                parameters.Add("@IDUNIT", param.Idunit.ToString());
                                parameters.Add("@IDBEND", param.Idbend.ToString());
                                validation1.AddRange(dbConnection.QueryAsync<ValidationValue>(SpName, parameters, commandType: CommandType.StoredProcedure).Result.ToList());

                                if (validation1.Count > 0)
                                {
                                    if (validation1[0].Tot < param.Nilaiup)

                                    {
                                        return BadRequest("Nilai UP " + param.Nilaiup.ToString() + " melebihi Nilai Realisasi UP yang bisa dimasukan " + validation1[0].Tot.ToString());
                                    }
                                }
                            }
                        }
                        Sts Insert = await _uow.StsRepo.Add(post);

                        if (Insert != null)
                        {
                            if (!String.IsNullOrEmpty(param.Idskp.ToString()) || param.Idskp != 0)
                            {
                                bool check_skpsts = await _uow.SkpstsRepo.isExist(w => w.Idskp == param.Idskp && w.Idsts == Insert.Idsts);
                                Skpsts skpsts = new Skpsts
                                {
                                    Idskp = param.Idskp,
                                    Idsts = Insert.Idsts
                                };
                                if (check_skpsts)
                                {
                                    _uow.SkpstsRepo.Remove(skpsts);
                                }
                                await _uow.SkpstsRepo.Add(skpsts);
                                await _uow.Complete();
                                using (IDbConnection dbConnection = _dbConnection)
                                {
                                    string query = @"INSERT INTO STSDETD(IDREK,IDNOJETRA,IDSTS,NILAI)
                                                     SELECT IDREK, IDNOJETRA, @IDSTS, SUM(NILAI) as NILAI FROM SKPDET
                                                    WHERE IDSKP = @IDSKP GROUP BY IDREK, IDNOJETRA";
                                    await _dbConnection.OpenAsync();
                                    var parameters = new DynamicParameters();
                                    parameters.Add("@IDSTS", Insert.Idsts);
                                    parameters.Add("@IDSKP", param.Idskp);
                                    dbConnection.Execute(query, parameters);
                                    dbConnection.Close();
                                }
                            }
                        }
                        trans.Commit();
                        Sts view = await _uow.StsRepo.ViewData(Insert.Idsts);
                        if (view != null)
                        {
                            if (!String.IsNullOrEmpty(param.Idskp.ToString()) || param.Idskp != 0)
                            {
                                view.Skpsts = await _uow.SkpstsRepo.ViewData(param.Idskp, view.Idsts);
                            }
                        }
                        return Ok(view);
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                        return BadRequest(ModelState);
                    }
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] StsPost param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Sts post = _mapper.Map<Sts>(param);
            post.Dateupdate = DateTime.Now;
            Sts old = await _uow.StsRepo.Get(w => w.Nosts.Trim() == param.Nosts.Trim() && w.Kdstatus.Trim() == post.Kdstatus.Trim() && w.Idxkode == post.Idxkode && w.Idbend == post.Idbend);

            long currentTotal = 0;

            if (old != null)
            {
                if (old.Idsts != param.Idsts)
                {
                    return BadRequest("No STS Telah Digunakan");
                }
            }
            try
            {
                if ((param.Kdstatus.Trim() == "13") || (param.Kdstatus.Trim() == "17") || (param.Kdstatus.Trim() == "19") || (param.Kdstatus.Trim() == "78") || (param.Kdstatus.Trim() == "79"))
                {
                    List<ValidationValue> validation1 = new List<ValidationValue>();

                    using (IDbConnection dbConnection = _dbConnection)
                    {
                        dbConnection.Open();
                        var SpName = "WSP_VALIDATIONSTS_UP";
                        var parameters = new DynamicParameters();
                        parameters.Add("@IDUNIT", param.Idunit.ToString());
                        parameters.Add("@IDBEND", param.Idbend.ToString());
                        validation1.AddRange(dbConnection.QueryAsync<ValidationValue>(SpName, parameters, commandType: CommandType.StoredProcedure).Result.ToList());

                        if (validation1.Count > 0)
                        {
                            if ((validation1[0].Tot - param.Nilaiup) < 0)
                            {
                                currentTotal = (long)(validation1[0].Tot - param.Nilaiup);
                                return BadRequest("Nilai UP " + param.Nilaiup.ToString() + " melebihi Nilai Realisasi UP yang bisa dimasukan " + currentTotal.ToString());
                            }
                        }
                    }
                }
                bool Update = await _uow.StsRepo.Update(post);
                if (Update)
                {
                    Sts data = await _uow.StsRepo.ViewData(post.Idsts);
                    Skpsts skpsts = await _uow.SkpstsRepo.Get(w => w.Idsts == data.Idsts);
                    if(skpsts != null)
                    {
                        data.Skpsts = await _uow.SkpstsRepo.ViewData(skpsts.Idskp, skpsts.Idsts);
                    }
                    return Ok(data);
                }
                return BadRequest("Update Gagal");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpDelete("{Idsts}")]
        public async Task<IActionResult> Delete(long Idsts)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                Sts data = await _uow.StsRepo.ViewData(Idsts);
                if (data == null) return BadRequest("Data Tidak Ditemukan");
                List<Stsdetb> stsdetbs = await _uow.StsdetbRepo.Gets(w => w.Idsts == data.Idsts);
                List<Stsdetd> stsdetds = await _uow.StsdetdRepo.Gets(w => w.Idsts == data.Idsts);
                List<Stsdetr> stsdetrs = await _uow.StsdetrRepo.Gets(w => w.Idsts == data.Idsts);
                if (stsdetbs.Count() > 0 || stsdetds.Count() > 0 || stsdetrs.Count() > 0)
                    return BadRequest("Hapus Gagal, Data Telah Digunakan Pada Rincian");
                _uow.StsRepo.Remove(data);
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
        [HttpPut("Pengesahan")]
        public async Task<IActionResult> Pengesahan([FromBody] StsPost param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Sts post = _mapper.Map<Sts>(param);
            try
            {
                bool sahkan = await _uow.StsRepo.Pengesahan(post);
                if (sahkan)
                {
                    Sts data = await _uow.StsRepo.ViewData(post.Idsts);
                    Skpsts skpsts = await _uow.SkpstsRepo.Get(w => w.Idsts == data.Idsts);
                    if (skpsts != null)
                    {
                        data.Skpsts = await _uow.SkpstsRepo.ViewData(skpsts.Idskp, skpsts.Idsts);
                    }
                    return Ok(data);
                }
                return BadRequest("Pengesahan Gagal");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
    }
}