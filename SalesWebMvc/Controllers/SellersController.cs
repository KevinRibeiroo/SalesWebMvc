﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Models.viewModels;
using SalesWebMvc.Services;

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

        public IActionResult Index()
        {
            var allSellers = _sellerService.GetAll();
            return View(allSellers);
        }

        public IActionResult Create()
        {
            var listDepartament = _departamentService.GetAllDepartament();
            var viewModel = new SellerFormViewModel()
            {
                Departaments = listDepartament
            };
            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.CreateSeller(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var seller = _sellerService.GetSellerById(id.Value);

            if (seller == null)
               return NotFound();

            return View(seller);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.RemoveSeller(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var seller = _sellerService.GetSellerById(id);

            if (seller == null)
                return NotFound();

            return View(seller);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var seller = _sellerService.GetSellerById(id.Value);
            var listDepartament = _departamentService.GetAllDepartament();
            var viewModel = new SellerFormViewModel()
            {
                Departaments = listDepartament,
                Seller = seller
            };

            if (seller == null)
                return NotFound();

            return View(viewModel);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Seller seller, int? id)
        {
            if ( seller == null)
            {
                return NotFound();
            }
            
            try
            {
                _sellerService.UpdateSeller(seller);

                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
