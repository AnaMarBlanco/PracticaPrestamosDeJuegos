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
    class PrestamosBLL
    {
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool paso = false;
            try
            {
                paso = contexto.Prestamos.Any(e => e.PrestamoId == id);
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

        public static bool Guardar(Prestamos Prestamo)
        {
            return Insertar(Prestamo);
        }

        private static bool Insertar(Prestamos Prestamo)
        {
            Contexto contexto = new Contexto();
            bool paso = false;
            try
            {
                if (contexto.Prestamos.Add(Prestamo) != null) { paso = (contexto.SaveChanges() > 0); }
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

        public static bool Modificar(Prestamos Prestamo)
        {
            Contexto contexto = new Contexto();
            bool paso = false;
            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete FROM ProductosDetalle Where TareaId={Prestamo.PrestamoId}");
                foreach (var item in Prestamo.Detalles)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }
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
                var Prestamo = contexto.Prestamos.Find(id);
                if (Prestamo != null)
                {
                    contexto.Prestamos.Remove(Prestamo);
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

        public static Prestamos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Prestamos Prestamo = new Prestamos();
            try
            {
                Prestamo = contexto.Prestamos.Include(x => x.Detalles)
                    .Where(x => x.PrestamoId == id)
                    .SingleOrDefault();
               
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Prestamo;
        }

        public static List<Prestamos> GetList(Expression<Func<Prestamos, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Prestamos> Lista = new List<Prestamos>();
            try
            {
                Lista = contexto.Prestamos.Where(criterio).ToList();
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
