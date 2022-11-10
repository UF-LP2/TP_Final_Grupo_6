using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


namespace tp_final
{
    public class Almacenamiento
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

            for (int j = 0; j < Pedido.ListaDeArticulos.Count; j++)
            {
                peso += Pedido.ListaDeArticulos[j].Peso;
            }

            return peso; //retorno el peso de este pedido
        }
        public int[] Beneficio(List<Pedidos> ListaPedidos)
        {
            int[] beneficios = new int[ListaPedidos.Count];
            for (int i = 0; i < ListaPedidos.Count; i++)
            {
                if (ListaPedidos[i].Pedido == TipoPedido.express)
                {
                    beneficios[i] = (int)VolumenPedidos(ListaPedidos[i]) / (int)PesoPedidos(ListaPedidos[i]) + 100; //mayor prioridad, le sumo 100
                }
                else if (ListaPedidos[i].Pedido == TipoPedido.normal)
                {
                    beneficios[i] = (int)VolumenPedidos(ListaPedidos[i]) / (int)PesoPedidos(ListaPedidos[i]) + 50;//pedidos normales tienen prioridad media
                }
                else
                {
                    beneficios[i] = (int)VolumenPedidos(ListaPedidos[i]) / (int)PesoPedidos(ListaPedidos[i]);//pedidos diferidos < prioridad
                }
            }
            return beneficios;
        }
        public void ElementosACargar(List<Pedidos> ListaPedidos, Vehiculos vehiculo, List<Pedidos> ListaSobrantes, List<Pedidos> PedidosACargar)
        {
            int i, j;
            int[] beneficios = Beneficio(ListaPedidos);
            float contpeso = 0;

            int[,] matriz = new int[ListaPedidos.Count + 1, (int)vehiculo.volumenDeCarga + 1]; //creamos la matriz

            //matriz[i][j] nos da el maximo valor i de pedidos que se pueden cargar con capacidad de volumen j

            //inicializamos matriz[0][j] =0 para todo 0≤j≤V

            for (j = 0; j <= vehiculo.volumenDeCarga; j++)

            {
                matriz[0, j] = 0;
            }
            //inicializamos matriz[i][0] para 0≤i≤N. No puedo agregar mas pedidos si no tengo lugar

            for (i = 0; i <= ListaPedidos.Count; i++)

            { matriz[i, 0] = 0; }


            //llenamos la matriz de forma ascendente

            for (i = 1; i < ListaPedidos.Count; i++)
            {
                for (j = 0; j <= vehiculo.volumenDeCarga; j++)
                {
                    //verificamos si el peso del articulo i es menor o igual a la capacidad del volumen, lo tomo como máximo una vez incluyendo el articulo actual y otra vez sin incluirlo (me fijo cuando tengo max beneficio)

                    if (VolumenPedidos(ListaPedidos[i - 1]) <= j && contpeso + PesoPedidos(ListaPedidos[i - 1]) <= vehiculo.pesoMaxDeCarga)
                    {
                        contpeso += PesoPedidos(ListaPedidos[i - 1]);
                        matriz[i, j] = Math.Max(beneficios[i - 1] + matriz[i - 1, j - (int)VolumenPedidos(ListaPedidos[i - 1])], matriz[i - 1, j]);

                    }
                    else //no se puede incluir el elemento actual
                    {
                        matriz[i, j] = matriz[i - 1, j];
                    }
                }
            }
            for(i= ListaPedidos.Count; i<0;i--)
            {
                for(j=(int)vehiculo.volumenDeCarga;j<0;j--)
                {
                    if (matriz[i, j] == matriz[i-1,j])
                    {
                        ListaSobrantes.Add(ListaPedidos[i]);
                    }
                    else
                    {
                        PedidosACargar.Add(ListaPedidos[i]);
                        ListaPedidos.RemoveAt(i);
                    }
                }
            }
            //una vez cargados los pedidos que ENTRAN en el camion, los ordenamos por orden de cercania entre nodos

        }
        /*public void VerificarPeso(List<Pedidos> PedidosACargar, Vehiculos vehiculo, List<Pedidos> ListaSobrantes)
        {
            List<Pedidos> ListaAux;

            float peso = 0;
            int i = 0;
            while (peso <= vehiculo.pesoMaxDeCarga)
            {
                peso += PesoPedidos(PedidosACargar[i]);
                i++;
            }
            for (int k = i; k < PedidosACargar.Count; k++)
            {
                ListaSobrantes.Add(PedidosACargar[k]);
                PedidosACargar.Remove(PedidosACargar[k]);
            }//guardo los pedidos que no entran en la lista de pedidos que se mandan en el siguiente viaje y los que entran los paso a la lista a cargar

        }*/
        public void IniciarReparto(List<Pedidos> PedidosACargar, Vehiculos vehiculo)
        {
            for (int i = 0; i < PedidosACargar.Count; i++)
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
