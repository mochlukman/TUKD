using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Controllers.Akuntansi
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaftreklakController : ControllerBase
    {
        private readonly IUow _uow;
        public DaftreklakController(IUow uow)
        {
            _uow = uow;
        }
        [HttpGet]
        public async Task<IActionResult> Gets(
            [FromQuery]string Idjnslak
            )
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            List<int?> Ids = new List<int?>();
            if (!String.IsNullOrEmpty(Idjnslak)) // kondisi memungkinkan Idjnslak kosong / ambil Semua Data
            {
                string[] Idjnslaks = Idjnslak.Split(",");
                List<int> ListIds = Idjnslaks.Select(int.Parse).ToList();
                Ids.AddRange(ListIds.Cast<int?>().ToList());
            }
            try
            {
                List<Daftreklak> data = await _uow.DaftreklakRepo.ViewDatas(Ids);
                return Ok(data);
            }catch(Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
    }
}