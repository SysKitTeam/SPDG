using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Acceleratio.SPDG.Generator;
using System.Xml.Serialization;
using System.IO;

namespace Acceleratio.SPDG.UI
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServerGeneratorDefinition definition = new ServerGeneratorDefinition();
            definition.SharePointURL = "http://sp-kreso";
            definition.CredentialsOfCurrentUser = false;
            definition.Username = "kresimir.korovljevic";
            definition.Password = "XXXX";
            definition.Domain = "acceleratio";

            XmlSerializer serializer = new XmlSerializer(typeof(ServerGeneratorDefinition));
            using (TextWriter writer = new StreamWriter("SPDG_Definition.xml"))
            {
                serializer.Serialize(writer, definition);
            } 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(ServerGeneratorDefinition));
            TextReader reader = new StreamReader("SPDG_Definition.xml");
            object obj = deserializer.Deserialize(reader);
            ServerGeneratorDefinition XmlData = (ServerGeneratorDefinition)obj;
            reader.Close();
        }
    }
}
