using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Linq;

namespace KlassaGenerering
{
    public class TBaseTypeDefinition : INotifyPropertyChanged
    {
        private int _RecId = 0;
        public int RecId { get { return _RecId; } set { _RecId = value; TriggerPropertyChanged("RecId"); } }

        public virtual string CSStreamInMethod(string FieldName) { return "CSStreamInMethod not implemented"; }
        public virtual string CSStreamOutMethod(string FieldName)
        {
            return "CSStreamOutMethod not implemented";
        }

        public virtual string CSDeclareFieldOfType(string FieldName, Boolean DeclarePublic) { return "DeclareFieldOfType not implemented"; }


        public virtual string DelphiStreamInMethod(string FieldName) { return "DelphiStreamInMethod not implemented"; }
        public virtual string DelphiStreamOutMethod(string FieldName) { return "DelphiStreamOutMethod not implemented"; }

        public virtual string DelphiDeclareFieldOfType(string FieldName) { return "DeclareFieldOfType not implemented"; }

        internal virtual string ProtoDeclareFieldOfType(string fieldName, int index)
        {
            return "ProtoDeclareFieldOfType not implemented " + this.GetType().ToString();
        }

        public virtual void SaveToXml(XElement ParentNode)
        {
            ParentNode.Add(new XAttribute("CLASS", GetType().Name));
            ParentNode.Add(new XAttribute("RecId", RecId));
        }

        public virtual void LoadFromXml(XElement Xml)
        {
            RecId = (int)Xml.Attribute("RecId");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void TriggerPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }


        public virtual string CSTypeName() {
            return "CSTypeName not implemented";
        }


        internal virtual string DelphiTypeName() {
            return "CSTypeName not implemented";
        }
    }

    public class TSimpleTypeDefinition : TBaseTypeDefinition
    {
        public static int FirstClassEnum = 10000;

        string _DelphiName = "";
        string _CSName = "";
        public string DelphiName { get { return _DelphiName; } set { _DelphiName = value; TriggerPropertyChanged("DelphiName"); } }
        public string CSName { get { return _CSName; } set { _CSName = value; TriggerPropertyChanged("CSName"); } }

        public string ProtoName { get; internal set; }

        public override string CSStreamInMethod(string FieldName)
        {
            return FieldName + " = ProxyStream.Read" + CSName + "(stream, \"" + FieldName + "\", MaxPos);";
        }
        public override string CSStreamOutMethod(string FieldName)
        {
            return "ProxyStream.Write" + CSName + "(stream, \"" + FieldName + "\", "+FieldName+");";
        }


        public override string CSDeclareFieldOfType(string FieldName, Boolean DeclarePublic)
        {
            if (DeclarePublic)
                return "public " + CSName + " " + FieldName + ";";
            else
                return CSName + " " + FieldName + ";";
        }

        internal override string ProtoDeclareFieldOfType(string fieldName, int index)
        {
            return ProtoName + " " + fieldName + " = " + index + ";";
        }


        public override string DelphiStreamInMethod(string FieldName)
        {
            return FieldName + " := ProxyStream.Read" + DelphiName + "(stream, '" + FieldName + "', MaxPos);";
        }

        public override string DelphiStreamOutMethod(string FieldName)
        {
            return "ProxyStream.Write" + DelphiName + "(stream, '" + FieldName + "', " + FieldName + ");";
        }

        public override string DelphiDeclareFieldOfType(string FieldName)
        {
            return "    " + FieldName + ": " + DelphiName + ";";
        }

        public override void SaveToXml(XElement ParentNode)
        {
            base.SaveToXml(ParentNode);
            ParentNode.Add(new XAttribute("DelphiName", DelphiName));
            ParentNode.Add(new XAttribute("CSName", CSName));
        }

        public override void LoadFromXml(XElement Xml)
        {
            base.LoadFromXml(Xml);
            DelphiName = (string)Xml.Attribute("DelphiName");
            CSName = (string)Xml.Attribute("CSName");
        }

        public override string CSTypeName() { return CSName; }

        internal override string DelphiTypeName() { return DelphiName; }
    }

    public class TEnumTypeDefinition : TBaseTypeDefinition
    {
        string _Name = "";
        string _DelphiPrefix = "";
        string _CSPrefix = "";
        string _DelphiItemPrefix = "";
        string _CSItemPrefix = "";
        TEnumTypeValueList _Values = new TEnumTypeValueList();

        public TEnumTypeValueList Values { get { return _Values; } set { _Values = value; TriggerPropertyChanged("Values"); } }
        public string Name { get { return _Name; } set { _Name = value; TriggerPropertyChanged("Name"); } }
        public string DelphiPrefix { get { return _DelphiPrefix; } set { _DelphiPrefix = value; TriggerPropertyChanged("DelphiPrefix"); } }
        public string CSPrefix { get { return _CSPrefix; } set { _CSPrefix = value; TriggerPropertyChanged("CSPrefix"); } }
        public string DelphiItemPrefix { get { return _DelphiItemPrefix; } set { _DelphiItemPrefix = value; TriggerPropertyChanged("DelphiItemPrefix"); } }
        public string CSItemPrefix { get { return _CSItemPrefix; } set { _CSItemPrefix = value; TriggerPropertyChanged("CSItemPrefix"); } }

        public void AddValueList(string ValueList)
        {
            string[] ValueArray = ValueList.Split(',');
            foreach (string valueName in ValueArray)
            {
                Values.Add(new TEnumTypeValue() { Name = valueName.Trim() });
            }
        }

        public override string CSStreamInMethod(string FieldName)
        {
            return FieldName + " = ProxyStream.Read" + CSPrefix + Name + "(stream, \"" + FieldName + "\", MaxPos);";
        }

        public override string CSStreamOutMethod(string FieldName)
        {
            return "ProxyStream.Write" + CSPrefix + Name + "(stream, \"" + FieldName + "\", " + FieldName + ");";
        }

        public override string CSDeclareFieldOfType(string FieldName, Boolean DeclarePublic)
        {
            if (DeclarePublic)
                return "public " + CSPrefix + Name + " " + FieldName + ";";
            else
                return CSPrefix + Name + " " + FieldName + ";";
        }

        internal override string ProtoDeclareFieldOfType(string fieldName, int index)
        {
            return Name + " " + fieldName + " = " + index + "; ";
        }

        public override string DelphiStreamInMethod(string FieldName)
        {
            return FieldName + " := ProxyStream.Read" + DelphiPrefix + Name + "(stream, '" + FieldName + "', MaxPos);";
        }

        public override string DelphiStreamOutMethod(string FieldName)
        {
            return "ProxyStream.Write" + DelphiPrefix + Name + "(stream, '" + FieldName + "', " + FieldName + ");";
        }

        public override string DelphiDeclareFieldOfType(string FieldName)
        {
            return "    " + FieldName + ": " + DelphiPrefix + Name + ";";
        }

        public override void SaveToXml(XElement ParentNode)
        {
            base.SaveToXml(ParentNode);
            ParentNode.Add(new XAttribute("Name", Name));
            ParentNode.Add(new XAttribute("DelphiPrefix", Name));
            ParentNode.Add(new XAttribute("CSPrefix", Name));
            ParentNode.Add(new XAttribute("DelphiItemPrefix", Name));
            ParentNode.Add(new XAttribute("CSItemPrefix", Name));

            XElement XValues = new XElement("ENUMVALUES");
            ParentNode.Add(XValues);
            foreach (TEnumTypeValue value in Values)
                XValues.Add(new XElement("ENUMVALUE", new XAttribute("Name", value.Name)));
        }

        public override void LoadFromXml(XElement Xml)
        {
            base.LoadFromXml(Xml);
            Name = (string)Xml.Attribute("Name");
            DelphiPrefix = (string)Xml.Attribute("DelphiPrefix");
            CSPrefix = (string)Xml.Attribute("CSPrefix");
            DelphiItemPrefix = (string)Xml.Attribute("DelphiItemPrefix");
            CSItemPrefix = (string)Xml.Attribute("CSItemPrefix");
            Values.Clear();
            foreach (XElement XEnumValue in Xml.Element("ENUMVALUES").Elements())
            {
                Values.Add(new TEnumTypeValue()
                {
                    Name = (string)XEnumValue.Attribute("Name"),
                });
            }

        }


        public override string CSTypeName() { return CSPrefix + Name; }

        internal override string DelphiTypeName() { return DelphiPrefix + Name; }
    }

    public class TEnumTypeValue : INotifyPropertyChanged
    {
        string _Name = "";
        public string Name { get { return _Name; } set { _Name = value; TriggerPropertyChanged("Name"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void TriggerPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
    }

    public class TEnumTypeValueList : ObservableCollection<TEnumTypeValue>
    {
    }

    public class TClassTypeDefinition : TBaseTypeDefinition
    {
        //public int ParentRecId { get; set; }
        //public int ClassEnum { get; set; }
        //public string ClassName { get; set; }
        //public TClassTypeFieldDefinitionList ClassFieldList { get; set; }
        //public bool CustomImplementationExists { get; set;}

        int _ParentRecId = 0;
        int _ClassEnum = 0;
        string _ClassName = "";
        TClassTypeFieldDefinitionList _ClassFieldList = new TClassTypeFieldDefinitionList();
        bool _CustomImplementationExists = false;

        public int ParentRecId { get { return _ParentRecId; } set { _ParentRecId = value; TriggerPropertyChanged("ParentRecId"); } }
        public int ClassEnum { get { return _ClassEnum; } set { _ClassEnum = value; TriggerPropertyChanged("ClassEnum"); } }
        public string ClassName { get { return _ClassName; } set { _ClassName = value; TriggerPropertyChanged("ClassName"); } }
        public TClassTypeFieldDefinitionList ClassFieldList { get { return _ClassFieldList; } set { _ClassFieldList = value; TriggerPropertyChanged("ClassFieldList"); } }
        public bool CustomImplementationExists { get { return _CustomImplementationExists; } set { _CustomImplementationExists = value; TriggerPropertyChanged("CustomImplementationExists"); } }

        public string GetCSStreamClassName()
        {
            return "T5Stream" + ClassName;
        }

        public string GetProtoStreamClassName()
        {
            return ClassName;
        }

        public string GetCSUIClassName()
        {
            return "T5UI" + ClassName;
        }

        public string GetDelphiStreamClassName()
        {
            return "T5Stream" + ClassName;
        }

        public TClassTypeDefinition()
        {
            //ClassFieldList = new TClassTypeFieldDefinitionList();
            //CustomImplementationExists = false;
        }

        public override string CSStreamInMethod(string FieldName)
        {
            return "      " + FieldName + " = ProxyStream.Read" + GetCSStreamClassName() + "(stream, \"" + FieldName + "\");";
        }

        public override string CSStreamOutMethod(string FieldName)
        {
            return FieldName + ".StreamOut(stream, \"" + FieldName + "\");";
        }

        public override string CSDeclareFieldOfType(string FieldName, Boolean DeclarePublic)
        {
            if (DeclarePublic)
                return "public " + GetCSStreamClassName() + " " + FieldName + " = new " + GetCSStreamClassName() + "();";
            else
                return GetCSStreamClassName() + " " + FieldName + " = new " + GetCSStreamClassName() + "();";
        }

        internal override string ProtoDeclareFieldOfType(string fieldName, int index)
        {
            return GetProtoStreamClassName() + " " + fieldName + " = " + index + ";";
        }

        public override string DelphiStreamInMethod(string FieldName)
        {
            return FieldName + " := ProxyStream.Read" + GetDelphiStreamClassName() + "(stream, '" + FieldName + "', MaxPos);"; //StreamIn" + ClassName + "(stream);";
        }

        public override string DelphiStreamOutMethod(string FieldName)
        {
            return FieldName + ".StreamOut(stream, '" + FieldName + "');";
        }

        public override string DelphiDeclareFieldOfType(string FieldName)
        {
            return "    " + FieldName + ": " + GetDelphiStreamClassName() + ";";
        }

        public override void SaveToXml(XElement ParentNode)
        {
            base.SaveToXml(ParentNode);
            ParentNode.Add(new XAttribute("ParentRecId", ParentRecId));
            ParentNode.Add(new XAttribute("ClassEnum", ClassEnum));
            ParentNode.Add(new XAttribute("ClassName", ClassName));
            ParentNode.Add(new XAttribute("CustomImplementationExists", CustomImplementationExists));
            XElement XFieldList = new XElement("FIELDLIST");
            ParentNode.Add(XFieldList);
            ClassFieldList.SaveToXml(XFieldList);
        }

        public override void LoadFromXml(XElement Xml)
        {
            base.LoadFromXml(Xml);
            ParentRecId = (int)Xml.Attribute("ParentRecId");
            ClassEnum = (int)Xml.Attribute("ClassEnum");
            ClassName = (string)Xml.Attribute("ClassName");
            CustomImplementationExists = (bool)Xml.Attribute("CustomImplementationExists");
            XElement XFieldsList = Xml.Element("FIELDLIST");
            ClassFieldList.Clear();
            ClassFieldList.LoadFromXml(XFieldsList);
        }


        internal IEnumerable<TClassTypeFieldDefinition> GetTotalClassFieldList(TypeDefinitionList typeDefinitionList)
        {
            TBaseTypeDefinition btd = typeDefinitionList.FirstOrDefault(a => a.RecId == ParentRecId);
            TClassTypeFieldDefinitionList result = new TClassTypeFieldDefinitionList();

            if (btd is TClassTypeDefinition)
            {
                TClassTypeDefinition parentClassDefinition = btd as TClassTypeDefinition;
                foreach (TClassTypeFieldDefinition def in parentClassDefinition.GetTotalClassFieldList(typeDefinitionList))
                    result.Add(def);
                foreach (TClassTypeFieldDefinition def in ClassFieldList)
                    result.Add(def);
                return result;
            }
            else
                return ClassFieldList;
        }

        public override string CSTypeName() { return GetCSStreamClassName(); }

        internal override string DelphiTypeName() { return GetDelphiStreamClassName(); }
    }

    public class TClassTypeCollectionDefinition : TClassTypeDefinition
    {
        int _ItemTypeRecId = 0;

        public int ItemTypeRecId { get { return _ItemTypeRecId; } set { _ItemTypeRecId = value; TriggerPropertyChanged("ItemTypeRecId"); } }
    }

    public class TClassTypeFieldDefinition : INotifyPropertyChanged
    {
        int _TypeRecId = 0;
        string _FieldName = "";

        public int TypeRecId { get { return _TypeRecId; } set { _TypeRecId = value; TriggerPropertyChanged("TypeRecId"); } }
        public string FieldName { get { return _FieldName; } set { _FieldName = value; TriggerPropertyChanged("FieldName"); } }
        //public string  { get { return _; } set { _ = value; TriggerPropertyChanged(""); } }  

        public event PropertyChangedEventHandler PropertyChanged;

        public void TriggerPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
    }

    public class TClassTypeFieldDefinitionList : ObservableCollection<TClassTypeFieldDefinition>
    {
        internal void SaveToXml(XElement ParentNode)
        {
            foreach (TClassTypeFieldDefinition field in this)
            {
                XElement XField = new XElement("FIELD");
                ParentNode.Add(XField);
                XField.Add(new XAttribute("FieldName", field.FieldName));
                XField.Add(new XAttribute("TypeRecId", field.TypeRecId));
            }
        }

        internal void LoadFromXml(XElement XFieldsList)
        {
            foreach (XElement XField in XFieldsList.Elements())
            {
                TClassTypeFieldDefinition field = new TClassTypeFieldDefinition()
                {
                    TypeRecId = (int)XField.Attribute("TypeRecId"),
                    FieldName = (string)XField.Attribute("FieldName"),
                };
                Add(field);
            }
        }
    }

    public class TypeDefinitionList : ObservableCollection<TBaseTypeDefinition>
    {
        public string TargetNameSpace { get; set; }

        public void LoadData()
        {
            #region "Kindof constants fields"
            //const int trByte = 1;
            const int trBoolean = 2;
            const int trInteger = 3;
            const int trString = 4;
            const int trTDateTime = 5;
            //const int trDouble = 6;
            const int trCardinal = 7;
            const int trWord = 8;

            TargetNameSpace = "Proxy.Streaming";
            #endregion
            #region "Base Types"

            Add(new TSimpleTypeDefinition() { RecId = 1, DelphiName = "Byte", CSName = "Byte", ProtoName = "bytes" });
            Add(new TSimpleTypeDefinition() { RecId = 2, DelphiName = "Boolean", CSName = "Boolean", ProtoName = "bool" });
            Add(new TSimpleTypeDefinition() { RecId = 3, DelphiName = "Integer", CSName = "Int32", ProtoName = "int32" });
            Add(new TSimpleTypeDefinition() { RecId = 4, DelphiName = "String", CSName = "String", ProtoName = "string" });
            Add(new TSimpleTypeDefinition() { RecId = 5, DelphiName = "TDateTime", CSName = "DateTime", ProtoName = "google.protobuf.Timestamp" });
            Add(new TSimpleTypeDefinition() { RecId = 6, DelphiName = "Double", CSName = "Double", ProtoName = "double" });
            Add(new TSimpleTypeDefinition() { RecId = 7, DelphiName = "Cardinal", CSName = "UInt32", ProtoName = "uint32" });
            Add(new TSimpleTypeDefinition() { RecId = 8, DelphiName = "Word", CSName = "UInt16", ProtoName = "uint32" });

            #endregion
            #region "Enumerations"

            TEnumTypeDefinition ed;
            ed = new TEnumTypeDefinition() { RecId = 1000, Name = "CallStateEnum", DelphiPrefix = "TCC", DelphiItemPrefix = "T5tcs", CSPrefix = "", CSItemPrefix = "" };
            Add(ed);
            ed.AddValueList("Idle, Initiated, Alerting, Connected, Queued, Error, Hold, Fail, Unknown, Offline");

            ed = new TEnumTypeDefinition() { RecId = 1001, Name = "AgentStateEnum", DelphiPrefix = "TCC", DelphiItemPrefix = "cca", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("Null, Busy, NotReady, Ready, WorkAfterCall");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1002, Name = "DeviceTypeEnum", DelphiPrefix = "TCC", DelphiItemPrefix = "ccd", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("Unknown, Device, Trunk, Mobile, External");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1003, Name = "DeviceSubTypeEnum", DelphiPrefix = "TCC", DelphiItemPrefix = "ccs", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("Unknown, ACD, Group, Trunk, IPTrunk, System, DECT, AnalogOrVirtual, ISDN, VoiceUnit, Other");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1004, Name = "DeviceStateEnum", DelphiPrefix = "TCC", DelphiItemPrefix = "ccd", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("Null, NotFound, Connected, Running, Disconnected, Error");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1005, Name = "SuggestedRuleEnum", DelphiPrefix = "T5", DelphiItemPrefix = "ssr", CSPrefix = "", CSItemPrefix = "ssr" };
            ed.AddValueList("Fixed,5min,10min,15min,30min,1h");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1006, Name = "SuggestedRoundTypeEnum", DelphiPrefix = "T5", DelphiItemPrefix = "sst", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("Regular,Up,Down");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1007, Name = "ForwardTypeEnum", DelphiPrefix = "T5", DelphiItemPrefix = "T5ft", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("Ignore, None, All, External, Internal");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1008, Name = "DNDTypeEnum", DelphiPrefix = "T5", DelphiItemPrefix = "T5dt", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("Ignore,On,Off");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1009, Name = "ResourceDetailLinkEnum", DelphiPrefix = "T5", DelphiItemPrefix = "T5dl", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("WrongType,Unknown,Local,Work,Home,Mobile,Fax,Mail,Web,Other,Mulap,SipAddr");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1010, Name = "ReservationSettingTypeEnum", DelphiPrefix = "T5", DelphiItemPrefix = "rst", CSPrefix = "", CSItemPrefix = "rst" };
            ed.AddValueList("Start, End");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1011, Name = "AppointmentTypeEnum", DelphiPrefix = "T5", DelphiItemPrefix = "T5at", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("Normal, Recurring");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1012, Name = "ReservationTypeEnum", DelphiPrefix = "T5", DelphiItemPrefix = "T5rt", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("Current, Reservation");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1013, Name = "ScopeTypeEnum", DelphiPrefix = "T5", DelphiItemPrefix = "T5st", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("Public, Private");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1014, Name = "ClientTypeEnum", DelphiPrefix = "T5", DelphiItemPrefix = "T5ct", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("Client,Terminal,Admin,Switchboard,Receptionist,Provider,Proxy,Custom,SmartClient");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1015, Name = "OriginExecutionEnum", DelphiPrefix = "T5", DelphiItemPrefix = "T5oe", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("Unknown,SetCurrent,AppPromotin,AppEnd,ResetAtMidnight");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1016, Name = "EndTypeEnum", DelphiPrefix = "T5", DelphiItemPrefix = "et", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("Undefined, Expected, Actual");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1017, Name = "PendingStateEnum", DelphiPrefix = "T5", DelphiItemPrefix = "T5ps", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("None,PendingCreate,PendingUpdate,PendingDelete,PendingCreateFail,PendingUpdateFail,PendingDeleteFail");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1018, Name = "NotifyCommandEnum", DelphiPrefix = "T5", DelphiItemPrefix = "T5cmd", CSPrefix = "", CSItemPrefix = "cmd" };
            ed.AddValueList("SendStaticData");
            ed.AddValueList("MaximizeSwitchBoard");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1019, Name = "EndingTimeEnum", DelphiPrefix = "T5", DelphiItemPrefix = "T5et", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("NoRequired,SameDay,Required");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1020, Name = "StateLengthEnum", DelphiPrefix = "T5", DelphiItemPrefix = "T5sl", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("OneDay,ExpectedEnd,UntilAnotherState");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1021, Name = "EndStampEnum", DelphiPrefix = "T5", DelphiItemPrefix = "T5es", CSPrefix = "", CSItemPrefix = "es" };
            ed.AddValueList("Beginning, End");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1022, Name = "StateTypeEnum", DelphiPrefix = "T5", DelphiItemPrefix = "T5st", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("UserState,ResourceState,SystemState");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1023, Name = "StateClassEnum", DelphiPrefix = "T5", DelphiItemPrefix = "T5sc", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("Free, Tentative, Busy, OutOfOffice");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1024, Name = "CallStatisticsTypeEnum", DelphiPrefix = "T5", DelphiItemPrefix = "T5cst", CSPrefix = "", CSItemPrefix = "cst" };
            ed.AddValueList("Idle,Busy");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1025, Name = "ResourceDetailTypeEnum", DelphiPrefix = "T5", DelphiItemPrefix = "T5rd", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("Unknown,Link");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1026, Name = "ExpectedResponse", DelphiPrefix = "T5", DelphiItemPrefix = "T5er", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("NoncePwd,Nonce,Password,Nothing,GlobalPassword");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1027, Name = "ResponseCoding", DelphiPrefix = "T5", DelphiItemPrefix = "T5rc", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("Plain,MD5,SHA");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1028, Name = "ChallengeReason", DelphiPrefix = "T5", DelphiItemPrefix = "T5cr", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("Login,NotAuthenticated,LicenseProblem,TooManyTries,Authenticated,NoPrivilege,NotWinAuthenticated,UserNotActive");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1029, Name = "CallLogType", DelphiPrefix = "T5", DelphiItemPrefix = "T5nlt", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("IsCallingAnswered,IsCalledAnswered,IsCallingNoAnswer,IsCalledNoAnswer,Answered");
            Add(ed);

            ed = new TEnumTypeDefinition() { RecId = 1030, Name = "CallLogDirectionType", DelphiPrefix = "T5", DelphiItemPrefix = "T5cldt", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("IncomingAndOutgoing,Outgoing,Incoming");
            Add(ed);


            ed = new TEnumTypeDefinition() { RecId = 1031, Name = "AuthenticationType", DelphiPrefix = "T5", DelphiItemPrefix = "T5at", CSPrefix = "", CSItemPrefix = "" };
            ed.AddValueList("Basic,AuthenticationPortal");
            Add(ed);

            #endregion
            #region "Class definitions"

            TClassTypeDefinition cd;
            TClassTypeCollectionDefinition ccd;
            cd = new TClassTypeDefinition() { RecId = 2000, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 1, ClassName = "BaseObject", ParentRecId = 0, CustomImplementationExists = true };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ClassEnum", TypeRecId = 3 });
            Add(cd);

            ccd = new TClassTypeCollectionDefinition() { RecId = 3000, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 3001, ClassName = "BaseObjectCollection", ItemTypeRecId = 2000, ParentRecId = 2000, CustomImplementationExists = true };
            Add(ccd);

            cd = new TClassTypeDefinition() { RecId = 2001, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 2, ClassName = "DeviceS", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "CallState", TypeRecId = 1000 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "QueueLength", TypeRecId = 3 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "DND", TypeRecId = 2 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Forwarded", TypeRecId = 2 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "IsGroupMember", TypeRecId = 2 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "AgentState", TypeRecId = 1001 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "IsPriorityCalls", TypeRecId = 2 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "DeviceId", TypeRecId = 4 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "NoOfHeld", TypeRecId = 3 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "DeviceType", TypeRecId = 1002 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "SubType", TypeRecId = 1003 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "DeviceState", TypeRecId = 1004 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ConnectorName", TypeRecId = 4 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Mute", TypeRecId = 2 });

            Add(cd);

            cd = new TClassTypeDefinition() { RecId = 2002, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 3, ClassName = "DeviceM", ParentRecId = 2001 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ConnectionList", TypeRecId = 2008 });
            Add(cd);

            cd = new TClassTypeDefinition() { RecId = 2003, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 4, ClassName = "DeviceL", ParentRecId = 2002 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "TransferList", TypeRecId = 2006 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ForwardType", TypeRecId = 3 }); 
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ForwardedTo", TypeRecId = 4 }); 
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "TransferredTo", TypeRecId = 4 }); 
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "RemoteOfficeEnabled", TypeRecId = 2 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "RemoteOfficeActive", TypeRecId = 2 }); 
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "RemoteOfficeNumber", TypeRecId = 4 }); 
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "OutboundCallerIdEnabled", TypeRecId = 2 }); 
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "OutboundCallerId", TypeRecId = 4 });             

            Add(cd);

            cd = new TClassTypeDefinition() { RecId = 2005, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 6, ClassName = "DeviceTransferedConnection", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "CallId", TypeRecId = 4 }); //PWriteString(Trans.CallID, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "DeviceId", TypeRecId = 4 }); //PWriteString(Trans.DeviceID, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "TransferredTo", TypeRecId = 4 }); //PWriteString(Trans.TransferredTo, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "State", TypeRecId = 1000 }); //PWriteInt(Ord(Trans.State), Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "CallingDevice", TypeRecId = 4 }); //PWriteString(Trans.CallingDevice, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "CalledDevice", TypeRecId = 4 }); //PWriteString(Trans.CalledDevice, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "IsMonitored", TypeRecId = 2 }); //PWriteBool(Trans.IsMonitored, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "CallCreatedTime", TypeRecId = 5 }); //Stream.Write(Trans.CallCreatedTime,sizeof(Trans.CallCreatedTime));
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ConnectionCreatedTime", TypeRecId = 5 }); //Stream.Write(Trans.ConnectionCreatedTime,sizeof(Trans.ConnectionCreatedTime));
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "CreatedTime", TypeRecId = 5 }); //Stream.Write(Trans.CreatedTime,sizeof(Trans.CreatedTime));
            Add(cd);

            cd = new TClassTypeCollectionDefinition() { RecId = 2006, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 7, ClassName = "DeviceTransferedConnectionCollection", ParentRecId = 3000, ItemTypeRecId = 2005 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ForwardType", TypeRecId = 3 }); // PWriteInt(Ord(Self.ForwardType), Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ForwardedTo", TypeRecId = 4 }); // PWriteString(Self.ForwardedTo, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "TransferredTo", TypeRecId = 4 }); // PWriteString(Self.FTransferredTo.Text, Stream);
            Add(cd);

            cd = new TClassTypeDefinition() { RecId = 2007, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 8, ClassName = "DeviceConnection", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "CallId", TypeRecId = 4 }); // PWriteString(Con. CallID                       , Stream);

            //
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "State", TypeRecId = 1000 }); // PWriteInt(Ord(Con.State                       ), Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "CallingDevice", TypeRecId = 4 }); // PWriteString(Con. CallingDevice               , Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "CalledDevice", TypeRecId = 4 }); // PWriteString(Con. CalledDevice             , Stream);

            //
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "RemoteState", TypeRecId = 1000 }); // PWriteInt(Ord(Con.RemoteState              ), Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "CallCreatedTime", TypeRecId = 5 }); // Stream.Write(Con. CallCreatedTime         , sizeof(Con.CallCreatedTime));
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ConnectionCreatedTime", TypeRecId = 5 }); // Stream.Write(Con. ConnectionCreatedTime   , sizeof(Con.ConnectionCreatedTime));
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "CreatedTime", TypeRecId = 5 }); // Stream.Write(Con. CreatedTime             , sizeof(Con.CreatedTime));                                         
            Add(cd);

            cd = new TClassTypeCollectionDefinition() { RecId = 2008, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 9, ClassName = "DeviceConnectionCollection", ParentRecId = 3000, ItemTypeRecId = 2007 };
            //cd.ClassFieldList.Add(new ClassField() { FieldName = "", TypeRecId =  }); //                               
            Add(cd);

            cd = new TClassTypeDefinition() { RecId = 2009, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 10, ClassName = "LyncState", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "SipAddress", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Availability", TypeRecId = trCardinal });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "AvailabilityName", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "DeviceType", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "DeviceTypeName", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Location", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "PersonalNote", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "OutOfOfficeNote", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ActivityToken", TypeRecId = trString });
            Add(cd);

            // ResourceDetail
            cd = new TClassTypeDefinition() { RecId = 2011, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 12, ClassName = "ResourceDetail", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Recid", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "OwnerId", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "DetailType", TypeRecId = 1025 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "DetailSubtype", TypeRecId = trWord });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Parameter", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Value", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Enabled", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Default", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Changed", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "SortOrder", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "PrivateInfo", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ExternalMonitor", TypeRecId = trBoolean });
            Add(cd);

            cd = new TClassTypeCollectionDefinition() { RecId = 2012, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 13, ClassName = "ResourceDetailCollection", ParentRecId = 3000, ItemTypeRecId = 2011 };
            Add(cd);

            cd = new TClassTypeDefinition() { RecId = 2013, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 14, ClassName = "Resource", ParentRecId = 2000 };
            //cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Recid", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "RecId", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "UserId", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "FirstName", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "LastName", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "DetailCollection", TypeRecId = 2012 }); //  Links.StreamOut(stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "HasRemoteCallingRights", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "HasNumberLog", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "LogExternalCalls", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "SynchronizeCalendar", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "UseAttachments", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "RequireAttachment", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "HasSendSmsAnonomousRights", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "UserImageVersion", TypeRecId = trString });
            Add(cd);

            cd = new TClassTypeCollectionDefinition() { RecId = 2014, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 15, ClassName = "ResourceCollection", ParentRecId = 3000, ItemTypeRecId = 2013 };
            Add(cd);



            cd = new TClassTypeDefinition() { RecId = 2015, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 16, ClassName = "CallerHistory", ParentRecId = 2000 };
            //cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Recid", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "DeviceId", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "CallTime", TypeRecId = trTDateTime });
            Add(cd);

            cd = new TClassTypeCollectionDefinition() { RecId = 2016, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 17, ClassName = "CallerHistoryCollection", ParentRecId = 3000, ItemTypeRecId = 2015 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ExternalNumber", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "OwnerName", TypeRecId = trString });
            Add(cd);

            cd = new TClassTypeDefinition() { RecId = 2017, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 18, ClassName = "ReservationDetail", ParentRecId = 2000 };
            //cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Recid", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "SettingType", TypeRecId = 1010 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ParamName", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ParamValue", TypeRecId = trString });
            Add(cd);

            cd = new TClassTypeCollectionDefinition() { RecId = 2018, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 19, ClassName = "ReservationDetailCollection", ParentRecId = 3000, ItemTypeRecId = 2017 };
            Add(cd);

            cd = new TClassTypeDefinition() { RecId = 2019, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 20, ClassName = "Reservation", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "GlobalId", TypeRecId = trInteger }); // writeInt(FGlobalID, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Id", TypeRecId = trInteger }); // writeInt(FID, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Key", TypeRecId = trString }); // writeString(FKey, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ResourceId", TypeRecId = trInteger }); // writeInt(, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "StartCode", TypeRecId = trInteger }); // writeInt(, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Subject", TypeRecId = trString }); // writeString(, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Location", TypeRecId = trString }); // writeString(, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "StartTime", TypeRecId = trTDateTime }); // writeDate(, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "EndTime", TypeRecId = trTDateTime }); // writeDate(, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "PendingState", TypeRecId = 1017 }); // writeInt(Ord(), Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "PendingError", TypeRecId = trString }); // WriteString(, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ReservationType", TypeRecId = 1012 }); // WriteInt(Ord(), Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Details", TypeRecId = 2017 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "EndType", TypeRecId = 1016 }); // WriteInt(Ord(), Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "History", TypeRecId = trBoolean }); // WriteBool(, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "DeleteWhenSent", TypeRecId = trBoolean }); // WriteBool(, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "AppointmentType", TypeRecId = 1011 }); // WriteInt(Ord(), Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Scope", TypeRecId = 1013 }); // WriteInt(Ord(), Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "MadeFrom", TypeRecId = 1014 }); // WriteInt(Ord(), Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "OriginSystem", TypeRecId = trString }); // WriteString(, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "OriginExecution", TypeRecId = 1015 }); // WriteInt(Ord(), Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "OriginResourceId", TypeRecId = trInteger }); // WriteInt(, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Attachment", TypeRecId = trString }); // WriteString(, Stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ServiceCode", TypeRecId = trString }); // WriteString(, Stream);
            Add(cd);

            cd = new TClassTypeCollectionDefinition() { RecId = 2020, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 21, ClassName = "ReservationCollection", ParentRecId = 3000, ItemTypeRecId = 2019 };
            Add(cd);


            cd = new TClassTypeDefinition() { RecId = 2021, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 22, ClassName = "ReservationUser", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "RecordId", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "UserKey", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Located", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "LocatedError", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Syncronize", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "InstanceRecId", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ReservationList", TypeRecId = 2020 });
            Add(cd);

            //cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Count", TypeRecId = tr }); writeInt(, Stream);

            cd = new TClassTypeDefinition() { RecId = 2022, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 23, ClassName = "NotifyCommand", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Operation", TypeRecId = 1018 });
            Add(cd);

            cd = new TClassTypeDefinition() { RecId = 2023, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 24, ClassName = "StaticData", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ResourceList", TypeRecId = 2014 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "LyncStateList", TypeRecId = 2025 });
            //cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "DeviceList", TypeRecId =  });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "SmallDeviceList", TypeRecId = 2030 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "LargeDeviceList", TypeRecId = 2031 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "StateCurrentList", TypeRecId = 2020 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "StateList", TypeRecId = 2027 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "TemplateList", TypeRecId = 2034 });
            Add(cd);

            cd = new TClassTypeCollectionDefinition() { RecId = 2025, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 26, ClassName = "LyncStateCollection", ParentRecId = 3000, ItemTypeRecId = 2009 };
            Add(cd);


            cd = new TClassTypeDefinition() { RecId = 2026, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 27, ClassName = "State", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "StateId", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Detail", TypeRecId = 2028 }); //.StreamOut(stream);
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Caption", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ShortName", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "SortOrder", TypeRecId = trWord });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ExpectedEnd", TypeRecId = 1019 }); //stream.Write(, sizeof(t3EndingTime)
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "StateLength", TypeRecId = 1020 }); //stream.Write(, sizeof(t3StateLength
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "EndStamp", TypeRecId = 1021 }); //stream.Write(, sizeof(t3EndStamp));
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "OptionsDugiIkkiSett", TypeRecId = trCardinal }); //stream.Write(, sizeof(t3StateOptions));
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "StateType", TypeRecId = 1022 }); //stream.Write(, sizeof(t3StateType));
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "StateClass", TypeRecId = 1023 }); //stream.Write(, sizeof(t3StateClass))
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "SuggestNextProfile", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "SuggestNextProfileName", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "StateColor", TypeRecId = trCardinal });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "CallStatisticsType", TypeRecId = 1024 });
            Add(cd);


            cd = new TClassTypeCollectionDefinition() { RecId = 2027, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 28, ClassName = "StateCollection", ParentRecId = 3000, ItemTypeRecId = 2026 };
            Add(cd);

            cd = new TClassTypeDefinition() { RecId = 2028, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 29, ClassName = "StateDetail", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "StateId", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "SuggestedLength", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "SuggestRoundRule", TypeRecId = 1005 }); //stream.Write(, sizeof(t3SuggestedRule));
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "SuggestRoundType", TypeRecId = 1006 }); //stream.Write(, sizeof(t3SuggestedRoundType));
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Description", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Location", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ForwardToDevice", TypeRecId = 1009 }); //stream.Write(, sizeof(t3ResDetailLink));
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ForwardToNumber", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ForwardToType", TypeRecId = 1007 }); //stream.Write(, sizeof(t3ForwardType));
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "DNDType", TypeRecId = 1008 }); //stream.Write(, sizeOf(t3DNDType));
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "PublicStateId", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "PublishedStateId", TypeRecId = trInteger });
            Add(cd);



            cd = new TClassTypeCollectionDefinition() { RecId = 2029, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 30, ClassName = "StateDetailCollection", ParentRecId = 3000, ItemTypeRecId = 2028 };
            Add(cd);


            cd = new TClassTypeCollectionDefinition() { RecId = 2030, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 31, ClassName = "SmallDeviceList", ParentRecId = 3000, ItemTypeRecId = 2001 };
            Add(cd);

            cd = new TClassTypeCollectionDefinition() { RecId = 2031, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 32, ClassName = "LargeDeviceList", ParentRecId = 3000, ItemTypeRecId = 2003 };
            Add(cd);


            cd = new TClassTypeDefinition() { RecId = 2032, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 33, ClassName = "ChallengeResponse", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Nonce", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "NoncePassword", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "User", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "UserRecId", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "UseWindowsAuthentication", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ExpectedResponse", TypeRecId = 1026 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "HashCoding", TypeRecId = 1027 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ChallengeReason", TypeRecId = 1028 });            
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "AuthenticationToken", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "AuthenticationType", TypeRecId = 1031});
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "AuthenticationPortalURL", TypeRecId = trString });

            Add(cd);

            cd = new TClassTypeDefinition() { RecId = 2033, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 34, ClassName = "Template", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "RecId", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Name", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ShortName", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "NextProfileId", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Current", TypeRecId = 2028 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Next", TypeRecId = 2028 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "SortOrder", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "EndType", TypeRecId = 1016 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "QuickStateOrder", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "QuickStateActive", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "QuickStateShowStateDialog", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "TaskbarOrder", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "TaskbarActive", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ActivateFromClient", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ActivateFromCalendar", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ActivateFromSMS", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ActivateFromSwitchBoard", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ActivateFromSmartClient", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Active", TypeRecId = trBoolean });
            Add(cd);

            cd = new TClassTypeCollectionDefinition() { RecId = 2034, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 35, ClassName = "TemplateList", ParentRecId = 3000, ItemTypeRecId = 2033 };
            Add(cd);

            cd = new TClassTypeDefinition() { RecId = 2035, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 36, ClassName = "ProxyRequest", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "BaseObject", TypeRecId = 2000 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ClientRequestChain", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ExtraInfo", TypeRecId = 2037 });
            Add(cd);            

            cd = new TClassTypeDefinition() { RecId = 2036, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 37, ClassName = "KeyValue", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Key", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Value", TypeRecId = trString });
            Add(cd);

            cd = new TClassTypeCollectionDefinition() { RecId = 2037, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 38, ClassName = "KeyValueList", ParentRecId = 3000, ItemTypeRecId = 2036 };
            Add(cd);

            cd = new TClassTypeDefinition() { RecId = 2038, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 39, ClassName = "ProxyCommand", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "CommandType", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "CommandParams", TypeRecId = 2037 });
            Add(cd);

            cd = new TClassTypeCollectionDefinition() { RecId = 2039, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 40, ClassName = "ReservationUserList", ParentRecId = 3000, ItemTypeRecId = 2021 };
            Add(cd);

            cd = new TClassTypeDefinition() { RecId = 2040, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 41, ClassName = "LoginData", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "UserName", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "UserPassword", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ServerUri", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ServerName", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ServerPort", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "DataValidated", TypeRecId = trBoolean });
            Add(cd);

            cd = new TClassTypeDefinition() { RecId = 2041, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 42, ClassName = "UserPreferences", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ShowProfileImagesInContactsList", TypeRecId = trBoolean });
            Add(cd);

            cd = new TClassTypeDefinition() { RecId = 2042, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 43, ClassName = "ConnectionStartup", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "NetVersion", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ConnectionType", TypeRecId = 1014 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "InstanceName", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ClientDescription", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "MachineName", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ClientVersion", TypeRecId = trString });
            Add(cd);

            cd = new TClassTypeDefinition() { RecId = 2043, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 44, ClassName = "ProxyCommandResult", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Result", TypeRecId = trBoolean });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ResultMessage", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ResultMessageID", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ResultParams", TypeRecId = 2037 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "RequestCommandMessageId", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "RequestCommandType", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "RequestCommandParams", TypeRecId = 2037 });
            Add(cd);

            cd = new TClassTypeDefinition() { RecId = 2044, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 45, ClassName = "UserImage", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "UserRecId", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "UserImage", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "UserImageVersion", TypeRecId = trString });
            Add(cd);

            cd = new TClassTypeCollectionDefinition() { RecId = 2045, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 46, ClassName = "UserImageList", ParentRecId = 3000, ItemTypeRecId = 2044 };
            Add(cd);

            cd = new TClassTypeDefinition() { RecId = 2046, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 47, ClassName = "NumberLog", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "RecID", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "DeviceID", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "PhoneNumber", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "PhoneNumberOwner", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "LogType", TypeRecId = 1029 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "Direction", TypeRecId = 1030 });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "StartTime", TypeRecId = trTDateTime });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "EndTime", TypeRecId = trTDateTime });
            Add(cd);

            cd = new TClassTypeCollectionDefinition() { RecId = 2047, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 48, ClassName = "NumberLogList", ParentRecId = 3000, ItemTypeRecId = 2046 };
            Add(cd);

            cd = new TClassTypeDefinition() { RecId = 2048, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 49, ClassName = "UserNumberLog", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "UserRecID", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "NumberLogList", TypeRecId = 2047 });
            Add(cd);


            cd = new TClassTypeDefinition() { RecId = 2049, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 50, ClassName = "UserAttachmentsDefinition", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ResourceId", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "DefinitionXML", TypeRecId = trString });
            Add(cd);

            cd = new TClassTypeDefinition() { RecId = 2050, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 52, ClassName = "UserAttachmentsData", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ResourceID", TypeRecId = trInteger });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "StateID", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "AttachmentId", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ParentFieldId", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ParentId", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "DataXML", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ClientId", TypeRecId = trString });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ParentUserData", TypeRecId = trString });
            Add(cd);



            cd = new TClassTypeDefinition() { RecId = 2051, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 53, ClassName = "UserAttachmentsDataLastUsed", ParentRecId = 2000 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "LastUsed", TypeRecId = trTDateTime });
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "DataXML", TypeRecId = trString });
            Add(cd);

            cd = new TClassTypeCollectionDefinition() { RecId = 2052, ClassEnum = TSimpleTypeDefinition.FirstClassEnum + 54, ClassName = "UserAttachmentsDataLastUsedCollection", ParentRecId = 3000, ItemTypeRecId = 2051 };
            cd.ClassFieldList.Add(new TClassTypeFieldDefinition() { FieldName = "ResourceId", TypeRecId = trInteger });
            Add(cd);

            #endregion
        }

    internal string GenerateDelphiEnumStreaming()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("unit ProxyEnumStreamUnit;");
            sb.AppendLine("");
            sb.AppendLine("interface");
            sb.AppendLine("uses ProxyBaseStreamUnit, System.Classes;");
            sb.AppendLine("");

            foreach (TBaseTypeDefinition type in this)
            {
                if (type is TEnumTypeDefinition)
                {
                    TEnumTypeDefinition def = type as TEnumTypeDefinition;
                    sb.AppendLine("type");
                    sb.AppendLine("  " + def.DelphiPrefix + def.Name + " = (");

                    for (int i = 0; i < def.Values.Count; i++)
                    {
                        TEnumTypeValue value = def.Values[i];
                        if (i < def.Values.Count - 1)
                            sb.AppendLine("    " + def.DelphiItemPrefix + value.Name + ", ");
                        else
                            sb.AppendLine("    " + def.DelphiItemPrefix + value.Name);
                    }
                    sb.AppendLine("  );");
                    sb.AppendLine("");
                }
            }

            sb.AppendLine("  type ProxyEnumStream = class(ProxyBasicStream)");
            foreach (TBaseTypeDefinition type in this)
            {
                if (type is TEnumTypeDefinition)
                {
                    TEnumTypeDefinition def = type as TEnumTypeDefinition;
                    string DelphiTypeName = def.DelphiPrefix + def.Name;
                    sb.AppendLine("    class procedure Write" + DelphiTypeName + "(Stream: TStream; PropertyName: String; Value: " + DelphiTypeName + ");");
                    sb.AppendLine("    class function Read" + DelphiTypeName + "(Stream: TStream; PropertyName: String; MaxPos:Integer): " + DelphiTypeName + ";");
                }
            }
            sb.AppendLine("  end;");
            sb.AppendLine("");
            sb.AppendLine("implementation");


            foreach (TBaseTypeDefinition type in this)
            {
                if (type is TEnumTypeDefinition)
                {
                    TEnumTypeDefinition def = type as TEnumTypeDefinition;
                    string DelphiTypeName = def.DelphiPrefix + def.Name;

                    sb.AppendLine("class procedure ProxyEnumStream.Write" + DelphiTypeName + "(Stream: TStream; PropertyName: String; Value: " + DelphiTypeName + ");");
                    sb.AppendLine("begin");
                    sb.AppendLine("  WriteInteger(Stream, PropertyName, ord(Value))");
                    sb.AppendLine("end;");
                    sb.AppendLine("");


                    sb.AppendLine("class function ProxyEnumStream.Read" + DelphiTypeName + "(Stream: TStream; PropertyName: String; MaxPos:Integer): " + DelphiTypeName + ";");
                    sb.AppendLine("begin");
                    sb.AppendLine("  result := " + DelphiTypeName + "(ReadInteger(Stream, PropertyName, MaxPos))");
                    sb.AppendLine("end;");
                    sb.AppendLine("");
                }
            }
            sb.AppendLine("");
            sb.AppendLine("end.");



            return sb.ToString();
        }

        internal string GenerateCSharpEnumStreaming()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("// ProxyEnumStreamUnit.cs");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.IO;");
            sb.AppendLine("using System.Threading.Tasks;");
            sb.AppendLine("");
            sb.AppendLine("namespace " + TargetNameSpace);
            sb.AppendLine("{");


            foreach (TBaseTypeDefinition type in this)
            {
                if (type is TEnumTypeDefinition)
                {
                    TEnumTypeDefinition def = type as TEnumTypeDefinition;
                    string CSharpTypeName = def.CSPrefix + def.Name;
                    sb.AppendLine("  public enum " + def.CSPrefix + CSharpTypeName + " { // RecId: " + def.RecId);
                    int cnt = 0;
                    foreach (TEnumTypeValue value in def.Values)
                    {
                        sb.AppendLine("    " + def.CSItemPrefix + value.Name + " = " + cnt.ToString() + ",");
                        cnt += 1;
                    }
                    sb.AppendLine("  }");
                    sb.AppendLine("");
                }
            }

            sb.AppendLine("");
            sb.AppendLine("    public class ProxyEnumStream : ProxyBaseStream");
            sb.AppendLine("    {");

            foreach (TBaseTypeDefinition type in this)
            {
                if (type is TEnumTypeDefinition)
                {
                    TEnumTypeDefinition def = type as TEnumTypeDefinition;
                    string CSharpTypeName = def.CSPrefix + def.Name;

                    sb.AppendLine("        public static " + CSharpTypeName + " Read" + CSharpTypeName + "(Stream stream, String PropertyName, int MaxPos)");
                    sb.AppendLine("        {");
                    sb.AppendLine("            return (" + CSharpTypeName + ")ReadInt32(stream, PropertyName, MaxPos);");
                    sb.AppendLine("        }");
                    sb.AppendLine("");
                    sb.AppendLine("        public static void Write" + CSharpTypeName + "(Stream stream, string PropertyName, " + CSharpTypeName + " value)");
                    sb.AppendLine("        {");
                    sb.AppendLine("            WriteInt32(stream, PropertyName, Convert.ToInt32(value));");
                    sb.AppendLine("        }");
                    sb.AppendLine("");
                }
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");
            return sb.ToString();
        }

        internal string GenerateProto()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("syntax = \"proto3\";");
            sb.AppendLine("");
            sb.AppendLine("import \"google/protobuf/empty.proto\";");
            sb.AppendLine("import \"google/protobuf/timestamp.proto\";");

            
            sb.AppendLine("");
            sb.AppendLine("option csharp_namespace = \"Totalview.Server\";");
            sb.AppendLine("");
            sb.AppendLine("package totalview;");
            sb.AppendLine("");
            sb.AppendLine("service Totalview {");
            sb.AppendLine("  rpc Connect(google.protobuf.Empty) returns (stream StreanPacket);");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("message StreanPacket {");
            sb.AppendLine("  // all classes here");
            sb.AppendLine("}");

            foreach (TBaseTypeDefinition type in this)
            {

                if(type is TEnumTypeDefinition def)
                {
                    sb.AppendLine("");
                    sb.AppendLine($"enum {def.CSTypeName()} {{");

                    int index = 0;
                    foreach (TEnumTypeValue value in def.Values)
                    {
                        sb.AppendLine("  " + def.CSTypeName().ToUpper() + "_" + value.Name.ToUpper() + " = " + index + ";");
                        index++;
                    }

                    sb.AppendLine("}");
                }
                else if (type is TClassTypeDefinition)
                {
                    TClassTypeDefinition cd = type as TClassTypeDefinition;
                    TClassTypeDefinition itemCD = null;
                    if (cd.CustomImplementationExists)
                        continue;

                    // SKIP
                    if (cd.GetProtoStreamClassName() == "ProxyRequest")
                        continue;
                    if (cd.GetProtoStreamClassName() == "ChallengeResponse")
                        continue;

                    TClassTypeDefinition parentCD = null;
                    if (cd.ParentRecId > 0)
                        parentCD = this.FirstOrDefault(a => a.RecId == cd.ParentRecId) as TClassTypeDefinition;

                    sb.AppendLine("");

                    if(parentCD is TClassTypeCollectionDefinition)
                    {
                        itemCD = this.FirstOrDefault(a => a.RecId == (cd as TClassTypeCollectionDefinition).ItemTypeRecId) as TClassTypeDefinition;
                        
                        sb.AppendLine($"message {cd.GetProtoStreamClassName()} {{");
                        sb.AppendLine($"    repeated {itemCD.GetProtoStreamClassName()} Items = 1;");
                        sb.AppendLine("}");
                    }
                    else
                    {
                        sb.AppendLine($"message {cd.GetProtoStreamClassName()} {{");

                        // FieldDefinition
                        int index = 1;
                        foreach (TClassTypeFieldDefinition cf in cd.ClassFieldList)
                        {
                            var fieldType = this.FirstOrDefault(a => a.RecId == cf.TypeRecId);

                            string fieldName = cf.FieldName.Substring(0, 1).ToLower() + cf.FieldName.Substring(1);
                            sb.AppendLine("  " + fieldType.ProtoDeclareFieldOfType(fieldName, index));
                            index++;
                        }
                        sb.AppendLine("}");
                    }
                }

            }

            return sb.ToString();
        }

        internal string GenerateCSharpObjectStream()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbAbstractCommands = new StringBuilder();

            sb.AppendLine("// ProxyObjectStreamUnit.cs");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.IO;");
            sb.AppendLine("using System.Threading.Tasks;");
            sb.AppendLine("using System.Diagnostics;");
            sb.AppendLine("");
            sb.AppendLine("namespace " + TargetNameSpace);
            sb.AppendLine("{");
            #region "ProxyStream Object"
            sb.AppendLine("     public class ProxyStream : ProxyEnumStream { ");
            foreach (TBaseTypeDefinition type in this)
            {
                if (type is TClassTypeDefinition)
                {
                    TClassTypeDefinition cd = type as TClassTypeDefinition;
                    if (cd.CustomImplementationExists)
                        continue;

                    sb.AppendLine("       public static " + cd.GetCSStreamClassName() + " Read" + cd.GetCSStreamClassName() + "(Stream stream, String PropertyName) {");
                    sb.AppendLine("         " + cd.GetCSStreamClassName() + " result = new " + cd.GetCSStreamClassName() + "();");
                    sb.AppendLine("         result.StreamIn(stream, PropertyName);");
                    sb.AppendLine("         return result;");
                    sb.AppendLine("       }");
                    sb.AppendLine("");

                    sb.AppendLine("       public static void Write" + cd.GetCSStreamClassName() + "(Stream stream, String PropertyName, " + cd.GetCSStreamClassName() + " Value) {");
                    sb.AppendLine("         Value.StreamOut(stream, PropertyName);");
                    sb.AppendLine("       }");
                    sb.AppendLine("");
                };
            };


            // Instantiera útfrá ClassEnum í stream:
            sb.AppendLine("       public static T5StreamBaseObject CreateT5BaseObjectFromClassEnum(int ClassEnum, Stream stream, String PropertyName)");
            sb.AppendLine("       {");
            sb.AppendLine("           switch (ClassEnum) {");
            foreach (TBaseTypeDefinition type in this)
            {
                if (type is TClassTypeDefinition)
                {
                    TClassTypeDefinition cd = type as TClassTypeDefinition;
                    if (cd.CustomImplementationExists)
                        continue;

                    sb.AppendLine("               case " + cd.ClassEnum + ":");
                    sb.AppendLine("                   return Read" + cd.GetCSStreamClassName() + "(stream, PropertyName);");
                }
            }

            sb.AppendLine("               default:");
            sb.AppendLine("                   // Class Enum unknown");
            sb.AppendLine("                   Debugger.Break();");
            sb.AppendLine("                   return null;");
            sb.AppendLine("           }");

            sb.AppendLine("       }");
            sb.AppendLine("");

            // Instantiera útfrá ClassEnum í stream:
            sb.AppendLine("       public static T5StreamBaseObject CreateFromStream(Stream stream, String PropertyName)");
            sb.AppendLine("       {");
            sb.AppendLine("           T5StreamBaseObject baseObj = new T5StreamBaseObject();");
            sb.AppendLine("           long pos = stream.Position;");
            sb.AppendLine("           baseObj.StreamIn(stream, PropertyName);");
            sb.AppendLine("           stream.Position = pos;");
            sb.AppendLine("           int classEnum = baseObj.ClassEnum;");
            sb.AppendLine("");
            sb.AppendLine("           return CreateT5BaseObjectFromClassEnum(classEnum, stream, PropertyName);");
            sb.AppendLine("        }");
            sb.AppendLine("");




            sb.AppendLine("     }");
            sb.AppendLine("");
            #endregion

            foreach (TBaseTypeDefinition type in this)
            {
                if (type is TClassTypeDefinition)
                {
                    TClassTypeDefinition cd = type as TClassTypeDefinition;
                    TClassTypeDefinition itemCD = null;
                    if (cd.CustomImplementationExists)
                        continue;

                    TClassTypeDefinition parentCD = null;
                    if (cd.ParentRecId > 0)
                        parentCD = this.FirstOrDefault(a => a.RecId == cd.ParentRecId) as TClassTypeDefinition;

                    if (parentCD != null)
                    {
                        if (parentCD is TClassTypeCollectionDefinition)
                        {
                            itemCD = this.FirstOrDefault(a => a.RecId == (cd as TClassTypeCollectionDefinition).ItemTypeRecId) as TClassTypeDefinition;

                            sb.AppendLine("  public class " + cd.GetCSStreamClassName() + " : " + parentCD.GetCSStreamClassName() + "<" + itemCD.GetCSStreamClassName() + "> {");
                        }
                        else
                            sb.AppendLine("  public class " + cd.GetCSStreamClassName() + " : " + parentCD.GetCSStreamClassName() + " {");
                    }
                    else
                        sb.AppendLine("  public class " + cd.GetCSStreamClassName() + " {");


                    // FieldDefinition
                    foreach (TClassTypeFieldDefinition cf in cd.ClassFieldList)
                    {
                        var fieldType = this.FirstOrDefault(a => a.RecId == cf.TypeRecId);
                        sb.AppendLine(fieldType.CSDeclareFieldOfType(cf.FieldName, true));
                    }
                    sb.AppendLine("");

                    // ClassEnum...
                    sb.AppendLine("     public override int GetClassEnum() {");
                    sb.AppendLine("         return " + cd.ClassEnum + ";");
                    sb.AppendLine("     }");
                    sb.AppendLine("");

                    // StreamIn
                    sb.AppendLine("    public override void StreamIn(Stream stream, String PropertyName) {");
                    sb.AppendLine("      base.StreamIn(stream, PropertyName);");
                    foreach (TClassTypeFieldDefinition cf in cd.ClassFieldList)
                    {
                        var fieldType = this.FirstOrDefault(a => a.RecId == cf.TypeRecId);
                        if (cd.GetCSStreamClassName() == "T5StreamProxyRequest" && cf.FieldName == "BaseObject")
                        {
                            sb.AppendLine("      T5StreamBaseObject baseObj = new T5StreamBaseObject();");
                            sb.AppendLine("      long pos = stream.Position;");
                            sb.AppendLine("      baseObj.StreamIn(stream, PropertyName);");
                            sb.AppendLine("      stream.Position = pos;");
                            sb.AppendLine("      int classEnum = baseObj.ClassEnum;");
                            sb.AppendLine("      BaseObject = ProxyStream.CreateT5BaseObjectFromClassEnum(classEnum, stream, PropertyName);");
                        }
                        else
                            sb.AppendLine("      " + fieldType.CSStreamInMethod(cf.FieldName));
                    }
                    sb.AppendLine("    if (ClassEnum == "+ cd.ClassEnum + ")");
                    sb.AppendLine("      MoveToEnd(stream);");
                    sb.AppendLine("    }");
                    sb.AppendLine("");


                    // StreamOut
                    sb.AppendLine("    public override void StreamOut(Stream stream, String PropertyName) {");
                    sb.AppendLine("      ClassEnum = GetClassEnum();");
                    sb.AppendLine("      base.StreamOut(stream, PropertyName);");
                    foreach (TClassTypeFieldDefinition cf in cd.ClassFieldList)
                    {
                        var fieldType = this.FirstOrDefault(a => a.RecId == cf.TypeRecId);
                        sb.AppendLine("      " + fieldType.CSStreamOutMethod(cf.FieldName));
                    }
                    sb.AppendLine("      SetPacketSize(stream);");
                    sb.AppendLine("    }");
                    sb.AppendLine("  }");
                    sb.AppendLine("");
                }

            }

            sb.AppendLine("}");

            return sb.ToString();
        }

        internal string GenerateDelphiObjectStream()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("unit ProxyObjectStreamUnit;");
            sb.AppendLine("");
            sb.AppendLine("interface");
            sb.AppendLine("uses System.Classes, System.SysUtils, ProxyBaseStreamUnit, ProxyEnumStreamUnit, Winapi.Windows;");
            sb.AppendLine("");
            sb.AppendLine("type");

            // Forward declarations
            foreach (TBaseTypeDefinition type in this)
            {
                if (type is TClassTypeDefinition)
                {
                    TClassTypeDefinition cd = type as TClassTypeDefinition;
                    if (cd.CustomImplementationExists)
                        continue;
                    sb.AppendLine("  " + cd.GetDelphiStreamClassName() + " = class;");
                }
            }
            sb.AppendLine("");

            // ProxyStream declaration
            sb.AppendLine("  ProxyStream = class(ProxyEnumStream)");
            foreach (TBaseTypeDefinition type in this)
            {
                if (type is TEnumTypeDefinition)
                {
                    TEnumTypeDefinition ed = type as TEnumTypeDefinition;
                    string classname = ed.DelphiPrefix + ed.Name;
                    sb.AppendLine("    class function Read" + classname + "(Stream: TStream; PropertyName: String; MaxPos:Integer): " + classname + ";");
                    sb.AppendLine("    class procedure Write" + classname + "(Stream: TStream; PropertyName: String; Value: " + classname + ");");

                }
                if (type is TClassTypeDefinition)
                {
                    TClassTypeDefinition cd = type as TClassTypeDefinition;
                    if (cd.CustomImplementationExists)
                        continue;
                    sb.AppendLine("    class function Read" + cd.GetDelphiStreamClassName() + "(Stream: TStream; PropertyName: String): " + cd.GetDelphiStreamClassName() + ";");
                    sb.AppendLine("    class procedure Write" + cd.GetDelphiStreamClassName() + "(Stream: TStream; PropertyName: String; Value: " + cd.GetDelphiStreamClassName() + ");");
                }
            }
            sb.AppendLine("  end;");

            // Class Declaration
            foreach (TBaseTypeDefinition type in this)
            {
                if (type is TClassTypeDefinition)
                {
                    TClassTypeDefinition cd = type as TClassTypeDefinition;
                    TClassTypeDefinition itemCD = null;
                    if (cd.CustomImplementationExists)
                        continue;

                    TClassTypeDefinition parentCD = null;
                    if (cd.ParentRecId > 0)
                        parentCD = this.FirstOrDefault(a => a.RecId == cd.ParentRecId) as TClassTypeDefinition;

                    if (parentCD != null)
                    {
                        if (parentCD is TClassTypeCollectionDefinition)
                        {
                            itemCD = this.FirstOrDefault(a => a.RecId == (cd as TClassTypeCollectionDefinition).ItemTypeRecId) as TClassTypeDefinition;

                            sb.AppendLine("  " + cd.GetDelphiStreamClassName() + " = class(" + parentCD.GetDelphiStreamClassName() + "<" + itemCD.GetDelphiStreamClassName() + ">)");
                        }
                        else
                            sb.AppendLine("  " + cd.GetDelphiStreamClassName() + " = class(" + parentCD.GetDelphiStreamClassName() + ")");
                    }
                    else
                        sb.AppendLine("  " + cd.GetDelphiStreamClassName() + " = class");

                    sb.AppendLine("  public");
                    //
                    // FieldDefinition
                    foreach (TClassTypeFieldDefinition cf in cd.ClassFieldList)
                    {
                        var fieldType = this.FirstOrDefault(a => a.RecId == cf.TypeRecId);
                        sb.AppendLine(fieldType.DelphiDeclareFieldOfType(cf.FieldName));
                    }

                    // Method declarations
                    sb.AppendLine("    constructor Create; override;");
                    sb.AppendLine("    destructor Destroy; override;");
                    sb.AppendLine("    procedure StreamIn(Stream: TStream; PropertyName: String); override;");
                    sb.AppendLine("    procedure StreamOut(Stream: TStream; PropertyName: String); override;");
                    sb.AppendLine("    function GetClassEnum: integer; override;");

                    // End declaration
                    sb.AppendLine("  end;");
                    sb.AppendLine("");

                }
            }


            sb.AppendLine("");
            sb.AppendLine("function StreamToT5BaseObject(Stream: TStream; PropertyName: String; StreamIn: Boolean = true): T5StreamBaseObject;");
            sb.AppendLine("function CreateT5BaseObjectFromClassEnum(ClassEnum: integer): T5StreamBaseObject;");


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Delphi object implementation
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////

            sb.AppendLine("");
            sb.AppendLine("implementation");
            sb.AppendLine("");

            // ProxyStream implementation
            sb.AppendLine("{ ProxyStream } ");
            foreach (TBaseTypeDefinition type in this)
            {
                if (type is TEnumTypeDefinition)
                {
                    TEnumTypeDefinition ed = type as TEnumTypeDefinition;
                    string classname = ed.DelphiPrefix + ed.Name;
                    sb.AppendLine("class function ProxyStream.Read" + classname + "(Stream: TStream; PropertyName: String; MaxPos:Integer): " + classname + ";");
                    sb.AppendLine("begin");
                    sb.AppendLine("  result := " + classname + "(ReadInteger(Stream, PropertyName, MaxPos));");
                    sb.AppendLine("end;");
                    sb.AppendLine("");

                    sb.AppendLine("class procedure ProxyStream.Write" + classname + "(Stream: TStream; PropertyName: String; Value: " + classname + ");");
                    sb.AppendLine("begin");
                    sb.AppendLine("  WriteInteger(Stream, PropertyName, ord(Value));");
                    sb.AppendLine("end;");
                    sb.AppendLine("");

                }
                if (type is TClassTypeDefinition)
                {
                    TClassTypeDefinition cd = type as TClassTypeDefinition;
                    if (cd.CustomImplementationExists)
                        continue;
                    sb.AppendLine("class function ProxyStream.Read" + cd.GetDelphiStreamClassName() + "(Stream: TStream; PropertyName: String): " + cd.GetDelphiStreamClassName() + ";");
                    sb.AppendLine("begin");
                    sb.AppendLine("  result := " + cd.GetDelphiStreamClassName() + ".Create;");
                    sb.AppendLine("  result.StreamIn(Stream, PropertyName)");
                    sb.AppendLine("end;");
                    sb.AppendLine("");

                    sb.AppendLine("class procedure ProxyStream.Write" + cd.GetDelphiStreamClassName() + "(Stream: TStream; PropertyName: String; Value: " + cd.GetDelphiStreamClassName() + ");");
                    sb.AppendLine("begin");
                    sb.AppendLine("  Value.StreamOut(Stream, PropertyName)");
                    sb.AppendLine("end;");
                    sb.AppendLine("");

                }
            }


            foreach (TBaseTypeDefinition type in this)
            {
                TClassTypeDefinition cd = null;
                TClassTypeDefinition itemCD = null;
                TClassTypeDefinition parentCD = null;

                if (type is TClassTypeDefinition)
                {
                    cd = type as TClassTypeDefinition;
                    itemCD = null;
                    if (cd.CustomImplementationExists)
                        continue;


                    if (cd.ParentRecId > 0)
                        parentCD = this.FirstOrDefault(a => a.RecId == cd.ParentRecId) as TClassTypeDefinition;

                    if (parentCD != null)
                    {
                        if (parentCD is TClassTypeCollectionDefinition)
                        {
                            itemCD = this.FirstOrDefault(a => a.RecId == (cd as TClassTypeCollectionDefinition).ItemTypeRecId) as TClassTypeDefinition;
                        }
                    }

                    sb.AppendLine("{ " + cd.GetDelphiStreamClassName() + " }");
                    sb.AppendLine("");
                    sb.AppendLine("constructor " + cd.GetDelphiStreamClassName() + ".Create;");
                    sb.AppendLine("begin");
                    sb.AppendLine("  inherited;");
                    foreach (TClassTypeFieldDefinition cf in cd.ClassFieldList)
                    {
                        var fieldType = this.FirstOrDefault(a => a.RecId == cf.TypeRecId);
                        if (fieldType is TClassTypeDefinition)
                        {
                            TClassTypeDefinition cd2 = fieldType as TClassTypeDefinition;
                            sb.AppendLine("  " + cf.FieldName + " := " + cd2.GetDelphiStreamClassName() + ".Create;");
                        }
                    }
                    sb.AppendLine("");
                    sb.AppendLine("end;");
                    sb.AppendLine("");
                    sb.AppendLine("destructor " + cd.GetDelphiStreamClassName() + ".Destroy;");
                    sb.AppendLine("begin");
                    sb.AppendLine("  inherited;");
                    foreach (TClassTypeFieldDefinition cf in cd.ClassFieldList)
                    {
                        var fieldType = this.FirstOrDefault(a => a.RecId == cf.TypeRecId);
                        if (fieldType is TClassTypeDefinition)
                            sb.AppendLine("  if assigned(" + cf.FieldName + ") then " + cf.FieldName + ".Destroy;");
                    }
                    sb.AppendLine("");
                    sb.AppendLine("end;");
                    sb.AppendLine("");
                    sb.AppendLine("procedure " + cd.GetDelphiStreamClassName() + ".StreamIn(Stream: TStream; PropertyName: String);");
                    if (cd.GetDelphiStreamClassName() == "T5StreamProxyRequest")
                        sb.AppendLine("var OldPos : Integer; Base : T5StreamBaseObject;");
                    sb.AppendLine("begin");
                    sb.AppendLine("  inherited;");
                    foreach (TClassTypeFieldDefinition cf in cd.ClassFieldList)
                    {
                        var fieldType = this.FirstOrDefault(a => a.RecId == cf.TypeRecId);

                        if (fieldType is TClassTypeDefinition)
                        {
                            if (cf.FieldName == "BaseObject" && cd.GetDelphiStreamClassName() == "T5StreamProxyRequest")
                            {
                                sb.AppendLine("  BaseObject.Free;");
                                sb.AppendLine("  OldPos:= Stream.Position;");
                                sb.AppendLine("  Base:= T5StreamBaseObject.Create;");
                                sb.AppendLine("  Base.StreamIn(stream, 'Object');");
                                sb.AppendLine("  Stream.Position := OldPos;");
                                sb.AppendLine("  BaseObject:= CreateT5BaseObjectFromClassEnum(base.ClassEnum);");
                                sb.AppendLine("  Base.free;");
                                sb.AppendLine("  BaseObject.StreamIn(Stream, PropertyName);");
                            }
                            else
                                sb.AppendLine("  " + cf.FieldName + ".StreamIn(Stream, PropertyName);");
                        }
                        else
                            sb.AppendLine("  " + fieldType.DelphiStreamInMethod(cf.FieldName));
                    }
                    //sb.AppendLine("");
                    sb.AppendLine("  if ClassEnum = " + cd.ClassEnum + " then");
                    sb.AppendLine("    MoveToEnd(stream);");
                    sb.AppendLine("end;");
                    sb.AppendLine("");
                    sb.AppendLine("procedure " + cd.GetDelphiStreamClassName() + ".StreamOut(Stream: TStream; PropertyName: String);");
                    sb.AppendLine("begin");
                    sb.AppendLine("  inherited;");
                    foreach (TClassTypeFieldDefinition cf in cd.ClassFieldList)
                    {
                        var fieldType = this.FirstOrDefault(a => a.RecId == cf.TypeRecId);
                        if (fieldType is TClassTypeDefinition)
                            sb.AppendLine("  " + cf.FieldName + ".StreamOut(Stream, PropertyName);");
                        else
                            sb.AppendLine("  " + fieldType.DelphiStreamOutMethod(cf.FieldName));
                    }
                    //sb.AppendLine("");
                    sb.AppendLine("  SetPacketSize(stream);");
                    sb.AppendLine("end;");
                    sb.AppendLine("");
                    sb.AppendLine("function " + cd.GetDelphiStreamClassName() + ".GetClassEnum: integer; ");
                    sb.AppendLine("begin");
                    sb.AppendLine("  result := " + cd.ClassEnum);
                    sb.AppendLine("end;");
                    sb.AppendLine("");
                }
            }

            sb.AppendLine("");


            sb.AppendLine("function CreateT5BaseObjectFromClassEnum(ClassEnum: integer): T5StreamBaseObject;");
            sb.AppendLine("begin");
            sb.AppendLine("  result := nil;");
            sb.AppendLine("  case ClassEnum of");
            foreach (TBaseTypeDefinition type in this)
            {
                if (type is TClassTypeDefinition)
                {
                    TClassTypeDefinition cd = type as TClassTypeDefinition;
                    //       TClassTypeDefinition itemCD = null;
                    if (cd.CustomImplementationExists)
                        continue;
                    sb.AppendLine("    " + cd.ClassEnum + ": result := " + cd.DelphiTypeName() + ".Create;");
                }
            }
            sb.AppendLine("  end;");
            sb.AppendLine("end;");
            sb.AppendLine("");



            sb.AppendLine("");


            sb.AppendLine("function StreamToT5BaseObject(Stream: TStream; PropertyName: String; StreamIn: Boolean = true): T5StreamBaseObject;");
            sb.AppendLine("var base: T5StreamBaseObject; oldpos: Cardinal;");
            sb.AppendLine("begin");
            sb.AppendLine("  oldPos := stream.Position;");
            sb.AppendLine("  base := T5StreamBaseObject.Create;");
            sb.AppendLine("  base.StreamIn(stream, 'Object');");
            sb.AppendLine("  stream.Position := oldPos;");
            sb.AppendLine("");
            sb.AppendLine("  result := CreateT5BaseObjectFromClassEnum(base.ClassEnum);");
            sb.AppendLine("");
            sb.AppendLine("  if (result <> nil) and (StreamIn) then");
            sb.AppendLine("    result.StreamIn(Stream, PropertyName);");
            sb.AppendLine("end;");
            sb.AppendLine("");



            sb.AppendLine("end.");

            return sb.ToString();
        }

        public XElement GetAsXml()
        {
            XElement result = new XElement("TYPELIST");
            result.Add(new XAttribute("TargetNameSpace", TargetNameSpace));
            foreach (TBaseTypeDefinition type in this)
            {
                XElement node = new XElement("TYPE");
                result.Add(node);
                type.SaveToXml(node);
            }
            return result;
        }

        internal void SetAsXml(XElement xml)
        {
            Clear();
            TargetNameSpace = (string)xml.Attribute("TargetNameSpace");
            foreach (XElement XType in xml.Elements("TYPE"))
            {
                string XClass = XType.Attribute("CLASS").Value;

                TBaseTypeDefinition td;
                if (XClass == "TSimpleTypeDefinition")
                    td = new TSimpleTypeDefinition();
                else if (XClass == "TEnumTypeDefinition")
                    td = new TEnumTypeDefinition();
                else if (XClass == "TClassTypeDefinition")
                    td = new TClassTypeDefinition();
                else if (XClass == "TClassTypeCollectionDefinition")
                    td = new TClassTypeCollectionDefinition();
                else if (XClass == "TCommandTypeDefinition")
                    td = new TCommandTypeDefinition();
                else
                {
                    MessageBox.Show("Invalid Class: " + XClass);
                    return;
                }

                td.LoadFromXml(XType);
                Add(td);
            }
        }

        internal string GenerateCSharpUIObjects()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("// ProxyUIObjectUnit.cs");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.IO;");
            sb.AppendLine("using System.Threading.Tasks;");
            sb.AppendLine("using System.Diagnostics;");
            sb.AppendLine("");
            sb.AppendLine("namespace " + TargetNameSpace);
            sb.AppendLine("{");

            foreach (TBaseTypeDefinition type in this)
            {
                if (type is TClassTypeDefinition)
                {
                    TClassTypeDefinition cd = type as TClassTypeDefinition;
                    //TClassTypeDefinition itemCD = null;
                    if (cd.CustomImplementationExists)
                        continue;

                    TClassTypeDefinition parentCD = null;
                    if (cd.ParentRecId > 0)
                        parentCD = this.FirstOrDefault(a => a.RecId == cd.ParentRecId) as TClassTypeDefinition;


                    //if (parentCD != null)
                    //    sb.AppendLine("    public class " + cd.GetCSUIClassName() + ": T5UIBaseObject<" + cd.GetCSStreamClassName() + ">, " + cd.GetCSStreamClassName());
                    //else
                    //    sb.AppendLine("    public class " + cd.GetCSUIClassName() + ": T5UIBaseObject<" + cd.GetCSStreamClassName() + ">");                    
                    sb.AppendLine("    public partial class " + cd.GetCSUIClassName() + ": T5UIBaseObject<" + cd.GetCSStreamClassName() + ">");
                    sb.AppendLine("    {");



                    // FieldDefinition
                    foreach (TClassTypeFieldDefinition cf in cd.GetTotalClassFieldList(this)) // cd.ClassFieldList)//  cd.GetTotalClassFieldList(this)) //
                    {
                        //var fieldType = this.FirstOrDefault(a => a.RecId == cf.TypeRecId);
                        //sb.AppendLine("     " + fieldType.CSDeclareFieldOfType(cf.FieldName));

                        var fieldType = this.FirstOrDefault(a => a.RecId == cf.TypeRecId);

                        if (fieldType is TSimpleTypeDefinition)
                        {
                            TSimpleTypeDefinition std = fieldType as TSimpleTypeDefinition;
                            sb.AppendLine("     public " + std.CSName + " " + cf.FieldName + " { get { return Data." + cf.FieldName + ";} set { Data." + cf.FieldName + " = value; TriggerPropertyChanged(\"" + cf.FieldName + "\");}} ");
                        }
                        else
                            if (fieldType is TEnumTypeDefinition)
                        {
                            TEnumTypeDefinition td = fieldType as TEnumTypeDefinition;
                            sb.AppendLine("     public " + td.CSPrefix + td.Name + " " + cf.FieldName + " { get { return Data." + cf.FieldName + ";} set { Data." + cf.FieldName + " = value; TriggerPropertyChanged(\"" + cf.FieldName + "\");}} ");
                        }
                        else
                                if (fieldType is TClassTypeCollectionDefinition)
                        {
                            TClassTypeCollectionDefinition ctcd = fieldType as TClassTypeCollectionDefinition;
                            //sb.AppendLine("     public " + ctd.GetCSUIClassName() + " " + cf.FieldName + " { get { return Data." + cf.FieldName + ";} set { Data." + cf.FieldName + " = value; TriggerPropertyChanged(\"" + cf.FieldName + "\");}} ");
                            sb.AppendLine("     public " + ctcd.GetCSStreamClassName() + " " + cf.FieldName + " { get { return Data." + cf.FieldName + ";} set { Data." + cf.FieldName + " = value; TriggerPropertyChanged(\"" + cf.FieldName + "\");}} ");
                        }
                        else
                                    if (fieldType is TClassTypeDefinition)
                        {
                            TClassTypeDefinition ctd = fieldType as TClassTypeDefinition;
                            //sb.AppendLine("     public " + ctd.GetCSUIClassName() + " " + cf.FieldName + " { get { return Data." + cf.FieldName + ";} set { Data." + cf.FieldName + " = value; TriggerPropertyChanged(\"" + cf.FieldName + "\");}} ");
                            sb.AppendLine("     public " + ctd.GetCSStreamClassName() + " " + cf.FieldName + " { get { return Data." + cf.FieldName + ";} set { Data." + cf.FieldName + " = value; TriggerPropertyChanged(\"" + cf.FieldName + "\");}} ");
                        }


                    }
                    sb.AppendLine("");

                    // Constructor
                    sb.AppendLine("    public " + cd.GetCSUIClassName() + "()");
                    sb.AppendLine("    {");
                    foreach (TClassTypeFieldDefinition cf in cd.GetTotalClassFieldList(this))// cd.ClassFieldList)
                        sb.AppendLine("      TriggerAll_PropertyNameList.Add(\"" + cf.FieldName + "\");");
                    sb.AppendLine("    }");
                    sb.AppendLine("");


                    //// ClassEnum...
                    //sb.AppendLine("     public string HerErTest() {");
                    //sb.AppendLine("         return \"Test\";");
                    //sb.AppendLine("     }");

                    ///
                    sb.AppendLine("   }");
                    sb.AppendLine("");
                }
            }
            sb.AppendLine("}");
            return sb.ToString();


        }

        
    }

    public class TClassTypeParameterDefinitionList : TClassTypeFieldDefinitionList { }

    public class TCommandTypeDefinition : TBaseTypeDefinition
    {
        public string MethodName { get; set; }
        TClassTypeParameterDefinitionList _ParameterList = new TClassTypeParameterDefinitionList();
        public TClassTypeParameterDefinitionList ParameterList { get { return _ParameterList; } set { _ParameterList = value; } }

        internal string DelphiInputParamterDeclaration(TypeDefinitionList typeDefinitionList)
        {
            string result = "";
            foreach (TClassTypeFieldDefinition Parameter in ParameterList)
            {
                TBaseTypeDefinition btd = typeDefinitionList.FirstOrDefault(a => a.RecId == Parameter.TypeRecId);
                result = result + "; " + Parameter.FieldName + ": " + btd.DelphiTypeName();
            }
            if (result != "")
                return result.Remove(0, 2);
            else
                return result;
        }

        internal string DelphiInputParamterList(TypeDefinitionList typeDefinitionList)
        {
            string result = "";
            foreach (TClassTypeFieldDefinition Parameter in ParameterList)
            {
                TBaseTypeDefinition btd = typeDefinitionList.FirstOrDefault(a => a.RecId == Parameter.TypeRecId);
                result = result + ", " + Parameter.FieldName;
            }
            if (result != "")
                return result.Remove(0, 2);
            else
                return result;
        }

        public override void LoadFromXml(XElement Xml)
        {
            base.LoadFromXml(Xml);
            MethodName = (string)Xml.Attribute("MethodName");

            XElement XParameterList = Xml.Element("PARAMETERLIST");
            ParameterList.Clear();
            ParameterList.LoadFromXml(XParameterList);
        }

        public override void SaveToXml(XElement ParentNode)
        {
            base.SaveToXml(ParentNode);

            ParentNode.Add(new XAttribute("MethodName", MethodName));
            XElement XParameterList = new XElement("PARAMETERLIST");
            ParentNode.Add(XParameterList);
            ParameterList.SaveToXml(XParameterList);
        }
    }

}




















