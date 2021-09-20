
namespace ImageFileOptionVisualizer
{
    partial class Form1
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
            this.DisplayTreeView = new System.Windows.Forms.TreeView();
            this.RefreshTreeDisplay = new System.Windows.Forms.Button();
            this.AvastCleanupOnlyMode = new System.Windows.Forms.CheckBox();
            this.IFEOEnabledOnlyMode = new System.Windows.Forms.CheckBox();
            this.AnythingMode = new System.Windows.Forms.CheckBox();
            this.RemoveSelectedEntry = new System.Windows.Forms.Button();
            this.RemoveEverything = new System.Windows.Forms.Button();
            this.EntryCountDisplay = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DisplayTreeView
            // 
            this.DisplayTreeView.Location = new System.Drawing.Point(12, 12);
            this.DisplayTreeView.Name = "DisplayTreeView";
            this.DisplayTreeView.Size = new System.Drawing.Size(381, 241);
            this.DisplayTreeView.TabIndex = 0;
            // 
            // RefreshTreeDisplay
            // 
            this.RefreshTreeDisplay.Location = new System.Drawing.Point(400, 163);
            this.RefreshTreeDisplay.Name = "RefreshTreeDisplay";
            this.RefreshTreeDisplay.Size = new System.Drawing.Size(147, 26);
            this.RefreshTreeDisplay.TabIndex = 1;
            this.RefreshTreeDisplay.Text = "Refresh List";
            this.RefreshTreeDisplay.UseVisualStyleBackColor = true;
            this.RefreshTreeDisplay.Click += new System.EventHandler(this.RefreshTreeDisplay_Click);
            // 
            // AvastCleanupOnlyMode
            // 
            this.AvastCleanupOnlyMode.Checked = true;
            this.AvastCleanupOnlyMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AvastCleanupOnlyMode.Location = new System.Drawing.Point(400, 12);
            this.AvastCleanupOnlyMode.Name = "AvastCleanupOnlyMode";
            this.AvastCleanupOnlyMode.Size = new System.Drawing.Size(147, 38);
            this.AvastCleanupOnlyMode.TabIndex = 2;
            this.AvastCleanupOnlyMode.Text = "Scan Only for Avast Cleanup Entries";
            this.AvastCleanupOnlyMode.UseVisualStyleBackColor = true;
            this.AvastCleanupOnlyMode.CheckedChanged += new System.EventHandler(this.AvastCleanupOnlyMode_CheckedChanged);
            // 
            // IFEOEnabledOnlyMode
            // 
            this.IFEOEnabledOnlyMode.Location = new System.Drawing.Point(400, 56);
            this.IFEOEnabledOnlyMode.Name = "IFEOEnabledOnlyMode";
            this.IFEOEnabledOnlyMode.Size = new System.Drawing.Size(147, 35);
            this.IFEOEnabledOnlyMode.TabIndex = 3;
            this.IFEOEnabledOnlyMode.Text = "Scan all Image File Execution Option";
            this.IFEOEnabledOnlyMode.UseVisualStyleBackColor = true;
            this.IFEOEnabledOnlyMode.CheckedChanged += new System.EventHandler(this.IFEOEnableOnlyMode_CheckedChanged);
            // 
            // AnythingMode
            // 
            this.AnythingMode.Location = new System.Drawing.Point(399, 97);
            this.AnythingMode.Name = "AnythingMode";
            this.AnythingMode.Size = new System.Drawing.Size(147, 26);
            this.AnythingMode.TabIndex = 4;
            this.AnythingMode.Text = "Scan everything";
            this.AnythingMode.UseVisualStyleBackColor = true;
            this.AnythingMode.CheckedChanged += new System.EventHandler(this.AnythingMode_CheckedChanged);
            // 
            // RemoveSelectedEntry
            // 
            this.RemoveSelectedEntry.Location = new System.Drawing.Point(400, 195);
            this.RemoveSelectedEntry.Name = "RemoveSelectedEntry";
            this.RemoveSelectedEntry.Size = new System.Drawing.Size(147, 26);
            this.RemoveSelectedEntry.TabIndex = 5;
            this.RemoveSelectedEntry.Text = "Reverted Selected";
            this.RemoveSelectedEntry.UseVisualStyleBackColor = true;
            this.RemoveSelectedEntry.Click += new System.EventHandler(this.RemoveSelectedEntry_Click);
            // 
            // RemoveEverything
            // 
            this.RemoveEverything.Location = new System.Drawing.Point(400, 227);
            this.RemoveEverything.Name = "RemoveEverything";
            this.RemoveEverything.Size = new System.Drawing.Size(147, 26);
            this.RemoveEverything.TabIndex = 6;
            this.RemoveEverything.Text = "Revert Anything";
            this.RemoveEverything.UseVisualStyleBackColor = true;
            this.RemoveEverything.Click += new System.EventHandler(this.RemoveEverything_Click);
            // 
            // EntryCountDisplay
            // 
            this.EntryCountDisplay.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EntryCountDisplay.Location = new System.Drawing.Point(399, 130);
            this.EntryCountDisplay.Name = "EntryCountDisplay";
            this.EntryCountDisplay.Size = new System.Drawing.Size(147, 30);
            this.EntryCountDisplay.TabIndex = 7;
            this.EntryCountDisplay.Text = "Entry Count : -";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 265);
            this.Controls.Add(this.EntryCountDisplay);
            this.Controls.Add(this.RemoveEverything);
            this.Controls.Add(this.RemoveSelectedEntry);
            this.Controls.Add(this.AnythingMode);
            this.Controls.Add(this.IFEOEnabledOnlyMode);
            this.Controls.Add(this.AvastCleanupOnlyMode);
            this.Controls.Add(this.RefreshTreeDisplay);
            this.Controls.Add(this.DisplayTreeView);
            this.Name = "Form1";
            this.Text = "ImageFileOptionVisualizer - TIRTAGT";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView DisplayTreeView;
        private System.Windows.Forms.Button RefreshTreeDisplay;
        private System.Windows.Forms.CheckBox AvastCleanupOnlyMode;
        private System.Windows.Forms.CheckBox IFEOEnabledOnlyMode;
        private System.Windows.Forms.CheckBox AnythingMode;
        private System.Windows.Forms.Button RemoveSelectedEntry;
        private System.Windows.Forms.Button RemoveEverything;
        private System.Windows.Forms.Label EntryCountDisplay;
    }
}

