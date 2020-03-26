using Infonet.CStoreCommander.Logging;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace Infonet.CStoreCommander.UI.Utility
{
    public class ViewUtility
    {
        private const string _signatureFilePath = "C:\\Program Files (x86)\\infonet-pos\\Sign.jpg";

        private InfonetLog _log = InfonetLogManager.GetLogger(typeof(ViewUtility));

        public T FindChild<T>(DependencyObject parent, string childName)
   where T : DependencyObject
        {
            if (parent == null)
            {
                parent = Window.Current.Content;
            }

            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;


            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            if (parent is Popup)
            {
                var p = parent as Popup;
                // recursively drill down the tree
                foundChild = FindChild<T>(p.Child, childName);
            }
            else
            {
                for (int i = 0; i < childrenCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    // If the child is not of the request child type child
                    T childType = child as T;
                    if (childType == null)
                    {
                        // recursively drill down the tree
                        foundChild = FindChild<T>(child, childName);

                        // If the child is found, break so we do not overwrite the found child. 
                        if (foundChild != null) break;
                    }
                    else if (!string.IsNullOrEmpty(childName))
                    {
                        var frameworkElement = child as FrameworkElement;
                        // If the child's name is set for search
                        if (frameworkElement != null && frameworkElement.Name == childName)
                        {
                            // if the child's name is of the request name
                            foundChild = (T)child;
                            break;
                        }
                    }
                    else
                    {
                        // child element found.
                        foundChild = (T)child;
                        break;
                    }
                }
            }

            return foundChild;
        }

        public async Task LoadImageOnInkCanvas()
        {
            try
            {
                var inkCanvas = FindChild<InkCanvas>(null, "inkCanvas");

                if (inkCanvas == null)
                {
                    return;
                }

                var filePath = await StorageFile.GetFileFromApplicationUriAsync(new Uri(_signatureFilePath));
                if (filePath != null)
                {
                    try
                    {
                        using (IRandomAccessStream stream = await filePath.OpenReadAsync())
                        {
                            await inkCanvas.InkPresenter.StrokeContainer.LoadAsync(stream);
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Info(ex.Message, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Info(ex.Message, ex);
            }
        }
    }
}
