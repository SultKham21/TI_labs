﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Threading;
using System.IO;

namespace Rsa_link
{
    public partial class Form1 : Form
    {


        Rsa_encrypter rsa_enc = new Rsa_encrypter();
        bool KeysExist = false;

        private void ShowKeys()
        {
            textBoxP.Text = rsa_enc.P.ToString();
            textBoxQ.Text = rsa_enc.Q.ToString();
            textBoxN.Text = rsa_enc.N.ToString();
            textBoxD.Text = rsa_enc.D.ToString();
            textBoxE.Text = rsa_enc.E.ToString();
    
        }

        private string GetSignPath(string path)
        {
            string s =path;

            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] == '.')
                {
                    s = s.Substring(0, i);

                }


            }
            return s + "-Sign.sgn";

        }

        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("info.dat"))
            {
                FileStream f = new FileStream("info.dat", FileMode.Open, FileAccess.Read);
                byte[] tmp = new byte[4048];
                int ByteRead = f.Read(tmp, 0, tmp.Length);

                string s = Encoding.ASCII.GetString(tmp, 0, ByteRead);

                string[] params1 = s.Split('_');
                if (params1.Length>=4)
                    numericUpDown1.Value = params1[0].Length * 2;

                rsa_enc.GenerateKeys(params1);
                ShowKeys();
                KeysExist = true;
                f.Close();
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            rsa_enc.GenerateKeys((int)numericUpDown1.Value);
            KeysExist = true;
            ShowKeys();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.CheckFileExists)
                {
                    TestedFile.Text = openFileDialog1.FileName;
                    SignPath.Text = GetSignPath(openFileDialog1.FileName) ;

                }


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!KeysExist)
            {
                label7.Text = "please, generate key";
                return;
            }
            if (TestedFile.Text == "")
            {
                label7.Text = "file not found!";
                return;
            }


            label7.Text = "Creating...";
            if (!rsa_enc.CreateSign(TestedFile.Text, SignPath.Text))
            {
                MessageBox.Show("Error!");
            }

            label7.Text = "Created.";

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {                               
                    SignPath.Text = openFileDialog1.FileName;          
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!KeysExist)
            {
                label7.Text = "Generate for  encrypt";
                return;
            }
            if (TestedFile.Text == "")
            {
                label7.Text = "File not found";
                return;
            }
            if (rsa_enc.CheckSign(TestedFile.Text, SignPath.Text))
            {
                MessageBox.Show("Sign right");
                label7.Text = "Sign right";
            }
            else
            {
                MessageBox.Show("Sign error");
                label7.Text = "Sign error";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (KeysExist )
            {
                FileStream f = new FileStream("info.dat", FileMode.Create, FileAccess.Write);
                string s = rsa_enc.P.ToString() + "_" + rsa_enc.Q.ToString()+ "_" + rsa_enc.E+"_"+rsa_enc.D;
                byte[] tmp = Encoding.ASCII.GetBytes(s);
                f.Write(tmp,0,tmp.Length);
                f.Close();
            }
            

            

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            
            if (f.ShowDialog() == DialogResult.OK)
            {
                if (f.DText != "")
                {
                    rsa_enc.GenerateKeys(new string[]{ f.PText, f.QText,f.EText,f.DText});

                }
                else
                {
                    rsa_enc.GenerateKeys(f.PText,f.QText);

                }
                ShowKeys();
                KeysExist = true;

            }


        }

        private void SignPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}