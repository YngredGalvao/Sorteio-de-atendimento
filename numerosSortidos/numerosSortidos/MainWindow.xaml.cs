using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace numerosSortidos
{
    public partial class MainWindow : Window
    {
        #region variaveis
        int[] moradores;
        string[] torre;
        StackPanel morador;
        StackPanel stkTorre;
        TextBlock nomeTorre;
        CheckBox p1;
        TextBlock nMorador;
        CheckBox p2;
        Button sortear;
        List<string> listaAtendimentoP1;
        List<string> listaAtendimentoP2;
        List<string> listaSemPrioridade;
        List<string> listaAtendimento;
        List<string> ordemAtendimento;
        Grid prioridade;
        IEnumerable<string> distinctMoradores;
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            CriaTela();
        }
        private void CriaTela()
        {
            try
            {
                morador = new StackPanel();
                moradores = new int[19] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
                torre = new string[3] { "TorreA", "TorreB", "TorreC" };

                foreach (var t in torre)
                {
                    stkTorre = new StackPanel();
                    stkTorre.Name = t;
                    stkTorre.Orientation = Orientation.Vertical;
                    stkTorre.Margin = new Thickness(15);
                    stkTorre.Width = 160;
                    nomeTorre = new TextBlock();
                    nomeTorre.Text = t;
                    nomeTorre.HorizontalAlignment = HorizontalAlignment.Stretch;
                    nomeTorre.TextAlignment = TextAlignment.Center;
                    nomeTorre.Margin = new Thickness(3);

                    prioridade = new Grid();
                    prioridade.HorizontalAlignment = HorizontalAlignment.Center;
                    TextBlock prd1 = new TextBlock();
                    prd1.Text = "Prioridade 1";
                    prd1.HorizontalAlignment = HorizontalAlignment.Left;
                    prd1.Padding = new Thickness(0, 0, 100, 0);
                    TextBlock prd2 = new TextBlock();
                    prd2.Text = "Prioridade 2";
                    prd2.HorizontalAlignment = HorizontalAlignment.Right;

                    prioridade.Children.Add(prd1);
                    prioridade.Children.Add(prd2);

                    stkTorre.Children.Add(nomeTorre);

                    stkTorre.Children.Add(prioridade);

                    foreach (var m in moradores)
                    {
                        morador = new StackPanel();
                        morador.Name = $"AP{m}";
                        morador.Orientation = Orientation.Horizontal;
                        morador.Margin = new Thickness(20, 5, 5, 5);
                        p1 = new CheckBox();
                        p1.Name = "p1";
                        nMorador = new TextBlock();
                        nMorador.Margin = stkTorre.Children.Count <= 10 ? new Thickness(30, 0, 30, 0) : new Thickness(30, 0, 23, 0);
                        nMorador.Text = morador.Name;
                        p2 = new CheckBox();
                        p2.Name = "p2";
                        morador.Children.Add(p1);
                        morador.Children.Add(nMorador);
                        morador.Children.Add(p2);
                        stkTorre.Children.Add(morador);
                    }

                    stkTela.Children.Add(stkTorre);
                }
                sortear = new Button();
                sortear.Click += Sortear_Click;
                sortear.HorizontalAlignment = HorizontalAlignment.Right;
                sortear.VerticalAlignment = VerticalAlignment.Bottom;
                sortear.Height = 20;
                sortear.Width = 100;
                sortear.Margin = new Thickness(5);
                sortear.Content = "Sortear";
                tela.Children.Add(sortear);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Sorteio()
        {
            int i = 1;
            try
            {
                listaAtendimentoP1 = new List<string>();
                listaAtendimentoP2 = new List<string>();
                listaSemPrioridade = new List<string>();
                listaAtendimento = new List<string>();

                foreach (var t in stkTela.Children.OfType<StackPanel>())
                {
                    foreach (var m in t.Children.OfType<StackPanel>())
                    {
                        foreach (var c in m.Children.OfType<CheckBox>())
                        {
                            if (c.Name == "p1" && c.IsChecked == true)
                            {
                                string proximo = $"{m.Name} - {t.Name}";
                                listaAtendimentoP1.Add(proximo);
                            }
                            if (c.Name == "p2" && c.IsChecked == true)
                            {
                                string proximo = $"{m.Name} - {t.Name}";
                                listaAtendimentoP2.Add(proximo);
                            }
                            if (c.IsChecked == false)
                            {
                                string proximo = $"{m.Name} - {t.Name}";
                                listaSemPrioridade.Add(proximo);
                            }
                        }
                    }
                }
                listaAtendimentoP1 = listaAtendimentoP1.OrderBy(a => Guid.NewGuid()).ToList();
                listaAtendimentoP2 = listaAtendimentoP2.OrderBy(a => Guid.NewGuid()).ToList();
                listaSemPrioridade = listaSemPrioridade.OrderBy(a => Guid.NewGuid()).ToList();

                if (listaAtendimentoP1.Count >= 1)
                    foreach (var x in listaAtendimentoP1)
                        listaAtendimento.Add(x);
                if (listaAtendimentoP2.Count >= 1)
                    foreach (var x in listaAtendimentoP2)
                        listaAtendimento.Add(x);
                if (listaSemPrioridade.Count >= 1)
                    foreach (var x in listaSemPrioridade)
                        listaAtendimento.Add(x);

                ordemAtendimento = new List<string>();

                distinctMoradores = listaAtendimento.Distinct();

                foreach (var y in distinctMoradores)
                {
                    string moradorOrdemAtendimento = $"{i} - {y}";
                    ordemAtendimento.Add(moradorOrdemAtendimento);
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Sortear_Click(object sender, RoutedEventArgs e)
        {
            Sorteio();
            Teste tela = new Teste(ordemAtendimento);
            tela.ShowDialog();
        }
    }
}
