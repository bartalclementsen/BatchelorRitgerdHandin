using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace KlassaGenerering
{
    public partial class MainForm : Form
    {
        TypeDefinitionList cdl;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            cdl = new TypeDefinitionList();
            cdl.LoadData();
        }

        private void btnDelphiEnums_Click(object sender, EventArgs e)
        {
            tbResult.Text = cdl.GenerateDelphiEnumStreaming();
        }

        private void btnDelphiObjects_Click(object sender, EventArgs e)
        {
            tbResult.Text = cdl.GenerateDelphiObjectStream();
        }

        private void btnCSEnums_Click(object sender, EventArgs e)
        {
            tbResult.Text = cdl.GenerateCSharpEnumStreaming();
        }

        private void btnCSObjects_Click(object sender, EventArgs e)
        {
            tbResult.Text = cdl.GenerateCSharpObjectStream();
        }

        private void btnCSUIObjects_Click(object sender, EventArgs e)
        {
            tbResult.Text = cdl.GenerateCSharpUIObjects();
        }

        private void btnSaveDefinition_Click(object sender, EventArgs e)
        {
            if (cdl == null) { 
                cdl = new TypeDefinitionList();
                cdl.LoadData();
            }
            XElement xml = cdl.GetAsXml();
            tbResult.Text = xml.ToString();
            xml.Save("C:\\temp\\ClassDefinitions.xml"); 
        }

        private void btnLoadDefinition_Click(object sender, EventArgs e)
        {
            cdl = new TypeDefinitionList();
            XElement xml = XElement.Load("C:\\temp\\ClassDefinitions.xml");
            cdl.SetAsXml(xml);

            tbResult.Text = cdl.GetAsXml().ToString();
        }

        private void ProtoButton_Click(object sender, EventArgs e)
        {
            tbResult.Text = cdl.GenerateProto();
        }
    }
}
