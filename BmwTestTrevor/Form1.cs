using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BmwTestTrevor
{
    public partial class Form1 : Form
    {


        /// <summary>
        /// declare source path and detination patth variables 
        /// </summary>
        string SourcePath = "";
        string DestinationPath = "";


        public Form1()
        {
            InitializeComponent();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();

            folder.ShowDialog();
            String path = folder.SelectedPath;
            SourceTextBox.Text = path;
            SourcePath = path;


        }

        private void button2_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog folder = new FolderBrowserDialog();

            folder.ShowDialog();
            String path = folder.SelectedPath;
            DestinationTextBox.Text = path;
            DestinationPath = path;


        }



        private void ReplicateButton_Click_1(object sender, EventArgs e)
        {

            StringBuilder sb = new StringBuilder();

            System.IO.DirectoryInfo dir1 = new System.IO.DirectoryInfo(SourcePath);
            System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(DestinationPath);

            

            IEnumerable<System.IO.FileInfo> list1 = dir1.GetFiles("*.*",
            System.IO.SearchOption.AllDirectories);

            IEnumerable<System.IO.FileInfo> list2 = dir2.GetFiles("*.*",
            System.IO.SearchOption.AllDirectories);





            bool IsInDestination = false;
            bool AppearsInDestinationButNotSource = false;
           

            //First condition : File not present in destination 

            foreach (System.IO.FileInfo a in list1)
            {
               

                foreach (System.IO.FileInfo s2 in list2)
                {
                    if (a.Name == s2.Name)
                    {
                        IsInDestination = true;
                        break;
                    }
                    else
                    {
                        IsInDestination = false;
                    }
                }

                if (IsInDestination == false)
                {
                    System.IO.File.Copy(a.FullName, System.IO.Path.Combine(DestinationPath, a.Name), true);
                    sb.AppendLine (  "copied the following file destination folder because it did not exist :" + dir2 + " filename" + a.Name);

                }
            }


                ///second condition 
                ///If a files is different in size or date and time then it should be replaced.
                /////

                foreach (System.IO.FileInfo b in list1)
                {


                    foreach (System.IO.FileInfo s2 in list2)
                    {
                        if (b.Name == s2.Name && ((b.CreationTime != s2.CreationTime) || (b.Length != s2.Length)))
                        {
                            System.IO.File.Copy(b.FullName, System.IO.Path.Combine(DestinationPath, b.Name), true);
                            sb.AppendLine("overwrote the following files of different size and timestamp to destination folder :" + dir2 + " filename" + b.Name);
                    }

                    }
                }

                ///third condition
                ///If a file exists in the destination but not the source it must be deleted from the destination.


                foreach (System.IO.FileInfo dest in list2)
                {


                    foreach (System.IO.FileInfo srce in list1)
                    {
                    if (dest.Name == srce.Name)
                    {

                        AppearsInDestinationButNotSource = false;
                        break;
                  

                    }

                    else {
                        AppearsInDestinationButNotSource = true;
                    }

                    }


                if (AppearsInDestinationButNotSource == true)

                {

                    string v = dest.FullName;
                   

                    System.IO.File.Delete(v);
                    sb.AppendLine("Deleted the following files that existed in destination but not source :" + dir2 + " filename" + v);
                }

                }
            

            ///replicate folders 

            CustomSearcher cs = new CustomSearcher();
            
            var sourcedir = CustomSearcher.GetDirectories(SourcePath);

            var destedir = CustomSearcher.GetDirectories(DestinationPath);



            foreach (string v in sourcedir)



            {

                if (destedir.Count > 0 )
                {

                    foreach (string b in destedir)

                    {


                        string sc = v.Replace(dir1.ToString(), "**");

                        string ds = b.Replace(dir2.ToString(), "**");

                        if (sc != ds)

                        {
                            Directory.CreateDirectory(sc.Replace("**", dir2.ToString()));
                            sb.AppendLine("created the following directries that did not exist in destination :" + sc.Replace("**", dir2.ToString()));

                        }
                    }
                }
                else {

                    foreach (string q in sourcedir) {


                        string sc = q.Replace(dir1.ToString(), "**");

                     


                        Directory.CreateDirectory(sc.Replace("**", dir2.ToString()));

                        sb.AppendLine("created the following directries that did not exist in destination :" + sc.Replace("**", dir2.ToString()));

                    }

                }

                }



            using (StreamWriter writer = new StreamWriter(@"c:\resultsTrevorExercise.txt"))
            {
                writer.WriteLine(sb);
       
              
            }
            label4.ForeColor = Color.Red;
           
            label4.Text = "COMPLETE !";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start(@"c:\resultsTrevorExercise.txt");
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}



                    
        







  














    
                    
                
            
        





