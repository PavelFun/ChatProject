namespace ChatClientV2._0
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
            this.tb_NIC = new System.Windows.Forms.TextBox();
            this.btn_enter = new System.Windows.Forms.Button();
            this.btn_send = new System.Windows.Forms.Button();
            this.rtb_chat = new System.Windows.Forms.RichTextBox();
            this.rtb_send = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // tb_NIC
            // 
            this.tb_NIC.Location = new System.Drawing.Point(12, 40);
            this.tb_NIC.Name = "tb_NIC";
            this.tb_NIC.Size = new System.Drawing.Size(182, 20);
            this.tb_NIC.TabIndex = 0;
            this.tb_NIC.TextChanged += new System.EventHandler(this.tb_NIC_TextChanged);
            // 
            // btn_enter
            // 
            this.btn_enter.Location = new System.Drawing.Point(200, 40);
            this.btn_enter.Name = "btn_enter";
            this.btn_enter.Size = new System.Drawing.Size(75, 23);
            this.btn_enter.TabIndex = 1;
            this.btn_enter.Text = "Вход";
            this.btn_enter.UseCompatibleTextRendering = true;
            this.btn_enter.UseVisualStyleBackColor = true;
            this.btn_enter.Click += new System.EventHandler(this.btn_enter_Click);
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(540, 265);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(158, 96);
            this.btn_send.TabIndex = 2;
            this.btn_send.Text = "Отправить";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            this.btn_send.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_send_KeyDown);
            // 
            // rtb_chat
            // 
            this.rtb_chat.Location = new System.Drawing.Point(12, 66);
            this.rtb_chat.Name = "rtb_chat";
            this.rtb_chat.Size = new System.Drawing.Size(510, 190);
            this.rtb_chat.TabIndex = 3;
            this.rtb_chat.Text = "";
            this.rtb_chat.TextChanged += new System.EventHandler(this.rtb_chat_TextChanged);
            // 
            // rtb_send
            // 
            this.rtb_send.Location = new System.Drawing.Point(12, 265);
            this.rtb_send.Name = "rtb_send";
            this.rtb_send.Size = new System.Drawing.Size(510, 96);
            this.rtb_send.TabIndex = 4;
            this.rtb_send.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 373);
            this.Controls.Add(this.rtb_send);
            this.Controls.Add(this.rtb_chat);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.btn_enter);
            this.Controls.Add(this.tb_NIC);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_NIC;
        private System.Windows.Forms.Button btn_enter;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.RichTextBox rtb_chat;
        private System.Windows.Forms.RichTextBox rtb_send;
    }
}

