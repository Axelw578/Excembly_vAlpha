using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Excembly_vAlpha.Controllers
{
    public class TecnicosController : Controller
    {
        // GET: TecnicosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TecnicosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TecnicosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TecnicosController/Create
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

        // GET: TecnicosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TecnicosController/Edit/5
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

        // GET: TecnicosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TecnicosController/Delete/5
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
