using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class BkustsRepo : Repo<Bkusts>, IBkustsRepo
    {
        public BkustsRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;
        public async Task<List<Bkusts>> ViewDatas(long Idunit, long Idbend)
        {
            List<Bkusts> data = await (
                from src in _tukdContext.Bkusts
                join sts in _tukdContext.Sts on src.Idsts equals sts.Idsts
                where src.Idunit == Idunit && src.Idbend == Idbend
                select new Bkusts
                {
                    Datecreate = src.Datecreate,
                    Idbend = src.Idbend,
                    Idunit = src.Idunit,
                    Idbkusts = src.Idbkusts,
                    Dateupdate = src.Dateupdate,
                    Idsts = src.Idsts,
                    Idttd = src.Idttd,
                    IdstsNavigation = sts ?? null,
                    Tglbkuskpd = src.Tglbkuskpd,
                    Tglvalid = src.Tglvalid,
                    Uraian = src.Uraian,
                    Nobkuskpd = src.Nobkuskpd
                }
                ).ToListAsync();
            return data;
        }

        public async Task<List<Bkusts>> ViewDatasForSpjtr(long Idspjtr, long Idunit, long Idbend)
        {
            List<long> Idsbkusts = await _tukdContext.Bkustsspjtr.Where(w => w.Idspjtr == Idspjtr).Select(s => s.Idbkusts).ToListAsync();
            List<Bkusts> data = await (
                from src in _tukdContext.Bkusts
                join sts in _tukdContext.Sts on src.Idsts equals sts.Idsts
                where src.Idunit == Idunit && src.Idbend == Idbend && !Idsbkusts.Contains(src.Idbkusts)
                select new Bkusts
                {
                    Datecreate = src.Datecreate,
                    Idbend = src.Idbend,
                    Idunit = src.Idunit,
                    Idbkusts = src.Idbkusts,
                    Dateupdate = src.Dateupdate,
                    Idsts = src.Idsts,
                    Idttd = src.Idttd,
                    IdstsNavigation = sts ?? null,
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
