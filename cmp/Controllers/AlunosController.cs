﻿using cmp.Controllers;
using EM.Domain.Conexao;
using EM.Domain.Entidades;
using EM.Domain.Services;
using EM.Repository;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;

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
                    return View(_repository.GetByContendoNoNome(conteudoBusca));
                case "Matricula":
                    try
                    {
                        List<Aluno> ConvertidoAlunoUnico = new List<Aluno>
                        {
                        _repository.GetByMatricula(Convert.ToInt32(conteudoBusca))
                        };

                        if (ConvertidoAlunoUnico[0].Nome == null)
                        {
                            return RedirectToAction("Index");
                        }

                        return View(ConvertidoAlunoUnico);
                    }
                    catch (FormatException)
                    {
                        TempData["MessageError"] = "Por favor informe um número para seleção em matrícula!";
                        return View(_repository.GetAll());
                    }
                    
                    
                default:
                    return View(_repository.GetAll());
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Aluno aluno)
        {
            if (CpfCnpjUtils.IsValid(aluno.Cpf) == true)
            {
                _repository.Add(aluno);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("CPFInvalido", "Erros");
            }

        }
        [HttpGet]
        public ActionResult Edit(int matricula)
        {
            var a = _repository.GetByMatricula(matricula);

            if (a == null)
            {
                return RedirectToAction("Index");
            }

            return View(a);
        }

        [HttpPost]
        public ActionResult Edit(Aluno aluno)
        {
            if (CpfCnpjUtils.IsValid(aluno.Cpf) == true)
            {
                _repository.Update(aluno);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("CPFInvalido", "Erros");
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
