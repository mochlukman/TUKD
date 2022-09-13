using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IDpadetrRepo : IRepo<Dpadetr>
    {
        Task<decimal?> getSubTotal(long iddpa);
        Task<string> GetKodeInduk(long iddpadet, long iddpa);
        void UpdateType(long iddpadetduk, long iddpa, string type);
        void UpdateNilaiInduk(long? iddpadetduk, long iddpa);
        Task<bool> Update(Dpadetr param);
    }
}
