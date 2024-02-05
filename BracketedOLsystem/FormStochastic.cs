using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSystem
{
    public partial class FormStochastic : Form
    {
        string PROJECT_PATH = "";
        LSystemStochastic _stochastic = new LSystemStochastic(new Random());

        public FormStochastic()
        {
            InitializeComponent();
            PROJECT_PATH = Application.StartupPath;
            PROJECT_PATH = Directory.GetParent(PROJECT_PATH).FullName;
            PROJECT_PATH = Directory.GetParent(PROJECT_PATH).FullName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _stochastic.AddRule("F", "F[+F]F[-F]F", 0.33f);
            _stochastic.AddRule("F", "F[+F]F", 0.33f);
            _stochastic.AddRule("F", "F[-F]F", 0.34f);
            _stochastic.AddRule("F", "F[-F[+F[-F]-F[-F]]F]", 0.8f);
            string sentence = _stochastic.GenerateStochastic(axiom: "F", n: 2, delta: 20.0f);
            _stochastic.Render(CreateGraphics(), sentence, this.Width / 2, 0, 90, this.Height, 10, 1);
        }

        private void FormStochastic_Load(object sender, EventArgs e)
        {

        }
    }
}
