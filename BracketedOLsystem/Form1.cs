using OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LSystem
{
    /// <summary>
    /// http://algorithmicbotany.org/papers/#abop
    /// </summary>
    public partial class Form1 : Form
    {
        LSystem _lSystem;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _lSystem = new LSystem();
            tbGrammer.Text = "X,F[+X]F[-X]+X\r\nF,FF";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _lSystem = new LSystem();
            //_lSystem.Init(n: (int)nbrNum.Value, delta: (float)nbrDelta.Value);
            _lSystem.LoadProductions(this.tbGrammer.Text);
            _lSystem.Render(this.pictureBox1.CreateGraphics(),
                axiom: this.tbAxiom.Text,
                px: this.pictureBox1.Width / 2,
                py: 10,
                prad: 90,
                height: this.pictureBox1.Height,
                branchLength: (float)this.nbrLength.Value,
                drawWidth: (float)this.nbrWidth.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //_lSystem.Init(n: (int)nbrNum.Value, delta: (float)nbrDelta.Value);
            _lSystem.LoadProductions(this.tbGrammer.Text);
            _lSystem.RenderRndColorRewriting(this.pictureBox1.CreateGraphics(),
                axiom: this.tbAxiom.Text,
                px: this.pictureBox1.Width / 2,
                py: 10,
                prad: 90,
                height: this.pictureBox1.Height,
                BranchLength: (float)this.nbrLength.Value);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog()== DialogResult.OK)
            {
                _lSystem.BranchColor = this.colorDialog1.Color;
                this.label1.BackColor = this.colorDialog1.Color;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                _lSystem.LeafColor = this.colorDialog1.Color;
                this.label2.BackColor = this.colorDialog1.Color;
            }
        }
    }
}
