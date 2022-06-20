// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Web;
using BingBackgroundDownloader;
using HtmlAgilityPack;

try
{
    var url = "https://www.bing.com";
    var web = new HtmlWeb();
    var doc = web.Load(url);

    var imgDiv = doc.DocumentNode.SelectSingleNode("//div[@class='img_cont']");
    if (imgDiv != null)
    {
// <div class="img_cont" style="background-image: url(/th?id=OHR.CelebratingSurfing_ROW9691697820_1920x1080.jpg&amp;rf=LaDigue_1920x1080.jpg); opacity: 1;"><div class="shaders"><div class="topLeft"></div><div class="topRight"></div><div class="bottom"></div></div></div>
        Logger.Instance.Debug("Image div found");

        var style = imgDiv.Attributes["style"].Value;

        var rx = new Regex("url\\((.*)\\)");
        if (rx.IsMatch(style))
        {
            Logger.Instance.Debug("No URL found in style attribute");
            var imgUrl = HttpUtility.HtmlDecode("https://www.bing.com" + rx.Match(style).Groups[1].Value);
            var dest = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "BingImages");
            if (!Directory.Exists(dest))
            {
                Directory.CreateDirectory(dest);
            }

            Logger.Instance.Debug($"Downloading image from: {imgUrl}");
            using var client = new WebClient();
            client.DownloadFile(new Uri(imgUrl), Path.Combine(dest, $"{DateTime.Now:yyyy-MM-dd}.jpg"));
            Logger.Instance.Debug($"Saved image to: {Path.Combine(dest, $"{DateTime.Now:yyyy-MM-dd}.jpg")}");
        }
    }
    else
    {
        Logger.Instance.Debug("No image div found");
    }

    Logger.Instance.Debug($"Done");
}
catch (Exception e)
{
    Logger.Instance.Error(e);
}
