using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
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

        private readonly HotKey _hotKey;

        public MainWindow(SpeechkinController controller, WindowFabric windowFabric)
        {
            _controller = controller;
            InitializeComponent();

            _controller.SetWindow(this);
            _hotKey = new HotKey(Key.F1,KeyModifier.Ctrl,ActivateTranslation,false);
        }

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
            _hotKey.Register();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _hotKey.Dispose();
        }

        private async void TranslateClick(object sender, RoutedEventArgs e)
        {
            await _controller.TranslateFromBox();
        }

        public void AddRequestInfo(TranslationRequest request)
        {
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
        }

        private async void translateTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                if (e.SystemKey == Key.Enter)
                {
                    await _controller.TranslateFromBox();
                }
            }
            
        }
    }
}
