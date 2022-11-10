using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_final
{
    internal class main
    {
        //creamos cocimundo
        Cocimundo cocimundo= new Cocimundo();
        AsignarRecorrido(cocimundo);
        //la lista que más pedidos de linea blanca tiene es la que se le asigna el furgón
        int cont1 = 0, cont2 = 0, cont3 = 0;
        cont1=LineaBlanca(cocimundo.ListaDeRecorrido1);
        cont2=LineaBlanca(cocimundo.ListaDeRecorrido2);
        cont3=LineaBlanca(cocimundo.ListaDeRecorrido3);
        AsignarVehiculo(cont1, cont2, cont3, cocimundo);

        // ANALISIS DE MI RECORRIDO + ALMACENAMIENTO DE LOS PEDIDOS CORRESPONDIENTES AL RECORRIDO 1
        AsignarCostoEnvio(cocimundo.ListaDeRecorrido1, cocimundo.ListaVehiculos[0]);
        List<Pedidos> ListaFinal1 = Ruteo_Por_Recorrido(cocimundo, cocimundo.ListaDeRecorrido1, cocimundo.ListaVehiculos[0], cocimundo.ListaSobrantes);
        // ORGANIZO MI RUTA-> ARMO UNA LISTA CON LOS PEDIDOS EN SU ORDEN DE ENTREGA DEL MÁS CERCANO AL MÁS LEJANO
        List<Pedidos> PedidosACargar1 = new List<Pedidos>();
        ElementosACargar(ListaFinal1, cocimundo.ListaVehiculos[0], cocimundo.ListaSobrantes, PedidosACargar1);
        VerificarPeso(PedidosACargar1, cocimundo.ListaVehiculos[0], cocimundo.ListaSobrantes);
        IniciarReparto(PedidosACargar1);
       

        // ANALISIS DE MI RECORRIDO + ALMACENAMIENTO DE LOS PEDIDOS CORRESPONDIENTES AL RECORRIDO 2
        // siempre seguimos agregando elementos a la lista de sobrantes, es unica, y todos los que no entraron quedan ahi
        // posteriormente si puedo llevarlos en mi camioneta lo haré
        AsignarCostoEnvio(cocimundo.ListaDeRecorrido2, cocimundo.ListaVehiculos[1]);
        List<Pedidos> ListaFinal2 = Ruteo_Por_Recorrido(cocimundo, cocimundo.ListaDeRecorrido2, cocimundo.ListaVehiculos[1], cocimundo.ListaSobrantes);
        // ORGANIZO MI RUTA-> ARMO UNA LISTA CON LOS PEDIDOS EN SU ORDEN DE ENTREGA DEL MÁS CERCANO AL MÁS LEJANO
        List<Pedidos> PedidosACargar2 = new List<Pedidos>();
        ElementosACargar(ListaFinal2, cocimundo.ListaVehiculos[1], cocimundo.ListaSobrantes, PedidosACargar2);
        VerificarPeso(PedidosACargar2, cocimundo.ListaVehiculos[1], cocimundo.ListaSobrantes);
        IniciarReparto(PedidosACargar2);


        // ANALISIS DE MI RECORRIDO + ALMACENAMIENTO DE LOS PEDIDOS CORRESPONDIENTES AL RECORRIDO 3
        // siempre seguimos agregando elementos a la lista de sobrantes, es unica, y todos los que no entraron quedan ahi
        // posteriormente si puedo llevarlos en mi camioneta lo haré
        AsignarCostoEnvio(cocimundo.ListaDeRecorrido3, cocimundo.ListaVehiculos[2]);
        List<Pedidos> ListaFinal3 = Ruteo_Por_Recorrido(cocimundo, cocimundo.ListaDeRecorrido3, cocimundo.ListaVehiculos[2], cocimundo.ListaSobrantes);
        // ORGANIZO MI RUTA-> ARMO UNA LISTA CON LOS PEDIDOS EN SU ORDEN DE ENTREGA DEL MÁS CERCANO AL MÁS LEJANO
        List<Pedidos> PedidosACargar3 = new List<Pedidos>();
        ElementosACargar(ListaFinal3, cocimundo.ListaVehiculos[2], cocimundo.ListaSobrantes, PedidosACargar3);
        VerificarPeso(PedidosACargar3, cocimundo.ListaVehiculos[2], cocimundo.ListaSobrantes);
        IniciarReparto(PedidosACargar3);

        // AL FINALIZAR EL DIA--> los vehículos van a ver recibido un % de daño por el/los recorrido/s hechos, entonces es ahi cuando les sumamos el daño de el/los recorrido/s.
        FinDelDia(cocimundo.ListaVehiculos[0]);
        FinDelDia(cocimundo.ListaVehiculos[1]);
        FinDelDia(cocimundo.ListaVehiculos[2]);
    }
}
