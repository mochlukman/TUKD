﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class BkpajakRepo : Repo<Bkpajak>, IBkpajakRepo
    {
        public BkpajakRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Bkpajak param)
        {
            Bkpajak data = await _tukdContext.Bkpajak.Where(w => w.Idbkpajak == param.Idbkpajak).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Nobkpajak = param.Nobkpajak;
            data.Tglbkpajak = param.Tglbkpajak;
            data.Uraian = param.Uraian;
            data.Tglvalid = param.Tglvalid;
            data.Dateupdate = param.Dateupdate;
            _tukdContext.Bkpajak.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0) return true;
            return false;
        }

        public async Task<Bkpajak> ViewData(long Idbkpajak)
        {
            Bkpajak data = await (
                from bkpajak in _tukdContext.Bkpajak
                join unit in _tukdContext.Daftunit on bkpajak.Idunit equals unit.Idunit
                join bend in _tukdContext.Bend on bkpajak.Idbend equals bend.Idbend
                join kode in _tukdContext.Zkode on bkpajak.Idttd equals kode.Idxkode
                join status in _tukdContext.Stattrs on bkpajak.Kdstatus.Trim() equals status.Kdstatus.Trim()
                join cair in _tukdContext.Jcair on bkpajak.Stcair equals cair.Stcair
                join kirim in _tukdContext.Jkirim on bkpajak.Stkirim equals kirim.Stkirim
                where bkpajak.Idbkpajak == Idbkpajak
                select new Bkpajak
                {
                    Idunit = bkpajak.Idunit,
                    Idbend = bkpajak.Idbend,
                    Idbkpajak = bkpajak.Idbkpajak,
                    Idttd = bkpajak.Idttd,
                    Datecreate = bkpajak.Datecreate,
                    Dateupdate = bkpajak.Dateupdate,
                    Kdrilis = bkpajak.Kdrilis,
                    Kdstatus = bkpajak.Kdstatus,
                    Stcair = bkpajak.Stcair,
                    Stkirim = bkpajak.Stkirim,
                    Tglbkpajak = bkpajak.Tglbkpajak,
                    Tglvalid = bkpajak.Tglvalid,
                    Nobkpajak = bkpajak.Nobkpajak,
                    Uraian = bkpajak.Uraian,
                    Idtransfer = bkpajak.Idtransfer,
                    IdbendNavigation = bend ?? null,
                    IdttdNavigation = kode ?? null,
                    IdunitNavigation = unit ?? null,
                    KdstatusNavigation = status ?? null,
                    StcairNavigation = cair ?? null,
                    StkirimNavigation = kirim ?? null
                }
                ).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<Bkpajak>> ViewDatas(long Idunit, long Idbend, string Kdstatus)
        {
            List<Bkpajak> data = await (
                from bkpajak in _tukdContext.Bkpajak
                join unit in _tukdContext.Daftunit on bkpajak.Idunit equals unit.Idunit
                join bend in _tukdContext.Bend on bkpajak.Idbend equals bend.Idbend
                join kode in _tukdContext.Zkode on bkpajak.Idttd equals kode.Idxkode
                join status in _tukdContext.Stattrs on bkpajak.Kdstatus.Trim() equals status.Kdstatus.Trim()
                join cair in _tukdContext.Jcair on bkpajak.Stcair equals cair.Stcair
                join kirim in _tukdContext.Jkirim on bkpajak.Stkirim equals kirim.Stkirim
                where bkpajak.Idunit == Idunit && bkpajak.Idbend == Idbend && bkpajak.Kdstatus.Trim() == Kdstatus.Trim()
                select new Bkpajak
                {
                    Idunit = bkpajak.Idunit,
                    Idbend = bkpajak.Idbend,
                    Idbkpajak = bkpajak.Idbkpajak,
                    Idttd = bkpajak.Idttd,
                    Datecreate = bkpajak.Datecreate,
                    Dateupdate = bkpajak.Dateupdate,
                    Kdrilis = bkpajak.Kdrilis,
                    Kdstatus = bkpajak.Kdstatus,
                    Stcair = bkpajak.Stcair,
                    Stkirim = bkpajak.Stkirim,
                    Tglbkpajak = bkpajak.Tglbkpajak,
                    Tglvalid = bkpajak.Tglvalid,
                    Nobkpajak = bkpajak.Nobkpajak,
                    Uraian = bkpajak.Uraian,
                    Idtransfer = bkpajak.Idtransfer,
                    IdbendNavigation = bend ?? null,
                    IdttdNavigation = kode ?? null,
                    IdunitNavigation = unit ?? null,
                    KdstatusNavigation = status ?? null,
                    StcairNavigation = cair ?? null,
                    StkirimNavigation = kirim ?? null
                }
                ).ToListAsync();
            return data;
        }
    }
}
