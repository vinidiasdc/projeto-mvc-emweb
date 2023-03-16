using EM.Domain.Entidades;
using EM.Domain.Services;
using EM.Repository;
using EM.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EM.Web.Controllers
{

    public class AlunosController : Controller
    {
        private readonly RepositorioAluno _repository;

        public AlunosController(RepositorioAluno logger)
        {
            _repository = logger;
        }

        public IActionResult Index(string tipoBusca, string conteudoBusca)
        {
            switch (tipoBusca)
            {
                case "Nome":
                    try
                    {
                        if (int.TryParse(conteudoBusca, out int resultado))
                        {
                            TempData["NomeInvalido"] = "Informe um nome na busca";
                            return View(_repository.GetAll());
                        }
                        else
                        {
                            return View(_repository.GetByContendoNoNome(conteudoBusca.ToLower()));
                        }
                    }
                    catch (FormatException) { }
                    
                    return View(_repository.GetByContendoNoNome(conteudoBusca.ToLower()));
                case "Matricula":

                    try
                    {
                        List<Aluno> ConvertidoAlunoUnico = new List<Aluno>
                        {
                        _repository.GetByMatricula(Convert.ToInt32(conteudoBusca))
                        };

                        if (ConvertidoAlunoUnico[0].Matricula == 0)
                        {
                            TempData["MatriculaInexistente"] = "Matrícula informada não existe no banco";
                            return RedirectToAction("Index");
                        }

                        return View(ConvertidoAlunoUnico);
                    }
                    catch (FormatException)
                    {
                        TempData["MessageError"] = "Por favor, informe um número para seleção em matrícula!";
                        return View(_repository.GetAll());
                    }

                default:
                    return View(_repository.GetAll());
            }
        }


        [HttpGet]
        public IActionResult Create_Edit(int matricula)
        {
            if (matricula == 0)
            {
                return RedirectToAction("Create");
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public IActionResult Create()
        {
            TempData["ACAO"] = "Create";
            return View("Create_Edit");
        }

        [HttpPost]
        public IActionResult Create(AlunoModel aluno)
        {
            if (CpfCnpjUtils.IsValid(aluno.Cpf) == true)
            {
                _repository.Add(aluno);
                return RedirectToAction("Index");
            }
            else
            {
                TempData["InvalidCPF"] = "Informe um cpf válido!";
                return View("Create_Edit");
            }
        }

        [HttpGet]
        public IActionResult Edit(int matricula)
        {
            TempData["ACAO"] = "Edit";

            Aluno aluno = _repository.GetByMatricula(matricula);
            aluno.Nome = aluno.Nome?.ToPadraoFormal();

            return View("Create_Edit", aluno);
        }

        [HttpPost]
        public IActionResult Edit(Aluno aluno)
        {

            if (CpfCnpjUtils.IsValid(aluno.Cpf) == true)
            {
                _repository.Update(aluno);
                return RedirectToAction("Index");
            }
            else
            {
                TempData["InvalidCPF"] = "Informe um cpf válido!";
                return View("Create_Edit");
            }

        }

        public ActionResult Delete(int matricula)
        {
            Aluno a1 = _repository.GetByMatricula(matricula);
            _repository.Remove(a1);
            return RedirectToAction("Index");
        }

    }
}
