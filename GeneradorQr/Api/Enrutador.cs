using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace GeneradorQr.Api
{
    class Enrutador
    {
        public async Task ManejarPeticion(HttpRequest request, HttpResponse response)
        {
            string texto = request.Form["texto"];
            string color_oscuro = request.Form["color_oscuro"];
            string color_claro = request.Form["color_claro"];

            try
            {
                var ctrl = new Controlador();
                var binario = await ctrl.GenerarQr(
                    texto,
                    color_oscuro,
                    color_claro);
                response.StatusCode = 200;
                response.Headers["Content-Type"] = "image/png";
                
                await response.BodyWriter.WriteAsync(binario);
                await response.CompleteAsync();
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                await response.WriteAsync(ex.Message);
            }
        }
    }
}