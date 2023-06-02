using System.Diagnostics;
using CsvHelper;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using vCardQRGenerator.Infrastructure.Service;
using vCardQRGenerator.Models;
using ZXing.QrCode;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.DrawingCore.Imaging;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Advanced;

namespace vCardQRGenerator.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICSVService _csvService;


    public HomeController(ILogger<HomeController> logger,
        ICSVService csvService)
    {
        _logger = logger;
        _csvService = csvService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost("read-csv-file")]
    public async Task<IActionResult> GetCSVFile([FromForm] IFormFileCollection file)
    {
        Console.WriteLine();
        var vcards = _csvService.ReadCSV<Vcard>(file[0].OpenReadStream());

        return Ok(vcards.OrderBy(x=>x.FirstName));
    }

    [HttpPost("generate-qr")]
    public async Task<IActionResult> GetQrCode([FromForm] string vCardInfo, IFormFile file)
    {
        Byte[] byteArray;
        var width = 250; // width of the QR Code
        var height = 250; // height of the QR Code
        var margin = 0;
        var qrCodeWriter = new ZXing.BarcodeWriterPixelData
        {
            Format = ZXing.BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width,
                Margin = margin
            }
        };
        var pixelData = qrCodeWriter.Write(vCardInfo.Replace("%0A", "\n"));

        using var image = Image.LoadPixelData<SixLabors.ImageSharp.PixelFormats.Bgra32>(pixelData.Pixels, pixelData.Width, pixelData.Height);

        if (file != null)
        {

            using var imageLogo = Image.Load(file.OpenReadStream());
            string newSize = ResizeImage(imageLogo, 50, 50);
            string[] aSize = newSize.Split(',');
            imageLogo.Mutate(h => h.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));

            image.Mutate(imageContext =>
            {
                // new Point((image.Width - imageLogo.Width) / 2, (image.Height - imageLogo.Height) / 2)
                imageContext.DrawImage(imageLogo, new Point((image.Width - imageLogo.Width) / 2, (image.Height - imageLogo.Height)/2), 1);
            });

        }

        using MemoryStream ms = new MemoryStream();
        image.SaveAsPng(ms);

        //file.CopyTo(ms);
        //fileArray = Encoding.ASCII.GetString(ms.ToArray());
        //fileArray = BitConverter.ToString(ms.ToArray()).Replace("-", "");
        return Ok(String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray())));




    }

    private byte[] UploadedFile(IFormFile file)
    {
        var fileArray = new byte[] { };

        if (file != null)
        {
            var image = Image.Load(file.OpenReadStream());
            string newSize = ResizeImage(image, 30, 30);
            string[] aSize = newSize.Split(',');
            image.Mutate(h => h.Resize(Convert.ToInt32(aSize[1]), Convert.ToInt32(aSize[0])));


            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, new JpegEncoder());

                //file.CopyTo(ms);
                //fileArray = Encoding.ASCII.GetString(ms.ToArray());
                //fileArray = BitConverter.ToString(ms.ToArray()).Replace("-", "");
                fileArray = ms.ToArray();
            }
        }
        return fileArray;
    }
    private string ResizeImage(Image img, int maxWidth, int maxHeight)
    {
        if (img.Width > maxWidth || img.Height > maxHeight)
        {
            double widthRatio = (double)img.Width / (double)maxWidth;
            double heightRatio = (double)img.Height / (double)maxHeight;
            double ratio = Math.Max(widthRatio, heightRatio);
            int newWidth = (int)(img.Width / ratio);
            int newHeight = (int)(img.Height / ratio);
            return newHeight.ToString() + "," + newWidth.ToString();
        }
        else
            return img.Height.ToString() + "," + img.Width.ToString();
    }


}



