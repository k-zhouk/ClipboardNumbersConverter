using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using CN_Converter.Formatters;

namespace CN_Converter.Common
{
    // Base class for INotifyPropertyChanged
    // Created in order not to implement the INotifyPropertyChanged for all classes
    class CNCBaseChange : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

    // Class stores main window options
    class MainWindowOptions : CNCBaseChange
    {
        // Variable to store the status of the window- stay on top or not
        private bool _wndStayOnTop = true;
        public bool WndStayOnTop
        {
            get => _wndStayOnTop;
            set { _wndStayOnTop = value; Notify("WndStayOnTop"); }
        }

        // Variable to store the status of the window minimization with Esc
        private bool _isEscMinimiseActive = true;
        public bool IsEscMinimiseActive
        {
            get => _isEscMinimiseActive;
            set { _isEscMinimiseActive = value; Notify("IsEscMinimiseActive"); }
        }

        // Variable to store the window state
        private System.Windows.WindowState _wndMinimized = System.Windows.WindowState.Normal;
        public System.Windows.WindowState WndMinimized
        {
            get => _wndMinimized;
            set { _wndMinimized = value; Notify("WndMinimized"); }
        }
    }

    // Enumerartion for a HEX string formatting
    public enum HexStringGroupingOptions { NoGrouping, Byte, Word, DoubleWord }

    // Enumeration for a BIN string size representation
    public enum BinStrSizeOptions { NoGrouping, HalfByte, Byte, Word }

    class CNCCommon : CNCBaseChange, INotifyDataErrorInfo
    {
        // Indication of the active convertion type (clipboard/ direct from the text box)
        public bool IsClipboardConvertionActive = true;

        // This property contains a split character for a DEC number
        private char _decSplitCharacter = '\0';
        public char DecSplitCharacter
        {
            get => _decSplitCharacter;
            set
            {
                if (_decSplitCharacter != value)
                {
                    _decSplitCharacter = value;
                    Notify("DecSplitCharacter");
                }
            }
        }

        // The property contains the HEX string grouping options
        private HexStringGroupingOptions _hexGrouping = HexStringGroupingOptions.NoGrouping;
        public HexStringGroupingOptions HexGroupingOption
        {
            get => _hexGrouping;
            set
            {
                if (_hexGrouping != value)
                {
                    _hexGrouping = value;
                    Notify("HexGroupingOption");
                }
            }
        }

        // The property contaions the BIN data size
        private BinStrSizeOptions _binStringSizeOption = BinStrSizeOptions.NoGrouping;
        public BinStrSizeOptions BinStringSizeOption
        {
            get => _binStringSizeOption;
            set
            {
                if (_binStringSizeOption != value)
                {
                    _binStringSizeOption = value;
                    Notify("BinStringSizeOption");
                }
            }
        }

        // The property contains text content of the clipboard
        private string _clipboardText = string.Empty;
        public string ClipboardTextContent
        {
            get => _clipboardText;
            set
            {
                if (_clipboardText != value)
                {
                    _clipboardText = value;
                    Notify("ClipboardTextContent");
                }
            }
        }

        // The property contains the DEC representation of the clipboard content
        private string _clipboardDecNum = "N/A";
        public string ClipboardDecNum
        {
            get => _clipboardDecNum;
            set
            {
                if (_clipboardDecNum != value)
                {
                    _clipboardDecNum = value;
                    Notify("ClipboardDecNum");
                }
            }
        }

        // Formatted representation of a DEC number
        private string _formattedDecString = string.Empty;
        public string FormattedDecString
        {
            get => _formattedDecString;
            set
            {
                if (_formattedDecString != value)
                {
                    _formattedDecString = value;
                    Notify("FormattedDecString");

                    if (!IsClipboardConvertionActive)
                    {
                        if (value != string.Empty)
                        {
                            _ = long.TryParse(FormattedDecString, out long tmpNum);
                            FormattedHexString = Convert.ToString(tmpNum, 16).ToUpper();
                            ClipboardBinNum = Convert.ToString(tmpNum, 2);
                            FormattedBinString = CNCStringsFormatters.FormatBinString(ClipboardBinNum, BinStringSizeOption);
                        }
                    }
                }
            }
        }

        // The property contains the HEX reporesentation of the clipboard content
        private string _clipboardHexNum = "N/A";
        public string ClipboardHexNum
        {
            get => _clipboardHexNum;
            set
            {
                if (_clipboardHexNum != value)
                {
                    _clipboardHexNum = value;
                    Notify("ClipboardHexNum");
                }
            }
        }

        private string _formattedHexString = string.Empty;
        public string FormattedHexString
        {
            get => _formattedHexString;
            set
            {
                if (_formattedHexString != value)
                {
                    _formattedHexString = value;
                    Notify("FormattedHexString");

                    if (!IsClipboardConvertionActive)
                    {
                        if (value != string.Empty)
                        {
                            _ = long.TryParse(FormattedHexString, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out long tmpNum);
                            FormattedDecString = Convert.ToString(tmpNum, 10).ToUpper();
                            ClipboardBinNum = Convert.ToString(tmpNum, 2);
                            FormattedBinString = CNCStringsFormatters.FormatBinString(ClipboardBinNum, BinStringSizeOption);
                        }
                    }
                }
            }
        }

        // The property contains the BIN reporesentation of the clipboard content
        private string _clipboardBinNumber = "N/A";
        public string ClipboardBinNum
        {
            get => _clipboardBinNumber;
            set
            {
                if (_clipboardBinNumber != value)
                {
                    _clipboardBinNumber = value;
                    Notify("ClipboardBinNumber");
                }
            }
        }

        // The property contains the formatted BIN string
        private string _formattedBinString = string.Empty;
        public string FormattedBinString
        {
            get => _formattedBinString;
            set
            {
                if (_formattedBinString != value)
                {
                    _formattedBinString = value;
                    Notify("FormattedBinString");
                }
            }
        }

        // Property for the status bar message
        private string _statusBarMessage = "OK";
        public string StatusBarMessage
        {
            get => _statusBarMessage;
            set
            {
                if (_statusBarMessage != value)
                {
                    _statusBarMessage = value;
                    Notify("StatusBarMessage");
                }
            }
        }

        public void SetFormattedPropertiesToEmpty()
        {
            FormattedDecString = string.Empty;
            FormattedHexString = string.Empty;
            FormattedBinString = string.Empty;
        }

        public void SetFormattedPropertiesToNa()
        {
            FormattedDecString = "N/A";
            FormattedHexString = "N/A";
            FormattedBinString = "N/A";
        }

        // INotifyDataErrorInfo interface implementation
        private Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public bool HasErrors => _errorsByPropertyName.Count > 0;

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsByPropertyName.ContainsKey(propertyName) ? _errorsByPropertyName[propertyName] : null;
        }

        // Supporting methods for errors processing
        private void AddError(string propertyName, string error)
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName[propertyName] = new List<string>();
            }

            if (!_errorsByPropertyName[propertyName].Contains(error))
            {
                _errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        public void ClearErrors(string propertyName)
        {
            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        // Convertion methods
        // Methods to to check whether the data in the clipboard can be converted into a DEC number
        public bool IsClipboardContentValidForConversion()
        {
            ClearErrors(nameof(StatusBarMessage));

            // Show an error message is the ClipboardTextContent is NULL
            if (ClipboardTextContent is null)
            {
                StatusBarMessage = string.Empty;
                SetFormattedPropertiesToNa();

                AddError(nameof(StatusBarMessage), "There is no text string in the clipboard to convert into a number");
                return false;
            }

            if (!IsClipboardContentValidDecNumber())
            {
                StatusBarMessage = string.Empty;
                SetFormattedPropertiesToNa();

                AddError(nameof(StatusBarMessage), "Clipboard text string cannot be converted into a long number");
                return false;
            }

            if (!long.TryParse(ClipboardTextContent, out _))
            {
                StatusBarMessage = string.Empty;
                SetFormattedPropertiesToNa();

                AddError(nameof(StatusBarMessage), "Clipboard text string cannot be converted into a long number");
                return false;
            }

            return true;
        }

        // Check if the clipboard content is a valid long DEC number
        private bool IsClipboardContentValidDecNumber()
        {
            if (!long.TryParse(ClipboardTextContent, out _))
            {
                return false;
            }
            return true;
        }

        // Check if the clipboard content is a valid HEX number
        // The input HEX string could be in the following formats:
        // abcdef
        // 0xabcdef / 0Xabcdef (C, C++, C#, Python)
        // 0abcdefh (x86 assembler)
        // TODO: implement the function IsClipboardContentValidHexNumber()
        private bool IsClipboardContentValidHexNumber()
        {
            _ = MessageBox.Show("This menthod has not been impemented yet", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
    }
}
