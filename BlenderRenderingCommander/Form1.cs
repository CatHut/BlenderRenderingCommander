using CatHut;
using System.Diagnostics;

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

                // �񓯊����\�b�h���Ăяo��
                await RunCommandAsync();

            }
            EnableEvent = true;
        }

        /// <summary>
        /// UI�ɐݒ肵���l��ǂݎ��
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
            }
            EnableEvent = true;
        }

        /// <summary>
        /// �����Ŏ����Ă���l��UI�ɏ�������
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
            // Process�I�u�W�F�N�g���쐬
            Process p = new Process();

            // �R�}���h�v�����v�g�̃p�X���w��
            p.StartInfo.FileName = Environment.GetEnvironmentVariable("ComSpec");

            // �R�}���h���C��������tree�R�}���h���w��
            p.StartInfo.Arguments = "/c chcp 65001 && " + CreateCommandText(AS.Data.CurrentCommand);

            // �o�͂�ǂݎ���悤�ɂ���
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = false;

            // �E�B���h�E��\�����Ȃ��悤�ɂ���
            p.StartInfo.CreateNoWindow = true;

            // �v���Z�X���N��
            p.Start();

            // �o�͂�ǂݎ��
            while (!p.StandardOutput.EndOfStream)
            {
                // ��s���ǂݎ��
                string line = await p.StandardOutput.ReadLineAsync();

                // �e�L�X�g�{�b�N�X�ɒǉ�����
                textBox_Log.AppendText(line + Environment.NewLine);
            }

            // �v���Z�X���I��
            p.WaitForExit();
            p.Close();
        }

        private string CreateCommandText(RenderingCommand rc)
        {
            //blender -b test.blend -S ����ҏW -a --verbose 0 -s 10 -e 20

            string ret = "";

            string exePath = "\"" + rc.BlenerExePath + "\""; // �t�@�C���p�X�̑O��Ƀ_�u���N�H�[�e�[�V������ǉ�
            string optionB = "-b";
            string filePath = "\"" + rc.BlenerFilePath + "\""; // �t�@�C���p�X�̑O��Ƀ_�u���N�H�[�e�[�V������ǉ�
            string optionS = "-S";
            string scene = "\"" + rc.Scene + "\""; // �t�@�C���p�X�̑O��Ƀ_�u���N�H�[�e�[�V������ǉ�
            string optionA = "-a";
            string optionVerbose = "--verbose 0";
            string optionStart = "-s";
            string startFrame = rc.StartFrame.ToString();
            string optionEnd = "-e";
            string endFrame = rc.EndFrame.ToString();

            ret += exePath + " ";
            ret += optionB + " ";
            ret += filePath + " ";
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
    }
}