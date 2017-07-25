using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MergePic
{
    public partial class Form1 : Form
    {
        Process p = new Process();
        //Process p1 = new Process();
        String language = "";
        String country = "";
        List<string> EnPicOrig = new List<string>();
        List<string> anotherPicOrig = new List<string>();
        List<string> EnPic = new List<string>();
        List<string> anotherPic = new List<string>();
        string Language = "";
        string extension = "";
        string languageHistory = "";
        public Form1()
        {
            // 实例一个Process类,启动一个独立进程
            //Process p = new Process();
            // 设定程序名
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

            //p1.StartInfo.FileName = "cmd.exe";
            //// 关闭Shell的使用
            //p1.StartInfo.UseShellExecute = false;
            //// 重定向标准输入
            //p1.StartInfo.RedirectStandardInput = true;
            //// 重定向标准输出
            //p1.StartInfo.RedirectStandardOutput = true;
            ////重定向错误输出
            //p1.StartInfo.RedirectStandardError = true;
            //// 设置不显示窗口
            //p1.StartInfo.CreateNoWindow = true;
            InitializeComponent();

            // Create the ToolTip and associate with the Form container. 
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip. 
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active. 
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox. 
            toolTip1.SetToolTip(this.buttonAdbDevice, "Check devices");
            toolTip1.SetToolTip(this.buttonAdbRoot, "Root Authority");
            toolTip1.SetToolTip(this.buttonRB, "Reboot");
            toolTip1.SetToolTip(this.buttonFolder, "Choose Folder");
            toolTip1.SetToolTip(this.buttonChangeLanguage, "Change Now!");
            toolTip1.SetToolTip(this.buttonCaptureUS, "Capture English");
            toolTip1.SetToolTip(this.buttonSave, "Capture Target");
            toolTip1.SetToolTip(this.textBoxPicRootPath, "Choose Folder");
            toolTip1.SetToolTip(this.comboBoxLanguage, "Select Language");
            toolTip1.SetToolTip(this.textBoxPicName, "Picture name should clear and concise");

            string adbPath = @"C:\adbForACT";
            string currentPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            //MessageBox.Show(currentPath);
            if (Directory.Exists(adbPath) == false)
            {
                Directory.CreateDirectory(adbPath);
            }
            //if (File.Exists(@"C:\adbForACT\adb.exe") == false)
            //{
            //    File.Copy(currentPath + "adb.exe", adbPath + "\\adb.exe", true);
            //    //File.Copy(currentPath + "AdbWinApi.dll", adbPath + "\\AdbWinApi.dll", true);
            //    //File.Copy(currentPath + "AdbWinUsbApi.dll", adbPath + "\\AdbWinUsbApi.dll", true);
            //}
            //if (File.Exists(@"C:\adbForACT\gort_sdk-19.apk") == false)
            //{
            //    //copy the apktool files to C:\adbForACT 
            //    File.Copy(currentPath + "gort_sdk-19.apk", adbPath + "\\gort_sdk-19.apk", true);

            //}
            //if (File.Exists(@"C:\adbForACT\setflafla") == false)
            //{
            //    //copy the setflafla and setflafla.a files to C:\adbForACT 
            //    File.Copy(currentPath + "setflafla", adbPath + "\\setflafla", true);
            //    File.Copy(currentPath + "setflafla.a", adbPath + "\\setflafla.a", true);
            //}


            //p.Start();
            //List<string> cmds = new List<string>();
            //cmds.Add("cd C:\\adbForACT");
            //cmds.Add("adb devices");
            //cmds.Add("adb install  gort_sdk-19.apk");
            ////cmds.Add("pause");
            ////依次输入我们需要的命令行
            //foreach (string cmd in cmds)
            //{
            //    p.StandardInput.WriteLine(cmd);
            //    //listBoxLog.Items.Add(DateTime.Now.ToString() + " " + "input " + cmd + " in CMD ");
            //}

            Phone myPhone = new Phone();
            myPhone.InstallAPK();
            myPhone.SetStayOn();
            //MessageBox.Show(myPhone.IsLandscape().ToString());
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "Image Files (*.bmp,*.jpg,*.png,*.jpeg)|*.bmp;*.jpg;*.png;*.jpeg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                textBox1.Text = file;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog2.InitialDirectory = @"C:\";
            openFileDialog2.Filter = "Image Files (*.bmp,*.jpg,*.png,*.jpeg)|*.bmp;*.jpg;*.png;*.jpeg";
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog2.FileName;
                textBox2.Text = file;
            }
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ImageCodecInfo myImageCodecInfo;
            System.Drawing.Imaging.Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            myImageCodecInfo = GetEncoderInfo("image/jpeg");
            myEncoder = System.Drawing.Imaging.Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            // Save the bitmap as a JPEG file with quality level 25.
            myEncoderParameter = new EncoderParameter(myEncoder, 25L);
            myEncoderParameters.Param[0] = myEncoderParameter;



            saveFileDialog1.Filter = "Image Files (*.bmp,*.jpg,*.png,*.jpeg)|*.bmp;*.jpg;*.png;*.jpeg";
            saveFileDialog1.DefaultExt = "jpg";
            saveFileDialog1.AddExtension = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image im1 = Image.FromFile(textBox1.Text);
                Image im2 = Image.FromFile(textBox2.Text);
                int wide = im1.Width + im2.Width;
                int high = im1.Height;
                Bitmap bm = new Bitmap(wide, high);
                Graphics gr = Graphics.FromImage(bm);
                gr.DrawImage(im2, 0, 0);
                gr.DrawImage(im1, im1.Width, 0);

                Brush brush = new SolidBrush(Color.Black);
                Pen pen = new Pen(brush, 5);
                Point p1 = new Point();
                Point p2 = new Point();
                p1.X = p2.X = wide / 2;
                p1.Y = 0;
                p2.Y = high;
                gr.DrawLine(pen, p1, p2);

                gr.Dispose();
                string name = saveFileDialog1.FileName;
                //bm.Save(name, System.Drawing.Imaging.ImageFormat.Gif);
                bm.Save(name, myImageCodecInfo, myEncoderParameters);
                //bm.Save(name);
                MessageBox.Show("Generate Complete!" + name);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = folderBrowserDialog1.SelectedPath;
                listBox1.Items.Add(textBox4.Text);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox5.Text.Length.Equals(0))
            {
                String dirPath = textBox4.Text;
                DirectoryInfo CurrentDir = new DirectoryInfo(dirPath);
                try
                {
                    if (CurrentDir.GetDirectories().Count() != 0)
                    {
                        foreach (DirectoryInfo dir in CurrentDir.GetDirectories())
                        {
                            if (dir.ToString().Contains("_"))
                            {
                                listBox1.Items.Add(CurrentDir + @"\" + dir.ToString());
                            }
                            else
                            {
                                //nothing to do
                            }
                            
                        }
                    }
 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                //清空4个容器
                EnPic.Clear();
                EnPicOrig.Clear();
                anotherPic.Clear();
                anotherPicOrig.Clear();
                //listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                //Language = textBox5.Text.Substring(textBox5.Text.LastIndexOf("\\"), textBox5.Text.Length - textBox5.Text.LastIndexOf("\\") - 1);
                //Language = textBox5.Text.Substring(textBox5.Text.Length - 5, 5);
                FindFiles(textBox4.Text);
                FindFilesPair(textBox5.Text);
                //listBox1.Items.Add(EnPic.Count().ToString());
                //listBox1.Items.Add(anotherPic.Count().ToString());
                //foreach (string a in EnPic)
                //{
                //    listBox2.Items.Add(a);
                //}
                //foreach (string a in anotherPic)
                //{
                //    listBox3.Items.Add(a);
                //}
                label14.Text = EnPic.Count().ToString();
                label15.Text = anotherPic.Count.ToString();
                extension = anotherPicOrig[0].Substring(anotherPicOrig[0].LastIndexOf(".") + 1, anotherPicOrig[0].Length - anotherPicOrig[0].LastIndexOf(".") - 1);
                int dotIndex = anotherPicOrig[0].LastIndexOf(".");
                string temp = anotherPicOrig[0].Substring(0, anotherPicOrig[0].Length - extension.Length - 1);
                Language = temp.Substring(temp.Length - 5, 5);
                label5.Text = Language + " count: ";
                listBox1.TopIndex = listBox1.Items.Count - 1;
 
            }

        }
        public void CheckTwoFolderFiles(String path)
        {
            //清空4个容器
            EnPic.Clear();
            EnPicOrig.Clear();
            anotherPic.Clear();
            anotherPicOrig.Clear();
            //listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            //Language = textBox5.Text.Substring(textBox5.Text.LastIndexOf("\\"), textBox5.Text.Length - textBox5.Text.LastIndexOf("\\") - 1);
            //Language = textBox5.Text.Substring(textBox5.Text.Length - 5, 5);
            FindFiles(textBox4.Text);
            FindFilesPair(textBox5.Text);
            label14.Text = EnPic.Count().ToString();
            label15.Text = anotherPic.Count.ToString();
            extension = anotherPicOrig[0].Substring(anotherPicOrig[0].LastIndexOf(".") + 1, anotherPicOrig[0].Length - anotherPicOrig[0].LastIndexOf(".") - 1);
            int dotIndex = anotherPicOrig[0].LastIndexOf(".");
            string temp = anotherPicOrig[0].Substring(0, anotherPicOrig[0].Length - extension.Length - 1);
            Language = temp.Substring(temp.Length - 5, 5);
            label5.Text = Language + " count: ";
            listBox1.TopIndex = listBox1.Items.Count - 1;
        }
        public void FindFiles(string dirPath)
        {
            //在指定目录下及子目录下寻找文件，并打印
            DirectoryInfo CurrentDir = new DirectoryInfo(dirPath);
            try
            {
                if (CurrentDir.GetDirectories().Count() != 0)
                {
                    foreach (DirectoryInfo dir in CurrentDir.GetDirectories())
                    {
                        //查找子目录
                        //MessageBox.Show(CurrentDir.ToString());
                        listBox1.Items.Add(CurrentDir + @"\" + dir.ToString());
                        FindFiles(CurrentDir + @"\" + dir.ToString());
                        //foreach (FileInfo f in dir.GetFiles())
                        //{
                        //    //查找文件
                        //    string fileFull = f.FullName;
                        //    string fileName = f.Name;
                        //    string tempFile = fileName.Substring(0, fileName.LastIndexOf("_"));
                        //    string EnfileName = tempFile.Substring(0, tempFile.LastIndexOf("_"));
                        //    //listBox2.Items.Add(CurrentDir + @"\" + fileName);
                        //    listBox2.Items.Add(EnfileName);
                        //    EnPicOrig.Add(fileFull);
                        //    EnPic.Add(EnfileName);
                        //}

                    }

                }
                else
                {
                    foreach (FileInfo f in CurrentDir.GetFiles())
                    {
                        if (f.Name != "Thumbs.db")
                        {
                            //查找文件
                            string fileFull = f.FullName;
                            string fileName = f.Name;
                            string tempFile = fileName.Substring(0, fileName.LastIndexOf("_"));
                            string EnfileName = tempFile.Substring(0, tempFile.LastIndexOf("_"));
                            //listBox2.Items.Add(CurrentDir + @"\" + fileName);
                            listBox2.Items.Add(EnfileName);
                            EnPicOrig.Add(fileFull);
                            EnPic.Add(EnfileName);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        public void FindFilesPair(string dirPath)
        {
            //在指定目录下及子目录下寻找文件，并打印
            DirectoryInfo CurrentDir = new DirectoryInfo(dirPath);
            try
            {
                if (CurrentDir.GetDirectories().Count() != 0)
                {
                    foreach (DirectoryInfo dir in CurrentDir.GetDirectories())
                    {
                        //查找子目录
                        //MessageBox.Show(CurrentDir.ToString());
                        listBox1.Items.Add(CurrentDir + @"\" + dir.ToString());
                        FindFilesPair(CurrentDir + @"\" + dir.ToString());
                        //foreach (FileInfo f in dir.GetFiles())
                        //{
                        //    //查找文件
                        //    string filefull = f.FullName;
                        //    string fileName = f.Name;
                        //    string tempFile = fileName.Substring(0, fileName.LastIndexOf("_"));
                        //    string anotherfileName = tempFile.Substring(0, tempFile.LastIndexOf("_"));
                        //    //listBox3.Items.Add(CurrentDir + @"\" + fileName);
                        //    listBox3.Items.Add(anotherfileName);
                        //    anotherPicOrig.Add(filefull);
                        //    anotherPic.Add(anotherfileName);
                        //}

                    }
                }
                else
                {
                    foreach (FileInfo f in CurrentDir.GetFiles())
                    {
                        if (f.Name != "Thumbs.db")
                        {
                            //查找文件
                            string filefull = f.FullName;
                            string fileName = f.Name;
                            string tempFile = fileName.Substring(0, fileName.LastIndexOf("_"));
                            string anotherfileName = tempFile.Substring(0, tempFile.LastIndexOf("_"));
                            //listBox3.Items.Add(CurrentDir + @"\" + fileName);
                            listBox3.Items.Add(anotherfileName);
                            anotherPicOrig.Add(filefull);
                            anotherPic.Add(anotherfileName);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.RootFolder = Environment.SpecialFolder.Desktop;
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = folderBrowserDialog2.SelectedPath;
                listBox1.Items.Add(textBox5.Text);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            folderBrowserDialog3.RootFolder = Environment.SpecialFolder.Desktop;
            if (folderBrowserDialog3.ShowDialog() == DialogResult.OK)
            {
                textBox6.Text = folderBrowserDialog3.SelectedPath;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {


            //比较两个list，把相同的合并生成新图片
            try
            {
                System.Diagnostics.Process.Start(textBox6.Text);
                string picEn="";
                string picAn="";
                string picTempEn = "";
                string picTempEnfinal = "";
                string picTempEnfinal1 = "";
                string picTempAn = "";
                string picTempAnfinal = "";
                string picTempAnfinal1 = "";
                foreach(string filename in EnPic)
                {
                    if (anotherPic.Contains(filename))
                    {
                        for (int i = 0; i < EnPicOrig.Count(); i++)
                        {
                            int place = EnPicOrig[i].LastIndexOf("\\");
                            picTempEn = EnPicOrig[i].Substring(place + 1, EnPicOrig[i].Length - place-1);
                            picTempEnfinal = picTempEn.Substring(0, picTempEn.LastIndexOf("_"));
                            picTempEnfinal1 = picTempEnfinal.Substring(0, picTempEnfinal.LastIndexOf("_"));
                            if (picTempEnfinal1.Equals(filename))
                            {
                                picEn = EnPicOrig[i];
                            }
                        }
                        for (int i = 0; i < anotherPicOrig.Count(); i++)
                        {
                            int place1 = anotherPicOrig[i].LastIndexOf("\\");
                            picTempAn = anotherPicOrig[i].Substring(place1 + 1, anotherPicOrig[i].Length - place1 - 1);
                            picTempAnfinal = picTempAn.Substring(0, picTempAn.LastIndexOf("_"));
                            picTempAnfinal1 = picTempAnfinal.Substring(0, picTempAnfinal.LastIndexOf("_"));
                            if (picTempAnfinal1.Equals(filename))
                            {
                                picAn = anotherPicOrig[i];
                            }
                        }
                        if (!picAn.Equals("") && !picEn.Equals(""))
                        {
                            MergePics(picEn, picAn, filename);
                        }
                    }
                    else
                    {
                        //do nothing
                    }
                    picAn = "";
                    picEn = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            listBox1.Items.Add("Conbine complete!");
            listBox1.TopIndex = listBox1.Items.Count - 1;
            //MessageBox.Show("Merge Complete!","Complete");
            FormFinish myFinish = new FormFinish();
            if (EnPic.Count() < anotherPic.Count)
            {
                myFinish.label3.Text = EnPic.Count.ToString();
            }
            else
            {
                myFinish.label3.Text = anotherPic.Count.ToString();
            }
            myFinish.Show();

        }
        public void MergePics(string pic1,string pic2,string picName)
        {

            ImageCodecInfo myImageCodecInfo;
            System.Drawing.Imaging.Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            myImageCodecInfo = GetEncoderInfo("image/jpeg");
            myEncoder = System.Drawing.Imaging.Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            // Save the bitmap as a JPEG file with quality level 25.
            myEncoderParameter = new EncoderParameter(myEncoder, 25L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            //为了得到需要创建的forder，创建全路径，需要用pic2的路径 减去 textBox5 的路径
            string delatPath = pic2.Remove(0, textBox5.Text.ToString().Length);
            //用存放的路径加上 这个路径，就是最终的路径和名字
            Image im1 = Image.FromFile(pic2);
            Image im2 = Image.FromFile(pic1);
            int wide = im1.Width + im2.Width;
            int high = im1.Height;
            Bitmap bm = new Bitmap(wide, high);
            Graphics gr = Graphics.FromImage(bm);
            gr.DrawImage(im1, 0, 0);
            gr.DrawImage(im2, im1.Width, 0);
            Brush brush = new SolidBrush(Color.Black);
            Pen pen = new Pen(brush, 5);
            Point p1=new Point();
            Point p2=new Point();
            p1.X=p2.X=wide/2;
            p1.Y=0;
            p2.Y=high;
            gr.DrawLine(pen, p1, p2);
            gr.Dispose();
            //生成的文件全路径，包括文件名，取自原文件
            //string name = textBox6.Text +"\\"+ picName+"_"+Language + "."+extension;
            //create language folder or not
            string name = "";
            string languageCode="";
            if (checkBoxNF.Checked)
            {
                name = textBox6.Text + delatPath;
            }
            else
            {
                int endofLanguage = textBox5.Text.LastIndexOf("\\");
                languageCode=textBox5.Text.Substring(endofLanguage+1);
                name = textBox6.Text +"\\"+languageCode+ delatPath;
            }
            
            int endofFolder = name.LastIndexOf("\\");
            string folderPath = name.Substring(0, endofFolder);
            System.IO.Directory.CreateDirectory(folderPath);
            //bm.Save(name);
            bm.Save(name, myImageCodecInfo, myEncoderParameters);
            //listBox1.Items.Add(picName + " combine complete");
            //MessageBox.Show("Generate Complete!" + name);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("IEXPLORE.EXE", "http://10.147.34.82/");
            //FormFinish formFinish = new FormFinish();
            //formFinish.Show();
            //formFinish.label2.Text = "123";
        }

        private void listBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Brush myBrush = Brushes.Red;
            for (int i = 0; i < listBox3.Items.Count; i++)
            {
                if (listBox2.Items[e.Index].ToString().Equals(listBox3.Items[i]))
                {
                    myBrush = Brushes.Black;
                }
            }
            e.Graphics.DrawString(listBox2.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds, null);
            e.DrawFocusRectangle();
        }

        private void listBox3_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Brush myBrush = Brushes.Blue;
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                if (listBox3.Items[e.Index].ToString().Equals(listBox2.Items[i]))
                {
                    myBrush = Brushes.Black;
                }
            }
            e.Graphics.DrawString(listBox3.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds, null);
            e.DrawFocusRectangle();
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Brush myBrush = Brushes.Black;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.Items[e.Index].ToString().Equals("Conbine complete!"))
                {
                    myBrush = Brushes.Red;
                }
            }
            
            e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds,null);
            //e.Graphics.FillRectangle(myBrush, e.Bounds);
            e.DrawFocusRectangle();

        }

        private void buttonAdbDevice_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start(@"C:\Documents and Settings\xp003698\Local Settings\Application Data\BRAT\Components\Adb\R1A002\adb.exe");
            dataGridViewShowDevice.Rows.Clear();
            listBoxLog.Items.Add(DateTime.Now.ToString() + " " + "Devices Searching......");
            string myLanguage = CheckLanguage();
            p.Start();
            //Clear devices lists
            //listBox_Devices.Items.Clear();

            List<string> cmds = new List<string>();
            //通过设置Process可以达到类似于cmds的效果，但是没有时间debug了。你可以尝试
            //p.StartInfo.FileName = Settings.Default.ADB_path + "adb.exe";
            //p.StartInfo.Arguments = " devices";


            //cmds.Add("cd " + Settings.Default.ADB_path);

            //20101021
            //cmds.Add("cd " + "C:\\Documents and Settings\\xp003698\\Local Settings\\Application Data\\BRAT\\Components\\Adb\\R1A002");
            //cmds.Add("cd " + folderBrowserDialogAdb.SelectedPath);
            //MessageBox.Show(listBoxAdb.Text.ToString());
            //cmds.Add("cd " + textBoxAdb.Text);
            //20101021
            //cmds.Add("cd C:\\adbForACT");
            //cmds.Add("adb root");
            cmds.Add("adb devices");
            //cmds.Add("pause");
            //依次输入我们需要的命令行
            foreach (string cmd in cmds)
            {
                p.StandardInput.WriteLine(cmd);
                //listBoxLog.Items.Add(DateTime.Now.ToString() + " " + "input " + cmd + " in CMD ");
            }
            //得到输入命令后得到的返回值

            StringBuilder rstBuilder = new StringBuilder();
            p.StandardInput.Close();
            
            while (!p.StandardOutput.EndOfStream)
            {
                string stmp = p.StandardOutput.ReadLine();
                rstBuilder.Append(stmp + Environment.NewLine);

                if (stmp.IndexOf("\tdevice") != -1)
                {
                    //listBox_Devices.Items.Add(stmp.Substring(0, stmp.IndexOf("\t")));
                    listBoxLog.Items.Add(DateTime.Now.ToString() + " " + stmp.Substring(0, stmp.IndexOf("\t")));
                    dataGridViewShowDevice.Rows.Add(stmp.Substring(0, stmp.IndexOf("\t")),myLanguage);
                    listBoxLog.Items.Add(DateTime.Now.ToString() + " " + "Found device: " + stmp.Substring(0, stmp.IndexOf("\t")));
                }
            }
            listBoxLog.TopIndex = listBoxLog.Items.Count - 1;
            //languageHistory = labelLanguage.Text;
            
        }

        private void buttonAdbRoot_Click(object sender, EventArgs e)
        {
            List<string> cmds = new List<string>();
            p.Start();
            //cmds.Add("cd C:\\adbForACT");
            cmds.Add("adb root");
            foreach (string cmd in cmds)
            {
                p.StandardInput.WriteLine(cmd);
                //listBoxLog.Items.Add(DateTime.Now.ToString() + " " + " input " + cmd + " in CMD ");
            }
            listBoxLog.Items.Add(DateTime.Now.ToString() + " " + " obtained root access authority");
        }

        private void buttonReboot_Click(object sender, EventArgs e)
        {
           
        }

        private void buttonCheckLanguage_Click(object sender, EventArgs e)
        {
            //check the system language
            p.Start();
            List<string> lc = new List<string>();
            List<string> cmds = new List<string>();
            string languge = "";
            //cmds.Add("cd C:\\adbForACT");
            cmds.Add("adb shell getprop persist.sys.locale");
            //cmds.Add("pause");
            //依次输入我们需要的命令行
            foreach (string cmd in cmds)
            {
                p.StandardInput.WriteLine(cmd);
                //listBoxLog.Items.Add(DateTime.Now.ToString() + " " + "input " + cmd + " in CMD ");
            };
            //得到输入命令后得到的返回值

            StringBuilder rstBuilder = new StringBuilder();
            p.StandardInput.Close();
            while (!p.StandardOutput.EndOfStream)
            {
                string stmp = p.StandardOutput.ReadLine();
                //rstBuilder.Append(stmp + Environment.NewLine);
                if (stmp.Length.Equals(5))
                {
                    languge = stmp;
                }
            }
            labelLanguage.Text = language.Replace("-","_");
            //if (lc.Count.Equals(2))
            //{
            //    listBoxLog.Items.Add("System Language is : " + lc.ElementAt(0));
            //    listBoxLog.Items.Add("System Country is : " + lc.ElementAt(1));
            //    labelLanguage.Text = lc.ElementAt(0) + "_" + lc.ElementAt(1);
            //    language = lc.ElementAt(0);
            //    country = lc.ElementAt(1);
            //}
            //else
            //{
            //    listBoxLog.Items.Add("wait a moment, try again.");
            //}
        }
        public string CheckLanguage()
        {
            //check the system language
            string getLanguage="";
            p.Start();
            //List<string> lc = new List<string>();
            List<string> cmds = new List<string>();
            //cmds.Add("cd C:\\adbForACT");
            cmds.Add("adb shell getprop persist.sys.locale");
            //cmds.Add("pause");
            //依次输入我们需要的命令行
            foreach (string cmd in cmds)
            {
                p.StandardInput.WriteLine(cmd);
                //listBoxLog.Items.Add(DateTime.Now.ToString() + " " + "input " + cmd + " in CMD ");
            };
            //得到输入命令后得到的返回值

            StringBuilder rstBuilder = new StringBuilder();
            p.StandardInput.Close();
            while (!p.StandardOutput.EndOfStream)
            {
                string stmp = p.StandardOutput.ReadLine();
                //rstBuilder.Append(stmp + Environment.NewLine);
                if (stmp.Length.Equals(5))
                {
                    getLanguage=stmp;
                }
            }
            labelLanguage.Text = getLanguage.Replace("-","_");
            //if (lc.Count.Equals(2))
            //{
            //    listBoxLog.Items.Add("System Language is : " + lc.ElementAt(0));
            //    listBoxLog.Items.Add("System Country is : " + lc.ElementAt(1));
            //    labelLanguage.Text = lc.ElementAt(0) + "_" + lc.ElementAt(1);
            //    getLanguage=lc.ElementAt(0) + "_" + lc.ElementAt(1);
            //    language = lc.ElementAt(0);
            //    country = lc.ElementAt(1);
            //}
            //else
            //{
            //    listBoxLog.Items.Add("wait a moment, try again.");
            //}
            return getLanguage;
        }



        private void button10_Click(object sender, EventArgs e)
        {
            folderBrowserDialog4.RootFolder = Environment.SpecialFolder.Desktop;
            if (folderBrowserDialog4.ShowDialog() == DialogResult.OK)
            {
                textBoxPicRootPath.Text = folderBrowserDialog4.SelectedPath;
                //listBox1.Items.Add(textBox3.Text);
            }
        }

        private void buttonChangeLanguage_Click(object sender, EventArgs e)
        {
            int numChecked = 0;
            string nameChecked="";
            foreach (Control cl in this.groupBox7.Controls)
            {
                if (cl is CheckBox)
                {
                    CheckBox cb = (CheckBox)cl;
                    if (cb.Checked == true)
                    {
                        numChecked++;
                        nameChecked=cb.Text;
                    }
                }

            }

            if (comboBoxLanguage.SelectedItem == null&&numChecked==0)
            {
                MessageBox.Show("Please choose language code first!");
            }
            else
            {
                if (comboBoxLanguage.SelectedItem != null&&numChecked==0)
                {
                    p.Start();
                    List<string> cmds = new List<string>();
                    String changeLC = comboBoxLanguage.SelectedItem.ToString();
                    String changeLanguage = changeLC.Substring(0, 2);
                    String changeCountry = changeLC.Substring(3, 2);
                    //cmds.Add("cd C:\\adbForACT");
                    ////cmds.Add("adb root");
                    ////cmds.Add("adb shell setprop persist.sys.language en");
                    ////cmds.Add("adb shell setprop persist.sys.country US");
                    ////cmds.Add("adb shell am broadcast -a android.Intent.ACTION_LOCALE_CHANGED");
                    ////cmds.Add("adb shell setprop ctl.restart zygote");
                    //cmds.Add("adb shell setprop persist.sys.language " + changeLanguage + " ;setprop persist.sys.country " + changeCountry + " ; stop;sleep 1; start");
                    ////cmds.Add("adb shell setprop persist.sys.language " + changeLanguage + " ;setprop persist.sys.country " + changeCountry + " ; am broadcast -a android.intent.action.LOCALE_CHANGED");
                    ////cmds.Add("pause");
                    ////依次输入我们需要的命令行

                    cmds.Add("adb shell am startservice -a com.sony.semctools.ave.gort.REGISTER_FOR_PHONE_EVENTS --es client_id gort");
                    cmds.Add("adb shell am startservice -a com.sony.semctools.ave.gort.SET_LOCALE --es locale " + comboBoxLanguage.SelectedItem.ToString());
                    cmds.Add("adb shell am startservice -a com.sony.semctools.ave.gort.EXECUTE_TASKS");
                    foreach (string cmd in cmds)
                    {
                        p.StandardInput.WriteLine(cmd);
                        Thread.Sleep(1000);
                        //listBoxLog.Items.Add(DateTime.Now.ToString() + " " + "input " + cmd + " in CMD ");
                    };
                    buttonCheckLanguage_Click(sender, e);
                    buttonAdbDevice_Click(sender, e);
                    labelLanguage.Text = CheckLanguage();
                    listBoxLog.Items.Add(DateTime.Now.ToString() + " " + "change langage to " + labelLanguage.Text);
                    listBoxLog.TopIndex = listBoxLog.Items.Count - 1;
                    if (languageHistory.Contains("ar_") ||
                       languageHistory.Equals("iw_IL") ||
                       languageHistory.Equals("fa_IR"))
                    {
                        MessageBox.Show("Please click OK in settings language manually again!");
                    }
                    else
                    {
                        if (comboBoxLanguage.SelectedItem.ToString().Contains("ar_") ||
                        comboBoxLanguage.SelectedItem.ToString().Equals("iw_IL") ||
                        comboBoxLanguage.SelectedItem.ToString().Equals("fa_IR"))
                        {
                            MessageBox.Show("Please click confirm language in settings manually again!");
                        }
                    }
                    languageHistory = comboBoxLanguage.SelectedItem.ToString();
                }
                else
                {
                    if (comboBoxLanguage.SelectedItem == null&& numChecked > 1)
                    {
                        MessageBox.Show("Please choose only 1 kind of language code !");
                    }
                    else
                    {
                        p.Start();
                        List<string> cmds = new List<string>();
                        String changeLC = nameChecked;
                        String changeLanguage = changeLC.Substring(0, 2);
                        String changeCountry = changeLC.Substring(3, 2);
                        ////依次输入我们需要的命令行

                        cmds.Add("adb shell am startservice -a com.sony.semctools.ave.gort.REGISTER_FOR_PHONE_EVENTS --es client_id gort");
                        cmds.Add("adb shell am startservice -a com.sony.semctools.ave.gort.SET_LOCALE --es locale " + nameChecked);
                        cmds.Add("adb shell am startservice -a com.sony.semctools.ave.gort.EXECUTE_TASKS");
                        foreach (string cmd in cmds)
                        {
                            p.StandardInput.WriteLine(cmd);
                            Thread.Sleep(1000);
                            //listBoxLog.Items.Add(DateTime.Now.ToString() + " " + "input " + cmd + " in CMD ");
                        };
                        buttonCheckLanguage_Click(sender, e);
                        buttonAdbDevice_Click(sender, e);
                        listBoxLog.Items.Add(DateTime.Now.ToString() + " " + "change langage to " + labelLanguage.Text);
                        listBoxLog.TopIndex = listBoxLog.Items.Count - 1;
                        if (languageHistory.Contains("ar_") ||
                           languageHistory.Equals("iw_IL") ||
                           languageHistory.Equals("fa_IR"))
                        {
                            MessageBox.Show("Please click OK in settings language manually again!");
                        }
                        else
                        {
                            if (nameChecked.Contains("ar_") ||
                            nameChecked.Equals("iw_IL") ||
                            nameChecked.Equals("fa_IR"))
                            {
                                MessageBox.Show("Please click confirm language in settings manually again!");
                            }
                        }
                        languageHistory = nameChecked;
                    }
                }

                
            }
        }
        public void ChangeLangage(String language)
        {
            p.Start();
            List<string> cmds = new List<string>();
            //String changeLC = comboBoxLanguage.SelectedItem.ToString();
            //String changeLanguage = changeLC.Substring(0, 2);
            //String changeCountry = changeLC.Substring(3, 2);
            //cmds.Add("cd C:\\adbForACT");
            cmds.Add("adb shell am startservice -a com.sony.semctools.ave.gort.REGISTER_FOR_PHONE_EVENTS --es client_id gort");
            cmds.Add("adb shell am startservice -a com.sony.semctools.ave.gort.SET_LOCALE --es locale " + language);
            cmds.Add("adb shell am startservice -a com.sony.semctools.ave.gort.EXECUTE_TASKS");
            foreach (string cmd in cmds)
            {
                p.StandardInput.WriteLine(cmd);
                Thread.Sleep(1000);
                //listBoxLog.Items.Add(DateTime.Now.ToString() + " " + "input " + cmd + " in CMD ");
            };
            listBoxLog.Items.Add(DateTime.Now.ToString() + " " + "change langage to " + language);
 
        }

        //public void CaptureScreenshots(string language)
        //{
        //    p.Start();
        //    if (Directory.Exists(textBoxPicRootPath.Text + "\\" + language) == false&&!language.Equals("en_US"))
        //    {
        //        Directory.CreateDirectory(textBoxPicRootPath.Text + "\\" + language );
        //    }
        //    List<string> cmds = new List<string>();
        //    //cmds.Add("cd C:\\adbForACT");
        //    cmds.Add("adb shell /system/bin/screencap -p /sdcard/screenshot.png");
        //    if (language.Equals("en_US"))
        //    {
        //        if (File.Exists("C:/adbForACT/0.png"))
        //        {
        //            if (pictureBoxUS.Image != null)
        //            {
        //                //pictureBox2.Image = null;
        //                pictureBoxUS.Image.Dispose();
        //                //pictureBox2.Image.reRefresh();
                        
        //                //try
        //                //{
        //                //    File.Delete("C:/adbForACT/0.png");

        //                //}
        //                //catch (Exception e)
        //                //{
        //                //    MessageBox.Show(e.ToString());
        //                //}
        //            }


        //            //pictureBox2.Refresh();
        //        }
        //        cmds.Add("adb pull /sdcard/screenshot.png C:/adbForACT/0.png");
        //    }
        //    else
        //    {
        //        if (File.Exists("C:/adbForACT/1.png"))
        //        {
        //            if (pictureBoxConbine.Image != null)
        //            {
        //                pictureBoxConbine.Image.Dispose();
        //            }
 
        //        }

        //        cmds.Add("adb pull /sdcard/screenshot.png C:/adbForACT/1.png");
        //    }
            
            
        //    //cmds.Add("adb pull /sdcard/screenshot.png " + textBoxPicRootPath.Text + "\\" + language + "\\" + textBoxPicName.Text + "_" + language + ".png");
        //    //cmds.Add("pause");
        //    //依次输入我们需要的命令行
        //    foreach (string cmd in cmds)
        //    {
        //        p.StandardInput.WriteLine(cmd);
        //        //listBoxLog.Items.Add(DateTime.Now.ToString() + " " + "input " + cmd + " in CMD ");
        //    };
        //    Thread.Sleep(3000);
        //    listBoxLog.Items.Add("Capture "+ language+" complete!"); 
        //}

        //private void buttonSave_Click(object sender, EventArgs e)
        //{
        //    if (textBoxPicName.Text.ToString().Equals(""))
        //    {
        //        MessageBox.Show("Please input picture name ");
        //    }
        //    else
        //    {
        //        if (textBoxPicRootPath.Text.ToString().Equals(""))
        //        {
        //            MessageBox.Show("Please input picture path first");
        //        }
        //        else
        //        {
        //            if(labelLanguage.Text.Equals("en_US"))
        //            {
        //                MessageBox.Show("Please change system language first");
        //            }
        //            else
        //            {
        //                CaptureScreenshots(labelLanguage.Text);
        //                Thread.Sleep(3000);
        //                listBoxLog.TopIndex = listBoxLog.Items.Count - 1;
        //                string picName = CombinePic();
        //                Thread.Sleep(1000);
        //                pictureBoxConbine.Image = Image.FromFile(picName);
        //            }
        //        }
        //    }
        //}

        //private void buttonCombine_Click(object sender, EventArgs e)
        //{
        //    ImageCodecInfo myImageCodecInfo;
        //    System.Drawing.Imaging.Encoder myEncoder;
        //    EncoderParameter myEncoderParameter;
        //    EncoderParameters myEncoderParameters;
        //    myImageCodecInfo = GetEncoderInfo("image/jpeg");
        //    myEncoder = System.Drawing.Imaging.Encoder.Quality;
        //    myEncoderParameters = new EncoderParameters(1);
        //    // Save the bitmap as a JPEG file with quality level 25.
        //    myEncoderParameter = new EncoderParameter(myEncoder, 25L);
        //    myEncoderParameters.Param[0] = myEncoderParameter;

        //    Image im1 = Image.FromFile("C:/adbForACT/1.png");
        //    Image im2 = Image.FromFile("C:/adbForACT/0.png");
        //    int wide = im1.Width + im2.Width;
        //    int high = im1.Height;
        //    Bitmap bm = new Bitmap(wide, high);
        //    Graphics gr = Graphics.FromImage(bm);
        //    gr.DrawImage(im1, 0, 0);
        //    gr.DrawImage(im2, im1.Width, 0);
        //    Brush brush = new SolidBrush(Color.Black);
        //    Pen pen = new Pen(brush, 5);
        //    Point p1 = new Point();
        //    Point p2 = new Point();
        //    p1.X = p2.X = wide / 2;
        //    p1.Y = 0;
        //    p2.Y = high;
        //    gr.DrawLine(pen, p1, p2);
        //    gr.Dispose();

        //    string name = textBoxPicRootPath.Text + "\\" + labelLanguage.Text.ToString() + "\\" + textBoxPicName.Text + ".png";
        //    bm.Save(name, myImageCodecInfo, myEncoderParameters);
        //    //listBox1.Items.Add(picName + " combine complete");
        //    //MessageBox.Show("Generate Complete!" + name);
        //    listBoxLog.Items.Add(name + " combine complete");
        //    im1.Dispose();
        //    im2.Dispose();
        //    listBoxLog.TopIndex = listBoxLog.Items.Count - 1;
        //    pictureBoxConbine.Image = Image.FromFile(name);
        //}
        public string CombinePic()
        {
            ImageCodecInfo myImageCodecInfo;
            System.Drawing.Imaging.Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            myImageCodecInfo = GetEncoderInfo("image/jpeg");
            myEncoder = System.Drawing.Imaging.Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            // Save the bitmap as a JPEG file with quality level 25.
            myEncoderParameter = new EncoderParameter(myEncoder, 25L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            Image im1 = Image.FromFile("C:/adbForACT/1.png");
            Image im2 = Image.FromFile("C:/adbForACT/0.png");
            Phone myPhone = new Phone();
            if (myPhone.IsLandscape() == true)
            {
                im1.RotateFlip(RotateFlipType.Rotate270FlipNone);
                im2.RotateFlip(RotateFlipType.Rotate270FlipNone);
                int wide = im1.Width;
                int high = im1.Height+im2.Height;
                Bitmap bm = new Bitmap(wide, high);
                Graphics gr = Graphics.FromImage(bm);
                gr.DrawImage(im1, 0, 0);
                gr.DrawImage(im2, 0, im1.Height);
                Brush brush = new SolidBrush(Color.Black);
                Pen pen = new Pen(brush, 5);
                Point p1 = new Point();
                Point p2 = new Point();
                p1.X = 0;
                p2.X = wide;
                p1.Y = p2.Y=high/2;
                gr.DrawLine(pen, p1, p2);
                gr.Dispose();
                string name = textBoxPicRootPath.Text + "\\" + labelLanguage.Text.ToString() + "\\" + textBoxPicName.Text + ".png";
                bm.Save(name, myImageCodecInfo, myEncoderParameters);
                listBoxLog.Items.Add(name + " combine complete");
                listBoxLog.TopIndex = listBoxLog.Items.Count - 1;
                im1.Dispose();
                im2.Dispose();
                return name;
            }
            else
            {
                int wide = im1.Width + im2.Width;
                int high = im1.Height;
                Bitmap bm = new Bitmap(wide, high);
                Graphics gr = Graphics.FromImage(bm);
                gr.DrawImage(im1, 0, 0);
                gr.DrawImage(im2, im1.Width, 0);
                Brush brush = new SolidBrush(Color.Black);
                Pen pen = new Pen(brush, 5);
                Point p1 = new Point();
                Point p2 = new Point();
                p1.X = p2.X = wide / 2;
                p1.Y = 0;
                p2.Y = high;
                gr.DrawLine(pen, p1, p2);
                gr.Dispose();
                string name = textBoxPicRootPath.Text + "\\" + labelLanguage.Text.ToString() + "\\" + textBoxPicName.Text + ".png";
                bm.Save(name, myImageCodecInfo, myEncoderParameters);
                listBoxLog.Items.Add(name + " combine complete");
                listBoxLog.TopIndex = listBoxLog.Items.Count - 1;
                im1.Dispose();
                im2.Dispose();
                return name;
            }

 
        }

        //private void button11_Click(object sender, EventArgs e)
        //{
        //    if (textBoxPicName.Text.ToString().Equals(""))
        //    {
        //        MessageBox.Show("Please input picture name first");
        //    }
        //    else
        //    {
        //        if (textBoxPicRootPath.Text.ToString().Equals(""))
        //        {
        //            MessageBox.Show("Please select picture store path first");
        //        }
        //        else
        //        {
        //            if (labelLanguage.Text.Equals("en_US"))
        //            {
        //                CaptureScreenshots(labelLanguage.Text);
        //                Thread.Sleep(3000);
        //                Image image = Image.FromFile("C:/adbForACT/0.png");
        //                Phone myPhone = new Phone();
        //                if (myPhone.IsLandscape() == true)
        //                {
        //                    image.RotateFlip(RotateFlipType.Rotate270FlipNone);
        //                }
        //                pictureBoxUS.Image = image;

        //                listBoxLog.TopIndex = listBoxLog.Items.Count - 1;
        //            }
        //            else
        //            {
        //                MessageBox.Show("Please change system language to en_US first!");
        //            }
                    
        //        }   
        //    }
        //}

        private void comboBoxLanguage_DropDown(object sender, EventArgs e)
        {
            //MessageBox.Show("comboBox opened!");
            if (comboBoxLanguage.SelectedItem.ToString().Contains("ar_") ||
                comboBoxLanguage.SelectedItem.ToString().Equals("iw_IL") ||
                comboBoxLanguage.SelectedItem.ToString().Equals("fa_IR"))
            {
                MessageBox.Show("Please click change then choose language manually again!");
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D && e.Alt)
            {
                buttonAdbDevice.PerformClick();
            }
            if (e.KeyCode == Keys.F && e.Alt)
            {
                buttonFolder.PerformClick();
            }
            if (e.KeyCode == Keys.D1 && e.Alt)
            {
                buttonCaptureUS.PerformClick();
            }
            if (e.KeyCode == Keys.D2 && e.Alt)
            {
                buttonSave.PerformClick();
            }
            if (e.KeyCode == Keys.E && e.Alt)
            {
                buttonErase.PerformClick();
            }
            if (e.KeyCode == Keys.C && e.Alt)
            {
                buttonChangeLanguage.PerformClick();
            }
        }

        private void buttonErase_Click(object sender, EventArgs e)
        {
            textBoxPicName.Clear();
        }

        private void mailToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:Chris.Zhao@sonymobile.com?subject=Feedback for LV Capture Tool");
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonRB_Click(object sender, EventArgs e)
        {
            List<string> cmds = new List<string>();
            p.Start();
            //cmds.Add("cd C:\\adbForACT");
            cmds.Add("adb reboot ");
            //MessageBox.Show(file, "choosed files");
            foreach (string cmd in cmds)
            {
                p.StandardInput.WriteLine(cmd);
            }
            listBoxLog.Items.Add(DateTime.Now.ToString() + " Reboot phone ......");
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            string logName = "log" + DateTime.Now.ToString("yyMMddhhmm")+".txt";
            ////int count =this.groupBox7.Controls.Count;
            ////MessageBox.Show(count.ToString());
            //List<string> langs = new List<string>();
            //foreach (Control cl in this.groupBox7.Controls)
            //{
            //    CheckBox cb = (CheckBox)cl;

            //    if (cb.Checked)
            //    {
            //        langs.Add(cb.Text);
            //    }
            //}

            ////MessageBox.Show(cb.Text);
            //List<string> cmds = new List<string>();
            //foreach (string a in langs)
            //{
            //    //cmds.Add("cd C:\\");
            //    cmds.Add("adb shell am startservice -a com.sony.semctools.ave.gort.REGISTER_FOR_PHONE_EVENTS --es client_id gort");
            //    cmds.Add("adb shell am startservice -a com.sony.semctools.ave.gort.SET_LOCALE --es locale " + a);
            //    cmds.Add("adb shell am startservice -a com.sony.semctools.ave.gort.EXECUTE_TASKS");
            //    cmds.Add("ping -n 6 127.0.0.1>nul");
            //    //cmds.Add("adb shell uiautomator runtest AutoRunner.jar -c sumire.features.SystemDateTimenew >>log.txt");
            //    cmds.Add(textBoxCommand.Text);
            //    cmds.Add("ping -n 6 127.0.0.1>nul");
            //}
            //p.Start();
            //foreach (string cmd in cmds)
            //{
            //    p.StandardInput.WriteLine(cmd);
            //}
            //p.Close();
            List<string> langs = new List<string>();
            string cmds = this.textBoxCommand.Text;
            string[] cmdArray = cmds.Replace("\r\n", "@").Split('@');
            string filePath = "C:\\adbForACT\\b.bat";
            FileStream fs = new FileStream(filePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            foreach (Control cl in this.groupBox7.Controls)
            {
                if (cl is CheckBox)
                { 
                    CheckBox cb = (CheckBox)cl;

                    if (cb.Checked)
                    {
                        langs.Add(cb.Text);
                    }
                }
            }
            foreach (string a in langs)
            {
                sw.WriteLine("adb shell am startservice -a com.sony.semctools.ave.gort.REGISTER_FOR_PHONE_EVENTS --es client_id gort");
                sw.WriteLine("adb shell am startservice -a com.sony.semctools.ave.gort.SET_LOCALE --es locale " + a );
                sw.WriteLine("adb shell am startservice -a com.sony.semctools.ave.gort.EXECUTE_TASKS");
                sw.WriteLine("ping -n 6 127.0.0.1>nul");
                //sw.WriteLine("adb shell pm clear com.sonymobile.android.addoncamera.supervideo");
                //sw.WriteLine("adb shell pm clear com.sonymobile.android.addoncamera.facefusion");
                //for (int i = 0; i < textBoxCommand.Items.Count; i++)
                //{
                //    sw.WriteLine(textBoxCommand.Items[i] + ">>" + logName);
                //}

                if (radioButton1.Checked == true)
                {
                    foreach (string cmd in cmdArray)
                    {
                        sw.WriteLine(cmd + ">>" + logName);
                    }
                }
                else
                {
                    foreach (string cmd in cmdArray)
                    {
                        sw.WriteLine(cmd);
                    }
                }
                
                sw.WriteLine("ping -n 6 127.0.0.1>nul");
            }
            sw.WriteLine("pause");
            sw.Flush();
            sw.Close();
            fs.Close();


            Process process = new Process();
            process.StartInfo.FileName = filePath;
            process.StartInfo.UseShellExecute = true;

            ////这里相当于传参数 
            //process.StartInfo.Arguments = "hello world";
            process.Start();

        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            List<string> langs = new List<string>();
            foreach (Control cl in this.groupBox7.Controls)
            {
                CheckBox cb = (CheckBox)cl;

                if (cb.Checked)
                {
                    langs.Add(cb.Text);
                }
            }

            //MessageBox.Show(cb.Text);
            List<string> cmds = new List<string>();
            foreach (string a in langs)
            {
                //cmds.Add("cd C:\\");
                cmds.Add("adb shell am startservice -a com.sony.semctools.ave.gort.REGISTER_FOR_PHONE_EVENTS --es client_id gort");
                cmds.Add("adb shell am startservice -a com.sony.semctools.ave.gort.SET_LOCALE --es locale " + a);
                cmds.Add("adb shell am startservice -a com.sony.semctools.ave.gort.EXECUTE_TASKS");
                cmds.Add("ping -n 6 127.0.0.1>nul");
                cmds.Add("adb shell uiautomator runtest CosmosL.jar -c sumire.features.Users>>loguser.txt");
                cmds.Add("ping -n 10 127.0.0.1>nul");
                cmds.Add("adb shell uiautomator runtest CosmosL.jar -c sumire.features.Usersuser>>loguser.txt");
                cmds.Add("ping -n 10 127.0.0.1>nul");
                cmds.Add("adb shell uiautomator runtest CosmosL.jar -c sumire.features.UsersGuest>>loguser.txt");
                cmds.Add("ping -n 10 127.0.0.1>nul");
            }
            p.Start();
            foreach (string cmd in cmds)
            {
                p.StandardInput.WriteLine(cmd);
            }
            p.Close();
        }

        private void button10_Click_2(object sender, EventArgs e)
        {
            foreach (Control cl in this.groupBox7.Controls)
            {
                if (cl is CheckBox)
                {
                    CheckBox cb = (CheckBox)cl;
                    cb.Checked = false;
                }
                
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            foreach (Control cl in this.groupBox7.Controls)
            {
                if (cl is CheckBox)
                {
                    CheckBox cb = (CheckBox)cl;
                    cb.Checked = true;
                }
                
            }
        }

        private void buttonLocal_Click(object sender, EventArgs e)
        {
            folderBrowserDialogLocal.RootFolder = Environment.SpecialFolder.Desktop;
            if (folderBrowserDialogLocal.ShowDialog() == DialogResult.OK)
            {
                textBoxLocal.Text = folderBrowserDialogLocal.SelectedPath;
            }
        }

        private void buttonXML_Click(object sender, EventArgs e)
        {
            folderBrowserDialogXML.SelectedPath = @"\\SEMCW27980\MenuTree";
            if (folderBrowserDialogXML.ShowDialog() == DialogResult.OK)
            {
                textBoxXML.Text = folderBrowserDialogXML.SelectedPath;
            }
        }

        private void buttonCompare_Click(object sender, EventArgs e)
        {
            List<String> listLocal = new List<string>();
            List<String> listServer = new List<string>();
            if (textBoxLocal.Text == "" || textBoxXML.Text == "" )
            {
                MessageBox.Show("Please select compare path first! ", "Warning");
            }
            else
            {
                listLocal.Clear();
                listServer.Clear();
                listBoxLocal.Items.Clear();
                listBoxServer.Items.Clear();
                //get all image name without language code
                DirectoryInfo CurrentDir = new DirectoryInfo(textBoxLocal.Text);
                foreach (FileInfo f in CurrentDir.GetFiles())
                {
                    if (f.Name != "Thumbs.db")
                    {
                        string imageName = f.Name.Substring(0, f.Name.Length - 10);
                        listLocal.Add(imageName);
                    }
                }
                FileInfo fileInfo = new FileInfo(this.folderBrowserDialogXML.SelectedPath + "\\data.xml");
                if (fileInfo.Exists == false)
                {
                    MessageBox.Show("Data File Error ! xml file not exist! ", "Warning");
                }
                else
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(fileInfo.ToString());
                    XmlNodeList nodeList = xmlDocument.GetElementsByTagName("Item");
                    foreach (XmlNode node in nodeList)
                    {
                        listServer.Add(node.Attributes["Name"].Value);
                    }
                }
                List<String> listCompare = listServer.Except(listLocal).ToList();
                labelMiss.Text = "Missing: " + listCompare.Count;
                foreach (string i in listCompare)
                {
                    listBoxLocal.Items.Add(i);
                }
                listCompare.Clear();
                listCompare = listLocal.Except(listServer).ToList();
                labelMore.Text = "Superfluous: " + listCompare.Count;
                foreach (string i in listCompare)
                {
                    listBoxServer.Items.Add(i);
                }
            }
        }
    }
}
