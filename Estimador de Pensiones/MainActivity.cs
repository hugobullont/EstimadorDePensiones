
using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Text;



namespace Estimador_de_Pensiones
{
    [Activity(Label = "Estimador", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        int ingresoInt;
        int idAFP;
        int idSex;
        int idFondo60;
        int idFondo65;
        double erre1;
        double erre2;
        double crucal;
        double fondodoublefinal;
        double diasDouble; 
        double treadoublefinal;
        double vandoublefinal;
        double pensiondoublefinal;
        int mesinterino;
        string nameFondo60;
        string nameFondo65;
        string nameSex;
        string nameAFP;
        string ingreso;
        string fecha;
        string dias;
        string treaFinal;
        string vanFinal;
        string pensionFinal;
        string fondoFinal;

        //PantallaFinal
        TextView IngresoBruto;
        TextView TREA;
        TextView VAN;
        TextView Pension;
        TextView Fondo;
        TextView VerAFP;

        //OtrasVariables
        DateTime fechaDate;
        DateTime FechaHoy = DateTime.Now;
        TimeSpan DifFechas;
        EditText IngresoFecha;
        EditText ingreso2;
        RadioGroup RadFondo60;
        RadioGroup RadFondo65;
        RadioButton SelectedFondo60;
        RadioButton SelectedFondo65;
        RadioGroup RadAFP;
        RadioButton SelectedAFP;
        RadioGroup RadSexo;
        RadioButton SelectedSex;
        

        double R1(int tipofondo1)
        {
            double r1=0;

            switch (tipofondo1)
            {
                case 0:
                    {
                        r1 = ((Math.Pow(1 + 0.02, (1.0 / 12.0))) - 1.0);
                        break;
                    }
                case 1:
                    {
                        r1 = ((Math.Pow(1 + 0.03, (1.0 / 12.0))) - 1.0);
                        break;
                    }
                case 2:
                    {
                        r1 = ((Math.Pow(1 + 0.05, (1.0 / 12.0))) - 1.0);
                        break;
                    }
                case 3:
                    {
                        r1 = ((Math.Pow(1 + 0.07, (1.0 / 12.0))) - 1.0);
                        break;
                    }
            }
            return r1;
        }

        double R2(int tipofondo2)
        {
            double r2=0;
            switch (tipofondo2)
            {
                case 0:
                    {
                        r2 = ((Math.Pow(1 + 0.02, (1.0 / 12.0))) - 1.0);
                        break;
                    }
                case 1:
                    {
                        r2 = ((Math.Pow(1 + 0.03, (1.0 / 12.0))) - 1.0);
                        break;
                    }
                case 2:
                    {
                        r2 = ((Math.Pow(1 + 0.05, (1.0 / 12.0))) - 1.0);
                        break;
                    }
                
            }
            return r2;

        }

        double calcSA(double RA, int NA, double r1, double r2, int n1)
        {
            double SA=0;
            int AV = 0;
            int n2 = 60;
            double DC = 1;
            int a = 10;
            int nen1 = 0;
            nen1 = 60 * 12 - n1;




            double pow1 = (((Math.Pow(1 + r1, nen1) - 1) / r1) * (Math.Pow(1 + r2, n2)) + ((Math.Pow(1 + r2, n2) - 1) / r2));
            double b = ((a / 100.0) * RA) + AV;
            SA = DC * (NA / 12.0) * b * pow1;
           

            return SA;
        }

        double calcVAN(double pension, double trea, int edad)
        {
            double van1=0;
            double van2=0;
            double treaa = trea / 100.0;
            for (int i = 1; i <= 240; i++)
            {
                van1 = van1 + (pension / (Math.Pow(1 + treaa, i)));
            }

            van2 = van1 * ((Math.Pow(1 + treaa, (780 - edad)) - 1) / (treaa * (Math.Pow(1 + treaa, (780 - edad)))));

            return van2;
        }

        double calcTREA(double _r1, double _r2)
        {
            double trea;
            double r1 = 0;
            double r2 = 0;
            r1 = (Math.Pow(1 + _r1, 12)) - 1;
            r1 = (Math.Pow(1 + _r2, 12)) - 1;
            trea = ((r1 + r2) / 2) * 100;
            return trea;
        }

        double CRU(int afp, int genero)
        {
            double cru=0;
            if (genero == 1)
            {
                switch (afp)
                {
                    case 0:
                        {
                            cru = 173.62;
                            break;
                        }
                    case 1:
                        {
                            cru = 173.34;
                            break;
                        }
                    case 2:
                        {
                            cru = 191.12;
                            break;
                        }
                    case 3:
                        {
                            cru = 175.44;
                            break;
                        }
                    default:
                        cru = 173.62;
                        break;
                }
            }
            else
                if (genero == 0)
            {
                switch (afp)
                {
                    case 0:
                        {
                            cru = 176.91;
                            break;
                        }
                    case 1:
                        {
                            cru = 177.56;
                            break;
                        }
                    case 2:
                        {
                            cru = 194.43;
                            break;
                        }
                    case 3:
                        {
                            cru = 179.67;
                            break;
                        }
                    default:
                        cru = 176.91;
                        break;
                }
            }
            return cru;
        }

        double calcPension(double _cru, double _SA)
        {
            double PensionDeJubilacion;

            PensionDeJubilacion = _SA / _cru;

            return PensionDeJubilacion;
        }



        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += Button1_Click;




        }

        private void Button4_Click(object sender, EventArgs e)
        {

            //Botones Fondos!!!

            RadFondo60 = FindViewById<RadioGroup>(Resource.Id.radio60);
            SelectedFondo60 = FindViewById<RadioButton>(RadFondo60.CheckedRadioButtonId);
            nameFondo60 = SelectedFondo60.Text;
            RadFondo65 = FindViewById<RadioGroup>(Resource.Id.radio65);
            SelectedFondo65 = FindViewById<RadioButton>(RadFondo65.CheckedRadioButtonId);
            nameFondo65 = SelectedFondo65.Text;

            switch (nameFondo60)
            {
                case "Fondo 0":
                    idFondo60 = 0;
                    break;
                case "Fondo 1":
                    idFondo60 = 1;
                    break;
                case "Fondo 2":
                    idFondo60 = 2;
                    break;
                case "Fondo 3":
                    idFondo60 = 3;
                    break;
            }

            switch (nameFondo65)
            {
                case "Fondo 0":
                    idFondo65 = 0;
                    break;
                case "Fondo 1":
                    idFondo65 = 1;
                    break;
                case "Fondo 2":
                    idFondo65 = 2;
                    break;
                
            }

            erre1 = R1(idFondo60);
            erre2 = R2(idFondo65);
            fondodoublefinal=Math.Round(calcSA(ingresoInt, 12, erre1, erre2, mesinterino),2);
            treadoublefinal=Math.Round(calcTREA(erre1, erre2),2);
            crucal=CRU(idAFP, idSex);
            pensiondoublefinal=Math.Round(calcPension(crucal, fondodoublefinal),2);
            vandoublefinal = Math.Round(calcVAN(pensiondoublefinal, treadoublefinal, mesinterino),2);
            fondoFinal = Convert.ToString(fondodoublefinal);
            treaFinal = Convert.ToString(treadoublefinal);
            pensionFinal = Convert.ToString(pensiondoublefinal);
            vanFinal = Convert.ToString(vandoublefinal);

            SetContentView(Resource.Layout.Page5);
            IngresoBruto = FindViewById<TextView>(Resource.Id.viewIngreso);
            VerAFP = FindViewById<TextView>(Resource.Id.viewAFP);
            TREA = FindViewById<TextView>(Resource.Id.viewTREA);
            VAN = FindViewById<TextView>(Resource.Id.viewVAN);
            Pension = FindViewById<TextView>(Resource.Id.viewPension);
            Fondo = FindViewById<TextView>(Resource.Id.viewFondo);
            IngresoBruto.Text = ingreso;
            TREA.Text = treaFinal;
            VAN.Text = vanFinal;
            Pension.Text = pensionFinal;
            Fondo.Text = fondoFinal;
            VerAFP.Text = nameAFP;

            Button button5 = FindViewById<Button>(Resource.Id.buttonFin);
            button5.Click += Button5_Click;


        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }


        private void Button3_Click(object sender, EventArgs e)
        {
            RadAFP = FindViewById<RadioGroup>(Resource.Id.radioAFP);
            SelectedAFP = FindViewById<RadioButton>(RadAFP.CheckedRadioButtonId);
            nameAFP = SelectedAFP.Text;

            //Integra=0 Profuturo=1 Prima=2 HABITAT=3

            switch (nameAFP)
            {
                case "Integra":idAFP = 0;
                    break;
                case "Profuturo":
                    idAFP = 1;
                    break;
                case "PRIMA":
                    idAFP = 2;
                    break;
                case "HABITAT":
                    idAFP = 3;
                    break;
            }
            
            SetContentView(Resource.Layout.Page4);
            Button button4 = FindViewById<Button>(Resource.Id.button4);
            button4.Click += Button4_Click;
        }





        private void Button2_Click(object sender, EventArgs e)
        {

            RadSexo = FindViewById<RadioGroup>(Resource.Id.RadSex);
            SelectedSex= FindViewById<RadioButton>(RadSexo.CheckedRadioButtonId);
            nameSex = SelectedSex.Text;

            switch (nameSex)
            {
                case "Hombre":
                    idSex = 1;
                    break;
                case "Mujer":
                    idSex = 1;
                    break;
                
            }



            ingreso2 = FindViewById<EditText>(Resource.Id.editIngresoBruto);
            ingreso = ingreso2.Text;
            ingresoInt = Convert.ToInt32(ingreso);

            IngresoFecha = FindViewById<EditText>(Resource.Id.editFechaNac);
            fecha = IngresoFecha.Text;
            fechaDate = Convert.ToDateTime(fecha);

            DifFechas = FechaHoy - fechaDate;

            dias = Convert.ToString(DifFechas);
            string[] dates = dias.Split(':');
            diasDouble = Convert.ToDouble(dates[0]);
            mesinterino = (int)diasDouble / 30;

            SetContentView(Resource.Layout.Page3);
            Button button3 = FindViewById<Button>(Resource.Id.button3);
            button3.Click += Button3_Click;
        }

       

        private void Button1_Click(object sender, EventArgs e)
        {


            SetContentView(Resource.Layout.Page2);

            Button button2 = FindViewById<Button>(Resource.Id.button2);
            button2.Click += Button2_Click;
        }





    }
}

