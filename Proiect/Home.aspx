<%@ Page Title="" Language="C#" MasterPageFile="~/Proiect.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Proiect.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .blue {
            color: blue;
        }

        .green {
            color: green;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="border-left-color:cornflowerblue; border-right-color:cornflowerblue; border-bottom-color:cornflowerblue; border-top-color:aliceblue; border-style: solid; border-radius: 0px 0px 12px 12px; padding: 0px 15px 15px 15px;">
        <div style="padding-left: 20px;">
            <h1>Bine ati venit!</h1>
        </div>

        <br />
        <h3>*Modul de operare al programului:</h3>

        <p class="lead" style="font-size:19px;">
            - Meniul programului contine 4 optiuni principale ('Home', <span class="blue">'Introducere date'</span>, <span class="blue">'Tiparire'</span>, <span class="blue">'Modificare procente'</span>),
        dintre care 2 (<span class="blue">'Introducere date'</span> si <span class="blue">'Tiparire'</span>) mai contin si alte functionalitati.
        <br />
            <br />
            - Pentru <b>operarea asupra datelor angajatilor</b> se acceseaza optiunea <span class="blue">'Introducere date'</span> care contine urmatoarele functionalitati:
        <br />
            &emsp;&emsp;<b>*</b> <b><span class="green">'Actualizare date'</span></b> - <u>actualizarea informatiei despre angajati</u>;
        <br />
            &emsp;&emsp;<b>*</b> <b><span class="green">'Adaugare angajati'</span></b> - <u>permite adaugarea unur noi angajati</u>;
        <br />
            &emsp;&emsp;<b>*</b> <b><span class="green">'Stergere angajati'</span></b> - <u>permite stergerea angajatului</u>.
        <br />
            <br />
            - Pentru <b>generarea de diferite rapoarte</b> se acceseaza optiunea <span class="blue">'Tiparire'</span> care poate genera urmatoarele rapoarte:
        <br />
            &emsp;&emsp;<b>*</b> <b><span class="green">'Stat plata'</span></b> - <u>afiseaza pe ecran statul de plata pentru toti angajatii si ofera posibilitatea de tiparire a acestuia la imprimanta</u>;
        <br />
            &emsp;&emsp;<b>*</b> <b><span class="green">'Fluturasi'</span></b> - <u>afiseaza pe ecran fluturasi pentru toti angajati (sau pentru un anumit angajat cautat) si ofera posibilitatea de tiparire a acestora la imprimanta</u>.
        <br />
            <br />
            - Pentru <b>modificarea procentelor</b> precum <b>CAS</b>, <b>CASS</b> si <b>IMPOZIT</b> se acceseaza optiunea <span class="blue">'Modificare procente'</span> care este protejata cu o parola de protectie.
        <br />
            <br />
        </p>

    </div>
</asp:Content>
