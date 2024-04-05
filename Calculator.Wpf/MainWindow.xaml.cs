using Calculator.Application.Evaluator;
using Calculator.Wpf.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Calculator.Wpf
{
    public partial class MainWindow : Window
    {
        private Grid _numberPanel;
        private Grid _additionalPanel;
        private Grid _lettersPanel;
        private Grid _radioButtonsGrid;
        private TextBlock _ExpressionTextBlock;
        private readonly char[] _mainButtonValues = new[] { '1', '2', '3', '+', '4', '5', '6', '-', '7', '8', '9', '*', '0', '.', 'C', '/' };
        private readonly string[] _additionalButtonValues = new[] { "<-", "=", "(", ")", "Res" };
        private readonly string[] _variablesName = new[] { "x", "y", "z", "i", "j", "k", "f",",","Save" };
        private readonly string[] _radioButtonValues = new[] { "Variables", "Functions" };
        private ExpressionEvaluator _expressionEvaluator;

        private UserValidator _validator;

        public MainWindow()
        {
            InitializeComponent();

            _numberPanel = InitializeGridPanel(4, 4);
            _additionalPanel = InitializeGridPanel(1, 5);
            _lettersPanel = InitializeGridPanel(3, 3);
            _radioButtonsGrid = InitializeGridPanel(2, 1);
            _ExpressionTextBlock = InitializeExpressionTextBlock();

            AddRadioButtons(_radioButtonsGrid, _radioButtonValues);

            AddButtons(_numberPanel, _mainButtonValues);
            AddButtons(_additionalPanel, _additionalButtonValues);
            AddButtons(_lettersPanel, _variablesName);

            SetGridPosition(_numberPanel, 2, 0);
            SetGridPosition(_additionalPanel, 1, 0);
            SetGridPosition(_lettersPanel, 2, 1);
            SetGridPosition(_radioButtonsGrid, 1, 1);
            SetGridSpan(_ExpressionTextBlock, 1, 2);

            Calculator.Children.Add(_lettersPanel);
            Calculator.Children.Add(_numberPanel);
            Calculator.Children.Add(_additionalPanel);
            Calculator.Children.Add(_radioButtonsGrid);
            Calculator.Children.Add(_ExpressionTextBlock);

            _validator = new UserValidator();
        }

        public void Initialize(ExpressionEvaluator expressionEvaluator)
        {
            _expressionEvaluator = expressionEvaluator;
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            Button pressedButton = (Button)sender;

            switch (pressedButton.Content)
            {
                case "C":
                    ClearExpression();
                    break;
                case "<-":
                    if (_ExpressionTextBlock.Text != String.Empty)
                    {
                        _ExpressionTextBlock.Text = _ExpressionTextBlock.Text
                            .Remove(_ExpressionTextBlock.Text.Count() - 1); ;
                    }
                    break;
                case "Res":
                    string validationResult = _validator.Validate(_ExpressionTextBlock.Text);
                    if (validationResult == "Expression is valid")
                    {
                        double res = _expressionEvaluator.EvaluateExpression(_ExpressionTextBlock.Text);
                        _ExpressionTextBlock.Text = res.ToString();
                    }
                    else
                    {
                        MessageBox.Show(validationResult, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                    break;
                case "Save":
                    var checkedRadioButton = _radioButtonsGrid.Children
                        .OfType<RadioButton>()
                        .FirstOrDefault(n => n.IsChecked==true);
                    if(checkedRadioButton.Content.ToString() == _radioButtonValues[0])
                    {
                        _expressionEvaluator.SaveVariable(_ExpressionTextBlock.Text);
                        MessageBox.Show("Variable " + _ExpressionTextBlock.Text + " has been added!");
                        ClearExpression();
                    }
                    if (checkedRadioButton.Content.ToString() == _radioButtonValues[1])
                    {
                        _expressionEvaluator.SaveFunction(_ExpressionTextBlock.Text);
                        MessageBox.Show("Func " + _ExpressionTextBlock.Text + " has been added!");
                        ClearExpression();
                    }
                    break;
                default:
                    _ExpressionTextBlock.Text += pressedButton.Content.ToString() ;
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
            _ExpressionTextBlock.Text = String.Empty;
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
