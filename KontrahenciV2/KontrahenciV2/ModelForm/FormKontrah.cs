﻿using KontrahenciV2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KontrahenciV2.ModelForm
{
    public partial class FormKontrah : Form
    {
        public FormKontrah()
        {
            InitializeComponent();
            UstawComboBoxy();
        }

        public FormKontrah(Kontrahent kontrahent)
        {
            InitializeComponent();
        }

        private void UstawComboBoxy()
        {
            comboBoxStatus.DataSource = Enum.GetValues(typeof(Status));
            comboBoxFormaZaplaty.DataSource = Enum.GetValues(typeof(FormaZaplaty));
        }

        private void CheckBoxAdresKor_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxAdresKor.Enabled = checkBoxAdresKor.Checked ? true : false;
        }

        private void ButtonAnuluj_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonZapisz_Click(object sender, EventArgs e)
        {
            Kontrahent kontrahent = GenerujKontrahenta();
            DodajKontrahentaDoBazy(kontrahent);
        }

        private void DodajKontrahentaDoBazy(Kontrahent kontrahent)
        {
            var status = SQLiteAdapter.DodajKontrahenta(kontrahent);
            if (!status)
            {
                MessageBox.Show("Błąd przy dodawaniu kontrahenta");
            }
        }

        private Kontrahent GenerujKontrahenta()
        {
            ParsowanieDanych(out FormaZaplaty forma, out Status status, out int terminZaplaty);
            Kontrahent kontrahent = new Kontrahent()
            {
                Nazwa = textBoxNazwa.Text,
                NazwaSkrocona = textBoxNazwaSkr.Text,
                Nip = textBoxNip.Text,
                Regon = textBoxRegon.Text,
                Telefon = textBoxTelefon.Text,
                Email = textBoxEMail.Text,
                AdresSiedziby = new Adres(textBoxMiejscowosc1.Text, textBoxUlica1.Text, textBoxNrD1.Text, textBoxNrM1.Text, textBoxKodP1.Text),
                AdresKorespondencyjny = new Adres(textBoxMiejscowosc2.Text, textBoxUlica2.Text, textBoxNrD2.Text, textBoxNrM2.Text, textBoxKodP2.Text),
                KontoBankowe = new KontoBankowe(textBoxNazwaBanku.Text, textBoxNrKonta.Text),
                TerminZaplaty = terminZaplaty,
                FormaZaplaty = forma,
                Status = status
            };
            return kontrahent;
        }

        private void ParsowanieDanych(out FormaZaplaty forma, out Status status, out int terminZaplaty)
        {
            Enum.TryParse(comboBoxFormaZaplaty.Text, out forma);
            Enum.TryParse(comboBoxFormaZaplaty.Text, out status);
            terminZaplaty = Int32.Parse(textBoxTermin.Text);
        }
    }
}
