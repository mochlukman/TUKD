using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class SppController : ControllerBase
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public SppController(IUow uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Gets([FromQuery]SppGet param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                List<Spp> datas = await _uow.SppRepo.ViewDatas(param);
                return Ok(datas);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpGet("noreg")]
        public async Task<IActionResult> GenerateNoReg([FromQuery][Required]long Idunit)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                string Noreg = await _uow.SppRepo.GenerateNoReg(Idunit);
                return Ok(new { Noreg });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SppPost param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Spp post = _mapper.Map<Spp>(param);
            if (post.Kdstatus.Trim() != "24") post.Idkeg = 0;
            post.Createdate = DateTime.Now;
            post.Createby = User.Claims.FirstOrDefault().Value;
            try
            {
                Spp insert = await _uow.SppRepo.Add(post);
                if(insert != null)
                {
                    return Ok(await _uow.SppRepo.ViewData(insert.Idspp));
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
        public async Task<IActionResult> Put([FromBody]SppPost param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Spp post = _mapper.Map<Spp>(param);
            post.Updatedate = DateTime.Now;
            post.Updateby = User.Claims.FirstOrDefault().Value;
            try
            {
                bool update = await _uow.SppRepo.Update(post);
                if (update)
                {
                    return Ok(await _uow.SppRepo.ViewData(post.Idspp));
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
        public async Task<IActionResult> Verifikasi([FromBody]SppPost param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Spp post = _mapper.Map<Spp>(param);
            post.Updatedate = DateTime.Now;
            post.Updateby = User.Claims.FirstOrDefault().Value;
            post.Validby = User.Claims.FirstOrDefault().Value;
            try
            {
                bool update = await _uow.SppRepo.Pengesahan(post);
                if (update)
                {
                    return Ok(await _uow.SppRepo.ViewData(post.Idspp));
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
        public async Task<IActionResult> Penolakan([FromBody]SppPost param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Spp post = _mapper.Map<Spp>(param);
            post.Updatedate = DateTime.Now;
            post.Updateby = User.Claims.FirstOrDefault().Value;
            post.Approveby = User.Claims.FirstOrDefault().Value;
            try
            {
                bool update = await _uow.SppRepo.Penolakan(post);
                if (update)
                {
                    return Ok(await _uow.SppRepo.ViewData(post.Idspp));
                }
                return BadRequest("Update Gagal");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpDelete("{Idspp}")]
        public async Task<IActionResult> Delete(long Idspp)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                Spp data = await _uow.SppRepo.Get(w => w.Idspp == Idspp);
                if (data == null) return BadRequest("Data Tidak Ditemukan");
                _uow.SppRepo.Remove(data);
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
        [HttpGet("tracking/{Idspp}")]
        public async Task<IActionResult> Tracking(long Idspp)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                List<DataTracking> data = await _uow.SppRepo.TrackingData(Idspp);
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