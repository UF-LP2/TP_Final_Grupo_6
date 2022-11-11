using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_final
{
    internal class Ruteo
    {
        public void AsignarRecorrido(Cocimundo cocimundo)
        {
            int j = 0;
            while (j < cocimundo.ListaPedidos.Count)
            {
                if (cocimundo.ListaPedidos[j].cliente.Direccion == "1" || cocimundo.ListaPedidos[j].cliente.Direccion == "0")
                //la direccion pertenece al recorrido 1  o es de Liniers
                {
                    cocimundo.ListaDeRecorrido1.Add(cocimundo.ListaPedidos[j]);//guardo ese pedido en mi sublista del recorrido 1

                }
                else if (cocimundo.ListaPedidos[j].cliente.Direccion == "2")
                //la direccion pertenece al recorrido 2
                {
                    cocimundo.ListaDeRecorrido2.Add(cocimundo.ListaPedidos[j]);//guardo ese pedido en mi sublista del recorrido 2
                }
                else
                //la direccion pertenece al recorrido 3
                {
                    cocimundo.ListaDeRecorrido3.Add(cocimundo.ListaPedidos[j]);//guardo ese pedido en mi sublista del recorrido 3
                }
                j++;
            }

        }
        public void AsignarDistanciaLiniers(List<Pedidos>ListaPedidos)
        {
            for(int i=0;i<ListaPedidos.Count;i++)
            {
                ListaPedidos[i].cliente.distancia_a_Liniers = DistanciaALiniers(ListaPedidos[i].cliente.Barrioaux);
            }
        }
        public List<Pedidos> Ruteo_Por_Recorrido(Cocimundo cocimundo, List<Pedidos> ListaDePedidos, Vehiculos vehiculo, List<Pedidos> ListaSobrantes)
        {
            int ContadorNafta = 0;
            Pedidos auxiliar; // una variable auxiliar del tipo pedido
            List<Pedidos> ListaAux = new List<Pedidos>();
            ListaAux[1] = null;
            ListaAux[0] = cocimundo.deposito; //Deposito seria la direccion del donde está el deposito el primero en la lista del ruteo será Liniers que es de donde partimos, no sumamos distancia
            ListaAux[1] = Distancias(ListaAux[0], ListaDePedidos, vehiculo);//la ciudad que este mas cerca al deposito la visito primero 
            vehiculo.kmPorViaje = ListaAux[1].cliente.distancia_a_Liniers; // agregamos los km que hay a la ciudad más cercana
            for (int i = 2; i < ListaAux.Count; i++)
            {
                auxiliar = Distancias(ListaAux[ListaAux.Count], ListaDePedidos, vehiculo);
                if (auxiliar == ListaAux[i - 1]) // significa que no puedo entregar mas pedidos porque no me alcanza la nafta
                {
                    ContadorNafta++; // lo usamos para tener una condición a la hora de copiarnos la lista y eso, porque si ponemos el for directamente lo hara sin importar si la nafta me alcanzo o no
                    break; // entonces dejo de recorrer la lista 
                }
                else
                {
                    ListaAux.Add(auxiliar); // lo guardo en mi lista de pedidos, ya que la nafta me alcanza 
                }
            }
            if (ContadorNafta == 1)//significa que no me alcanzo la nafta por ende me guardo los pedidos que siguen en la lista en una auxiliar, para hacerlos en otra instancia
            {
                for (int k = 0; k < ListaSobrantes.Count; k++)
                {
                    for (int j = k; j < ListaDePedidos.Count; j++)
                    {
                        ListaSobrantes[k] = ListaDePedidos[j];
                        return ListaAux; // retorno la lista del ruteo, y me guardo en mi listasobrantes los pedidos que no he podido entregar
                    }
                }
            }
            // es la lista final del ruteo, la cual  esta ordenada de forma tal de ahorrar en el consumo de nafta
            return ListaAux;
        }
        public Pedidos Distancias(Pedidos Pedido, List<Pedidos> ListaPedidos, Vehiculos vehiculo)
        {
            int ContadorNafta = 0;
            Pedidos ProxPedido = null; //es un elemento auxiliar del tipo pedido
            if (ListaPedidos.Count == 1)// significa que llegue al ultimo pedido de mi recorrido
            {
                ProxPedido = ListaPedidos[0]; // es el único elemento por ende es el primero de mi lista
                double distancia_mas_Cercano = Math.Sqrt(Math.Pow(Pedido.cliente.distancia_a_Liniers, 2) + Math.Pow(ProxPedido.cliente.distancia_a_Liniers, 2)); // vuelvo a calcular la distancia entre mi nodo actual y el elegido como mi proximo pedido
                vehiculo.kmPorViaje += (float)distancia_mas_Cercano;
                if (!VerificarNafta(vehiculo)) // si la nafta de mi vehículo no me alcanza
                {
                    ContadorNafta++;
                    vehiculo.kmPorViaje = vehiculo.kmPorViaje - (float)distancia_mas_Cercano; // retrocedo al momento previo de añadir ese pedido a mi recorrido
                    ProxPedido = Pedido; // retorno el anterior a él
                    return ProxPedido;
                }
                else
                {
                    ListaPedidos.RemoveAt(ProxPedido.ID);//la borramos porque ya pertenece a la ruta de entrega, y pasara a ser mi nuevo nodo de actual
                    return ProxPedido;
                }
            }
            for (int i = 0; i < ListaPedidos.Count(); i++) // recorro la lista de los pedidos que me quedan por hacer
            {

                if (i == 0)
                {
                    ProxPedido = ListaPedidos[i];
                }
                double distancia = Math.Sqrt(Math.Pow(Pedido.cliente.distancia_a_Liniers, 2) + Math.Pow(ListaPedidos[i].cliente.distancia_a_Liniers, 2)); //calculamos la distancia entre mi nodo  actual y los destinos que me quedan por recorrer
                double distancia_ProxPedido = Math.Sqrt(Math.Pow(ProxPedido.cliente.distancia_a_Liniers, 2) + Math.Pow(ListaPedidos[i].cliente.distancia_a_Liniers, 2));
                if (distancia < distancia_ProxPedido)// el de menor distancia pasará a ocupar el lugar de ProxPedido, al terminar de recorrer la lista tendre mi proximo pedido al que debo ir
                {
                    ProxPedido = ListaPedidos[i];

                }//guardo la ciudad a menor distancia de mi ultima ciudad visitada
            }
            double distancia_del_mas_cercano = Math.Sqrt(Math.Pow(Pedido.cliente.distancia_a_Liniers, 2) + Math.Pow(ProxPedido.cliente.distancia_a_Liniers, 2)); // vuelvo a calcular la distancia entre mi nodo actual y el elegido como mi proximo pedido
            vehiculo.kmPorViaje = vehiculo.kmPorViaje + (float)distancia_del_mas_cercano; //sumo los km que tengo que hacer para ir hasta ese próximo pedido
            if (!VerificarNafta(vehiculo))// si la nafta de mi vehículo no me alcanza
            {
                ContadorNafta++;
                vehiculo.kmPorViaje -= (float)distancia_del_mas_cercano; // retrocedo al momento previo de añadir ese pedido a mi recorrido
                ProxPedido = Pedido; // devuelvo el anterior a él
                return ProxPedido;
            }
            else
            {
                // el tamanio de la lista y sus componentes, van siendo modificados en cada iteracion de la función distancia llamada en ruteo!
                ListaPedidos.RemoveAt(ProxPedido.ID);//la borramos porque ya pertenece a la ruta de entrega, y pasara a ser mi nuevo nodo de actual
                return ProxPedido; //devuelvo el proximo a ir
            }
        }
        public bool VerificarNafta(Vehiculos vehiculo) // vehiculo es un elemento del tipo vehiculo
        {
            float kilometros = (vehiculo.consumoNafta * vehiculo.tanqueNafta) / 100;//los kilometros que puedo recorrer con un tanque de nafta
            if (vehiculo.kmPorViaje <= kilometros * 2)//lo multiplico x2 para tener en cuenta la vuelta
            {
                return true; // si me alcanzan los litros  de nafta para hacer los km de ese recorrido

            }
            else
            {
                return false;
            }
        }
        //ListaPedidosRecorrido es una lista de elementos del tipo pedido, cocimundo es un elemento del tipo Cocimundo
        public int LineaBlanca(List<Pedidos> ListaPedidos) // retorna la cantidad de pedidos que son de linea blanca en un recorrido
        {
            int contador = 0;
            for (int i = 0; i < ListaPedidos.Count; i++)
            {
                for (int j = 0; j < ListaPedidos[i].ListaDeArticulos.Count; j++)
                {
                    if (ListaPedidos[i].ListaDeArticulos[i].CategoriaPedido == TipoLineaPedido.LineaBlanca)
                    {
                        contador++;
                    }
                }
            }
            return contador;
        }
       public void AsignarVehiculoARecorrido(int cont1, int cont2, int cont3, Cocimundo cocimundo) // funcion que compara los contadores de linea blanca
        {
            if (cont1 > cont3 && cont1 > cont2)
            {
                cocimundo.ListaVehiculos[0].Vehiculo = TipoVehiculo.furgon;
                if (cont2 < cont3)
                {
                    cocimundo.ListaVehiculos[1].Vehiculo = TipoVehiculo.camioneta;
                    cocimundo.ListaVehiculos[2].Vehiculo = TipoVehiculo.furgoneta;
                }
                else
                {
                    cocimundo.ListaVehiculos[1].Vehiculo = TipoVehiculo.furgoneta;
                    cocimundo.ListaVehiculos[2].Vehiculo = TipoVehiculo.camioneta;
                }
            }

            if (cont2 > cont3 && cont2 > cont1)
            {
                cocimundo.ListaVehiculos[1].Vehiculo = TipoVehiculo.furgon;
                if (cont1 < cont3)
                {
                    cocimundo.ListaVehiculos[0].Vehiculo = TipoVehiculo.camioneta;
                    cocimundo.ListaVehiculos[2].Vehiculo = TipoVehiculo.furgoneta;
                }
                else if (cont1 > cont3)
                {
                    cocimundo.ListaVehiculos[2].Vehiculo = TipoVehiculo.camioneta;
                    cocimundo.ListaVehiculos[0].Vehiculo = TipoVehiculo.furgoneta;
                }

            }
            if (cont3 > cont2 && cont3 > cont1)
            {
                cocimundo.ListaVehiculos[1].Vehiculo = TipoVehiculo.furgon;
                if (cont1 < cont2)
                {
                    cocimundo.ListaVehiculos[0].Vehiculo = TipoVehiculo.camioneta;
                    cocimundo.ListaVehiculos[1].Vehiculo = TipoVehiculo.furgoneta;
                }
                else
                {
                    cocimundo.ListaVehiculos[1].Vehiculo = TipoVehiculo.camioneta;
                    cocimundo.ListaVehiculos[0].Vehiculo = TipoVehiculo.furgoneta;
                }
            }

        }
        public void AsignarCostoEnvio(List<Pedidos> ListaPedidos, Vehiculos vehiculo)
        {
            if (vehiculo.Vehiculo == TipoVehiculo.camioneta)
            {
                for (int i = 0; i < ListaPedidos.Count(); i++)
                {
                    ListaPedidos[i].CostoEnvio = Constantes.CostoEnvioCamioneta;
                }

            }
            if (vehiculo.Vehiculo == TipoVehiculo.furgoneta)
            {
                for (int i = 0; i < ListaPedidos.Count; i++)
                {
                    ListaPedidos[i].CostoEnvio = Constantes.CostoEnvioFurgoneta;
                }

            }
            else // el envio se hara en el furgón
            {
                for (int i = 0; i < ListaPedidos.Count; i++)
                {
                    ListaPedidos[i].CostoEnvio = Constantes.CostoEnvioFurgon;
                }
            }
        }
        public int DistanciaALiniers(Distancia barrios)
        {
            int d = 0;
            switch(barrios)
            {
                case Distancia.Chacarita:
                    d = 23;break;
                case Distancia.LaBoca:
                    d = 25;break;
                case Distancia.PuertoMadero:
                    d = 18; break;
                case Distancia.Flores:
                    d = 17; break;
                case Distancia.Caballito:
                    d = 15; break;
                case Distancia.Retiro:
                    d = 27; break;
                case Distancia.Palermo:
                    d = 14; break;
                case Distancia.Belgrano:
                    d = 20; break;
                case Distancia.VillaUrquiza:
                    d = 13; break;
                case Distancia.VillaDevoto:
                    d = 12; break;
                case Distancia.VillaLugano:
                    d = 10; break;
                case Distancia.ParqueAvellaneda:
                    d = 9; break;
                case Distancia.VelezSarfield:
                    d = 8; break;
                case Distancia.MonteCastro:
                    d = 7; break;
                case Distancia.Mataderos:
                    d = 4; break;
                case Distancia.VillaLuro:
                    d = 2; break;
                case Distancia.Versalles:
                    d = 3; break;
                case Distancia.Avellaneda:
                    d = 21; break;
                case Distancia.Lanus:
                    d = 19; break;
                case Distancia.LomasDeZamora:
                    d = 18; break;
                case Distancia.LaMatanza:
                    d = 5; break;
                case Distancia.VicenteLopez:
                    d = 17; break;
                case Distancia.SanMartin:
                    d = 11; break;
                case Distancia.TresDeFebrero:
                    d = 6; break;
                case Distancia.Liniers:
                    d = 0; break;
                case Distancia.Floresta:
                    d = 5; break;
                case Distancia.VillaReal:
                    d = 5; break;
                case Distancia.VillaDelParque:
                    d = 9; break;
                case Distancia.VillaSantaRita:
                    d =7; break;
                case Distancia.VillaGralMitre:
                    d = 8; break;
                case Distancia.LaPaternal:
                    d = 13; break;
                case Distancia.VillaCrespo:
                    d = 12; break;
                case Distancia.Agronomia:
                    d = 10; break;
                case Distancia.ParqueChas:
                    d = 12; break;
                case Distancia.VillaUrtuzar:
                    d = 18; break;
                case Distancia.VillaPueyrredon:
                    d = 10; break;
                case Distancia.Coghlan:
                    d = 16; break;
                case Distancia.Saavedra:
                    d = 14; break;
                case Distancia.Nuñez:
                    d = 18; break;
                case Distancia.Colegiales:
                    d = 19; break;
                case Distancia.Recoleta:
                    d = 27; break;
                case Distancia.Almagro:
                    d = 15; break;
                case Distancia.ParqueChacabuco:
                    d = 12; break;
                case Distancia.VillaRiachuelo:
                    d = 10; break;
                case Distancia.VillaSoldati:
                    d = 10; break;
                case Distancia.NuevaPompeya:
                    d = 11; break;
                case Distancia.Boedo:
                    d = 13; break;
                case Distancia.Barracas:
                    d = 19; break;
                case Distancia.ParquePatricios:
                    d = 17; break;
                case Distancia.Constitucion:
                    d = 17; break;
                case Distancia.SanTelmo:
                    d = 17; break;
                case Distancia.SanNicolas:
                    d = 19; break;
                case Distancia.Montserrat:
                    d = 17; break;
                case Distancia.Balvanera:
                    d = 17; break;
                case Distancia.SanCristobal:
                    d = 14; break;
                case Distancia.SanIsidro:
                    d = 27; break;
                case Distancia.Ituzaingo:
                    d = 19; break;
                case Distancia.Hurlingham:
                    d = 14; break;
                case Distancia.Moron:
                    d = 14; break;
            }
            return d;
        }
    }
}