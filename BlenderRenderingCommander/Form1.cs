using CatHut;
using System.Diagnostics;
using System.Text;

namespace BlenderRenderingCommander
{
    public partial class Form1 : Form
    {
        AppSetting<BrcData> AS;
        bool EventEnable = true;
        bool InterruptionFlag = false;
        bool IsProcessing = false;
        DateTime StartTime;
        int MAX_HISTORY_NUM = 10;


        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            if (AS == null)
            {
                AS = new AppSetting<BrcData>();
            }
            AS.Load();

            WriteUiValues();
        }

        private void DisableUi()
        {
            textBoxEx_Exe.Enabled = false;
            textBoxEx_File.Enabled = false;
            numericUpDown_Start.Enabled = false;
            numericUpDown_End.Enabled = false;
            textBoxEx_Scene.Enabled = false;
            button_Clear.Enabled = false;
            listView_History.Enabled = false;
        }

        private void EnableUi()
        {
            textBoxEx_Exe.Enabled = true;
            textBoxEx_File.Enabled = true;
            numericUpDown_Start.Enabled = true;
            numericUpDown_End.Enabled = true;
            textBoxEx_Scene.Enabled = true;
            button_Clear.Enabled = true;
            listView_History.Enabled = true;
        }

        private void UpdateCommandHistory(RenderingCommand rc)
        {
            if (AS.Data.CommandHistory.Contains(rc))
            {
                AS.Data.CommandHistory.Remove(rc);
            }

            var temp = CatHutCommon.DeepClone(rc);
            AS.Data.CommandHistory.Enqueue(temp);

            if (AS.Data.CommandHistory.Count > MAX_HISTORY_NUM)
            {
                AS.Data.CommandHistory.Dequeue();
            }

        }


        private async void button_Rendering_Click(object sender, EventArgs e)
        {
            if (IsProcessing == false)
            {
                button_Rendering.Text = "レンダリング中止";
                label_Status.Text = "実行中";

                StartTime = DateTime.Now;
                label_StartDateTime.Text = "開始日時:" + StartTime.ToString("MM/dd HH:mm:ss");
                ReadUiValues();
                DisableUi();
                if (false == CheckSettings())
                {
                    //実行しない
                    EnableUi();
                    button_Rendering.Text = "レンダリング";
                    label_Status.Text = "未実行";
                    label_StartDateTime.Text = "開始日時:MM/DD hh:mm:ss";
                    label_EndDateTime.Text = "終了日時:MM/DD hh:mm:ss";
                    label_Progress.Text = "";

                }
                else
                {
                    UpdateCommandHistory(AS.Data.CurrentCommand);
                    IsProcessing = true;

                    //実行
                    // 非同期メソッドを呼び出す
                    await Task.Run(() => RunCommandAsync());

                    EnableUi();
                    button_Rendering.Text = "レンダリング";
                    label_Status.Text = "未実行";
                    DateTime dt = DateTime.Now;
                    label_EndDateTime.Text = "終了日時:" + dt.ToString("MM/dd HH:mm:ss");
                    TimeSpan ts = dt - StartTime;
                    label_PastTime.Text = "経過時間:" + ts.ToString(@"hh\:mm\:ss");
                    label_Progress.Text = "";
                    textBox_Log.AppendText("****" + textBoxEx_File.FileNameWithExtension + " Rendering finished. ****" + Environment.NewLine);
                    WriteUiValues();
                }

            }
            else
            {
                InterruptionFlag = true;
            }

        }

        /// <summary>
        /// UIに設定した値を読み取る
        /// </summary>
        private void ReadUiValues()
        {
            EventEnable = false;
            {

                AS.Data.CurrentCommand.BlenerExePath = textBoxEx_Exe.Text;
                AS.Data.CurrentCommand.BlenerFilePath = textBoxEx_File.Text;
                AS.Data.CurrentCommand.EndFrame = (int)numericUpDown_End.Value;
                AS.Data.CurrentCommand.StartFrame = (int)numericUpDown_Start.Value;
                AS.Data.CurrentCommand.Scene = textBoxEx_Scene.Text;
                AS.Data.CurrentCommand.OutputFolder = textBoxEx_OutputFolder.Text;

                AS.Save();
            }
            EventEnable = true;
        }

        /// <summary>
        /// 内部で持っている値をUIに書き込む
        /// </summary>
        private void WriteUiValues()
        {
            EventEnable = false;
            {
                textBoxEx_Exe.Text = AS.Data.CurrentCommand.BlenerExePath;
                textBoxEx_File.Text = AS.Data.CurrentCommand.BlenerFilePath;
                numericUpDown_End.Value = AS.Data.CurrentCommand.EndFrame;
                numericUpDown_Start.Value = AS.Data.CurrentCommand.StartFrame;
                textBoxEx_Scene.Text = AS.Data.CurrentCommand.Scene;
                textBoxEx_OutputFolder.Text = AS.Data.CurrentCommand.OutputFolder;


                listView_History.Items.Clear();

                foreach (var cmd in AS.Data.CommandHistory.ToArray().Reverse())
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = Path.GetFileName(cmd.BlenerFilePath);
                    lvi.SubItems.Add(cmd.Scene);
                    lvi.SubItems.Add(cmd.StartFrame.ToString());
                    lvi.SubItems.Add(cmd.EndFrame.ToString());
                    lvi.SubItems.Add(cmd.BlenerFilePath);
                    lvi.SubItems.Add(cmd.BlenerExePath);
                    lvi.SubItems.Add(cmd.OutputFolder);

                    listView_History.Items.Add(lvi);

                }

                foreach (ColumnHeader ch in listView_History.Columns)
                {
                    ch.Width = -2;
                }

                listView_History.SelectedIndices.Clear();
            }

            EventEnable = true;
        }

        private async Task RunCommandAsync()
        {

            // Processオブジェクトを作成
            Process p = new Process();


            // コマンドプロンプトのパスを指定
            string exePath = "\"" + AS.Data.CurrentCommand.BlenerExePath + "\""; // ファイルパスの前後にダブルクォーテーションを追加
            p.StartInfo.FileName = exePath;

            // コマンドライン引数を指定
            p.StartInfo.Arguments = CreateOptionText(AS.Data.CurrentCommand);

            // 出力を読み取れるようにする
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.RedirectStandardInput = true;
            //p.StartInfo.StandardInputEncoding = Encoding.UTF8; // 日本語に対応したエンコーディングを指定
            p.StartInfo.StandardOutputEncoding = Encoding.UTF8; // 日本語に対応したエンコーディングを指定

            // ウィンドウを表示しないようにする
            p.StartInfo.CreateNoWindow = true;

            //Debug.WriteLine("A");

            // プロセスを起動
            p.Start();

            //Debug.WriteLine("B");

            // 出力を読み取る
            while (!p.StandardOutput.EndOfStream)
            {
                //Debug.WriteLine("C");

                // 一行ずつ読み取る
                string line = await p.StandardOutput.ReadLineAsync();

                // テキストボックスに追加する
                textBox_Log.Invoke((Action)(() => textBox_Log.AppendText(line + Environment.NewLine)));

                //フレーム表示更新
                if (line.IndexOf("Append frame ") > -1)
                {
                    var str = line.Replace("Append ", "");
                    label_Progress.Invoke((Action)(() => label_Progress.Text = str));

                    DateTime dt = DateTime.Now;
                    TimeSpan ts = dt - StartTime;
                    label_PastTime.Invoke((Action)(() => label_PastTime.Text = "経過時間:" + ts.ToString(@"hh\:mm\:ss")));

                }

                if (InterruptionFlag == true)
                {
                    // テキストボックスに追加する
                    textBox_Log.Invoke((Action)(() => textBox_Log.AppendText("強制終了します" + Environment.NewLine)));

                    //Debug.WriteLine("G");
                    // Ctrl+Cのシグナルを送る
                    //p.StandardInput.WriteLine("\x3");
                    //Debug.WriteLine("H");
                    p.Kill();
                    InterruptionFlag = false;
                }

            }


            //Debug.WriteLine("D");

            await p.WaitForExitAsync();

            //Debug.WriteLine("E");

            // プロセスを閉じる
            p.Close();

            //Debug.WriteLine("F");
            IsProcessing = false;
            button_Rendering.Invoke((Action)(() => button_Rendering.Text = "レンダリング"));


        }

        private string CreateOptionText(RenderingCommand rc)
        {
            //blender -b test.blend -S 動画編集 -a --verbose 0 -s 10 -e 20

            string ret = "";

            string exePath = "\"" + rc.BlenerExePath + "\""; // ファイルパスの前後にダブルクォーテーションを追加
            string optionB = "-b";
            string filePath = "\"" + rc.BlenerFilePath + "\""; // ファイルパスの前後にダブルクォーテーションを追加
            string optionS = "-S";
            string scene = "\"" + rc.Scene + "\""; // ファイルパスの前後にダブルクォーテーションを追加
            string optionStart = "-s";
            string startFrame = rc.StartFrame.ToString();
            string optionEnd = "-e";
            string endFrame = rc.EndFrame.ToString();
            string optionO = "-o";
            string outputFoler = "\"" + rc.OutputFolder + "/\""; // ファイルパスの前後にダブルクォーテーションを追加
            string optionVerbose = "--verbose 0";
            string optionA = "-a";

            //ret += exePath + " ";
            ret += optionB + " ";
            ret += filePath + " ";
            ret += optionS + " ";
            ret += scene + " ";
            if (rc.StartFrame != 0 && rc.EndFrame != 0)
            {
                ret += " ";
                ret += optionStart + " ";
                ret += startFrame + " ";
                ret += optionEnd + " ";
                ret += endFrame + " ";
            }
            if (rc.OutputFolder != "")
            {
                ret += optionO + " ";
                ret += outputFoler + " ";
            }

            ret += optionVerbose + " ";
            ret += optionA;

            textBox_Log.Invoke((Action)(() => textBox_Log.AppendText("Created Command" + Environment.NewLine)));
            textBox_Log.Invoke((Action)(() => textBox_Log.AppendText(ret + Environment.NewLine)));

            return ret;

        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            if (EventEnable == false) { return; }
            EventEnable = false;
            {
                AS.Data.CurrentCommand.EndFrame = 0;
                AS.Data.CurrentCommand.StartFrame = 0;
                WriteUiValues();
            }
            EventEnable = true;
        }

        private bool CheckSettings()
        {
            if (textBoxEx_Exe.FileExists == false)
            {
                textBox_Log.AppendText(textBoxEx_Exe.Text + " doesn't Exist." + Environment.NewLine);
                return false;
            }

            if (textBoxEx_File.FileExists == false)
            {
                textBox_Log.AppendText(textBoxEx_File.Text + " doesn't Exist." + Environment.NewLine);
                return false;
            }

            if (textBoxEx_Scene.Text == "")
            {
                textBox_Log.AppendText("Please input any Scene Name." + Environment.NewLine);
                return false;
            }

            if (textBoxEx_OutputFolder.Text != "" && !Directory.Exists(textBoxEx_OutputFolder.Text))
            {
                textBox_Log.AppendText("Output Folder doesn't exists." + Environment.NewLine);
                return false;
            }


            return true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            AS.Save();
        }

        private void timer_ValueChanged_Tick(object sender, EventArgs e)
        {
            timer_ValueChanged.Stop();
            if (EventEnable == false) { return; }
            EventEnable = false;
            {
                ReadUiValues();
            }
            EventEnable = true;
        }

        private void textBoxEx_Exe_TextChanged(object sender, EventArgs e)
        {
            timer_ValueChanged.Stop();
            timer_ValueChanged.Start();
        }

        private void textBoxEx_File_TextChanged(object sender, EventArgs e)
        {
            timer_ValueChanged.Stop();
            timer_ValueChanged.Start();

        }

        private void textBoxEx_Scene_TextChanged(object sender, EventArgs e)
        {
            timer_ValueChanged.Stop();
            timer_ValueChanged.Start();

        }

        private void numericUpDown_Start_ValueChanged(object sender, EventArgs e)
        {
            if (EventEnable == false) { return; }
            EventEnable = false;
            {
                if (numericUpDown_End.Value < numericUpDown_Start.Value)
                {
                    numericUpDown_Start.Value = numericUpDown_End.Value;
                }

            }
            EventEnable = true;

            timer_ValueChanged.Stop();
            timer_ValueChanged.Start();

        }

        private void numericUpDown_End_ValueChanged(object sender, EventArgs e)
        {
            if (EventEnable == false) { return; }
            EventEnable = false;
            {
                if (numericUpDown_End.Value < numericUpDown_Start.Value)
                {
                    numericUpDown_End.Value = numericUpDown_Start.Value;
                }

            }
            EventEnable = true;

            timer_ValueChanged.Stop();
            timer_ValueChanged.Start();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InterruptionFlag = true;
        }

        private void listView_History_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EventEnable == false) { return; }
            EventEnable = false;
            {
                if (listView_History.SelectedItems.Count > 0)
                {

                    textBoxEx_Exe.Text = listView_History.SelectedItems[0].SubItems[5].Text;
                    textBoxEx_File.Text = listView_History.SelectedItems[0].SubItems[4].Text;
                    numericUpDown_End.Value = decimal.Parse(listView_History.SelectedItems[0].SubItems[3].Text);
                    numericUpDown_Start.Value = decimal.Parse(listView_History.SelectedItems[0].SubItems[2].Text);
                    textBoxEx_Scene.Text = listView_History.SelectedItems[0].SubItems[1].Text;

                    ReadUiValues();

                }
            }
            EventEnable = true;
        }

        private void textBoxEx_OutputFolder_TextChanged(object sender, EventArgs e)
        {
            timer_ValueChanged.Stop();
            timer_ValueChanged.Start();
        }
    }
}