using System.Drawing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectFreeelancer.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.IO;
using QuestPDF.Drawing;
using System.ComponentModel;
using SkiaSharp;
using QuestPDF.Infrastructure;
using ProjectFreeelancer.Services;

namespace ProjectFreeelancer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReciboController : ControllerBase
    {

        private readonly ProyectofreelancerContext dbcontext;
        private  Service _service;


        public ReciboController(ProyectofreelancerContext context)
        {
            _service = new Service(context);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<FileContentResult> Guardar([FromBody] Recibo request)
        {
                var documentResponse = await _service.DocumentResponse(request);
                return File(documentResponse.GeneratePdf(), "application/pdf", true);

        }
    }
}
