using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Models.viewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SalesWebMvcContext _context;
        private readonly SellerService _sellerService;
        private readonly DepartamentService _departamentService;
        public SellersController(SellerService sellerService, DepartamentService departamentService)
        {
            _sellerService = sellerService;
            _departamentService = departamentService;
        }

        public async Task<IActionResult> Index()
        {
            var allSellers = await _sellerService.GetAll();
            return View(allSellers);
        }

        public async Task<IActionResult> Create()
        {
            var listDepartament = await _departamentService.GetAllDepartament();
            var viewModel = new SellerFormViewModel()
            {
                Departaments = listDepartament
            };
            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departamentService.GetAllDepartament();
                var viewModel = new SellerFormViewModel { Seller = seller, Departaments = departments };
                return View(viewModel);
            }

            await _sellerService.CreateSeller(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return ValidateIdNotProvided();
            }
            var seller = await _sellerService.GetSellerById(id.Value);

            if (seller == null)
                return ValidateIdNotFound();

            return View(seller);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveSeller(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            
            
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return ValidateIdNotProvided();
            }
            var seller = await _sellerService.GetSellerById(id);

            if (seller == null)
                return ValidateIdNotFound();

            return View(seller);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return ValidateIdNotProvided();
            }
            var seller = await _sellerService.GetSellerById(id.Value);
            var listDepartament = await  _departamentService.GetAllDepartament();
            var viewModel = new SellerFormViewModel()
            {
                Departaments = listDepartament,
                Seller = seller
            };

            if (seller == null)
                return ValidateIdNotFound();

            return View(viewModel);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Seller seller, int? id)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departamentService.GetAllDepartament();
                var viewModel = new SellerFormViewModel { Seller = seller, Departaments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try
            {
                await _sellerService.UpdateSeller(seller);

                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }
        public IActionResult Error(string? message)
        {
            ErrorViewModel errorView = new ErrorViewModel()
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(errorView); 
        }

        private IActionResult ValidateIdNotProvided()
        {
           return RedirectToAction(nameof(Error), new {message = "Id not provided" });
        }
        private IActionResult ValidateIdNotFound()
        {
            return RedirectToAction(nameof(Error), new { message = "Id not found" });
        }
    }
}
