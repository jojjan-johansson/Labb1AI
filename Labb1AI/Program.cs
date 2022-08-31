using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Azure;
using Azure.AI.TextAnalytics;

namespace Labb1AI
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                // Get config settings from AppSettings
                IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                IConfigurationRoot configuration = builder.Build();
                string cogSvcEndpoint = configuration["CognitiveServicesEndpoint"];
                string cogSvcKey = configuration["CognitiveServiceKey"];

                // Set console encoding to unicode Varför? https://scripts.sil.org/cms/scripts/page.php?site_id=nrsi&id=utconvertq1 
                Console.InputEncoding = Encoding.Unicode;
                Console.OutputEncoding = Encoding.Unicode;

                // Create client using endpoint and key
                AzureKeyCredential credentials = new AzureKeyCredential(cogSvcKey);
                Uri endpoint = new Uri(cogSvcEndpoint);
                TextAnalyticsClient CogClient = new TextAnalyticsClient(endpoint, credentials);


                Console.WriteLine("Här ser jag vilket språk du skriver på, skriv in lite text:");
                DetectedLanguage detectedLanguage = CogClient.DetectLanguage(Console.ReadLine());
                Console.WriteLine($"\nDu har skrivit en text på språket : {detectedLanguage.Name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
