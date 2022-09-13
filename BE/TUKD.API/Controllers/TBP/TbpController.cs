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

namespace TUKD.API.Controllers.TBP
{
    [Route("api/[controller]")]
    [ApiController]
    public class TbpController : ControllerBase
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public TbpController(IUow uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Gets([FromQuery]TbpGet param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                if(param.Kdstatus == "x")
                {
                    List<Tbp> datas = await _uow.TbpRepo.ViewDatas(param.Idunit, param.Idxkode);
                    return Ok(datas);
                } else
                {
                    List<string> status = param.Kdstatus.Split(",").ToList();
                    List<Tbp> datas = await _uow.TbpRepo.ViewDatas(param.Idunit, status, param.Idxkode, param.Idbend, param.Isvalid);
                    return Ok(datas);
                }
                
            }
            catch(Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpGet("{Idtbp}")]
        public async Task<IActionResult> Get(long Idtbp)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                Tbp data = await _uow.TbpRepo.ViewData(Idtbp);
                return Ok(data);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpGet("For-Bku")]
        public async Task<IActionResult> GetForBku([FromQuery] BkuParamRef param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                List<Bkutbp> bkutbps = await _uow.BkutbpRepo.Gets(w => w.Idunit == param.Idunit && w.Idbend == param.Idbend);
                List<long> Idtbps = new List<long>();
                if (bkutbps.Count() > 0)
                {
                    bkutbps.ForEach(f =>
                    {
                        Idtbps.Add(f.Idtbp);
                    });
                }
                List<Tbp> datas = await _uow.TbpRepo.Gets(w => w.Idunit == param.Idunit && w.Idbend1 == param.Idbend && !Idtbps.Contains(w.Idtbp));
                List<Tbp> views = new List<Tbp>();
                if (datas.Count() > 0)
                {
                    foreach (var d in datas)
                    {
                        Tbp data = await _uow.TbpRepo.ViewData(d.Idtbp);
                        if (data != null)
                        {
                            views.Add(data);
                        }

                    }
                }
                return Ok(datas);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TbpPost param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Tbp post = _mapper.Map<Tbp>(param);
            string[] splitNo = param.Notbp.Split("/");
            if (splitNo[0].ToLower().Contains("x")) return BadRequest("Harap Pengisian Nomor Disesuaikan!, Ex.(00001)");
            bool checkNo = await _uow.TbpRepo.isExist(w => w.Notbp.Trim() == post.Notbp.Trim() && w.Kdstatus.Trim() == post.Kdstatus.Trim() && w.Idxkode == post.Idxkode && w.Idbend1 == post.Idbend1);
            if (checkNo) return BadRequest("Nomor Sudah Digunakan");
            post.Datecreate = DateTime.Now;
            try
            {
                Tbp insert = await _uow.TbpRepo.Add(post);
                if (insert != null) {
                    if(param.Idskp.ToString() != "0")
                    {
                        bool isExist = await _uow.SkptbpRepo.isExist(w => w.Idskp == param.Idskp && w.Idtbp == insert.Idtbp);
                        if (!isExist)
                        {
                            Skptbp skptbp = new Skptbp
                            {
                                Idskp = param.Idskp,
                                Idtbp = insert.Idtbp
                            };
                            await _uow.SkptbpRepo.Add(skptbp);
                            await _uow.Complete();
                        }
                    }
                    return Ok(await _uow.TbpRepo.ViewData(insert.Idtbp));
                }
                return BadRequest("Gagal Input");
            }catch(Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]TbpPost param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Tbp post = _mapper.Map<Tbp>(param);
            post.Datecreate = DateTime.Now;
            Tbp Old = await _uow.TbpRepo.Get(w => w.Notbp.Trim() == post.Notbp.Trim() && w.Kdstatus.Trim() == post.Kdstatus.Trim() && w.Idxkode == post.Idxkode && w.Idbend1 == post.Idbend1);
            if(Old != null)
            {
                if (Old.Idtbp != post.Idtbp) return BadRequest("Nomor Sudah Digunakan");
            }
            try
            {
                bool update = await _uow.TbpRepo.Update(post);
                if (update)
                    return Ok(await _uow.TbpRepo.ViewData(post.Idtbp));
                return BadRequest("Gagal Input");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpDelete("{Idtbp}")]
        public async Task<IActionResult> Delete(long Idtbp)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                Tbp data = await _uow.TbpRepo.Get(w => w.Idtbp == Idtbp);
                if (data == null) return BadRequest("Dat Tidak Tersedia");
                long dett = await _uow.TbpdettRepo.Count(w => w.Idtbp == data.Idtbp);
                if (dett > 0) return BadRequest("Gagal Hapus, Data Telah digunakan");
                long detd = await _uow.TbpdetdRepo.Count(w => w.Idtbp == data.Idtbp);
                if (detd > 0) return BadRequest("Gagal Hapus, Data Telah digunakan");
                _uow.TbpRepo.Remove(data);
                if (await _uow.Complete()) return Ok();
                return BadRequest("Gagal Hapus");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
    }
}