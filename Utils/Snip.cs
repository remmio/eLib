﻿namespace eLib.Utils
{
    class Snip
    {

        #region EF

        //Update-Database -ConfigurationTypeName eSchool.Core.Migrations.SchoolContext.Configuration
        //Update-Database -ConfigurationTypeName eSchool.Core.Migrations.EconomatContext.Configuration

        //       Add-Migration -ConfigurationTypeName eSchool.Core.Migrations.SchoolContext.Configuration "v "
        //       Add-Migration -ConfigurationTypeName eSchool.Core.Migrations.EconomatContext.Configuration "v "

        //Enable-Migrations -ContextTypeName SchoolContext -MigrationsDirectory Migrations\SchoolContext
        //Enable-Migrations -ContextTypeName EconomatContext -MigrationsDirectory Migrations\EconomatContext

        #endregion

        

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
        //  DisplayDateEnd="{Binding Source={x:Static system:DateTime.Today}, Mode=OneWay}"
        // <!--Validation.ErrorTemplate ="{DynamicResource ValidationErrorTemplate}" Validation.Error="Validation_Error" Text="{Binding UpdateSourceTrigger=PropertyChanged, Path=Name, ValidatesOnDataErrors=true, NotifyOnValidationError=true}"/>-->

        #endregion


        #region TASK

        //Stopwatch stopWatch = Stopwatch.StartNew ();

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



        #region ToolBar

        //<!--<ToolBar Style = "{StaticResource ToolBarStyle}" DockPanel.Dock="Top">
        //    <Grid HorizontalAlignment = "Stretch">
        //        <Grid.ColumnDefinitions>
        //            <ColumnDefinition/>
        //            <ColumnDefinition/>
        //            <ColumnDefinition/>
        //            <ColumnDefinition/>
        //            <ColumnDefinition/>
        //            <ColumnDefinition/>
        //        </Grid.ColumnDefinitions>
        //        <Button Content="&#xf015;" Grid.Column="0" Style="{DynamicResource ToolButtonStyle}" Click="StudentsViewBackButton_Click" />
        //        <Button Content = "&#xf055;" Grid.Column="1" Style="{DynamicResource ToolButtonStyle}" Name="_ADD_BUTON" Click="AddButon_Click" />
        //        <Button Content = "&#xf014;" Grid.Column="2" Style="{DynamicResource ToolButtonStyle}" Name="_DELETE_BUTTON" Click="DeleteButton_Click"/>
        //        <Button Content = "&#xf15d;" Grid.Column="3" Style="{DynamicResource ToolButtonStyle}" />
        //        <Button Content = "&#xf0dc;" Grid.Column="4" Style="{DynamicResource ToolButtonStyle}" />
        //        <Grid Grid.Column="5">
        //            <Grid.ColumnDefinitions>
        //                <ColumnDefinition />
        //                <ColumnDefinition Width = "40"/>
        //            </Grid.ColumnDefinitions>
        //            <TextBox Height= "20" Background= "Beige" Margin= "0,0,10,0" HorizontalAlignment= "Stretch" VerticalContentAlignment= "Center" BorderThickness= "0"/>
        //            <Button Content= "&#xf002;" Margin= "0,0,10,0" Style= "{DynamicResource ToolButtonStyle}" Grid.Column= "1"/>
        //        </Grid>
        //    </Grid>
        //</ToolBar>-->

        #endregion
    }
}
