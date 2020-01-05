using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace A20_Ex01_Daniel_203105572_Dor_206318537
{
	public partial class MonitorForm : Form
	{
		public MonitorForm()
		{
			InitializeComponent();
		}

		public string MonitorText
		{
			get { return textBoxMonitor.Text; }
			set { textBoxMonitor.Text = value; }
		}
	}
}