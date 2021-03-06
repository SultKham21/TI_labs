using Microsoft.Win32;
using System;
using System.Text;
using System.Windows;

namespace lab_4
{

    public partial class MainWindow : Window
    {
        public static string srcPath;
        public static string destPath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void cipher_Click(object sender, RoutedEventArgs e)
        {
            if (DataIsCorrect())
            {
                ulong inputKey = Convert.ToUInt64(key.Text);
                LFSR.register = inputKey;

                LFSR.Crypt(srcPath, destPath);
                MessageBox.Show("The file was successfully encrypted");
            }
        }

        private void btnSrc_Click(object sender, RoutedEventArgs e)
        {
            string path = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                path = openFileDialog.FileName;
            }
            srcPath = path;
            src.Text = srcPath;
        }

        private void btnDest_Click(object sender, RoutedEventArgs e)
        {
            string path = "";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                path = saveFileDialog.FileName;
            }

            destPath = path;
            dest.Text = destPath;
        }
        private bool DataIsCorrect()
        {
            try
            {
                ulong data = Convert.ToUInt64(key.Text);
                return true;
            }
            catch
            {
                MessageBox.Show("You have inputed incorrect data");
                return false;
            }
        }
    }
}
