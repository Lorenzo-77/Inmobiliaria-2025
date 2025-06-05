using Microsoft.AspNetCore.Mvc;
using InmobiliariaLorenzo.Models;
using Microsoft.AspNetCore.Authorization;
using InmobiliariaLorenzo.ViewModels;

namespace InmobiliariaLorenzo.Controllers
{
    [Authorize]
    public class ContratosController : Controller
    {
        private readonly IRepositorioContrato repoContrato;
        private readonly IRepositorioInmueble repoInmuble;
        private readonly IRepositorioInquilino repoInquilino;
        private readonly IConfiguration config;

        public ContratosController(IRepositorioContrato repo, IRepositorioInmueble repIn, IRepositorioInquilino repoInq, IConfiguration config)
        {
            this.repoContrato = repo;
            this.repoInmuble = repIn;
            this.repoInquilino = repoInq;
            this.config = config;
        }

        // GET: Contratos
        [Route("[controller]/Index")]
        public ActionResult Index()
        {
            var lista = repoContrato.ObtenerTodos();
            return View(lista);
        }

        // GET: Contratos/Details/5
        public ActionResult Details(int id)
        {
            var contrato = repoContrato.ObtenerPorId(id);
            return View(contrato);
        }

        // GET: Contratos/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.listaInmuebles = repoInmuble.ObtenerTodos();
            ViewBag.listaInquilinos = repoInquilino.ObtenerTodos();
            return View();
        }

        // POST: Contratos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contrato contrato)
        {
            ViewBag.listaInmuebles = repoInmuble.ObtenerTodos();
            ViewBag.listaInquilinos = repoInquilino.ObtenerTodos();

            // VALIDACIÓN FECHAS
            if (contrato.Fecha_Fin < contrato.Fecha_Inicio)
            {
                ModelState.AddModelError("Fecha_Fin", "La fecha de fin no puede ser anterior a la fecha de inicio.");
                return View(contrato);
            }

            if (repoInmuble.VerificarDisponibilidad(contrato.Id_Inmueble, contrato.Fecha_Inicio, contrato.Fecha_Fin))
            {
                repoContrato.Alta(contrato);
                TempData["creado"] = "Contrato Creado";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Otro"] = "No se pudo crear el contrato: no hay disponibilidad.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Contratos/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var contrato = repoContrato.ObtenerPorId(id);
            ViewBag.listaInmuebles = repoInmuble.ObtenerTodos();
            ViewBag.listaInquilinos = repoInquilino.ObtenerTodos();
            ViewBag.Mensaje = TempData["Mensaje"];
            return View(contrato);
        }

        // POST: Contratos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contrato contrato)
        {
            ViewBag.listaInmuebles = repoInmuble.ObtenerTodos();
            ViewBag.listaInquilinos = repoInquilino.ObtenerTodos();

            if (contrato.Fecha_Fin < contrato.Fecha_Inicio)
            {
                ModelState.AddModelError("Fecha_Fin", "La fecha de fin no puede ser anterior a la fecha de inicio.");
                return View(contrato);
            }

            // contrato.ModificadoPor = User.Identity.Name;
            // contrato.FechaModificacion = DateTime.Now;

            repoContrato.Modificacion(contrato);
            TempData["editado"] = "Contrato editado";
            return RedirectToAction(nameof(Index));
        }

        // GET: Contratos/Delete/5
        [Authorize(policy: "Administrador")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var contrato = repoContrato.ObtenerPorId(id);
            return View(contrato);
        }

        // POST: Contratos/Delete/5
        [Authorize(policy: "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Contrato contrato)
        {

            // contrato.EliminadoPor = User.Identity.Name;
            // contrato.FechaEliminacion = DateTime.Now;
            repoContrato.Baja(id);
            TempData["eliminado"] = "Contrato eliminado";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult ContratosPorInquilino(int id)
        {
            var lista = repoContrato.ObtenerContratosPorInquilino(id);
            return View(lista);
        }

        [HttpGet]
        public ActionResult ContratosVigentes()
        {
            var lista = repoContrato.ObtenerContratosVigentes();
            return View(lista);
        }

        [HttpGet]
        public ActionResult ContratosInmuebles()
        {
            var lista = repoContrato.ObtenerTodos();
            ViewBag.listaInmuebles = repoInmuble.ObtenerTodos();
            return View(lista);
        }

        [HttpPost]
        public ActionResult ContratosInmuebles(ContratoBusqueda cb)
        {
            var lista = repoContrato.ObtenerContratosPorInmueble(cb);
            ViewBag.listaInmuebles = repoInmuble.ObtenerTodos();
            return View(lista);
        }

        [HttpGet]
        public ActionResult CreateContrato(int id_inmueble = 0)
        {
            ViewBag.id_inmueble = id_inmueble;
            ViewBag.listaInmuebles = repoInmuble.ObtenerTodos();
            ViewBag.listaInquilinos = repoInquilino.ObtenerTodos();
            return View("Create");
        }

        [HttpGet]
        public ActionResult FinalizarContrato(int id, int id_inquilino = 0)
        {
            var contrato = repoContrato.ObtenerPorId(id);
            ViewBag.id_inquilino = id_inquilino;
            ViewBag.Monto = repoContrato.CalcularMontoCancelacion(id);
            return View(contrato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinalizarContrato(int id, Contrato contrato, int id_inquilino = 0)
        {
            ViewBag.id_inquilino = id_inquilino;
            repoContrato.FinalizarContrato(id);
            TempData["Otro"] = "Contrato Finalizado Correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult RenovarContrato(int id_inmueble = 0, int id_inquilino = 0)
        {
            ViewBag.id_inmueble = id_inmueble;
            ViewBag.id_inquilino = id_inquilino;
            ViewBag.listaInmuebles = repoInmuble.ObtenerTodos();
            ViewBag.listaInquilinos = repoInquilino.ObtenerTodos();
            return View("Create");
        }
    }
}
