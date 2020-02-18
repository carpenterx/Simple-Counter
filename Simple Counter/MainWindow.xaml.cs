using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Simple_Counter
{
    public partial class MainWindow : Window
    {
        private bool deleteButtons = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void IncrementCounter(object sender, RoutedEventArgs e)
        {
            if(deleteButtons)
            {
                var btn = (sender as Button);
                canvas.Children.Remove(btn);
            }
            else
            {
                var secondText = (((sender as Button).Content as StackPanel).Children[1] as TextBlock);
                object tag = secondText.Tag;
                int count = Convert.ToInt32(tag);
                count++;
                secondText.Tag = count;
                secondText.Text = btnLabel2.Text + count;
            }
        }

        private void AddNewButton(object sender, RoutedEventArgs e)
        {
            Button btn = new Button();
            btn.Click += new RoutedEventHandler(IncrementCounter);
            if (btnWidth.Text != "")
            {
                btn.Width = Convert.ToInt32(btnWidth.Text);
            }
            if (btnHeight.Text != "")
            {
                btn.Height = Convert.ToInt32(btnHeight.Text);
            }
            if (btnX.Text != "")
            {
                Canvas.SetLeft(btn, Convert.ToInt32(btnX.Text));
            }
            if (btnY.Text != "")
            {
                Canvas.SetTop(btn, Convert.ToInt32(btnY.Text));
            }
            if(btnColor.Text != "")
            {
                btn.Background = new BrushConverter().ConvertFromString(btnColor.Text) as SolidColorBrush;
            }
            
            StackPanel innerStack = new StackPanel();
            innerStack.Orientation = Orientation.Vertical;

            TextBlock t1 = new TextBlock();
            TextBlock t2 = new TextBlock();
            t1.HorizontalAlignment = HorizontalAlignment.Center;
            t1.Text = btnLabel1.Text;
            t2.HorizontalAlignment = HorizontalAlignment.Center;
            t2.Text = btnLabel2.Text;
            t2.Tag = 0;
            if(btnTxtColor.Text != "")
            {
                t1.Foreground = new BrushConverter().ConvertFromString(btnTxtColor.Text) as SolidColorBrush;
                t2.Foreground = new BrushConverter().ConvertFromString(btnTxtColor.Text) as SolidColorBrush;
            }

            innerStack.Children.Add(t1);
            innerStack.Children.Add(t2);

            btn.Content = innerStack;

            canvas.Children.Add(btn);
        }

        private void ToggleDeletion(object sender, RoutedEventArgs e)
        {
            CheckBox delBtn = (sender as CheckBox);
            if(delBtn.IsChecked == true)
            {
                delBtn.Content = "Deleting";
                deleteButtons = true;
            }
            else
            {
                delBtn.Content = "Delete";
                deleteButtons = false;
            }
        }

        private void SaveData(object sender, RoutedEventArgs e)
        {
            List<string> row1 = new List<string>();
            List<string> row2 = new List<string>();
            foreach (var child in canvas.Children)
            {
                var firstText = (((child as Button).Content as StackPanel).Children[0] as TextBlock);
                var secondText = (((child as Button).Content as StackPanel).Children[1] as TextBlock);
                row1.Add(firstText.Text);
                row2.Add(secondText.Text);
            }

            var csv = new StringBuilder();
            csv.AppendLine(String.Join(",", row1));
            csv.AppendLine(String.Join(",", row2));

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".csv"; // Default file extension
            dlg.Filter = "Text CSV|*.csv"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                File.WriteAllText(dlg.FileName, csv.ToString());
            }
        }
    }
}
