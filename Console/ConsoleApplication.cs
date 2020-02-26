using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using TestToolsBase.Modell;
using System.Windows.Forms;
using System.IO;
using TestToolsBase.Sql;
using TestToolsBase.Properties;
using System.ComponentModell;
using TestToolsBase.ErrorHandling;
using TestToolsBase.Helper;

namespace TestToolsBase.Console
{
    public class ConsoleApplication
    {
        const int DEFAULT_HEIGHT = 42;
        const int DEFAULT_WIDTH = 120;
        
        
        /// <summary>
        /// - Sets minimal Console size(120x60) and buffer(3.000x60)
        /// - Setup Trace Listener
        /// -Write program arguments to trace (be avare about passwords...)
        /// </summary>
        public static void ProgramStating(string[] args, bool cancelEnabled_in)
        {
            TraceHelper.SetupListener(); //enable default logfile

            if(args != null)
                Trace.TraceInformation(string.Format("Program arguments: {0}", args.ItemsToString(" ")));

            try
            {
                if (SystemInformation.VirtualScreen.Width > 666) //do not increase wnd size in case of small monitor
                {
                    if (System.Console.WindowHeight < DEFAULT_HEIGHT)
                        System.Console.WindowHeight = DEFAULT_HEIGHT;

                    if (System.Console.WindowWidth < DEFAULT_WIDTH || System.Console.BufferWidth < DEFAULT_WIDTH)
                    {
                        System.Console.BufferWidth = DEFAULT_WIDTH;
                        System.Console.WindowWidth = DEFAULT_WIDTH;
                    }
                }

                if (System.Console.BufferHeight < 6000)
                    System.Console.BufferHeight = 6000;
            }
            catch (Exception ex)
            {
                Trace.TraceWarning( "Exception at ProgramStating: {0}", ex.ReportError(false));
            }

            if(cancelEnabled_in)
                System.Console.CancelKeyPress += new ConsoleCancelEventHandler(ConsoleApplication.DefaultCancelEvetnHandler); //enable cancel (and create log in that case)
        }

        /// <summary>
        /// -Informs user about the measure
        /// -Makes possible to save tracefile in case of not succesfull measure
        /// -Adds MeassureCollectionBase.DetailedTraceInfo to tracefile (if MeassureCollectionBase has been specified by argument)
        /// 
        /// Note: use 'return (int)exitCode;' at end of the main
        /// </summary>
        /// <param name="exitCode"></param>
        /// <param name="measures">for store the DetailedTraceInfo into the trace file. Can be null</param>
        /// <param name="errorSummary_in">Object containing summary of errors, ToSting is displayed in case of MeasureFailed. Value can be null</param>
        public static void ProgramEnd(ConsoleExitCode exitCode_in, MeasureCollectionBase measures_in, object errorSummary_in, bool noUserInteraction_in)
        {
            if (noUserInteraction_in && System.Console.KeyAvailable) //user pressed a key, 
            {
                noUserInteraction_in = false; //wait other (e.g. press t)
                ConsoleHelper.ClearInputBuffer();
            }

            System.Console.WriteLine();
            if (exitCode_in == ConsoleExitCode.MeasureOK)
            {
                AssertMeasureState(true);

                System.Console.ForegroundColor = ConsoleColor.Green;
                ConsoleHelper.InfoMessage(Resources.ConsoleApplication_ProgramEndOk);
            }
            else if (exitCode_in == ConsoleExitCode.MeasureFailed)
            {
                ConsoleHelper.ErrorMessage(Resources.ConsoleApplication_ProgramEndFailed);
                AssertMeasureState(false);

                if (errorSummary_in != null)
                {
                    System.Console.WriteLine();
                    ConsoleHelper.InfoMessage(Resources.ConsoleApplication_ProgramEndErrorSummary);
                    ConsoleHelper.InfoMessage(errorSummary_in.ToString());
                    ConsoleHelper.InfoMessage("-----------------------------------------------------------------------------------");
                }
            }
            else
            {
                //if(measures_in != null) SqlLog.TheLog.SaveLog("Measure Canceled");

                if (measures_in != null)
                    Trace.TraceInformation(measures_in.DetailedTraceInfo);

                ConsoleHelper.WarningMessage(Resources.ConsoleApplication_ProgramEndCanceled);
            }
            System.Console.ForegroundColor = ConsoleColor.Gray;

            //User requested trace:
            if (!noUserInteraction_in)
            {
                List<ConsoleMenuItem> items = new List<ConsoleMenuItem>();
                items.Add(new ConsoleMenuItem(Resources.QUIT, "0", null,
                    ConsoleColor.Yellow));
                items.Add(new ConsoleMenuItem(Resources.SHOW_MEASURE_INFO, "1", new ConsoleMenuItem.DoWorkDelegate(
                        delegate()
                        {
                            using (TestToolsBase.MeasureEnvironment.InfoForm f = new TestToolsBase.MeasureEnvironment.InfoForm())
                                f.ShowDialog();
                        }),
                    ConsoleColor.Gray));
                items.Add(new ConsoleMenuItem(Resources.SAVE_TRACE, "2", new ConsoleMenuItem.DoWorkDelegate(delegate() { TraceHelper.MarkTraceFileForStore("UserRequest"); }),
                    ConsoleColor.Gray));

                ConsoleMenu menu = new ConsoleMenu(null, null, true, false, 0, items);

                menu.DoMenu(0, null);
            }

            if (TraceHelper.HasTraceFileMarkedForStore)
            {
                if (measures_in != null)
                    Trace.TraceInformation(measures_in.DetailedTraceInfo);

                if (TraceHelper.TraceFileDirectory != string.Empty)
                {
                    System.Console.WriteLine();
                    ConsoleHelper.InfoMessage(Resources.ConsoleApplication_TracefileSavedInfo,
                        TraceHelper.TraceFileDirectory);
                }
            }
            Environment.Exit((int)exitCode_in);
        }

        private static void AssertMeasureState(bool state_in)
        {
            if ((MeasureCollectionBase.TheMeasures is ICheckableBase)
                && (MeasureCollectionBase.TheMeasures as ICheckableBase).CheckResult != state_in)
            {
                ConsoleHelper.ErrorMessage("Inkonzisztens mérés állapot. Értesítse a fejlesztést.");
                TraceHelper.MarkTraceFileForStore("AssertMeasureState_Failed");
            }
        }

        /// <summary>
        /// Asks user confirmation for cancel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static public void DefaultCancelEvetnHandler(object sender, ConsoleCancelEventArgs e)
        {

            int startPos =System.Console.CursorTop;
            if (ConsoleHelper.ConsoleMessage(Resources.ConsoleApplication_CancelQuestion, MessageBoxButtons.YesNo, DialogResult.No, false, null)
                == DialogResult.Yes)
            {
                if(MeasureCollectionBase.TheMeasures != null)
                    Trace.TraceInformation(MeasureCollectionBase.TheMeasures.DetailedTraceInfo);

                TraceHelper.MarkTraceFileForStore("UserCancelRequest"); //save log and exit
            }
            else
            {
                e.Cancel = true;
                ConsoleHelper.ClearArea(startPos,System.Console.CursorTop);
            }
        }
    }
}
