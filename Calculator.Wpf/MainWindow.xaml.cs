using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Calculator.Wpf
{

    public partial class MainWindow : Window
    {
        Grid numberPanel;
        Grid additionalPanel;
        Grid lettersPanel;
        Grid radioButtonsGrid;
        TextBlock ExpressionTextBlock;
        private string textBoxVal = string.Empty;
        private readonly char[] mainButtonValues = new[] { '1', '2', '3', '+', '4', '5', '6', '-', '7', '8', '9', '*', '0', '.', 'C', '/' };
        private readonly string[] additionalButtonValues = new[] { "<-", "=", "(", ")", "->" };
        private readonly string[] variablesName = new[] { "x", "y", "z", "i", "j", "k", "f",",","Save" };
        private readonly string[] radioButtonValues = new[] { "Variables", "Functions" };

        //public readonly Dictionary<string, string> variablesDictionary = new Dictionary<string, string>();

        public MainWindow()
        {
            InitializeComponent();

            numberPanel = InitializeGridPanel(4, 4);
            additionalPanel = InitializeGridPanel(1, 5);
            lettersPanel = InitializeGridPanel(3, 3);
            radioButtonsGrid = InitializeGridPanel(2, 1);
            ExpressionTextBlock = InitializeExpressionTextBlock();

            AddRadioButtons(radioButtonsGrid, radioButtonValues);

            AddButtons(numberPanel, mainButtonValues);
            AddButtons(additionalPanel, additionalButtonValues);
            AddButtons(lettersPanel, variablesName);

            SetGridPosition(numberPanel, 2, 0);
            SetGridPosition(additionalPanel, 1, 0);
            SetGridPosition(lettersPanel, 2, 1);
            SetGridPosition(radioButtonsGrid, 1, 1);
            SetGridSpan(ExpressionTextBlock, 1, 2);

            Calculator.Children.Add(lettersPanel);
            Calculator.Children.Add(numberPanel);
            Calculator.Children.Add(additionalPanel);
            Calculator.Children.Add(radioButtonsGrid);
            Calculator.Children.Add(ExpressionTextBlock);

        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            Button pressedButton = (Button)sender;
            switch (pressedButton.Content)
            {
                case 'C':
                    ClearExpression();
                    break;
                case "<-":
                    if (textBoxVal != String.Empty)
                    {
                        textBoxVal = textBoxVal.Remove(textBoxVal.Count() - 1);
                        ExpressionTextBlock.Text = textBoxVal;
                    }
                    break;
                case "->":
                    
                    break;
                case "Save":
                    var checkedRadioButton = radioButtonsGrid.Children.OfType<RadioButton>().FirstOrDefault(n => n.IsChecked==true);
                    if(checkedRadioButton.Content.ToString() == radioButtonValues[0])//variable
                    {
                        MessageBox.Show("Var");

                    }
                    if (checkedRadioButton.Content.ToString() == radioButtonValues[1])//func
                    {
                        MessageBox.Show("Func");
                    }
                    break;
                default:
                    textBoxVal += pressedButton.Content;
                    ExpressionTextBlock.Text = textBoxVal;
                    break;
            }

        }

        private void SetGridPosition(FrameworkElement panel, int row, int colomn)
        {
            Grid.SetRow(panel, row);
            Grid.SetColumn(panel, colomn);
        }

        private void SetGridSpan(FrameworkElement panel, int rowSpan, int colomnSpan)
        {
            Grid.SetColumnSpan(panel, rowSpan);
            Grid.SetColumnSpan(panel, colomnSpan);
        }
        
        void ClearExpression()
        {
            textBoxVal = string.Empty;
            ExpressionTextBlock.Text = textBoxVal;
        }
        private void AddRadioButtons(Grid panel, string[] radioButtonValues)
        {
            for (int i = 0; i < radioButtonValues.Length; i++)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Content = radioButtonValues[i];
                radioButton.Resources.Add(typeof(RadioButton), (Style)FindResource(typeof(ToggleButton)));
                SetGridPosition(radioButton, i, 0);
                panel.Children.Add(radioButton);
            }

        }
        private void AddButtons<T>(Grid panel, IList<T> operations)
        {
            int index = 0;

            for (int row = 0; row < panel.RowDefinitions.Count; row++)
            {
                for (int colomn = 0; colomn < panel.ColumnDefinitions.Count; colomn++)
                {
                    if (index == operations.Count()) { return; };

                    Button numberBtn = new Button();
                    numberBtn.Click += InputButton_Click;
                    numberBtn.Content = operations[index].ToString();
                    numberBtn.Margin = new Thickness(1, 2, 1, 2);
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
            gridPanel.Margin = new Thickness(5, 5, 5, 5);

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
        private TextBlock InitializeExpressionTextBlock()
        {
            return new TextBlock
            {
                FontSize = 24,
                TextAlignment = TextAlignment.Right,
                Background = Brushes.LightGray
            };
        } 
    }

}
