using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Dto;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class SpjlpjRepo : Repo<Spjlpj>, ISpjlpjRepo
    {
        public SpjlpjRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<SpjlpjView> ViewData(long Idspjlpj)
        {
            SpjlpjView data = await (
                from spjlpj in _tukdContext.Spjlpj
                join spj in _tukdContext.Spj on spjlpj.Idspj equals spj.Idspj
                where spjlpj.Idspjlpj == Idspjlpj
                select new SpjlpjView
                {
                    Datecreate = spjlpj.Datecreate,
                    Dateupdate = spjlpj.Dateupdate,
                    Idlpj = spjlpj.Idlpj,
                    Idspj = spjlpj.Idspj,
                    Idspjlpj = spjlpj.Idspjlpj,
                    IdspjNavigation = spj ?? null
                }
                ).FirstOrDefaultAsync();
            if(data != null)
            {
                List<long> Idsbpk = _tukdContext.Bpkspj.Where(w => w.Idspj == data.Idspj).Select(s => s.Idbpk).Distinct().ToList();
                if (Idsbpk.Count() > 0)
                {
                    data.Nilai = _tukdContext.Bpkdetr.Where(w => Idsbpk.Contains(w.Idbpk)).Select(s => s.Nilai).Sum();
                }
            }
            return data;
        }

        public async Task<List<SpjlpjView>> ViewDatas(long Idlpj)
        {
            List<SpjlpjView> data = await(
                from spjlpj in _tukdContext.Spjlpj
                join spj in _tukdContext.Spj on spjlpj.Idspj equals spj.Idspj
                where spjlpj.Idlpj == Idlpj
                select new SpjlpjView
                {
                    Datecreate = spjlpj.Datecreate,
                    Dateupdate = spjlpj.Dateupdate,
                    Idlpj = spjlpj.Idlpj,
                    Idspj = spjlpj.Idspj,
                    Idspjlpj = spjlpj.Idspjlpj,
                    IdspjNavigation = spj ?? null
                }
                ).ToListAsync();
            if(data.Count() > 0)
            {
                data.ForEach(f =>
                {
                    List<long> Idsbpk = _tukdContext.Bpkspj.Where(w => w.Idspj == f.Idspj).Select(s => s.Idbpk).Distinct().ToList();
                    if(Idsbpk.Count() > 0)
                    {
                        f.Nilai = _tukdContext.Bpkdetr.Where(w => Idsbpk.Contains(w.Idbpk)).Select(s => s.Nilai).Sum();
                    }
                });
            }
            return data;
        }
    }
}
