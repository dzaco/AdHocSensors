using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AdHocSensors.WpfApp.Helpers
{
    public static class InputValidationHelper
    {
        #region Enum Declarations

        public enum NumericFormat
        {
            Double,
            Int,
            Uint,
            Natural
        }

        #endregion Enum Declarations

        #region Dependency Properties & CLR Wrappers

        public static readonly DependencyProperty OnlyNumericProperty =
            DependencyProperty.RegisterAttached("OnlyNumeric", typeof(NumericFormat?), typeof(InputValidationHelper),
                new PropertyMetadata(null, DependencyPropertiesChanged));

        public static void SetOnlyNumeric(TextBox element, NumericFormat value) => element.SetValue(OnlyNumericProperty, value);

        public static NumericFormat GetOnlyNumeric(TextBox element) => (NumericFormat)element.GetValue(OnlyNumericProperty);

        #endregion Dependency Properties & CLR Wrappers

        #region Dependency Properties Methods

        private static void DependencyPropertiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is TextBox textBox))
                throw new Exception("Attached property must be used with TextBox.");

            switch (e.Property.Name)
            {
                case "OnlyNumeric":
                    {
                        var castedValue = (NumericFormat?)e.NewValue;

                        if (castedValue.HasValue)
                        {
                            textBox.PreviewTextInput += TextBox_PreviewTextInput;
                        }
                        else
                        {
                            textBox.PreviewTextInput -= TextBox_PreviewTextInput;
                        }

                        break;
                    }
            }
        }

        #endregion Dependency Properties Methods

        private static void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = (TextBox)sender;

            switch (GetOnlyNumeric(textBox))
            {
                case NumericFormat.Double:
                    {
                        DoubleInputValidation(sender, e);
                        break;
                    }

                case NumericFormat.Int:
                    {
                        NumberInputValidation(e);
                        break;
                    }
            }
        }

        public static void NumberInputValidation(TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public static void DoubleInputValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }
    }
}