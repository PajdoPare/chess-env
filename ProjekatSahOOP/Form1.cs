using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjekatSahOOP
{

    public partial class Form1 : Form
    {
        GameState GS;
        GrafikaTabla GT;
        Kvadrat? Selected;
        List<Kvadrat> Legalni;
        ListBox ListPotez;
        Label StatusO, BeliU, CrniU;
        Button NewGame;

        public Form1()
        {
            InitializeComponent();
            Text = "Sah";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            ImageMapping.Load();
            GS = new GameState();

            GS.PromocijaObavezna += GS_Promo;
            GT = new GrafikaTabla();
            Deselect();
            SidePanel();
            GT.GS = GS;
            GT.Size = new Size(800, 800);
            GT.Klik += GT_Klik;
            Controls.Add(GT);
            UpdateUI();
        }
        int PieceVal(Tip t)
        {
            switch(t)
            {
                case Tip.Pesak:
                    return 1;
                case Tip.Skakac:
                    return 3;
                case Tip.Lovac:
                    return 3;
                case Tip.Top:
                    return 5;
                case Tip.Kraljica:
                    return 9;
                default:
                    return 0;
            }
        }

    void Deselect()
        {
            Selected = null;
            GT.Selected = null;
            GT.Legalni = new List<Kvadrat>();
            Legalni = new List<Kvadrat>();
        }
        void GS_Promo(Kvadrat P, Kvadrat O)
        {
            try
            {
                PromotionForm PF = new PromotionForm(GS.CijiPotez);
                PF.ShowDialog();
                Tip t = PF.Chosen;
                GS.Unapredjenje(P, O, t);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void SidePanel()
        {
            Panel sidePanel = new Panel
            {
                Location = new Point(800, 0),
                Size = new Size(300, 800),
                BackColor = Color.LightGray
            };
            StatusO = new Label
            {
                Location = new Point(10, 10),
                Size = new Size(200, 30),
                Font = new Font("Comic Sans", 12, FontStyle.Bold),
                Text = "Igrac Beli je na potezu."
            };
            ListPotez = new ListBox
            {
                Location = new Point(10, 50),
                Size = new Size(200, 500),
                Font = new Font("Comic Sans", 10, FontStyle.Regular),
                BackColor = Color.FromArgb(40, 164, 221),
                ForeColor = Color.White,
                ScrollAlwaysVisible = false,
            };
            NewGame = new Button
            {
                Location = new Point(10, 700),
                Size = new Size(200, 40),
                Text = "Nova igra",
                Font = new Font("Comic Sans", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(40, 164, 221),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            NewGame.Click += (s, e) =>
            {
                GS = new GameState();
                GT.GS = GS;
                Deselect();
                ListPotez.Items.Clear();
                UpdateUI();
                GS.PromocijaObavezna += GS_Promo;
            };
            BeliU = new Label
            {
                Location = new Point(10, 560),
                Size = new Size(270, 30),
                Font = new Font("Comic Sans", 12, FontStyle.Bold),
                Text = "Beli: 0"
            };
            CrniU = new Label
            {
                Location = new Point(10, 600),
                Size = new Size(270, 30),
                Font = new Font("Comic Sans", 12, FontStyle.Bold),
                Text = "Crni: 0"
            };
            sidePanel.Controls.Add(BeliU);
            sidePanel.Controls.Add(CrniU);
            sidePanel.Controls.Add(StatusO);
            sidePanel.Controls.Add(ListPotez);
            sidePanel.Controls.Add(NewGame);
            Controls.Add(sidePanel);
            ClientSize = new Size(1020, 800);
        }
            private void GT_Klik(Kvadrat obj)
        {
            if (Selected == null)
            {
                Piece p = GS.Board.GetPiece(obj);
                if (p == null || p.beli != GS.CijiPotez) return;
                Selected = obj;
                Legalni = GS.LegalMoves(obj);
                GT.Dock = DockStyle.Left;
                GT.Selected = Selected;
                GT.Legalni = Legalni;
            }
            else if (Legalni.Contains(obj))
            {
                GS.PokusajPotez(Selected.Value, obj);
                Deselect();
                UpdateUI();
            }
            else
            {
                Piece p = GS.Board.GetPiece(obj);
                if (p == null || p.beli != GS.CijiPotez)
                {
                    Deselect();
                }
                else
                {
                    Selected = obj;
                    Legalni = GS.LegalMoves(obj);
                    GT.Selected = Selected;
                    GT.Legalni = Legalni;
                }

            }
            GT.Invalidate();
        }
        void UpdateUI()
        {
            GS.Flipped = !GS.Flipped;
            GT.LastMove = GS.MoveHistory.LastOrDefault();
            GT.Invalidate();
            UpdateStatusO();
            UpdateMoveHistory();
            UpdateCapturedPieces();
            ListPotez.ForeColor = GS.CijiPotez ? Color.White : Color.Black;

        }
        void UpdateCapturedPieces()
        {
            BeliU.Text = "Beli: " + CapturedPieces(GS.BeliUzeo);
            CrniU.Text = "Crni: " + CapturedPieces(GS.CrniUzeo);
        }
        string CapturedPieces(List<Piece> uhv)
        {
            List<Piece> pieces = uhv.OrderByDescending(p => PieceVal(p.T)).ToList();
            return string.Join("", pieces.Select(p => p.ToString()));
        }
        void UpdateMoveHistory()
        {
            if (GS.MoveHistory.Count == 0) return;
            Potez pos = GS.MoveHistory.Last();
            ListPotez.Items.Add($"{pos.Polazno.ToString()} -> {pos.Odredisno.ToString()}");
            ListPotez.TopIndex = ListPotez.Items.Count - 1;
        }
        void UpdateStatusO()
        {
            string igrac = GS.CijiPotez ? "Beli" : "Crni";
            string opp = GS.CijiPotez ? "Crni" : "Beli";
            if (GS.St == Status.Normal)
            {
                StatusO.Text = "Igrac " + igrac + " je na potezu.";
            }
            else if (GS.St == Status.Sah)
            {
                StatusO.Text = "Igrac " + igrac + " je u sahu.";
            }
            else if (GS.St == Status.Mat)
            {
                StatusO.Text = "Mat! Igrac " + opp + "\nje pobedio";
            }
            else if (GS.St == Status.Pat)
            {
                StatusO.Text = "Pat! Remi.";
            }
        }
    }
}
