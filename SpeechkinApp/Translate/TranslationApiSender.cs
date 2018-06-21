using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SpeechkinApp.Settings;

namespace SpeechkinApp.Translate
{
    public class TranslationApiSender: ITranslationApiSender
    {
        private readonly HttpClient _httpClient;

        private readonly ITranslationSettings _translationSettings;

        public TranslationApiSender(ITranslationSettings translationSettings)
        {
            _translationSettings = translationSettings;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_translationSettings.TranslatorUrl);
            _httpClient.Timeout = _translationSettings.TranslatorTimeout;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaFormats.JsonFormat));
        }

        public Task<HttpResponseMessage> Send(HttpRequestMessage message, CancellationToken token)
        {
            return _httpClient.SendAsync(message, token);
        }
    }
}
