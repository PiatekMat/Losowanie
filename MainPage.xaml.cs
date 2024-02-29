using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace ListaZakupów
{
    public partial class MainPage : ContentPage
    {
        private string folderPath = Path.Combine(FileSystem.Current.AppDataDirectory, "Klasy");
        private List<string> klasy = new List<string> { "Klasa A", "Klasa B", "Klasa C" };
        ObservableCollection<Student> uczniowie { get; set; } = new ObservableCollection<Student>();
        List<string> list = new List<string>();
        List<string> obecni = new List<string>();
        int happy;

        private int losuj;

        public MainPage()
        {
            InitializeComponent();
            pickerKlasy.ItemsSource = klasy;
        }

        private void DodajUcznia_Clicked(object sender, EventArgs e)
        {
            string filePath = Path.Combine(folderPath, $"{pickerKlasy.SelectedItem?.ToString()}.txt");
            if (!string.IsNullOrWhiteSpace(txtUczen.Text))
            {
                list.Add(txtUczen.Text);
                string wybranaKlasa = pickerKlasy.SelectedItem?.ToString();
                txtUczen.Text = string.Empty;
                ZapiszUczniow(wybranaKlasa);
                WczytajUczniow_Clicked(this, new EventArgs());
            }
        }

        private void LosujUcznia_Clicked(object sender, EventArgs e)
        {
            if (obecni.Count != 0)
            {
                Random random = new Random();
                int index = random.Next(0, obecni.Count);
                while (list[happy] == obecni[index])
                {
                    index = random.Next(0, obecni.Count);
                }

                string wylosowanyUczen = obecni[index];
                DisplayAlert("Wylosowany uczeń", wylosowanyUczen, "OK");
            }
            else
            {
                DisplayAlert("Błąd", "Brak uczniów do losowania.", "OK");
            }
        }
        private async void Edit_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewPage1(list, pickerKlasy.SelectedItem?.ToString()));
        }
        
        private void WczytajUczniow_Clicked(object sender, EventArgs e)
        {
            int i = 1;
            uczniowie.Clear();
            obecni.Clear();
            list = new List<string>();

            string filePath = Path.Combine(folderPath, $"{pickerKlasy.SelectedItem?.ToString()}.txt");
            if (File.Exists(filePath))
            {
                list = File.ReadAllLines(filePath).ToList();

                foreach (string line in list)
                {
                    Student s = new Student();
                    s.Name = line;
                    s.np = i.ToString();
                    i++;
                    uczniowie.Add(s);
                }
            }
            foreach (string item in list)
            {
              
                obecni.Add(item);
            }
            
            studentsContainer.ItemsSource = uczniowie;

            Random random = new Random();
            int index = random.Next(0, list.Count);
            happy = index;
            obecni.Remove(list[index]);
            index++;
            Happy.Text = index.ToString();
        }

        private void ZapiszUczniow(string klasa)
        {
            string filePath = Path.Combine(folderPath, $"{klasa}.txt");
            try
            {
                File.WriteAllLines(filePath, list);
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Błąd podczas zapisywania uczniów: {ex.Message}", "OK");
            }
        }
        
        class Student
        {
            public string np {  get; set; }
            public string Name { get; set; }
        }
    }
}
