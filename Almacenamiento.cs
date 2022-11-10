using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_final
{
    internal class Almacenamiento
    {
        public float VolumenPedidos(Pedidos Pedido)
        {
            float vol = 0;
            for (int j = 0; j < Pedido.ListaDeArticulos.Count; j++)
            {
                vol = Pedido.ListaDeArticulos[j].ancho * Pedido.ListaDeArticulos[j].largo * Pedido.ListaDeArticulos[j].alto;
            }

            return vol; //retorno el volumen de este pedido
        }
        public float PesoPedidos(Pedidos Pedido)
        {
            float peso = 0;
            
                for(int j = 0; j < Pedido.ListaDeArticulos.Count;j++)
                {
                    peso += Pedido.ListaDeArticulos[j].Peso;
                }
         
            return peso; //retorno el peso de este pedido
        }
        public void ElementosACargar(List<Pedidos> ListaPedidos, Vehiculos vehiculo, List<Pedidos> ListaSobrantes,List<Pedidos> PedidosACargar)
        {
            int i = 0;
            float j = vehiculo.volumenDeCarga;
            Pedidos[,] matriz = new Pedidos[(int)vehiculo.volumenDeCarga, ListaPedidos.Count];
            for(i=0;i<ListaPedidos.Count-1;i++)
            {
                int k;
                for (k=i;k<ListaPedidos.Count-1;k++)
                {
                    if (VolumenPedidos(ListaPedidos[i]) / PesoPedidos(ListaPedidos[i]) >VolumenPedidos(ListaPedidos[k + 1]) / PesoPedidos(ListaPedidos[k + 1])
                    {
                        Pedidos Aux = ListaPedidos[i];
                    }
                }
        
            }
            //TODO cambiar este codigo
            while(j<=0)//mientras que el volumen del pedido entre en el vehiculo, lo cargo, sino lo paso a la lista de pedidos que se entregaran en la prox salida
            {
                if (VolumenPedidos(ListaPedidos[i]) < j)
                {
                    j -= VolumenPedidos(ListaPedidos[i]);
                    PedidosACargar.Add(ListaPedidos[i]);
                    ListaPedidos.Remove(ListaPedidos[i]);
                }
                else
                    ListaSobrantes.Add(ListaPedidos[i]);
                ListaPedidos.Remove(ListaPedidos[i]);
                i++;
            }

        }
        public void VerificarPeso(List<Pedidos> PedidosACargar,Vehiculos vehiculo,List<Pedidos>ListaSobrantes)
        {
            List<Pedidos> ListaAux;

            float peso=0;
            int i = 0;
            while(peso<=vehiculo.pesoMaxDeCarga)
            {
                peso += PesoPedidos(PedidosACargar[i]);
                i++;
            }
            for(int k=i;k<PedidosACargar.Count;k++)
            {
                ListaSobrantes.Add(PedidosACargar[k]);
                PedidosACargar.Remove(PedidosACargar[k]);
            }//guardo los pedidos que no entran en la lista de pedidos que se mandan en el siguiente viaje y los que entran los paso a la lista a cargar
    
        }
        public void IniciarReparto(List<Pedidos>PedidosACargar,Vehiculos vehiculo)
        {
            for(int i=0;i<PedidosACargar.Count;i++)
            {
                PedidosACargar[i].enviado = true; //modificamos el estado de entrega
                vehiculo.cantViajes++;
            }
        }
        public void FinDelDia(Vehiculos vehiculo)
        {
            vehiculo.danio += (float)0.1;
            //le incrementamos el daño respecto de la depreciacion anual del vehiculo
        }
    }
}
