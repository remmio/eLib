using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLib
{
    class Snip
    {

        //Enable-Migrations -ContextTypeName SchoolContext -MigrationsDirectory Migrations\SchoolContext
        //Enable-Migrations -ContextTypeName EconomatContext -MigrationsDirectory Migrations\EconomatContext



        //Add-Migration -ConfigurationTypeName DataService.Migrations.SchoolContext.Configuration "InitialDatabaseCreation"
        //Add-Migration -ConfigurationTypeName DataService.Migrations.EconomatContext.Configuration "InitialDatabaseCreation"

        //Update-Database -ConfigurationTypeName DataService.Migrations.SchoolContext.Configuration
        //Update-Database -ConfigurationTypeName DataService.Migrations.EconomatContext.Configuration



        //Stopwatch stopWatch = Stopwatch.StartNew ();



        #region WPF RELATIVE SOURCE BINDING

        //        <Canvas Name = "Parent0" >
        //    < Border Name="Parent1"
        //             Width="{Binding RelativeSource={RelativeSource Self},
        //             Path=Parent.ActualWidth
        //    }"
        //             Height="{Binding RelativeSource={RelativeSource Self},
        //             Path=Parent.ActualHeight
        //}">
        //        <Canvas Name = "Parent2" >
        //            < Border Name="Parent3"
        //            Width="{Binding RelativeSource={RelativeSource Self},
        //           Path=Parent.ActualWidth}"
        //           Height="{Binding RelativeSource={RelativeSource Self},
        //              Path=Parent.ActualHeight}">
        //               <Canvas Name = "Parent4" >
        //               < TextBlock FontSize="16" 
        //               Margin="5" Text="Display the name of the ancestor"/>
        //               <TextBlock FontSize = "16"
        //                 Margin="50" 
        //            Text="{Binding RelativeSource={RelativeSource  
        //                       FindAncestor,
        //                       AncestorType={x:Type Border}, 
        //                       AncestorLevel=2},Path=Name}" 
        //                       Width="200"/>
        //                </Canvas>
        //            </Border>
        //        </Canvas>
        //     </Border>
        //   </Canvas>        
        // <!--Validation.ErrorTemplate ="{DynamicResource ValidationErrorTemplate}" Validation.Error="Validation_Error" Text="{Binding UpdateSourceTrigger=PropertyChanged, Path=Name, ValidatesOnDataErrors=true, NotifyOnValidationError=true}"/>-->

        #endregion


        #region TASK

        //new Task(() =>
        //{
        //    Dispatcher.BeginInvoke(new Action(GetPatternData));
        //}).ContinueWith(delegate
        //{
        //    if (_isAdd)
        //    {
        //        DisplayDefault();
        //    }
        //    else
        //    {
        //        Display(App.DataS.Pedagogy.Inscriptions.GetInscriptionById(new Guid(inscriptionToOpen)));
        //    }
        //}).Start(); 

        //void Home ( )
        //{
        //    var firstTask = new Task (GetTheStudents);
        //    var secondTask = firstTask.ContinueWith (( t ) => DisplayTheStudents ());
        //    firstTask.Start ();
        //}

        //Db = new Service ();
        //new Thread(() => Db = new Service()).Start();

        //void GetTheStudents ( )
        //{
        //    MessageBox.Show ("started");
        //    StudentsBuff = App.Db.GetAllStudents ();
        //}
        //void DisplayTheStudents ( )
        //{
        //    MessageBox.Show ("finished");
        //    this.Studentslist.ItemsSource = StudentsBuff;
        //    this.Studentslist.Items.Refresh ();
        //    MessageBox.Show ("Affter finished");
        //}

        //Dispatcher.BeginInvoke(new Action(() => { ClassWeekSchedule.UpdateData(OpenedClass.CLASSE_ID); }));
        //Dispatcher.BeginInvoke(new Action(() => { MatieresList.ItemsSource = App.ModelS.GetClassMatieresCards(OpenedClass.CLASSE_ID); }));
        //Dispatcher.BeginInvoke(new Action(() => { StudentsList.ItemsSource = App.DataS.GetClassStudents(OpenedClass.CLASSE_ID); }));

        // test from web

        #endregion





























    }
}
