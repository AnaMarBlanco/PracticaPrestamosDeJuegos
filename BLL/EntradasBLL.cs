using Microsoft.EntityFrameworkCore;
using PracticaPrestamosDeJuegos.BLL;
using PracticaPrestamosDeJuegos.DAL;
using PracticaPrestamosDeJuegos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PracticaPrestamosDeJuegoss.BLL
{
    public class EntradasBLL
    {
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool paso = false;
            try
            {
                paso = contexto.Entradas.Any(e => e.EntradaId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static bool Guardar(Entradas Entrada)
        {
            if (Entrada.EntradaId == 0)
                return Insertar(Entrada);
            else
            {
                if (Existe(Entrada.EntradaId))
                {
                    return Modificar(Entrada);
                }
                else
                    return false;
            }
        }

        private static bool Insertar(Entradas Entrada)
        {
            Juegos Juego;
            Contexto contexto = new Contexto();
            bool paso = false;
            try
            {
                if (contexto.Entradas.Add(Entrada) != null) {
                    Juego = JuegosBLL.Buscar(Entrada.JuegoId);
                    Juego.Existencia += Entrada.Existencia;
                    JuegosBLL.Guardar(Juego);
                    paso = (contexto.SaveChanges() > 0); 
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static bool Modificar(Entradas Entrada)
        {
            Contexto contexto = new Contexto();
            bool paso = false;
            try
            {
                
                var anterior = EntradasBLL.Buscar(Entrada.EntradaId);
                var Juego = JuegosBLL.Buscar(anterior.JuegoId);
                int cant = anterior.Existencia;
                Juego.Existencia -= cant;
                JuegosBLL.Guardar(Juego);
                
                var Juego1 = JuegosBLL.Buscar(Entrada.JuegoId);
                Juego1.Existencia = Entrada.Existencia;
                JuegosBLL.Guardar(Juego1);
                contexto.Entry(Entrada).State = EntityState.Modified;
                paso = (contexto.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            Contexto contexto = new Contexto();
            bool paso = false;
            try
            {
                var Entrada = contexto.Entradas.Find(id);
                if (Entrada != null)
                {
                    contexto.Entradas.Remove(Entrada);
                    paso = (contexto.SaveChanges() > 0);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static Entradas Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Entradas Entrada = new Entradas();
            try
            {
                Entrada = contexto.Entradas.Find(id);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Entrada;
        }

        public static List<Entradas> GetList()
        {
            Contexto contexto = new Contexto();
            List<Entradas> Lista = new List<Entradas>();
            try
            {
                Lista = contexto.Entradas.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Lista;
        }

        public static List<Entradas> GetList(Expression<Func<Entradas, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Entradas> Lista = new List<Entradas>();
            try
            {
                Lista = contexto.Entradas.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Lista;
        }
    }
}
