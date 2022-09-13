using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TUKD.API.Dto;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Controllers.SPJ
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpjsppController : ControllerBase
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly DbConnection _dbConnection;
        private readonly TukdContext _tukdContext;
        public SpjsppController(IUow uow, IMapper mapper, DbConnection dbConnection, TukdContext tukdContext)
        {
            _mapper = mapper;
            _uow = uow;
            _dbConnection = dbConnection;
            _tukdContext = tukdContext;
        }
        [HttpGet("by-spp")]
        public async Task<IActionResult> GetBySpp(
            [FromQuery][Required]long Idspp,
            [FromQuery]string Kdstatus
            )
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (String.IsNullOrEmpty(Kdstatus)) Kdstatus = "42";
            try
            {
                List<SpjsppView> datas = await _uow.SpjsppRepo.GetBySpp(Idspp, Kdstatus);
                return Ok(datas);
            }catch(Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        [HttpPost("from-spp")]
        public async Task<IActionResult> PostFromSpp([FromBody][Required] Spjspp param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            param.Datecreate = DateTime.Now;
            var SpName = "WSP_TRANSFER_SPJSPP";
            try
            {
                using (var trans = await _tukdContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        Spjspp insert = await _uow.SpjsppRepo.Add(param);
                        bool success = false;
                        if(insert != null)
                        {
                            using (IDbConnection dbConnection = _dbConnection)
                            {
                                dbConnection.Open();
                                var parameters = new DynamicParameters();
                                parameters.Add("@IDSPP", param.Idspp);
                                parameters.Add("@IDSPJ", param.Idspj);
                                var rowTransfer = await dbConnection.ExecuteAsync(SpName, parameters, commandType: CommandType.StoredProcedure);
                                if (rowTransfer > 0)
                                {
                                    success = true;
                                }
                                else
                                {
                                    ModelState.AddModelError("error", "Input Gagal");
                                    success = false;
                                }
                                dbConnection.Close();
                            }
                        }
                        else
                        {
                            success = false;
                        }

                        if (success)
                        {
                            trans.Commit();
                            List<SpjsppView> datas = await _uow.SpjsppRepo.GetBySpp(param.Idspp, "42");
                            return Ok(datas);
                        }
                        else
                        {
                            trans.Rollback();
                            return BadRequest(ModelState);
                        }
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
        [HttpDelete("{Idsppspj}")]
        public async Task<IActionResult> Delete(long Idsppspj)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                Spjspp data = await _uow.SpjsppRepo.Get(w => w.Idsppspj == Idsppspj);
                if (data == null) return BadRequest("Data Tidak Tersedia");
                _uow.SpjsppRepo.Remove(data);
                if (await _uow.Complete())
                    return Ok();
                return BadRequest("Hapus Gagal");
            } catch (Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }

    }
}