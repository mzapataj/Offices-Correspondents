using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebSites.Services.Interfaces;

namespace Common.Controllers
{
    public class CorresponsalesController : Controller
    {
        private readonly ICorresponsalesServices _corresponsalesServices;
        private readonly IOficinasServices _oficinasServices;

        public CorresponsalesController(ICorresponsalesServices corresponsalesServices, IOficinasServices oficinasServices)
        {
            _corresponsalesServices = corresponsalesServices;
            _oficinasServices = oficinasServices;
        }

        // GET: Corresponsales
        public async Task<IActionResult> Index()
        {

            var corresponsales =await _corresponsalesServices.GetAll();
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

            var corresponsal = await _corresponsalesServices.Details(id);
            var task = _oficinasServices.GetOficinasCorresponsal(id);

            if (corresponsal == null)
            {
                return NotFound();
            }

            var oficinas = await task;
            if (oficinas.Success)
            {
                corresponsal.Oficinas = oficinas.Info
                            .Select(oficina => new Oficina()
                            { OfiId = oficina.OfiId, OfiNombre = oficina.OfiNombre, OfiCorresponsalId = oficina.OfiCorresponsalId }).ToList();
            }

            return View(corresponsal);
        }

        // GET: Corresponsales/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CorCorresponsalId,CorNombre")] Corresponsal corresponsal)
        {
            var task = await _corresponsalesServices.Create(corresponsal);

            if (task.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(corresponsal);
            }
        }


        // GET: Corresponsales/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {            
            if (id == null)
            {
                return NotFound();
            }            

            var corresponsal = await _corresponsalesServices.Details(id);            

            if (corresponsal == null)
            {
                return NotFound();
            }

            return View(corresponsal);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long? id, [Bind("CorCorresponsalId,CorNombre")] Corresponsal editCorresponsal)
        {
            var task = _corresponsalesServices.Edit(id, editCorresponsal);

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
            new Dictionary<int, int>();
            if (id == null)
            {
                return NotFound();
            }

            var corresponsal = await _corresponsalesServices.Details(id);
            var task = _oficinasServices.GetOficinasCorresponsal(id);
            if (corresponsal == null)
            {
                return NotFound();
            }

            var oficinas = await task;
            if (oficinas.Success)
            {
                corresponsal.Oficinas = oficinas.Info
                            .Select(oficina => new Oficina()
                            { OfiId = oficina.OfiId, OfiNombre = oficina.OfiNombre, OfiCorresponsalId = oficina.OfiCorresponsalId }).ToList();
            }

            return View(corresponsal);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(long? CorCorresponsalId)
        {
            var task = _corresponsalesServices.Delete(CorCorresponsalId);

            if (CorCorresponsalId == null)
            {
                return NotFound();
            }

            var response = await task;
            var xdf = new int[2];
                
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
    }
}
