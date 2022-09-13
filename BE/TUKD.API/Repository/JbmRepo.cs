﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class JbmRepo : Repo<Jbm>, IJbmRepo
    {
        public JbmRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _c => _context as TukdContext;

        public async Task<bool> Update(Jbm param)
        {
            Jbm data = await _c.Jbm.Where(w => w.Idjbm == param.Idjbm).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Kdbm = param.Kdbm;
            data.Nmbm = param.Nmbm;
            _c.Jbm.Update(data);
            if (await _c.SaveChangesAsync() > 0) return true;
            return false;
        }
    }
}
