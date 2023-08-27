using CatHut;
using System.Diagnostics;
using System.Text;

namespace BlenderRenderingCommander
{
    public partial class Form1 : Form
    {
        AppSetting<BrcData> AS;
        bool EnableEvent = true;


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


        private async void button_Rendering_Click(object sender, EventArgs e)
        {
            if (EnableEvent == false) { return; }
            EnableEvent = false;
            {
                ReadUiValues();

                if (false == CheckSettings())
                {
                    EnableEvent = true;
                    return;
                }

                // 非同期メソッドを呼び出す
                await RunCommandAsync();

            }
            EnableEvent = true;
        }

        /// <summary>
        /// UIに設定した値を読み取る
        /// </summary>
        private void ReadUiValues()
        {
            EnableEvent = false;
            {
                AS.Data.CurrentCommand.BlenerExePath = textBoxEx_Exe.Text;
                AS.Data.CurrentCommand.BlenerFilePath = textBoxEx_File.Text;
                AS.Data.CurrentCommand.EndFrame = (int)numericUpDown_End.Value;
                AS.Data.CurrentCommand.StartFrame = (int)numericUpDown_Start.Value;
                AS.Data.CurrentCommand.Scene = textBoxEx_Scene.Text;

                AS.Save();
            }
            EnableEvent = true;
        }

        /// <summary>
        /// 内部で持っている値をUIに書き込む
        /// </summary>
        private void WriteUiValues()
        {
            EnableEvent = false;

            textBoxEx_Exe.Text = AS.Data.CurrentCommand.BlenerExePath;
            textBoxEx_File.Text = AS.Data.CurrentCommand.BlenerFilePath;
            numericUpDown_End.Value = AS.Data.CurrentCommand.EndFrame;
            numericUpDown_Start.Value = AS.Data.CurrentCommand.StartFrame;
            textBoxEx_Scene.Text = AS.Data.CurrentCommand.Scene;

            EnableEvent = true;
        }

        private async Task RunCommandAsync()
        {
            // Processオブジェクトを作成
            Process p = new Process();

            // コマンドプロンプトのパスを指定
            p.StartInfo.FileName = Environment.GetEnvironmentVariable("ComSpec");

            // コマンドライン引数にtreeコマンドを指定
            p.StartInfo.Arguments = "/c chcp 65001 && " + CreateCommandText(AS.Data.CurrentCommand);
            //p.StartInfo.Arguments = "/c " + CreateCommandText(AS.Data.CurrentCommand);

            // 出力を読み取れるようにする
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = false;
            //p.StartInfo.StandardInputEncoding = Encoding.UTF8; // 日本語に対応したエンコーディングを指定
            p.StartInfo.StandardOutputEncoding = Encoding.UTF8; // 日本語に対応したエンコーディングを指定

            // ウィンドウを表示しないようにする
            p.StartInfo.CreateNoWindow = true;

            // プロセスを起動
            p.Start();

            // 出力を読み取る
            while (!p.StandardOutput.EndOfStream)
            {
                // 一行ずつ読み取る
                string line = await p.StandardOutput.ReadLineAsync();

                // テキストボックスに追加する
                textBox_Log.AppendText(line + Environment.NewLine);
            }

            // プロセスを終了
            p.WaitForExit();
            p.Close();
        }

        private string CreateCommandText(RenderingCommand rc)
        {
            //blender -b test.blend -S 動画編集 -a --verbose 0 -s 10 -e 20

            string ret = "";

            string exePath = "\"" + rc.BlenerExePath + "\""; // ファイルパスの前後にダブルクォーテーションを追加
            string optionB = "-b";
            string filePath = "\"" + rc.BlenerFilePath + "\""; // ファイルパスの前後にダブルクォーテーションを追加
            string optionS = "-S";
            string scene = "\"" + rc.Scene + "\""; // ファイルパスの前後にダブルクォーテーションを追加
            string optionA = "-a";
            string optionVerbose = "--verbose 0";
            string optionStart = "-s";
            string startFrame = rc.StartFrame.ToString();
            string optionEnd = "-e";
            string endFrame = rc.EndFrame.ToString();

            ret += exePath + " ";
            ret += optionB + " ";
            ret += filePath + " ";
            ret += optionS + " ";
            ret += scene + " ";
            ret += optionA + " ";
            ret += optionVerbose;

            if (rc.StartFrame != 0 && rc.EndFrame != 0)
            {
                ret += " ";
                ret += optionStart + " ";
                ret += startFrame + " ";
                ret += optionEnd + " ";
                ret += endFrame + " ";
            }

            textBox_Log.AppendText("Created Command" + Environment.NewLine);
            textBox_Log.AppendText(ret + Environment.NewLine);

            return ret;

        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            if (EnableEvent == false) { return; }
            EnableEvent = false;
            {
                AS.Data.CurrentCommand.EndFrame = 0;
                AS.Data.CurrentCommand.StartFrame = 0;
                WriteUiValues();
            }
            EnableEvent = true;
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

            return true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            AS.Save();
        }

        private void timer_ValueChanged_Tick(object sender, EventArgs e)
        {
            timer_ValueChanged.Stop();
            if (EnableEvent == false) { return; }
            EnableEvent = false;
            {
                ReadUiValues();
            }
            EnableEvent = true;
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
            timer_ValueChanged.Stop();
            timer_ValueChanged.Start();

        }

        private void numericUpDown_End_ValueChanged(object sender, EventArgs e)
        {
            timer_ValueChanged.Stop();
            timer_ValueChanged.Start();

        }
    }
}