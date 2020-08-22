using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using QRCoder;

namespace GeneradorQr.Api
{
    class Controlador
    {
        private readonly int pixeles_por_punto = 10;
        public async Task<byte[]> GenerarQr(string texto, string colorOscuro, string colorClaro)
        {
            return await Task.Factory.StartNew(() => {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrData = qrGenerator.CreateQrCode(
                    texto,
                    QRCodeGenerator.ECCLevel.M
                );
                QRCode qrCode = new QRCode(qrData);
                Bitmap bitmap = qrCode.GetGraphic(
                    pixeles_por_punto,
                    DecodificarColor(colorOscuro),
                    DecodificarColor(colorClaro),
                    true
                );
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            });
        }

        private Color DecodificarColor(string colorHtml)
        {
            int intColor = int.Parse(colorHtml, NumberStyles.HexNumber);
            int rojo = (intColor & 0xff0000) >> 16;
            int verde = (intColor & 0x00ff00) >> 8;
            int azul = intColor & 0x0000ff;
            return Color.FromArgb(
                rojo,
                verde,
                azul);
        }
    }
}