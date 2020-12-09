using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace numerosSortidos
{
    public partial class Teste : Window
    {
        List<string> atendimentoCsv = new List<string>();
        public Teste(List<string> listaAtendimento)
        {
            InitializeComponent();
            CriaTelaLista(listaAtendimento);
            atendimentoCsv = listaAtendimento;
        }

        private void CriaTelaLista(List<string> atendimento)
        {
            try
            {
                ListBox lista = new ListBox();
                Button exportar = new Button();

                exportar.Click += new RoutedEventHandler(Exportar_Click);
                exportar.Content = "Exportar";
                exportar.VerticalAlignment = VerticalAlignment.Top;
                exportar.HorizontalAlignment = HorizontalAlignment.Left;
                lista.Items.Add(exportar);

                foreach(var i in atendimento)
                lista.Items.Add(i);

                stkLista.Children.Add(lista);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Exportar_Click(object sender, RoutedEventArgs e)
        {
            ExportarCsv.ExportaCsv(atendimentoCsv, "Lista Atendimento");
        }

        public static class ExportarCsv
        {
            public static void ExportaCsv<T>(List<T> listaGenerica, string nomeArquivo)
            {
                var sb = new StringBuilder();
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var finalPath = Path.Combine(basePath, nomeArquivo + ".csv");
                var info = typeof(T).GetProperties();
                if (!File.Exists(finalPath))
                {
                    var file = File.Create(finalPath);
                    file.Close();
                    TextWriter sw = new StreamWriter(finalPath, true);
                    sw.Write(sb.ToString());
                    sw.Close();
                }
                foreach (var obj in listaGenerica)
                {
                    sb = new StringBuilder();
                    sb.AppendLine(obj.ToString());
                    TextWriter sw = new StreamWriter(finalPath, true);
                    sw.Write(sb.ToString());
                    sw.Close();
                }
            }
        }
    }
}
