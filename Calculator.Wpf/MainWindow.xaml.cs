using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Calculator.Wpf
{

    public partial class MainWindow : Window
    {
        private string textBoxVal = string.Empty;
        private readonly char[] mainButtonValues = new[] { '1', '2', '3', '+', '4', '5', '6', '-', '7', '8', '9', '*', '0', '.', 'C', '/' };
        private readonly string[] additionalButtonValues = new[] { "<-", "=", "(", ")", "->" };
        private readonly string[] variablesName = new[] { "x", "y", "z", "i", "j", "k", "f",",","Save" };
        //private readonly string[] toolbarValues = new[] { "Variables", "Functions" };
        private ListBox VariablesListBox = new ListBox();

        Dictionary<string, string> variablesDictionary = new Dictionary<string, string>();

        public MainWindow()
        {
            InitializeComponent();


            Grid numberPanel = InitializeGridPanel(4, 4);
            Grid additionalPanel = InitializeGridPanel(1, 5);
            InitializeVariablePanel();

            AddButtons(numberPanel, mainButtonValues);
            AddButtons(additionalPanel, additionalButtonValues);

            SetGridPosition(numberPanel, 3, 0);
            SetGridPosition(additionalPanel, 2, 0);

            Calculator.Children.Add(numberPanel);
            Calculator.Children.Add(additionalPanel);
            //Calculator.Children.Add(InitializeToolbar());
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
                        ExpressionTextBox.Text = textBoxVal;
                    }
                    break;
                case "Save":
                    SaveVariable();
                    break;
                default:
                    textBoxVal += pressedButton.Content;
                    ExpressionTextBox.Text = textBoxVal;
                    break;
            }

        }
        private void SetGridPosition(FrameworkElement panel, int row, int colomn)
        {
            Grid.SetRow(panel, row);
            Grid.SetColumn(panel, colomn);
        }
        void ClearExpression()
        {
            textBoxVal = string.Empty;
            ExpressionTextBox.Text = textBoxVal;
        }
        private void SaveVariable()
        {
            string[] variableKeyValue = ExpressionTextBox.Text.Split('=');
            variablesDictionary.Add(variableKeyValue[0], variableKeyValue[1]);
            ClearExpression();
            AddVariableToListBox(variableKeyValue[0]);
        }
        private void AddVariableToListBox(string varName)
        {
            Grid grid = InitializeGridPanel(1, 3);

            Button removeBtn = new Button();
            Button variableBtn = new Button();
            TextBlock variableValueTextBlock = new TextBlock();

            removeBtn.Content = "Delete";
            variableBtn.Content = varName;
            removeBtn.Click += DeleteVar;
            variableBtn.Click += InputButton_Click;

            variableValueTextBlock.Text = variablesDictionary[varName];

            Grid.SetColumn(variableValueTextBlock, 1);
            Grid.SetColumn(removeBtn, 2);

            grid.Children.Add(variableBtn);
            grid.Children.Add(variableValueTextBlock);
            grid.Children.Add(removeBtn);

            VariablesListBox.Items.Add(grid);
        }
        private void DeleteVar(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            Grid grid = (Grid)b.Parent;
            Button varB = (Button)grid.Children[0];
            variablesDictionary.Remove(varB.Content.ToString());
            grid.Children.Clear();
            ListBox varLB = (ListBox)grid.Parent;
            varLB.Items.Remove(grid);
        }

        private void InitializeVariablePanel()
        {
            ColumnDefinition variableColomn = new ColumnDefinition();
            variableColomn.Width = new GridLength(2, GridUnitType.Star);
            Calculator.ColumnDefinitions.Add(variableColomn);

            Grid VariablesNamePanel = InitializeGridPanel(3, 3);
            AddButtons(VariablesNamePanel, variablesName);
            SetGridPosition(VariablesNamePanel, 2, 1);
            SetGridPosition(VariablesListBox, 3, 1);
            Calculator.Children.Add(VariablesNamePanel);
            Calculator.Children.Add(VariablesListBox);

        }
        private void SetGridSpan(FrameworkElement panel, int rowSpan, int colomnSpan)
        {
            Grid.SetColumnSpan(panel, rowSpan);
            Grid.SetColumnSpan(panel, colomnSpan);
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
                    numberBtn.Content = operations[index];
                    numberBtn.Margin = new Thickness(1, 3, 1, 3);
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
        //private ToolBar InitializeToolbar()
        //{
        //    ToolBar menu = new ToolBar();
        //    foreach (var item in toolbarValues)
        //    {
        //        RadioButton checkBox = new RadioButton();
        //        checkBox.Checked += RadioButton_Checked;
        //        checkBox.Content = item;
        //        menu.Items.Add(checkBox);
        //        SetGridSpan(menu, 1, 4);
        //    }
        //    return menu;
        //}

        //private void RadioButton_Checked(object sender, RoutedEventArgs e)
        //{
        //}


    }

}
