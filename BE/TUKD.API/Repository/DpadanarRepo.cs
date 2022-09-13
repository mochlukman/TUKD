﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class DpadanarRepo : Repo<Dpadanar>, IDpadanarRepo
    {
        public DpadanarRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Dpadanar param)
        {
            Dpadanar data = await _tukdContext.Dpadanar.Where(w => w.Iddpadanar == param.Iddpadanar).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Idjdana = param.Idjdana;
                data.Nilai = param.Nilai;
                data.Dateupdate = param.Dateupdate;
                _tukdContext.Dpadanar.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
