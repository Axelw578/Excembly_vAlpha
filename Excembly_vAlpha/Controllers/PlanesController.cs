using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Excembly_vAlpha.Controllers
{
    public class PlanesController : Controller
    {
        // GET: PlanesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PlanesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PlanesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlanesController/Create
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

        // GET: PlanesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PlanesController/Edit/5
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

        // GET: PlanesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlanesController/Delete/5
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
