using ProjetoMVC.Models;
using ProjetoMVC.Models.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoMVC.Controllers
{
    public class TelefoneController : Controller
    {
        private TelefoneREP  repositorio = new TelefoneREP();
        private FornecedorREP fornecedorREP = new FornecedorREP();

        // GET: Telefone
        public ActionResult Index()
        {
            // return View();
            return View(repositorio.GetAll());
        }

        // GET: Telefone/Create
        public ActionResult Create()
        {
            return View();
        }


        public ActionResult Novo(int id)
        {
            Telefone p = new Telefone()
            {
                Fornecedor = fornecedorREP.GetById(id).Nome
            };
            return RedirectToAction("../Telefone/Create", p);

        }
        // POST: Telefone/Create
        [HttpPost]
        public ActionResult Create(Telefone p)
        {

            if (ModelState.IsValid)
            {
                repositorio.Save(p);
                return RedirectToAction("../Fornecedor/Telefones/" + fornecedorREP.GetByName(p.Fornecedor).Id);
            }
            else
            {
                return View(p);
            }
        }


        // POST: Telefone/Editar/5 
        public ActionResult Editar(int id)
        {
            return View(repositorio.GetById(id));
        }

        // POST: Telefone/Edit/5
        [HttpPost]
        public ActionResult Editar(Telefone p)
        {
            if (ModelState.IsValid)
            {
                repositorio.Update(p);
                return RedirectToAction("../Fornecedor/Telefones/" + fornecedorREP.GetByName((repositorio.GetById(p.Id)).Fornecedor).Id);
            }
            else
            {
                return View(p);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(repositorio.GetById(id));
        }

        // POST: Telefone/Delete/5
        [HttpPost]
        public ActionResult Delete(Telefone a)
        {
            a = repositorio.GetById(a.Id);
            repositorio.DeleteById(a.Id);
            return RedirectToAction("../Fornecedor/FornTelefones/" + fornecedorREP.GetByName(a.Fornecedor).Id);

        }





    }
}