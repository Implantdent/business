using Business.Business;
using Business.Exceptions;
using Business.Utils;
using Dal;
using Dal.Dto;
using Dal.Exceptions;
using Dal.Persistences;
using Entities;
using Moq;

namespace Business.Test
{
    /// <summary>
    /// Realiza las pruebas sobre la clase de persistencia de usuarios
    /// </summary>
    public class UserTest
    {
        /// <summary>
        /// Capa de negocio de los usuarios
        /// </summary>
        private readonly UserBusiness _business;

        /// <summary>
        /// Inicializa la configuración de la prueba
        /// </summary>
        public UserTest()
        {
            //Arrange
            Mock<IUserPersistence> mock = new();
            Mock<IPersistence<LogDb>> mockLog = new();
            Mock<ICrypto> mockCrypto = new();

            mock.Setup(p => p.List("", It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new ListResult<User>([], 0));

            mock.Setup(p => p.List("error", It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws<BusinessException>();

            mock.Setup(p => p.Read(It.IsAny<User>()))
                .Returns((User user) =>
                {
                    if (user.Id == -1)
                    {
                        throw new PersistentException();
                    }
                    return user;
                });

            mock.Setup(p => p.Insert(It.IsAny<User>()))
                .Returns((User user) =>
                {
                    if (user.Id == -1)
                    {
                        throw new PersistentException();
                    }
                    else
                    {
                        return user;
                    }
                });

            mock.Setup(p => p.Update(It.IsAny<User>()))
                .Returns((User user) =>
                {
                    if (user.Id == -1)
                    {
                        throw new PersistentException();
                    }
                    return user;
                });

            mock.Setup(p => p.Delete(It.IsAny<User>()))
                .Returns((User user) =>
                {
                    if (user.Id == -1)
                    {
                        throw new PersistentException();
                    }
                    return user;
                });

            mock.Setup(p => p.ReadByEmailAndPassword(It.IsAny<User>(), It.IsAny<string>()))
                .Returns((User user, string password) =>
                {
                    if (user.Id == -1)
                    {
                        throw new PersistentException();
                    }
                    return user;
                });

            mock.Setup(p => p.ReadByEmail(It.IsAny<User>()))
                .Returns((User user) =>
                {
                    if (user.Email == "error")
                    {
                        throw new PersistentException();
                    }
                    return user;
                });

            mock.Setup(p => p.UpdatePassword(It.IsAny<User>(), It.IsAny<string>()))
                .Returns((User user, string password) =>
                {
                    if (user.Id == -1)
                    {
                        throw new PersistentException();
                    }
                    return user;
                });

            mock.Setup(p => p.ListRoles("", It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<User>()))
                .Returns(new ListResult<Role>([], 0));

            mock.Setup(p => p.ListRoles("error", It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<User>()))
                .Throws<PersistentException>();

            mock.Setup(p => p.ListNotRoles(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<User>()))
                .Returns((string filters, string orders, int limit, int offset, User user) =>
                {
                    if (user.Id == -1)
                    {
                        throw new PersistentException();
                    }
                    return new ListResult<Role>([], 0);
                });

            mock.Setup(p => p.InsertRole(It.IsAny<Role>(), It.IsAny<User>())).
                Returns((Role role, User user) =>
                {
                    if (user.Id == -1)
                    {
                        throw new PersistentException();
                    }
                    else
                    {
                        return role;
                    }
                });

            mock.Setup(p => p.DeleteRole(It.IsAny<Role>(), It.IsAny<User>())).
                Returns((Role role, User user) =>
                {
                    if (user.Id == -1)
                    {
                        throw new PersistentException();
                    }
                    return role;
                });

            mockLog.Setup(p => p.Insert(It.IsAny<LogDb>())).
                Returns(new LogDb());

            mockCrypto.Setup(p => p.Encrypt(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).
                Returns((string input, string key, string iv) =>
                {
                    return "";
                });

            _business = new(mock.Object, mockLog.Object, mockCrypto.Object);
        }

        /// <summary>
        /// Prueba la consulta de un listado de usuarios
        /// </summary>
        [Fact]
        public void List_User_Ok()
        {
            //Arrange

            //Act
            ListResult<User> list = _business.List("", "", 0, 0);

            //Assert
            Assert.Equal(0, list.Total);
        }

        /// <summary>
        /// Prueba la consulta de un listado de usuarios con error
        /// </summary>
        [Fact]
        public void List_User_Exception()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<BusinessException>(() => _business.List("error", "", 0, 0));
        }

        /// <summary>
        /// Prueba la lectura de un usuario
        /// </summary>
        [Fact]
        public void List_Read_Ok()
        {
            //Arrange
            User user = new();

            //Act
            user = _business.Read(user);

            //Assert
            Assert.Equal(0, user.Id);
        }

        /// <summary>
        /// Prueba la lectura de un usuario con error
        /// </summary>
        [Fact]
        public void List_Read_Exception()
        {
            //Arrange
            User user = new()
            {
                Id = -1
            };

            //Act

            //Assert
            Assert.Throws<BusinessException>(() => _business.Read(user));
        }

        /// <summary>
        /// Prueba la inserción de un usuario
        /// </summary>
        [Fact]
        public void List_Insert_Ok()
        {
            //Arrange
            User user = new();

            //Act
            user = _business.Insert(user, new User());

            //Assert
            Assert.Equal(0, user.Id);
        }

        /// <summary>
        /// Prueba la inserción de un usuario con error
        /// </summary>
        [Fact]
        public void List_Insert_Exception()
        {
            //Arrange
            User user = new()
            {
                Id = -1
            };

            //Act

            //Assert
            Assert.Throws<BusinessException>(() => _business.Insert(user, new User()));
        }

        /// <summary>
        /// Prueba la actualización de un usuario
        /// </summary>
        [Fact]
        public void List_Update_Ok()
        {
            //Arrange
            User user = new();

            //Act
            user = _business.Update(user, new User());

            //Assert
            Assert.Equal(0, user.Id);
        }

        /// <summary>
        /// Prueba la actualización de un usuario con error
        /// </summary>
        [Fact]
        public void List_Update_Exception()
        {
            //Arrange
            User user = new()
            {
                Id = -1
            };

            //Act

            //Assert
            Assert.Throws<BusinessException>(() => _business.Update(user, new User()));
        }

        /// <summary>
        /// Prueba la eliminación de un usuario
        /// </summary>
        [Fact]
        public void List_Delete_Ok()
        {
            //Arrange
            User user = new();

            //Act
            user = _business.Delete(user, new User());

            //Assert
            Assert.Equal(0, user.Id);
        }

        /// <summary>
        /// Prueba la eliminación de un usuario con error
        /// </summary>
        [Fact]
        public void List_Delete_Exception()
        {
            //Arrange
            User user = new()
            {
                Id = -1
            };

            //Act

            //Assert
            Assert.Throws<BusinessException>(() => _business.Delete(user, new User()));
        }


        /// <summary>
        /// Prueba la consulta de un usuario dado su login y contraseña
        /// </summary>
        [Fact]
        public void ReadByEmailAndPassword_User_Ok()
        {
            //Arrange
            User user = new();

            //Act
            user = _business.ReadByEmailAndPassword(user, "", "", "");

            //Assert
            Assert.Equal(0, user.Id);
        }

        /// <summary>
        /// Prueba la consulta de un usuario que no existe dado su login y password
        /// </summary>
        [Fact]
        public void ReadByEmailAndPassword_User_Exception()
        {
            //Arrange
            User user = new() { Id = -1 };

            //Act

            //Assert
            Assert.Throws<BusinessException>(() => _business.ReadByEmailAndPassword(user, "", "", ""));
        }

        /// <summary>
        /// Prueba la consulta de un usuario dado su login
        /// </summary>
        [Fact]
        public void ReadByEmail_User_Ok()
        {
            //Arrange
            User user = new() { Email = "test1@test.com" };

            //Act
            user = _business.ReadByEmail(user);

            //Assert
            Assert.Equal(0, user.Id);
        }

        /// <summary>
        /// Prueba la consulta de un usuario dado su login con error de persistencia
        /// </summary>
        [Fact]
        public void ReadByEmail_User_Exception()
        {
            //Act, Assert
            Assert.Throws<BusinessException>(() => _business.ReadByEmail(new() { Email = "error" }));
        }

        /// <summary>
        /// Prueba la actualización de la contraseña de un usuario
        /// </summary>
        [Fact]
        public void UpdatePassword_User_Ok()
        {
            //Arrange
            User user = new() { Id = 0, Email = "leandrobaena@gmail.com" };

            //Act
            _ = _business.UpdatePassword(user, "", "", "", new() { Id = 1 });

            //Assert
            Assert.Equal(0, user.Id);
        }

        /// <summary>
        /// Prueba la actualización de la contraseña de un usuario con error de persitencia
        /// </summary>
        [Fact]
        public void UpdatePassword_User_Exception()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<BusinessException>(() => _business.UpdatePassword(new() { Id = -1 }, "", "", "", new() { Id = 1 }));
        }

        /// <summary>
        /// Prueba la consulta de un listado de roles de un usuario con filtros, ordenamientos y límite
        /// </summary>
        [Fact]
        public void ListRoles_User_Ok()
        {
            //Arrange

            //Act
            ListResult<Role> list = _business.ListRoles("", "", 10, 0, new() { Id = 1 });

            //Assert
            Assert.Empty(list.List);
        }

        /// <summary>
        /// Prueba la consulta de un listado de roles de un usuario con filtros, ordenamientos y límite y con errores
        /// </summary>
        [Fact]
        public void ListRoles_User_Exception()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<BusinessException>(() => _business.ListRoles("error", "", 10, 0, new() { Id = 1 }));
        }

        /// <summary>
        /// Prueba la consulta de un listado de roles no asignados a un usuario con filtros, ordenamientos y límite
        /// </summary>
        [Fact]
        public void ListNotRoles_User_Ok()
        {
            //Arrange

            //Act
            ListResult<Role> list = _business.ListNotRoles("", "", 10, 0, new() { Id = 1 });

            //Assert
            Assert.Empty(list.List);
        }

        /// <summary>
        /// Prueba la consulta de un listado de roles no asociados a un usuario con filtros, ordenamientos y límite y con errores
        /// </summary>
        [Fact]
        public void ListNotRoles_User_Exception()
        {
            //Arrange

            //Act

            //Assert
            _ = Assert.Throws<BusinessException>(() => _business.ListNotRoles("", "", 10, 0, new() { Id = -1 }));
        }

        /// <summary>
        /// Prueba la inserción de un rol de un usuario
        /// </summary>
        [Fact]
        public void InsertRole_User_Ok()
        {
            //Arrange

            //Act
            Role role = _business.InsertRole(new() { Id = 4 }, new() { Id = 1 }, new() { Id = 1 });

            //Assert
            Assert.NotEqual(0, role.Id);
        }

        /// <summary>
        /// Prueba la inserción de un rol de un usuario duplicado
        /// </summary>
        [Fact]
        public void InsertRole_User_Exception()
        {
            //Arrange

            //Act

            //Assert
            _ = Assert.Throws<BusinessException>(() => _business.InsertRole(new() { Id = 1 }, new() { Id = -1 }, new() { Id = 1 }));
        }

        /// <summary>
        /// Prueba la eliminación de un rol de un usuario
        /// </summary>
        [Fact]
        public void DeleteRole_User_Ok()
        {
            //Arrange

            //Act
            _ = _business.DeleteRole(new() { Id = 2 }, new() { Id = 1 }, new() { Id = 1 });
            ListResult<Role> list = _business.ListRoles("", "", 10, 0, new() { Id = 1 });

            //Assert
            Assert.Equal(0, list.Total);
        }

        /// <summary>
        /// Prueba la eliminación de un rol de un usuario con error de persistencia
        /// </summary>
        [Fact]
        public void DeleteRole_User_Exception()
        {
            //Act Assert
            Assert.Throws<BusinessException>(() => _business.DeleteRole(new() { Id = 1 }, new() { Id = -1 }, new() { Id = 1 }));
        }
    }
}