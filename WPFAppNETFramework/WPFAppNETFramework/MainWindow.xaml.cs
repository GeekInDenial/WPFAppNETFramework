using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using Ookii.Dialogs.Wpf;
using ErikE.Shuriken;

namespace WPFAppNETFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _folderDirectory = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Folder_Name_button_Click(object sender, RoutedEventArgs e)
        {
        // Following has info on Vista and before OSes and how to get a file and / or folder dual purpose dialog class
        // https://stackoverflow.com/questions/31059/how-do-you-configure-an-openfiledialog-to-select-folders
            var fInstance = new ErikE.Shuriken.FolderSelectDialog();
            fInstance.Show(); //this works shows "Select Folder" dialog
            //string folderName = fInstance.ToString();
            string folderName = fInstance.FileName;
            _folderDirectory = folderName;
        }

        private void Folder_Name_Initialized(object sender, EventArgs e)
        {
        //TODO Update Folder Name Text with "Selected Folder". Verify Property to use.
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            var instance = new StackBasedIteration();
            if (_folderDirectory != null)
            {
                //TODO: Add filename rather than just Temp.tsv in user's temp directory
                StackBasedIteration.TraverseTree(_folderDirectory);
                string tstr = "See 'Temp.tsv' file in user's temp folder using Windows key + R and type '%temp%' (hit Enter)";
                System.Windows.MessageBox.Show(tstr);
                Application.Current.Shutdown();
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); //irreversible.
        }
    }
}