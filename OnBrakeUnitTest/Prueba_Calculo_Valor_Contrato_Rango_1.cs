using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBrakeUnitTest
{
    [TestClass]
    public class Prueba_Calculo_Valor_Contrato_Rango_1
    {
        [TestMethod]
        public void Prueba_Rango_1_Calculo_Contrato()
        {
           

            /*Arrange*/
            OnBrake.Negocio.CalculosContrato calculosContrato = new OnBrake.Negocio.CalculosContrato();
            int Asistentes = 2;
            int PersonalAdicional = 3;
            double valoruf = 28716.52;/*Valor de la uf el Dia 30/05/2020 */
            double resultadoEsperado = 6 * valoruf;
            double resultado = 0;

            /*Act*/

            /*Assert*/
            Assert.AreEqual(resultadoEsperado,resultado);


        }
    }
}