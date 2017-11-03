using System;
using System.Xml;
using System.IO;

namespace GitVersionTree
{
	public class PortableConfiguration : IConfiguration
	{
		private const String FILE_NAME = "config.xml";
		private const String DEFAULT_SECTION = "configuration";

		XmlDocument xmlDoc = new XmlDocument();
		XmlNode rootNode;

		public PortableConfiguration ()
		{
			createFile ();
		}

		private void createFile(){
			String path = getFilePath ();
			if (!File.Exists (path)) {				
				rootNode = xmlDoc.CreateElement (DEFAULT_SECTION);
				xmlDoc.AppendChild (rootNode);
				xmlDoc.Save (path);
			}
		}

		private string getFilePath(){
			return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FILE_NAME);
		}

		private string getDefaultSection(string Section){
			String newSection = Section; 
			if (newSection == "")
				newSection = DEFAULT_SECTION;
			return newSection;
		}
			
		public string Read(string Name, string Section = ""){
			string newSection = getDefaultSection (Section);

			XmlNode newNode = null;

			string ret = "";

			if (File.Exists (FILE_NAME)) {
				xmlDoc.Load (FILE_NAME);
				newNode = xmlDoc.SelectSingleNode ("//"+ newSection +"/" + Name);
				if (newNode != null)
					ret = newNode.InnerText;
			} 
			return ret;
		}

		public void Write(string Name, string Value, string Section = ""){

			string newSection = getDefaultSection (Section);

			XmlNode newNode = null;
			if (File.Exists (FILE_NAME)) {
				xmlDoc.Load (FILE_NAME);
				newNode = xmlDoc.SelectSingleNode ("//"+ newSection +"/" + Name);
			} 

			if (newNode == null) {
				newNode = xmlDoc.CreateElement (Name);
				rootNode = xmlDoc.SelectSingleNode ("//" + newSection);
				rootNode.AppendChild(newNode);
			}
				
			newNode.InnerText = Value;
			xmlDoc.Save(FILE_NAME);
		}
	}
}

