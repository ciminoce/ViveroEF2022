using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using ViveroEF2022.Context;
using ViveroEF2022.Entities;

namespace ViveroEF2022.Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continua = true;
            do
            {

                Console.Clear();
                Console.WriteLine("0-Salir del sistema");
                Console.WriteLine("1-Listas las Plantas");
                Console.WriteLine("2-Lista los tipos de envases");
                Console.WriteLine("3-Listas los tipos de plantas");
                Console.WriteLine("4-Asignar Envases aleatorios");
                Console.WriteLine("5-Agregar tipo de envase");
                Console.WriteLine("6-Consultar las plantas por tipo de envase");
                Console.WriteLine("7-Borrar un tipo de envase");
                Console.WriteLine("8-Proyectar en objeto anónimo");
                Console.WriteLine("9-Mostrar plantas precios inferiores a uno ingresado");
                Console.WriteLine("10-Mostrar Paginado");
                Console.WriteLine("11-Agregar un tipo de planta");
                Console.WriteLine("12-Agregar una nueva Planta");
                Console.WriteLine("13-Agrupar por tipo");
                Console.WriteLine("14-Datos estadísticos por tipo de Envase");
                Console.WriteLine("15-Estadisticos mayores a 30 registros");
                Console.WriteLine("16-Primeros 10 mayores a un valor");
                Console.WriteLine("17-Todas menores a un valor random");
                Console.WriteLine("18-Alguna igual a un valor random");
                Console.WriteLine("19-Inferiores al precio promedio");
                Console.WriteLine("20-Mostrar mientras precio inferior a 100");
                Console.WriteLine("21-Saltar los precios menores a 100");
                var opcion = int.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 0:
                        continua = false;
                        break;
                    case 1:
                        ListarPlantas();
                        break;
                    case 2:
                        ListarTiposDeEnvases();
                        break;
                    case 3:
                        ListarTipoDePlantas();
                        break;
                    case 4:
                        AsignarEnvasesAleatorio();
                        break;
                    case 5:
                        AgregarTipoDeEnvase();
                        break;
                    case 6:
                        ConsultarPlantasPorTipoDeEnvase();
                        break;
                    case 7:
                        BorrarTipoDeEnvase();
                        break;
                    case 8:
                        ProyectarEnObjetoAnonimo();
                        break;
                    case 9:
                        MostrarPreciosMenoresEnPlantas();
                        break;
                    case 10:
                        MostrarPlantasPaginado();
                        break;
                    case 11:
                        AgregarTipoDePlanta();
                        break;
                    case 12:
                        AgregarNuevaPlanta();
                        break;
                    case 13:
                        AgruparPorTipo();
                        break;
                    case 14:
                        EstadisticaPorTipoEnvase();
                        break;
                    case 15:
                        EstadisticasPorTipoEnvaseMasDe30();
                        break;
                    case 16:
                        Primeros10Mayores();
                        break;
                    case 17:
                        TodasMenoresARandom();
                        break;
                    case 18:
                        AlgunaIgualARandom();
                        break;
                    case 19:
                        InferioresAlPrecioPromedio();
                        break;
                    case 20:
                        MostrarMientrasInferior100();
                        break;
                    case 21:
                        SaltarMientrasInferior100();
                        break;
                }
            } while (continua);


        }

        private static void SaltarMientrasInferior100()
        {
            using (var context = new ViveroDbContext())
            {
                var query = context.Plantas.OrderBy(p => p.PrecioVenta)
                    .ToList();
                var plantas = query.SkipWhile(p => p.PrecioVenta < 100)
                .ToList();
                foreach (var planta in plantas)
                {
                    Console.WriteLine($"{planta.Descripcion} {planta.PrecioVenta}");
                }
            }

            Console.ReadLine();
        }

        private static void MostrarMientrasInferior100()
        {
            using (var context = new ViveroDbContext())
            {
                var query = context.Plantas.ToList();
                var menores = query.TakeWhile(p => p.PrecioVenta < 300)
                    .ToList();
                Console.WriteLine($"{menores.Count()}");
                foreach (var planta in menores)
                {
                    Console.WriteLine($"{planta.Descripcion} {planta.PrecioVenta}");
                }

            }

            Console.ReadLine();
        }

        private static void InferioresAlPrecioPromedio()
        {
            using (var context = new ViveroDbContext())
            {
                var precioPromedio = context.Plantas.Average(p => p.PrecioVenta);
                Console.WriteLine($"Precio promedio= {precioPromedio}");
                var query = context.Plantas.Where(p => p.PrecioVenta < precioPromedio).ToList();
                foreach (var planta in query)
                {
                    Console.WriteLine($"{planta.Descripcion} {planta.PrecioVenta}");
                }
            }

            Console.ReadLine();
        }

        private static void AlgunaIgualARandom()
        {
            var r = new Random();
            var valor = r.Next(50, 61);
            using (var context = new ViveroDbContext())
            {
                var query = context.Plantas.Any(p => p.PrecioVenta == valor);
                if (query)
                {
                    var plantas = context.Plantas
                        .Where(p => p.PrecioVenta == valor)
                        .ToList();
                    foreach (var planta in plantas)
                    {
                        Console.WriteLine($"{planta.Descripcion} {planta.PrecioVenta}");
                    }
                }
                else
                {
                    Console.WriteLine($"No hay ninguna planta con el precio igual a {valor}");
                }
            }
            Console.ReadLine();
        }

        private static void TodasMenoresARandom()
        {
            var r = new Random();
            var valor = r.Next(250, 3000);
            using (var context = new ViveroDbContext())
            {
                var query = context.Plantas.All(p => p.PrecioVenta < valor);
                if (query)
                {
                    Console.WriteLine($"Todas menores a {valor}");
                }
                else
                {
                    Console.WriteLine($"No todas son menores a {valor}");
                    var plantas = context.Plantas.Where(p => p.PrecioVenta >= valor).ToList();
                    foreach (var planta in plantas)
                    {
                        Console.WriteLine($"{planta.Descripcion} {planta.PrecioVenta}");
                    }
                }

            }

            Console.ReadLine();
        }

        private static void Primeros10Mayores()
        {
            Console.Write("Ingrese un precio para comparar:");
            var precio = decimal.Parse(Console.ReadLine());
            using (var context = new ViveroDbContext())
            {
                var query = context.Plantas.Where(p => p.PrecioVenta > precio)
                    .OrderBy(p => p.PrecioVenta)
                    .Take(10)
                    .ToList();
                foreach (var planta in query)
                {
                    Console.WriteLine($"{planta.Descripcion} {planta.PrecioVenta}");
                }

                Console.ReadLine();

            }
        }

        private static void EstadisticasPorTipoEnvaseMasDe30()
        {
            using (var context = new ViveroDbContext())
            {
                var grupo = context.Plantas.Include(p => p.TipoDeEnvase.Descripcion)
                    .GroupBy(g => g.TipoDeEnvase.Descripcion)
                    .Where(g => g.Count() > 30)
                    .Select(s => new
                    {
                        Envase = s.Key,
                        Cantidad = s.Count()
                    })
                    .ToList();
                foreach (var g in grupo)
                {
                    Console.WriteLine($"{g.Envase} {g.Cantidad}");
                }

                Console.ReadLine();
            }
        }

        private static void EstadisticaPorTipoEnvase()
        {
            using (var context = new ViveroDbContext())
            {
                var grupo = context.Plantas.Include(p => p.TipoDeEnvase)
                    .GroupBy(g => g.TipoDeEnvase.Descripcion)
                    .OrderBy(g => g.Key)
                    .Select(s => new
                    {
                        Envase = s.Key,
                        Cantidad = s.Count(),
                        MayorPrecio = s.Max(se => se.PrecioVenta),
                        MenorPrecio = s.Min(se => se.PrecioVenta)
                    }).ToList();
                foreach (var gr in grupo)
                {
                    Console.WriteLine($"{gr.Envase} {gr.Cantidad} {gr.MenorPrecio} {gr.MayorPrecio}");
                }

                Console.ReadLine();
            }
        }

        private static void AgruparPorTipo()
        {
            using (var context = new ViveroDbContext())
            {
                var grupo = context.Plantas.Include(p => p.TipoDePlanta)
                    .GroupBy(g => g.TipoDePlanta.Descripcion).ToList();
                foreach (var gr in grupo)
                {
                    Console.WriteLine($"Tipo de Planta: {gr.Key}");
                    foreach (var p in gr)
                    {
                        Console.WriteLine($"{p.Descripcion} {p.PrecioVenta}");
                    }
                    Console.WriteLine($"Cantidad: {gr.Count()}");
                    Console.Write("Presione enter para continuar");
                    Console.ReadLine();
                }
            }
            Console.ReadLine();
        }

        private static void AgregarNuevaPlanta()
        {
            Console.Write("Ingrese la descripción de la planta:");
            var nuevaDesc = Console.ReadLine();
            Console.Write("Ingrese el precio:");
            var nuevoPrecio = decimal.Parse(Console.ReadLine());
            var nuevoEnvase = SeleccionarEnvase();
            var nuevoTipo = SeleccionarTipo();

            var nuevaPlanta = new Planta()
            {
                Descripcion = nuevaDesc,
                PrecioVenta = nuevoPrecio,
                TipoDeEnvaseId = nuevoEnvase,
                TipoDePlantaId = nuevoTipo
            };

            using (var context = new ViveroDbContext())
            {
                if (!context.Plantas.Any(p => p.Descripcion == nuevaPlanta.Descripcion))
                {
                    context.Plantas.Add(nuevaPlanta);
                    context.SaveChanges();
                    Console.WriteLine("Registro agregado");
                }
                else
                {
                    Console.WriteLine($"Error registro existente {nuevaPlanta.Descripcion}");
                }

                Console.ReadLine();

            }


        }

        private static int SeleccionarTipo()
        {
            ListarTipoDePlantas();
            int tipoSeleccionado = 0;
            do
            {
                Console.Write("Seleccione un tipo de planta:");
                tipoSeleccionado = int.Parse(Console.ReadLine());
                using (var context = new ViveroDbContext())
                {
                    var tipoEnDb = context.TiposDePlantas
                        .SingleOrDefault(tp => tp.TipoDePlantaId == tipoSeleccionado);
                    if (tipoEnDb == null)
                    {
                        Console.WriteLine("ERROR al seleccionar el tipo de planta");
                    }
                    else
                    {
                        break;
                    }
                }
            } while (true);

            return tipoSeleccionado;
        }

        private static int SeleccionarEnvase()
        {
            ListarTiposDeEnvases();
            int tipoSeleccionado = 0;
            do
            {
                Console.Write("Seleccione un tipo de envase:");
                tipoSeleccionado = int.Parse(Console.ReadLine());
                using (var context = new ViveroDbContext())
                {
                    var tipoEnDb = context.TiposDeEnvases
                        .SingleOrDefault(tp => tp.TipoDeEnvaseId == tipoSeleccionado);
                    if (tipoEnDb == null)
                    {
                        Console.WriteLine("ERROR al seleccionar el tipo de envase");
                    }
                    else
                    {
                        break;
                    }
                }
            } while (true);

            return tipoSeleccionado;

        }

        private static void ListarTipoDePlantas()
        {
            Console.Clear();
            using (var context = new ViveroDbContext())
            {
                var lista = context.TiposDePlantas.ToList();
                foreach (var tipoDePlanta in lista)
                {
                    Console.WriteLine($"{tipoDePlanta.TipoDePlantaId}-{tipoDePlanta.Descripcion}");
                }
            }

            Console.ReadLine();
        }

        private static void ListarTiposDeEnvases()
        {
            //Console.Clear();
            using (var context = new ViveroDbContext())
            {
                var lista = context.TiposDeEnvases.ToList();
                foreach (var tipoDeEnvase in lista)
                {
                    Console.WriteLine($"{tipoDeEnvase.TipoDeEnvaseId}-{tipoDeEnvase.Descripcion}");
                }
            }

            Console.ReadLine();
        }

        private static void AgregarTipoDePlanta()
        {
            using (var context = new ViveroDbContext())
            {
                Console.Write("Ingrese el tipo de planta:");
                var tipoDePlantaIngresada = Console.ReadLine();
                var nuevoTipoPlanta = new TipoDePlanta()
                {
                    Descripcion = tipoDePlantaIngresada
                };
                if (!context.TiposDePlantas.Any(tp => tp.Descripcion == nuevoTipoPlanta.Descripcion))
                {

                    context.TiposDePlantas.Add(nuevoTipoPlanta);
                    context.SaveChanges();
                    Console.WriteLine("Registro agregado");


                }
                else
                {
                    Console.WriteLine($"ERROR: ya existe el tipo de planta {nuevoTipoPlanta.Descripcion}");
                }
                Console.ReadLine();
            }
        }

        private static void MostrarPlantasPaginado()
        {
            Console.Write("Ingrese la cantidad de registros por página:");
            var cantidadPorPagina = int.Parse(Console.ReadLine());
            using (var context = new ViveroDbContext())
            {
                var paginas = CalcularPaginas(cantidadPorPagina, context.Plantas.Count());
                for (int pagina = 0; pagina < paginas; pagina++)
                {
                    Console.WriteLine($"Página: {pagina}");
                    Console.WriteLine("------------Detalle ------");
                    var detallePlantas = context.Plantas
                        .OrderBy(p => p.Descripcion)
                        .Skip(pagina * cantidadPorPagina)
                        .Take(cantidadPorPagina).ToList();
                    foreach (var planta in detallePlantas)
                    {
                        Console.WriteLine($"{planta.Descripcion} {planta.PrecioVenta}");
                    }
                    Console.WriteLine("Presione para continuar");
                    Console.ReadLine();
                }
            }
        }

        private static int CalcularPaginas(int cantidadPorPagina, int cantidadPlantas)
        {
            if (cantidadPorPagina > cantidadPlantas)
            {
                return 1;
            }
            else if (cantidadPlantas % cantidadPorPagina > 0)
            {
                return cantidadPlantas / cantidadPorPagina + 1;
            }
            else
            {
                return cantidadPlantas / cantidadPorPagina;
            }
        }

        private static void MostrarPreciosMenoresEnPlantas()
        {
            Console.Write("Ingrese un precio:");
            var precio = decimal.Parse(Console.ReadLine());
            using (var context = new ViveroDbContext())
            {
                //var lista = context.Plantas.Where(p => p.PrecioVenta < precio).Take(10).ToList();
                var lista = context.Plantas.Where(p => p.PrecioVenta < precio)
                    .OrderBy(p => p.PrecioVenta)
                    .Take(10).ToList();

                foreach (var planta in lista)
                {
                    Console.WriteLine($"{planta.Descripcion} {planta.PrecioVenta}");
                }
                Console.WriteLine($"Cantidad registros: {lista.Count()}");
                Console.ReadLine();
            }
        }

        private static void ProyectarEnObjetoAnonimo()
        {
            using (var context = new ViveroDbContext())
            {
                var lista = context.Plantas
                    .Include(p => p.TipoDePlanta)
                    .Select(p => new
                    {
                        NombrePlanta = p.Descripcion,
                        TipoPlanta = p.TipoDePlanta.Descripcion
                    }).ToList();
                foreach (var registro in lista)
                {
                    Console.WriteLine($"{registro.NombrePlanta} {registro.TipoPlanta}");
                }

                Console.ReadLine();
            }
        }


        private static void BorrarTipoDeEnvase()
        {
            using (var context = new ViveroDbContext())
            {
                var listaEnvases = context.TiposDeEnvases.ToList();
                do
                {
                    Console.Clear();
                    Console.WriteLine("Lista de envases");
                    foreach (var tipoDeEnvase in listaEnvases)
                    {
                        Console.WriteLine($"{tipoDeEnvase.TipoDeEnvaseId}-{tipoDeEnvase.Descripcion}");
                    }


                    Console.Write("Seleccione tipo de envase (0 para salir ):");
                    var tipoEnvase = int.Parse(Console.ReadLine());
                    if (tipoEnvase == 0)
                    {
                        return;
                    }
                    var tipoEnvaseInDb = context.TiposDeEnvases.SingleOrDefault(te => te.TipoDeEnvaseId == tipoEnvase);
                    if (tipoEnvaseInDb != null)
                    {
                        context.TiposDeEnvases.Remove(tipoEnvaseInDb);
                        context.SaveChanges();

                        Console.WriteLine("Registro borrado");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Envase no existente....");
                    }
                } while (true);

            }

            Console.ReadLine();

        }

        private static void ConsultarPlantasPorTipoDeEnvase()
        {
            using (var context = new ViveroDbContext())
            {
                var listaEnvases = context.TiposDeEnvases.ToList();
                do
                {
                    Console.Clear();
                    Console.WriteLine("Lista de envases");
                    foreach (var tipoDeEnvase in listaEnvases)
                    {
                        Console.WriteLine($"{tipoDeEnvase.TipoDeEnvaseId}-{tipoDeEnvase.Descripcion}");
                    }


                    Console.Write("Seleccione tipo de envase (0 para salir ):");
                    var tipoEnvase = int.Parse(Console.ReadLine());
                    if (tipoEnvase == 0)
                    {
                        return;
                    }
                    var tipoEnvaseInDb = context.TiposDeEnvases.SingleOrDefault(te => te.TipoDeEnvaseId == tipoEnvase);
                    if (tipoEnvaseInDb != null)
                    {
                        var listaDePlantasPorEnvase = context.Plantas
                            .Where(p => p.TipoDeEnvaseId == tipoEnvaseInDb.TipoDeEnvaseId).ToList();
                        foreach (var planta in listaDePlantasPorEnvase)
                        {
                            Console.WriteLine($"{planta.Descripcion}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Envase no existente....");
                    }
                    Console.ReadLine();
                } while (true);

            }


        }

        private static void AgregarTipoDeEnvase()
        {
            using (var context = new ViveroDbContext())
            {
                var envaseNuevo = new TipoDeEnvase()
                {
                    Descripcion = "Borrame"
                };
                context.TiposDeEnvases.Add(envaseNuevo);
                context.SaveChanges();
                Console.WriteLine("Envase agregado");
            }
        }

        private static void AsignarEnvasesAleatorio()
        {
            using (var context = new ViveroDbContext())
            {
                var lista = context.Plantas.Include(p => p.TipoDeEnvase).ToList();
                Random r = new Random();
                foreach (var planta in lista)
                {
                    planta.TipoDeEnvaseId = r.Next(1, 5);
                }
                context.SaveChanges();
                foreach (var planta in lista)
                {
                    Console.WriteLine($"{planta.Descripcion} {planta.TipoDeEnvase.Descripcion}");
                }

            }

            Console.ReadLine();
        }

        private static void ListarPlantas()
        {
            using (var context = new ViveroDbContext())
            {
                var lista = context.Plantas.ToList();
                foreach (var planta in lista)
                {
                    Console.WriteLine($"{planta.Descripcion}");
                }
            }

            Console.ReadLine();
        }
    }
}
