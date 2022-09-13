﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JrekController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        public JrekController(IMapper mapper, IUow uow)
        {
            _mapper = mapper;
            _uow = uow;
        }
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                List<Jrek> data = await _uow.JrekRepo.Gets();
                return Ok(data);
            }
            catch(Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
    }
}