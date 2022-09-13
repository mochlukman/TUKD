﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TUKD.API.Interface;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Controllers.BPK
{
    [Route("api/[controller]")]
    [ApiController]
    public class BpkpajakdetController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        public BpkpajakdetController(IMapper mapper, IUow uow)
        {
            _uow = uow;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Gets([FromQuery]BpkpajakdetGet param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                List<Bpkpajakdet> data = await _uow.BpkpajakdetRepo.ViewDatas(param);
                return Ok(data);
            }
            catch(Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpGet("{Idbpkpajakdet}")]
        public async Task<IActionResult> Get(long Idbpkpajakdet)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                Bpkpajakdet data = await _uow.BpkpajakdetRepo.ViewData(Idbpkpajakdet);
                return Ok(data);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BpkpajakdetPost param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                List<Bpkpajakdet> Result = new List<Bpkpajakdet>();
                if(param.Idpajak.Count() > 0)
                {
                    for(var i = 0; i < param.Idpajak.Count(); i++)
                    {
                        Bpkpajakdet post = new Bpkpajakdet
                        {
                            Datecreate = DateTime.Now,
                            Idbpkpajak = param.Idbpkpajak,
                            Idpajak = param.Idpajak[i],
                            Nilai = 0
                        };
                        bool check = await _uow.BpkpajakdetRepo.isExist(w => w.Idbpkpajak == param.Idbpkpajak && w.Idpajak == param.Idpajak[i]);
                        if (!check)
                        {
                            Bpkpajakdet insert = await _uow.BpkpajakdetRepo.Add(post);
                            if (insert != null)
                                Result.Add(await _uow.BpkpajakdetRepo.ViewData(insert.Idbpkpajakdet));
                        }
                    }
                }
                return Ok(Result);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]BpkpajakdetUpdate param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Bpkpajakdet post = _mapper.Map<Bpkpajakdet>(param);
            post.Dateupdate = DateTime.Now;
            try
            {
                bool update = await _uow.BpkpajakdetRepo.Update(post);
                if (update) {
                    Bpkpajakdet data = await _uow.BpkpajakdetRepo.Get(w => w.Idbpkpajakdet == post.Idbpkpajakdet);
                    List<Bpkpajakstrdet> bpkpajakstrdets = await _uow.BpkpajakstrdetRepo.Gets(w => w.Idbpkpajak == data.Idbpkpajak);
                    if(bpkpajakstrdets.Count() > 0)
                    {
                        for(var i = 0; i < bpkpajakstrdets.Count(); i++)
                        {
                            bpkpajakstrdets[i].Nilai = await _uow.BpkpajakdetRepo.sumNilai(data.Idbpkpajak);
                            await _uow.BpkpajakstrdetRepo.Update(bpkpajakstrdets[i]);
                        }
                    }
                    return Ok(await _uow.BpkpajakdetRepo.ViewData(post.Idbpkpajakdet));
                }
                return BadRequest("Update Gagal");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpDelete("{Idbpkpajakdet}")]
        public async Task<IActionResult> Delete(long Idbpkpajakdet)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                Bpkpajakdet data = await _uow.BpkpajakdetRepo.ViewData(Idbpkpajakdet);
                if (data == null) return BadRequest("Data Tidak Ditemukan");
                _uow.BpkpajakdetRepo.Remove(data);
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
    }
}