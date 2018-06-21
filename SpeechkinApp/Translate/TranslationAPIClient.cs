using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Newtonsoft.Json;
using SpeechkinApp.Settings;

namespace SpeechkinApp.Translate
{
    public class TranslationApiClient
    {
        private readonly ITranslationSettings _translationSettings;

        private readonly ITranslationApiSender _sender;

        private const string Path = "/translate";

        private static readonly IDictionary<TranslationLanguage,string> _languages = new Dictionary<TranslationLanguage, string>
        {
            {TranslationLanguage.English,"en" },
            {TranslationLanguage.Russian,"ru" },
            {TranslationLanguage.German,"de" },
        };

        public TranslationApiClient(ITranslationSettings translationSettings, ITranslationApiSender sender)
        {
            _translationSettings = translationSettings;
            _sender = sender;
        }

        public async Task<TranslationResponse> Send(TranslationRequest request, CancellationToken token)
        {
            var result = new TranslationResponse();
            result.IsSuccess = false;

            CheckParameters(request);

            var url = MakeUri(request);

            var bodyRaw = MakBody(request);

            using (var message = new HttpRequestMessage(HttpMethod.Post, url))
            {
                message.Content = new StringContent(bodyRaw,Encoding.UTF8, MediaFormats.JsonFormat);
                message.Headers.Add("Ocp-Apim-Subscription-Key", _translationSettings.TranslatorPrimaryKey);

                HttpResponseMessage response = null;

                try
                {
                    response = await _sender.Send(message, token);
                }
                catch (HttpRequestException ex)
                {
                    result.ErrorMessage = ex.Message;
                }

                if (response!=null && response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var results = JsonConvert.DeserializeObject<TranslatedResult[]>(responseBody);
                    result.TranslatedResults.AddRange(results);
                    result.IsSuccess = true;
                }
            }

            return result;
        }

        private static void CheckParameters(TranslationRequest request)
        {
            if (!request.Items.Any())
            {
                throw new InvalidOperationException("Items are empty");
            }

            if (request.To == TranslationLanguage.Auto)
            {
                throw new InvalidOperationException("Field 'To' must`t set Auto");
            }
        }

        private static string MakBody(TranslationRequest request)
        {
            var body = request.Items.Select(i => new {i.Text});

            var bodyRaw = JsonConvert.SerializeObject(body);
            return bodyRaw;
        }

        private static Url MakeUri(TranslationRequest request)
        {
            var url = Path.SetQueryParam("api-version", "3.0");

            if (request.From != TranslationLanguage.Auto)
            {
                url = url.SetQueryParam("from", _languages[request.From]);
            }

            url = url.SetQueryParam("to", _languages[request.To]);
            return url;
        }
    }
}
