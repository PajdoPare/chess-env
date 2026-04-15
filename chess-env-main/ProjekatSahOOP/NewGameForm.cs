using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace ProjekatSahOOP
{
    public class VremeOpcija
    {
        public string Tekst { get; set; }
        public double Minuti { get; set; }
        public override string ToString() => Tekst;
        public VremeOpcija(string Tekst, double Minuti)
        { 
            this.Tekst = Tekst;
            this.Minuti = Minuti;
        }
    }

    public class NewGameForm : Form
    {
        public TimeSpan Odabrano { get; private set; } = TimeSpan.FromMinutes(10);
        public Button Confirm;
        ComboBox comboBox;
        VremeOpcija[] options;
        public NewGameForm()
        {
            Text = "Nova igra";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(300, 120);
            Label Naslov = new Label
            {
                Text = "Odaberite vremenski limit:",
                Location = new Point(10, 10),
                Size = new Size(280, 20),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(40, 164, 221),
                Font = new Font("Comic Sans MS", 10, FontStyle.Bold)
            };
            comboBox = new ComboBox
            {
                Location = new Point(10, 40),
                Size = new Size(280, 20),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Comic Sans MS", 10, FontStyle.Regular),
                BackColor = Color.FromArgb(40, 164, 221),
                ForeColor = Color.White
            };
             options = new VremeOpcija[]
            {
                 new VremeOpcija("1 minut  (Bullet)",         1),
                new VremeOpcija("2 minuta (Bullet)", 2),
                new VremeOpcija("3 minuta (Blitz)", 3),
                new VremeOpcija("5 minuta (Blitz)", 5),
                new VremeOpcija("10 minuta (Rapid)", 10),
                new VremeOpcija("15 minuta (Rapid)", 15),
                new VremeOpcija("30 minuta (Classical)", 30),
                new VremeOpcija("60 minuta (Classical)", 60),
                new VremeOpcija("120 minuta (Classical)", 120),
                new VremeOpcija("Bez ograničenja",-1)
            };
            foreach (var option in options)
            {
                comboBox.Items.Add(option.Tekst);
            }
            comboBox.SelectedIndex = 4;
            comboBox.DisplayMember = "Tekst";
            Confirm = new Button
            {
                Text = "Potvrdi",
                Location = new Point(10, 70),
                Size = new Size(280, 30),
                BackColor = Color.FromArgb(40, 164, 221),
                ForeColor = Color.White,
                Font = new Font("Comic Sans MS", 10, FontStyle.Bold)
            };
            Confirm.Click += Confirm_Click;
            Controls.Add(Naslov);
            Controls.Add(comboBox);
            Controls.Add(Confirm);
        }
        void Confirm_Click(object sender, EventArgs e)
        {
            
            var selectedOption = options[comboBox.SelectedIndex];
            Odabrano = selectedOption.Minuti < 0 ? TimeSpan.MaxValue : TimeSpan.FromMinutes(selectedOption.Minuti);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
