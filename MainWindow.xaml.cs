using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_calculator
{
    public partial class MainWindow : Window
    {
        private string currentInput = "";
        private string lastOperator = "";
        private double lastNumber = 0.0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Content.ToString();

            if (buttonText == ".")
            {
                if (!currentInput.Contains("."))
                {
                    currentInput += buttonText;
                }
            }
            else
            {
                currentInput += buttonText;
            }

            UpdateResultText(currentInput);
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Content.ToString();

            if (currentInput != "")
            {
                if (lastOperator != "")
                {
                    CalculateResult();
                }
                else
                {
                    lastNumber = double.Parse(currentInput);
                }

                currentInput = "";
                lastOperator = buttonText;
            }
            else if (lastOperator != "" && buttonText != "-")
            {
                lastOperator = buttonText;
            }
            else if (buttonText == "-")
            {
                currentInput = "-";
                UpdateResultText(currentInput);
            }
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            if (lastOperator != "" && currentInput != "")
            {
                CalculateResult();
                lastOperator = "";
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            currentInput = "";
            lastOperator = "";
            lastNumber = 0.0;

            UpdateResultText("0");
        }

        private void PercentButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentInput != "")
            {
                double percent = double.Parse(currentInput) / 100.0;
                UpdateResultText(percent.ToString());
                currentInput = percent.ToString();
            }
        }

        private void UpdateResultText(string text)
        {
            ResultTextBox.Text = text;
        }

        private void CalculateResult()
        {
            double currentNumber = double.Parse(currentInput);

            switch (lastOperator)
            {
                case "+":
                    lastNumber += currentNumber;
                    break;
                case "-":
                    lastNumber -= currentNumber;
                    break;
                case "*":
                    lastNumber *= currentNumber;
                    break;
                case "/":
                    if (currentNumber != 0)
                    {
                        lastNumber /= currentNumber;
                    }
                    else
                    {
                        MessageBox.Show("Cannot divide by zero", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
            }

            UpdateResultText(lastNumber.ToString());
            currentInput = "";
        }
    }

}
