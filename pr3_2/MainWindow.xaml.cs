using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace pr3_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EquationType_Checked(object sender, RoutedEventArgs e)
        {
            InputFieldsPanel.Visibility = Visibility.Visible; // показывать контейнер с полями ввода
            CLabel.Visibility = (sender == QuadraticRadioButton) ? Visibility.Visible : Visibility.Collapsed;
            CTextBox.Visibility = (sender == QuadraticRadioButton) ? Visibility.Visible : Visibility.Collapsed;
            if (sender != QuadraticRadioButton)
            {
                CTextBox.Text = ""; // очищаем поле c, чтобы не было путаницы
            }

        }



        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!LinearRadioButton.IsChecked.Value && !QuadraticRadioButton.IsChecked.Value)
                {
                    MessageBox.Show("Выберите тип уравнения.");
                    return;
                }

                if (string.IsNullOrEmpty(ATextBox.Text) || string.IsNullOrEmpty(BTextBox.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните поля 'a' и 'b'.");
                    return;
                }

                double a = double.Parse(ATextBox.Text);
                double b = double.Parse(BTextBox.Text);
                double c = 0;

                if (QuadraticRadioButton.IsChecked.Value)
                {
                    if (string.IsNullOrEmpty(CTextBox.Text))
                    {
                        MessageBox.Show("Пожалуйста, заполните поле 'c'.");
                        return;
                    }
                    c = double.Parse(CTextBox.Text);
                }

                if (QuadraticRadioButton.IsChecked.Value && a == 0) // проверка для квадратного уравнения
                {
                    MessageBox.Show("При a = 0 квадратное уравнение становится линейным. Решаем его как линейное.");
                    if (b == 0 & c != 0)
                    {
                        ResultTextBox.Text = "Решений нет";
                        return;
                    }
                    if (b == 0 & c == 0)
                    {
                        ResultTextBox.Text = "Бесконечно много решений";
                        return;
                    }

                    else
                    {
                        double x = -c / b; // решение линейного уравнения bx + c = 0
                        ResultTextBox.Text = x.ToString();
                    }
                   
                    return;
                }

                if (LinearRadioButton.IsChecked.Value)
                {
                    if (a == 0)
                    {
                        if (b == 0)
                        {
                            ResultTextBox.Text = "Бесконечно много решений";
                            return;
                        }
                        else
                        {
                            ResultTextBox.Text = "Решений нет";
                            return;
                        }
                    }
                    double x = -b / a;
                    ResultTextBox.Text = x.ToString();
                }
                else if (QuadraticRadioButton.IsChecked.Value)
                {
                    double discriminant = b * b - 4 * a * c;

                    if (discriminant < 0)
                    {
                        ResultTextBox.Text = "Нет действительных корней";
                    }
                    else if (discriminant == 0)
                    {
                        double x = -b / (2 * a);
                        ResultTextBox.Text = "x = " + x.ToString();
                    }
                    else
                    {
                        double x1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                        double x2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                        ResultTextBox.Text = "x1 = " + x1.ToString() + "; x2 = " + x2.ToString();
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите числовые значения");
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("Ошибка: Деление на ноль"); // более точное сообщение
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла непредвиденная ошибка: " + ex.Message);
            }
        }

        // обработчик, который не позволяет вводить в TextBox ничего, кроме чисел
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
