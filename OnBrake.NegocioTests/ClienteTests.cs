using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OnBrake.Negocio.Tests
{
    [TestClass()]
    public class ClienteTests
    {
        [TestMethod]
        public void CreatePrueba_Creacion_Cliente()/*prueba unitaria Agregar cliente*/
        {
            bool resultado = false;

           OnBrake.Negocio.Cliente cli = new Cliente()
            {
                RutCliente = "20320341-3",
                RazonSocial = "nada",
                NombreContacto = "Miguel Gomez",
                MailContacto = "miguelgomezpana2006@gmail.com",
                Direccion = "Calle siempre viva 232",
                Telefono = "98287773",
                IdActividadEmpresa = 1,
                IdTipoEmpresa = 10
            };


            
            
                resultado = cli.Create();
                Assert.AreEqual(true, resultado);

            /*Testeado Correctament el 04-06-2020*/
            

        }
    }
}