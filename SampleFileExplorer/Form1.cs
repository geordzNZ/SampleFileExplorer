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
        private string filePath = @"C:\SampleFolder";
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
            string selectedFile = "";
            FileAttributes fileAttrib;
            try
            {
                if (isFile)
                {
                    selectedFile = $@"{filePath}\{currentlySelectedItemName}";
                    FileInfo fileDetails = new FileInfo(selectedFile);

                    lblFileName.Text = fileDetails.Name;
                    lblFileType.Text = fileDetails.Extension;
                    lblCreatedDate.Text = fileDetails.CreationTime.ToString();
                    lblModifiedDate.Text = fileDetails.LastWriteTime.ToString();
                    fileAttrib = File.GetAttributes(selectedFile);
                }
                else
                {
                    fileAttrib = File.GetAttributes(filePath);
                    fileList = new DirectoryInfo(filePath);
                    FileInfo[] files = fileList.GetFiles();  //GETS ALL THE FILES
                    DirectoryInfo[] dirs = fileList.GetDirectories();  // GETS ALL THE DIRECTORIES

                    lstvDisplay.Items.Clear();
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        lstvDisplay.Items.Add(dirs[i].Name, 0);
                    }

                    for (int i = 0; i < files.Length; i++)
                    {
                        int iconID = 7;
                        switch (files[i].Extension.ToLower())
                        {
                            case ".doc":
                            case ".docx":
                            case ".docm":
                                iconID = 1;
                                break;
                            case ".gif":
                            case ".jpg":
                            case ".png":
                                iconID = 2;
                                break;
                            case ".pdf":
                                iconID = 3;
                                break;
                            case ".txt":
                                iconID = 4;
                                break;
                            case ".csv":
                            case ".xls":
                            case ".xlsx":
                                iconID = 5;
                                break;
                            case ".zip":
                                iconID = 6;
                                break;
                        }
                        lstvDisplay.Items.Add(files[i].Name, iconID);
                    }
                }
            }
            catch (Exception e)
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
            loadButtonAction();
        }

        public void removeTrailingSlash()
        {
            string path = txtFilePath.Text;
            if(path.LastIndexOf(@"\") == path.Length-1)
            {
                txtFilePath.Text = path.Substring(0,path.Length-1);
            }
        }

        public void goBackAction()
        {
            try 
            {
                removeTrailingSlash();
                string path = txtFilePath.Text;
                path = path.Substring(0, path.LastIndexOf(@"\"));
                this.isFile = false;
                txtFilePath.Text = path;
                removeTrailingSlash();
            }
            catch (Exception e)
            {

            }

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            goBackAction();
            loadButtonAction();
        }

        private void lstvDisplay_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            currentlySelectedItemName = e.Item.Text;

            FileAttributes fileAttrib = File.GetAttributes($@"{filePath}\{currentlySelectedItemName}");
            if ((fileAttrib & FileAttributes.Directory) == FileAttributes.Directory)
            {
                isFile = false;
                txtFilePath.Text = $@"{filePath}\{currentlySelectedItemName}";
            } else
            {
                isFile = true;
            }
        }

        private void lstvDisplay_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            loadButtonAction();
        }
    }
}
