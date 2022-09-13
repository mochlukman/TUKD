using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Repository
{
    public class PanjarRepo : Repo<Panjar>, IPanjarRepo
    {
        public PanjarRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<List<long>> GetIds(long Idpanjar)
        {
            List<long> Ids = await _tukdContext.Panjar
                .Where(w => w.Idpanjar == Idpanjar)
                .Select(s => s.Idpanjar).ToListAsync();
            return Ids;
        }
        public async Task<bool> Update(Panjar param)
        {
            Panjar data = await _tukdContext.Panjar.Where(w => w.Idpanjar == param.Idpanjar).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Uraian = param.Uraian;
            data.Tglpanjar = param.Tglpanjar;
            data.Tglvalid = param.Tglvalid;
            data.Kdstatus = param.Kdstatus;
            data.Dateupdate = param.Dateupdate;
            data.Nopanjar = param.Nopanjar;
            data.Stbank = param.Stbank;
            data.Sttunai = param.Sttunai;
            _tukdContext.Panjar.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0) return true;
            return false;
        }

        public async Task<List<Panjar>> ViewDatas(PanjarGet param)
        {
            List<Panjar> Result = new List<Panjar>();
            IQueryable<Panjar> query = (
                from data in _tukdContext.Panjar
                join bendahara in _tukdContext.Bend on data.Idbend equals bendahara.Idbend
                join status in _tukdContext.Stattrs on data.Kdstatus.Trim() equals status.Kdstatus.Trim()
                join kode in _tukdContext.Zkode on data.Idxkode equals kode.Idxkode
                join bku in _tukdContext.Bkupanjar on data.Idpanjar equals bku.Idpanjar into bkuMacth from bku_data in bkuMacth.DefaultIfEmpty()
                select new Panjar
                {
                    Idbend = data.Idbend,
                    Idxkode = data.Idxkode,
                    Datecreate = data.Datecreate,
                    Dateupdate = data.Dateupdate,
                    Idpanjar = data.Idpanjar,
                    Idpeg = data.Idpeg,
                    Idunit = data.Idunit,
                    Nopanjar = data.Nopanjar,
                    Stbank = data.Stbank,
                    Kdstatus  = data.Kdstatus,
                    Sttunai = data.Sttunai,
                    Tglvalid = data.Tglvalid,
                    Tglpanjar = data.Tglpanjar,
                    Uraian = data.Uraian,
                    IdbendNavigation = bendahara ?? null,
                    KdstatusNavigation = status ?? null,
                    IdxkodeNavigation = kode ?? null,
                    Bkupanjar = bkuMacth.ToList() ?? null
                }
                ).AsQueryable();
            if(param.Idunit.ToString() != "0")
            {
                query = query.Where(w => w.Idunit == param.Idunit).AsQueryable();
            }
            if(param.Idbend.ToString() != "0")
            {
                query = query.Where(w => w.Idbend == param.Idbend).AsQueryable();
            }
            if(param.Idxkode.ToString() != "0")
            {
                query = query.Where(w => w.Idxkode == param.Idxkode).AsQueryable();
            }
            if(param.Kdstatus != "x")
            {
                List<string> kdstatus = param.Kdstatus.Split(",").ToList();
                if(kdstatus.Count() > 0)
                {
                    query = query.Where(w => kdstatus.Contains(w.Kdstatus.Trim())).AsQueryable();
                }
            }
            Result = await query.ToListAsync();
            return Result;
        }

        public async Task<Panjar> ViewData(long Idpanjar)
        {
            Panjar Result = await (
                from data in _tukdContext.Panjar
                join bendahara in _tukdContext.Bend on data.Idbend equals bendahara.Idbend
                join status in _tukdContext.Stattrs on data.Kdstatus.Trim() equals status.Kdstatus.Trim()
                join kode in _tukdContext.Zkode on data.Idxkode equals kode.Idxkode
                join bku in _tukdContext.Bkupanjar on data.Idpanjar equals bku.Idpanjar into bkuMacth
                from bku_data in bkuMacth.DefaultIfEmpty()
                where data.Idpanjar == Idpanjar
                select new Panjar
                {
                    Idbend = data.Idbend,
                    Idxkode = data.Idxkode,
                    Datecreate = data.Datecreate,
                    Dateupdate = data.Dateupdate,
                    Idpanjar = data.Idpanjar,
                    Idpeg = data.Idpeg,
                    Idunit = data.Idunit,
                    Nopanjar = data.Nopanjar,
                    Stbank = data.Stbank,
                    Kdstatus = data.Kdstatus,
                    Sttunai = data.Sttunai,
                    Tglvalid = data.Tglvalid,
                    Tglpanjar = data.Tglpanjar,
                    Uraian = data.Uraian,
                    IdbendNavigation = bendahara ?? null,
                    KdstatusNavigation = status ?? null,
                    IdxkodeNavigation = kode ?? null,
                    Bkupanjar = bkuMacth.ToList() ?? null
                }
                ).FirstOrDefaultAsync();
            return Result;
        }
    }
}
