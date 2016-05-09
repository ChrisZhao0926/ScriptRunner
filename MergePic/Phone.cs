using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MergePic
{
    class Phone
    {
        public Phone()
        {
            //contruction function
            //bool isLandscape = false;
        }
        public void SetStayOn()
        {
            
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            // 关闭Shell的使用
            p.StartInfo.UseShellExecute = false;
            // 重定向标准输入
            p.StartInfo.RedirectStandardInput = true;
            // 重定向标准输出
            p.StartInfo.RedirectStandardOutput = true;
            //重定向错误输出
            p.StartInfo.RedirectStandardError = true;
            // 设置不显示窗口
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            List<string> cmds = new List<string>();
            //cmds.Add("cd C:\\adbForACT");
            cmds.Add("adb shell /system/bin/svc power stayon usb");
            foreach (string cmd in cmds)
            {
                p.StandardInput.WriteLine(cmd);
                Thread.Sleep(1000);
            };
        }
        public void InstallAPK()
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            // 关闭Shell的使用
            p.StartInfo.UseShellExecute = false;
            // 重定向标准输入
            p.StartInfo.RedirectStandardInput = true;
            // 重定向标准输出
            p.StartInfo.RedirectStandardOutput = true;
            //重定向错误输出
            p.StartInfo.RedirectStandardError = true;
            // 设置不显示窗口
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            List<string> cmds = new List<string>();
            //cmds.Add("cd C:\\adbForACT");
            cmds.Add("adb devices");
            cmds.Add("adb install  gort_sdk-19.apk");
            //cmds.Add("pause");
            //依次输入我们需要的命令行
            foreach (string cmd in cmds)
            {
                p.StandardInput.WriteLine(cmd);
            }
        }
        public bool IsLandscape()
        { 
            bool result =false;
            string cur = "";
            string width = "";
            string height = "";
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            // 关闭Shell的使用
            p.StartInfo.UseShellExecute = false;
            // 重定向标准输入
            p.StartInfo.RedirectStandardInput = true;
            // 重定向标准输出
            p.StartInfo.RedirectStandardOutput = true;
            //重定向错误输出
            p.StartInfo.RedirectStandardError = true;
            // 设置不显示窗口
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            List<string> cmds = new List<string>();
            //cmds.Add("cd C:\\adbForACT");
            cmds.Add("adb shell dumpsys window displays  | findstr \"init\"");
            //依次输入我们需要的命令行
            foreach (string cmd in cmds)
            {
                p.StandardInput.WriteLine(cmd);
            }
            //得到输入命令后得到的返回值
            StringBuilder rstBuilder = new StringBuilder();
            p.StandardInput.Close();
            while (!p.StandardOutput.EndOfStream)
            {
                string stmp = p.StandardOutput.ReadLine();
                rstBuilder.Append(stmp + Environment.NewLine);

                if (stmp.IndexOf("    init") != -1)
                {
                    //listBox_Devices.Items.Add(stmp.Substring(0, stmp.IndexOf("\t")));
                    //listBoxLog.Items.Add(DateTime.Now.ToString() + " " + stmp.Substring(0, stmp.IndexOf("\t")));
                    //dataGridViewShowDevice.Rows.Add(stmp.Substring(0, stmp.IndexOf("\t")), myLanguage);
                    //listBoxLog.Items.Add(DateTime.Now.ToString() + " " + "Found device: " + stmp.Substring(0, stmp.IndexOf("\t")));
                    int startCur = stmp.IndexOf("cu");
                    int endCur = stmp.Substring(startCur).IndexOf(" ");
                    cur = stmp.Substring(startCur, endCur);
                    int equ = cur.IndexOf("=");
                    int x = cur.IndexOf("x");
                    width = cur.Substring(equ + 1,x-equ-1);
                    height = cur.Substring(x + 1);
                    if (Int16.Parse(width) > Int16.Parse(height))
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }
    }
}
