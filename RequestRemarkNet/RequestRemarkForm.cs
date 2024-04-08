using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RequestRemarkNet
{
    public partial class RequestRemarkForm : Form
    {
        private const int MAX_FREE_TEXT = 4000;

        public int ExitCode;
        public string RemarkText;
        public string OperID;
        public string OperName;
        public string RemarkStat;
        public string SdgName;

        private string strNewRemark;


        //holds the OLD remarks separated to the important and regula ones:
        private string SavedRemarkDesc;
        private string SavedImportantRemarkDesc;

        private bool unhandeledText, updateButtonPressed;
        //saves the new remarks written on this opening of the form:

        public RequestRemarkForm()
        {
            InitializeComponent();
        
        }
        //todo implement
        public void InitializeRemark(string RemarkDesc, string sdgName)
        {
            int iBufferIndex = -1;
            //if(sdgName == null) {
            //    TxtFreeTextTemplate.Text = "";
            //    txtImportantTextTemplate.Text = "";
            //    return;
            //}
            if (!string.IsNullOrEmpty(RemarkDesc))
            {
                iBufferIndex = RemarkDesc.IndexOf('@');

            }
            if (iBufferIndex == -1)
            {
                SavedRemarkDesc = RemarkDesc;
                SavedImportantRemarkDesc = "";
            }
            else
            {
                if (iBufferIndex != 0)
                {
                    SavedRemarkDesc = RemarkDesc.Substring(0, iBufferIndex);
                }
                else
                {
                    SavedRemarkDesc = "";
                }

                try
                {
                    if (RemarkDesc.Length - iBufferIndex > 0)
                    {
                        SavedImportantRemarkDesc = RemarkDesc.Substring(iBufferIndex + 1, RemarkDesc.Length - iBufferIndex-1);
                    }
                    else
                    {
                        SavedImportantRemarkDesc = "";
                    }

                }
                catch (Exception e)
                {
                    SavedImportantRemarkDesc = "";
                }

            }

            TxtFreeTextTemplate.Text = SavedRemarkDesc;
            txtImportantTextTemplate.Text = SavedImportantRemarkDesc;
            //TxtFreeTextTemplate.Rtf = SavedRemarkDesc;
            //txtImportantTextTemplate.Rtf = SavedImportantRemarkDesc;
        }
        public void InitializeOperatorID(string OperatorID)
        {
            OperID = OperatorID;
        }

        public void InitializeOperatorName(string OperatorName)
        {
            OperName = OperatorName;
        }

        public void InitializeRemarkStat(string Remark_Status)
        {
            RemarkStat = Remark_Status;
        }

        public void InitializeSdgName(string Sdg_Name)
        {
            SdgName = Sdg_Name;
        }

        public void ReadOnly(bool ReadOnlyFlag)
        {
            if (ReadOnlyFlag == true)
            {
                //TxtFreeTextToUpdate.Enabled = false;
                //CmdClear.Enabled =false;
                //CmdComplete.Enabled =false;
                //CmdSave.Enabled = false;
                //CmdUpdate.Enabled = false;
            }

        }


        private void CmdClear_Click(object sender, EventArgs e)
        {
            clearRegularRemark();
        }
        //used when:
        //1. choosing to clean the new written remarks
        //2. moving the remarks to the IMPORTANT part
        private void clearRegularRemark()
        {
            //TxtFreeTextTemplate.Rtf = "{\\rtf1\\fbidis\\ansi\\ansicpg1255\\deff0\\deflang1037\\rtldoc" +
            //            "{\\fonttbl{\\f0\\fswiss\\fcharset177{\\*\\fname Arial;}Arial (Hebrew);}}" +
            //            "\\viewkind4\\uc1\\pard\\rtlpar\\qr\\f0\\rtlch\\fs24 \\par}";

            TxtFreeTextTemplate.Text = SavedRemarkDesc;
            //strNewRemark = "";
            updateButtonPressed = false;
        }

        private void CmdClose_Click(object sender, EventArgs e)
        {
            ExitCode = 0;
            RemarkText = TxtFreeTextTemplate.Text + "@" + txtImportantTextTemplate.Text;
            RequestRemarkForm_FormClosed();
        }

        private void Form_Unload(int Cancel)
        {
            if ((unhandeledText || updateButtonPressed) && ExitCode != 999)
            {
                DialogResult dialogResult = MessageBox.Show("טקסט לא נשמר. האם לצאת בכל זאת?", "הערות לדרישה", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else if (dialogResult == DialogResult.No)
                {
                    // Cancel = true;
                }
            }
        }

        private void TxtFreeTextToUpdate_Change(object sender, EventArgs e)
        {
            if (TxtFreeTextToUpdate.Text != "")
            {
                unhandeledText = true;
            }
            else
            {
                unhandeledText = false;
            }
        }

        private void CmdComplete_Click(object sender, EventArgs e)
        {
            string TmpFreeText, TmpStr;
            if (string.IsNullOrEmpty(TxtFreeTextTemplate.Text) && string.IsNullOrEmpty(txtImportantTextTemplate.Text))
            {
                MessageBox.Show("לא נכתבו הערות");
                return;
            }
            if (RemarkStat == "P")
            {
                TmpStr = " :שוחרר על-ידי המשתמש (" + OperID + " - " + OperName + ") :בתאריך " + DateTime.Now;
                TmpFreeText = TxtFreeTextTemplate.Text + "\n" + TmpStr + "\n";
                TxtFreeTextTemplate.Text = TmpFreeText;
            }
            updateButtonPressed = false;
            ExitCode = 2;
            if (txtImportantTextTemplate.Text != "")
            {
                ExitCode = 3;
            }
            RemarkText = TxtFreeTextTemplate.Text + "@" + txtImportantTextTemplate.Text;
            RequestRemarkForm_FormClosed();

        }

        private void cmdImportant_Click(object sender, EventArgs e)
        {
            //no remarks written to pass to the important area:
            if (string.IsNullOrEmpty(strNewRemark))
            {
                MessageBox.Show("לא נכתבו הערות");
                return;
            }
            UpdateImportantRemark(strNewRemark);
            clearRegularRemark();

            ExitCode = 3;

            //'if there are remarks already - show the RED color
            //'else - the 1st remark is IMPORTANT and the BLUE color will show
            if (TxtFreeTextTemplate.Text != "")
            {
                if (RemarkStat == "P")
                {
                    ExitCode = 1;
                }
            }

            strNewRemark = "";
            updateButtonPressed = false;
            RemarkText = TxtFreeTextTemplate.Text + "@" + txtImportantTextTemplate.Text;
            RequestRemarkForm_FormClosed();

        }

        private void UpdateImportantRemark(string RemarkStr)
        {
            string TmpFreeText, TmpStr;
            if (RemarkStr.Trim() == "")
            {
                return;
            }
            if (txtImportantTextTemplate.Text.Length + RemarkStr.Length > MAX_FREE_TEXT)
            {
                MessageBox.Show("קלט שגוי, מקסימום 4000 תווים להערה");
            }
            TmpStr = " :עודכן על-ידי המשתמש (" + OperID + " - " + OperName + ") :בתאריך " + System.DateTime.Now;
            TmpFreeText = txtImportantTextTemplate.Text + "\n" + RemarkStr;
            //txtImportantTextTemplate.Rtf = "{\\rtf1\\fbidis\\ansi\\ansicpg1255\\deff0\\deflang1037\\rtldoc" +
            //            "{\\fonttbl{\\f0\\fswiss\\fcharset177{\\*\\fname Arial;}Arial (Hebrew);}}" +
            //            "\\viewkind4\\uc1\\pard\\rtlpar\\qr\\f0\\rtlch\\fs24 \\par}";
            txtImportantTextTemplate.Text = TmpFreeText;
        }

        public void CmdSave_Click(object sender, EventArgs e)
        {
            string TmpFreeText;
            string TmpStr;
            if (string.IsNullOrEmpty(TxtFreeTextTemplate.Text) && string.IsNullOrEmpty(txtImportantTextTemplate.Text))
            {
                MessageBox.Show("לא נכתבו הערות");
                return;
            }
            if (RemarkStat == "C")
            {
                TmpStr = " :עוכב על-ידי )המשתמש (" + OperID + " - " + OperName + ") :בתאריך " + DateTime.Now;
                TmpFreeText = TxtFreeTextTemplate.Text + "\n" + TmpStr + "\n";
                TxtFreeTextTemplate.Text = TmpFreeText;
            }

            updateButtonPressed = false;
            ExitCode = 1;
            RemarkText = TxtFreeTextTemplate.Text + "@" + txtImportantTextTemplate.Text;
            RequestRemarkForm_FormClosed();


        }

        public void AddRemark(string RemarkStr)
        {
            UpdateRemark(RemarkStr);
            RemarkText = TxtFreeTextTemplate.Text;
        }

        private void UpdateRemark(string RemarkStr)
        {
            string TmpFreeText;
            string TmpStr;
            if (!string.IsNullOrEmpty(RemarkStr))
            {
                if (TxtFreeTextTemplate.Text.Length + RemarkStr.Length > MAX_FREE_TEXT)
                {
                    MessageBox.Show("קלט שגוי, מקסימום 4000 תווים להערה");
                }


                //TmpStr = ":עודכן על-ידי המשתמש" + getOperatorName() + " - " + " :בתאריך " + System.DateTime.Now;
                TmpStr = " :עודכן על-ידי המשתמש (" + OperID + " - " + OperName + ") :בתאריך " + System.DateTime.Now;


                TmpFreeText = TxtFreeTextTemplate.Text + "\n" + RemarkStr + "\n" + TmpStr + "\n";
                strNewRemark += "\n" + RemarkStr + "\n" + TmpStr + "\n";
                //TxtFreeTextTemplate.Rtf = "{\\rtf1\\fbidis\\ansi\\ansicpg1255\\deff0\\deflang1037\\rtldoc" +
                //            "{\\fonttbl{\\f0\\fswiss\\fcharset177{\\*\\fname Arial;}Arial (Hebrew);}}" +
                //            "\\viewkind4\\uc1\\pard\\rtlpar\\qr\f0\\rtlch\\fs24 \\par}";
                TxtFreeTextTemplate.Text = TmpFreeText;
            }
            RemarkStr = "";


            updateButtonPressed = true;
        }

        //Continue Here -----------------------------------

        //private void CmdUpdate_Click()
        //{
        //    UpdateRemark(TxtFreeTextToUpdate.Text);
        //    TxtFreeTextToUpdate.Text = "";
        //}

        private void CmdUpdate_Click(object sender, EventArgs e)
        {
            UpdateRemark(TxtFreeTextToUpdate.Text);
            TxtFreeTextToUpdate.Text = "";
        }

        //private void Form_Load()
        //{
        //    ExitCode = 0;
        //    strNewRemark = "";
        //    RemarkText = "";
        //    //Me.Caption = "הערות לדרישה " + SdgName;
        //    TxtFreeTextToUpdate.Rtf = "{\\rtf1\\fbidis\\ansi\\ansicpg1255\\deff0\\deflang1037\\rtldoc" +
        //                    "{\\fonttbl{\\f0\\fswiss\\fcharset177{\\*\\fname Arial;}Arial (Hebrew);}}" +
        //                    "\\viewkind4\\uc1\\pard\\rtlpar\\qr\f0\\rtlch\\fs24 \\par}";
        //    if (TxtFreeTextTemplate.Text.Trim() == "")
        //    {
        //        TxtFreeTextTemplate.Rtf = "{\\rtf1\\fbidis\\ansi\\ansicpg1255\\deff0\\deflang1037\\rtldoc" +
        //                    "{\\fonttbl{\\f0\\fswiss\\fcharset177{\\*\\fname Arial;}Arial (Hebrew);}}" +
        //                    "\\viewkind4\\uc1\\pard\\rtlpar\\qr\f0\\rtlch\\fs24 \\par}";
        //    }
        //    txtImportantTextTemplate.Rtf = "{\\rtf1\\fbidis\\ansi\\ansicpg1255\\deff0\\deflang1037\\rtldoc" +
        //                    "{\\fonttbl{\\f0\\fswiss\\fcharset177{\\*\\fname Arial;}Arial (Hebrew);}}" +
        //                    "\\viewkind4\\uc1\\pard\\rtlpar\\qr\f0\\rtlch\\fs24 \\par}";

        //}

        private void TxtFreeTextTemplate_Click(object sender, EventArgs e)
        {
            //todo set language to hebrew
        }


        private void RequestRemarkForm_Load(object sender, EventArgs e)
        {
            ExitCode = 0;
            strNewRemark = "";
            RemarkText = "";
            //TxtFreeTextToUpdate.Rtf = "{\\rtf1\\fbidis\\ansi\\ansicpg1255\\deff0\\deflang1037\\rtldoc" +
            //                "{\\fonttbl{\\f0\\fswiss\\fcharset177{\\*\\fname Arial;}Arial (Hebrew);}}" +
            //                "\\viewkind4\\uc1\\pard\\rtlpar\\qr\f0\\rtlch\\fs24 \\par}";
            //if (string.IsNullOrEmpty(TxtFreeTextTemplate.Text))
            //{
            //    TxtFreeTextTemplate.Rtf = "{\\rtf1\\fbidis\\ansi\\ansicpg1255\\deff0\\deflang1037\\rtldoc" +
            //                "{\\fonttbl{\\f0\\fswiss\\fcharset177{\\*\\fname Arial;}Arial (Hebrew);}}" +
            //                "\\viewkind4\\uc1\\pard\\rtlpar\\qr\f0\\rtlch\\fs24 \\par}";
            //}
            //txtImportantTextTemplate.Rtf = "{\\rtf1\\fbidis\\ansi\\ansicpg1255\\deff0\\deflang1037\\rtldoc" +
            //                "{\\fonttbl{\\f0\\fswiss\\fcharset177{\\*\\fname Arial;}Arial (Hebrew);}}" +
            //                "\\viewkind4\\uc1\\pard\\rtlpar\\qr\f0\\rtlch\\fs24 \\par}";
        }




        private void RequestRemarkForm_FormClosed()
        {
            bool flag = true;
            if ((unhandeledText || updateButtonPressed) && ExitCode != 999)
            {
                flag = false;
                DialogResult dialogResult = MessageBox.Show("טקסט לא נשמר. האם לצאת בכל זאת?", "הערות לדרישה", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                   // this.Hide();

                    this.Close();
                }
                else if (dialogResult == DialogResult.No)
                {
                }
            }
            if (flag)
            {
                this.clearRegularRemark();
                
                this.Close();
            }
        }



    }
}
