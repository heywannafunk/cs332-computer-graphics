
namespace CGLab5
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
            this.lSystemsButton = new System.Windows.Forms.Button();
            this.midpointButton = new System.Windows.Forms.Button();
            this.bezierButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lSystemsButton
            // 
            this.lSystemsButton.Location = new System.Drawing.Point(75, 184);
            this.lSystemsButton.Name = "lSystemsButton";
            this.lSystemsButton.Size = new System.Drawing.Size(274, 136);
            this.lSystemsButton.TabIndex = 0;
            this.lSystemsButton.Text = "L-systems";
            this.lSystemsButton.UseVisualStyleBackColor = true;
            this.lSystemsButton.Click += new System.EventHandler(this.lSystemsButton_Click);
            // 
            // midpointButton
            // 
            this.midpointButton.Location = new System.Drawing.Point(454, 184);
            this.midpointButton.Name = "midpointButton";
            this.midpointButton.Size = new System.Drawing.Size(274, 136);
            this.midpointButton.TabIndex = 1;
            this.midpointButton.Text = "Midpoint displacement";
            this.midpointButton.UseVisualStyleBackColor = true;
            this.midpointButton.Click += new System.EventHandler(this.midpointButton_Click);
            // 
            // bezierButton
            // 
            this.bezierButton.Location = new System.Drawing.Point(828, 184);
            this.bezierButton.Name = "bezierButton";
            this.bezierButton.Size = new System.Drawing.Size(274, 136);
            this.bezierButton.TabIndex = 2;
            this.bezierButton.Text = "Cubic Bezier splines";
            this.bezierButton.UseVisualStyleBackColor = true;
            this.bezierButton.Click += new System.EventHandler(this.bezierButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 529);
            this.Controls.Add(this.bezierButton);
            this.Controls.Add(this.midpointButton);
            this.Controls.Add(this.lSystemsButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button lSystemsButton;
        private System.Windows.Forms.Button midpointButton;
        private System.Windows.Forms.Button bezierButton;
    }
}

