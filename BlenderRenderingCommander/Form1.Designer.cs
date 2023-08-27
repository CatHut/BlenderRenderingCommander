namespace BlenderRenderingCommander
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
            button_Rendering = new Button();
            textBoxEx1 = new CatHut.TextBoxEx();
            label1 = new Label();
            textBoxEx2 = new CatHut.TextBoxEx();
            listView_History = new ListView();
            label_File = new Label();
            numericUpDown_Start = new NumericUpDown();
            numericUpDown_End = new NumericUpDown();
            textBoxEx3 = new CatHut.TextBoxEx();
            label3 = new Label();
            label_StartFrame = new Label();
            label_EndFrame = new Label();
            columnHeader_File = new ColumnHeader();
            columnHeader_Scene = new ColumnHeader();
            columnHeader_StartFrame = new ColumnHeader();
            columnHeader_EndFrame = new ColumnHeader();
            columnHeader_Path = new ColumnHeader();
            columnHeader_Exe = new ColumnHeader();
            textBox_Log = new TextBox();
            label_History = new Label();
            label_Log = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_Start).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_End).BeginInit();
            SuspendLayout();
            // 
            // button_Rendering
            // 
            button_Rendering.Location = new Point(661, 165);
            button_Rendering.Name = "button_Rendering";
            button_Rendering.Size = new Size(75, 23);
            button_Rendering.TabIndex = 0;
            button_Rendering.Text = "レンダリング";
            button_Rendering.UseVisualStyleBackColor = true;
            // 
            // textBoxEx1
            // 
            textBoxEx1.AllowDrop = true;
            textBoxEx1.Location = new Point(100, 12);
            textBoxEx1.Name = "textBoxEx1";
            textBoxEx1.Size = new Size(438, 23);
            textBoxEx1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 15);
            label1.Name = "label1";
            label1.Size = new Size(65, 15);
            label1.TabIndex = 2;
            label1.Text = "BlenderExe";
            // 
            // textBoxEx2
            // 
            textBoxEx2.AllowDrop = true;
            textBoxEx2.Location = new Point(100, 83);
            textBoxEx2.Name = "textBoxEx2";
            textBoxEx2.Size = new Size(438, 23);
            textBoxEx2.TabIndex = 3;
            // 
            // listView_History
            // 
            listView_History.Columns.AddRange(new ColumnHeader[] { columnHeader_File, columnHeader_Scene, columnHeader_StartFrame, columnHeader_EndFrame, columnHeader_Path, columnHeader_Exe });
            listView_History.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView_History.Location = new Point(29, 224);
            listView_History.Name = "listView_History";
            listView_History.Size = new Size(492, 206);
            listView_History.TabIndex = 4;
            listView_History.UseCompatibleStateImageBehavior = false;
            listView_History.View = View.Details;
            // 
            // label_File
            // 
            label_File.AutoSize = true;
            label_File.Location = new Point(69, 86);
            label_File.Name = "label_File";
            label_File.Size = new Size(25, 15);
            label_File.TabIndex = 5;
            label_File.Text = "File";
            label_File.TextAlign = ContentAlignment.TopRight;
            // 
            // numericUpDown_Start
            // 
            numericUpDown_Start.Location = new Point(100, 141);
            numericUpDown_Start.Name = "numericUpDown_Start";
            numericUpDown_Start.Size = new Size(96, 23);
            numericUpDown_Start.TabIndex = 7;
            // 
            // numericUpDown_End
            // 
            numericUpDown_End.Location = new Point(100, 170);
            numericUpDown_End.Name = "numericUpDown_End";
            numericUpDown_End.Size = new Size(96, 23);
            numericUpDown_End.TabIndex = 8;
            // 
            // textBoxEx3
            // 
            textBoxEx3.AllowDrop = true;
            textBoxEx3.Location = new Point(100, 112);
            textBoxEx3.Name = "textBoxEx3";
            textBoxEx3.Size = new Size(152, 23);
            textBoxEx3.TabIndex = 9;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(56, 115);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 10;
            label3.Text = "Scene";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // label_StartFrame
            // 
            label_StartFrame.AutoSize = true;
            label_StartFrame.Location = new Point(29, 145);
            label_StartFrame.Name = "label_StartFrame";
            label_StartFrame.Size = new Size(65, 15);
            label_StartFrame.TabIndex = 11;
            label_StartFrame.Text = "Start Frame";
            label_StartFrame.TextAlign = ContentAlignment.TopRight;
            // 
            // label_EndFrame
            // 
            label_EndFrame.AutoSize = true;
            label_EndFrame.Location = new Point(33, 172);
            label_EndFrame.Name = "label_EndFrame";
            label_EndFrame.Size = new Size(61, 15);
            label_EndFrame.TabIndex = 12;
            label_EndFrame.Text = "End Frame";
            label_EndFrame.TextAlign = ContentAlignment.TopRight;
            // 
            // columnHeader_File
            // 
            columnHeader_File.Text = "File";
            // 
            // columnHeader_Scene
            // 
            columnHeader_Scene.Text = "Scene";
            // 
            // columnHeader_StartFrame
            // 
            columnHeader_StartFrame.Text = "Start";
            // 
            // columnHeader_EndFrame
            // 
            columnHeader_EndFrame.Text = "End";
            // 
            // columnHeader_Path
            // 
            columnHeader_Path.Text = "Path";
            // 
            // columnHeader_Exe
            // 
            columnHeader_Exe.Text = "Exe";
            // 
            // textBox_Log
            // 
            textBox_Log.Location = new Point(29, 476);
            textBox_Log.Multiline = true;
            textBox_Log.Name = "textBox_Log";
            textBox_Log.ReadOnly = true;
            textBox_Log.Size = new Size(492, 171);
            textBox_Log.TabIndex = 13;
            // 
            // label_History
            // 
            label_History.AutoSize = true;
            label_History.Location = new Point(29, 206);
            label_History.Name = "label_History";
            label_History.Size = new Size(31, 15);
            label_History.TabIndex = 14;
            label_History.Text = "履歴";
            label_History.TextAlign = ContentAlignment.TopRight;
            // 
            // label_Log
            // 
            label_Log.AutoSize = true;
            label_Log.Location = new Point(29, 458);
            label_Log.Name = "label_Log";
            label_Log.Size = new Size(25, 15);
            label_Log.TabIndex = 15;
            label_Log.Text = "ログ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 704);
            Controls.Add(label_Log);
            Controls.Add(label_History);
            Controls.Add(textBox_Log);
            Controls.Add(label_EndFrame);
            Controls.Add(label_StartFrame);
            Controls.Add(label3);
            Controls.Add(textBoxEx3);
            Controls.Add(numericUpDown_End);
            Controls.Add(numericUpDown_Start);
            Controls.Add(label_File);
            Controls.Add(listView_History);
            Controls.Add(textBoxEx2);
            Controls.Add(label1);
            Controls.Add(textBoxEx1);
            Controls.Add(button_Rendering);
            Name = "Form1";
            Text = "Blender Rendering Commander";
            ((System.ComponentModel.ISupportInitialize)numericUpDown_Start).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_End).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button_Rendering;
        private CatHut.TextBoxEx textBoxEx1;
        private Label label1;
        private CatHut.TextBoxEx textBoxEx2;
        private ListView listView_History;
        private Label label_File;
        private NumericUpDown numericUpDown_Start;
        private NumericUpDown numericUpDown_End;
        private CatHut.TextBoxEx textBoxEx3;
        private Label label3;
        private Label label_StartFrame;
        private Label label_EndFrame;
        private ColumnHeader columnHeader_File;
        private ColumnHeader columnHeader_Scene;
        private ColumnHeader columnHeader_StartFrame;
        private ColumnHeader columnHeader_EndFrame;
        private ColumnHeader columnHeader_Path;
        private ColumnHeader columnHeader_Exe;
        private TextBox textBox_Log;
        private Label label_History;
        private Label label_Log;
    }
}