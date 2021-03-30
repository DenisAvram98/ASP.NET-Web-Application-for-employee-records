using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proiect
{
    public partial class Proiect : System.Web.UI.MasterPage
    {
        static string[] imgArray;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LabelDataOra.Text = DateTime.Now.ToString();
                Image1.ImageUrl = "~/Poze/86938.jpg";
                Image2.ImageUrl = "~/Poze/PIA23689.jpg";

                DirectoryInfo directory = new DirectoryInfo(@"E:\Master - Anul 1 Sem1\TPW\Proiect\Proiect\Proiect\Poze\");
                FileInfo[] files = directory.GetFiles("*.jpg");
                imgArray = new string[files.Length];
                int i = 0;
                foreach (FileInfo file in files)
                {
                    imgArray[i++] = file.Name;
                }

                string activePage = Request.RawUrl;
                if (activePage.Contains("Home"))
                {
                    pageHome.Attributes.Add("class", "active");
                }
                else if (activePage.Contains("ActualizareDate"))
                {
                    pageInDate.Attributes.Add("class", "active");
                    pageInActualizareDate.Attributes.Add("class", "active");
                }
                else if (activePage.Contains("AdaugareAngajati"))
                {
                    pageInDate.Attributes.Add("class", "active");
                    pageInAdaugareAngajati.Attributes.Add("class", "active");
                }
                else if (activePage.Contains("StergereAngajati"))
                {
                    pageInDate.Attributes.Add("class", "active");
                    pageInStergereAngajati.Attributes.Add("class", "active");
                }
                else if (activePage.Contains("StatPlata"))
                {
                    pageTiparire.Attributes.Add("class", "active");
                    pageTiparireStatePlata.Attributes.Add("class", "active");
                }
                else if (activePage.Contains("Fluturasi"))
                {
                    pageTiparire.Attributes.Add("class", "active");
                    pageTiparireFluturasi.Attributes.Add("class", "active");
                }
                else if (activePage.Contains("ModificareProcente"))
                {
                    pageModificareProcente.Attributes.Add("class", "active");
                }
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            LabelDataOra.Text = DateTime.Now.ToString();
            string imgUrl1 = Image1.ImageUrl.ToString();
            string imgUrl2 = Image2.ImageUrl.ToString();

            Random random = new Random();
            bool check = false;
            bool check2 = false;

            while (check == false)
            {

                int i = random.Next(0, imgArray.Length - 1);
                if (imgUrl1 != "~/Poze/" + imgArray[i])
                {
                    if ("~/Poze/" + imgArray[i] != imgUrl2)
                    {
                        Image1.ImageUrl = "~/Poze/" + imgArray[i];
                        check = true;

                        while (check2 == false)
                        {
                            int j = random.Next(0, imgArray.Length - 1);
                            if (j != i)
                            {
                                if (imgUrl2 != "~/Poze/" + imgArray[j])
                                {
                                    Image2.ImageUrl = "~/Poze/" + imgArray[j];
                                    check2 = true;
                                }
                                else
                                {
                                    check2 = false;
                                }
                            }
                            else
                            {
                                check2 = false;
                            }
                        }
                    }
                    else
                    {
                        check = false;
                    }
                }
                else
                {
                    check = false;
                }
            }
        }
    }
}