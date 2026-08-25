namespace PLatoAIAssistantTest
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			panel1 = new Panel();
			btnSend = new Button();
			txtPrompt = new TextBox();
			label1 = new Label();
			groupBox1 = new GroupBox();
			rbGeneral = new RadioButton();
			rbQueryEphemeral = new RadioButton();
			rbQueryJson = new RadioButton();
			rbQueryDB = new RadioButton();
			panel1.SuspendLayout();
			groupBox1.SuspendLayout();
			SuspendLayout();
			// 
			// panel1
			// 
			panel1.Controls.Add(btnSend);
			panel1.Controls.Add(txtPrompt);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(groupBox1);
			panel1.Dock = DockStyle.Top;
			panel1.Location = new Point(0, 0);
			panel1.Margin = new Padding(2);
			panel1.Name = "panel1";
			panel1.Size = new Size(867, 120);
			panel1.TabIndex = 0;
			// 
			// btnSend
			// 
			btnSend.Location = new Point(548, 84);
			btnSend.Name = "btnSend";
			btnSend.Size = new Size(75, 23);
			btnSend.TabIndex = 7;
			btnSend.Text = "Send";
			btnSend.UseVisualStyleBackColor = true;
			btnSend.Click += btnSend_Click;
			// 
			// txtPrompt
			// 
			txtPrompt.Location = new Point(143, 84);
			txtPrompt.Name = "txtPrompt";
			txtPrompt.Size = new Size(390, 23);
			txtPrompt.TabIndex = 6;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12, 87);
			label1.Name = "label1";
			label1.Size = new Size(120, 15);
			label1.TabIndex = 5;
			label1.Text = "Send Remote Prompt";
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(rbGeneral);
			groupBox1.Controls.Add(rbQueryEphemeral);
			groupBox1.Controls.Add(rbQueryJson);
			groupBox1.Controls.Add(rbQueryDB);
			groupBox1.Location = new Point(12, 12);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(620, 57);
			groupBox1.TabIndex = 4;
			groupBox1.TabStop = false;
			groupBox1.Text = "Behavior";
			// 
			// rbGeneral
			// 
			rbGeneral.AutoSize = true;
			rbGeneral.Checked = true;
			rbGeneral.Location = new Point(400, 22);
			rbGeneral.Name = "rbGeneral";
			rbGeneral.Size = new Size(121, 19);
			rbGeneral.TabIndex = 3;
			rbGeneral.TabStop = true;
			rbGeneral.Text = "General Questions";
			rbGeneral.UseVisualStyleBackColor = true;
			rbGeneral.CheckedChanged += rbGeneral_CheckedChanged;
			// 
			// rbQueryEphemeral
			// 
			rbQueryEphemeral.AutoSize = true;
			rbQueryEphemeral.Location = new Point(243, 22);
			rbQueryEphemeral.Name = "rbQueryEphemeral";
			rbQueryEphemeral.Size = new Size(151, 19);
			rbQueryEphemeral.TabIndex = 2;
			rbQueryEphemeral.Text = "Analyze Ephemeral Files";
			rbQueryEphemeral.UseVisualStyleBackColor = true;
			rbQueryEphemeral.CheckedChanged += rbQueryEphemeral_CheckedChanged;
			// 
			// rbQueryJson
			// 
			rbQueryJson.AutoSize = true;
			rbQueryJson.Location = new Point(127, 22);
			rbQueryJson.Name = "rbQueryJson";
			rbQueryJson.Size = new Size(110, 19);
			rbQueryJson.TabIndex = 1;
			rbQueryJson.Text = "Query Json Data";
			rbQueryJson.UseVisualStyleBackColor = true;
			rbQueryJson.CheckedChanged += rbQueryJson_CheckedChanged;
			// 
			// rbQueryDB
			// 
			rbQueryDB.AutoSize = true;
			rbQueryDB.Location = new Point(18, 22);
			rbQueryDB.Name = "rbQueryDB";
			rbQueryDB.Size = new Size(108, 19);
			rbQueryDB.TabIndex = 0;
			rbQueryDB.Text = "Query Database";
			rbQueryDB.UseVisualStyleBackColor = true;
			rbQueryDB.CheckedChanged += rbQueryDB_CheckedChanged;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(867, 646);
			Controls.Add(panel1);
			Margin = new Padding(2);
			Name = "Form1";
			Text = "Plato Assistant Demonstration Application";
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Panel panel1;
		private GroupBox groupBox1;
		private RadioButton rbQueryEphemeral;
		private RadioButton rbQueryJson;
		private RadioButton rbQueryDB;
		private RadioButton rbGeneral;
		private Button btnSend;
		private TextBox txtPrompt;
		private Label label1;
	}
}
