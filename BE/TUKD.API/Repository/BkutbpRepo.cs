using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class BkutbpRepo : Repo<Bkutbp>, IBkutbpRepo
    {
        public BkutbpRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<List<Bkutbp>> ViewDatas(long Idunit, long Idbend)
        {
            List<Bkutbp> data = await (
                from src in _tukdContext.Bkutbp
                join tbp in _tukdContext.Tbp on src.Idtbp equals tbp.Idtbp
                where src.Idunit == Idunit && src.Idbend == Idbend
                select new Bkutbp
                {
                    Datecreate = src.Datecreate,
                    Idbend = src.Idbend,
                    Idunit = src.Idunit,
                    Idbkutbp = src.Idbkutbp,
                    Dateupdate = src.Dateupdate,
                    Idtbp = src.Idtbp,
                    Idttd = src.Idttd,
                    IdtbpNavigation = tbp ?? null,
                    Tglbkuskpd = src.Tglbkuskpd,
                    Tglvalid = src.Tglvalid,
                    Uraian = src.Uraian,
                    Nobkuskpd = src.Nobkuskpd
                }
                ).ToListAsync();
            return data;
        }

        public async Task<List<Bkutbp>> ViewDatasForSpjtr(long Idspjtr, long Idunit, long Idbend)
        {
            List<long> Idsbkutbp = await _tukdContext.Bkutbpspjtr.Where(w => w.Idspjtr == Idspjtr).Select(s => s.Idbkutbp).ToListAsync();
            List<Bkutbp> data = await (
                from src in _tukdContext.Bkutbp
                join tbp in _tukdContext.Tbp on src.Idtbp equals tbp.Idtbp
                where src.Idunit == Idunit && src.Idbend == Idbend && !Idsbkutbp.Contains(src.Idbkutbp)
                select new Bkutbp
                {
                    Datecreate = src.Datecreate,
                    Idbend = src.Idbend,
                    Idunit = src.Idunit,
                    Idbkutbp = src.Idbkutbp,
                    Dateupdate = src.Dateupdate,
                    Idtbp = src.Idtbp,
                    Idttd = src.Idttd,
                    IdtbpNavigation = tbp ?? null,
                    Tglbkuskpd = src.Tglbkuskpd,
                    Tglvalid = src.Tglvalid,
                    Uraian = src.Uraian,
                    Nobkuskpd = src.Nobkuskpd
                }
                ).ToListAsync();
            return data;
        }
    }
}
