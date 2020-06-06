using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnBrake.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OnBrake.Negocio.Tests
{
    [TestClass()]
    public class CalculosContratoTests
    {
        [TestMethod]
        public void CalcularValorAdicionalContratoPrueba_Calculo_Valor_Contrato()
            /* prueba unitaria para Calculo Total Contrato*/
        {
            /* instanciamos el  Calculos Contrato y luego agregamos el metodo de calcular que tiene que tener parametros*/
            OnBrake.Negocio.CalculosContrato calculosContrato = new OnBrake.Negocio.CalculosContrato();
            int asistentes =2;
            int personalAdicional = 3;
            int valorbaseEvento = 3;/* modalidad Light Break*/
            double resultado=0 ;
            double valor_uf = 28716.52;
            double valor_esperado = 0;
            valor_esperado = 9 * valor_uf;/*multiplicamos la cantidad de uf por su valor*/
            calculosContrato.CalcularValorAdicionalContrato(asistentes,personalAdicional,valorbaseEvento, ref resultado);
            Assert.AreEqual(resultado, valor_esperado);
            
          
        }/*Testeado el 04/06/2020 (Correcto)*/
    }
}