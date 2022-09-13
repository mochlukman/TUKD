using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IDaftrekeningRepo : IRepo<Daftrekening>
    {
        Task<List<Daftrekening>> Search(RekeningSearchParam param);
        Task<List<Daftrekening>> StartKode(string startkode);
        Task<PrimengTableResult<Daftrekening>> StartKodePaging(PrimengTableParam<RekeningStartKodeParam> param);
        Task<List<Daftrekening>> GetByRkar(long Idunit, long Idkeg);
        Task<List<LookupTreeDto>> TreeCheckboxByDpa(long Idunit, long Idspp, string Kdtahap, int Idnojetra, long Idxkode, string Kdstatus);
        Task<List<Daftrekening>> ByJenisAkun(List<long?> Idjnsakun);
        Task<Daftrekening> ViewData(long Idrek);
        Task<bool> Update(Daftrekening param);
        Task<List<TreeTableRekeningRoot>> TreeTableRekeningByBpk(long Idbpk);
        Task<PrimengTableResult<Daftrekening>> ByStsdetd(PrimengTableParam<RekGlobalParam> param);
        Task<PrimengTableResult<Daftrekening>> ForStsdetr(PrimengTableParam<RekGlobalParam> param);
        Task<PrimengTableResult<Daftrekening>> BySpmdetd(PrimengTableParam<RekGlobalParam> param);
        Task<PrimengTableResult<Daftrekening>> BySpmdetb(PrimengTableParam<RekGlobalParam> param);
        Task<PrimengTableResult<Daftrekening>> ByForRkad(PrimengTableParam<RekGlobalParam> param);
        Task<PrimengTableResult<Daftrekening>> ByForRkar(PrimengTableParam<RekGlobalParam> param);
        Task<PrimengTableResult<Daftrekening>> ByForRkab(PrimengTableParam<RekGlobalParam> param);
        Task<PrimengTableResult<Daftrekening>> ByDpa(PrimengTableParam<RekGlobalParam> param);
        Task<PrimengTableResult<Daftrekening>> ByDpaB(PrimengTableParam<RekGlobalParam> param);

    }
}
