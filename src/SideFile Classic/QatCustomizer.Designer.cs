namespace SideFile.Classic
{
    partial class QatCustomizer
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
            components = new System.ComponentModel.Container();
            buttonOk = new Button();
            buttonCancel = new Button();
            buttonAdd = new Button();
            buttonRemove = new Button();
            lbAll = new ListBox();
            lbCurrent = new ListBox();
            searchBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            groupBox1 = new GroupBox();
            descLabel = new Label();
            buttonUp = new Button();
            buttonDown = new Button();
            toolTip1 = new ToolTip(components);
            headingTextControl1 = new HeadingTextControl();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonOk
            // 
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.Location = new Point(436, 499);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(75, 23);
            buttonOk.TabIndex = 0;
            buttonOk.Text = "OK";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Location = new Point(350, 499);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 1;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonAdd
            // 
            buttonAdd.Enabled = false;
            buttonAdd.Location = new Point(234, 214);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(86, 23);
            buttonAdd.TabIndex = 4;
            buttonAdd.Text = "Add >>";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonRemove
            // 
            buttonRemove.Enabled = false;
            buttonRemove.Location = new Point(234, 243);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new Size(86, 23);
            buttonRemove.TabIndex = 5;
            buttonRemove.Text = "<< Remove";
            buttonRemove.UseVisualStyleBackColor = true;
            buttonRemove.Click += buttonRemove_Click;
            // 
            // lbAll
            // 
            lbAll.HorizontalScrollbar = true;
            lbAll.Location = new Point(29, 72);
            lbAll.Name = "lbAll";
            lbAll.SelectionMode = SelectionMode.MultiExtended;
            lbAll.Size = new Size(199, 349);
            lbAll.Sorted = true;
            lbAll.TabIndex = 6;
            lbAll.SelectedIndexChanged += lbAll_SelectedIndexChanged;
            // 
            // lbCurrent
            // 
            lbCurrent.HorizontalScrollbar = true;
            lbCurrent.Location = new Point(325, 72);
            lbCurrent.Name = "lbCurrent";
            lbCurrent.SelectionMode = SelectionMode.MultiExtended;
            lbCurrent.Size = new Size(185, 379);
            lbCurrent.TabIndex = 7;
            lbCurrent.SelectedIndexChanged += lbCurrent_SelectedIndexChanged;
            // 
            // searchBox
            // 
            searchBox.Location = new Point(29, 427);
            searchBox.Name = "searchBox";
            searchBox.PlaceholderText = "Search commands...";
            searchBox.Size = new Size(199, 23);
            searchBox.TabIndex = 8;
            searchBox.TextChanged += searchBox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 49);
            label1.Name = "label1";
            label1.Size = new Size(121, 15);
            label1.TabIndex = 9;
            label1.Text = "Available commands:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(313, 49);
            label2.Name = "label2";
            label2.Size = new Size(110, 15);
            label2.TabIndex = 10;
            label2.Text = "Pinned commands:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(descLabel);
            groupBox1.Location = new Point(31, 456);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(256, 67);
            groupBox1.TabIndex = 11;
            groupBox1.TabStop = false;
            groupBox1.Text = "Description";
            // 
            // descLabel
            // 
            descLabel.Location = new Point(11, 19);
            descLabel.Name = "descLabel";
            descLabel.Size = new Size(239, 40);
            descLabel.TabIndex = 0;
            descLabel.Text = "Select an item to view its description";
            // 
            // buttonUp
            // 
            buttonUp.Enabled = false;
            buttonUp.Location = new Point(294, 398);
            buttonUp.Name = "buttonUp";
            buttonUp.Size = new Size(26, 23);
            buttonUp.TabIndex = 12;
            buttonUp.Text = "▲";
            toolTip1.SetToolTip(buttonUp, "Move up");
            buttonUp.UseVisualStyleBackColor = true;
            buttonUp.Click += buttonUp_Click;
            // 
            // buttonDown
            // 
            buttonDown.Enabled = false;
            buttonDown.Location = new Point(294, 426);
            buttonDown.Name = "buttonDown";
            buttonDown.Size = new Size(26, 23);
            buttonDown.TabIndex = 13;
            buttonDown.Text = "▼";
            toolTip1.SetToolTip(buttonDown, "Move down");
            buttonDown.UseVisualStyleBackColor = true;
            buttonDown.Click += buttonDown_Click;
            // 
            // headingTextControl1
            // 
            headingTextControl1.Location = new Point(15, 7);
            headingTextControl1.Name = "headingTextControl1";
            headingTextControl1.Size = new Size(285, 36);
            headingTextControl1.TabIndex = 14;
            headingTextControl1.Text = "Customize Quick Access Toolbar";
            // 
            // QatCustomizer
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(527, 532);
            Controls.Add(headingTextControl1);
            Controls.Add(buttonDown);
            Controls.Add(buttonUp);
            Controls.Add(groupBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(searchBox);
            Controls.Add(lbCurrent);
            Controls.Add(lbAll);
            Controls.Add(buttonRemove);
            Controls.Add(buttonAdd);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOk);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "QatCustomizer";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Customize Quick Access Toolbar";
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonOk;
        private Button buttonCancel;
        private Button buttonAdd;
        private Button buttonRemove;
        private ListBox lbAll;
        private ListBox lbCurrent;
        private TextBox searchBox;
        private Label label1;
        private Label label2;
        private GroupBox groupBox1;
        private Label descLabel;
        private Button buttonUp;
        private Button buttonDown;
        private ToolTip toolTip1;
        private HeadingTextControl headingTextControl1;
    }
}