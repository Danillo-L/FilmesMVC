using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FilmesMVC.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;


namespace FilmesMVC.Controllers
{
    public class FilmeController : Controller
    {
        public string uriBase = "http://filmes.somee.com/FilmeWebApi/Filmes/";

        public IActionResult Index()
        {
            return View();
        }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }

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
                //string token = HttpContext.Session.//GetString("SessionTokenUsuario");
                //httpClient.DefaultRequestHeaders.//Authorization = new //AuthenticationHeaderValue("Bearer", //token);

                var content = new StringContent(JsonConvert.SerializeObject(f));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = string.Format("Filme {0}, Id {1} salvo com sucesso!", f.Nome, f.Id ,serialized);
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

        

        [HttpGet]
        public async Task<ActionResult> DetailsAsync(int? id)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
            // string token = HttpContext.Session.GetString("SessionTokenUsuario");
            // httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    FilmeViewModel f = await Task.Run(() =>
                    JsonConvert.DeserializeObject<FilmeViewModel>(serialized));
                    return View(f);
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

        [HttpGet]
        public async Task<ActionResult> EditAsync(int? id)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                //string token = HttpContext.Session.GetString("SessionTokenUsuario");
                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await httpClient.GetAsync(uriBase + id.ToString());
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    FilmeViewModel f = await Task.Run(() =>
                    JsonConvert.DeserializeObject<FilmeViewModel>(serialized));
                    return View(f);
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
        public async Task<ActionResult> EditAsync(FilmeViewModel f)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                //string token = HttpContext.Session.GetString("SessionTokenUsuario");
                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                var content = new StringContent(JsonConvert.SerializeObject(f));
                content.Headers.ContentType = new MediaTypeHeaderValue("aplication/json");

                HttpResponseMessage response = await httpClient.PutAsync(uriBase, content);
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] =
                        string.Format("Filme {0} atualizado com sucesso!", f.Nome);
                    
                    return RedirectToAction("Index");
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

        [HttpGet]
        public async Task<ActionResult> DeleteAsync (int id)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                //string token = HttpContext.Session.GetString("SessionTokenUsuario");
                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await httpClient.DeleteAsync(uriBase + id.ToString());
                string serialized = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["Mensagem"] = string.Format("filme id {0} removido com sucesso!", id);
                    return RedirectToAction("Index");
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
       

    }      
}