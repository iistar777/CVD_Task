namespace DAL.ResponseAttributes
{
    /// <summary>
    /// Данные о результатах запроса
    /// </summary>
    public class ResponseInfo
    {
        /// <summary>
        /// Признак успешности результата операции
        /// </summary>
		public bool IsSuccessful { get; set; }

        /// <summary>
        /// Сообщение об ошибке в случае неуспешного результата операции 
        /// </summary>
		public string ErrorMessage { get; set; }
    }
}
