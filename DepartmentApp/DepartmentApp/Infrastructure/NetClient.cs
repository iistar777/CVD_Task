using DAL.ResponseAttributes;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentApp.Infrastructure
{
    /// <summary>
    /// Клиент для отправки авторизованных запросов на web API.
    /// </summary>
    public class NetClient
    {
        /// <summary>
        /// Базовый адрес web API
        /// </summary>
        private readonly Uri _baseUrlAddress;

        /// <summary>
        /// Конструктор для осуществления запросов к методам web API
        /// </summary>
        /// <param name="baseUrlAddress">Базовый адрес web API</param>        
        public NetClient(string baseUrlAddress)
        {
            _baseUrlAddress = new Uri(baseUrlAddress);
        }

        /// <summary>
        /// Отправка запроса методом POST
        /// </summary>
        /// <typeparam name="TResponse">Тип возвращаемого объекта</typeparam>
        /// <param name="value">Значение, которое передается на отправку в тело запроса</param>
        /// <param name="address">URL-адрес для отправки</param>
        /// <returns></returns>
        public async Task<TResponse> SendRequest<TResponse>(object value, string address)
            where TResponse : BaseResponseDTO, new()
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                };

                handler.DefaultProxyCredentials = CredentialCache.DefaultCredentials;

                using (HttpClient client = new HttpClient(handler))
                {
                    client.Timeout = TimeSpan.FromMinutes(5);

                    string jsonRequest = JsonConvert.SerializeObject(value, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    using (HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json"))
                    {
                        using (HttpResponseMessage response = await client.PostAsync(new Uri(_baseUrlAddress, address), content).ConfigureAwait(false))
                        {
                            response.EnsureSuccessStatusCode();

                            using (Stream stream = await response.Content.ReadAsStreamAsync())
                            using (StreamReader streamReader = new StreamReader(stream))
                            using (JsonReader jsonReader = new JsonTextReader(streamReader))
                            {
                                JsonSerializer serializer = new JsonSerializer();

                                TResponse result = serializer.Deserialize<TResponse>(jsonReader);
                                return result;
                            }
                        }
                    }
                }
            }
            catch (JsonReaderException)
            {
                string message = $"Сервер вернул ответ в нераспознанном формате.{Environment.NewLine}Проверьте Ваши настройки подключения к сети, настройки прокси-сервера, либо обратитесь к системному администратору.";

                TResponse tresponce = new TResponse();
                tresponce.RespInfo.IsSuccessful = false;
                tresponce.RespInfo.ErrorMessage = message;
                return tresponce;
            }
            catch (OutOfMemoryException)
            {
                string message = $"Слишком большой объем, пожалуйста, воспользуйтесь экспортом";

                TResponse tresponce = new TResponse();
                tresponce.RespInfo.IsSuccessful = false;
                tresponce.RespInfo.ErrorMessage = message;
                return tresponce;
            }
            catch (Exception ex)
            {
                TResponse tresponce = new TResponse();
                tresponce.RespInfo.IsSuccessful = false;
                tresponce.RespInfo.ErrorMessage = ex.Message;
                return tresponce;
            }
        }
    }
}
