namespace KlassaGenerering
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
            this.btnDelphiEnums = new System.Windows.Forms.Button();
            this.btnDelphiObjects = new System.Windows.Forms.Button();
            this.btnCSEnums = new System.Windows.Forms.Button();
            this.btnCSObjects = new System.Windows.Forms.Button();
            this.btnCSUIObjects = new System.Windows.Forms.Button();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.btnSaveDefinition = new System.Windows.Forms.Button();
            this.btnLoadDefinition = new System.Windows.Forms.Button();
            this.ProtoButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDelphiEnums
            // 
            this.btnDelphiEnums.Location = new System.Drawing.Point(13, 13);
            this.btnDelphiEnums.Name = "btnDelphiEnums";
            this.btnDelphiEnums.Size = new System.Drawing.Size(113, 23);
            this.btnDelphiEnums.TabIndex = 0;
            this.btnDelphiEnums.Text = "Delphi Enums";
            this.btnDelphiEnums.UseVisualStyleBackColor = true;
            this.btnDelphiEnums.Click += new System.EventHandler(this.btnDelphiEnums_Click);
            // 
            // btnDelphiObjects
            // 
            this.btnDelphiObjects.Location = new System.Drawing.Point(132, 13);
            this.btnDelphiObjects.Name = "btnDelphiObjects";
            this.btnDelphiObjects.Size = new System.Drawing.Size(113, 23);
            this.btnDelphiObjects.TabIndex = 1;
            this.btnDelphiObjects.Text = "Delphi Objects";
            this.btnDelphiObjects.UseVisualStyleBackColor = true;
            this.btnDelphiObjects.Click += new System.EventHandler(this.btnDelphiObjects_Click);
            // 
            // btnCSEnums
            // 
            this.btnCSEnums.Location = new System.Drawing.Point(13, 42);
            this.btnCSEnums.Name = "btnCSEnums";
            this.btnCSEnums.Size = new System.Drawing.Size(113, 23);
            this.btnCSEnums.TabIndex = 2;
            this.btnCSEnums.Text = "C# Enums";
            this.btnCSEnums.UseVisualStyleBackColor = true;
            this.btnCSEnums.Click += new System.EventHandler(this.btnCSEnums_Click);
            // 
            // btnCSObjects
            // 
            this.btnCSObjects.Location = new System.Drawing.Point(132, 42);
            this.btnCSObjects.Name = "btnCSObjects";
            this.btnCSObjects.Size = new System.Drawing.Size(113, 23);
            this.btnCSObjects.TabIndex = 3;
            this.btnCSObjects.Text = "C# Objects";
            this.btnCSObjects.UseVisualStyleBackColor = true;
            this.btnCSObjects.Click += new System.EventHandler(this.btnCSObjects_Click);
            // 
            // btnCSUIObjects
            // 
            this.btnCSUIObjects.Location = new System.Drawing.Point(251, 42);
            this.btnCSUIObjects.Name = "btnCSUIObjects";
            this.btnCSUIObjects.Size = new System.Drawing.Size(113, 23);
            this.btnCSUIObjects.TabIndex = 4;
            this.btnCSUIObjects.Text = "C# UI Objects";
            this.btnCSUIObjects.UseVisualStyleBackColor = true;
            this.btnCSUIObjects.Click += new System.EventHandler(this.btnCSUIObjects_Click);
            // 
            // tbResult
            // 
            this.tbResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbResult.Location = new System.Drawing.Point(16, 100);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbResult.Size = new System.Drawing.Size(679, 306);
            this.tbResult.TabIndex = 5;
            this.tbResult.WordWrap = false;
            // 
            // btnSaveDefinition
            // 
            this.btnSaveDefinition.Location = new System.Drawing.Point(563, 13);
            this.btnSaveDefinition.Name = "btnSaveDefinition";
            this.btnSaveDefinition.Size = new System.Drawing.Size(117, 23);
            this.btnSaveDefinition.TabIndex = 6;
            this.btnSaveDefinition.Text = "Save Definition";
            this.btnSaveDefinition.UseVisualStyleBackColor = true;
            this.btnSaveDefinition.Click += new System.EventHandler(this.btnSaveDefinition_Click);
            // 
            // btnLoadDefinition
            // 
            this.btnLoadDefinition.Location = new System.Drawing.Point(563, 42);
            this.btnLoadDefinition.Name = "btnLoadDefinition";
            this.btnLoadDefinition.Size = new System.Drawing.Size(117, 23);
            this.btnLoadDefinition.TabIndex = 7;
            this.btnLoadDefinition.Text = "Load Definition";
            this.btnLoadDefinition.UseVisualStyleBackColor = true;
            this.btnLoadDefinition.Click += new System.EventHandler(this.btnLoadDefinition_Click);
            // 
            // ProtoButton
            // 
            this.ProtoButton.Location = new System.Drawing.Point(13, 71);
            this.ProtoButton.Name = "ProtoButton";
            this.ProtoButton.Size = new System.Drawing.Size(113, 23);
            this.ProtoButton.TabIndex = 8;
            this.ProtoButton.Text = "Proto";
            this.ProtoButton.UseVisualStyleBackColor = true;
            this.ProtoButton.Click += new System.EventHandler(this.ProtoButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 418);
            this.Controls.Add(this.ProtoButton);
            this.Controls.Add(this.btnLoadDefinition);
            this.Controls.Add(this.btnSaveDefinition);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.btnCSUIObjects);
            this.Controls.Add(this.btnCSObjects);
            this.Controls.Add(this.btnCSEnums);
            this.Controls.Add(this.btnDelphiObjects);
            this.Controls.Add(this.btnDelphiEnums);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDelphiEnums;
        private System.Windows.Forms.Button btnDelphiObjects;
        private System.Windows.Forms.Button btnCSEnums;
        private System.Windows.Forms.Button btnCSObjects;
        private System.Windows.Forms.Button btnCSUIObjects;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Button btnSaveDefinition;
        private System.Windows.Forms.Button btnLoadDefinition;
        private System.Windows.Forms.Button ProtoButton;
    }
}

