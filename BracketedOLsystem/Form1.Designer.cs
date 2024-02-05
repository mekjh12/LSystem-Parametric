namespace LSystem
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.nbrNum = new System.Windows.Forms.NumericUpDown();
            this.nbrDelta = new System.Windows.Forms.NumericUpDown();
            this.nbrLength = new System.Windows.Forms.NumericUpDown();
            this.tbGrammer = new System.Windows.Forms.TextBox();
            this.tbAxiom = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nbrWidth = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbrNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbrDelta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbrLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbrWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(665, 756);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 216);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(236, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "생성(Edge Rewriting)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // nbrNum
            // 
            this.nbrNum.Location = new System.Drawing.Point(12, 12);
            this.nbrNum.Name = "nbrNum";
            this.nbrNum.Size = new System.Drawing.Size(75, 21);
            this.nbrNum.TabIndex = 2;
            this.toolTip1.SetToolTip(this.nbrNum, "N");
            this.nbrNum.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // nbrDelta
            // 
            this.nbrDelta.DecimalPlaces = 1;
            this.nbrDelta.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nbrDelta.Location = new System.Drawing.Point(93, 12);
            this.nbrDelta.Name = "nbrDelta";
            this.nbrDelta.Size = new System.Drawing.Size(75, 21);
            this.nbrDelta.TabIndex = 3;
            this.toolTip1.SetToolTip(this.nbrDelta, "delta");
            this.nbrDelta.Value = new decimal(new int[] {
            225,
            0,
            0,
            65536});
            // 
            // nbrLength
            // 
            this.nbrLength.Location = new System.Drawing.Point(12, 39);
            this.nbrLength.Name = "nbrLength";
            this.nbrLength.Size = new System.Drawing.Size(75, 21);
            this.nbrLength.TabIndex = 4;
            this.toolTip1.SetToolTip(this.nbrLength, "Draw Length");
            this.nbrLength.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // tbGrammer
            // 
            this.tbGrammer.Location = new System.Drawing.Point(12, 129);
            this.tbGrammer.Multiline = true;
            this.tbGrammer.Name = "tbGrammer";
            this.tbGrammer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbGrammer.Size = new System.Drawing.Size(237, 81);
            this.tbGrammer.TabIndex = 5;
            // 
            // tbAxiom
            // 
            this.tbAxiom.Location = new System.Drawing.Point(12, 102);
            this.tbAxiom.Name = "tbAxiom";
            this.tbAxiom.Size = new System.Drawing.Size(236, 21);
            this.tbAxiom.TabIndex = 6;
            this.tbAxiom.Text = "X";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 245);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(236, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "생성(Node Rewriting)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Maroon;
            this.label1.Font = new System.Drawing.Font("나눔고딕코딩", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.BlanchedAlmond;
            this.label1.Location = new System.Drawing.Point(12, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Branch Color";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label2.Font = new System.Drawing.Font("나눔고딕코딩", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.BlanchedAlmond;
            this.label2.Location = new System.Drawing.Point(12, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Leaf Color";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // nbrWidth
            // 
            this.nbrWidth.DecimalPlaces = 1;
            this.nbrWidth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nbrWidth.Location = new System.Drawing.Point(93, 39);
            this.nbrWidth.Name = "nbrWidth";
            this.nbrWidth.Size = new System.Drawing.Size(75, 21);
            this.nbrWidth.TabIndex = 10;
            this.toolTip1.SetToolTip(this.nbrWidth, "Draw Width");
            this.nbrWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 756);
            this.Controls.Add(this.nbrWidth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tbAxiom);
            this.Controls.Add(this.tbGrammer);
            this.Controls.Add(this.nbrLength);
            this.Controls.Add(this.nbrDelta);
            this.Controls.Add(this.nbrNum);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbrNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbrDelta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbrLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbrWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown nbrNum;
        private System.Windows.Forms.NumericUpDown nbrDelta;
        private System.Windows.Forms.NumericUpDown nbrLength;
        private System.Windows.Forms.TextBox tbGrammer;
        private System.Windows.Forms.TextBox tbAxiom;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.NumericUpDown nbrWidth;
    }
}

