using System.Xml.Serialization;
using System;
using HoffmanWebstatistic.Models.SerializerModels;
using System.Xml.Linq;
using HoffmanWebstatistic.Models.General;
using HoffmanWebstatistic.Models.Hoffman;
using System.Text;
using HoffmanWebstatistic.Repository;

namespace HoffmanWebstatistic.Services
{
    public static class OperatorsXMLFile
    {

        

        public static void FormationAndSendXMLFileForStands(StandRepository _standRepository, List<Operator> operators)
        {

            XDocument xdoc = new XDocument();

            XElement operatorsXML = new XElement("STANDPOOL");
            int operatorCount = 0;
            
            foreach (Stand stand in _standRepository.GetAll()) 
            {
                XElement xmlElement = new XElement("stand");
                XAttribute standNameAttr = new XAttribute("number", CryptData(stand.StandName));
                xmlElement.Add(standNameAttr);
                operatorsXML.Add(xmlElement);

                foreach (Operator @operator in operators)
                {
                    operatorCount++;
                    XElement xmlElement2 = new XElement("user");
                    xmlElement.Add(xmlElement2);
                    XAttribute operatorNameAttr = new XAttribute("number", operatorCount);
                    xmlElement2.Add(operatorNameAttr);
                    XElement operatorCompanyElem = new XElement("login", CryptData(@operator.OLogin));
                    XElement operatorAgeElem = new XElement("password", CryptData(@operator.OPassword));
                    xmlElement2.Add(operatorCompanyElem);
                    xmlElement2.Add(operatorAgeElem);                    
                }
                operatorCount = 0;                
            }

            InteractionStand interactionStand = new InteractionStand(_standRepository);

            xdoc.Add(operatorsXML);
            xdoc.Save(interactionStand.operatorFilePathInProject);

            
            interactionStand.SendFileOnStands("Operator");

        }
        

        private static string CryptData(string str)
        {
            StringBuilder myString = new StringBuilder();
            byte[] myStrToByteArr = Encoding.ASCII.GetBytes(str);
            int sizeArr = myStrToByteArr.Length;

            for (int i = 0; i < sizeArr; i++)
            {
                byte value = myStrToByteArr[i];
                myString.Append((value + 50) * 51);
            }

            return myString.ToString();
        }
    }
}
