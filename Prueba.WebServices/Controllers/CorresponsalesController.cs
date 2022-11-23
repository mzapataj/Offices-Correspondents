using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba.Model;
using Prueba.Model.Dao;
using Prueba.WebServices.Repository;

namespace Common.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CorresponsalesController : Controller
    {
        private readonly Prueba_ControlBoxContext _context;

        public CorresponsalesController(Prueba_ControlBoxContext context)
        {
            _context = context;            
        }

        // GET: Corresponsales
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            return _context.Corresponsales != null ?
                        Json(await _context.Corresponsales.ToListAsync()) :
                        Problem("Entity set 'Prueba_ControlBoxContext.Corresponsales'  is null.");
        }

        // GET: Corresponsales/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Corresponsales == null)
            {
                return NotFound();
            }

            var corresponsale = await _context.Corresponsales
                .FirstOrDefaultAsync(m => m.CorCorresponsalId == id);
            if (corresponsale == null)
            {
                return NotFound();
            }            
            return Json(corresponsale);
        }

        // POST: Corresponsales/Create
        [HttpPost("Create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CorCorresponsalId,CorNombre")] Corresponsal corresponsale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(corresponsale);
                await _context.SaveChangesAsync();
                return Json(
                    new ResponseMessage<Corresponsal>()
                    {
                        Info = corresponsale,
                        Success = true
                    }
                );
            }
            return Json(
                new ResponseMessage<Corresponsal>()
                {
                    Info = corresponsale,
                    Success = false
                }
            );
        }

        // POST: Corresponsales/Edit/5
        [HttpPost("Edit/{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("CorCorresponsalId,CorNombre")] Corresponsal corresponsale)
        {
            if (id != corresponsale.CorCorresponsalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(corresponsale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CorresponsaleExists(corresponsale.CorCorresponsalId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new ResponseMessage<Corresponsal>()
                {
                    Info = corresponsale,
                    Success = true
                });
            }
            return Json(new ResponseMessage<Corresponsal>()
            {
                Info = corresponsale,
                Success = false
            });
        }

        // POST: Corresponsales/DeleteConfirmed/5
        [HttpPost("Delete/{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            if (_context.Corresponsales == null)
            {
                return Problem("Entity set 'Prueba_ControlBoxContext.Corresponsales'  is null.");
            }
            var corresponsale = await _context.Corresponsales.FindAsync(id);
            if (corresponsale != null)
            {
                var oficinas = _context.Oficinas.Where(oficina => oficina.OfiCorresponsalId == corresponsale.CorCorresponsalId);

                _context.Oficinas.RemoveRange(oficinas);
                _context.Corresponsales.Remove(corresponsale);
            }

            await _context.SaveChangesAsync();
            return Json(new ResponseMessage<Corresponsal>()
            {
                Info = corresponsale,
                Success = true
            }); ;
        }

        private bool CorresponsaleExists(long id)
        {
            return (_context.Corresponsales?.Any(e => e.CorCorresponsalId == id)).GetValueOrDefault();
        }


        [HttpGet("GetCorresponsalesCountOficinas")]
        public async Task<IActionResult> GetCorresponsalesCountOficinas()
        {
            if (_context.Oficinas == null)
            {
                return Problem("Entity set 'Prueba_ControlBoxContext.Oficinas'  is null.");
            }

            if (_context.Corresponsales == null)
            {
                return Problem("Entity set 'Prueba_ControlBoxContext.Oficinas'  is null.");
            }

            var officeNameMaxLen = from officeMaxLen in _context.Oficinas
                                  group officeMaxLen by officeMaxLen.OfiCorresponsalId into g
                                  select new { OfiCorresponsalId = g.Key, MaxOfiLen = g.Max(x => x.OfiNombre.Length), CountPerOfiCorresponsalId = g.Count()};


            var totalProdcuts = from office in _context.Oficinas 
                                join corresponsal in _context.Corresponsales on office.OfiCorresponsalId equals corresponsal.CorCorresponsalId                                
                                join office2 in officeNameMaxLen on office.OfiCorresponsalId equals office2.OfiCorresponsalId
                                where office2.MaxOfiLen == office.OfiNombre.Length
                                group new { corresponsal, office2 } by
                                new
                                {
                                    corresponsal.CorCorresponsalId,
                                    corresponsal.CorNombre,
                                    office.OfiNombre,
                                    office2.CountPerOfiCorresponsalId
                                }
                                into g
                                select new CorresponsalesOficinas() { CorCorresponsalId = g.Key.CorCorresponsalId, CorNombre = g.Key.CorNombre, TotalOficinas = g.Key.CountPerOfiCorresponsalId, OfiNameMaxLen = g.Key.OfiNombre};

            return Json(totalProdcuts);
        }
    }
}