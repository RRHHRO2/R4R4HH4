using Proyecto.Context;
using System.Linq;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();


        [HttpGet]
        public ActionResult Index()
        {
            // Cantidad de empleados (usando IdEmpleado)
            var totalEmpleados = db.Empleados.Select(e => e.IdEmpleado).Count();

            // Cantidad de ausencias (usando Id)
            var totalAusencias = db.Ausencias.Select(a => a.Id).Count();

            // Cantidad de contratos (usando Id)
            var totalContratos = db.Contratos.Select(c => c.Id).Count();

            // Simulación de contrataciones mensuales
            var contratacionesMensuales = new int[] { 12, 19, 7, 15, 10, 17 };

            // Pasar valores a la vista
            ViewBag.TotalEmpleados = totalEmpleados;
            ViewBag.TotalAusencias = totalAusencias;
            ViewBag.TotalContratos = totalContratos;
            ViewBag.ContratacionesMensuales = contratacionesMensuales;

            return View();
        }
    }
}
