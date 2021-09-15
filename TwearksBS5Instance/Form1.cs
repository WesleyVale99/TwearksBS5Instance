using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwearksBS5Instance.Classes;

namespace TwearksBS5Instance
{
    public partial class Form1 : Form
    {
        public int countBreak_1 = -1;
        public int countBreak_2 = -1;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.ForeColor = Color.Blue;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (countBreak_1 > -1 && countBreak_2 > -1)
            {
                File.WriteAllLines(@"C:\ProgramData\BlueStacks_nxt\bluestacks.conf", DataFiles.bluestacksConfig);
                UpdateTokenValue(countBreak_1, countBreak_2);
                MessageBox.Show("Operação realizada com sucesso.");
            }
            else
            {
                MessageBox.Show("Operação não realizada.");
            }
        }
        public bool LoadConfig()
        {
            try
            {
                string Path_1 = @"C:\ProgramData\BlueStacks_nxt\bluestacks.conf";
                string Path_2 = @"C:\ProgramData\BlueStacks_nxt\Engine\UserData\InputMapper\UserFiles\" + toolStripComboBox1.SelectedItem;
                if (File.Exists(Path_1) && File.Exists(Path_2))
                {
                    DataFiles.bluestacksConfig = File.ReadAllLines(Path_1);
                    // UpdateTokenValue()

                    //verificação do json
                    DataFiles.freefirethMapper = JsonConvert.DeserializeObject<ImputMapper>(File.ReadAllText(Path_2));

                    foreach (var ControlSchemes in DataFiles.freefirethMapper.ControlSchemes)
                    {
                        countBreak_1 += 1;
                        if (!ControlSchemes.BuiltIn)
                            break;
                    }
                    foreach (var GameControls in DataFiles.freefirethMapper.ControlSchemes[countBreak_1].GameControls)
                    {
                        countBreak_2 += 1;
                        if (GameControls.Tweaks > 0)
                            break;
                    }
                    textBox1.Text = DataFiles.freefirethMapper.ControlSchemes[countBreak_1].GameControls[countBreak_2].Tweaks.ToString();
                    textBox2.Text = DataFiles.freefirethMapper.ControlSchemes[countBreak_1].GameControls[countBreak_2].ExclusiveDelay.ToString();
                    textBox3.Text = DataFiles.bluestacksConfig[41].Split('"')[1];
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LoadConfig())
            {
                GroupEnable();
                toolStripComboBox1.Enabled = false;
            }
            else
            {
                MessageBox.Show("Erro ao localizar os arquivos.");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            DataFiles.bluestacksConfig[41] = "bst.instance.Nougat32.dpi=\"" + textBox3.Text + "\"";
        }
        public byte[] CreateArray(ImputMapper imput)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, imput);
               return  ms.ToArray();
            }
        }
        public void GroupEnable()
        {
            label4.Enabled = true;
            label2.Enabled = true;
            label3.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            button1.Enabled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.instagram.com/_wslyvale/");
            Process.Start("https://www.tiktok.com/@wesley7350?lang=pt-BR");
        }
        public void UpdateTokenValue(int count1, int count2)
        {
            string Path_2 = @"C:\ProgramData\BlueStacks_nxt\Engine\UserData\InputMapper\UserFiles\" + toolStripComboBox1.SelectedItem;
            dynamic jsonObj = JsonConvert.DeserializeObject(File.ReadAllText(Path_2));
            jsonObj["ControlSchemes"][count1]["GameControls"][count2]["Tweaks"] = int.Parse(textBox1.Text);
            jsonObj["ControlSchemes"][count1]["GameControls"][count2]["ExclusiveDelay"] = int.Parse(textBox2.Text);
            string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText(Path_2, output);
        }
    }
}
