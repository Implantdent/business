namespace Business.Exceptions
{
    /// <summary>
    /// Excepción personalizada para la lógica de entidades
    /// </summary>
    [Serializable]
    public class BusinessException : Exception
    {
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public BusinessException() { }

        /// <summary>
        /// Crea una excepción con un mensaje
        /// </summary>
        /// <param name="message">Mensaje de la excepción</param>
        public BusinessException(string message) : base(message) { }

        /// <summary>
        /// Crea una excepción con un mensaje y una excepción interna
        /// </summary>
        /// <param name="message">Mensaje de la excepción</param>
        /// <param name="inner">Excepción interna</param>
        public BusinessException(string message, Exception inner) : base(message, inner) { }
    }
}
