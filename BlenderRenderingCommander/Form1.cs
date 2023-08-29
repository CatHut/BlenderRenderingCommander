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

        // �L�����Z�� �g�[�N�� �\�[�X���쐬
        CancellationTokenSource Cts;

        // �L�����Z�� �g�[�N�� ���擾
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

            // �L�����Z�� �g�[�N�� �\�[�X���쐬
            Cts = new CancellationTokenSource();

            // �L�����Z�� �g�[�N�� ���擾
            Ct = Cts.Token;

            WriteUiValues();
        }


        private async void button_Rendering_Click(object sender, EventArgs e)
        {
            if (IsProcessing == false)
            {
                IsProcessing = true;
                button_Rendering.Text = "�����_�����O���~";
                ReadUiValues();

                if (false == CheckSettings())
                {
                    return;
                }

                // �񓯊����\�b�h���Ăяo��
                await Task.Run(() => RunCommandAsync());

                button_Rendering.Text = "�����_�����O";

            }
            else
            {
                InterruptionFlag = true;
            }

        }

        /// <summary>
        /// UI�ɐݒ肵���l��ǂݎ��
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
        /// �����Ŏ����Ă���l��UI�ɏ�������
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

            // Process�I�u�W�F�N�g���쐬
            Process p = new Process();


            // �R�}���h�v�����v�g�̃p�X���w��
            string exePath = "\"" + AS.Data.CurrentCommand.BlenerExePath + "\""; // �t�@�C���p�X�̑O��Ƀ_�u���N�H�[�e�[�V������ǉ�
            p.StartInfo.FileName = exePath;

            // �R�}���h���C���������w��
            p.StartInfo.Arguments = CreateOptionText(AS.Data.CurrentCommand);

            // �o�͂�ǂݎ���悤�ɂ���
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.RedirectStandardInput = true;
            //p.StartInfo.StandardInputEncoding = Encoding.UTF8; // ���{��ɑΉ������G���R�[�f�B���O���w��
            p.StartInfo.StandardOutputEncoding = Encoding.UTF8; // ���{��ɑΉ������G���R�[�f�B���O���w��

            // �E�B���h�E��\�����Ȃ��悤�ɂ���
            p.StartInfo.CreateNoWindow = true;

            //Debug.WriteLine("A");

            // �v���Z�X���N��
            p.Start();

            //Debug.WriteLine("B");

            // �o�͂�ǂݎ��
            while (!p.StandardOutput.EndOfStream)
            {
                //Debug.WriteLine("C");

                // ��s���ǂݎ��
                string line = await p.StandardOutput.ReadLineAsync();

                // �e�L�X�g�{�b�N�X�ɒǉ�����
                textBox_Log.Invoke((Action)(() => textBox_Log.AppendText(line + Environment.NewLine)));


                if (InterruptionFlag == true)
                {
                    // �e�L�X�g�{�b�N�X�ɒǉ�����
                    textBox_Log.Invoke((Action)(() => textBox_Log.AppendText("�����I�����܂�" + Environment.NewLine)));

                    //Debug.WriteLine("G");
                    // Ctrl+C�̃V�O�i���𑗂�
                    //p.StandardInput.WriteLine("\x3");
                    //Debug.WriteLine("H");
                    p.Kill();
                    InterruptionFlag = false;
                }

            }


            //Debug.WriteLine("D");

            await p.WaitForExitAsync();

            //Debug.WriteLine("E");

            // �v���Z�X�����
            p.Close();

            //Debug.WriteLine("F");
            IsProcessing = false;
            button_Rendering.Invoke((Action)(() => button_Rendering.Text = "�����_�����O"));


        }

        private string CreateOptionText(RenderingCommand rc)
        {
            //blender -b test.blend -S ����ҏW -a --verbose 0 -s 10 -e 20

            string ret = "";

            string exePath = "\"" + rc.BlenerExePath + "\""; // �t�@�C���p�X�̑O��Ƀ_�u���N�H�[�e�[�V������ǉ�
            string optionB = "-b";
            string filePath = "\"" + rc.BlenerFilePath + "\""; // �t�@�C���p�X�̑O��Ƀ_�u���N�H�[�e�[�V������ǉ�
            string optionS = "-S";
            string scene = "\"" + rc.Scene + "\""; // �t�@�C���p�X�̑O��Ƀ_�u���N�H�[�e�[�V������ǉ�
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

            // �L�����Z���̗v���𑗂�
            //Cts.Cancel();
        }
    }
}