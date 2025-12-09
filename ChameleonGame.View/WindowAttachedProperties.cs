using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChameleonGame.View
{
    public class WindowAttachedProperties
    {
        public static readonly DependencyProperty DialogResultProperty =
            DependencyProperty.RegisterAttached(
                "DialogResult",
                typeof(bool?),
                typeof(WindowAttachedProperties),
                new PropertyMetadata(false, OnDialogResultChanged));
        public static bool? GetDialogResult(DependencyObject obj)
        {
            return (bool)obj.GetValue(DialogResultProperty);
        }
        public static void SetDialogResult(DependencyObject obj, bool? value)
        {
            obj.SetValue(DialogResultProperty, value);
        }
        private static void OnDialogResultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window && e.NewValue is bool result)
            {
                try
                {
                    window.DialogResult = result;
                }
                catch
                {

                    window.Close();
                }
            }
        }
    }
}
