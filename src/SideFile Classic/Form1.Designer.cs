using WinForms.Ribbon;

namespace SideFile.Classic
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
            Ribbon = new RibbonStrip();
            splitContainer1 = new SplitContainer();
            leftListView = new FileListView();
            rightListView = new FileListView();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // Ribbon
            // 
            Ribbon.Location = new Point(0, 0);
            Ribbon.MarkupHeader = "SideFile.Classic.Ribbon.RibbonMarkup.h";
            Ribbon.MarkupResource = "SideFile.Classic.Ribbon.RibbonMarkup.ribbon";
            Ribbon.Name = "Ribbon";
            Ribbon.Size = new Size(800, 116);
            Ribbon.TabIndex = 1;
            Ribbon.ViewCreated += Ribbon_ViewCreated;
            Ribbon.ViewDestroy += Ribbon_ViewDestroy;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 116);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(leftListView);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(rightListView);
            splitContainer1.Size = new Size(800, 334);
            splitContainer1.SplitterDistance = 266;
            splitContainer1.TabIndex = 0;
            // 
            // leftListView
            // 
            leftListView.BorderStyle = BorderStyle.FixedSingle;
            leftListView.Dock = DockStyle.Fill;
            leftListView.Location = new Point(0, 0);
            leftListView.Name = "leftListView";
            leftListView.Size = new Size(266, 334);
            leftListView.TabIndex = 0;
            leftListView.UseCompatibleStateImageBehavior = false;
            leftListView.View = View.Details;
            leftListView.Navigated += leftListView_Navigated;
            // 
            // rightListView
            // 
            rightListView.BorderStyle = BorderStyle.None;
            rightListView.Dock = DockStyle.Fill;
            rightListView.Location = new Point(0, 0);
            rightListView.Name = "rightListView";
            rightListView.Size = new Size(530, 334);
            rightListView.TabIndex = 0;
            rightListView.UseCompatibleStateImageBehavior = false;
            rightListView.View = View.Details;
            rightListView.Navigated += rightListView_Navigated;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Controls.Add(Ribbon);
            Name = "Form1";
            Text = "SideFile Classic - [This PC | This PC]";
            FormClosed += Form1_FormClosed;
            Shown += Form1_Shown;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private FileListView leftListView;
        private FileListView rightListView;
        public RibbonStrip Ribbon;
        public SplitContainer splitContainer1;
    }
}
