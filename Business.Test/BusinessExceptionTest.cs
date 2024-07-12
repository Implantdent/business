using Business.Exceptions;

namespace Dal.Test
{
    public class BusinessExceptionTest
    {
        /// <summary>
        /// Realiza la prueba de creación de BusinessException por defecto
        /// </summary>
        [Fact]
        public void CreateBasic_PersistentException_Ok()
        {
            //Arrange
            BusinessExceptionTest ex = new();
            //Act

            //Assert
            Assert.IsType<BusinessExceptionTest>(ex);
        }

        /// <summary>
        /// Realiza la prueba de creación de BusinessException con mensaje
        /// </summary>
        [Fact]
        public void CreateWithMessage_BusinessException_Ok()
        {
            //Arrange
            BusinessException ex = new("Prueba");
            //Act

            //Assert
            Assert.IsType<BusinessException>(ex);
        }

        /// <summary>
        /// Realiza la prueba de creación de BusinessException con mensaje y exepción interna
        /// </summary>
        [Fact]
        public void CreateInnerException_BusinessException_Ok()
        {
            //Arrange
            BusinessException ex = new("Prueba", new Exception());
            //Act

            //Assert
            Assert.IsType<BusinessException>(ex);
        }
    }
}