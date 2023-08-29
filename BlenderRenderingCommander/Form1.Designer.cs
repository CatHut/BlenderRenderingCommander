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
            components = new System.ComponentModel.Container();
            button_Rendering = new Button();
            textBoxEx_Exe = new CatHut.TextBoxEx();
            label1 = new Label();
            textBoxEx_File = new CatHut.TextBoxEx();
            listView_History = new ListView();
            columnHeader_File = new ColumnHeader();
            columnHeader_Scene = new ColumnHeader();
            columnHeader_StartFrame = new ColumnHeader();
            columnHeader_EndFrame = new ColumnHeader();
            columnHeader_Path = new ColumnHeader();
            columnHeader_Exe = new ColumnHeader();
            label_File = new Label();
            numericUpDown_Start = new NumericUpDown();
            numericUpDown_End = new NumericUpDown();
            textBoxEx_Scene = new CatHut.TextBoxEx();
            label3 = new Label();
            label_StartFrame = new Label();
            label_EndFrame = new Label();
            textBox_Log = new TextBox();
            label_History = new Label();
            label_Log = new Label();
            label_StatusLabel = new Label();
            label_Status = new Label();
            button_Clear = new Button();
            timer_ValueChanged = new System.Windows.Forms.Timer(components);
            label_Progress = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_Start).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_End).BeginInit();
            SuspendLayout();
            // 
            // button_Rendering
            // 
            button_Rendering.Location = new Point(83, 59);
            button_Rendering.Name = "button_Rendering";
            button_Rendering.Size = new Size(104, 23);
            button_Rendering.TabIndex = 0;
            button_Rendering.Text = "レンダリング";
            button_Rendering.UseVisualStyleBackColor = true;
            button_Rendering.Click += button_Rendering_Click;
            // 
            // textBoxEx_Exe
            // 
            textBoxEx_Exe.AllowDrop = true;
            textBoxEx_Exe.Location = new Point(83, 8);
            textBoxEx_Exe.Name = "textBoxEx_Exe";
            textBoxEx_Exe.Size = new Size(689, 23);
            textBoxEx_Exe.TabIndex = 1;
            textBoxEx_Exe.TextChanged += textBoxEx_Exe_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 11);
            label1.Name = "label1";
            label1.Size = new Size(65, 15);
            label1.TabIndex = 2;
            label1.Text = "BlenderExe";
            // 
            // textBoxEx_File
            // 
            textBoxEx_File.AllowDrop = true;
            textBoxEx_File.Location = new Point(83, 111);
            textBoxEx_File.Name = "textBoxEx_File";
            textBoxEx_File.Size = new Size(689, 23);
            textBoxEx_File.TabIndex = 3;
            textBoxEx_File.TextChanged += textBoxEx_File_TextChanged;
            // 
            // listView_History
            // 
            listView_History.Columns.AddRange(new ColumnHeader[] { columnHeader_File, columnHeader_Scene, columnHeader_StartFrame, columnHeader_EndFrame, columnHeader_Path, columnHeader_Exe });
            listView_History.FullRowSelect = true;
            listView_History.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView_History.Location = new Point(12, 262);
            listView_History.MultiSelect = false;
            listView_History.Name = "listView_History";
            listView_History.Size = new Size(760, 206);
            listView_History.TabIndex = 4;
            listView_History.UseCompatibleStateImageBehavior = false;
            listView_History.View = View.Details;
            listView_History.SelectedIndexChanged += listView_History_SelectedIndexChanged;
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
            // label_File
            // 
            label_File.AutoSize = true;
            label_File.Location = new Point(52, 114);
            label_File.Name = "label_File";
            label_File.Size = new Size(25, 15);
            label_File.TabIndex = 5;
            label_File.Text = "File";
            label_File.TextAlign = ContentAlignment.TopRight;
            // 
            // numericUpDown_Start
            // 
            numericUpDown_Start.Location = new Point(83, 169);
            numericUpDown_Start.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDown_Start.Name = "numericUpDown_Start";
            numericUpDown_Start.Size = new Size(96, 23);
            numericUpDown_Start.TabIndex = 7;
            numericUpDown_Start.ValueChanged += numericUpDown_Start_ValueChanged;
            // 
            // numericUpDown_End
            // 
            numericUpDown_End.Location = new Point(83, 198);
            numericUpDown_End.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDown_End.Name = "numericUpDown_End";
            numericUpDown_End.Size = new Size(96, 23);
            numericUpDown_End.TabIndex = 8;
            numericUpDown_End.ValueChanged += numericUpDown_End_ValueChanged;
            // 
            // textBoxEx_Scene
            // 
            textBoxEx_Scene.AllowDrop = true;
            textBoxEx_Scene.Location = new Point(83, 140);
            textBoxEx_Scene.Name = "textBoxEx_Scene";
            textBoxEx_Scene.Size = new Size(152, 23);
            textBoxEx_Scene.TabIndex = 9;
            textBoxEx_Scene.TextChanged += textBoxEx_Scene_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(39, 143);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 10;
            label3.Text = "Scene";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // label_StartFrame
            // 
            label_StartFrame.AutoSize = true;
            label_StartFrame.Location = new Point(12, 173);
            label_StartFrame.Name = "label_StartFrame";
            label_StartFrame.Size = new Size(65, 15);
            label_StartFrame.TabIndex = 11;
            label_StartFrame.Text = "Start Frame";
            label_StartFrame.TextAlign = ContentAlignment.TopRight;
            // 
            // label_EndFrame
            // 
            label_EndFrame.AutoSize = true;
            label_EndFrame.Location = new Point(16, 200);
            label_EndFrame.Name = "label_EndFrame";
            label_EndFrame.Size = new Size(61, 15);
            label_EndFrame.TabIndex = 12;
            label_EndFrame.Text = "End Frame";
            label_EndFrame.TextAlign = ContentAlignment.TopRight;
            // 
            // textBox_Log
            // 
            textBox_Log.Location = new Point(12, 498);
            textBox_Log.Multiline = true;
            textBox_Log.Name = "textBox_Log";
            textBox_Log.ReadOnly = true;
            textBox_Log.ScrollBars = ScrollBars.Both;
            textBox_Log.Size = new Size(760, 171);
            textBox_Log.TabIndex = 13;
            // 
            // label_History
            // 
            label_History.AutoSize = true;
            label_History.Location = new Point(12, 244);
            label_History.Name = "label_History";
            label_History.Size = new Size(31, 15);
            label_History.TabIndex = 14;
            label_History.Text = "履歴";
            label_History.TextAlign = ContentAlignment.TopRight;
            // 
            // label_Log
            // 
            label_Log.AutoSize = true;
            label_Log.Location = new Point(12, 480);
            label_Log.Name = "label_Log";
            label_Log.Size = new Size(25, 15);
            label_Log.TabIndex = 15;
            label_Log.Text = "ログ";
            // 
            // label_StatusLabel
            // 
            label_StatusLabel.AutoSize = true;
            label_StatusLabel.Location = new Point(26, 93);
            label_StatusLabel.Name = "label_StatusLabel";
            label_StatusLabel.Size = new Size(51, 15);
            label_StatusLabel.TabIndex = 16;
            label_StatusLabel.Text = "ステータス";
            label_StatusLabel.TextAlign = ContentAlignment.TopRight;
            // 
            // label_Status
            // 
            label_Status.AutoSize = true;
            label_Status.Location = new Point(83, 93);
            label_Status.Name = "label_Status";
            label_Status.Size = new Size(43, 15);
            label_Status.TabIndex = 17;
            label_Status.Text = "未実行";
            // 
            // button_Clear
            // 
            button_Clear.Location = new Point(185, 169);
            button_Clear.Name = "button_Clear";
            button_Clear.Size = new Size(50, 23);
            button_Clear.TabIndex = 18;
            button_Clear.Text = "クリア";
            button_Clear.UseVisualStyleBackColor = true;
            button_Clear.Click += button_Clear_Click;
            // 
            // timer_ValueChanged
            // 
            timer_ValueChanged.Tick += timer_ValueChanged_Tick;
            // 
            // label_Progress
            // 
            label_Progress.AutoSize = true;
            label_Progress.Location = new Point(158, 59);
            label_Progress.Name = "label_Progress";
            label_Progress.Size = new Size(0, 15);
            label_Progress.TabIndex = 19;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 681);
            Controls.Add(label_Progress);
            Controls.Add(button_Clear);
            Controls.Add(label_Status);
            Controls.Add(label_StatusLabel);
            Controls.Add(label_Log);
            Controls.Add(label_History);
            Controls.Add(textBox_Log);
            Controls.Add(label_EndFrame);
            Controls.Add(label_StartFrame);
            Controls.Add(label3);
            Controls.Add(textBoxEx_Scene);
            Controls.Add(numericUpDown_End);
            Controls.Add(numericUpDown_Start);
            Controls.Add(label_File);
            Controls.Add(listView_History);
            Controls.Add(textBoxEx_File);
            Controls.Add(label1);
            Controls.Add(textBoxEx_Exe);
            Controls.Add(button_Rendering);
            Name = "Form1";
            Text = "Blender Rendering Commander";
            FormClosed += Form1_FormClosed;
            ((System.ComponentModel.ISupportInitialize)numericUpDown_Start).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_End).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button_Rendering;
        private CatHut.TextBoxEx textBoxEx_Exe;
        private Label label1;
        private CatHut.TextBoxEx textBoxEx_File;
        private ListView listView_History;
        private Label label_File;
        private NumericUpDown numericUpDown_Start;
        private NumericUpDown numericUpDown_End;
        private CatHut.TextBoxEx textBoxEx_Scene;
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
        private Label label_StatusLabel;
        private Label label_Status;
        private Button button_Clear;
        private System.Windows.Forms.Timer timer_ValueChanged;
        private Label label_Progress;
    }
}