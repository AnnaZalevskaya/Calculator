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

namespace Calculator.Wpf
{

    public partial class MainWindow : Window
    {
        string textBoxVal = string.Empty;
        char[] operationsArr = new[] { '+', '-', '*', '/' };
        char[] numbers = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        public MainWindow()
        {
            InitializeComponent();
            Grid numberPanel = InitializeGridPanel(3, 3);
            Grid operationPanel = InitializeGridPanel(4, 1);

            AddButtons(numberPanel, numbers);
            AddButtons(operationPanel, operationsArr);

            Grid.SetRow(numberPanel, 1);
            Grid.SetColumn(numberPanel, 1);
            Calculator.Children.Add(numberPanel);

            Grid.SetRow(operationPanel, 1);
            Grid.SetColumn(operationPanel, 0);
            Calculator.Children.Add(operationPanel);
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            textBoxVal += ((Button)sender).Content.ToString();
            ExpressionTextBox.Text = textBoxVal;
        }
        private void AddButtons(Grid panel, char[] operations)
        {
            int index = 0;

            for (int row = 0; row < panel.RowDefinitions.Count; row++)
            {
                for (int colomn = 0; colomn < panel.ColumnDefinitions.Count; colomn++)
                {
                    Button numberBtn = new Button();
                    numberBtn.Click += InputButton_Click;
                    numberBtn.Content = operations[index];
                    Grid.SetRow(numberBtn, row);
                    Grid.SetColumn(numberBtn, colomn);
                    panel.Children.Add(numberBtn);
                    index++;
                }
            }

        }

        private Grid InitializeGridPanel(int row, int column)
        {
            Grid gridPanel = new Grid();
            gridPanel.ShowGridLines = true;
            for (int i = 0; i < row; i++)
            {
                gridPanel.RowDefinitions.Add(new RowDefinition());
            }
            for (int j = 0; j < column; j++)
            {
                gridPanel.ColumnDefinitions.Add(new ColumnDefinition());

            }
            return gridPanel;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            textBoxVal = string.Empty;
            ExpressionTextBox.Text = textBoxVal;

        }
    }
}
