namespace DAL.ResponseAttributes
{
    /// <summary>
    /// Базовый класс DTO ответов от сервера
    /// </summary>
    public class BaseResponseDTO
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BaseResponseDTO()
        {
            RespInfo = new ResponseInfo();
        }

        /// <summary>
        /// Данные о результатах запроса
        /// </summary>
        public ResponseInfo RespInfo { get; set; }
    }
}
