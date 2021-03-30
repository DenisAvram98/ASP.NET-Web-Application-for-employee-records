using CrystalDecisions.CrystalReports.Engine;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proiect
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        OracleConnection con = new OracleConnection("DATA SOURCE = localhost:1521 / XE;PASSWORD=student;PERSIST SECURITY INFO=True;USER ID = STUDENT");
        OracleDataAdapter oDa;
        DataSet ds;
        ReportDocument report;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    con.Open();
                    string cmd = "SELECT NRCRT AS \"Nr.crt.\", NUME AS \"Nume\", PRENUME AS \"Prenume\", FUNCTIE AS \"Functie\", SALARBAZA AS \"Salariu de baza\", SPORPROCENT AS \"Spor (%)\", PREMIIBRUTE AS \"Premii brute\", TOTALBRUT AS \"Total brut\", BRUTIMPOZABIL AS \"Brut impozabil\", IMPOZIT AS \"Impozit\", CAS, CASS, RETINERI AS \"Retineri salariale\", VIRATCARD AS \"Virat pe card\" FROM ANGAJATI ORDER BY NRCRT";
                    //string cmd= "SELECT NRCRT, NUME, PRENUME, FUNCTIE, SALARBAZA, SPORPROCENT, PREMIIBRUTE, TOTALBRUT, BRUTIMPOZABIL, IMPOZIT, CAS, CASS, RETINERI, VIRATCARD FROM ANGAJATI ORDER BY NRCRT";
                    oDa = new OracleDataAdapter(cmd, con);
                    ds = new DataSet();
                    oDa.Fill(ds, "Angajati");

                    report = new ReportDocument();
                    //string path = Server.MapPath("StatPlata.rpt");
                    string path = Server.MapPath("test.rpt");
                    report.Load(path);
                    report.SetDataSource(ds.Tables["Angajati"]);
                    CrystalReportViewer1.ReportSource = report;
                    Session["Report"] = report;

                    LabelPrincipalErr.Text = "";
                    ds.Dispose();
                    oDa.Dispose();
                }
                catch (Exception ex)
                {
                    LabelPrincipalErr.Text="Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                    LabelPrincipalErr.ForeColor = Color.Red;
                }
                finally
                {
                    con.Close();
                }
            }
            if (Session["Report"]!=null)
            {
                CrystalReportViewer1.ReportSource = Session["Report"];
            }
        }
    }
}