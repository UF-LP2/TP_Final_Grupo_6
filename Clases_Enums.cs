using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Constantes
{
    public const int CostoEnvioCamioneta=50;
    public const int CostoEnvioFurgoneta=50;
    public const int CostoEnvioFurgon=50;
}
public enum TipoPedido
{
    express = 1, normal = 3, diferido = 4
}
public enum TipoVehiculo
{
    camioneta, furgoneta, furgon
}
public enum TipoLineaPedido
{
    Electrodomesticos, LineaBlanca, Electronicos, Televisores
}
public enum TipoArticulo
{
    heladera, lavarropa, cocina, calefon, termotanque, secarropa, microondas, freezer, licuadora, exprimidor,
    rallador, cafetera, molinillo, computadora, impresora, televisor, celular
}
public enum Barrio
{
    Liniers = 0, Mataderos = 2, ParqueAvellaneda = 2, VillaLuro = 1, VelezSarfield = 1, Floresta = 1, MonteCastro = 1, Versalles = 3, VillaReal = 3, VillaDevoto = 3, VillaDelParque = 1, VillaSantaRita = 1,
    VillaGralMitre = 1, LaPaternal = 1, VillaCrespo = 1, Chacarita = 1, Agronomia = 1, ParqueChas = 1, VillaUrtuzar = 1, VillaPueyrredon = 3, VillaUrquiza = 3, Coghlan = 3, Saavedra = 3, Nuñez = 3, Belgrano = 1, Colegiales = 1,
    Palermo = 1, Recoleta = 1, Almagro = 1, Caballito = 1, ParqueChacabuco = 1, Flores = 1, VillaLugano = 2, VillaRiachuelo = 2, VillaSoldati = 2, NuevaPompeya = 2, Boedo = 1, Barracas = 2, ParquePatricios = 2, LaBoca = 2,
    Constitucion = 1, SanTelmo = 1, PuertoMadero = 1, SanNicolas = 1, Montserrat = 1, Balvanera = 1, SanCristobal, Retiro = 1, SanIsidro = 3, LaMatanza = 3, TresDeFebrero = 3, VicenteLopez = 3, Ituzaingo = 3,
    SanMartin = 3, Hurlingham = 3, Moron = 3, LomasDeZamora = 1, Lanus = 2, Avellaneda = 2
} // le asignamos 1, 2 o 3 dependiendo del recorrido al que pertenezca 

public class Cocimundo
{
    public string nombre;
    public Pedidos deposito;
    public List<Vehiculos> ListaVehiculos = new List<Vehiculos>(3);
    public List<Pedidos> ListaPedidos = new List<Pedidos>();
    // las siguientes listas estan inicialmente vacias
    public List<Pedidos> ListaDeRecorrido1 = new List<Pedidos>();
    public List<Pedidos> ListaDeRecorrido2 = new List<Pedidos>();
    public List<Pedidos> ListaDeRecorrido3 = new List<Pedidos>();
    
}
public class Pedidos
{
    public int ID; // identificador del pedido, se genera automaticamente de forma random
    public TipoPedido Pedido;// express normal o diferido
    public Clientes cliente; // puntero al cliente que ordeno ese pedido
    public List<Articulos> ListaDeArticulos; // es una Lista de tipo articulos !!// vamos a tener una funciòn que nos  retorne el volumen total del pedido, porque tiene que entregarse todo en una misma tirada-->volumenTotal()
    // va a tener una funcion que devuelve el peso total de todos los articulos de ese pedido--> PesoTotal();
    public bool enviado; // se setea en true si fue enviado, sino se pone en false
    public int CostoEnvio; //se setea dependiendo de que vehiculo lo lleve

}
public class Vehiculos
{
    public TipoVehiculo Vehiculo;//enum que vehiculo es 
    public float pesoMaxDeCarga; //camioneta=1200-15kg de la rampa- furgoneta=3500-15kg de la rampa- furgon= 4900 (kg)
    public float volumenDeCarga;//camioneta=5.4937-0.02 de la rampa- furgoneta=17-0.02 de la rampa- furgon=18 (m3)
    public float consumoNafta;//camioneta=6.1- furgoneta=6.9- furgon=8.9(l/100km)
    public float tanqueNafta;// camioneta =50litros- furgoneta= 90litros- furgon=90 litros 
    public float danio = 0; // se incrementa con los viajes
    public float kmPorViaje; //se los pasamos de func distancia
    public int cantViajes; // cantidad de viajes que se hicieron, ya que si es una camioneta puede hacer hasta 4 viajes por día
}

public class Articulos
{
    public TipoLineaPedido CategoriaPedido;
    public TipoArticulo TipoDeArticulo;//enum con todos los articulos
    public float Peso; // peso en kg del producto teniendo en cuenta su envoltorio TipoLineaPedido CategoriaPedido; // si pertenece a linea blanca, etc
    public float largo; //Volumen es un enum donde definimos los volumenes d cada articulo
    public float ancho;
    public float alto;
   
  
    // vamos a tener una funcion que calcula el volumen total
}
public class Clientes
{
    public string nombre;
    public string apellido;
    public string DNI;
    public string Direccion;
    public float distancia_a_Liniers;
    private Barrio Barrio;
}