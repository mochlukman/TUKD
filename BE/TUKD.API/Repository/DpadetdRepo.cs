using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class DpadetdRepo : Repo<Dpadetd>, IDpadetdRepo
    {
        public DpadetdRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<string> GetKodeInduk(long iddpadet, long iddpa)
        {
            string kode = await _tukdContext.Dpadetd.Where(w => w.Iddpadetd == iddpadet && w.Iddpad == iddpa)
                 .Select(s => s.Kdjabar.Trim()).FirstOrDefaultAsync();
            return kode;
        }

        public async Task<decimal?> getSubTotal(long iddpa)
        {
            decimal? total = await _tukdContext.Dpadetd.Where(w =>
                             w.Iddpad == iddpa &&
                             (w.Iddpadetdduk == 0 || String.IsNullOrEmpty(w.Iddpadetdduk.ToString())))
                             .Select(s => s.Subtotal)
                             .SumAsync();
            return total;
        }

        public async Task<bool> Update(Dpadetd param)
        {
            Dpadetd data = await _tukdContext.Dpadetd.Where(w => w.Iddpadetd == param.Iddpadetd).FirstOrDefaultAsync();
            if (data == null)
                return false;
            data.Uraian = param.Uraian;
            data.Ekspresi = param.Ekspresi;
            data.Jumbyek = param.Jumbyek;
            data.Kdjabar = param.Kdjabar;
            data.Iddpadetdduk = param.Iddpadetdduk;
            data.Satuan = param.Satuan;
            data.Tarif = param.Tarif;
            data.Subtotal = param.Subtotal;
            data.Idsatuan = param.Idsatuan;
            data.Type = param.Type;
            _tukdContext.Dpadetd.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public void UpdateNilaiInduk(long? iddpadetduk, long iddpa)
        {
            decimal? total = _tukdContext.Dpadetd.Where(w => w.Iddpadetdduk == iddpadetduk && w.Iddpad == iddpa)
               .Select(s => s.Subtotal)
               .Sum();
            Dpadetd data = _tukdContext.Dpadetd.Where(w => w.Iddpadetd == iddpadetduk && w.Iddpad == iddpa).FirstOrDefault();
            if (data != null)
            {
                data.Subtotal = total;
                _tukdContext.Dpadetd.Update(data);
                _tukdContext.SaveChanges();
                if (data.Iddpadetdduk != 0 || !String.IsNullOrEmpty(data.Iddpadetdduk.ToString()))
                    UpdateNilaiInduk(data.Iddpadetdduk, data.Iddpad);
            }
        }

        public void UpdateType(long iddpadetduk, long iddpa, string type)
        {
            Dpadetd data = _tukdContext.Dpadetd.Where(w => w.Iddpadetd == iddpadetduk && w.Iddpad == iddpa).FirstOrDefault();
            if (data != null)
            {
                data.Type = type;
                _tukdContext.Dpadetd.Update(data);
                _tukdContext.SaveChanges();
            }
        }
    }
}
