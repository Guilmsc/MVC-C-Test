using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Policy;
using System.Threading.Tasks;
using WebMvc.Models;
using WebMvc.Models.ViewModels;
using WebMvc.Services;
using WebMvc.Services.Exceptions;

namespace WebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _SellerService;
        private readonly DepartmentService _departmentService;
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _SellerService = sellerService;
            _departmentService = departmentService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _SellerService.FindAllAsync();

            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            var departments  = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            await _SellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            var obj = await _SellerService.FindByIDAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new {message = "Seller not found"});
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {await _SellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));

            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new {Message = e.Message});
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _SellerService.FindByIDAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Seller not found" });
            }
            return View(obj);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _SellerService.FindByIDAsync(id.Value);
            if( obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Seller not found" });
            }
            List<Department> departments = await _departmentService.FindAllAsync();

            SellerFormViewModel viewModel = new SellerFormViewModel { Seller=obj, Departments=departments};

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments=departments};
                return View(viewModel);
            }
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Seller doesn't match with Id" });
            }
            try
            {
            await _SellerService.UpdateAsync(seller);
            return RedirectToAction(nameof(Index));

            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };

            return View(viewModel);
        }
    }
}
