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
    public partial class WebForm5 : System.Web.UI.Page
    {
        OracleConnection con = new OracleConnection("DATA SOURCE = localhost:1521 / XE;PASSWORD=student;PERSIST SECURITY INFO=True;USER ID = STUDENT");
        OracleCommand oCmd;
        OracleDataAdapter oDa;
        DataSet ds;

        protected void IncarcareDate()
        {
            con.Open();
            string cmd = "select NrCrt as \"Numar curent\", Nume as \"Nume\", Prenume as \"Prenume\", Functie as \"Functie\", SalarBaza as \"Salariu de baza\", SporProcent as \"Spor (%)\", PremiiBrute as \"Premii brute\", TotalBrut as \"Total brut\", BrutImpozabil as \"Brut impozabil\", Impozit as \"Impozit\", CAS, CASS, Retineri as \"Retineri salariale\", ViratCard as \"Virat pe card\" from Angajati order by NrCrt";
            oDa = new OracleDataAdapter(cmd, con);
            ds = new DataSet();
            oDa.Fill(ds, "Angajati");
            GridView1.DataSource = ds.Tables["Angajati"];
            GridView1.DataBind();

            ds.Dispose();
            oDa.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    IncarcareDate();
                    LabelPrincipalErr.Text = "";
                }
                catch (Exception ex)
                {
                    LabelPrincipalErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                    LabelPrincipalErr.ForeColor = Color.Red;
                }
                finally
                {
                    con.Close();
                }
            }
        }


        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
                    row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                    // Set the last parameter to True 
                    // to register for event validation. 
                    row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + row.DataItemIndex, true); //command name Select
                    row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");
                }
            }
            base.Render(writer);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap; text-align:center; padding:4px;");
            }
        }

        protected void cautaBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (numeTB.Text.Trim() != "" && prenumeTB.Text.Trim() == "")
            {
                try
                {
                    con.Open();
                    string cmd = "select NrCrt as \"Numar curent\", Nume as \"Nume\", Prenume as \"Prenume\", Functie as \"Functie\", SalarBaza as \"Salariu de baza\", SporProcent as \"Spor (%)\", PremiiBrute as \"Premii brute\", TotalBrut as \"Total brut\", BrutImpozabil as \"Brut impozabil\", Impozit as \"Impozit\", CAS, CASS, Retineri as \"Retineri salariale\", ViratCard as \"Virat pe card\" from Angajati where Nume='" + numeTB.Text.Trim() + "' order by NrCrt";
                    //string cmd = "select * from Angajati where Nume='" + numeTB.Text.Trim() + "'";
                    oDa = new OracleDataAdapter(cmd, con);
                    ds = new DataSet();
                    oDa.Fill(ds, "Angajati");
                    GridView1.DataSource = ds.Tables["Angajati"].DefaultView;
                    GridView1.DataBind();

                    if (ds.Tables["Angajati"].Rows.Count > 0)
                    {
                        if (btn.Text == "Cauta")
                        {
                            LabelCautareErr.Text = "Cautarea a avut succes!";
                            LabelCautareErr.ForeColor = Color.Green;
                        }

                        if (ds.Tables["Angajati"].Rows.Count == 1)
                        {
                            if (btn.Text == "Cauta")
                            {
                                GridView1.SelectedIndex = 0;
                            }
                            else
                            {
                                GridView1.SelectedIndex = -1;
                            }
                            stergeBtn.Visible = true;
                        }
                        else
                        {
                            stergeBtn.Visible = true;
                        }
                    }
                    else
                    {
                        if (btn.Text == "Cauta")
                        {
                            LabelCautareErr.Text = "Angajatul cu numele '" + numeTB.Text.Trim() + "' nu a fost gasit!";
                            LabelCautareErr.ForeColor = Color.Red;
                            stergeBtn.Visible = false;
                        }
                    }
                    oDa.Dispose();
                    ds.Dispose();
                }
                catch (Exception ex)
                {
                    LabelCautareErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                    LabelCautareErr.ForeColor = Color.Red;
                    stergeBtn.Visible = false;
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
                    string cmd = "select NrCrt as \"Numar curent\", Nume as \"Nume\", Prenume as \"Prenume\", Functie as \"Functie\", SalarBaza as \"Salariu de baza\", SporProcent as \"Spor (%)\", PremiiBrute as \"Premii brute\", TotalBrut as \"Total brut\", BrutImpozabil as \"Brut impozabil\", Impozit as \"Impozit\", CAS, CASS, Retineri as \"Retineri salariale\", ViratCard as \"Virat pe card\" from Angajati where Prenume like '%" + prenumeTB.Text.Trim() + "%' order by NrCrt";
                    //string cmd = "select * from Angajati where Nume='" + numeTB.Text.Trim() + "'";
                    oDa = new OracleDataAdapter(cmd, con);
                    ds = new DataSet();
                    oDa.Fill(ds, "Angajati");
                    GridView1.DataSource = ds.Tables["Angajati"].DefaultView;
                    GridView1.DataBind();

                    if (ds.Tables["Angajati"].Rows.Count > 0)
                    {
                        if (btn.Text == "Cauta")
                        {
                            LabelCautareErr.Text = "Cautarea a avut succes!";
                            LabelCautareErr.ForeColor = Color.Green;
                        }

                        if (ds.Tables["Angajati"].Rows.Count == 1)
                        {
                            if (btn.Text == "Cauta")
                            {
                                GridView1.SelectedIndex = 0;
                            }
                            else
                            {
                                GridView1.SelectedIndex = -1;
                            }
                            stergeBtn.Visible = true;
                        }
                        else
                        {
                            stergeBtn.Visible = true;
                        }
                    }
                    else
                    {
                        if (btn.Text == "Cauta")
                        {
                            LabelCautareErr.Text = "Angajatul cu prenumele '" + prenumeTB.Text.Trim() + "' nu a fost gasit!";
                            LabelCautareErr.ForeColor = Color.Red;
                            stergeBtn.Visible = false;
                        }
                    }
                    oDa.Dispose();
                    ds.Dispose();
                }
                catch (Exception ex)
                {
                    LabelCautareErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                    LabelCautareErr.ForeColor = Color.Red;
                    stergeBtn.Visible = false;
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
                    string cmd = "select NrCrt as \"Numar curent\", Nume as \"Nume\", Prenume as \"Prenume\", Functie as \"Functie\", SalarBaza as \"Salariu de baza\", SporProcent as \"Spor (%)\", PremiiBrute as \"Premii brute\", TotalBrut as \"Total brut\", BrutImpozabil as \"Brut impozabil\", Impozit as \"Impozit\", CAS, CASS, Retineri as \"Retineri salariale\", ViratCard as \"Virat pe card\" from Angajati where Nume='" + numeTB.Text.Trim() + "' and Prenume like '%" + prenumeTB.Text.Trim() + "%' order by NrCrt";
                    //string cmd = "select * from Angajati where Nume='" + numeTB.Text.Trim() + "'";
                    oDa = new OracleDataAdapter(cmd, con);
                    ds = new DataSet();
                    oDa.Fill(ds, "Angajati");
                    GridView1.DataSource = ds.Tables["Angajati"].DefaultView;
                    GridView1.DataBind();

                    if (ds.Tables["Angajati"].Rows.Count > 0)
                    {
                        if (btn.Text == "Cauta")
                        {
                            LabelCautareErr.Text = "Cautarea a avut succes!";
                            LabelCautareErr.ForeColor = Color.Green;
                        }

                        if (ds.Tables["Angajati"].Rows.Count == 1)
                        {
                            if (btn.Text == "Cauta")
                            {
                                GridView1.SelectedIndex = 0;
                            }
                            else
                            {
                                GridView1.SelectedIndex = -1;
                            }
                            stergeBtn.Visible = true;
                        }
                        else
                        {
                            stergeBtn.Visible = true;
                        }
                    }
                    else
                    {
                        if (btn.Text == "Cauta")
                        {
                            LabelCautareErr.Text = "Angajatul '" + numeTB.Text.Trim() + " " + prenumeTB.Text.Trim() + "' nu a fost gasit!";
                            LabelCautareErr.ForeColor = Color.Red;
                            stergeBtn.Visible = false;
                        }
                    }
                    oDa.Dispose();
                    ds.Dispose();
                }
                catch (Exception ex)
                {
                    LabelCautareErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                    LabelCautareErr.ForeColor = Color.Red;
                    stergeBtn.Visible = false;
                }
                finally
                {
                    con.Close();
                }

            }
            else if (numeTB.Text.Trim() == "" && prenumeTB.Text.Trim() == "" && GridView1.Rows.Count > 0)
            {
                stergeBtn.Visible = false;
                GridView1.SelectedIndex = -1;
                try
                {
                    IncarcareDate();
                    LabelPrincipalErr.Text = "";
                }
                catch (Exception ex)
                {
                    LabelPrincipalErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                    LabelPrincipalErr.ForeColor = Color.Red;
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
                stergeBtn.Visible = false;
            }
        }

        protected void anulareBtn_Click(object sender, EventArgs e)
        {
            numeTB.Text = "";
            prenumeTB.Text = "";
            LabelCautareErr.Text = "";
            LabelPrincipalErr.Text = "";
            try
            {
                IncarcareDate();
                LabelPrincipalErr.Text = "";
            }
            catch (Exception ex)
            {
                LabelPrincipalErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelPrincipalErr.ForeColor = Color.Red;
            }
            finally
            {
                con.Close();
            }
            GridView1.SelectedIndex = -1;
            stergeBtn.Visible = false;
        }

        protected void stergeBtn_Click(object sender, EventArgs e)
        {
            int test = GridView1.SelectedIndex;
            if (GridView1.SelectedIndex >= 0)
            {
                int indexGV = GridView1.SelectedIndex;
                int nrCrt = Int32.Parse(GridView1.Rows[indexGV].Cells[0].Text.ToString().Trim());

                try
                {
                    con.Open();
                    string cmd = "delete from Angajati where NrCrt=" + nrCrt;
                    oCmd = new OracleCommand(cmd, con);
                    oCmd.ExecuteNonQuery();

                    LabelCautareErr.Text = "Stergerea angajatului a fost efectuata cu succes!";
                    LabelCautareErr.ForeColor = Color.Green;
                    oCmd.Dispose();
                    if (GridView1.Rows.Count == 1)
                    {
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                        GridView1.SelectedIndex = -1;
                    }
                    else
                    {
                        con.Close(); //Nu se poate realiza conexiunea la baza de date: Connection is already open
                        EventArgs eventArgs = new EventArgs();
                        cautaBtn_Click(stergeBtn, eventArgs);
                    }
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
                LabelCautareErr.Text = "Nu ati selectat angajatul care doriti sa il stergeti!";
                LabelCautareErr.ForeColor = Color.Red;
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                LabelCautareErr.Text = "";
                stergeBtn.Visible = true;
            }
        }
    }
}