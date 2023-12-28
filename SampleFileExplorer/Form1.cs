using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleFileExplorer
{
    public partial class Form1 : Form
    {
        private string filePath = "D:";
        private bool isFile = false;
        private string currentlySelectedItemName = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtFilePath.Text = filePath;
            loadFilesAndDirectories();
        }

        public void loadFilesAndDirectories() 
        {
            DirectoryInfo fileList;
            try
            {
                fileList = new DirectoryInfo(filePath);
                FileInfo[] files = fileList.GetFiles();  //GETS ALL THE FILES
                DirectoryInfo[] dirs = fileList.GetDirectories();  // GETS ALL THE DIRECTORIES

                for (int i = 0; i < dirs.Length; i++)
                {
                    lstvDisplay.Items.Add(files[i].Name);
                }

                for (int i = 0; i < files.Length; i++)
                {
                    lstvDisplay.Items.Add(files[i].Name);
                }

            }
            catch(Exception e)
            {
            
            }
        }

        public void loadButtonAction()
        {
            filePath = txtFilePath.Text;
            loadFilesAndDirectories();
            isFile = false;
        }


        private void btnGo_Click(object sender, EventArgs e)
        {

        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            loadButtonAction();
        }

        private void lstvDisplay_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            currentlySelectedItemName = e.Item.Text;
        }
    }
}
