using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proiect
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        HttpPostedFile pfile;
        OracleConnection con = new OracleConnection("DATA SOURCE = localhost:1521 / XE;PASSWORD=student;PERSIST SECURITY INFO=True;USER ID = STUDENT");
        OracleCommand oCmd;
        OracleDataReader oDr;

        static int cas, cass, impozit;

        protected void TotalBrut_SiRestuCalcule()
        {
            if (Int32.Parse(salarBazaTB.Text) > 0 && Int32.Parse(sporProcentTB.Text) >= 0 && Int32.Parse(premiiBruteTB.Text) >= 0)
            {
                float totalBrut;
                float salarBaza = float.Parse(salarBazaTB.Text);
                float sporProcent = float.Parse(sporProcentTB.Text);
                float premiiBrute = float.Parse(premiiBruteTB.Text);
                totalBrut = salarBaza * (1 + sporProcent / 100) + premiiBrute;
                totalBrutTB.Text = ((int)totalBrut).ToString();

                float CAS = totalBrut * ((float)cas / 100);
                float CASS = totalBrut * ((float)cass / 100);
                casTB.Text = ((int)CAS).ToString();
                cassTB.Text = ((int)CASS).ToString();

                float brutImpozabil = totalBrut - CAS - CASS;
                brutImpozabilTB.Text = ((int)brutImpozabil).ToString();

                float IMPOZIT = brutImpozabil * ((float)impozit / 100);
                impozitTB.Text = ((int)IMPOZIT).ToString();

                if (Int32.Parse(retineriTB.Text) >= 0)
                {
                    float retineri = float.Parse(retineriTB.Text);
                    float viratCard = totalBrut - IMPOZIT - CAS - CASS - retineri;
                    viratCardTB.Text = ((int)viratCard).ToString();
                }
            }
        }

        protected void sporProcentTB_TextChanged(object sender, EventArgs e)
        {
            TotalBrut_SiRestuCalcule();
        }

        protected void premiiBruteTB_TextChanged(object sender, EventArgs e)
        {
            TotalBrut_SiRestuCalcule();
        }

        protected void retineriTB_TextChanged(object sender, EventArgs e)
        {
            TotalBrut_SiRestuCalcule();
        }

        protected void anulareBtn_Click(object sender, EventArgs e)
        {
            Session["pozaAngajat"] = null;
            Session["postedFile"] = null;
            numeTB.Text = "";
            prenumeTB.Text = "";
            functieTB.Text = "";
            salarBazaTB.Text = 0.ToString();
            sporProcentTB.Text = 0.ToString();
            premiiBruteTB.Text = 0.ToString();
            totalBrutTB.Text = 0.ToString();
            casTB.Text = 0.ToString();
            cassTB.Text = 0.ToString();
            brutImpozabilTB.Text = 0.ToString();
            impozitTB.Text = 0.ToString();
            retineriTB.Text = 0.ToString();
            viratCardTB.Text = 0.ToString();
            pozaAngajatImg.ImageUrl = "";
            pozaAngajatImg.Attributes.Add("style", "visibility:hidden;");

            LabelPrincipalErr.Text = "Anulare: toate campurile au fost resetate la valorile initiale.";
            LabelPrincipalErr.ForeColor = Color.Green;
            LabelRezultatAdaugare.Text = "Anulare: toate campurile au fost resetate la valorile initiale.";
            LabelRezultatAdaugare.ForeColor = Color.Green;
        }

        protected void adaugaAngajatBtn_Click(object sender, EventArgs e)
        {
            TotalBrut_SiRestuCalcule();

            try
            {
                con.Open();
                string cmd = "insert into Angajati (NrCrt,Nume,Prenume,Functie,SalarBaza,SporProcent,PremiiBrute,TotalBrut,BrutImpozabil,Impozit,CAS,CASS,Retineri,ViratCard,Poza) values (:1,:2,:3,:4, :5,:6,:7,:8,:9,:10,:11,:12,:13,:14,:15)";
                oCmd = new OracleCommand(cmd, con);

                OracleParameter nrC, n, p, f, sB, sP, pB, tB, bI, i, cas, cass, r, vC, poza;
                nrC = new OracleParameter();
                n = new OracleParameter();
                p = new OracleParameter();
                f = new OracleParameter();
                sB = new OracleParameter();
                sP = new OracleParameter();
                pB = new OracleParameter();
                tB = new OracleParameter();
                bI = new OracleParameter();
                i = new OracleParameter();
                cas = new OracleParameter();
                cass = new OracleParameter();
                r = new OracleParameter();
                vC = new OracleParameter();
                poza = new OracleParameter();

                nrC.Value = null;
                n.Value = numeTB.Text.Trim();
                p.Value = prenumeTB.Text.Trim();
                f.Value = functieTB.Text.Trim();
                sB.Value = salarBazaTB.Text;
                sP.Value = sporProcentTB.Text;
                pB.Value = premiiBruteTB.Text;
                tB.Value = totalBrutTB.Text;
                bI.Value = brutImpozabilTB.Text;
                i.Value = impozitTB.Text;
                cas.Value = casTB.Text;
                cass.Value = cassTB.Text;
                r.Value = retineriTB.Text;
                vC.Value = viratCardTB.Text;

                //If first time page is submitted and we have file in FileUpload control but not in session
                if (Session["pozaAngajat"] == null && pozaAngajat.HasFile != false && Session["postedFile"] == null)
                {
                    poza.Value = Blob();
                }
                // Next time submit and Session has values but FileUpload is Blank
                else if (Session["pozaAngajat"] != null && pozaAngajat.HasFile == false && Session["postedFile"] != null)
                {
                    pfile = (HttpPostedFile)Session["postedFile"];
                    byte[] data = new byte[pfile.ContentLength];
                    pfile.InputStream.Flush();
                    pfile.InputStream.Read(data, 0, pfile.ContentLength);
                    poza.Value = data;
                }
                // Now there could be another sictution when Session has File but user want to change the file
                else if (pozaAngajat.HasFile != false)
                {
                    poza.Value = Blob();
                }
                else
                {
                    poza.Value = null;
                }

                oCmd.Parameters.Add(nrC);
                oCmd.Parameters.Add(n);
                oCmd.Parameters.Add(p);
                oCmd.Parameters.Add(f);
                oCmd.Parameters.Add(sB);
                oCmd.Parameters.Add(sP);
                oCmd.Parameters.Add(pB);
                oCmd.Parameters.Add(tB);
                oCmd.Parameters.Add(bI);
                oCmd.Parameters.Add(i);
                oCmd.Parameters.Add(cas);
                oCmd.Parameters.Add(cass);
                oCmd.Parameters.Add(r);
                oCmd.Parameters.Add(vC);
                oCmd.Parameters.Add(poza);
                oCmd.ExecuteNonQuery();

                LabelPrincipalErr.Text = "Angajatul a fost adaugat cu succes!";
                LabelPrincipalErr.ForeColor = Color.Green;
                LabelRezultatAdaugare.Text = "Angajatul a fost adaugat cu succes!";
                LabelRezultatAdaugare.ForeColor = Color.Green;
                oCmd.Dispose();
            }
            catch (Exception ex)
            {
                LabelPrincipalErr.ForeColor = Color.Red;
                LabelPrincipalErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                LabelRezultatAdaugare.ForeColor = Color.Red;
                LabelRezultatAdaugare.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
            }
            finally
            {
                con.Clone();
            }
        }

        protected byte[] Blob()
        {
            pfile = pozaAngajat.PostedFile;
            byte[] data = new byte[pfile.ContentLength];
            pfile.InputStream.Read(data, 0, pfile.ContentLength);
            return data;
        }

        protected void salarBazaTB_TextChanged(object sender, EventArgs e)
        {
            TotalBrut_SiRestuCalcule();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["pozaAngajat"] = null;
                Session["postedFile"] = null;
                salarBazaTB.Text = 0.ToString();
                sporProcentTB.Text = 0.ToString();
                premiiBruteTB.Text = 0.ToString();
                totalBrutTB.Text = 0.ToString();
                casTB.Text = 0.ToString();
                cassTB.Text = 0.ToString();
                brutImpozabilTB.Text = 0.ToString();
                impozitTB.Text = 0.ToString();
                retineriTB.Text = 0.ToString();
                viratCardTB.Text = 0.ToString();

                try
                {
                    con.Open();
                    string cmd = "select casprocent, cassprocent, impozitprocent from procente where id=1";
                    oCmd = new OracleCommand(cmd, con);
                    oDr = oCmd.ExecuteReader();
                    if (oDr.HasRows)
                    {
                        oDr.Read();
                        cas = Int32.Parse(oDr["casprocent"].ToString().Trim());
                        cass = Int32.Parse(oDr["cassprocent"].ToString().Trim());
                        impozit = Int32.Parse(oDr["impozitprocent"].ToString().Trim());
                    }
                    oDr.Dispose();
                    oCmd.Dispose();
                    LabelPrincipalErr.Text = "";
                    LabelCAS.Text = "CAS (" + cas + "%)";
                    LabelCASS.Text = "CASS (" + cass + "%)";
                    LabelImpozit.Text = "Impozit (" + impozit + "%)";
                }
                catch (Exception ex)
                {
                    LabelPrincipalErr.ForeColor = Color.Red;
                    LabelPrincipalErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }

            //If first time page is submitted and we have file in FileUpload control but not in session
            // Store the values to SEssion Object
            if (Session["pozaAngajat"] == null && pozaAngajat.HasFile != false && Session["postedFile"] == null)
            {
                var img = Server.MapPath(ClientID + ".jpg");
                pozaAngajat.PostedFile.SaveAs(img);
                Session["pozaAngajat"] = ClientID + ".jpg";
                Session["postedFile"] = pozaAngajat.PostedFile;
                pozaAngajatImg.ImageUrl = ClientID + ".jpg";
                pozaAngajatImg.Attributes.Add("style", "visibility:visible;");

                //pfile = (HttpPostedFile)Session["postedFile"];
                //pfile.SaveAs(Server.MapPath("test.jpg"));
                //pozaAngajatImg.ImageUrl = "test.jpg";
            }
            // Next time submit and Session has values but FileUpload is Blank
            // Return the values from session to FileUpload
            else if (Session["pozaAngajat"] != null && pozaAngajat.HasFile == false && Session["postedFile"] != null)
            {
                pozaAngajatImg.ImageUrl = Session["pozaAngajat"].ToString();
                pozaAngajatImg.Attributes.Add("style", "visibility:visible;");

                //pfile = (HttpPostedFile)Session["postedFile"];
                //pfile.SaveAs(Server.MapPath("test.jpg"));
                //pozaAngajatImg.ImageUrl = "test.jpg";
            }
            // Now there could be another sictution when Session has File but user want to change the file
            // In this case we have to change the file in session object
            else if (pozaAngajat.HasFile != false)
            {
                var img = Server.MapPath(ClientID + ".jpg");
                pozaAngajat.PostedFile.SaveAs(img);
                Session["pozaAngajat"] = ClientID + ".jpg";
                Session["postedFile"] = pozaAngajat.PostedFile;
                pozaAngajatImg.ImageUrl = ClientID + ".jpg";
                pozaAngajatImg.Attributes.Add("style", "visibility:visible;");

                //pfile = (HttpPostedFile)Session["postedFile"];
                //pfile.SaveAs(Server.MapPath("test.jpg"));
                //pozaAngajatImg.ImageUrl = "test.jpg";
            }
        }
    }
}