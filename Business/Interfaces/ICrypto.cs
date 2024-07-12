namespace Business.Utils
{
    /// <summary>
    /// Métodos de encripción y descencripciónd etexto usando AES
    /// </summary>
    public interface ICrypto
    {
        /// <summary>
        /// Desencripta una cadena de texto usando el algoritmo AES
        /// </summary>
        /// <param name="input">Cadena a desencriptar</param>
        /// <param name="key">Llave de cifrado</param>
        /// <param name="iv">Vector de inicialización de cifrado</param>
        /// <returns>Cadena desencriptada</returns>
        public string Decrypt(string input, string key, string iv);

        /// <summary>
        /// Encripta una cadena de texto usando el algoritmo AES
        /// </summary>
        /// <param name="input">Cadena a encriptar</param>
        /// <param name="key">Llave de cifrado</param>
        /// <param name="iv">Vector de inicialización de cifrado</param>
        /// <returns>Cadena encriptada</returns>
        public string Encrypt(string input, string key, string iv);
    }
}
