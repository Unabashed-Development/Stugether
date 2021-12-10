using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace View.Components
{
    // https://stackoverflow.com/a/52295570
    public class YesNoButton : Button
    {
        public string Question { get; set; }
        protected override void OnClick()
        {
            if (string.IsNullOrWhiteSpace(Question))
            {
                base.OnClick();
                return;
            }

            var messageBoxResult = MessageBox.Show(Question, "Bevestiging", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                base.OnClick();
            }
        }
    }
}
