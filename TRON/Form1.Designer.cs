namespace TRON
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
            panelItems = new Panel();
            panelPowers = new Panel();
            SuspendLayout();
            // 
            // panelItems
            // 
            panelItems.BackColor = SystemColors.ActiveCaptionText;
            panelItems.Location = new Point(904, -2);
            panelItems.Name = "panelItems";
            panelItems.Size = new Size(75, 958);
            panelItems.TabIndex = 0;
            // 
            // panelPowers
            // 
            panelPowers.BackColor = SystemColors.ActiveCaptionText;
            panelPowers.Location = new Point(1, 891);
            panelPowers.Name = "panelPowers";
            panelPowers.Size = new Size(902, 65);
            panelPowers.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 953);
            Controls.Add(panelPowers);
            Controls.Add(panelItems);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Panel panelItems;
        private Panel panelPowers;
    }
}
