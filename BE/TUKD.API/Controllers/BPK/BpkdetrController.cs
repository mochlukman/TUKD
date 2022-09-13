using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TUKD.API.Interface;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;


namespace TUKD.API.Controllers.BPK
{
    [Route("api/[controller]")]
    [ApiController]
    public class BpkdetrController : ControllerBase
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly DbConnection _dbConnection;
        public BpkdetrController(IUow uow, IMapper mapper, DbConnection dbConnection)
        {
            _uow = uow;
            _mapper = mapper;
            _dbConnection = dbConnection;
        }
        [HttpGet]
        public async Task<IActionResult> Gets([FromQuery] BpkdetrGet param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                List<Bpkdetr> datas = await _uow.BpkdetrRepo.ViewDatas(param);
                return Ok(datas);
            } catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpGet("paging")]
        public async Task<IActionResult> Paging([FromQuery] PrimengTableParam<BpkdetrGet> param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                PrimengTableResult<Bpkdetr> datas = await _uow.BpkdetrRepo.Paging(param);
                return Ok(datas);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpGet("{Idbpkdet}")]
        public async Task<IActionResult> Get(long Idbpkdet)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                Bpkdetr data = await _uow.BpkdetrRepo.ViewData(Idbpkdet);
                return Ok(data);
            }
            catch (Exception e)

            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BpkdetrPost param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            List<Bpkdetr> views = new List<Bpkdetr> { };
            try
            {
                if (param.Idrek.Count() > 0)
                {
                    for (var i = 0; i < param.Idrek.Count(); i++)
                    {
                        Bpkdetr insert = await _uow.BpkdetrRepo.Add(new Bpkdetr
                        {
                            Idnojetra = 21,
                            Datecreate = DateTime.Now,
                            Idkeg = param.Idkeg,
                            Idrek = param.Idrek[i],
                            Idbpk = param.Idbpk,
                            Nilai = 0
                        });
                        if (insert != null)
                        {
                            views.Add(await _uow.BpkdetrRepo.ViewData(insert.Idbpkdetr));
                        }
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
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]BpkdetrUpdate param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Bpkdetr post = _mapper.Map<Bpkdetr>(param);
            post.Dateupdate = DateTime.Now;

            long currentTotal = 0;
            long NilBelanja = 0;
            long RealBelanja = 0;
            long SisaBelanja = 0;

            List<ValidationValue> validation1 = new List<ValidationValue>();
            try
            {
                Bpk bpk = await _uow.BpkRepo.Get(w => w.Idbpk == post.Idbpk);
                using (IDbConnection dbConnection = _dbConnection)
                {
                    if (bpk.Idjbayar == 1)
                    { }
                    dbConnection.Open();
                    var SpName = "WSP_VALIDATIONBPK_TUNAI";
                    var parameters = new DynamicParameters();
                    parameters.Add("@IDUNIT", bpk.Idunit.ToString());
                    parameters.Add("@IDBEND", bpk.Idbend.ToString());
                    parameters.Add("@IDBPK", bpk.Idbpk.ToString());
                    validation1.AddRange(dbConnection.QueryAsync<ValidationValue>(SpName, parameters, commandType: CommandType.StoredProcedure).Result.ToList());

                    if (validation1.Count > 0)
                    {

                        if ((validation1[0].Tot - param.Nilai) < 0)
                        {
                            currentTotal = (long)(validation1[0].Tot - param.Nilai);
                            NilBelanja = (long)(validation1[0].Penambah);
                            RealBelanja = (long)(validation1[0].Pengurang);
                            SisaBelanja = (long)(validation1[0].Tot);
                            return BadRequest("Nilai Belanja " + NilBelanja.ToString() + ", Nilai Total Belanja " + RealBelanja.ToString() + ", Nilai Belanja yang masih bisa diinput " + SisaBelanja.ToString());
                        }
                    }
                }


                    bool Update = await _uow.BpkdetrRepo.Update(post);
                if (Update)
                {
                    return Ok(await _uow.BpkdetrRepo.ViewData(post.Idbpkdetr));
                }
                return BadRequest("Update Gagal");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpDelete("{Idbpkdet}")]
        public async Task<IActionResult> Delete(long Idbpkdet)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                Bpkdetr data = await _uow.BpkdetrRepo.Get(w => w.Idbpkdetr == Idbpkdet);
                if (data == null) return BadRequest("Data Tidak Ditemukank");
                _uow.BpkdetrRepo.Remove(data);
                if (await _uow.Complete())
                    return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
    }
}