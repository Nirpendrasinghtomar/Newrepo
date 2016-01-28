using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        string seriousloc = @"C:\Users\meme\serious";
            string funnyloc =@"C:\Users\meme\funny";

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstfile.Items.Clear();
            DirectoryInfo maindir = new DirectoryInfo(@"C:\Users\meme");
           List<String> FileList = new List<string>();
           if (listBox1.SelectedIndex == 0)
           {
               foreach (var dir in maindir.GetDirectories())
               {
                   foreach (var file in dir.GetFiles())
                   {
                   lstfile.Items.Add(file.Name);
                   }
               }


           }
        
           
           if (listBox1.SelectedIndex == 1)
           {
               DirectoryInfo funnydir = new DirectoryInfo(funnyloc);
               foreach (var funfile in funnydir.GetFiles())
               {
                   lstfile.Items.Add(funfile.Name);

               }
           }
           else
           {
               DirectoryInfo seriousdir = new DirectoryInfo(seriousloc);
               foreach (var serfile in seriousdir.GetFiles())
               {
                   lstfile.Items.Add(serfile.Name);

               }
           }
            }

        private void button1_Click(object sender, EventArgs e)
        {
            string memUrl = textBox1.Text;
            string saveLoc = " ";
            if (listBox2.SelectedIndex == 0)
            {
                 saveLoc = funnyloc+@"\"+DateTime.Now.Millisecond+".jpg";
            }
            else
            {
                saveLoc = seriousloc + @"\" + DateTime.Now.Millisecond + ".jpg";
            }
            byte[] imageBytes;
            HttpWebRequest imageRequest = (HttpWebRequest)WebRequest.Create(memUrl);
            WebResponse imageResponse = imageRequest.GetResponse();


            Stream responseStream = imageResponse.GetResponseStream();

            using (BinaryReader br = new BinaryReader(responseStream))
            {
                imageBytes = br.ReadBytes(50000000);
                br.Close();
            }
            responseStream.Close();
            imageResponse.Close();

            FileStream fs = new FileStream(saveLoc, FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter bw = new BinaryWriter(fs);
            try
            {
                bw.Write(imageBytes);
            }
            finally
            {
                fs.Close();
                bw.Close();
            }

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                label1.Text = "Meme Catalog";
                pictureBox1.Image = null;

                string selectedfile = " ";
                if (lstfile.FocusedItem.Focused == true)
                {
                    selectedfile = lstfile.FocusedItem.Text;
                }
                else
                {
                    label1.Text = "please select any image and click Show selected button";
                }

                string selefileloc = " ";
                if (listBox1.SelectedItem == "Funny")
                {
                    selefileloc = funnyloc + @"\" + selectedfile;
                }
                if (listBox1.SelectedItem == "serious")
                {
                    selefileloc = seriousloc + @"\" + selectedfile;
                }
                else 
                {
                    selefileloc=seriousloc + @"\" + selectedfile;
                    
                    if (!File.Exists(selefileloc.ToString()))
                    {
                        selefileloc = funnyloc + @"\" + selectedfile;
                    }
                }
                FileStream stream = new FileStream(selefileloc, FileMode.Open, FileAccess.Read);
                pictureBox1.Image = Image.FromStream(stream);
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                stream.Close();
            }
            catch {
                label1.Text = "An error has occured. Please contact developer.";
            }
        }
        
        }


        
    }

