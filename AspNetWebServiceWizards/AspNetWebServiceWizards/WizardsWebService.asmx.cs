using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;

namespace AspNetWebServiceWizards
{
    /// <summary>
    /// Summary description for WizardsWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WizardsWebService : System.Web.Services.WebService
    {

        private string filePath = Path.Combine(HttpRuntime.AppDomainAppPath,"Wizards.xml");

        [WebMethod]
        public List<Wizard> GetWizard()
        {
            var doc = XDocument.Load(filePath);
            var elements = doc.Root.Elements();
            var wizards = new List<Wizard>(); //declare a Wizard data type list

            foreach (var e in elements)
            {
                //instantiate a Wizard class object
                Wizard w = new Wizard
                {
                    //setting a value to each fields in Wizard class
                    Id = int.Parse(e.Attribute("Id").Value),
                    Name = new Name
                    {
                        FirstName = e.Element("Name").Element("FirstName").Value,
                        LastName = e.Element("Name").Element("LastName").Value
                    },
                    School = e.Element("School").Value,
                    BloodStatus = e.Element("BloodStatus").Value,
                    Occupation = e.Element("Occupation").Value,
                    Birthday = new Birthday
                    {
                        Year = int.Parse(e.Element("Birthday").Element("Year").Value),
                        Month = int.Parse(e.Element("Birthday").Element("Month").Value),
                        Day = int.Parse(e.Element("Birthday").Element("Day").Value),
                    }
                };
                wizards.Add(w);
            }
            return wizards; //return wizards list
        }
        [WebMethod]
        public void UpdateWizard(string id, string firstName, string lastName, string school, string bloodStatus, string occupation, string year, string month, string day)
        {
            var doc = XDocument.Load(filePath);
            var elements = doc.Root.Elements();

            foreach (var e in elements)
            {
                if (e.Attribute("Id").Value == id)
                {
                    e.Element("Name").Element("FirstName").SetValue(firstName);
                    e.Element("Name").Element("LastName").SetValue(lastName);
                    e.Element("School").SetValue(school);
                    e.Element("BloodStatus").SetValue(bloodStatus);
                    e.Element("Occupation").SetValue(occupation);
                    e.Element("Birthday").Element("Year").SetValue(year);
                    e.Element("Birthday").Element("Month").SetValue(month);
                    e.Element("Birthday").Element("Day").SetValue(day);
                    doc.Save(filePath);
                    return;
                }
            }
        }
        [WebMethod]
        public void DeleteWizard(string id)
        {
            var doc = XDocument.Load(filePath);
            var elements = doc.Root.Elements();
            foreach (var e in elements)
            {
                if (e.Attribute("Id").Value == id)
                {
                    e.Remove();
                    doc.Save(filePath);
                    return;
                }
            }
        }
        [WebMethod]
        public void AddWizard(string firstName, string lastName, string school, string bloodStatus, string occupation, string year, string month, string day)
        {
            var doc = XDocument.Load(filePath);
            var elements = doc.Root.Elements();

            int highest = 0;
            foreach (var e in elements)
            {
                int id = int.Parse(e.Attribute("Id").Value);
                if (highest < id)
                {
                    highest = id;
                }
                highest++;
            }

            XElement wizardElement = new XElement("Wizard");

            wizardElement.Add(new XAttribute("Id", highest));

            XElement nameElement = new XElement("Name");
            nameElement.Add(new XElement("FirstName", firstName));
            nameElement.Add(new XElement("LastName", lastName));
            wizardElement.Add(nameElement);

            wizardElement.Add(new XElement("School", school));
            wizardElement.Add(new XElement("BloodStatus", bloodStatus));
            wizardElement.Add(new XElement("Occupation", occupation));

            XElement birthdayElement = new XElement("Birthday");
            birthdayElement.Add(new XElement("Year", year));
            birthdayElement.Add(new XElement("Month", month));
            birthdayElement.Add(new XElement("Day", day));
            wizardElement.Add(birthdayElement);

            doc.Root.Add(wizardElement);
            doc.Save(filePath);
        }
    }
}
