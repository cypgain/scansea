using Newtonsoft.Json;
using ScanSeaProtocols.Messages;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScanSeaApp.Core
{
    public class WebServiceConnection
    {

        private const string Url = "http://192.168.1.37:45455";

        public async Task<CodeMessageData> Login(string code)
        {
            using(var client = new HttpClient())
            {
                CodeMessageData messageData = null;

                string url = $"{Url}/code/{code}";
                try
                {
                    Console.WriteLine("1");
                    string json = await client.GetStringAsync(url);
                    Console.WriteLine("1");
                    messageData = JsonConvert.DeserializeObject<CodeMessageData>(json);
                    Console.WriteLine("1");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    messageData = null;
                }

                return messageData;
            }
        }

        public async Task<PermissionsMessageData> GetPermissions(string code)
        {
            using (var client = new HttpClient())
            {
                PermissionsMessageData messageData = null;

                string url = $"{Url}/permissions/{code}";
                try
                {
                    string json = await client.GetStringAsync(url);
                    messageData = JsonConvert.DeserializeObject<PermissionsMessageData>(json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    messageData = null;
                }

                return messageData;
            }
        }

        public async Task<OCLibreNumeroMessageData> GetOcLibreNumero(string code, int agence, string numero)
        {
            using (var client = new HttpClient())
            {
                OCLibreNumeroMessageData messageData = null;

                string url = $"{Url}/oclibre/numero/{code}/{agence}/{numero}";
                try
                {
                    string json = await client.GetStringAsync(url);
                    messageData = JsonConvert.DeserializeObject<OCLibreNumeroMessageData>(json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    messageData = null;
                }

                return messageData;
            }
        }

        public async Task<OCLibrePaletteMessageData> GetOcLibrePalette(string code, int agence, string palette, int idrec, int idcgmt)
        {
            using (var client = new HttpClient())
            {
                OCLibrePaletteMessageData messageData = null;

                string url = $"{Url}/oclibre/palette/{code}/{agence}/{palette}/{idrec}/{idcgmt}";
                try
                {
                    string json = await client.GetStringAsync(url);
                    messageData = JsonConvert.DeserializeObject<OCLibrePaletteMessageData>(json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    messageData = null;
                }

                return messageData;
            }
        }

        public async Task<InventaireNumeroMessageData> GetInventaireNumero(string code, int agence, string numero)
        {
            using (var client = new HttpClient())
            {
                InventaireNumeroMessageData messageData = null;

                string url = $"{Url}/inventaire/numero/{code}/{agence}/{numero}";
                try
                {
                    string json = await client.GetStringAsync(url);
                    messageData = JsonConvert.DeserializeObject<InventaireNumeroMessageData>(json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    messageData = null;
                }

                return messageData;
            }
        }

        public async Task<InventairePaletteMessageData> GetInventairePalette(string code, int agence, int inventaire, string emplacement, string palette)
        {
            using (var client = new HttpClient())
            {
                InventairePaletteMessageData messageData = null;

                string url = $"{Url}/inventaire/palette/{code}/{agence}/{inventaire}/{emplacement}/{palette}";
                try
                {
                    string json = await client.GetStringAsync(url);
                    messageData = JsonConvert.DeserializeObject<InventairePaletteMessageData>(json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    messageData = null;
                }

                return messageData;
            }
        }
    }
}
