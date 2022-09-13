using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class TbpRepo : Repo<Tbp>, ITbpRepo
    {
        public TbpRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<List<long>> GetIds(long Idtbp)
        {
            List<long> Ids = await _tukdContext.Tbp
                .Where(w => w.Idtbp == Idtbp)
                .Select(s => s.Idtbp).ToListAsync();
            return Ids;
        }

        public async Task<bool> Update(Tbp param)
        {
            Tbp data = await _tukdContext.Tbp.Where(w => w.Idtbp == param.Idtbp).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Notbp = param.Notbp;
            data.Tgltbp = param.Tgltbp;
            data.Tglvalid = param.Tglvalid;
            data.Penyetor = param.Penyetor;
            data.Alamat = param.Alamat;
            data.Uraitbp = param.Uraitbp;
            data.Dateupdate = param.Dateupdate;
            _tukdContext.Tbp.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0) return true;
            return false;
        }

        public async Task<Tbp> ViewData(long Idtbp)
        {
           Tbp data = await (
                from tbp in _tukdContext.Tbp
                join status in _tukdContext.Stattrs on tbp.Kdstatus.Trim() equals status.Kdstatus.Trim()
                where tbp.Idtbp == Idtbp
                select new Tbp
                {
                    Idtbp = tbp.Idtbp,
                    Idunit = tbp.Idunit,
                    Notbp = tbp.Notbp,
                    Idbend1 = tbp.Idbend1,
                    Kdstatus = tbp.Kdstatus,
                    Idbend2 = tbp.Idbend2,
                    Idxkode = tbp.Idxkode,
                    Tgltbp = tbp.Tgltbp,
                    Penyetor = tbp.Penyetor,
                    Alamat = tbp.Alamat,
                    Uraitbp = tbp.Uraitbp,
                    Tglvalid = tbp.Tglvalid,
                    Datecreate = tbp.Datecreate,
                    Dateupdate = tbp.Dateupdate,
                    KdstatusNavigation = status ?? null
                }
                ).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<Tbp>> ViewDatas(long Idunit, List<string> Kdstatus, int Idxkode, long? Idbend, bool Isvalid)
        {
            if (Isvalid)
            {
                List<Tbp> datas = await (
                from tbp in _tukdContext.Tbp
                join status in _tukdContext.Stattrs on tbp.Kdstatus.Trim() equals status.Kdstatus.Trim()
                where tbp.Idunit == Idunit && Kdstatus.Contains(tbp.Kdstatus.Trim()) && tbp.Idxkode == Idxkode && tbp.Idbend1 == Idbend && !String.IsNullOrEmpty(tbp.Tglvalid.ToString())
                select new Tbp
                {
                    Idtbp = tbp.Idtbp,
                    Idunit = tbp.Idunit,
                    Notbp = tbp.Notbp,
                    Idbend1 = tbp.Idbend1,
                    Kdstatus = tbp.Kdstatus,
                    Idbend2 = tbp.Idbend2,
                    Idxkode = tbp.Idxkode,
                    Tgltbp = tbp.Tgltbp,
                    Penyetor = tbp.Penyetor,
                    Alamat = tbp.Alamat,
                    Uraitbp = tbp.Uraitbp,
                    Tglvalid = tbp.Tglvalid,
                    Datecreate = tbp.Datecreate,
                    Dateupdate = tbp.Dateupdate,
                    KdstatusNavigation = status ?? null
                }
                ).ToListAsync();
                return datas;
            } else
            {
                List<Tbp> datas = await (
                from tbp in _tukdContext.Tbp
                join status in _tukdContext.Stattrs on tbp.Kdstatus.Trim() equals status.Kdstatus.Trim()
                where tbp.Idunit == Idunit && Kdstatus.Contains(tbp.Kdstatus.Trim()) && tbp.Idxkode == Idxkode && tbp.Idbend1 == Idbend
                select new Tbp
                {
                    Idtbp = tbp.Idtbp,
                    Idunit = tbp.Idunit,
                    Notbp = tbp.Notbp,
                    Idbend1 = tbp.Idbend1,
                    Kdstatus = tbp.Kdstatus,
                    Idbend2 = tbp.Idbend2,
                    Idxkode = tbp.Idxkode,
                    Tgltbp = tbp.Tgltbp,
                    Penyetor = tbp.Penyetor,
                    Alamat = tbp.Alamat,
                    Uraitbp = tbp.Uraitbp,
                    Tglvalid = tbp.Tglvalid,
                    Datecreate = tbp.Datecreate,
                    Dateupdate = tbp.Dateupdate,
                    KdstatusNavigation = status ?? null
                }
                ).ToListAsync();
                return datas;
            }
            
        }

        public async Task<List<Tbp>> ViewDatas(long Idunit, int Idxkode)
        {
            List<Tbp> datas = await (
                from tbp in _tukdContext.Tbp
                join status in _tukdContext.Stattrs on tbp.Kdstatus.Trim() equals status.Kdstatus.Trim()
                where tbp.Idunit == Idunit && tbp.Idxkode == Idxkode
                select new Tbp
                {
                    Idtbp = tbp.Idtbp,
                    Idunit = tbp.Idunit,
                    Notbp = tbp.Notbp,
                    Idbend1 = tbp.Idbend1,
                    Kdstatus = tbp.Kdstatus,
                    Idbend2 = tbp.Idbend2,
                    Idxkode = tbp.Idxkode,
                    Tgltbp = tbp.Tgltbp,
                    Penyetor = tbp.Penyetor,
                    Alamat = tbp.Alamat,
                    Uraitbp = tbp.Uraitbp,
                    Tglvalid = tbp.Tglvalid,
                    Datecreate = tbp.Datecreate,
                    Dateupdate = tbp.Dateupdate,
                    KdstatusNavigation = status ?? null
                }
                ).ToListAsync();
            return datas;
        }
    }
}