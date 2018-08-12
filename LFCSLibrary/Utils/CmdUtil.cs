using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LFCSLibrary.Utils
{
    public class CmdUtil
    {
        /// <summary>
        /// 执行cmd命令 2018.08.12
        /// </summary>
        /// <param name="cmd">需要执行的命令</param>
        /// <param name="cmdPath">指定cmd.exe在本机中的全路径，留空则默认为：C:\Windows\System32\cmd.exe</param>
        /// <returns>cmd执行完毕后窗口的输出信息</returns>
        public static string RunCmd(string cmd, string cmdPath)
        {
            string outPut = string.Empty;
            if (string.IsNullOrWhiteSpace(cmdPath))
                cmdPath = @"C:\Windows\System32\cmd.exe";
            //不管命令是否成功均执行exit命令，否则当调用ReadToEnd()方法时会处于假死状态
            cmd = cmd.Trim().TrimEnd('&') + "&exit";
            using (Process p = new Process())
            {
                p.StartInfo.FileName = cmdPath;
                p.StartInfo.UseShellExecute = false; // 是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true; // 接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true; // 由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;  // 重定向标准错误输出
                p.StartInfo.CreateNoWindow = true; // 不显示程序窗口
                p.Start(); // 启动程序
                p.StandardInput.AutoFlush = true;
                // 向cmd窗口写入命令
                p.StandardInput.WriteLine(cmd);
                //设置执行命令（使用上面方式写入命令则不用下面的方式）
                //p.StartInfo.Arguments = "/c " + cmd;

                //p.StandardInput.WriteLine("exit");

                // 获取cmd窗口的输出信息
                outPut = p.StandardOutput.ReadToEnd();
                p.WaitForExit(); // 等待程序执行完退出进程
                p.Close();
            }
            return outPut;
        }


    }
}
