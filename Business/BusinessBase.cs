using Business.Exceptions;
using Dal;
using Dal.Dto;
using Entities;
using System.Text.Json;

namespace Business
{
    /// <summary>
    /// Clase base para el manejo de la capa de negocios de las distintas
    /// entidades del módulo de autenticación y autorización
    /// </summary>
    /// <typeparam name="T">Entidad que se maneja en la capa de negocio</typeparam>
    /// <param name="persistence">Persistencia de la entidad</param>
    /// <param name="logPersistence">Persistencia de los registros en base de datos</param>
    public abstract class BusinessBase<T>(IPersistence<T> persistence, IPersistence<LogDb> logPersistence) : IBussines<T> where T : IEntity
    {
        /// <summary>
        /// Administrador de persistencia de la entidad
        /// </summary>
        protected IPersistence<T> _persistence = persistence;

        /// <summary>
        /// Administrador de persistencia de la entidad
        /// </summary>
        protected IPersistence<LogDb> _logPersistence = logPersistence;

        /// <summary>
        /// Trae un listado de entidades desde la base de datos
        /// </summary>
        /// <param name="filters">Filtros aplicados a la consulta</param>
        /// <param name="orders">Ordenamientos aplicados a la base de datos</param>
        /// <param name="limit">Límite de registros a traer</param>
        /// <param name="offset">Corrimiento desde el que se cuenta el número de registros</param>
        /// <returns>Listado de entidades</returns>
        /// <exception cref="BusinessException">Si hubo una excepción al consultar las entidades</exception>
        public ListResult<T> List(string filters, string orders, int limit, int offset)
        {
            try
            {
                return _persistence.List(filters, orders, limit, offset);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al listar objetos de la clase " + typeof(T).Name, ex);
            }
        }

        /// <summary>
        /// Consulta una entidad dado su identificador
        /// </summary>
        /// <param name="entity">Entidad a consultar</param>
        /// <returns>Entidad con los datos cargados desde la base de datos o por defecto si no lo pudo encontrar</returns>
        /// <exception cref="BusinessException">Si hubo una excepción al consultar la entidad</exception>
        public T Read(T entity)
        {
            try
            {
                return _persistence.Read(entity);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al consultar un objeto de la clase " + typeof(T).Name, ex);
            }
        }

        /// <summary>
        /// Inserta una entidad en la base de datos
        /// </summary>
        /// <param name="entity">Entidad a insertar</param>
        /// <param name="user">Usuario que realiza la inserción</param>
        /// <returns>Entidad insertada con el id generado por la base de datos</returns>
        /// <exception cref="BusinessException">Si hubo una excepción al insertar la entidad</exception>
        public T Insert(T entity, User user)
        {
            try
            {
                T result = _persistence.Insert(entity);
                _logPersistence.Insert(new LogDb()
                {
                    Action = 'I',
                    Table = _persistence.GetTableName(),
                    TableId = _persistence.GetEntityId(),
                    User = user,
                    Values = JsonSerializer.Serialize(entity)
                });
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al insertar un objeto de la clase " + typeof(T).Name, ex);
            }
        }

        /// <summary>
        /// Actualiza una entidad en la base de datos
        /// </summary>
        /// <param name="entity">Entidad a actualizar</param>
        /// <param name="user">Usuario que realiza la actualización</param>
        /// <returns>Entidad actualizada</returns>
        /// <exception cref="BusinessException">Si hubo una excepción al actualizar la entidad</exception>
        public T Update(T entity, User user)
        {
            try
            {
                T result = _persistence.Update(entity);
                _logPersistence.Insert(new LogDb()
                {
                    Action = 'U',
                    Table = _persistence.GetTableName(),
                    TableId = _persistence.GetEntityId(),
                    User = user,
                    Values = JsonSerializer.Serialize(entity)
                });
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al actualizar una objeto de la clase " + typeof(T).Name, ex);
            }
        }

        /// <summary>
        /// Elimina una entidad de la base de datos
        /// </summary>
        /// <param name="entity">Entidad a eliminar</param>
        /// <param name="user">Usuario que realiza la eliminación</param>
        /// <returns>Entidad eliminada</returns>
        /// <exception cref="BusinessException">Si hubo una excepción al eliminar la entidad</exception>
        public T Delete(T entity, User user)
        {
            try
            {
                T result = _persistence.Delete(entity);
                _logPersistence.Insert(new LogDb()
                {
                    Action = 'D',
                    Table = _persistence.GetTableName(),
                    TableId = _persistence.GetEntityId(),
                    User = user,
                    Values = JsonSerializer.Serialize(entity)
                });
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al eliminar un objeto de la clase " + typeof(T).Name, ex);
            }
        }
    }
}
