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
using Forms = System.Windows.Forms;

namespace FileCreator
{

    public partial class MainWindow : Window
    {
        private readonly Creator _creator = new Creator();
        private readonly List<string> fileTypes = new List<string> { "WORD", "BAT", "CMD", "SH" };
        public MainWindow()
        {
            InitializeComponent();
            fileTypeBox.ItemsSource = fileTypes;
        }

        private void DirectroryButton_Click(object sender, RoutedEventArgs e)
        {
            using (Forms.FolderBrowserDialog openDirectoryDialog = new Forms.FolderBrowserDialog())
            {
                openDirectoryDialog.ShowDialog();
                DirectoryBox.Text = openDirectoryDialog.SelectedPath;
            }
        }

        private void CreateFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DirectoryBox.Text == "" || FileNameBox.Text == "") return;

                if (!Int32.TryParse(FileCount.Text, out int fileCounts)) return;

                List<FileData> allFiles = new List<FileData>();
                for (int x = 1; x <= fileCounts; x++)
                    allFiles.Add(new FileData
                    {
                        FileName = String.Format(FileNameBox.Text + x),
                        FileDirectory = DirectoryBox.Text,
                        FileContext = String.Format(FileContains.Text + "#" + x)
                    });

                _creator.CreateFiles(allFiles, fileTypeBox.SelectedItem.ToString(), (bool) ArchiveCheckBox.IsChecked);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
