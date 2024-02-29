using System;
using System.Collections.Generic;

namespace ListaZakupów;

public partial class NewPage1 : ContentPage
{
    private List<string> uczniowie;
    private string klasa;

    public NewPage1(List<string> uczniowie, string klasa)
    {
        InitializeComponent();
        this.uczniowie = uczniowie;
        lstUczniowie.ItemsSource = uczniowie;
        this.klasa = klasa;
    }

    private void UsunUcznia_Clicked(object sender, EventArgs e)
    {
        if (lstUczniowie.SelectedItem != null)
        {
            uczniowie.Remove(lstUczniowie.SelectedItem.ToString());
            lstUczniowie.ItemsSource = null;
            lstUczniowie.ItemsSource = uczniowie;
            ZapiszUczniow();
        }
    }
    private async void edytuj_Clicked(object sender, EventArgs e)
    {
        if (lstUczniowie.SelectedItem != null)
        {
            var _person = lstUczniowie.SelectedItem.ToString();
            int editedIndex = uczniowie.IndexOf(_person);

            string NewStudent = await DisplayPromptAsync($"Edytujesz ucznia: {_person}", "", initialValue: $"{_person}");
            if(NewStudent != null)
            {
uczniowie[editedIndex] = NewStudent;
            lstUczniowie.ItemsSource = null;
            lstUczniowie.ItemsSource = uczniowie;
            ZapiszUczniow();
            }
        }
    }
    private void ZapiszUczniow()
    {
        string folderPath = Path.Combine(FileSystem.Current.AppDataDirectory, "Klasy");

        string filePath = Path.Combine(folderPath, $"{klasa}.txt");
        try
        {
            File.WriteAllLines(filePath, uczniowie);
        }
        catch (Exception ex)
        {
            DisplayAlert("Błąd", $"Błąd podczas zapisywania uczniów: {ex.Message}", "OK");
        }
    }
    private void PersonEdit()
    {
        
    }

}