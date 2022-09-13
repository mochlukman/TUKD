﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class BktmemdetnRepo : Repo<Bktmemdetn>, IBktmemdetnRepo
    {
        public BktmemdetnRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _ctx => _context as TukdContext;
        public async Task<bool> Update(long Idbmdet, long Nilai)
        {
            Bktmemdetn data = await _ctx.Bktmemdetn.Where(w => w.Idbmdetn == Idbmdet).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Nilai = Nilai;
            _ctx.Bktmemdetn.Update(data);
            if (await _ctx.SaveChangesAsync() > 0) return true;
            return false;
        }
    }
}
