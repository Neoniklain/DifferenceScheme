namespace DifferenceScheme
{
    partial class Begin
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
            this.N_box = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.k_slow = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.k_length = new System.Windows.Forms.TextBox();
            this.Stop = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.nt_box = new System.Windows.Forms.TextBox();
            this.Error_label = new System.Windows.Forms.Label();
            this.Start = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Fx_box = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.anT = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // N_box
            // 
            this.N_box.Font = new System.Drawing.Font("Garamond", 12F);
            this.N_box.Location = new System.Drawing.Point(200, 18);
            this.N_box.Name = "N_box";
            this.N_box.Size = new System.Drawing.Size(119, 30);
            this.N_box.TabIndex = 0;
            this.N_box.Text = "50";
            this.N_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Garamond", 12F);
            this.label1.Location = new System.Drawing.Point(13, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 61);
            this.label1.TabIndex = 1;
            this.label1.Text = "Еоличество точек на сторону.:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.k_slow);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.k_length);
            this.panel1.Controls.Add(this.Stop);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.nt_box);
            this.panel1.Controls.Add(this.Error_label);
            this.panel1.Controls.Add(this.Start);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.Fx_box);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.N_box);
            this.panel1.Location = new System.Drawing.Point(6, 600);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(863, 141);
            this.panel1.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Garamond", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(334, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(181, 30);
            this.label6.TabIndex = 13;
            this.label6.Text = "Коэф. замедленя";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // k_slow
            // 
            this.k_slow.Font = new System.Drawing.Font("Garamond", 12F);
            this.k_slow.Location = new System.Drawing.Point(521, 46);
            this.k_slow.Name = "k_slow";
            this.k_slow.Size = new System.Drawing.Size(119, 30);
            this.k_slow.TabIndex = 12;
            this.k_slow.Text = "1";
            this.k_slow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Garamond", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(334, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(181, 30);
            this.label4.TabIndex = 11;
            this.label4.Text = "Коэф. длительности";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // k_length
            // 
            this.k_length.Font = new System.Drawing.Font("Garamond", 12F);
            this.k_length.Location = new System.Drawing.Point(521, 7);
            this.k_length.Name = "k_length";
            this.k_length.Size = new System.Drawing.Size(119, 30);
            this.k_length.TabIndex = 10;
            this.k_length.Text = "1";
            this.k_length.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Stop
            // 
            this.Stop.BackColor = System.Drawing.Color.White;
            this.Stop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Stop.Font = new System.Drawing.Font("Garamond", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Stop.Location = new System.Drawing.Point(655, 46);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(203, 31);
            this.Stop.TabIndex = 9;
            this.Stop.Text = "Стоп";
            this.Stop.UseVisualStyleBackColor = false;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Garamond", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(13, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 30);
            this.label5.TabIndex = 8;
            this.label5.Text = "Шагов по времени:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nt_box
            // 
            this.nt_box.Font = new System.Drawing.Font("Garamond", 12F);
            this.nt_box.Location = new System.Drawing.Point(200, 106);
            this.nt_box.Name = "nt_box";
            this.nt_box.Size = new System.Drawing.Size(119, 30);
            this.nt_box.TabIndex = 7;
            this.nt_box.Text = "50";
            this.nt_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Error_label
            // 
            this.Error_label.Font = new System.Drawing.Font("Garamond", 12F);
            this.Error_label.ForeColor = System.Drawing.Color.Red;
            this.Error_label.Location = new System.Drawing.Point(334, 47);
            this.Error_label.Name = "Error_label";
            this.Error_label.Size = new System.Drawing.Size(306, 30);
            this.Error_label.TabIndex = 6;
            this.Error_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Start
            // 
            this.Start.BackColor = System.Drawing.Color.White;
            this.Start.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Start.Font = new System.Drawing.Font("Garamond", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Start.Location = new System.Drawing.Point(655, 6);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(203, 31);
            this.Start.TabIndex = 2;
            this.Start.Text = "Старт";
            this.Start.UseVisualStyleBackColor = false;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Garamond", 12F);
            this.label3.Location = new System.Drawing.Point(13, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 30);
            this.label3.TabIndex = 3;
            this.label3.Text = "Функция:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Fx_box
            // 
            this.Fx_box.Font = new System.Drawing.Font("Garamond", 12F);
            this.Fx_box.Location = new System.Drawing.Point(115, 70);
            this.Fx_box.Name = "Fx_box";
            this.Fx_box.Size = new System.Drawing.Size(204, 30);
            this.Fx_box.TabIndex = 2;
            this.Fx_box.Text = "(x+y)*t";
            this.Fx_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Garamond", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(881, 39);
            this.label2.TabIndex = 1;
            this.label2.Text = "Стабилизирующие правки";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // anT
            // 
            this.anT.AccumBits = ((byte)(0));
            this.anT.AutoCheckErrors = false;
            this.anT.AutoFinish = false;
            this.anT.AutoMakeCurrent = true;
            this.anT.AutoSwapBuffers = true;
            this.anT.BackColor = System.Drawing.Color.Black;
            this.anT.ColorBits = ((byte)(32));
            this.anT.DepthBits = ((byte)(16));
            this.anT.Location = new System.Drawing.Point(6, 53);
            this.anT.Name = "anT";
            this.anT.Size = new System.Drawing.Size(863, 541);
            this.anT.StencilBits = ((byte)(0));
            this.anT.TabIndex = 2;
            this.anT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ant_MouseDown);
            this.anT.MouseMove += new System.Windows.Forms.MouseEventHandler(this.anT_MouseMove);
            this.anT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ant_MouseUp);
            // 
            // Begin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(881, 753);
            this.Controls.Add(this.anT);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Begin";
            this.Text = "DifferenceScheme";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox N_box;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Fx_box;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Start;
        private Tao.Platform.Windows.SimpleOpenGlControl anT;
        private System.Windows.Forms.Label Error_label;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox nt_box;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox k_slow;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox k_length;
    }
}

