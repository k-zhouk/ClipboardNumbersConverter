using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CN_Converter.Common;
using CN_Converter.Formatters;

namespace CN_Converter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowOptions mainWindowOptions = new MainWindowOptions();
        CNCCommon cNCCommon = new CNCCommon();

        public MainWindow()
        {
            InitializeComponent();

            wndMain.DataContext = mainWindowOptions;
            grdMainGrid.DataContext = cNCCommon;
        }

        private void chkbxStayOnTop_Checked(object sender, RoutedEventArgs e)
        {
            mainWindowOptions.WndStayOnTop = true;
        }

        private void chkbxStayOnTop_Unchecked(object sender, RoutedEventArgs e)
        {
            mainWindowOptions.WndStayOnTop = false;
        }

        private void chckbxEscToMinimize_Checked(object sender, RoutedEventArgs e)
        {
            mainWindowOptions.IsEscMinimiseActive = true;
        }

        private void chckbxEscToMinimize_Unchecked(object sender, RoutedEventArgs e)
        {
            mainWindowOptions.IsEscMinimiseActive = false;
        }

        private void wndMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (mainWindowOptions.IsEscMinimiseActive)
            {
                if (e.Key == Key.Escape)
                {
                    mainWindowOptions.WndMinimized = System.Windows.WindowState.Minimized;
                }
            }

            // CTRL+1 to set convertion source to the clipboard
            if (e.Key == Key.D1 && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                if (!cNCCommon.IsClipboardConvertionActive)
                {
                    rbConvertFromClipboard.IsChecked = true;
                }
            }

            // CTRL+2 to set convertion source to a TextBox
            if (e.Key == Key.D2 && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                rbConvertFromTextBox.IsChecked = true;
            }
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                cNCCommon.ClipboardTextContent = Clipboard.GetText();
            }
            else
            {
                cNCCommon.ClipboardTextContent = null;
            }

            bool validContent = cNCCommon.IsClipboardContentValidForConversion();

            if (validContent)
            {
                long tmpNum = long.Parse(cNCCommon.ClipboardTextContent);

                cNCCommon.ClipboardDecNum = Convert.ToString(tmpNum, 10);
                cNCCommon.FormattedDecString = CNCStringsFormatters.FormatDecString(cNCCommon.ClipboardDecNum, cNCCommon.DecSplitCharacter);

                cNCCommon.ClipboardHexNum = Convert.ToString(tmpNum, 16);
                cNCCommon.FormattedHexString = CNCStringsFormatters.FormatHexString(cNCCommon.ClipboardHexNum, cNCCommon.HexGroupingOption);

                cNCCommon.ClipboardBinNum = Convert.ToString(tmpNum, 2);
                cNCCommon.FormattedBinString = CNCStringsFormatters.FormatBinString(cNCCommon.ClipboardBinNum, cNCCommon.BinStringSizeOption);

                cNCCommon.StatusBarMessage = "OK";
            }
        }

        private void cmbbxDecGrouping_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update the DEC formatting option based on the selection in the dropdown
            switch (cmbbxDecGrouping.SelectedItem)
            {
                case "No thousands split":
                    cNCCommon.DecSplitCharacter = '\0';
                    break;
                case "Split with spaces":
                    cNCCommon.DecSplitCharacter = ' ';
                    break;
                case "Split with dots":
                    cNCCommon.DecSplitCharacter = '.';
                    break;
                case "Split with commas":
                    cNCCommon.DecSplitCharacter = ',';
                    break;
            }
            cNCCommon.FormattedDecString = CNCStringsFormatters.FormatDecString(cNCCommon.ClipboardDecNum, cNCCommon.DecSplitCharacter);
        }

        private void cmbbxHexGrouping_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update the HEX formatting option based on the selection in the dropdown
            switch (cmbbxHexGrouping.SelectedItem)
            {
                case "No grouping":
                    cNCCommon.HexGroupingOption = HexStringGroupingOptions.NoGrouping;
                    break;
                case "Byte":
                    cNCCommon.HexGroupingOption = HexStringGroupingOptions.Byte;
                    break;
                case "Word":
                    cNCCommon.HexGroupingOption = HexStringGroupingOptions.Word;
                    break;
                case "Doube word":
                    cNCCommon.HexGroupingOption = HexStringGroupingOptions.DoubleWord;
                    break;
            }
            cNCCommon.FormattedHexString = CNCStringsFormatters.FormatHexString(cNCCommon.ClipboardHexNum, cNCCommon.HexGroupingOption);
        }

        private void rbConvertFromClipboard_Checked(object sender, RoutedEventArgs e)
        {
            cNCCommon.IsClipboardConvertionActive = true;
            cNCCommon.SetFormattedPropertiesToNa();

            cNCCommon.StatusBarMessage = "OK";
        }

        private void rbConvertFromTextBox_Checked(object sender, RoutedEventArgs e)
        {
            cNCCommon.IsClipboardConvertionActive = false;

            cNCCommon.ClearErrors("StatusBarMessage");

            cmbbxDecGrouping.SelectedItem = "No thousands split";
            cmbbxHexGrouping.SelectedItem = "No grouping";

            cNCCommon.SetFormattedPropertiesToEmpty();

            cNCCommon.StatusBarMessage = "OK";
        }

        private void rbNoGrouping_Checked(object sender, RoutedEventArgs e)
        {
            cNCCommon.FormattedBinString = CNCStringsFormatters.FormatBinString(cNCCommon.ClipboardBinNum, cNCCommon.BinStringSizeOption);
        }

        private void rbHalfByte_Checked(object sender, RoutedEventArgs e)
        {
            cNCCommon.FormattedBinString = CNCStringsFormatters.FormatBinString(cNCCommon.ClipboardBinNum, cNCCommon.BinStringSizeOption);
        }

        private void rbByte_Checked(object sender, RoutedEventArgs e)
        {
            cNCCommon.FormattedBinString = CNCStringsFormatters.FormatBinString(cNCCommon.ClipboardBinNum, cNCCommon.BinStringSizeOption);
        }

        private void rbWord_Checked(object sender, RoutedEventArgs e)
        {
            cNCCommon.FormattedBinString = CNCStringsFormatters.FormatBinString(cNCCommon.ClipboardBinNum, cNCCommon.BinStringSizeOption);
        }

        private void txtbxDecValue_GotFocus(object sender, RoutedEventArgs e)
        {
            txtbxDecValue.Select(txtbxDecValue.Text.Length, 0);
        }

        private void txtbxHexValue_GotFocus(object sender, RoutedEventArgs e)
        {
            txtbxHexValue.Select(txtbxHexValue.Text.Length, 0);
        }
    }
}
