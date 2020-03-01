using ProjetoMVC.Models;
using ProjetoMVC.Models.Repositorios;
using ProjetoMVC.Ultis;
using System.Web.Mvc;

namespace ProjetoMVC.Controllers
{
    public class FornecedorController : Controller
    {
        private FornecedorREP repositorio = new FornecedorREP();
        private EmpresaREP empresaREP = new EmpresaREP();
        private TelefoneREP telefoneREP = new TelefoneREP();

        // GET: Fornecedor
        public ActionResult Index()
        {
            // return View();
            return View(repositorio.GetAll());
        }

        // GET: Fornecedor/Create
        public ActionResult Create()
        {
            return View();
        }


        public ActionResult Novo(int id)
        {
            Fornecedor p = new Fornecedor()
            {
                Empresa = empresaREP.GetById(id).NomeFantasia
            };
            return RedirectToAction("../Fornecedor/Create", p);

        }
        // POST: Fornecedor/Create
        [HttpPost]
        public ActionResult Create(Fornecedor p)
        {
            Empresa empresa = empresaREP.GetByName(p.Empresa);

            if (Validador.ValidaCPF(p.CpfCnpj))
            {
                p.Tipo = 0;
                if (string.IsNullOrWhiteSpace(p.RG))
                {
                    ModelState.AddModelError("RG", "Para cadastro de pessoa fisica é necessario informar o RG.");
                    return View();
                }

                if (string.IsNullOrWhiteSpace(p.DataNascimento.ToString()))
                {
                    ModelState.AddModelError("DataNascimento", "Para cadastro de pessoa fisica é necessario informar a Data de nascimento.");
                    return View();
                }

                if ( p.DataNascimento.ToString().Equals("01/01/0001 00:00:00"))
                {
                    ModelState.AddModelError("DataNascimento", "Para cadastro de pessoa fisica é necessario informar a data de nascimento.");
                    return View();
                }


                if (Validador.calcularIdade(p.DataNascimento) < 18 && empresa.UF.ToUpper().Equals("PR") )
                {
                    ModelState.AddModelError("DataNascimento", "Para empresas do Paraná não é permitido cadastro de fornecedores menores de idade.");
                    return View();
                }
            } else if (Validador.ValidaCNPJ(p.CpfCnpj))
            {
                p.Tipo = 1;
            }
            else
            {
                ModelState.AddModelError("CpfCnpj", "Informe um CPF ou CNPJ válido."); 
                return View();
            }

            if (ModelState.IsValid)
            {
                repositorio.Save(p);
                return RedirectToAction("../Empresa/Fornecedores/" + empresaREP.GetByName(p.Empresa).Id);
            }
            else
            {
                return View(p);
            }
        }
 

        // POST: Fornecedor/Editar/5 
        public ActionResult Editar(int id)
        { 
            return View(repositorio.GetById(id));
        }

        // POST: Fornecedor/Edit/5
        [HttpPost]
        public ActionResult Editar(Fornecedor p)
        {
            if (ModelState.IsValid)
            {
                repositorio.Update(p);
                return RedirectToAction("../Empresa/Fornecedores/" + empresaREP.GetByName((repositorio.GetById(p.Id)).Empresa).Id);
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

        // POST: Fornecedor/Delete/5
        [HttpPost]
        public ActionResult Delete(Fornecedor a)
        {
            a = repositorio.GetById(a.Id);
            repositorio.DeleteById(a.Id);
            return RedirectToAction("../Empresa/Fornecedores/" + empresaREP.GetByName(a.Empresa).Id);

        }

        // GET: Empresa/Fornecedores/1
        public ActionResult Telefones(int id)
        {
            return View(telefoneREP.GetByRef(id));
        }



    }
}