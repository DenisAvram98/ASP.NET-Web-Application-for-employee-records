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
    public partial class WebForm2 : System.Web.UI.Page
    {
        OracleConnection con = new OracleConnection("DATA SOURCE = localhost:1521 / XE;PASSWORD=student;PERSIST SECURITY INFO=True;USER ID = STUDENT");
        OracleCommand oCmd;
        OracleDataReader oDr;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Autentificare"] != null)
            {
                if (Session["Autentificare"].ToString() == "autentificat")
                {
                    tabelProcenteDiv.Attributes.Add("style", "display:;");
                    autentificareDiv.Attributes.Add("style", "display:none;");
                    modificareParolaDiv.Attributes.Add("style", "display:;");
                }
            }
            else if (Session["Autentificare"] == null)
            {
                tabelProcenteDiv.Attributes.Add("style", "display:none;");
                autentificareDiv.Attributes.Add("style", "display:;");
                modificareParolaDiv.Attributes.Add("style", "display:none;");
                LabelAutentificareErr.Text = "";
            }

            parolaVecheTB.Attributes.Add("value", parolaVecheTB.Text.Trim());
            parolaNouaTB.Attributes.Add("value", parolaNouaTB.Text.Trim());
            confirmareParolaTB.Attributes.Add("value", confirmareParolaTB.Text.Trim());
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap; text-align:center; padding:4px;");
            }
        }

        protected string DecryptPassword()
        {
            string decryptPassword = null;
            con.Open();
            string cmd = "select Parola, CheieParola from Procente where ID=1";
            oCmd = new OracleCommand(cmd, con);
            oDr = oCmd.ExecuteReader();
            if (oDr.HasRows)
            {
                oDr.Read();
                string encryptedPassword = oDr["Parola"].ToString();
                int encryptionKey = Int32.Parse(oDr["CheieParola"].ToString());
                char p;
                for (int i = 0; i < encryptedPassword.Length; i++)
                {
                    p = encryptedPassword[i];
                    p = (char)(p ^ encryptionKey);
                    decryptPassword = decryptPassword + p;
                }
                return decryptPassword;
            }

            oDr.Dispose();
            oCmd.Dispose();
            return "";
        }

        protected void autentificareBtn_Click(object sender, EventArgs e)
        {
            if (parolaTB.Text.Trim() != "")
            {
                try
                {
                    string decryptPassword = DecryptPassword();
                    if (decryptPassword != "")
                    {
                        if (parolaTB.Text.Trim() == decryptPassword)
                        {
                            LabelAutentificareErr.Text = "Autentificarea a fost efectuata cu succes!";
                            LabelAutentificareErr.ForeColor = Color.Green;
                            Session["Autentificare"] = "autentificat";

                            tabelProcenteDiv.Attributes.Add("style", "display:;");
                            autentificareDiv.Attributes.Add("style", "display:none;");
                            modificareParolaDiv.Attributes.Add("style", "display:;");
                            autentificareBtn.Enabled = false;
                        }
                        else
                        {
                            LabelAutentificareErr.Text = "Parola introdusa este gresita!";
                            LabelAutentificareErr.ForeColor = Color.Red;
                            Session["Autentificare"] = null;

                            tabelProcenteDiv.Attributes.Add("style", "display:none;");
                            autentificareDiv.Attributes.Add("style", "display:;");
                            modificareParolaDiv.Attributes.Add("style", "display:none;");
                            autentificareBtn.Enabled = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LabelAutentificareErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                    LabelAutentificareErr.ForeColor = Color.Red;
                    Session["Autentificare"] = null;

                    tabelProcenteDiv.Attributes.Add("style", "display:none;");
                    autentificareDiv.Attributes.Add("style", "dipslay:;");
                    modificareParolaDiv.Attributes.Add("style", "display:none;");
                    autentificareBtn.Enabled = true;
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                LabelAutentificareErr.Text = "Nu ati introdus parola!";
                LabelAutentificareErr.ForeColor = Color.Red;
                Session["Autentificare"] = null;

                tabelProcenteDiv.Attributes.Add("style", "display:none;");
                autentificareDiv.Attributes.Add("style", "display:;");
                modificareParolaDiv.Attributes.Add("style", "display:none;");
                autentificareBtn.Enabled = true;
            }
        }

        protected void salvareParolaBtn_Click(object sender, EventArgs e)
        {
            string decryptPassword = null;
            if (Session["Autentificare"] != null)
            {
                if (Session["Autentificare"].ToString() == "autentificat")
                {
                    if (parolaVecheTB.Text.Trim() != "")
                    {
                        try
                        {
                            decryptPassword = DecryptPassword();
                        }
                        catch(Exception ex)
                        {
                            LabelParolaVecheErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                            LabelParolaVecheErr.ForeColor = Color.Red;
                        }
                        finally
                        {
                            con.Close();
                        }
                        if (decryptPassword != "")
                        {
                            if (parolaVecheTB.Text.Trim() == decryptPassword)
                            {
                                LabelParolaVecheErr.Text = "";
                                if (parolaNouaTB.Text.Trim()!="")
                                {
                                    if (parolaNouaTB.Text.Trim() != parolaVecheTB.Text.Trim())
                                    {
                                        if (parolaNouaTB.Text.Trim().Length >= 7)
                                        {
                                            LabelParolaNouaErr.Text = "";
                                            string newPassword = parolaNouaTB.Text.Trim();
                                            if (confirmareParolaTB.Text.Trim() != "")
                                            {
                                                string confirmPassword = confirmareParolaTB.Text.Trim();
                                                if (newPassword == confirmPassword)
                                                {
                                                    LabelConfirmareParoalErr.Text = "";
                                                    char p;
                                                    Random random = new Random();
                                                    int encryptionKey = random.Next(100, 1000);
                                                    string encryptedPassword = null;
                                                    for (int i = 0; i < confirmPassword.Length; i++)
                                                    {
                                                        p = confirmPassword[i];
                                                        p = (char)(p ^ encryptionKey);
                                                        encryptedPassword = encryptedPassword + p;
                                                    }

                                                    try
                                                    {
                                                        con.Open();
                                                        string cmd = "update Procente set Parola='" + encryptedPassword + "', CheieParola=" + encryptionKey + " where id=1";
                                                        oCmd = new OracleCommand(cmd, con);
                                                        oCmd.ExecuteNonQuery();
                                                        oCmd.Dispose();

                                                        LabelSalvareParolaErr.Text = "Parola a fost modificata cu succes! <br />In 5 secunde veti fi redirectionati la pagina de autentificare...";
                                                        LabelSalvareParolaErr.ForeColor = Color.Green;
                                                        Session["Autentificare"] = null;
                                                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() { window.location.replace('ModificareProcente.aspx') }, 5000);", true);
                                                        autentificareBtn.Enabled = true;
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        LabelSalvareParolaErr.Text = "Nu se poate realiza conexiunea la baza de date: " + ex.Message;
                                                        LabelSalvareParolaErr.ForeColor = Color.Red;
                                                    }
                                                    finally
                                                    {
                                                        con.Close();
                                                    }
                                                }
                                                else
                                                {
                                                    LabelConfirmareParoalErr.Text = "Parola noua si cofirmarea parolei nu corespund!";
                                                    LabelConfirmareParoalErr.ForeColor = Color.Red;
                                                }
                                            }
                                            else
                                            {
                                                LabelConfirmareParoalErr.Text = "Nu ati confirmat parola noua!";
                                                LabelConfirmareParoalErr.ForeColor = Color.Red;
                                            }
                                        }
                                        else
                                        {
                                            LabelParolaNouaErr.Text = "Parola noua trebuie sa fie alcatuita din minim 7 caractere, cifre, etc.";
                                            LabelParolaNouaErr.ForeColor = Color.Red;
                                        }
                                    }
                                    else
                                    {
                                        LabelParolaNouaErr.Text = "Parola noua trebuie sa fie diferita de vechea parola!";
                                        LabelParolaNouaErr.ForeColor = Color.Red;
                                    }
                                }
                                else
                                {
                                    LabelParolaNouaErr.Text = "Nu ati introdus parola noua!";
                                    LabelParolaNouaErr.ForeColor = Color.Red;
                                }
                            }
                            else
                            {
                                LabelParolaVecheErr.Text = "Parola veche este gresita!";
                                LabelParolaVecheErr.ForeColor = Color.Red;
                            }
                        }
                    }
                    else
                    {
                        LabelParolaVecheErr.Text = "Nu ati introdus parola veche!";
                        LabelParolaVecheErr.ForeColor = Color.Red;
                    }
                }
            }
        }

        protected void iesireBtn_Click(object sender, EventArgs e)
        {
            Session["Autentificare"] = null;
            Response.Redirect("ModificareProcente.aspx");
        }
    }
}