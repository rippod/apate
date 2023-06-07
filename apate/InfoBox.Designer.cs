namespace apate
{
    partial class InfoBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            YesButton = new Button();
            NoButton = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // YesButton
            // 
            YesButton.Anchor = AnchorStyles.None;
            YesButton.DialogResult = DialogResult.Yes;
            YesButton.Location = new Point(64, 123);
            YesButton.Name = "YesButton";
            YesButton.Size = new Size(100, 30);
            YesButton.TabIndex = 0;
            YesButton.Text = "是(&Y)";
            YesButton.UseVisualStyleBackColor = true;
            // 
            // NoButton
            // 
            NoButton.Anchor = AnchorStyles.None;
            NoButton.DialogResult = DialogResult.No;
            NoButton.Location = new Point(188, 123);
            NoButton.Name = "NoButton";
            NoButton.Size = new Size(100, 30);
            NoButton.TabIndex = 1;
            NoButton.Text = "否(&N)";
            NoButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(333, 101);
            label1.TabIndex = 2;
            // 
            // InfoBox
            // 
            AcceptButton = YesButton;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = NoButton;
            ClientSize = new Size(353, 162);
            ControlBox = false;
            Controls.Add(label1);
            Controls.Add(NoButton);
            Controls.Add(YesButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "InfoBox";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "InfoBox";
            ResumeLayout(false);
        }

        #endregion

        private Button YesButton;
        private Button NoButton;
        private Label label1;
    }
}