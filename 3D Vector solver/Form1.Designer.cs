namespace _3D_Vector_solver
{
    partial class MainForm
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
            this.cbxOptions = new System.Windows.Forms.ComboBox();
            this.tbxLn1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxLn2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxPoint = new System.Windows.Forms.TextBox();
            this.btnSolve = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbxOptions
            // 
            this.cbxOptions.FormattingEnabled = true;
            this.cbxOptions.Items.AddRange(new object[] {
            "Intersection between a point and a line",
            "Intersection between two lines",
            "Are two lines skew?",
            "Shortest distance between two lines"});
            this.cbxOptions.Location = new System.Drawing.Point(26, 95);
            this.cbxOptions.Name = "cbxOptions";
            this.cbxOptions.Size = new System.Drawing.Size(368, 21);
            this.cbxOptions.TabIndex = 0;
            // 
            // tbxLn1
            // 
            this.tbxLn1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tbxLn1.Location = new System.Drawing.Point(461, 91);
            this.tbxLn1.Name = "tbxLn1";
            this.tbxLn1.Size = new System.Drawing.Size(196, 26);
            this.tbxLn1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(458, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter a line equation as 6 comma separated values";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(458, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(278, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Enter another line equation as 6 comma separated values";
            // 
            // tbxLn2
            // 
            this.tbxLn2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tbxLn2.Location = new System.Drawing.Point(461, 136);
            this.tbxLn2.Name = "tbxLn2";
            this.tbxLn2.Size = new System.Drawing.Size(196, 26);
            this.tbxLn2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(458, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(211, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Enter a point as 3 comma separated values";
            // 
            // tbxPoint
            // 
            this.tbxPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tbxPoint.Location = new System.Drawing.Point(461, 181);
            this.tbxPoint.Name = "tbxPoint";
            this.tbxPoint.Size = new System.Drawing.Size(196, 26);
            this.tbxPoint.TabIndex = 5;
            // 
            // btnSolve
            // 
            this.btnSolve.Location = new System.Drawing.Point(182, 298);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(136, 84);
            this.btnSolve.TabIndex = 7;
            this.btnSolve.Text = "Solve!";
            this.btnSolve.UseVisualStyleBackColor = true;
            this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSolve);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxPoint);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxLn2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxLn1);
            this.Controls.Add(this.cbxOptions);
            this.Name = "MainForm";
            this.Text = "Vector Solver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxOptions;
        private System.Windows.Forms.TextBox tbxLn1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxLn2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxPoint;
        private System.Windows.Forms.Button btnSolve;
    }
}

