using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FilmesMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FilmesMVC.Controllers
{
    public class FilmeController : Controller
    {
        public string uriBase = "http://filmes.somee.com/FilmeWebApi/Filme/";

        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {

            try
            {
                string uriComplementar = "GetAll";

                HttpClient httpClient = new HttpClient();
                //string token = HttpContext.Session.GetString("SessionTokenUsuario");
                //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await httpClient.GetAsync(uriBase + uriComplementar);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<FilmeViewModel> listaFilmes = await Task.Run(() =>
                         JsonConvert.DeserializeObject<List<FilmeViewModel>>(serialized));

                    return View(listaFilmes);
                }
                else
                    throw new System.Exception(serialized);
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(FilmeViewModel f)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string token = HttpContext.Session.GetString("SessionTokenUsuario");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var content = new StringContent(JsonConvert.SerializeObject(f));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = string.Format("Filme {0}, Id {1} salvo com sucesso!", f.Nome, serialized);
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new System.Exception(serialized);
                }
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Create");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }















    }
}