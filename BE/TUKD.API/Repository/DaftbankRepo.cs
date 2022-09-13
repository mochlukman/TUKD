﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class DaftbankRepo : Repo<Daftbank>, IDaftbankRepo
    {
        public DaftbankRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Daftbank param)
        {
            Daftbank data = await _tukdContext.Daftbank.Where(w => w.Idbank == param.Idbank).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Kdbank = param.Kdbank;
                data.Akbank = param.Akbank;
                data.Alamat = param.Alamat;
                data.Telepon = param.Telepon;
                data.Cabang = param.Cabang;
                data.Datecreate = param.Datecreate;
                _tukdContext.Daftbank.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
