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
    public partial class SettingsWindow : Window
    {
        UIHandler uiHandler = new UIHandler();

        public string language = "";

        public SettingsWindow()
        {
            InitializeComponent();

            uiHandler.AddGrid(SettingsMain, "ui_settings_main");

            uiHandler.ShowGrid("ui_settings_main");

            SettingsMain_LanguageList.Items.Clear();
            SettingsMain_LanguageList.Items.Add("en");
            SettingsMain_LanguageList.Items.Add("cz");
            SettingsMain_LanguageList.Items.Add("de");
            SettingsMain_LanguageList.SelectedIndex = 0;
        }

        public void Translate(LanguageHandler lh)
        {
            Lbl_SettingsMain_Language.Content = lh.SETTINGS_LANGUAGE;
            SettingsMain_Save.Content = lh.GENERAL_SAVE;

            Title = lh.SETTINGS_WINDOW_TITLE;
        }

        private void SettingsMain_Save_Click(object sender, RoutedEventArgs e)
        {
            language = (string)SettingsMain_LanguageList.SelectedItem;
            Close();
        }
    }
}
