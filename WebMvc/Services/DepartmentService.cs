using System.Collections.Generic;
using System.Linq;
using WebMvc.Data;
using WebMvc.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebMvc.Services
{
    public class DepartmentService
    {
        private readonly WebMvcContext _context;

        public DepartmentService(WebMvcContext context)
        {
            _context = context;
        }   

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
