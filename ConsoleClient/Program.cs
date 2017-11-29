using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using RESTCalculService;

namespace DotnetmentorsClient
{
    class Program
    {
        static void Main(string[] args)
        {
            calculEmprunte();
            //CalculTabAmortissement();
            Console.ReadKey(true);
        }

        private static void calculEmprunte()
        {
            cCalulInputContract input = new cCalulInputContract
            {
                dMontantAchat = 10550M,
                dFondsPropores= 20000M,
                iDureeCredit = 240,
                iTauxInteretAnnuel = 2.40
            };
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(cCalulInputContract));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, input);
            WebClient webClient = new WebClient();
            string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            string serviceURL = string.Format("http://localhost:61090/CalulService.svc/CalculEmprunte", "POST", data); 
            byte[] data2 = webClient.DownloadData(serviceURL);
            Stream stream = new MemoryStream(data2);
            DataContractJsonSerializer obj = new DataContractJsonSerializer(typeof(cCalulInputContract));
            cCalculOutputContract output = obj.ReadObject(stream) as cCalculOutputContract;
            Console.WriteLine("Montant à emprunter : " + output.dMontantaEmprunter);
            Console.WriteLine("Mensualité : " + output.dMensualite);
        }

        

        private static void CalculTabAmortissement()
        {
            cCalulInputContract order = new cCalulInputContract
            {

            };

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(cCalulInputContract));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, order);
            string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            WebClient webClient = new WebClient();            
            webClient.Headers["Content-type"] = "application/json";            
            webClient.Encoding = Encoding.UTF8;
            webClient.UploadString("http://localhost:61090/CalulService.svc/GetTabAmortissement", "POST", data);              
            Console.WriteLine(" successfully...");  
        }
    }
}
