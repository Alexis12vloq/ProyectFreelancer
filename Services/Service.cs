using Azure.Core;
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
namespace ProjectFreeelancer.Services
{
    public class Service
    {
        private readonly ProyectofreelancerContext dbcontext;
        public Service(ProyectofreelancerContext context)
        {
            dbcontext = context;
        }

        public async Task<Document> DocumentResponse(Recibo request)
        {
            await dbcontext.Recibos.AddAsync(request);
            await dbcontext.SaveChangesAsync();
            string imagenBase64 = request.Logo;
            byte[] imagentran;
            string base64Image = imagenBase64.Split(',')[1];

            byte[] imageBytes = Convert.FromBase64String(base64Image);

            MemoryStream ms = new MemoryStream(imageBytes);

            Image imagen = Image.FromStream(ms);

            using (MemoryStream ms2 = new MemoryStream())
            {
                imagen.Save(ms2, imagen.RawFormat);
                imagentran = ms2.ToArray();
            }
            var document =  Document.Create(container =>
            {

                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                        .Text(request.Titulo)
                        .SemiBold().FontSize(46).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(20);
                            x.Item().Text($"Nombres completos: {request.NombresCompletos}");
                            x.Item().Text($"Descripción: {request.Descripcion}");
                            x.Item().Text($"Monto a cobrar: {request.MontoCobrar} {request.TipoMoneda}");
                            x.Item().Text($"Tipo de documento: {request.TipoDocumento}");
                            x.Item().Text($"Número de documento: {request.NumeroDocumento}");
                            x.Item().Text($"Dirección: {request.Direccion}");
                            x.Item().Image(imagentran);

                        });
                });
            });
            return document;
        }
    }
}
