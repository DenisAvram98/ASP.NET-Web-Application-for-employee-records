using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proiect
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        OracleConnection con = new OracleConnection("DATA SOURCE = localhost:1521 / XE;PASSWORD=student;PERSIST SECURITY INFO=True;USER ID = STUDENT");
        OracleCommand oCmd;
        OracleDataReader oDr;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["Filter"] = null;
            }
            if (Session["Filter"] != null)
            {
                SqlDataSource1.FilterExpression = Session["Filter"].ToString();
                SqlDataSource1.DataBind();
                DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                int rowCount = dv.Count;
                if (dv.Count > 0)
                {}
                else
                {
                    Session["Filter"] = null;
                    ImgControlsHidden();
                }
            }
            else
            {
                SqlDataSource1.FilterExpression = "";
                SqlDataSource1.DataBind();
                //ImgControlsHidden();
            }
        }

        /*
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
        }*/

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e) //ca tabelu sa nu foloseasca warp-text
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap; text-align:center; padding:4px;");
            }
        }

        protected void IncarcarePozaAngajat(int nrCrt)
        {
            con.Open();
            string cmd = "select Poza from Angajati where NrCrt=" + nrCrt;
            oCmd = new OracleCommand(cmd, con);
            oDr = oCmd.ExecuteReader();

            if (oDr.HasRows)
            {
                oDr.Read();
                if (oDr["poza"] != DBNull.Value)
                {
                    BinaryWriter binaryW = null;
                    string temp = Server.MapPath("temp.jpg");
                    binaryW = new BinaryWriter(File.OpenWrite(temp));
                    binaryW.Write((byte[])oDr["Poza"]);
                    binaryW.Flush();
                    binaryW.Close();

                    pozaAngajatImg.ImageUrl = "temp.jpg";
                    pozaAngajatImg.Attributes.Add("style", "visibility: visible;");
                    TableDataFileUpload.Attributes.Add("style", "visibility: visible;");
                }
                else
                {
                    pozaAngajatImg.Attributes.Add("style", "visibility: visible;");
                    pozaAngajatImg.ImageUrl = "";
                    TableDataFileUpload.Attributes.Add("style", "visibility: visible;");
                }
            }
            else
            {
                pozaAngajatImg.Attributes.Add("style", "visibility: visible;");
                pozaAngajatImg.ImageUrl = "";
                TableDataFileUpload.Attributes.Add("style", "visibility: visible;");
            }
            LabelCautareErr.Text = "";
            LabelSalvareImagineErr.Text = "";

            oDr.Close();
            oCmd.Dispose();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int indexGV = Convert.ToInt32(e.CommandArgument);
                int nrCrt = Int32.Parse(GridView1.Rows[indexGV].Cells[1].Text.ToString().Trim());

                try
                {
                    IncarcarePozaAngajat(nrCrt);
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
            else if (e.CommandName == "Edit")
            {
                int indexGV = Convert.ToInt32(e.CommandArgument);
                int nrCrt = Int32.Parse(GridView1.Rows[indexGV].Cells[1].Text.ToString().Trim());
                GridView1.SelectedIndex = indexGV;

                try
                {
                    IncarcarePozaAngajat(nrCrt);
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
        }

        protected void salvareImgBtn_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                int indexGV = GridView1.SelectedIndex;
                int nrCrt = Int32.Parse(GridView1.Rows[indexGV].Cells[1].Text.ToString().Trim());
                string cmd = "update Angajati set Poza=:1 where NrCrt=" + nrCrt;
                oCmd = new OracleCommand(cmd, con);

                OracleParameter poza = new OracleParameter();
                if (pozaAngajatFU.HasFile)
                {
                    HttpPostedFile pFile = pozaAngajatFU.PostedFile;
                    byte[] data = new byte[pFile.ContentLength];
                    pFile.InputStream.Read(data, 0, pFile.ContentLength);
                    poza.Value = data;

                    oCmd.Parameters.Add(poza);
                    oCmd.ExecuteNonQuery();

                    LabelSalvareImagineErr.ForeColor = Color.Green;
                    LabelSalvareImagineErr.Text = "Imaginea a fost salvata cu succes!";

                    BinaryWriter binaryW = null;
                    string temp = Server.MapPath("temp.jpg");
                    binaryW = new BinaryWriter(File.OpenWrite(temp));
                    binaryW.Write(data);
                    binaryW.Flush();
                    binaryW.Close();
                    pozaAngajatImg.ImageUrl = "temp.jpg";
                }
                else
                {
                    //imaginea nu ii selectata
                }
                oCmd.Dispose();
            }
            catch (Exception ex)
            {
                LabelSalvareImagineErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelSalvareImagineErr.ForeColor = Color.Red;
            }
            finally
            {
                con.Close();
            }
        }

        protected void ApelArtificialGridView_RowCommand()
        {
            CommandEventArgs commandArgs = new CommandEventArgs("Select", GridView1.SelectedIndex.ToString()); // e.CommandName, e.CommandArgument (string, string)
            GridViewCommandEventArgs eventArgs = new GridViewCommandEventArgs(GridView1.SelectedRow, GridView1, commandArgs);
            GridView1_RowCommand(GridView1, eventArgs);
        }

        protected void ImgControlsHidden()
        {
            pozaAngajatImg.Attributes.Add("style", "visibility: hidden;");
            pozaAngajatImg.ImageUrl = "";
            TableDataFileUpload.Attributes.Add("style", "visibility: hidden;");
            LabelSalvareImagineErr.Text = "";
        }

        protected void cautaBtn_Click(object sender, EventArgs e)
        {
            if (numeTB.Text.Trim() != "" && prenumeTB.Text.Trim() == "")
            {
                //string FilterExpression = "Nume" + " LIKE '%{0}%'";
                //SqlDataSource1.FilterParameters.Clear();
                //SqlDataSource1.FilterParameters.Add(new ControlParameter("Nume", "numeTB", "Text"));
                //SqlDataSource1.FilterExpression=FilterExpression;
                Session["Filter"] = "Nume='" + numeTB.Text.Trim() + "'";
                SqlDataSource1.FilterExpression = "Nume='" + numeTB.Text.Trim() + "'";
                SqlDataSource1.DataBind();
                GridView1.DataBind();

                DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                int rowCount = dv.Count;
                if (dv.Count > 0)
                {
                    LabelCautareErr.Text = "Cautarea a avut succes!";
                    LabelCautareErr.ForeColor = Color.Green;
                    GridView1.SelectedIndex = 0;

                    //apelam evenimentul GridView1_RowCommand pentru a verifica daca angajatul are poza sau nu,
                    //adica pentru afisarea controalelor pentru poza angajatului
                    ApelArtificialGridView_RowCommand();
                }
                else
                {
                    Session["Filter"] = null;
                    LabelCautareErr.Text = "Angajatul cu numele '" + numeTB.Text.Trim() + "' nu a fost gasit!";
                    LabelCautareErr.ForeColor = Color.Red;
                    ImgControlsHidden();
                }
            }
            else if (numeTB.Text.Trim() == "" && prenumeTB.Text.Trim() != "")
            {
                Session["Filter"] = "Prenume like '%" + prenumeTB.Text.Trim() + "%'";
                SqlDataSource1.FilterExpression = "Prenume like '%" + prenumeTB.Text.Trim() + "%'";
                SqlDataSource1.DataBind();
                GridView1.DataBind();

                DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                int rowCount = dv.Count;
                if (dv.Count > 0)
                {
                    LabelCautareErr.Text = "Cautarea a avut succes!";
                    LabelCautareErr.ForeColor = Color.Green;
                    GridView1.SelectedIndex = 0;
                    ApelArtificialGridView_RowCommand();
                }
                else
                {
                    Session["Filter"] = null;
                    LabelCautareErr.Text = "Angajatul cu prenumele '" + prenumeTB.Text.Trim() + "' nu a fost gasit!";
                    LabelCautareErr.ForeColor = Color.Red;
                    ImgControlsHidden();
                }
            }
            else if (numeTB.Text.Trim() != "" && prenumeTB.Text.Trim() != "")
            {
                Session["Filter"] = "Nume='" + numeTB.Text.Trim() + "' and Prenume like '%" + prenumeTB.Text.Trim() + "%'";
                SqlDataSource1.FilterExpression = "Nume='" + numeTB.Text.Trim() + "' and Prenume like '%" + prenumeTB.Text.Trim() + "%'";
                SqlDataSource1.DataBind();
                GridView1.DataBind();

                DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                int rowCount = dv.Count;
                if (dv.Count > 0)
                {
                    LabelCautareErr.Text = "Cautarea a avut succes!";
                    LabelCautareErr.ForeColor = Color.Green;
                    GridView1.SelectedIndex = 0;
                    ApelArtificialGridView_RowCommand();
                }
                else
                {
                    Session["Filter"] = null;
                    LabelCautareErr.Text = "Angajatul '" + numeTB.Text.Trim() + " " + prenumeTB.Text.Trim() + "' nu a fost gasit!";
                    LabelCautareErr.ForeColor = Color.Red;
                    ImgControlsHidden();
                }
            }
            else
            {
                Session["Filter"] = null;
                LabelCautareErr.Text = "Nu ati introdus criteriul de cautare!";
                LabelCautareErr.ForeColor = Color.Red;
                ImgControlsHidden();
            }
        }

        protected void anulareBtn_Click(object sender, EventArgs e)
        {
            numeTB.Text = "";
            prenumeTB.Text = "";
            ImgControlsHidden();
            LabelCautareErr.Text = "";
            LabelSalvareImagineErr.Text = "";
            Session["Filter"] = null;
            SqlDataSource1.FilterExpression = "";
            SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            SqlDataSource1.DataBind();
            GridView1.DataBind();
            GridView1.SelectedIndex = -1;
        }

        protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            if (Session["Filter"] != null)
            {

                if (numeTB.Text.Trim() != "" && prenumeTB.Text.Trim() == "")
                {
                    int index = ((GridView)sender).EditIndex;
                    GridViewRow row = GridView1.Rows[index];
                    TextBox nume = (TextBox)row.FindControl("numeGV");

                    if (numeTB.Text.Trim() != nume.Text.Trim())
                    {
                        Session["Filter"] = "Nume='" + nume.Text.Trim() + "'";
                    }
                    else if (numeTB.Text.Trim() == nume.Text.Trim())
                    {
                        Session["Filter"] = "Nume='" + nume.Text.Trim() + "'";
                    }
                }
                else if (numeTB.Text.Trim() == "" && prenumeTB.Text.Trim() != "")
                {
                    int index = ((GridView)sender).EditIndex;
                    GridViewRow row = GridView1.Rows[index];
                    TextBox prenume = (TextBox)row.FindControl("prenumeGW");

                    if (prenumeTB.Text.Trim() != prenume.Text.Trim())
                    {
                        Session["Filter"] = "Prenume like '%" + prenume.Text.Trim() + "%'";
                    }
                    else if (prenumeTB.Text.Trim() == prenume.Text.Trim())
                    {
                        Session["Filter"] = "Prenume like '%" + prenume.Text.Trim() + "%'";
                    }
                }
                else if (numeTB.Text.Trim() != "" && prenumeTB.Text.Trim() != "")
                {
                    int index = ((GridView)sender).EditIndex;
                    GridViewRow row = GridView1.Rows[index];
                    TextBox nume = (TextBox)row.FindControl("numeGV");
                    TextBox prenume = (TextBox)row.FindControl("prenumeGW");

                    if (numeTB.Text.Trim() != nume.Text.Trim())
                    {
                        Session["Filter"] = "Nume='" + nume.Text.Trim() + "' and Prenume like '%" + prenume.Text.Trim() + "%'";
                    }
                    else if (prenumeTB.Text.Trim() != prenume.Text.Trim())
                    {
                        Session["Filter"] = "Nume='" + nume.Text.Trim() + "' and Prenume like '%" + prenume.Text.Trim() + "%'";
                    }
                    else if (numeTB.Text.Trim() != nume.Text.Trim() && prenumeTB.Text.Trim() != prenume.Text.Trim())
                    {
                        Session["Filter"] = "Nume='" + nume.Text.Trim() + "' and Prenume like '%" + prenume.Text.Trim() + "%'";
                    }
                    else if (numeTB.Text.Trim() == nume.Text.Trim() && prenumeTB.Text.Trim() == prenume.Text.Trim())
                    {
                        Session["Filter"] = "Nume='" + nume.Text.Trim() + "' and Prenume like '%" + prenume.Text.Trim() + "%'";
                    }
                }

                SqlDataSource1.FilterExpression = Session["Filter"].ToString();
                SqlDataSource1.DataBind();
                DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                int rowCount = dv.Count;
                if (dv.Count > 0)
                {
                    GridView1.SelectedIndex = -1;
                    ImgControlsHidden();
                }
                else
                {
                    Session["Filter"] = null;
                    ImgControlsHidden();
                }
            }
        }
    }
}