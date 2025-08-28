using Microsoft.Win32;
using System;
using System.Net;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;

namespace S2SWallpaper
{
    class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private const int SPI_SETDESKWALLPAPER = 20;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDCHANGE = 0x02;

        static void Main(string[] args)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile("https://s2s.uk.com/wp-content/uploads/2024/05/24_01_S2S_Screensaver_1920x1080_cropped.png", @"c:\ProgramData\s2sWallpaper.png");
            }
            string wallpaperPath = @"c:\ProgramData\s2sWallpaper.png"; // Replace with your image path
            SetWallpaper(wallpaperPath);
        }

        static void SetWallpaper(string path)
        {
            // Set wallpaper style in the registry
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            if (key != null)
            {
                key.SetValue("WallpaperStyle", 10); // Style
                key.Close();
            }
            int result = SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
            if (result == 0)
            {
                //Console.WriteLine("Failed to set wallpaper.");
            }
            else
            {
                //Console.WriteLine("Wallpaper set successfully!");
            }
        }
    }
}
