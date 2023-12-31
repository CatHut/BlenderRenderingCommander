﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatHut;

namespace BlenderRenderingCommander
{
    public class RenderingCommand
    {
        public string BlenerExePath { get; set; }
        public string BlenerFilePath { get; set; }
        public string Scene { get; set; }
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }
        public string OutputFolder { get; set; }

        public RenderingCommand() 
        {
            BlenerExePath = "";
            BlenerFilePath = "";
            Scene = "";
            StartFrame = 0;
            EndFrame = 0;
            OutputFolder = "";
        }


        /// <summary>
        /// 比較対象と内容を比較する
        /// </summary>
        /// <param name="rc">比較対象</param>
        /// <returns>true:一致、false:不一致</returns>
        public bool IsEqual(RenderingCommand rc)
        {

            if (BlenerExePath != rc.BlenerExePath)
            {
                return false;
            }

            if (BlenerFilePath != rc.BlenerFilePath)
            {
                return false;
            }

            if (Scene != rc.Scene)
            {
                return false;
            }

            if (StartFrame != rc.StartFrame)
            {
                return false;
            }

            if (EndFrame != rc.EndFrame)
            {
                return false;
            }

            if (OutputFolder != rc.OutputFolder)
            {
                return false;
            }

            return true;

        }


        // Equalsメソッドをオーバーライドする
        public override bool Equals(object obj)
        {
            // objがRenderingCommand型でなければfalseを返す
            if (!(obj is RenderingCommand))
            {
                return false;
            }

            // objをRenderingCommand型にキャストする
            RenderingCommand other = (RenderingCommand)obj;

            // コマンドの内容やパラメータなどを比較して、等しいかどうかを返す
            return IsEqual(other);
        }

    }


    public class BrcData
    {

        public RenderingCommand CurrentCommand { get; set; }

        public SerializableQueue<RenderingCommand> CommandHistory { get; set; }

        public BrcData()
        {
            CommandHistory = new SerializableQueue<RenderingCommand>();
            CurrentCommand = new RenderingCommand();
        }


    }
}
