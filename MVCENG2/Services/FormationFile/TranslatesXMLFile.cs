using System.Xml.Serialization;
using System;
using HoffmanWebstatistic.Models.SerializerModels;
using System.Xml.Linq;
using HoffmanWebstatistic.Models.Hoffman;
using System.Text;
using HoffmanWebstatistic.Repository;
using HoffmanWebstatistic.Services.InteractionStand;

namespace HoffmanWebstatistic.Services.FormationFile
{
    public class TranslatesXMLFile
    {

        public void FormationXMLFileForStands(IEnumerable<Translate> translates)
        {
            XDocument xdoc = new XDocument();

            XElement TranslateXML = new XElement("Entries");


            foreach (Translate translate in translates)
            {
                XElement E_Element = new XElement("E");

                XAttribute translateKeyAttr = new XAttribute("key", translate.EngVariant);
                E_Element.Add(translateKeyAttr);

                XAttribute translateValueAttr = new XAttribute("value", translate.RusVariant);
                E_Element.Add(translateValueAttr);

                TranslateXML.Add(E_Element);
            }

            TranslateOperation translateOperation = new TranslateOperation();

            xdoc.Add(TranslateXML);
            xdoc.Save(translateOperation.translationFilePathInProject);

        }
    }
}
