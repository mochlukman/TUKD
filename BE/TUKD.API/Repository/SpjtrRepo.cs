﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Dto;
using TUKD.API.Interface;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Repository
{
    public class SpjtrRepo : Repo<Spjtr>, ISpjtrRepo
    {
        public SpjtrRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<PrimengTableResult<Spjtr>> Paging(PrimengTableParam<SpjGetsParam> param)
        {
            PrimengTableResult<Spjtr> Result = new PrimengTableResult<Spjtr>();
            IQueryable<Spjtr> query = (
                from spj in _tukdContext.Spjtr
                join unit in _tukdContext.Daftunit on spj.Idunit equals unit.Idunit
                join bend in _tukdContext.Bend on spj.Idbend equals bend.Idbend
                select new Spjtr
                {
                    Idspjtr = spj.Idspjtr,
                    Idbend = spj.Idbend,
                    Idttd = spj.Idttd,
                    Idunit = spj.Idunit,
                    Idxkode = spj.Idxkode,
                    Nosah = spj.Nosah,
                    Kdstatus = spj.Kdstatus,
                    Nospj = spj.Nospj,
                    Tglspj = spj.Tglspj,
                    Tglsah = spj.Tglsah,
                    Tglbuku = spj.Tglbuku,
                    Tglvalid = spj.Tglvalid,
                    Keterangan = spj.Keterangan,
                    Datecreate = spj.Datecreate,
                    Dateupdate = spj.Dateupdate
                }
                ).AsQueryable();
            if (param.Parameters != null)
            {
                query = query.Where(w => w.Idunit == param.Parameters.Idunit && w.Idxkode == 1).AsQueryable();
                if (!String.IsNullOrEmpty(param.Parameters.Kdstatus))
                {
                    List<string> split_status = param.Parameters.Kdstatus.Split(",").ToList();
                    query = query.Where(w => split_status.Contains(w.Kdstatus.Trim())).AsQueryable();
                }
                if (!String.IsNullOrEmpty(param.Parameters.Idbend.ToString()) || param.Parameters.Idbend.ToString() != "0")
                {
                    query = query.Where(w => w.Idbend == param.Parameters.Idbend).AsQueryable();
                }
            }
            if (!String.IsNullOrEmpty(param.GlobalFilter))
            {
                query = query.Where(w =>
                    EF.Functions.Like(w.Nospj.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Nosah.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Keterangan.Trim(), "%" + param.GlobalFilter + "%")
                ).AsQueryable();
            }
            if (!String.IsNullOrEmpty(param.SortField))
            {
                if (param.SortField == "nospj")
                {
                    if (param.SortOrder > 0)
                    {
                        query = query.OrderBy(o => o.Nospj).AsQueryable();
                    }
                    else
                    {
                        query = query.OrderByDescending(o => o.Nospj).AsQueryable();
                    }
                }
                else if (param.SortField == "tglspj")
                {
                    if (param.SortOrder > 0)
                    {
                        query = query.OrderBy(o => o.Tglspj).AsQueryable();
                    }
                    else
                    {
                        query = query.OrderByDescending(o => o.Tglspj).AsQueryable();
                    }
                }
                else if (param.SortField == "nosah")
                {
                    if (param.SortOrder > 0)
                    {
                        query = query.OrderBy(o => o.Nosah).AsQueryable();
                    }
                    else
                    {
                        query = query.OrderByDescending(o => o.Nosah).AsQueryable();
                    }
                }
                else if (param.SortField == "tglbuku")
                {
                    if (param.SortOrder > 0)
                    {
                        query = query.OrderBy(o => o.Tglbuku).AsQueryable();
                    }
                    else
                    {
                        query = query.OrderByDescending(o => o.Tglbuku).AsQueryable();
                    }
                }
                else if (param.SortField == "keterangan")
                {
                    if (param.SortOrder > 0)
                    {
                        query = query.OrderBy(o => o.Keterangan).AsQueryable();
                    }
                    else
                    {
                        query = query.OrderByDescending(o => o.Keterangan).AsQueryable();
                    }
                }
                else if (param.SortField == "tglsah")
                {
                    if (param.SortOrder > 0)
                    {
                        query = query.OrderBy(o => o.Tglsah).AsQueryable();
                    }
                    else
                    {
                        query = query.OrderByDescending(o => o.Tglsah).AsQueryable();
                    }
                }
            }
            Result.Data = await query.Skip(param.Start).Take(param.Rows).ToListAsync();
            Result.Totalrecords = await query.CountAsync();
            return Result;
        }

        public async Task<bool> Update(Spjtr param)
        {
            Spjtr data = await _tukdContext.Spjtr.Where(w => w.Idspjtr == param.Idspjtr).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Nospj = param.Nospj;
            data.Tglspj = param.Tglspj;
            data.Tglbuku = param.Tglbuku;
            data.Keterangan = param.Keterangan;
            data.Dateupdate = param.Dateupdate;
            _tukdContext.Spjtr.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<Spjtr> ViewData(long Idspjtr)
        {
            Spjtr data = await (
                from spj in _tukdContext.Spjtr
                join unit in _tukdContext.Daftunit on spj.Idunit equals unit.Idunit
                join bend in _tukdContext.Bend on spj.Idbend equals bend.Idbend
                where spj.Idspjtr == Idspjtr
                select new Spjtr
                {
                    Idspjtr = spj.Idspjtr,
                    Idbend = spj.Idbend,
                    Idttd = spj.Idttd,
                    Idunit = spj.Idunit,
                    Idxkode = spj.Idxkode,
                    Nosah = spj.Nosah,
                    Kdstatus = spj.Kdstatus,
                    Nospj = spj.Nospj,
                    Tglspj = spj.Tglspj,
                    Tglsah = spj.Tglsah,
                    Tglbuku = spj.Tglbuku,
                    Tglvalid = spj.Tglvalid,
                    Keterangan = spj.Keterangan,
                    Datecreate = spj.Datecreate,
                    Dateupdate = spj.Dateupdate
                }
                ).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<Spjtr>> ViewDatas(SpjGetsParam param)
        {
            IQueryable<Spjtr> query = (
                from spj in _tukdContext.Spjtr
                join unit in _tukdContext.Daftunit on spj.Idunit equals unit.Idunit
                join bend in _tukdContext.Bend on spj.Idbend equals bend.Idbend
                select new Spjtr
                {
                    Idspjtr = spj.Idspjtr,
                    Idbend = spj.Idbend,
                    Idttd = spj.Idttd,
                    Idunit = spj.Idunit,
                    Idxkode = spj.Idxkode,
                    Nosah = spj.Nosah,
                    Kdstatus = spj.Kdstatus,
                    Nospj = spj.Nospj,
                    Tglspj = spj.Tglspj,
                    Tglsah = spj.Tglsah,
                    Tglbuku = spj.Tglbuku,
                    Tglvalid = spj.Tglvalid,
                    Keterangan = spj.Keterangan,
                    Datecreate = spj.Datecreate,
                    Dateupdate = spj.Dateupdate
                }
                ).AsQueryable();
            query = query.Where(w => w.Idunit == param.Idunit && w.Idxkode == 1).AsQueryable();
            if (!String.IsNullOrEmpty(param.Kdstatus))
            {
                List<string> split_status = param.Kdstatus.Split(",").ToList();
                query = query.Where(w => split_status.Contains(w.Kdstatus.Trim())).AsQueryable();
            }
            List<Spjtr> datas = await query.ToListAsync();
            return datas;
        }
    }
}
