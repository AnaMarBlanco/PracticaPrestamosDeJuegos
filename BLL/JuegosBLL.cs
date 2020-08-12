using Microsoft.EntityFrameworkCore;
using PracticaPrestamosDeJuegos.DAL;
using PracticaPrestamosDeJuegos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PracticaPrestamosDeJuegos.BLL
{
    class JuegosBLL
    {
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool paso = false;
            try
            {
                paso = contexto.Juegos.Any(e => e.JuegoId == id);
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

        public static bool Guardar(Juegos Juego)
        {//me escucha? 
            if (Juego.JuegoId == 0)
                return Insertar(Juego);
            else
            {
                if (Existe(Juego.JuegoId))
                {
                    return Modificar(Juego);
                }
                else
                    return false;
            }
        }

        private static bool Insertar(Juegos Juego)
        {
            Contexto contexto = new Contexto();
            bool paso = false;
            try
            {
                if (contexto.Juegos.Add(Juego) != null) { paso = (contexto.SaveChanges() > 0); }
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

        public static bool Modificar(Juegos Juego)
        {
            Contexto contexto = new Contexto();
            bool paso = false;
            try
            {
                contexto.Entry(Juego).State = EntityState.Modified;
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
                var Juego = contexto.Juegos.Find(id);
                if (Juego != null)
                {
                    contexto.Juegos.Remove(Juego);
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

        public static Juegos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Juegos Juego = new Juegos();
            try
            {
                Juego = contexto.Juegos.Find(id);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Juego;
        }

        public static List<Juegos> GetList()
        {
            Contexto contexto = new Contexto();
            List<Juegos> Lista = new List<Juegos>();
            try
            {
                Lista = contexto.Juegos.ToList();
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

        public static List<Juegos> GetList(Expression<Func<Juegos, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Juegos> Lista = new List<Juegos>();
            try
            {
                Lista = contexto.Juegos.Where(criterio).ToList();
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
