﻿using System.Collections.Generic;
using System.Linq;
using WebMvc.Data;
using WebMvc.Models;
using Microsoft.EntityFrameworkCore;
using WebMvc.Services;
using WebMvc.Services.Exceptions;
using System.Threading.Tasks;

namespace WebMvc.Services
{
    public class SellerService
    {
        private readonly WebMvcContext _context;

        public SellerService(WebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)
        {
            
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        public async Task<Seller> FindByIDAsync(int id)
        {
            return await _context.Seller.Include(obj=>obj.Department).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id Not found");
            }
            try
            {
            _context.Update(obj);
            await _context.SaveChangesAsync();

            }
            catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
