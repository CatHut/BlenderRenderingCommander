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

        // キャンセル トークン ソースを作成
        CancellationTokenSource Cts;

        // キャンセル トークン を取得
        CancellationToken Ct;


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

            // キャンセル トークン ソースを作成
            Cts = new CancellationTokenSource();

            // キャンセル トークン を取得
            Ct = Cts.Token;

            WriteUiValues();
        }


        private async void button_Rendering_Click(object sender, EventArgs e)
        {
            if (IsProcessing == false)
            {
                IsProcessing = true;
                button_Rendering.Text = "レンダリング中止";
                ReadUiValues();

                if (false == CheckSettings())
                {
                    return;
                }

                // 非同期メソッドを呼び出す
                await Task.Run(() => RunCommandAsync());

                button_Rendering.Text = "レンダリング";

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

            textBoxEx_Exe.Text = AS.Data.CurrentCommand.BlenerExePath;
            textBoxEx_File.Text = AS.Data.CurrentCommand.BlenerFilePath;
            numericUpDown_End.Value = AS.Data.CurrentCommand.EndFrame;
            numericUpDown_Start.Value = AS.Data.CurrentCommand.StartFrame;
            textBoxEx_Scene.Text = AS.Data.CurrentCommand.Scene;

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
            timer_ValueChanged.Stop();
            timer_ValueChanged.Start();

        }

        private void numericUpDown_End_ValueChanged(object sender, EventArgs e)
        {
            timer_ValueChanged.Stop();
            timer_ValueChanged.Start();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InterruptionFlag = true;

            // キャンセルの要求を送る
            //Cts.Cancel();
        }
    }
}