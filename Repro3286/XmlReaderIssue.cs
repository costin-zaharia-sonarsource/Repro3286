using System.Xml;

namespace ValueTupleRepro
{
    class XmlReaderIssue
    {
        // https://jira.sonarsource.com/browse/RSPEC-5656
        // S2755
        public void Noncompliant()
        {
            XmlDocument parser = new XmlDocument(); // Noncompliant: XmlDocument is not safe by default
            parser.LoadXml("xxe.xml");

            // .NET Framework 4.5.2+
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.XmlResolver = new XmlUrlResolver();
            XmlReader reader = XmlReader.Create("xxe.xml", settings); // Noncompliant: XmlReader is safe by default and becomes unsafe if DtdProcessing = Parse and XmlResolver is not null
            while (reader.Read())
            { }

        }
    }
}