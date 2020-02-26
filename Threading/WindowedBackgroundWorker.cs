using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModell;
using System.Linq;
using System.Text;
using System.Windows;
using TestToolsBase.GUI;
using System.Windows.Forms.VisualStyles;
using TestToolsBase.GUI.ProgressWindows;
using System.Windows.Forms;
using System.Windows.Threading;

namespace TestToolsBase.Threading
{
    public class WindowedBackgroundWorker : BackgroundWorker
    {
        #region Fields and Properties

        ProgressWindow _pw;
        private Window _owner;

        //private  Window _owner;

        //public Window Owner
        //{
        //    get { return _owner; }
        //    set
        //    {
        //        if (_owner != value)
        //        {
        //            _owner = value;
        //        }
        //    }
        //}

        #endregion Fields and Properties

        private Form progressForm;

        public Form ProgressForm
        {
            get { return ProgressForm; }
            set { ProgressForm = value; }
        }


        #region Constructors

        public WindowedBackgroundWorker() : base() { }

        public WindowedBackgroundWorker(Window owner_in)
            : base()
        {

            _owner = owner_in;
        }

        #endregion Constructors

        #region Event Handlers

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            DispatcherOperation op = System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
              {
                  _pw = new ProgressWindow(_owner);
                  ProgressForm = new Form();
                  ProgressForm.Controls.Add((Control)_pw);
                  if (this.WorkerSupportsCancellation)
                      ProgressForm.Closed += ProgressForm_Closed;
                  else
                      _pw.btCancel.IsEnabled = false;

                  ProgressForm.Show();
              }));
            base.OnDoWork(e);
        }

        private void ProgressForm_Closed(object sender, EventArgs e)
        {
            this.CancelAsync();
        }

        public void EventHandler(object progressForm_Closed)
        {
            this.CancelAsync();
        }

        protected override void OnProgressChanged(ProgressChangedEventArgs e)
        {
            if (e != null)
            {
                _pw.ChangeProgressBarRemotely(e.ProgressPercentage);
                if (e.UserState != null)
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        _pw.Text = e.UserState.ToString();
                    }));
            }
            base.OnProgressChanged(e);
        }

        protected override void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
        {
            base.OnRunWorkerCompleted(e);
            System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                ProgressForm.Close();
            }));
        }

        #endregion Event Handlers

        private void InitializeComponent()
        {

        }
    }
}