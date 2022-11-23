using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba.Model;
using Prueba.Model.Dao;
using Prueba.WebServices.Repository;

namespace Common.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OficinasController : Controller
    {
        private readonly Prueba_ControlBoxContext _context;

        public OficinasController(Prueba_ControlBoxContext context)
        {
            _context = context;
        }

        // GET: Oficinas
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var prueba_ControlBoxContext = _context.Oficinas.Include(o => o.OfiCorresponsal);
            return Json(await prueba_ControlBoxContext.ToListAsync());
        }

        [HttpGet("GetOficinasCorresponsal/{id}")]
        public async Task<IActionResult> GetOficinasCorresponsal(long id)
        {
            var corresponsal = await _context.Corresponsales.FindAsync(id);

            if(corresponsal is null)
            {
                return Json(new ResponseMessage<ICollection<Oficina>> { Info = null, Success=false});
            }

            var oficinas = _context.Oficinas.Where(oficina => oficina.OfiCorresponsalId == corresponsal.CorCorresponsalId).ToList();
            //corresponsal.Oficinas = oficinas;
            return Json(new ResponseMessage<ICollection<Oficina>> { Info = oficinas, Success = true });
        }

        // GET: Oficinas/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Oficinas == null)
            {
                return NotFound();
            }

            var oficina = await _context.Oficinas
                .Include(o => o.OfiCorresponsal)
                .FirstOrDefaultAsync(m => m.OfiId == id);
            if (oficina == null)
            {
                return NotFound();
            }

            return Json(oficina);
        }

        // POST: Oficinas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfiId,OfiCorresponsalId,OfiNombre")] OficinaCreate oficina)
        {
            var newOficina = new Oficina()
            {
                OfiId = oficina.OfiId,
                OfiCorresponsalId = oficina.OfiCorresponsalId,
                OfiNombre = oficina.OfiNombre
            };

            if (ModelState.IsValid)
            {
                _context.Add(newOficina);
                await _context.SaveChangesAsync();
                return Json(new ResponseMessage<Oficina>() { Info = newOficina, Success = true });
            }

            return Json(new ResponseMessage<Oficina>() { Info = newOficina, Success = false });
        }

        // POST: Oficinas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Edit/{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("OfiId,OfiCorresponsalId,OfiNombre")] OficinaCreate oficina)
        {
            var editOficina = new Oficina()
            {
                OfiId = oficina.OfiId,
                OfiCorresponsalId = oficina.OfiCorresponsalId,
                OfiNombre = oficina.OfiNombre
            };

            if (id != editOficina.OfiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editOficina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OficinaExists(editOficina.OfiId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new ResponseMessage<Oficina>() { Info = editOficina , Success = true } );
            }

            return Json(new ResponseMessage<Oficina>() { Info = editOficina, Success = false });
        }

        // POST: Oficinas/Delete/5
        [HttpPost("Delete/{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            if (_context.Oficinas == null)
            {
                return Problem("Entity set 'Prueba_ControlBoxContext.Oficinas'  is null.");
            }
            var oficina = await _context.Oficinas.FindAsync(id);
            if (oficina != null)
            {
                _context.Oficinas.Remove(oficina);
            }

            await _context.SaveChangesAsync();
            return Json(new ResponseMessage<Oficina> { Info = null, Success = true });
        }

        private bool OficinaExists(long id)
        {
            return (_context.Oficinas?.Any(e => e.OfiId == id)).GetValueOrDefault();
        }
    }
}
