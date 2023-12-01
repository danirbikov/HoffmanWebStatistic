
using ServicesWebAPI.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace HoffmanWebstatistic.Services
{
    public class XSDValidator
    {
        public bool validateXSD(string xmlFilePath, byte[] xsdBytes)
        {
            bool validationResult = true;

            XmlSchemaSet schemaSet = new XmlSchemaSet();
            using (MemoryStream xsdStream = new MemoryStream(xsdBytes))
            {
                schemaSet.Add("MOM.Production", XmlReader.Create(xsdStream));
            }

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = schemaSet;

            settings.ValidationEventHandler += (sender, e) =>
            {
                validationResult = false;
                LoggerNLOG.LogWarning("Структура XML файла некорреткна при валидации ХМЛ (MES): " + xmlFilePath + "\n");
            };

            using (XmlReader reader = XmlReader.Create(xmlFilePath, settings))
            {
                try
                {                    
                    while (reader.Read()) { }
                }
                catch (XmlException ex)
                {
                    LoggerNLOG.LogWarning("Структура XML файла некорреткна при валидации ХМЛ (MES): " + xmlFilePath + "\n" + ex.ToString());
                    return false;
                }
            }

            return validationResult;

        }
    }
}