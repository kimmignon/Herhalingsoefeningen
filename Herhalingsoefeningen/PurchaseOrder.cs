using System.Xml.Serialization;
using System.Collections.Generic;

//Klasse om data van de xml file te mappen op een instantie van een klasse.
namespace Herhalingsoefeningen
{
    //De data wordt opgeslagen in een instantie van de PurchaseOrderklasse (startende vanaf de root van de xml file) 
    //De adress gegevens worden opgeslagen in aparte klasse Adress en alsook de items. In een purchaseOrders instanties worden alle items bijgehouden in een lijst
    [XmlRoot("PurchaseOrder")]
    public class PurchaseOrder
    {
        [XmlAttribute("PurchaseOrderNumber")]
        public string PurchaseOrderNumber { get; set; }
        [XmlElement("Address")]
        public Address Address { get; set; }
        [XmlArray("Items"), XmlArrayItem("Item")]
        public List<Item> Items { get; set; }

    }

    //De gegevens/items van het adres worden eerst apart opgeslagen in een instantie van de klasse Address
    public class Address
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Street")]
        public string Street { get; set; }
        [XmlElement("City")]
        public string City { get; set; }
        [XmlElement("State")]
        public string State { get; set; }
        [XmlElement("Zip")]
        public string Zip { get; set; }
        [XmlElement("Country")]
        public string Country { get; set; }
    }

    //Ook voor de items wordt een aparte klasse aangemaakt om in een item-instantie het partnummer, productnaam en quantity op te slagen
    public class Item
    {
        [XmlAttribute("PartNumber")]
        public string PartNumber { get; set; }

        [XmlElement("ProductName")]
        public string ProductName { get; set; }

        [XmlElement("Quantity")]
        public int Quantity { get; set; }

    }
}
