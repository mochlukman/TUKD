using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class BkpajakdetstrRepo : Repo<Bkpajakdetstr>, IBkpajakdetstrRepo
    {
        public BkpajakdetstrRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Bkpajakdetstr param)
        {
            Bkpajakdetstr data = await _tukdContext.Bkpajakdetstr.Where(w => w.Idbkpajakdetstr == param.Idbkpajakdetstr).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Idbilling = param.Idbilling;
            data.Tglidbilling = param.Tglidbilling;
            data.Tglexpire = param.Tglexpire;
            data.Ntpn = param.Ntpn;
            data.Ntb = param.Ntb;
            data.Dateupdate = param.Dateupdate;
            _tukdContext.Bkpajakdetstr.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0) return true;
            return false;
        }

        public async Task<Bkpajakdetstr> ViewData(long Idbkpajakdetstr)
        {
            Bkpajakdetstr data = await (
                from det in _tukdContext.Bkpajakdetstr
                join bkpajak in _tukdContext.Bkpajak on det.Idbkpajak equals bkpajak.Idbkpajak
                join pajak in _tukdContext.Pajak on det.Idpajak equals pajak.Idpajak
                //join bpk in _tukdContext.Bpkpajakstr on det.Idbpkpajakstr equals bpk.Idbpkpajakstr
                where det.Idbkpajakdetstr == Idbkpajakdetstr
                select new Bkpajakdetstr
                {
                    Idbkpajak = det.Idbkpajak,
                    Idbkpajakdetstr = det.Idbkpajakdetstr,
                    Idpajak = det.Idpajak,
                    Idbpkpajakstr = det.Idbpkpajakstr,
                    Datecreate = det.Datecreate,
                    Dateupdate = det.Dateupdate,
                    Idbilling = det.Idbilling,
                    Tglexpire = det.Tglexpire,
                    Tglidbilling = det.Tglidbilling,
                    Ntb = det.Ntb,
                    Ntpn = det.Ntpn,
                    IdbkpajakNavigation = bkpajak ?? null,
                    //IdbpkpajakstrNavigation = bpk ?? null,
                    IdpajakNavigation = pajak ?? null
                }
            ).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<Bkpajakdetstr>> ViewDatas(long Idbkpajak)
        {
            List<Bkpajakdetstr> data = await (
                from det in _tukdContext.Bkpajakdetstr
                join bkpajak in _tukdContext.Bkpajak on det.Idbkpajak equals bkpajak.Idbkpajak
                join pajak in _tukdContext.Pajak on det.Idpajak equals pajak.Idpajak
                //join bpk in _tukdContext.Bpkpajakstr on det.Idbpkpajakstr equals bpk.Idbpkpajakstr
                where det.Idbkpajak == Idbkpajak
                select new Bkpajakdetstr
                {
                    Idbkpajak = det.Idbkpajak,
                    Idbkpajakdetstr = det.Idbkpajakdetstr,
                    Idpajak = det.Idpajak,
                    Idbpkpajakstr = det.Idbpkpajakstr,
                    Datecreate = det.Datecreate,
                    Dateupdate = det.Dateupdate,
                    Idbilling = det.Idbilling,
                    Tglexpire = det.Tglexpire,
                    Tglidbilling = det.Tglidbilling,
                    Ntb = det.Ntb,
                    Ntpn = det.Ntpn,
                    IdbkpajakNavigation = bkpajak ?? null,
                    //IdbpkpajakstrNavigation = bpk ?? null,
                    IdpajakNavigation = pajak ?? null
                }
            ).ToListAsync();
            return data;
        }
    }
}
