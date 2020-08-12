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
    class AmigosBLL
    {
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool paso = false;
            try
            {
                paso = contexto.Amigos.Any(e => e.AmigoId == id);
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

        public static bool Guardar(Amigos Amigo)
        {//me escucha? 
            if(Amigo.AmigoId==0)
            return Insertar(Amigo);
            else
            {
                if (Existe(Amigo.AmigoId))
                {
                    return Modificar(Amigo);
                }
                else
                    return false;
            }
        }

        private static bool Insertar(Amigos Amigo)
        {
            Contexto contexto = new Contexto();
            bool paso = false;
            try
            {
                if (contexto.Amigos.Add(Amigo) != null) { paso = (contexto.SaveChanges() > 0); }
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

        public static bool Modificar(Amigos Amigo)
        {
            Contexto contexto = new Contexto();
            bool paso = false;
            try
            {
                contexto.Entry(Amigo).State = EntityState.Modified;
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
                var Amigo = contexto.Amigos.Find(id);
                if (Amigo != null)
                {
                    contexto.Amigos.Remove(Amigo);
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

        public static Amigos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Amigos Amigo = new Amigos();
            try
            {
                Amigo = contexto.Amigos.Find(id);
               
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Amigo;
        }

        public static List<Amigos> GetList()
        {
            Contexto contexto = new Contexto();
            List<Amigos> Lista = new List<Amigos>();
            try
            {
                Lista = contexto.Amigos.ToList();
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

        public static List<Amigos> GetList(Expression<Func<Amigos, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Amigos> Lista = new List<Amigos>();
            try
            {
                Lista = contexto.Amigos.Where(criterio).ToList();
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
