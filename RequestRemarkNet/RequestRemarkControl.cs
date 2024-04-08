using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ADODB;

using LSSERVICEPROVIDERLib;
using LSExtensionWindowLib;
using Oracle.ManagedDataAccess.Client;
using Telerik.WinControls.Primitives;

namespace RequestRemarkNet
{
    public partial class RequestRemarkControl : UserControl
    {
        private const string BUTTON_FACE = "#F0F0F0";
        private const string GREEN = "&HFF00&";
        private const string RED = "&HFF&";
        private const string BLUE = "&HFF8080";
        private const string PURPLE = "&HC000C0";
        //need to check what type it is
        private string importantColor;

        private const string COMPLETED = "C";
        private const string NON_COMPLETED = "P";
        private const string IMPORTANT = "I";

        private INautilusDBConnection _ntlsCon;
        private IExtensionWindowSite2 _ntlsSite;
        private INautilusUser _ntlsUser;

        private OracleConnection Connection;
        private OracleCommand cmd;
        OracleDataReader reader;
        private string _connectionString;
        private double sessionId;
        private double operatorID;

        //public ADODB.Connection Connection;
        public string SdgName;
        public string OperatorID;
        public string OperatorName;

        //private ADODB.Recordset RequestRemark;
        private string RequestRemarkDesc;
        private string RemarkStatus;



        RequestRemarkForm FrmRequestRemark;
        public event Action<string> StatusChanged;

        public RequestRemarkControl()
        {
            InitializeComponent();
            FrmRequestRemark = new RequestRemarkForm();
        }


        public void AddRemark(string RemarkToAdd)
        {
            try
            {
                //ADODB.Recordset rst;
                string sql, TmpStr;

                //check if new remark
                sql = " select  1 from lims_sys.u_request_remark r where r.name = '" + SdgName + "'";
                //execute that sql
                //rst = Connection.Execute(sql);
                cmd = new OracleCommand(sql, Connection);
                var reader = cmd.ExecuteScalar();

                if (reader != null /*rst.EOF*/)
                {
                    FrmRequestRemark = new RequestRemarkForm();
                    FrmRequestRemark.InitializeRemark(RequestRemarkDesc, SdgName);
                    FrmRequestRemark.AddRemark(RemarkToAdd);
                    RequestRemarkDesc = FrmRequestRemark.RemarkText;
                    UpdateRemark("C");
                    FrmRequestRemark.ExitCode = 999;
                }
                else
                {
                    //if remark exists, enter manualy
                    TmpStr = "עוכב על-ידי המשתמש: (" + operatorID + " - " + OperatorName + ") בתאריך: " + DateTime.Now;

                    //TmpStr = "עוכב על-ידי המשתמש: " + _ntlsUser.GetOperatorName() +
                    //    OperatorID + " - " + OperatorName + " " +
                    //    "בתאריך: " + DateTime.Now;
                    RemarkToAdd = RemarkToAdd.Replace('@', '#');
                    //RemarkToAdd = Replace(RemarkToAdd, "@", "#");

                    sql = " update lims_sys.u_request_remark r ";
                    sql = sql + " set r.DESCRIPTION= substr(r.DESCRIPTION,1,instr(r.DESCRIPTION,'@')-1)";
                    sql = sql + "|| '\n\n" + RemarkToAdd + "\n'";
                    sql = sql + "|| '" + TmpStr + "\n\n'";
                    sql = sql + "|| substr(r.DESCRIPTION,instr(r.DESCRIPTION,'@')) ";
                    sql = sql + " where r.name = '" + SdgName + "'";

                    cmd = new OracleCommand(sql, Connection);
                    //Connection.Execute(sql);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in RequestRemark.ocx - AddRemark: " + ex.Message);
            }
        }

        public void AddSuspendedRemark(string RemarkToAdd)
        {
            try
            {
                AddRemark(RemarkToAdd);
                LockRemark();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in RequestRemark.ocx - AddRemark: " + ex.Message);
            }
        }

        public void AddNormalRemark(string RemarkToAdd)
        {
            try
            {
                FrmRequestRemark = new RequestRemarkForm();
                FrmRequestRemark.ExitCode = 2;

                FrmRequestRemark.InitializeRemark(RequestRemarkDesc, SdgName);
                FrmRequestRemark.AddRemark(RemarkToAdd);
                RemarkStatus = COMPLETED;
                FrmRequestRemark.RemarkStat = "C";
                FrmRequestRemark.CmdSave_Click(new object(), new EventArgs());
                RequestRemarkDesc = FrmRequestRemark.RemarkText;
                UpdateRemark(RemarkStatus);
                UpdateRemarkStatus(RemarkStatus);
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR is: " + e.Message);
            }

        }


        public void LockRemark()
        {
            UpdateRemarkStatus(NON_COMPLETED);
        }

        public void UnLockRemark()
        {
            UpdateRemarkStatus(COMPLETED);
        }

        public void InitializeConnection()
        {
            Connection = GetConnection();
        }

        public void GetsdgName(ref string Sdg_Name)
        {
            SdgName = Sdg_Name;
            FrmRequestRemark.InitializeSdgName(SdgName);
        }
        

        public void GetOperatorID(string Operator_ID)
        {
            //ADODB.Recordset OperatorRST;
            OperatorID = Operator_ID;

            string sql = "select FULL_NAME from lims_sys.operator " +
                                        "where operator_id = '" + OperatorID + "'";

            cmd = new OracleCommand(sql, Connection);
            var reader = cmd.ExecuteScalar();
            OperatorName = reader.ToString();

            //OperatorRST = Connection.Execute("select U_HEBREW_NAME from lims_sys.operator_user " +
            //                            "where operator_id = '" + OperatorID + "'");
            //OperatorName = OperatorRST("U_HEBREW_NAME");
            FrmRequestRemark.InitializeOperatorID(OperatorID);
            FrmRequestRemark.InitializeOperatorName(OperatorName);
        }

        public void setOperatorIdAndName()
        {
            operatorID = _ntlsUser.GetOperatorId();
            OperatorID = operatorID.ToString();
            string name = string.Empty;
            string sql = "select FULL_NAME from lims_sys.operator " +
                            "where operator_id = '" + operatorID + "'";

            using (cmd = new OracleCommand(sql, Connection))
            {
                OracleDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    name = Convert.ToString(reader[0]);
                }

                if (string.IsNullOrEmpty(name))
                {
                    sql = "select NAME from lims_sys.operator " +
                        "where operator_id = '" + operatorID + "'";
                    cmd.CommandText = sql;

                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        name = Convert.ToString(reader[0]);


                    }
                }

                FrmRequestRemark.InitializeOperatorID(operatorID.ToString());
                FrmRequestRemark.InitializeOperatorName(name);

                reader.Close();
            }
        }

        public void SetImportantColorToPurple()
        {
            importantColor = PURPLE;
        }


        private void LocalRefresh()
        {
            string strSQL;
            RemarkStatus = NON_COMPLETED;
            RequestRemarkDesc = "";

            strSQL = "select * from lims_sys.u_request_remark, lims_sys.u_request_remark_user " +
         "where u_request_remark.name = '" + SdgName + "' and u_request_remark.u_request_remark_id = " +
         "u_request_remark_user.u_request_remark_id";

            cmd = new OracleCommand(strSQL, Connection);
            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                RequestRemarkDesc = nte(reader["DESCRIPTION"].ToString());
                switch (nte(reader["U_STATUS"].ToString()))
                {
                    case COMPLETED:
                        {
                            RemarkStatus = COMPLETED;
                            ((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor = Color.Lime;
                            ((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor2 = Color.Lime;
                            ((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor3 = Color.Lime;
                            ((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor4 = Color.Lime;
                            CmdShowRemark.ButtonElement.ForeColor = Color.Black;
                            if (StatusChanged != null)
                                StatusChanged(COMPLETED);
                            break;
                        }
                    case NON_COMPLETED:
                        {
                            ((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor = Color.Red;
                            ((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor2 = Color.Red;
                            ((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor3 = Color.Red;
                            ((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor4 = Color.Red;
                            CmdShowRemark.ButtonElement.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FBFBFB");
                            StatusChanged(NON_COMPLETED);
                            break;
                        }
                    case IMPORTANT:
                        {
                            ((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor = System.Drawing.Color.FromArgb(128, 128, 255);
                            ((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor2 = System.Drawing.Color.FromArgb(128, 128, 255);
                            ((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor3 = System.Drawing.Color.FromArgb(128, 128, 255);
                            ((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor4 = System.Drawing.Color.FromArgb(128, 128, 255);
                            CmdShowRemark.ButtonElement.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FBFBFB");
                            StatusChanged(IMPORTANT);
                            break;
                        }

                    default:
                        {
                            //((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor = SystemColors.ButtonFace; //System.Drawing.ColorTranslator.FromHtml(BUTTON_FACE);
                            //((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor2 = SystemColors.ButtonFace; //System.Drawing.ColorTranslator.FromHtml(BUTTON_FACE);
                            //((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor3 = SystemColors.ButtonFace; //System.Drawing.ColorTranslator.FromHtml(BUTTON_FACE);
                            //((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor4 = SystemColors.ButtonFace; //System.Drawing.ColorTranslator.FromHtml(BUTTON_FACE); 
                            //CmdShowRemark.ButtonElement.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FBFBFB");
                            StatusChanged(COMPLETED);
                            break;
                        }
                }
            }
            else
            {
                ((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor = System.Drawing.Color.FromArgb(232, 241, 252);
                ((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor2 = System.Drawing.Color.FromArgb(233, 241, 252);
                ((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor3 = System.Drawing.Color.FromArgb(211, 226, 244);
                ((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor4 = System.Drawing.Color.FromArgb(231, 240, 251);
                CmdShowRemark.ButtonElement.ForeColor = System.Drawing.Color.FromArgb(21, 66, 139);

                //CmdShowRemark.BackColor = System.Drawing.Color.FromName(BUTTON_FACE);
                StatusChanged(COMPLETED);
            }

        }

        private string nte(string e)
        {
            return string.IsNullOrEmpty(e) ? "" : e;
        }

        public void RefreshButton()
        {
            LocalRefresh();
        }


        private void GetRequestRemarkDESC(int ExitFlag)
        {
            if (ExitFlag != 1 && ExitFlag != 2 && ExitFlag != 3)
            {
                return;
            }

            string StatusToUpdate = "";
            switch (ExitFlag)
            {
                case 1:
                    {
                        StatusToUpdate = NON_COMPLETED;
                        break;
                    }
                case 2:
                    {
                        StatusToUpdate = COMPLETED;
                        break;
                    }
                case 3:
                    {
                        StatusToUpdate = IMPORTANT;
                        break;
                    }
                default:
                    break;
            }

            if (string.IsNullOrEmpty(SdgName))
                return;

            UpdateRemark(StatusToUpdate);
            UpdateRemarkStatus(StatusToUpdate);
            if (StatusToUpdate == COMPLETED)
            {
                UpdateRemarkCompleted();
            }
        }

        public void UpdateRemark(string StatToUpdate)
        {
            if (string.IsNullOrEmpty(SdgName))
                return;

            string strSQL;
            strSQL = "select * from lims_sys.u_request_remark where name = '" + SdgName + "'";
            //RequestRemark = Connection.Execute(strSQL);
            cmd = new OracleCommand(strSQL, Connection);
            OracleDataReader RequestRemark = cmd.ExecuteReader();
            if (!RequestRemark.HasRows)
            {
                BuildNewRemark(RequestRemarkDesc, StatToUpdate);
            }

            UpdateRemarkDesc(RequestRemarkDesc);
        }

        public void BuildNewRemark(string Desc, string Status)
        {
            if (string.IsNullOrEmpty(SdgName))
                return;

            //ADODB.Recordset SQ_RequestRemark;
            string strSQL, RequestRemarkID;
            strSQL = "select lims.sq_u_request_remark.nextval from dual";
            cmd = new OracleCommand(strSQL, Connection);
            var SQ_RequestRemark = cmd.ExecuteScalar();
            //SQ_RequestRemark = Connection.Execute("select lims.sq_u_request_remark.nextval from dual");
            RequestRemarkID = SQ_RequestRemark.ToString();
            Desc = CheckApostrophe(Desc);

            strSQL = "insert into lims_sys.u_request_remark (U_REQUEST_REMARK_ID, NAME, DESCRIPTION, VERSION, VERSION_STATUS) " +
            "values (" + RequestRemarkID + ", " +
            "'" + SdgName + "', " +
            "'" + Desc + "', " +
            "'1', " + "'A')";
            cmd = new OracleCommand(strSQL, Connection);
            cmd.ExecuteScalar();
            //Connection.Execute(strSQL);

            strSQL = "insert into lims_sys.u_request_remark_user (U_REQUEST_REMARK_ID, U_STATUS, U_CREATED_ON, " +
        "U_COMPLETED_ON, U_CREATED_BY, U_COMPLETED_BY) values (" + RequestRemarkID + ", '" + Status + "', " +
        "sysdate, NULL, '" + OperatorID + "', NULL)";
            cmd = new OracleCommand(strSQL, Connection);
            cmd.ExecuteScalar();
            //Connection.Execute(strSQL);
        }

        public void UpdateRemarkDesc(string Desc)
        {
            string strSQL;
            Desc = CheckApostrophe(Desc);
            strSQL = "update lims_sys.u_request_remark set DESCRIPTION = '" + Desc + "' where name = '" + SdgName + "'";
            cmd = new OracleCommand(strSQL, Connection);
            cmd.ExecuteScalar();
            //Connection.Execute(strSQL);
        }


        public void UpdateRemarkStatus(string Status)
        {
            if (string.IsNullOrEmpty(SdgName))
                return;

            string strSQL;
            strSQL = "select U_REQUEST_REMARK_ID from lims_sys.u_request_remark where name = '" + SdgName + "'";
            cmd = new OracleCommand(strSQL, Connection);
            var RequestRemark = cmd.ExecuteScalar();
            //RequestRemark = Connection.Execute(strSQL);
            string id = RequestRemark.ToString();
            strSQL = "update lims_sys.u_request_remark_user set U_STATUS = '" + Status + "' where u_request_remark_id = " + id;
            cmd = new OracleCommand(strSQL, Connection);
            cmd.ExecuteScalar();

            //Connection.Execute(strSQL);
        }

        private void UpdateRemarkCompleted()
        {
            if (string.IsNullOrEmpty(SdgName))
                return;

            string strSQL;
            strSQL = "select * from lims_sys.u_request_remark where name = '" + SdgName + "'";
            cmd = new OracleCommand(strSQL, Connection);
            var RequestRemark = cmd.ExecuteScalar();
            //RequestRemark = Connection.Execute(strSQL);
            string id = RequestRemark.ToString();

            strSQL = "update lims_sys.u_request_remark_user set U_COMPLETED_ON = sysdate, U_COMPLETED_BY = '" + OperatorID + "' where u_request_remark_id = " + id;
            cmd = new OracleCommand(strSQL, Connection);
            //Connection.Execute(strSQL);
        }

        private string CheckApostrophe(string s)
        {
            string s2 = "";
            for (int i = 0; i < s.Length - 1; i++)
            {
                if (s[i].ToString() == "'")
                {
                    s2 = s2 + "''";
                }
                else
                {
                    s2 = s2 + s[i];
                }
            }
            return s2;
        }

        public string GetRemarkStatus(string SdgName)
        {
            string strSQL;
            //ADODB.Recordset RemarkRs;
            strSQL = "select rru.u_status from lims_sys.u_request_remark rr, lims_sys.u_request_remark_user rru " +
         "where rr.name = '" + SdgName + "' and rr.u_request_remark_id = rru.u_request_remark_id";
            cmd = new OracleCommand(strSQL, Connection);
            var RemarkRs = cmd.ExecuteScalar();
            //RemarkRs = Connection.Execute(strSQL);
            if (RemarkRs != null)
            {
                return nte(RemarkRs.ToString());
            }
            return "";
        }

        private void UserControl_Initialize()
        {
            importantColor = BLUE;
        }

        public OracleConnection GetConnection()
        {

            OracleConnection connection = null;

            if (_ntlsCon != null)
            {


                // Initialize variables
                String roleCommand;
                // Try/Catch block
                try
                {


                    var C = _ntlsCon.GetServerIsProxy();
                    var C2 = _ntlsCon.GetServerName();
                    var C4 = _ntlsCon.GetServerType();

                    var C6 = _ntlsCon.GetServerExtra();

                    var C8 = _ntlsCon.GetPassword();
                    var C9 = _ntlsCon.GetLimsUserPwd();
                    var C10 = _ntlsCon.GetServerIsProxy();
                    var DD = _ntlsSite;




                    var u = _ntlsUser.GetOperatorName();
                    var u1 = _ntlsUser.GetWorkstationName();



                    _connectionString = _ntlsCon.GetADOConnectionString();

                    var splited = _connectionString.Split(';');

                    var cs = "";

                    for (int i = 1; i < splited.Count(); i++)
                    {
                        cs += splited[i] + ';';
                    }
                    //<<<<<<< .mine
                    var username = _ntlsCon.GetUsername();
                    if (string.IsNullOrEmpty(username))
                    {
                        var serverDetails = _ntlsCon.GetServerDetails();
                        cs = "User Id=/;Data Source=" + serverDetails + ";";
                    }


                    //Create the connection
                    connection = new OracleConnection(cs);



                    // Open the connection
                    connection.Open();

                    // Get lims user password
                    string limsUserPassword = _ntlsCon.GetLimsUserPwd();

                    // Set role lims user
                    if (limsUserPassword == "")
                    {
                        // LIMS_USER is not password protected
                        roleCommand = "set role lims_user";
                    }
                    else
                    {
                        // LIMS_USER is password protected.
                        roleCommand = "set role lims_user identified by " + limsUserPassword;
                    }

                    // set the Oracle user for this connecition
                    OracleCommand command = new OracleCommand(roleCommand, connection);

                    // Try/Catch block
                    try
                    {
                        // Execute the command
                        command.ExecuteNonQuery();
                    }
                    catch (Exception f)
                    {
                        // Throw the exception
                        throw new Exception("Inconsistent role Security : " + f.Message);
                    }

                    // Get the session id
                    sessionId = _ntlsCon.GetSessionId();

                    // Connect to the same session
                    string sSql = string.Format("call lims.lims_env.connect_same_session({0})", sessionId);

                    // Build the command
                    command = new OracleCommand(sSql, connection);

                    // Execute the command
                    command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    // Throw the exception
                    throw e;
                }

                // Return the connection
            }

            return connection;
        }


        public void InitializeCtrl(ADODB.Connection Con, string p)
        {
            throw new NotImplementedException();
        }

        public void sampleInput(string p)
        {
            this.Visible = true;
            GetsdgName(ref p);
            RefreshButton();
        }

        public void GetConnectionParams(INautilusDBConnection ntlsCon, IExtensionWindowSite2 ntlsSite, INautilusUser ntlsUser)
        {
            _ntlsCon = ntlsCon;
            _ntlsSite = ntlsSite;
            _ntlsUser = ntlsUser;

        }
        bool flag = true;
        public void Reset()
        {
            if (this == null)
                this.Visible = false;


            SdgName = null;
            ((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor = System.Drawing.Color.FromArgb(232, 241, 252);
            ((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor2 = System.Drawing.Color.FromArgb(233, 241, 252);
            ((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor3 = System.Drawing.Color.FromArgb(211, 226, 244);
            ((FillPrimitive)CmdShowRemark.ButtonElement.GetChildrenByType(typeof(FillPrimitive))[0]).BackColor4 = System.Drawing.Color.FromArgb(231, 240, 251);
            CmdShowRemark.ButtonElement.ForeColor = System.Drawing.Color.FromArgb(21, 66, 139);

        } 


        private void CmdShowRemark_Click(object sender, EventArgs e)
        {
            if (SdgName == null)
            {
                MessageBox.Show("לא ניתן להוסיף הערות לדרישה שטרם התקבלה");
                flag = true;
                return;
            }
            FrmRequestRemark = new RequestRemarkForm();
            FrmRequestRemark.Text = getTitle();
            setOperatorIdAndName();


            FrmRequestRemark.InitializeRemark(RequestRemarkDesc, SdgName);
            FrmRequestRemark.InitializeRemarkStat(RemarkStatus);

            if (RemarkStatus == COMPLETED)
            {
                FrmRequestRemark.ReadOnly(true);
            }

            FrmRequestRemark.ShowDialog();
            RequestRemarkDesc = FrmRequestRemark.RemarkText;
            GetRequestRemarkDESC(FrmRequestRemark.ExitCode);
            LocalRefresh();
        }

        private string getTitle()
        {
            int sdgID;
            string patholabName = string.Empty;
            if (string.IsNullOrEmpty(SdgName))
                return string.Empty;

            try
            {
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = Connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = string.Format("select SDG_ID from lims_sys.sdg where name = '{0}'", SdgName);

                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        sdgID = Convert.ToInt32(reader[0]);

                        cmd.CommandText = string.Format("select U_PATHOLAB_NUMBER from lims_sys.sdg_user where sdg_id = '{0}'", sdgID.ToString());

                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            patholabName = Convert.ToString(reader[0]);
                        }
                    }

                    reader.Close();
                }

                return SdgName + " - " + patholabName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return string.Empty;
            }
        }

        private void RequestRemarkControl_Load(object sender, EventArgs e)
        {

        }
    }
}
