using System.Xml.Linq;
using HoffmanWebstatistic.Models.Hoffman;
using System.Text;
using HoffmanWebstatistic.Repository;
using System.Text.RegularExpressions;
using HoffmanWebstatistic.Services.FormationFile;
using HoffmanWebstatistic.Services.InteractionStand;

namespace HoffmanWebstatistic.Services.FormationFile
{
    public static class OperatorsXMLFile
    {


        public static void FormationXMLFileForStands(StandRepository _standRepository, List<Operator> operators)
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
                    XElement operatorLoginElem = new XElement("login", CryptData(@operator.OLogin));
                    XElement operatorPasswordElem = new XElement("password", CryptData(@operator.OPassword));
                    xmlElement2.Add(operatorLoginElem);
                    xmlElement2.Add(operatorPasswordElem);
                }
                operatorCount = 0;
            }

            StandOperation interactionStand = new StandOperation();

            xdoc.Add(operatorsXML);
            xdoc.Save(interactionStand.operatorFilePathInProject);


        }


        private static string CryptData(string str)
        {
            // Удаляем возможные переносы
            str = Regex.Replace(str, "[\r\n]+", "");

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
