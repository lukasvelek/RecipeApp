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
using System.Windows.Shapes;

namespace RecipeApp
{
    /// <summary>
    /// Interakční logika pro SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public bool isOpened = true;

        public RecipeAppBackend backend;

        public SettingsWindow()
        {
            InitializeComponent();
        }

        public void LoadUI()
        {
            btn_settings_save.Content = backend.GetText("btn_settings_save");
        }

        public void LoadLanguages()
        {
            List<Language> languages = backend.ReturnLanguagesFromFile();

            int x = 0;

            int si = -1;

            foreach (Language language in languages)
            {
                SettingsLanguageList.Items.Add(language);

                if(backend.currentLanguage == language)
                {
                    si = x;
                }

                x++;
            }

            SettingsLanguageList.SelectedIndex = si;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            isOpened = false;
        }

        private void btn_settings_save_Click(object sender, RoutedEventArgs e)
        {
            backend.UpdateConfig((Language)SettingsLanguageList.SelectedItem);
        }
    }
}
