// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Xml;

Console.WriteLine("This program is written by Gary Wee.");
Console.WriteLine("Date: 2022 May 13");
Console.WriteLine("This program parse the rainfall station kml file downloaded from https://data.taipei and retrieve data into DataTable format, which could be further usage in the future.");

Console.WriteLine("");
Console.WriteLine("Programming Running...");
string fileName = "Taipei_RainfallStation.kml";
DataTable dt = GetRainfallStationInfo_DataTable(fileName);
Console.WriteLine("End");


DataTable GetRainfallStationInfo_DataTable(string fileName)
{
    DataTable dt = new DataTable();
    dt.Columns.Add("Name");
    dt.Columns.Add("Station ID");
    dt.Columns.Add("Longitude");
    dt.Columns.Add("Latitude");

    XmlDocument doc = new XmlDocument();
    doc.Load(fileName);
    XmlNodeList placemarks = doc.GetElementsByTagName("Placemark");

    foreach (XmlElement station in placemarks)
    {
        DataRow row = dt.NewRow();
        string name = station.GetElementsByTagName("name")[0].InnerText;
        var simpleData = station.GetElementsByTagName("SimpleData");
        string stationID = "";
        double lon = 0, lat = 0;
        foreach (XmlNode node in simpleData)
        {
            if (node.Attributes["name"].Value == "STATIONID")
            {
                stationID = node.InnerText;
            }
            if (node.Attributes["name"].Value == "lat")
            {
                lon = Convert.ToDouble(node.InnerText);
            }
            if (node.Attributes["name"].Value == "lon")
            {
                lat = Convert.ToDouble(node.InnerText);
                break;
            }
        }
        row["Name"] = name;
        row["Station ID"] = stationID;
        row["Longitude"] = lon;
        row["Latitude"] = lat;
        dt.Rows.Add(row);
    }
    return dt;
}
