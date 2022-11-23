using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Common.Models;
using WebSites.Services.Interfaces;
using Prueba.Model.Dao;

namespace Common.Controllers
{
    public class OficinasController : Controller
    {
        private readonly IOficinasServices _oficinasServices;
        private readonly ICorresponsalesServices _corresponsalesServices;

        public OficinasController(IOficinasServices oficinasServices, ICorresponsalesServices corresponsalesServices)
        {
            _oficinasServices = oficinasServices;
            _corresponsalesServices = corresponsalesServices;
        }

        // GET: Corresponsales
        public async Task<IActionResult> Index()
        {

            var corresponsales = await _oficinasServices.GetAll();
            return (corresponsales != null && corresponsales?.Count() > 0) ?
                        View(corresponsales.ToList()) :
                        Problem("Entity set 'Prueba_ControlBoxContext.Corresponsales'  is null.");
        }

            // GET: Corresponsales/Details/5
        public async Task<IActionResult> Details(long? id)
        {            
            if (id == null)
            {
                return NotFound();
            }

            var oficina = await _oficinasServices.Details(id);
            
            if (oficina == null)
            {
                return NotFound();
            }            

            return View(oficina);
        }

        // GET: Corresponsales/Create
        public async Task<IActionResult> Create()
        {
            var corresponsales = await _corresponsalesServices.GetAll();
            ViewData["OfiCorresponsalId"] = new SelectList(corresponsales, "CorCorresponsalId", "CorNombre");
            return View(new Oficina());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfiCorresponsalId,OfiNombre")] OficinaCreate oficina)
        {
            var task = await _oficinasServices.Create(oficina);

            if (task.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(oficina);
            }
        }


        // GET: Corresponsales/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var task = _oficinasServices.Details(id);
            var taskCorresponsales = _corresponsalesServices.GetAll();

            if (id == null)
            {
                return NotFound();
            }

            var oficina = await task;

            if (oficina == null)
            {
                return NotFound();
            }

            var corresponsales = await taskCorresponsales;

            
            ViewData["OfiCorresponsalId"] = 
                new SelectList(
                    corresponsales.ToList(),
                    "CorCorresponsalId", "CorNombre",
                    oficina.OfiCorresponsalId);

            return View(oficina);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long? id, [Bind("OfiId,OfiCorresponsalId,OfiNombre")] OficinaCreate editOficina)
        {
            var task = _oficinasServices.Edit(id, editOficina);

            if (id == null)
            {
                return NotFound();
            }

            var response = await task;

            if (response.Info == null)
            {
                return NotFound();
            }

            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(response.Info);
            }
        }

        // GET: Corresponsales/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var task = _oficinasServices.Details(id);

            if (id == null)
            {
                return NotFound();
            }

            var corresponsal = await task;

            if (corresponsal == null)
            {
                return NotFound();
            }


            return View(corresponsal);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(long? OfiId)
        {
            var task = _oficinasServices.Delete(OfiId);

            if (OfiId == null)
            {
                return NotFound();
            }

            var response = await task;

            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(response.Info);
            }
        }
    }
}
