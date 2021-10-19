using System;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace Herhalingsoefeningen
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
     //Deel 1: XML file inlezen en data mappen + gewijzigde data wegschrijven naar nieuwe Xml file

            //een instantie van de serializer wordt aangemaakt om de xml file te parsen als een PurchaseOrder item
            var serializer = new XmlSerializer(typeof(PurchaseOrder));
            var stream = new FileStream("purchase.xml", FileMode.Open, FileAccess.Read);

            //Een purchaseOrder instantie wordt aangemaakt met de geparste items 
            PurchaseOrder? purchaseorder = (PurchaseOrder?)serializer.Deserialize(stream);

            //Wijzigingen aan de instantie die we geparsed hebben
            purchaseorder.PurchaseOrderNumber = "99544";
            purchaseorder.Address.Street = "Molenstraat";
        
            //Output variabele aanmaken die wordt opgeslaan in een nieuwe xml file genaamd "result"
            var outputstream = new FileStream("result.xml", FileMode.CreateNew, FileAccess.Write);
            //de serializer slaat de instantie purchaseorder (die nu gewijzigd is) op in de output stream
            serializer.Serialize(outputstream, purchaseorder);



    //Deel 2: Linq om alle items op te vragen die de letter "B" in hun partnummer hebben
            //Xml file opladen
            var purchase = XElement.Load("purchase.xml");

            //items met letter B in partNumber opvragen
            var items = purchase.Elements("Item").Where(node => node.Attribute("PartNumber").Value.Contains('B'));

            //Lijst opstellen om alle items met letter B verderop in op te slaan
            List<Item> selectedItems = new List<Item>();

            //van elke var "item" in de collectie "items" wordt een item-instantie aangemaakt die wordt opgeslaan in de itemlijst
            foreach (var item in items)
            {
                string productName = (string)item.Element("ProductName");
                string partNumber = (string)item.Attribute("PartNumber");
                int quantity = (int)item.Element("Quantity");

                Item item1 = new Item();
                item1.ProductName = productName;
                item1.PartNumber = partNumber;
                item1.Quantity = quantity;

                selectedItems.Add(item1);
            }

    //Deel 3: Uploaden van items in selectedItems-lijst naar database
            //reporitory insantie aanmaken (deze wordt ook gebruikt om data op te vragen in forms)
            ItemRepository repository = new ItemRepository();

            //Geselecteerde items uploaden in database
            repository.AddItemsToDatabase(selectedItems);



     //Deel 4: Winforms: geselecteerde items printen + lijst van alle items tonen in database
            //tekst met productnamen met letter B om te tonen op winforms
            string itemsPrint = "Products: \n";
            foreach (Item item in selectedItems)
            {
                itemsPrint += "- " + item.ProductName + "\n";
            }

            //tekst met alle producten uit db om te tonen na het klikken van een button
            List<Item> alleItems = repository.GetAllItems();
            string allItemsPrint = "Items in database: \n";
            foreach (Item item in alleItems)
            {
                allItemsPrint += "[Productnaam:" + item.ProductName + ", aantal: " + item.Quantity + "] \n";
            }


            ApplicationConfiguration.Initialize();

        //form1 instantie aanmaken
            Form1 form = new Form1();
        //Via functies juiste tekst van items meegeven aan tekstboxen
            form.SetTextBox1Value(itemsPrint);
            form.SetTextBox2(allItemsPrint);

        //Form tonen aan gebruiker
            Application.Run(new Form1());

            
    
            return;

            



        }
    }
}
