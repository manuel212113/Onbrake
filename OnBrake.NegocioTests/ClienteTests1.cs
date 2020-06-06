using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnBrake.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBrake.Negocio.Tests
{
    [TestClass()]
    public class ClienteTests1
    {
        [TestMethod]
        public void UpdatePrueba_Actualizacion_Cliente()/*Prueba unitaria Actualizacion Datos Cliente*/
        {
            bool resultado = false;
             OnBrake.Negocio.Cliente cli = new Cliente()
            {
                 RutCliente = "20320341-3",
                 RazonSocial = "nada",
                 NombreContacto = "Miguel Gomez",
                 MailContacto = "miguelgomezBZ22@GMAIL.COM",
                 Direccion = "Calle siempre viva 232",
                 Telefono = "98287773",
                 IdActividadEmpresa = 1,
                 IdTipoEmpresa = 10
             };


            
            
                resultado = cli.Update();
                Assert.AreEqual(true, resultado);
        }
    }
}