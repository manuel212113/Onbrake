using System;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows;

namespace OnBrake
{
    public class UF_VALOR
    {
        public double ValorDiaUf { get; set; }


        public class Serie
        {
            public DateTime Fecha { get; set; }
            public double Valor { get; set; }
        }

        public class InfoUf
        {
            public string Version { get; set; }
            public string Autor { get; set; }
            public string Codigo { get; set; }
            public string Nombre { get; set; }
            public string Unidad_medida { get; set; }
            public IList<Serie> Serie { get; set; }
        }
        public void ObtenerResultadoUf( ref double ufv)
        {
            InfoUf uf;
            string fecha = System.DateTime.Now.ToString("dd-MM-yyyy");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://www.mindicador.cl/api/uf/"+fecha);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                 uf = JsonConvert.DeserializeObject<InfoUf>(json);
                   ufv = uf.Serie[0].Valor;
                

            }

        }


    } 

    
}
