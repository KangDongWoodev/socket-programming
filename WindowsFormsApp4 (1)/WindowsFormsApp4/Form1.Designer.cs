namespace WindowsFormsApp4
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
            this.btnCon = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.button_red = new System.Windows.Forms.Button();
            this.button_green = new System.Windows.Forms.Button();
            this.button_blue = new System.Windows.Forms.Button();
            this.button_orginal = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(21, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(537, 624);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnCon
            // 
            this.btnCon.Location = new System.Drawing.Point(835, 88);
            this.btnCon.Name = "btnCon";
            this.btnCon.Size = new System.Drawing.Size(190, 37);
            this.btnCon.TabIndex = 1;
            this.btnCon.Text = "이미지연결";
            this.btnCon.UseVisualStyleBackColor = true;
            this.btnCon.Click += new System.EventHandler(this.btnCon_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(835, 28);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(188, 43);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "저장위치";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(580, 28);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(224, 229);
            this.textBox1.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(833, 220);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(190, 37);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "해제";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // button_red
            // 
            this.button_red.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.button_red.Location = new System.Drawing.Point(582, 263);
            this.button_red.Name = "button_red";
            this.button_red.Size = new System.Drawing.Size(80, 61);
            this.button_red.TabIndex = 12;
            this.button_red.Text = "R";
            this.button_red.UseVisualStyleBackColor = true;
            this.button_red.Click += new System.EventHandler(this.button_red_Click);
            // 
            // button_green
            // 
            this.button_green.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.button_green.Location = new System.Drawing.Point(682, 263);
            this.button_green.Name = "button_green";
            this.button_green.Size = new System.Drawing.Size(80, 61);
            this.button_green.TabIndex = 13;
            this.button_green.Text = "G";
            this.button_green.UseVisualStyleBackColor = true;
            this.button_green.Click += new System.EventHandler(this.button_green_Click);
            // 
            // button_blue
            // 
            this.button_blue.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.button_blue.Location = new System.Drawing.Point(795, 263);
            this.button_blue.Name = "button_blue";
            this.button_blue.Size = new System.Drawing.Size(80, 61);
            this.button_blue.TabIndex = 14;
            this.button_blue.Text = "B";
            this.button_blue.UseVisualStyleBackColor = true;
            this.button_blue.Click += new System.EventHandler(this.button_blue_Click);
            // 
            // button_orginal
            // 
            this.button_orginal.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.button_orginal.Location = new System.Drawing.Point(900, 263);
            this.button_orginal.Name = "button_orginal";
            this.button_orginal.Size = new System.Drawing.Size(80, 61);
            this.button_orginal.TabIndex = 15;
            this.button_orginal.Text = "O";
            this.button_orginal.UseVisualStyleBackColor = true;
            this.button_orginal.Click += new System.EventHandler(this.button_orginal_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(582, 349);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(223, 121);
            this.textBox2.TabIndex = 16;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(835, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(190, 37);
            this.button1.TabIndex = 17;
            this.button1.Text = "시리얼연결";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(582, 524);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(223, 49);
            this.textBox3.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(579, 491);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 19;
            this.label1.Text = "필터적용";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 669);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button_orginal);
            this.Controls.Add(this.button_blue);
            this.Controls.Add(this.button_green);
            this.Controls.Add(this.button_red);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCon);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCon;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button button_red;
        private System.Windows.Forms.Button button_green;
        private System.Windows.Forms.Button button_blue;
        private System.Windows.Forms.Button button_orginal;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
    }
}

