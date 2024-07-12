using Business.Utils;
using Microsoft.Extensions.Configuration;

namespace Business.Test
{
    /// <summary>
    /// Realiza las pruebas sobre la clase de persistencia de usuarios
    /// </summary>
    public class CryptoTest
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Llave de cifrado
        /// </summary>
        private readonly string key;

        /// <summary>
        /// Vector de inicialización de cifrado
        /// </summary>
        private string iv;

        /// <summary>
        /// Inicializa la configuración de la prueba
        /// </summary>
        public CryptoTest()
        {
            //Arrange
            configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, false)
                .AddEnvironmentVariables()
                .Build();

            key = configuration["Aes:Key"] ?? "";
            iv = configuration["Aes:IV"] ?? "";
        }

        /// <summary>
        /// Prueba la consulta de un listado de usuarios
        /// </summary>
        [Fact]
        public void Encrypt_Crypto_Ok()
        {
            //Arrange
            string plainText = "Texto plano";
            Crypto crypto = new();

            //Act
            string cipherText = crypto.Encrypt(plainText, key, iv);

            //Assert
            Assert.Equal("uVO7lo4LyQYHUPm8lHKSsA==", cipherText);
        }

        /// <summary>
        /// Prueba la consulta de un listado de usuarios
        /// </summary>
        [Fact]
        public void Decrypt_Crypto_Ok()
        {
            //Arrange
            string cipherText = "uVO7lo4LyQYHUPm8lHKSsA==";
            Crypto crypto = new();

            //Act
            string plainText = crypto.Decrypt(cipherText, key, iv);

            //Assert
            Assert.Equal("Texto plano", plainText);
        }
    }
}