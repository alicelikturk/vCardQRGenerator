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
        var vcards = _csvService.ReadCSV<Vcard>(file[0].OpenReadStream());

        return Ok(vcards);
    }

    [HttpPost("generate-qr")]
    public async Task<IActionResult> GetQrCode(string vCardInfo)
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


        using MemoryStream ms = new MemoryStream();
        image.SaveAsPng(ms);

        //file.CopyTo(ms);
        //fileArray = Encoding.ASCII.GetString(ms.ToArray());
        //fileArray = BitConverter.ToString(ms.ToArray()).Replace("-", "");
        Console.WriteLine(vCardInfo);
        return Ok(String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray())));




    }


}



