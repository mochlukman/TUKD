using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TUKD.API.Dto;
using TUKD.API.Interface;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Controllers.SKP
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkpController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        public SkpController(IMapper mapper, IUow uow)
        {
            _uow = uow;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Gets([FromQuery] SkpGet param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                List<Skp> datas = await _uow.SkpRepo.ViewDatas(param.Idunit, param.Idbend, param.Idxkode, param.Kdstatus, param.Istglvalid);
                List<SkpView> views = _mapper.Map<List<SkpView>>(datas);
                if(views.Count() > 0)
                {
                    foreach(var v in views)
                    {
                        v.KdstatusNavigation = await _uow.StattrsRepo.Get(w => w.Kdstatus.Trim() == v.Kdstatus.Trim());
                    }
                }
                return Ok(views);
            }catch(Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpGet("{Idskp}")]
        public async Task<IActionResult> Get(long Idskp)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                Skp data = await _uow.SkpRepo.ViewData(Idskp);
                SkpView view = _mapper.Map<SkpView>(data);
                if(view != null)
                {
                    view.KdstatusNavigation = await _uow.StattrsRepo.Get(w => w.Kdstatus.Trim() == view.Kdstatus.Trim());
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
        public async Task<IActionResult> Post([FromBody] SkpPost param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Skp post = _mapper.Map<Skp>(param);
            bool checkNoSkp = await _uow.SkpRepo.isExist(w => w.Noskp.Trim() == param.Noskp.Trim() && w.Kdstatus.Trim() == post.Kdstatus.Trim() && w.Idxkode == post.Idxkode && w.Idbend == post.Idbend);
            if (checkNoSkp) return BadRequest("No SKP Telah Digunakan");
            try
            {
                Skp Insert = await _uow.SkpRepo.Add(post);
                if (Insert != null)
                {
                    SkpView view = _mapper.Map<SkpView>(Insert);
                    if (view != null)
                    {
                        view.KdstatusNavigation = await _uow.StattrsRepo.Get(w => w.Kdstatus.Trim() == view.Kdstatus.Trim());
                    }
                    return Ok(view);
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
        public async Task<IActionResult> Update([FromBody] SkpPost param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Skp post = _mapper.Map<Skp>(param);
            Skp old = await _uow.SkpRepo.Get(w => w.Noskp.Trim() == param.Noskp.Trim() && w.Kdstatus.Trim() == post.Kdstatus.Trim() && w.Idxkode == post.Idxkode && w.Idbend == post.Idbend);
            if (old != null)
            {
                if (old.Idskp != param.Idskp)
                {
                    return BadRequest("No SKP Telah Digunakan");
                }
            }
            try
            {
                bool Update = await _uow.SkpRepo.Update(post);
                if (Update)
                {
                    Skp data = await _uow.SkpRepo.ViewData(post.Idskp);
                    SkpView view = _mapper.Map<SkpView>(data);
                    if (view != null)
                    {
                        view.KdstatusNavigation = await _uow.StattrsRepo.Get(w => w.Kdstatus.Trim() == view.Kdstatus.Trim());
                    }
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
        [HttpDelete("{Idskp}")]
        public async Task<IActionResult> Delete(long Idskp)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                Skp data = await _uow.SkpRepo.ViewData(Idskp);
                if (data == null) return BadRequest("Data Tidak Ditemukan");
                List<Skpdet> skpdets = await _uow.SkpdetRepo.Gets(w => w.Idskp == Idskp);
                if (skpdets.Count() > 0) return BadRequest("Hapus Gagal, SKP Memiliki Rincian");
                _uow.SkpRepo.Remove(data);
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