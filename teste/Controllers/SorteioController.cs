using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using teste.Models;

namespace teste.Controllers
{
    public class SorteioController : Controller
    {
        public ActionResult SorteioAleatorio()
        {
            IEnumerable<People> contatos = null;

            using (var client = new HttpClient()) { 
                client.BaseAddress = new Uri($"https://localhost:5001/api/Sorteio");

            //HTTP GET
            var responseTaska = client.GetAsync(client.BaseAddress);
            responseTaska.Wait();
            var resultado = responseTaska.Result;

            if (resultado.IsSuccessStatusCode)
            {
                var readTask = resultado.Content.ReadAsAsync<IList<People>>();
                readTask.Wait();
                contatos = readTask.Result;
            }
            else
            {
                contatos = Enumerable.Empty<People>();
                ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
            }
            return View(contatos);
            }



        }
    }
}
