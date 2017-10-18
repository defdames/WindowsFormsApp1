using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //  Just to kill WINWORD.EXE if it is running
                //killprocess("winword");
                //  copy letter format to temp.doc
                File.Copy("C:\\POs\\POTemplate.docx", "c:\\POs\\"+txtPoNumber.Text+".docx", true);
                //  create missing object
                object missing = Missing.Value;
                //  create Word application object
                Word.Application wordApp = new Word.Application();
                //  create Word document object
                Word.Document aDoc = null;
                //  create & define filename object with temp.doc
                object filename = "c:\\POs\\"+txtPoNumber.Text+".docx";
                //  if temp.doc available
                if (File.Exists((string)filename))
                {
                    object readOnly = false;
                    object isVisible = false;
                    //  make visible Word application
                    wordApp.Visible = false;
                    //  open Word document named temp.doc
                    aDoc = wordApp.Documents.Open(ref filename, ref missing,
                    ref readOnly, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref isVisible, ref missing, ref missing,
                    ref missing, ref missing);
                    aDoc.Activate();
                    //  Call FindAndReplace()function for each change
                    FindAndReplace(wordApp, "[PONumber]", txtPoNumber.Text);
                    FindAndReplace(wordApp, "[Company]", txtCompany.Text.Trim());
                    
                    FindAndReplace(wordApp, "[Addr1]", txtAddress1.Text.Trim());
                    FindAndReplace(wordApp, "[Addr2]", txtAddress2.Text.Trim());
                    FindAndReplace(wordApp, "[City]", txtCity.Text.Trim());
                    FindAndReplace(wordApp, "[State]", txtState.Text.Trim());
                    FindAndReplace(wordApp, "[DATE]", dateTimePicker.Text.Trim());
                    if (string.IsNullOrWhiteSpace(txtZip.Text))
                    {
                        FindAndReplace(wordApp, "[Zip]", "");
                    }
                    else
                    {
                        FindAndReplace(wordApp, "[Zip]", ", " + txtZip.Text.Trim());
                    }
                    
                    FindAndReplace(wordApp, "[1Q]", txtQty1.Text.Trim());
                    FindAndReplace(wordApp, "[2Q]", txtQty2.Text.Trim());
                    FindAndReplace(wordApp, "[3Q]", txtQty3.Text.Trim());
                    FindAndReplace(wordApp, "[1DESC]", txtDesc1.Text.Trim());
                    FindAndReplace(wordApp, "[2DESC]", txtDesc2.Text.Trim());
                    FindAndReplace(wordApp, "[3DESC]", txtDesc3.Text.Trim());
                    FindAndReplace(wordApp, "[Comments]", txtComments.Text.Trim());



                    //  save temp.doc after modified
                    aDoc.Save();
                    wordApp.Quit();
                    //Open Document
                    Word.Application ap = new Word.Application();
                    Document document = ap.Documents.Open(@"c:\\POs\\"+txtPoNumber.Text+".docx");
                    ap.Visible = true;

                    //Clear Text Boxes
                    txtPoNumber.Text = "";
                    txtCompany.Text = "";
                    txtAddress1.Text = "";
                    txtAddress2.Text = "";
                    txtCity.Text = "";
                    txtState.Text = "";
                    txtZip.Text = "";
                    txtQty1.Text ="";
                    txtQty2.Text = "";
                    txtQty3.Text = "";
                    txtDesc1.Text = "";
                    txtDesc2.Text = "";
                    txtDesc3.Text = "";
                    txtComments.Text = "";

                }
                else

                
                MessageBox.Show("File does not exist.",
            "No File", MessageBoxButtons.OK,
            MessageBoxIcon.Information);
                //killprocess("winword");
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(txtPoNumber.Text))
                {
                    MessageBox.Show("Please Enter PO Number", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Error in process.", "Internal Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void FindAndReplace(Word.Application wordApp, 
			object findText, object replaceText)
{   
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;
            wordApp.Selection.Find.Execute(ref findText, ref matchCase,
                ref matchWholeWord, ref matchWildCards, ref matchSoundsLike,
                ref matchAllWordForms, ref forward, ref wrap, ref format,
                ref replaceText, ref replace, ref matchKashida, 
						ref matchDiacritics,
                ref matchAlefHamza, ref matchControl);
}

        private void txtDesc3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
