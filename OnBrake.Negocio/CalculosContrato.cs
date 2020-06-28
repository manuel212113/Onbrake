using System.Windows;
using OnBrake.Negocio;


namespace OnBrake.Negocio
{

    public class CalculosContrato
    {
        private UF_VALOR valor_dia_uf = new UF_VALOR();
        double ufvalordia = 0;



        public void CalculoContratoCoffeBreak(int cantAsis/*asistentes*/, int cantPersA,double valorbase/*valor base tipo de evento*/,ref double valor_final /* variable para obtener el valor total de los calculos*/)
        {
            valor_dia_uf.ObtenerResultadoUf(ref ufvalordia);/*se obtiene el valor uf desde una api y se guarda en la variable*/
             /* luego creamos unas variables double para tener el valor total de asistentes dependiendo del tramo que aparecen en la tabla  y tambien el de personal adicional*/

            double valor_Asistente = 0;
            double valor_personal = 0;

            int CantAsistentes = cantAsis;
            int cantPersonal = cantPersA;

         
            if (CantAsistentes < 0)
            {
                MessageBox.Show("Los Asistentes no pueden ser Negativo");
            }


            if (CantAsistentes > 0 && CantAsistentes<20 )/*esta en este rango la prueba */
            {
                valor_Asistente = 3 * ufvalordia;
            }
            else if (CantAsistentes > 20 && CantAsistentes < 51)
            {
                valor_Asistente = 5 * ufvalordia;

            }
            else if (CantAsistentes > 50)
            {
                valor_Asistente = 7 * ufvalordia;


            }
            else
            {
                CantAsistentes = 0;
                valor_Asistente = 0;
            }

            if (cantPersonal == 2)
            {
                valor_personal = 2 * ufvalordia;

            }
            else if (cantPersonal == 3)
            {
                valor_personal = 3 * ufvalordia;
            }

            else if (cantPersonal == 4)
            {
                valor_personal = 3.5 * ufvalordia;
            }
            else if (cantPersonal > 4)
            {
                cantPersonal = cantPersonal - 4;
                double porcentajeAdicional = 0;
                porcentajeAdicional =   3.5;
                valor_personal = porcentajeAdicional * ufvalordia;

            }
            
            else
            {
                
            }
            if (valor_Asistente > 0 && valor_personal > 0)
            {


                double valorBaseCalculado = valorbase * ufvalordia;
                valor_final = (valor_Asistente) + (valor_personal) + (valorBaseCalculado);
            }
        }
        public void CalculoContratoCocktail(int cantAsis/*asistentes*/, int cantPersA, double valorbase/*valor base tipo de evento*/, ref double valor_final /* variable para obtener el valor total de los calculos*/, bool musicaAmbiental,int tipoAmbientacion)
        {
            valor_dia_uf.ObtenerResultadoUf(ref ufvalordia);/*se obtiene el valor uf desde una api y se guarda en la variable*/
                                                            /* luego creamos unas variables double para tener el valor total de asistentes dependiendo del tramo que aparecen en la tabla  y tambien el de personal adicional*/

            double valor_Asistente = 0;
            double valor_personal = 0;

            int CantAsistentes = cantAsis;
            int cantPersonal = cantPersA;

            int musicaUF = musicaAmbiental ? 1 : 0;



            if (CantAsistentes < 0)
            {
                MessageBox.Show("Los Asistentes no pueden ser Negativo");
            }


            if (CantAsistentes > 0 && CantAsistentes < 20)/*esta en este rango la prueba */
            {
                valor_Asistente = 4 * ufvalordia;
            }
            else if (CantAsistentes > 20 && CantAsistentes < 51)
            {
                valor_Asistente = 6 * ufvalordia;

            }
            else if (CantAsistentes > 50)
            {
                valor_Asistente = 7 * ufvalordia;


            }
            else
            {
                CantAsistentes = 0;
                valor_Asistente = 0;
            }

            if (cantPersonal == 2)
            {
                valor_personal = 2 * ufvalordia;

            }
            else if (cantPersonal == 3)
            {
                valor_personal = 3 * ufvalordia;
            }

            else if (cantPersonal == 4)
            {
                valor_personal = 3.5 * ufvalordia;
            }
            else if (cantPersonal > 4)
            {
                cantPersonal = cantPersonal - 4;
                double porcentajeAdicional = 0;
                porcentajeAdicional = 3.5;
                valor_personal = porcentajeAdicional * ufvalordia;

            }

            else
            {

            }
            if (valor_Asistente > 0 && valor_personal > 0)
            {


                double valorBaseCalculado = (valorbase+musicaUF+tipoAmbientacion) * ufvalordia;
                valor_final = (valor_Asistente) + (valor_personal) + (valorBaseCalculado);
            }
        }

        public void CalculoContratoCena(int cantAsis/*asistentes*/, int cantPersA, double valorbase/*valor base tipo de evento*/, ref double valor_final /* variable para obtener el valor total de los calculos*/, bool musicaAmbiental)
        {
            valor_dia_uf.ObtenerResultadoUf(ref ufvalordia);/*se obtiene el valor uf desde una api y se guarda en la variable*/
                                                            /* luego creamos unas variables double para tener el valor total de asistentes dependiendo del tramo que aparecen en la tabla  y tambien el de personal adicional*/

            double valor_Asistente = 0;
            double valor_personal = 0;

            int CantAsistentes = cantAsis;
            int cantPersonal = cantPersA;

            double musicaUF = musicaAmbiental ? 1.5 : 0;



            if (CantAsistentes < 0)
            {
                MessageBox.Show("Los Asistentes no pueden ser Negativo");
            }


            if (CantAsistentes > 0 && CantAsistentes < 20)/*esta en este rango la prueba */
            {
                valor_Asistente = (CantAsistentes* 1.5) * ufvalordia;
            }
            else if (CantAsistentes > 20 && CantAsistentes < 51)
            {
                valor_Asistente = (CantAsistentes * 1.2) * ufvalordia;

            }
            else if (CantAsistentes > 50)
            {
                valor_Asistente = (CantAsistentes * 1) * ufvalordia;


            }
            else
            {
                CantAsistentes = 0;
                valor_Asistente = 0;
            }

            if (cantPersonal == 2)
            {
                valor_personal = 3 * ufvalordia;

            }
            else if (cantPersonal == 3)
            {
                valor_personal = 4 * ufvalordia;
            }

            else if (cantPersonal == 4)
            {
                valor_personal = 5 * ufvalordia;
            }
            else if (cantPersonal > 4)
            {
                cantPersonal = cantPersonal - 4;
                double porcentajeAdicional = 5;
                porcentajeAdicional = (cantPersonal*0.5)+porcentajeAdicional ;
                valor_personal = porcentajeAdicional * ufvalordia;

            }

            else
            {
              
            }
            if (valor_Asistente > 0 && valor_personal > 0)
            {


                double valorBaseCalculado = (valorbase + musicaUF) * ufvalordia;
                valor_final = (valor_Asistente) + (valor_personal) + (valorBaseCalculado);
            }
        }
    }
}
