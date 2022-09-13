using System;
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
    public class RkadetrRepo : Repo<Rkadetr>, IRkadetrRepo
    {
        public RkadetrRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;
        public void GetLastChild(long Idrkadetr)
        {
            List<Rkadetr> child = _tukdContext.Rkadetr.Where(w => w.Idrkadetrduk == Idrkadetr).ToList();
            if (child.Count() > 0)
            {
                for (var i = 0; i < child.Count(); i++)
                {
                    GetLastChild(child[i].Idrkadetr);
                }
            }
            else
            {
                Rkadetr data = _tukdContext.Rkadetr.Where(w => w.Idrkadetr == Idrkadetr).FirstOrDefault();
                if (data != null)
                {
                    CalculateSubTotal(data.Idrkadetrduk);
                }
            }
        }
        public void CalculateSubTotal(long? Idrkadetrduk)
        {
            Rkadetr parent = _tukdContext.Rkadetr.Where(w => w.Idrkadetr == Idrkadetrduk).FirstOrDefault();
            if (parent != null)
            {
                decimal? subTotalChild = _tukdContext.Rkadetr.Where(w => w.Idrkadetrduk == parent.Idrkadetr).Sum(s => s.Subtotal);
                parent.Subtotal = subTotalChild;
                _tukdContext.Rkadetr.Update(parent);
                if (_tukdContext.SaveChanges() > 0)
                {
                    CalculateSubTotal(parent.Idrkadetrduk);
                }
            }
        }
        public async Task<PrimengTableResult<Rkadetr>> Paging(PrimengTableParam<RkadetrGet> param)
        {
            PrimengTableResult<Rkadetr> Result = new PrimengTableResult<Rkadetr>();
            IQueryable<Rkadetr> Query = (
                 from data in _tukdContext.Rkadetr
                 join rka in _tukdContext.Rkar on data.Idrkar equals rka.Idrkar
                 select new Rkadetr
                 {
                     Createdby = data.Createdby,
                     Createddate = data.Createddate,
                     Ekspresi = data.Ekspresi,
                     Idrkar = data.Idrkar,
                     Idrkadetr = data.Idrkadetr,
                     Idrkadetrduk = data.Idrkadetrduk,
                     IdrkarNavigation = rka ?? null,
                     Idsatuan = data.Idsatuan,
                     Idstdharga = data.Idstdharga,
                     Inclsubtotal = data.Inclsubtotal,
                     Jumbyek = data.Jumbyek,
                     Kdjabar = data.Kdjabar,
                     Satuan = data.Satuan,
                     Subtotal = data.Subtotal,
                     Tarif = data.Tarif,
                     Type = data.Type,
                     Updateby = data.Updateby,
                     Updatetime = data.Updatetime,
                     Uraian = data.Uraian
                 }
                ).AsQueryable();
            if (param.Parameters.Idrkar.ToString() != "0")
            {
                Query = Query.Where(w => w.Idrkar == param.Parameters.Idrkar).AsQueryable();
            }
            if (!String.IsNullOrEmpty(param.GlobalFilter))
            {
                Query = Query.Where(w => EF.Functions.Like(w.Kdjabar.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Uraian.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Ekspresi.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Jumbyek.ToString(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Satuan.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Tarif.ToString(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Type.Trim(), "%" + param.GlobalFilter + "%")
                ).AsQueryable();
            }
            Result.Data = await Query.Skip(param.Start).Take(param.Rows).OrderBy(o => o.Kdjabar.Trim()).ToListAsync();
            Result.Totalrecords = await Query.CountAsync();
            if (Result.Data.Count() > 0)
            {
                Result.OptionalResult = new PrimengTableResultOptional
                {
                    TotalNilai = Query.Where(w => w.Type.Trim() == "D").Sum(s => s.Subtotal)
                };
            }
            return Result;
        }

        public async Task<bool> Update(Rkadetr param)
        {
            Rkadetr data = await _tukdContext.Rkadetr.Where(w => w.Idrkadetr == param.Idrkadetr).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Kdjabar = param.Kdjabar;
            data.Uraian = param.Uraian;
            data.Ekspresi = param.Ekspresi;
            data.Satuan = param.Satuan;
            data.Tarif = param.Tarif;
            data.Subtotal = param.Subtotal;
            data.Jumbyek = param.Jumbyek;
            data.Idstdharga = param.Idstdharga;
            _tukdContext.Rkadetr.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public void UpdateToHeader(long Idrkadetr)
        {
            Rkadetr data = _tukdContext.Rkadetr.Where(w => w.Idrkadetr == Idrkadetr).FirstOrDefault();
            decimal? totalChild = _tukdContext.Rkadetr.Where(w => w.Idrkadetrduk == Idrkadetr).Sum(s => s.Subtotal);
            if (data != null)
            {
                data.Type = "H";
                data.Subtotal = totalChild;
                data.Jumbyek = 1;
                data.Ekspresi = "1";
                data.Satuan = null;
                data.Tarif = 0;
                _tukdContext.Rkadetr.Update(data);
                _tukdContext.SaveChanges();
            }
        }

        public async Task<Rkadetr> ViewData(long Idrkadetr)
        {
            Rkadetr Result = await (
                from data in _tukdContext.Rkadetr
                join rka in _tukdContext.Rkar on data.Idrkar equals rka.Idrkar
                where data.Idrkadetr == Idrkadetr
                select new Rkadetr
                {
                    Createdby = data.Createdby,
                    Createddate = data.Createddate,
                    Ekspresi = data.Ekspresi,
                    Idrkar = data.Idrkar,
                    Idrkadetr = data.Idrkadetr,
                    Idrkadetrduk = data.Idrkadetrduk,
                    IdrkarNavigation = rka ?? null,
                    Idsatuan = data.Idsatuan,
                    Idstdharga = data.Idstdharga,
                    Inclsubtotal = data.Inclsubtotal,
                    Jumbyek = data.Jumbyek,
                    Kdjabar = data.Kdjabar,
                    Satuan = data.Satuan,
                    Subtotal = data.Subtotal,
                    Tarif = data.Tarif,
                    Type = data.Type,
                    Updateby = data.Updateby,
                    Updatetime = data.Updatetime,
                    Uraian = data.Uraian
                }
                ).FirstOrDefaultAsync();
            return Result;
        }

        public async Task<List<Rkadetr>> ViewDatas(RkadetrGet param)
        {
            List<Rkadetr> Result = await (
               from data in _tukdContext.Rkadetr
               join rka in _tukdContext.Rkar on data.Idrkar equals rka.Idrkar
               where data.Idrkar == param.Idrkar
                select new Rkadetr
                {
                    Createdby = data.Createdby,
                    Createddate = data.Createddate,
                    Ekspresi = data.Ekspresi,
                    Idrkar = data.Idrkar,
                    Idrkadetr = data.Idrkadetr,
                    Idrkadetrduk = data.Idrkadetrduk,
                    IdrkarNavigation = rka ?? null,
                    Idsatuan = data.Idsatuan,
                    Idstdharga = data.Idstdharga,
                    Inclsubtotal = data.Inclsubtotal,
                    Jumbyek = data.Jumbyek,
                    Kdjabar = data.Kdjabar,
                    Satuan = data.Satuan,
                    Subtotal = data.Subtotal,
                    Tarif = data.Tarif,
                    Type = data.Type,
                    Updateby = data.Updateby,
                    Updatetime = data.Updatetime,
                    Uraian = data.Uraian
                }
                ).ToListAsync();
            return Result;
        }
    }
}
