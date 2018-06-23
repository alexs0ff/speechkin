using System;
using System.Threading;
using System.Threading.Tasks;
using SpeechkinApp.Settings;
using SpeechkinApp.Speech;
using SpeechkinApp.Translate;

namespace SpeechkinApp.Main
{
    public class SpeechkinController: WindowControllerBase<MainWindowDataModel>
    {
        private readonly WindowFabric _windowFabric;

        private readonly SpeechRecognitionClient _recognitionClient;

        private readonly TranslationApiClient _translationApiClient;

        public SpeechkinController(WindowFabric windowFabric, SpeechRecognitionClient recognitionClient, TranslationApiClient translationApiClient)
        {
            _windowFabric = windowFabric;
            _recognitionClient = recognitionClient;
            _translationApiClient = translationApiClient;
            Model = new MainWindowDataModel();
            Model.IsStarted = false;
            Model.FromLanguages.Add(new LanguageItem{Id = (int)TranslationLanguage.Auto,Text = "Auto"});
            Model.FromLanguages.Add(new LanguageItem{Id = (int)TranslationLanguage.Russian,Text = "Russian" });
            Model.FromLanguages.Add(new LanguageItem{Id = (int)TranslationLanguage.English,Text = "English" });
            Model.FromLanguages.Add(new LanguageItem{Id = (int)TranslationLanguage.German,Text = "German" });


            Model.ToLanguages.Add(new LanguageItem { Id = (int)TranslationLanguage.Russian, Text = "Russian" });
            Model.ToLanguages.Add(new LanguageItem { Id = (int)TranslationLanguage.English, Text = "English" });
            Model.ToLanguages.Add(new LanguageItem { Id = (int)TranslationLanguage.German, Text = "German" });

            Model.FromLanguageId = (int) TranslationLanguage.Auto;
            Model.ToLanguageId = (int) TranslationLanguage.English;
        }

        public void ShowSettings()
        {
            var window = _windowFabric.CreateWindow<SettingsWindow>();

            window.ShowDialog();
        }

        public void StartRecognition()
        {
            _recognitionClient.Start(parameters =>
            {
                parameters.OnNewItemAction = OnNewRecognition;
                parameters.OnEnd += s =>
                {
                    Stop();
                };
            });
            Model.IsStarted = true;
        }

        public void Stop()
        {
            Model.IsStarted = false;
            _recognitionClient.Stop();
        }

        private void OnNewRecognition(RecognitionItem newItem)
        {
            Model.RecognitionItems.Add(newItem);
        }
       

        public async Task Translate(string text, Action<TranslationRequest> requestAction,
            Action<TranslationResponse> responseAction)
        {
            TranslationResponse response;

            try
            {
                var request = new TranslationRequest();
                request.From = (TranslationLanguage)Model.FromLanguageId;
                request.To = (TranslationLanguage)Model.ToLanguageId;
                request.Items.Add(new TranslationItem
                {
                    Text = text
                });

                requestAction?.Invoke(request);
                response = await _translationApiClient.Send(request, CancellationToken.None);
            }
            catch (Exception ex)
            {
                response = new TranslationResponse();
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
            }

            responseAction?.Invoke(response);

        }
    }
}
