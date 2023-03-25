using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModel;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FinAllDepartAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            try
            {
                await _sellerService.InsertAsync(seller);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = await _sellerService.GetByIdAsync(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = await _sellerService.GetByIdAsync(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = await _sellerService.GetByIdAsync(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            List<Department> departments = await _departmentService.FinAllDepartAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {

            if (id != seller.Id)
            {
                return NotFound();
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));

            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
        }
    }
}
