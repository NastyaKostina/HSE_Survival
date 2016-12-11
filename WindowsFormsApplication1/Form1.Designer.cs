namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Ground = new System.Windows.Forms.FlowLayoutPanel();
            this.timer_move = new System.Windows.Forms.Timer(this.components);
            this.Sky = new System.Windows.Forms.Panel();
            this.Player = new System.Windows.Forms.PictureBox();
            this.timer_Jump = new System.Windows.Forms.Timer(this.components);
            this.timer_Falling = new System.Windows.Forms.Timer(this.components);
            this.Block = new System.Windows.Forms.PictureBox();
            this.Sky.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Player)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Block)).BeginInit();
            this.SuspendLayout();
            // 
            // Ground
            // 
            this.Ground.BackColor = System.Drawing.Color.Lime;
            this.Ground.Location = new System.Drawing.Point(0, 268);
            this.Ground.Name = "Ground";
            this.Ground.Size = new System.Drawing.Size(669, 76);
            this.Ground.TabIndex = 1;
            // 
            // timer_move
            // 
            this.timer_move.Enabled = true;
            this.timer_move.Interval = 1;
            this.timer_move.Tick += new System.EventHandler(this.timer_move_Tick);
            // 
            // Sky
            // 
            this.Sky.BackColor = System.Drawing.Color.Cyan;
            this.Sky.Controls.Add(this.Block);
            this.Sky.Controls.Add(this.Player);
            this.Sky.Dock = System.Windows.Forms.DockStyle.Top;
            this.Sky.Location = new System.Drawing.Point(0, 0);
            this.Sky.Name = "Sky";
            this.Sky.Size = new System.Drawing.Size(669, 268);
            this.Sky.TabIndex = 2;
            // 
            // Player
            // 
            this.Player.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Player.Location = new System.Drawing.Point(125, 228);
            this.Player.Name = "Player";
            this.Player.Size = new System.Drawing.Size(26, 41);
            this.Player.TabIndex = 0;
            this.Player.TabStop = false;
            // 
            // timer_Jump
            // 
            this.timer_Jump.Enabled = true;
            this.timer_Jump.Interval = 1;
            this.timer_Jump.Tick += new System.EventHandler(this.timer_Jump_Tick);
            // 
            // Block
            // 
            this.Block.BackColor = System.Drawing.Color.DarkBlue;
            this.Block.Location = new System.Drawing.Point(251, 228);
            this.Block.Name = "Block";
            this.Block.Size = new System.Drawing.Size(106, 40);
            this.Block.TabIndex = 1;
            this.Block.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 344);
            this.Controls.Add(this.Sky);
            this.Controls.Add(this.Ground);
            this.Name = "Form1";
            this.Text = "Game";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.Sky.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Player)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Block)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel Ground;
        private System.Windows.Forms.Timer timer_move;
        public System.Windows.Forms.PictureBox Player;
        public System.Windows.Forms.Panel Sky;
        private System.Windows.Forms.Timer timer_Jump;
        private System.Windows.Forms.Timer timer_Falling;
        private System.Windows.Forms.PictureBox Block;
    }
}

