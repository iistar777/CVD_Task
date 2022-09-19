using DAL.ResponseAttributes;

namespace WebApi.Infrastructure
{
	/// <summary>
	/// Расширения для класса ResponseInfo
	/// </summary>
	internal static class ResponseInfoExtensions
	{
		/// <summary>
		/// Добавить информацию о безошибочном выполнении
		/// </summary>
		/// <param name="respInfo">Данные о результатах запроса</param>
		internal static void AddResponseInformationParameters(this ResponseInfo respInfo)
		{
			respInfo.ErrorMessage = null;
			respInfo.IsSuccessful = true;
		}

		/// <summary>
		/// Добавить информацию о выполнении с ошибкой
		/// </summary>
		/// <param name="respInfo">Данные о результатах запроса</param>
		/// <param name="errorMessage">Сообщение с ошибкой</param>
		internal static void AddResponseInfoParametersWithException(this ResponseInfo respInfo, string errorMessage)
		{
			respInfo.IsSuccessful = false;
			respInfo.ErrorMessage = errorMessage;
		}
	}
}
