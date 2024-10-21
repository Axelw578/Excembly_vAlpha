using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Excembly_vAlpha.Controllers
{
    public class TrabajosController : Controller
    {
        // GET: TrabajosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TrabajosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TrabajosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrabajosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TrabajosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TrabajosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TrabajosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TrabajosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
