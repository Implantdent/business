using Business.Exceptions;
using Business.Utils;
using Dal;
using Dal.Dto;
using Dal.Persistences;
using Entities;
using System.Text.Json;

namespace Business.Business
{
    /// <summary>
    /// Capa de negocio para el manejo de usuarios de la aplicación
    /// </summary>
    /// <param name="persistence">Persistencia de los usuarios</param>
    /// <param name="logDbPersistence">Persistencia de logs de base de datos</param>
    public class UserBusiness(IPersistence<User> persistence, IPersistence<LogDb> logDbPersistence, ICrypto crypto) : BusinessBase<User>(persistence, logDbPersistence), IBussinesUser
    {
        /// <summary>
        /// Utilidad de criptografía
        /// </summary>
        private readonly ICrypto _crypto = crypto;

        public User ReadByEmailAndPassword(User entity, string password, string key, string iv)
        {
            try
            {
                return ((IUserPersistence)_persistence).ReadByEmailAndPassword(entity, _crypto.Decrypt(password, key, iv));
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al consultar un usuario por su login y contraseña", ex);
            }
        }

        public User ReadByEmail(User entity)
        {
            try
            {
                return ((IUserPersistence)_persistence).ReadByEmail(entity);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al consultar un usuario por su login", ex);
            }
        }

        public User UpdatePassword(User entity, string password, string key, string iv, User user)
        {
            try
            {
                User result = ((IUserPersistence)_persistence).UpdatePassword(entity, _crypto.Decrypt(password, key, iv));
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
                throw new BusinessException("Error al actualizar la contrseña de un usuario", ex);
            }
        }

        public ListResult<Role> ListRoles(string filters, string orders, int limit, int offset, User user)
        {
            try
            {
                return ((IUserPersistence)_persistence).ListRoles(filters, orders, limit, offset, user);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al lsitar los roles asociados a un usuario", ex);
            }
        }

        public ListResult<Role> ListNotRoles(string filters, string orders, int limit, int offset, User user)
        {
            try
            {
                return ((IUserPersistence)_persistence).ListNotRoles(filters, orders, limit, offset, user);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al listar los roles no asociados a un usuario", ex);
            }
        }

        public Role InsertRole(Role role, User user, User user1)
        {
            try
            {
                Role result = ((IUserPersistence)_persistence).InsertRole(role, user);
                _logPersistence.Insert(new LogDb()
                {
                    Action = 'I',
                    Table = "UserRole",
                    TableId = 0,
                    User = user1,
                    Values = JsonSerializer.Serialize(role) + " - " + JsonSerializer.Serialize(user)
                });
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al insertar un rol asociado con un usuario", ex);
            }
        }

        public Role DeleteRole(Role role, User user, User user1)
        {
            try
            {
                Role result = ((IUserPersistence)_persistence).DeleteRole(role, user);
                _logPersistence.Insert(new LogDb()
                {
                    Action = 'D',
                    Table = "UserRole",
                    TableId = 0,
                    User = user1,
                    Values = JsonSerializer.Serialize(role) + " - " + JsonSerializer.Serialize(user)
                });
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al eliminar un rol asociado con un usuario", ex);
            }
        }
    }
}
