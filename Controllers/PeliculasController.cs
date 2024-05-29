using Microsoft.AspNetCore.Mvc;
using MvcPeliculasApiAWS.Models;
using MvcPeliculasApiAWS.Services;

namespace MvcPeliculasApiAWS.Controllers
{
    public class PeliculasController : Controller
    {
        private ServiceApiPeliculas service;
        private ServiceStorageAWS serviceStorage;
        public PeliculasController(ServiceApiPeliculas service, ServiceStorageAWS serviceStorage)
        {
            this.service = service;
            this.serviceStorage = serviceStorage;
        }


        public async Task<IActionResult> Index()
        {
            List<Pelicula> pelis = await this.service.GetPeliculasAsync();
            return View(pelis);
        }
        public IActionResult PeliculasActor()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PeliculasActor(string actor)
        {
            List<Pelicula> pelis = await this.service.GetPeliculasActorAsync(actor);
            return View(pelis);
        }
        public async Task<IActionResult> Details(int id)
        {
            Pelicula peli = await this.service.FindPeliculaAsync(id);
            return View(peli);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create
            (Pelicula peli, IFormFile file)
        {
            peli.Foto = file.FileName;

            using(Stream stream = file.OpenReadStream())
            {
                await this.serviceStorage.UploadFileAsync(file.FileName, stream);
            }

            await this.service.CreatePeliculaAsync(peli);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            Pelicula peli = await this.service.FindPeliculaAsync(id);
            return View(peli);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Pelicula peli)
        {
            await this.service.UpdatePeliculaAsync(peli);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePeliculaAsync(id);
            return RedirectToAction("Index");
        }
    }
}
