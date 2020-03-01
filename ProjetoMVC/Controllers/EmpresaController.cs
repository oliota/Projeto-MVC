using ProjetoMVC.Models;
using ProjetoMVC.Models.Repositorios;
using System.Web.Mvc;

namespace ProjetoMVC.Controllers
{
    public class EmpresaController : Controller
    {

        private EmpresaREP repositorio = new EmpresaREP();
        private FornecedorREP fornecedorREP = new FornecedorREP();

        // GET: Empresa
        public ActionResult Index()
        {
            // return View();
            return View(repositorio.GetAll());
        }


        // GET: Empresa/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Empresa/Create
        [HttpPost]
        public ActionResult Create(Empresa empresa)
        {

            if (ModelState.IsValid)
            {
                repositorio.Save(empresa);
                return RedirectToAction("Index");
            }
            else
            {
                return View(empresa);
            }
        }


        // POST: Empresa/Editar/5 
        public ActionResult Editar(int id)
        { 
            return View(repositorio.GetById(id));
        }

        // POST: Empresa/Edit/5
        [HttpPost]
        public ActionResult Editar(Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                repositorio.Update(empresa);
                return RedirectToAction("Index");
            }
            else
            {
                return View(empresa);
            }


        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(repositorio.GetById(id));
        }


        // POST: Empresa/Delete/5
        [HttpPost]
        public ActionResult Delete(Empresa empresa)
        {
            repositorio.Delete(empresa);
            return RedirectToAction("Index");

        }
        // GET: Empresa/Fornecedores/1
        public ActionResult Fornecedores(int id)
        {
            return View(fornecedorREP.GetByRef(id));
        }





    }
}
