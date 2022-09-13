using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TUKD.API.Dto;
using TUKD.API.Interface;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Controllers.SPM
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpmController : ControllerBase
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public SpmController(IUow uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Gets([FromQuery] SpmGet param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                List<Spm> datas = await _uow.SpmRepo.ViewDatas(param);
                return Ok(datas);
            }catch(Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpGet("noreg")]
        public async Task<IActionResult> GenerateNoReg(
            [FromQuery][Required]long Idunit,
            [FromQuery]int Idxkode,
            [FromQuery]long Idbend,
            [FromQuery]string Kdstatus
            )
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                string Noreg = "";
                if(Idxkode.ToString() == "0" && Idbend.ToString() == "0" && String.IsNullOrEmpty(Kdstatus))
                {
                    Noreg = await _uow.SpmRepo.GenerateNoReg(Idunit);
                } else
                {
                    Noreg = await _uow.SpmRepo.GenerateNoReg(Idunit, Idbend, Idxkode, Kdstatus);
                }
                
                return Ok(new { Noreg });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SpmPost param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Spm post = _mapper.Map<Spm>(param);
            if (post.Kdstatus.Trim() != "24") post.Idkeg = 0;
            post.Createdate = DateTime.Now;
            post.Createby = User.Claims.FirstOrDefault().Value;
            bool check = await _uow.SpmRepo.isExist(w =>
                w.Idunit == param.Idunit && w.Kdstatus.Trim() == param.Kdstatus.Trim() &&
                w.Idxkode == param.Idxkode && w.Idspd == param.Idspd && w.Idspp == param.Idspp);
            if (check) return BadRequest("No. SPP & No. SPD telah digunakan");
            try
            {
                Spm insert = await _uow.SpmRepo.Add(post);
                if (insert != null)
                {
                    return Ok(await _uow.SpmRepo.ViewData(insert.Idspm));
                }
                return BadRequest("Input Gagal");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]SpmPost param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Spm post = _mapper.Map<Spm>(param);
            post.Updatedate = DateTime.Now;
            post.Updateby = User.Claims.FirstOrDefault().Value;
            Spm check = await _uow.SpmRepo.Get(w =>
                w.Idunit == param.Idunit && w.Kdstatus.Trim() == param.Kdstatus.Trim() &&
                w.Idxkode == param.Idxkode && w.Idspd == param.Idspd && w.Idspp == param.Idspp);
            if (check != null) {
                if(check.Idspm != param.Idspm)
                {
                    return BadRequest("No. SPP & No. SPD telah digunakan");
                }
            }
            if (check.Tglvalid != null)
            {
                return BadRequest("Gagal Update, Spp Telah Disahkan");
            }
            try
            {
                bool update = await _uow.SpmRepo.Update(post);
                if (update)
                {
                    return Ok(await _uow.SpmRepo.ViewData(post.Idspm));
                }
                return BadRequest("Update Gagal");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpPut("pengesahan")]
        public async Task<IActionResult> Verifikasi([FromBody]SpmPost param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Spm post = _mapper.Map<Spm>(param);
            post.Updatedate = DateTime.Now;
            post.Updateby = User.Claims.FirstOrDefault().Value;
            post.Validby = User.Claims.FirstOrDefault().Value;
            try
            {
                bool update = await _uow.SpmRepo.Pengesahan(post);
                if (update)
                {
                    return Ok(await _uow.SpmRepo.ViewData(post.Idspm));
                }
                return BadRequest("Update Gagal");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpPut("penolakan")]
        public async Task<IActionResult> Penolakan([FromBody]SpmPost param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Spm post = _mapper.Map<Spm>(param);
            post.Updatedate = DateTime.Now;
            post.Updateby = User.Claims.FirstOrDefault().Value;
            post.Approveby = User.Claims.FirstOrDefault().Value;
            try
            {
                bool update = await _uow.SpmRepo.Penolakan(post);
                if (update)
                {
                    return Ok(await _uow.SpmRepo.ViewData(post.Idspm));
                }
                return BadRequest("Update Gagal");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpDelete("{Idspm}")]
        public async Task<IActionResult> Delete(long Idspm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                Spm data = await _uow.SpmRepo.Get(w => w.Idspm == Idspm);
                if (data == null) return BadRequest("Data Tidak Ditemukan");
                _uow.SpmRepo.Remove(data);
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
        [HttpGet("tracking/{Idspm}")]
        public async Task<IActionResult> Tracking(long Idspm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                List<DataTracking> data = await _uow.SpmRepo.TrackingData(Idspm);
                return Ok(data);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
    }
}