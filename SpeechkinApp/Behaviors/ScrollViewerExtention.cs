using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SpeechkinApp.Behaviors
{
    public static class ScrollViewerExtention
    {
        public static void ScrollToEnd(this FlowDocumentScrollViewer viewer)
        {
            GetScroller(viewer)?.ScrollToEnd();
        }

        private static ScrollViewer GetScroller(DependencyObject obj)
        {
            do
            {
                if (VisualTreeHelper.GetChildrenCount(obj) > 0)
                {
                    obj = VisualTreeHelper.GetChild(obj as Visual, 0);
                }
                else
                {
                    return null;
                }
            }
            while (!(obj is ScrollViewer));

            return obj as ScrollViewer;
        }
    }
}
