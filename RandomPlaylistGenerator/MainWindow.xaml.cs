using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace RandomPlaylistGenerator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string SourceFolderPath;
        string DestinationFolderPath;
        string[] FoundFiles;
        int AvailableFiles;
        int RequiredFiles;
        Random RandomObject = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void SourceDirBrowse_Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog SrcFolderDialog = new FolderBrowserDialog();
            if (SrcFolderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SourceFolderPath = SrcFolderDialog.SelectedPath;
                SourceFolderPath += "\\";
                SourceDirPath_TextBox.Text = SourceFolderPath;
            }
            FoundFiles = Directory.GetFiles(SourceFolderPath, @"*.mp3");
            AvailableFiles = FoundFiles.Length;
            AvailableFiles_Label.Content = AvailableFiles;
        }

        private void DestinationDirBrowse_Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog DestFolderDialog = new FolderBrowserDialog();
            if (DestFolderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DestinationFolderPath = DestFolderDialog.SelectedPath;
                DestinationFolderPath += "\\";
                DestinationDirPath_TextBox.Text = DestinationFolderPath;
            }
        }

        private void Generate_Button_Click(object sender, RoutedEventArgs e)
        {
            int RandomNumber;
            RequiredFiles = Convert.ToInt32(AmountOfFiles_TextBox.Text);
            for (int i = 0; i < RequiredFiles; i++)
            {
                RandomNumber = RandomObject.Next(0, AvailableFiles);
                string ChosenFile = FoundFiles[RandomNumber].Substring(SourceFolderPath.Length);
                try
                {
                    switch (FilesAction_ComboBox.SelectedIndex)
                    {
                        case 0:
                            File.Copy(System.IO.Path.Combine(SourceFolderPath, ChosenFile), System.IO.Path.Combine(DestinationFolderPath, ChosenFile), true);
                            break;
                        case 1:
                            File.Move(System.IO.Path.Combine(SourceFolderPath, ChosenFile), System.IO.Path.Combine(DestinationFolderPath, ChosenFile));
                            break;
                        default:
                            File.Copy(System.IO.Path.Combine(SourceFolderPath, ChosenFile), System.IO.Path.Combine(DestinationFolderPath, ChosenFile), true);
                            break;
                    }
                }
                catch (FileNotFoundException fex)
                {
                    continue;
                }
            }
        }
    }
}
