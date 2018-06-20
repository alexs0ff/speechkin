using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Threading;

namespace SpeechkinApp.Behaviors
{
    public class ObservableCollectionWrapper
    {
        private readonly Paragraph _paragraph;

        private readonly INotifyCollectionChanged _collectionChanged;

        private readonly IEnumerable _collection;

        private readonly string _templateName;

        public ObservableCollectionWrapper(Paragraph paragraph, INotifyCollectionChanged collectionChanged)
        {
            _paragraph = paragraph ?? throw new ArgumentNullException(nameof(paragraph));
            _collectionChanged = collectionChanged ?? throw new ArgumentNullException(nameof(collectionChanged));

            _collection = collectionChanged as IEnumerable;

            if (_collection == null)
            {
                throw new ArgumentNullException(nameof(collectionChanged));
            }
            _templateName = ParagraphInlineBehavior.GetTemplateResourceName(_paragraph);
        }

        public void Wrap()
        {
            if (_collection != null && _templateName != null)
            {
                _paragraph.Inlines.Clear();
                foreach (var inline in _collection)
                {
                    AddLine(inline);
                }
            }

            _collectionChanged.CollectionChanged+=CollectionChangedOnCollectionChanged;
        }

        private void CollectionChangedOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            foreach (var newItem in notifyCollectionChangedEventArgs.NewItems)
            {
                AddLine(newItem);
            }
        }

        private void AddLine(object inline)
        {
            if (!_paragraph.Dispatcher.CheckAccess())
            {
                _paragraph.Dispatcher.Invoke(() =>
                {
                    AddLineInner(inline);
                });
            }
            else
            {
                AddLineInner(inline);
            }
            
        }

        private void AddLineInner(object inline)
        {
            ArrayList templateList = _paragraph.FindResource(_templateName) as ArrayList;
            Span span = new Span();
            span.DataContext = inline;
            foreach (var templateInline in templateList)
            {
                span.Inlines.Add(templateInline as Inline);
            }
            _paragraph.Inlines.Add(span);
        }
    }
}
