using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using SpeechkinApp.Behaviors;
using SpeechkinApp.Infrastructure;
using SpeechkinApp.Translate;

namespace SpeechkinApp.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// TODO: rewrite to navigation layout
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SpeechkinController _controller;

        private readonly HotKey _activateWindowKey;

        private readonly HotKey _showTranslationWindowHotKey;

        private readonly Popup _popup;

        private readonly TextBlock _popupText;

        public MainWindow(SpeechkinController controller, WindowFabric windowFabric)
        {
            _controller = controller;
            InitializeComponent();

            _controller.SetWindow(this);
            _activateWindowKey = new HotKey(Key.F2,KeyModifier.Ctrl,ActivateTranslation,false);
            _showTranslationWindowHotKey = new HotKey(Key.F1,KeyModifier.Ctrl, ShowTranslation, false);
            _popup = new Popup();
            _popupText = new TextBlock();

            InitializePopup();
        }

        #region Popup

        private void InitializePopup()
        {
            _popup.AllowsTransparency = true;
            _popup.Placement = PlacementMode.MousePoint;
            _popup.MaxWidth = 300;
            _popup.StaysOpen = false;
            _popup.MouseDown += (sender, args) =>
            {
                _popup.IsOpen = false;
            };

            var popUpBorder = new Border();
            _popup.Child = popUpBorder;
            popUpBorder.BorderBrush = Brushes.LightBlue;
            popUpBorder.BorderThickness = new Thickness(2);
            popUpBorder.Background = Brushes.Azure;


            popUpBorder.Child = _popupText;
            _popupText.Margin = new Thickness(10);
            _popupText.TextWrapping = TextWrapping.Wrap;
        }

        private void AddTranslatedTextToPopup(TranslationResponse response)
        {
            AddTranslatedText(response);

            var text = response?.TranslatedResults.FirstOrDefault()?.Translations.FirstOrDefault()?.Text;
            SetPopUpText(text);
        }

        private void ClosePopUp()
        {
            if (!_popup.Dispatcher.CheckAccess())
            {
                _popup.Dispatcher.Invoke(ClosePopUp);
                return;
            }

            _popup.IsOpen = false;
        }

        private void SetPopUpText(string text)
        {
            if (!_popup.Dispatcher.CheckAccess())
            {
                _popup.Dispatcher.Invoke(() => SetPopUpText(text));
                return;
            }

            _popupText.Text = text;
            _popup.IsOpen = true;
        }

        #endregion Popup

        private void ActivateTranslation(HotKey hotKey)
        {
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
            }
            Activate();
            translateTextBox.Focus();
            translateTextBox.Select(0, translateTextBox.Text.Length);
        }

        private const int MaxClipboardTextLength = 2000;

        private void ShowTranslation(HotKey hotKey)
        {
            var text = RemoteGetText.GetTextFromControlAtMousePosition();
            if (!string.IsNullOrWhiteSpace(text))
            {
                if (text.Length> MaxClipboardTextLength)
                {
                    text = text.Substring(0, MaxClipboardTextLength);
                }
                Task.Factory.StartNew(async () =>
                    {
                        await _controller.Translate(text, AddRequestInfo, AddTranslatedTextToPopup);
                    }
                );
            }
        }


        private void ShowOptionsClick(object sender, RoutedEventArgs e)
        {
            _controller.ShowSettings();
        }

        private void StartClick(object sender, RoutedEventArgs e)
        {
            _controller.StartRecognition();
        }

        private void StopClick(object sender, RoutedEventArgs e)
        {
            _controller.Stop();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _activateWindowKey.Register();
            _showTranslationWindowHotKey.Register();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _activateWindowKey.Dispose();
            _showTranslationWindowHotKey.Dispose();
        }

        private async void TranslateClick(object sender, RoutedEventArgs e)
        {
            await TranslateFromBox();
        }

        private async Task TranslateFromBox()
        {
            var text = translateTextBox.SelectedText;

            if (string.IsNullOrWhiteSpace(text))
            {
                text = translateTextBox.Text;
            }

            if (!string.IsNullOrWhiteSpace(text))
            {
                await _controller.Translate(text, AddRequestInfo, AddTranslatedText);
            }
        }

        private async Task TranslateFromSpeech()
        {
            var text = FlowDocumentReader?.Selection?.Text;

            if (!string.IsNullOrWhiteSpace(text))
            {
                await _controller.Translate(text, AddRequestInfo, AddTranslatedText);
            }
        }

        public void AddRequestInfo(TranslationRequest request)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(() => AddRequestInfo(request));
                return;
            }

            var requestParagraph = new Paragraph();

            requestParagraph.Inlines.Add(new Run("From "));
            requestParagraph.Inlines.Add(new Bold(new Run($"{request.From}")));
            requestParagraph.Inlines.Add(new Run(" to "));
            requestParagraph.Inlines.Add(new Bold(new Run($"{request.To}")));

            requestParagraph.Inlines.Add(new LineBreak());

            foreach (var translationItem in request.Items)
            {
                requestParagraph.Inlines.Add(new Italic(new Run(translationItem.Text)));
            }

            TranslationAreaDocument.Blocks.Add(requestParagraph);
        }

        public void AddTranslatedText(TranslationResponse response)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(() => AddTranslatedText(response));
                return;
            }

            var responseParagraph = new Paragraph();

            if (!response.IsSuccess)
            {

                var redSpan = new Span(new Run(response.ErrorMessage));
                redSpan.Foreground = Brushes.OrangeRed;
                responseParagraph.Inlines.Add(new Run("Error: "));
                responseParagraph.Inlines.Add(redSpan);
            }
            else
            {
                var resultSpan = new Span(new Run(response.ErrorMessage));

                resultSpan.Foreground = Brushes.DarkGreen;


                foreach (var responseTranslatedResult in response.TranslatedResults)
                {
                    if (responseTranslatedResult!=null)
                    {
                        foreach (var translatedItem in responseTranslatedResult.Translations)
                        {
                            resultSpan.Inlines.Add(translatedItem.Text);
                        }
                    }
                }

                responseParagraph.Inlines.Add(resultSpan);
            }

            
            TranslationAreaDocument.Blocks.Add(responseParagraph);
            TranslatorFlowDocumentScrollViewer.ScrollToEnd();
        }

        private async void translateTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                {
                    await TranslateFromBox();
                }
            }

            else if(e.Key == Key.F8)
            {
                _controller.ChangeRussianToEnglish();
            }
        }

        private async void FlowDocumentReader_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                {
                    await TranslateFromSpeech();
                }
            }
        }
    }
}
