using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
    public partial class WebForm8 : System.Web.UI.Page
    {
        OracleConnection con = new OracleConnection("DATA SOURCE = localhost:1521 / XE;PASSWORD=student;PERSIST SECURITY INFO=True;USER ID = STUDENT");
        OracleDataAdapter oDa;
        DataSet ds;
        ReportDocument report;

        protected void IncarcareRaport (DataSet ds)
        {
            report = new ReportDocument();
            string path = Server.MapPath("Fluturasi.rpt");
            report.Load(path);
            report.SetDataSource(ds.Tables["Angajati"]);
            CrystalReportViewer1.ReportSource = report;
            Session["Fluturasi"] = report;
        }

        protected void IncarcareRaport ()
        {
            con.Open();
            string cmd = "SELECT NRCRT AS \"Nr.crt.\", NUME AS \"Nume\", PRENUME AS \"Prenume\", FUNCTIE AS \"Functie\", SALARBAZA AS \"Salariu de baza\", SPORPROCENT AS \"Spor (%)\", PREMIIBRUTE AS \"Premii brute\", TOTALBRUT AS \"Total brut\", BRUTIMPOZABIL AS \"Brut impozabil\", IMPOZIT AS \"Impozit\", CAS, CASS, RETINERI AS \"Retineri salariale\", VIRATCARD AS \"Virat pe card\" FROM ANGAJATI ORDER BY NRCRT";
            oDa = new OracleDataAdapter(cmd, con);
            ds = new DataSet();
            oDa.Fill(ds, "Angajati");

            IncarcareRaport(ds);
            ds.Dispose();
            oDa.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    IncarcareRaport();
                    LabelPrincipalErr.Text = "";
                }
                catch (Exception ex)
                {
                    LabelPrincipalErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                    LabelPrincipalErr.ForeColor = Color.Red;
                }
                finally
                {
                    con.Clone();
                }
            }

            if (Session["Fluturasi"] != null)
            {
                CrystalReportViewer1.ReportSource = Session["Fluturasi"];
            }
        }

        protected void cautaBtn_Click(object sender, EventArgs e)
        {
            if (numeTB.Text.Trim() != "" && prenumeTB.Text.Trim() == "")
            {
                try
                {
                    con.Open();
                    string cmd = "select NrCrt as \"Nr.crt.\", Nume as \"Nume\", Prenume as \"Prenume\", Functie as \"Functie\", SalarBaza as \"Salariu de baza\", SporProcent as \"Spor (%)\", PremiiBrute as \"Premii brute\", TotalBrut as \"Total brut\", BrutImpozabil as \"Brut impozabil\", Impozit as \"Impozit\", CAS, CASS, Retineri as \"Retineri salariale\", ViratCard as \"Virat pe card\" from Angajati where Nume='" + numeTB.Text.Trim() + "' order by NrCrt";
                    oDa = new OracleDataAdapter(cmd, con);
                    ds = new DataSet();
                    oDa.Fill(ds, "Angajati");

                    if (ds.Tables["Angajati"].Rows.Count > 0)
                    {
                        LabelCautareErr.Text = "Cautarea a avut succes!";
                        LabelCautareErr.ForeColor = Color.Green;

                        IncarcareRaport(ds);
                    }
                    else
                    {
                        LabelCautareErr.Text = "Angajatul cu numele '" + numeTB.Text.Trim() + "' nu a fost gasit!";
                        LabelCautareErr.ForeColor = Color.Red;
                    }
                    oDa.Dispose();
                    ds.Dispose();
                }
                catch (Exception ex)
                {
                    LabelCautareErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                    LabelCautareErr.ForeColor = Color.Red;
                }
                finally
                {
                    con.Close();
                }
            }
            else if (numeTB.Text.Trim() == "" && prenumeTB.Text.Trim() != "")
            {
                try
                {
                    con.Open();
                    string cmd = "select NrCrt as \"Nr.crt.\", Nume as \"Nume\", Prenume as \"Prenume\", Functie as \"Functie\", SalarBaza as \"Salariu de baza\", SporProcent as \"Spor (%)\", PremiiBrute as \"Premii brute\", TotalBrut as \"Total brut\", BrutImpozabil as \"Brut impozabil\", Impozit as \"Impozit\", CAS, CASS, Retineri as \"Retineri salariale\", ViratCard as \"Virat pe card\" from Angajati where Prenume like '%" + prenumeTB.Text.Trim() + "%' order by NrCrt";
                    oDa = new OracleDataAdapter(cmd, con);
                    ds = new DataSet();
                    oDa.Fill(ds, "Angajati");

                    if (ds.Tables["Angajati"].Rows.Count > 0)
                    {
                        LabelCautareErr.Text = "Cautarea a avut succes!";
                        LabelCautareErr.ForeColor = Color.Green;

                        IncarcareRaport(ds);
                    }
                    else
                    {
                        LabelCautareErr.Text = "Angajatul cu prenumele '" + prenumeTB.Text.Trim() + "' nu a fost gasit!";
                        LabelCautareErr.ForeColor = Color.Red;
                    }
                    oDa.Dispose();
                    ds.Dispose();
                }
                catch (Exception ex)
                {
                    LabelCautareErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                    LabelCautareErr.ForeColor = Color.Red;
                }
                finally
                {
                    con.Close();
                }
            }
            else if (numeTB.Text.Trim() != "" && prenumeTB.Text.Trim() != "")
            {
                try
                {
                    con.Open();
                    string cmd = "select NrCrt as \"Nr.crt.\", Nume as \"Nume\", Prenume as \"Prenume\", Functie as \"Functie\", SalarBaza as \"Salariu de baza\", SporProcent as \"Spor (%)\", PremiiBrute as \"Premii brute\", TotalBrut as \"Total brut\", BrutImpozabil as \"Brut impozabil\", Impozit as \"Impozit\", CAS, CASS, Retineri as \"Retineri salariale\", ViratCard as \"Virat pe card\" from Angajati where Nume='" + numeTB.Text.Trim() + "' and Prenume like '%" + prenumeTB.Text.Trim() + "%' order by NrCrt";
                    oDa = new OracleDataAdapter(cmd, con);
                    ds = new DataSet();
                    oDa.Fill(ds, "Angajati");

                    if (ds.Tables["Angajati"].Rows.Count > 0)
                    {
                        LabelCautareErr.Text = "Cautarea a avut succes!";
                        LabelCautareErr.ForeColor = Color.Green;

                        IncarcareRaport(ds);
                    }
                    else
                    {
                        LabelCautareErr.Text = "Angajatul '" + numeTB.Text.Trim() + " " + prenumeTB.Text.Trim() + "' nu a fost gasit!";
                        LabelCautareErr.ForeColor = Color.Red;
                    }
                    oDa.Dispose();
                    ds.Dispose();
                }
                catch (Exception ex)
                {
                    LabelCautareErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                    LabelCautareErr.ForeColor = Color.Red;
                }
                finally
                {
                    con.Close();
                }

            }
            else
            {
                LabelCautareErr.Text = "Nu ati introdus criteriul de cautare!";
                LabelCautareErr.ForeColor = Color.Red;
            }
        }

        protected void anulareBtn_Click(object sender, EventArgs e)
        {
            numeTB.Text = "";
            prenumeTB.Text = "";
            LabelCautareErr.Text = "";
            IncarcareRaport();
        }
    }
}