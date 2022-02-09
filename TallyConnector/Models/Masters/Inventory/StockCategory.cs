﻿namespace TallyConnector.Models.Masters.Inventory;

[XmlRoot(ElementName = "STOCKCATEGORY")]
public class StockCategory : TallyXmlJson, ITallyObject
{
    public StockCategory()
    {
        LanguageNameList = new();
    }

    [XmlElement(ElementName = "MASTERID")]
    public int? TallyId { get; set; }

    [XmlAttribute(AttributeName = "NAME")]
    [JsonIgnore]
    public string OldName { get; set; }

    private string name;

    [XmlElement(ElementName = "NAME")]
    [Required]
    public string Name
    {
        get
        {
            name = (name == null || name == string.Empty) ? OldName : name;
            return name;
        }
        set => name = value;
    }

    [XmlElement(ElementName = "PARENT")]
    public string Parent { get; set; }

    [XmlIgnore]
    public string Alias { get; set; }

    [JsonIgnore]
    [XmlElement(ElementName = "LANGUAGENAME.LIST")]
    public List<LanguageNameList> LanguageNameList { get; set; }
    /// <summary>
    /// Accepted Values //Create, Alter, Delete
    /// </summary>
    [JsonIgnore]
    [XmlAttribute(AttributeName = "Action")]
    public string Action { get; set; }

    [XmlElement(ElementName = "GUID")]
    public string GUID { get; set; }

    public void CreateNamesList()
    {
        if (LanguageNameList.Count == 0)
        {
            LanguageNameList.Add(new LanguageNameList());
            LanguageNameList[0].NameList.NAMES.Add(Name);

        }
        if (Alias != null && Alias != string.Empty)
        {
            LanguageNameList[0].LanguageAlias = Alias;
        }
    }
    public new string GetXML(XmlAttributeOverrides attrOverrides = null)
    {
        CreateNamesList();
        return base.GetXML(attrOverrides);
    }

    public void PrepareForExport()
    {
        CreateNamesList();
    }
}

[XmlRoot(ElementName = "ENVELOPE")]
public class StockCatEnvelope : TallyXmlJson
{

    [XmlElement(ElementName = "HEADER")]
    public Header Header { get; set; }

    [XmlElement(ElementName = "BODY")]
    public SCBody Body { get; set; } = new();
}

[XmlRoot(ElementName = "BODY")]
public class SCBody
{
    [XmlElement(ElementName = "DESC")]
    public Description Desc { get; set; } = new();

    [XmlElement(ElementName = "DATA")]
    public SCData Data { get; set; } = new();
}

[XmlRoot(ElementName = "DATA")]
public class SCData
{
    [XmlElement(ElementName = "TALLYMESSAGE")]
    public SCMessage Message { get; set; } = new();

    [XmlElement(ElementName = "COLLECTION")]
    public CostCatColl Collection { get; set; } = new CostCatColl();


}

[XmlRoot(ElementName = "COLLECTION")]
public class CostCatColl
{
    [XmlElement(ElementName = "STOCKCATEGORY")]
    public List<StockCategory> StockCategories { get; set; }
}

[XmlRoot(ElementName = "TALLYMESSAGE")]
public class SCMessage
{
    [XmlElement(ElementName = "STOCKCATEGORY")]
    public StockCategory StockCategory { get; set; }
}
