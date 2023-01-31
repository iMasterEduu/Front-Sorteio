using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using teste.Models;

namespace teste.Controllers
{
    public class ContatosController : Controller
    {
        public ActionResult Index(string Cota = "IDOSO")
        {
            IEnumerable<People> contatos = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44369/api/ListaPessoas");

                //HTTP GET
                var responseTask = client.GetAsync(client.BaseAddress);
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<People>>();
                    readTask.Wait();
                    contatos = readTask.Result;
                }
                else
                {
                    contatos = Enumerable.Empty<People>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
                contatos = contatos.Where(x => x.Cota == Cota).ToList();

                return View(contatos);
            }
        }
    }
}
