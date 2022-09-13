using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TUKD.API.Interface;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JjnspajakController : ControllerBase
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public JjnspajakController(IUow uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            try
            {
                List<Jnspajak> datas = await _uow.JnspajakRepo.Gets();
                return Ok(datas);
            }catch(Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpGet("{Idjnspajak}")]
        public async Task<IActionResult> Get(int Idjnspajak)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                Jnspajak data = await _uow.JnspajakRepo.Get(w => w.Idjnspajak == Idjnspajak);
                return Ok(data);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]JnspajakPost param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Jnspajak post = _mapper.Map<Jnspajak>(param);
            try
            {
                Jnspajak insert = await _uow.JnspajakRepo.Add(post);
                if(insert != null)
                    return Ok(insert);
                return BadRequest("Input Gagal");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]JnspajakPost param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Jnspajak data = await _uow.JnspajakRepo.Get(w => w.Idjnspajak == param.Idjnspajak);
            if (data == null) return BadRequest("Data Tidak Ditemukan");
            data.Nmjnspajak = param.Nmjnspajak;
            try
            {
                bool update = await _uow.JnspajakRepo.Update(data);
                if (update)
                    return Ok(data);
                return BadRequest("Update Gagal");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpDelete("{Idjnspajak}")]
        public async Task<IActionResult> Delete(int Idjnspajak)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                Jnspajak data = await _uow.JnspajakRepo.Get(w => w.Idjnspajak == Idjnspajak);
                if (data == null) return BadRequest("Data Tidak Ditemukan");
                _uow.JnspajakRepo.Remove(data);
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