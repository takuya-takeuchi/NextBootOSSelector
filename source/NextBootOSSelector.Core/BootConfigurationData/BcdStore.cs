using System.Collections;
using System.ComponentModel;
using System.Management;

namespace Ouranos.NextBootOSSelector.BootConfigurationData {
    // 関数 ShouldSerialize<PropertyName> は、特定のプロパティをシリアル化する必要があるかどうかを調べるために VS プロパティ ブラウザで使用される関数です。これらの関数はすべての ValueType プロパティに追加されます ( NULL に設定できない型のプロパティ、Int32, BOOLなど)。これらの関数は Is<PropertyName>Null 関数を使用します。これらの関数はまた、プロパティの NULL 値を調べるプロパティのための TypeConverter 実装でも使用され、Visual studio でドラッグ アンド ドロップをする場合は、プロパティ ブラウザに空の値が表示されるようにします。
    // Functions Is<PropertyName>Null() は、プロパティが NULL かどうかを調べるために使用されます。
    // 関数 Reset<PropertyName> は Null 許容を使用できる読み込み/書き込みプロパティに追加されます。これらの関数は、プロパティを NULL に設定するためにプロパティ ブラウザの VS デザイナによって使用されます。
    // プロパティ用にクラスに追加されたすべてのプロパティは、Visual Studio デザイナ内での動作を定義するように、また使用する TypeConverter を定義するように設定されています。
    // WMI クラス用に生成された事前バインディング クラスです。BcdStore
    internal class BcdStore : Component {
        
        // クラスが存在する場所にWMI 名前空間を保持するプライベート プロパティです。
        private static string CreatedWmiNamespace = "root\\WMI";
        
        // このクラスを作成した WMI クラスの名前を保持するプライベート プロパティです。
        private static string CreatedClassName = "BcdStore";
        
        // さまざまなメソッドで使用される ManagementScope を保持するプライベート メンバ変数です。
        private static ManagementScope statMgmtScope;
        
        private ManagementSystemProperties PrivateSystemProperties;
        
        // 基になる LateBound WMI オブジェクトです。
        private ManagementObject PrivateLateBoundObject;
        
        // クラスの '自動コミット' 動作を保存するメンバ変数です。
        private bool AutoCommitProp;
        
        // インスタンスを表す埋め込みプロパティを保持するプライベート変数です。
        private ManagementBaseObject embeddedObj;
        
        // 現在使用されている WMI オブジェクトです。
        private ManagementBaseObject curObj;
        
        // インスタンスが埋め込みオブジェクトかどうかを示すフラグです。
        private bool isEmbedded;
        
        // 下記は WMI オブジェクトを使用してクラスのインスタンスを初期化するコンストラクタのオーバーロードです。
        public BcdStore() {
            this.InitializeObject(null, null, null);
        }
        
        public BcdStore(string keyFilePath) {
            this.InitializeObject(null, new ManagementPath(ConstructPath(keyFilePath)), null);
        }
        
        public BcdStore(ManagementScope mgmtScope, string keyFilePath) {
            this.InitializeObject(mgmtScope, new ManagementPath(ConstructPath(keyFilePath)), null);
        }
        
        public BcdStore(ManagementPath path, ObjectGetOptions getOptions) {
            this.InitializeObject(null, path, getOptions);
        }
        
        public BcdStore(ManagementScope mgmtScope, ManagementPath path) {
            this.InitializeObject(mgmtScope, path, null);
        }
        
        public BcdStore(ManagementPath path) {
            this.InitializeObject(null, path, null);
        }
        
        public BcdStore(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) {
            this.InitializeObject(mgmtScope, path, getOptions);
        }
        
        public BcdStore(ManagementObject theObject) {
            this.Initialize();
            if (this.CheckIfProperClass(theObject)) {
                this.PrivateLateBoundObject = theObject;
                this.PrivateSystemProperties = new ManagementSystemProperties(this.PrivateLateBoundObject);
                this.curObj = this.PrivateLateBoundObject;
            }
            else {
                throw new System.ArgumentException("クラス名が一致しません。");
            }
        }
        
        public BcdStore(ManagementBaseObject theObject) {
            this.Initialize();
            if (this.CheckIfProperClass(theObject)) {
                this.embeddedObj = theObject;
                this.PrivateSystemProperties = new ManagementSystemProperties(theObject);
                this.curObj = this.embeddedObj;
                this.isEmbedded = true;
            }
            else {
                throw new System.ArgumentException("クラス名が一致しません。");
            }
        }
        
        // WMI クラスの名前空間を返すプロパティです。
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OriginatingNamespace {
            get {
                return "root\\WMI";
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ManagementClassName {
            get {
                string strRet = CreatedClassName;
                if ((this.curObj != null)) {
                    if ((this.curObj.ClassPath != null)) {
                        strRet = ((string)(this.curObj["__CLASS"]));
                        if (((strRet == null) 
                                    || (strRet == string.Empty))) {
                            strRet = CreatedClassName;
                        }
                    }
                }
                return strRet;
            }
        }
        
        // WMI オブジェクトのシステム プロパティを取得するための埋め込みオブジェクトをポイントするプロパティです。
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagementSystemProperties SystemProperties {
            get {
                return this.PrivateSystemProperties;
            }
        }
        
        // 基になる LateBound WMI オブジェクトを返すプロパティです。
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagementBaseObject LateBoundObject {
            get {
                return this.curObj;
            }
        }
        
        // オブジェクトの ManagementScope です。
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagementScope Scope {
            get
            {
                if ((this.isEmbedded == false)) {
                    return this.PrivateLateBoundObject.Scope;
                }
                return null;
            }
            set {
                if ((this.isEmbedded == false)) {
                    this.PrivateLateBoundObject.Scope = value;
                }
            }
        }
        
        // WMI オブジェクトのコミット動作を表示するプロパティです。 これが true の場合、プロパティが変更するたびに WMI オブジェクトは自動的に保存されます (すなわち、プロパティを変更した後で Put() が呼び出されます)。
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AutoCommit {
            get {
                return this.AutoCommitProp;
            }
            set {
                this.AutoCommitProp = value;
            }
        }
        
        // 基になる WMI オブジェクトの ManagementPath です。
        [Browsable(true)]
        public ManagementPath Path {
            get
            {
                if ((this.isEmbedded == false)) {
                    return this.PrivateLateBoundObject.Path;
                }
                return null;
            }
            set {
                if ((this.isEmbedded == false)) {
                    if ((this.CheckIfProperClass(null, value, null) != true)) {
                        throw new System.ArgumentException("クラス名が一致しません。");
                    }
                    this.PrivateLateBoundObject.Path = value;
                }
            }
        }
        
        // さまざまなメソッドで使用されるプライベート スタティック スコープ プロパティです。
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static ManagementScope StaticScope {
            get {
                return statMgmtScope;
            }
            set {
                statMgmtScope = value;
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("A BcdStore is uniquely identified by its file path. The system store is denoted v" +
            "ia an empty file path.")]
        public string FilePath {
            get {
                return ((string)(this.curObj["FilePath"]));
            }
        }
        
        private bool CheckIfProperClass(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions OptionsParam)
        {
            if (((path != null) 
                        && (string.Compare(path.ClassName, this.ManagementClassName, true, System.Globalization.CultureInfo.InvariantCulture) == 0))) {
                return true;
            }
            return this.CheckIfProperClass(new ManagementObject(mgmtScope, path, OptionsParam));
        }

        private bool CheckIfProperClass(ManagementBaseObject theObj) {
            if (((theObj != null) 
                        && (string.Compare(((string)(theObj["__CLASS"])), this.ManagementClassName, true, System.Globalization.CultureInfo.InvariantCulture) == 0))) {
                return true;
            }
            System.Array parentClasses = ((System.Array)(theObj["__DERIVATION"]));
            if ((parentClasses != null)) {
                int count = 0;
                for (count = 0; (count < parentClasses.Length); count = (count + 1)) {
                    if ((string.Compare(((string)(parentClasses.GetValue(count))), this.ManagementClassName, true, System.Globalization.CultureInfo.InvariantCulture) == 0)) {
                        return true;
                    }
                }
            }
            return false;
        }
        
        [Browsable(true)]
        public void CommitObject() {
            if ((this.isEmbedded == false)) {
                this.PrivateLateBoundObject.Put();
            }
        }
        
        [Browsable(true)]
        public void CommitObject(PutOptions putOptions) {
            if ((this.isEmbedded == false)) {
                this.PrivateLateBoundObject.Put(putOptions);
            }
        }
        
        private void Initialize() {
            this.AutoCommitProp = true;
            this.isEmbedded = false;
        }
        
        private static string ConstructPath(string keyFilePath) {
            string strPath = "root\\WMI:BcdStore";
            strPath = string.Concat(strPath, string.Concat(".FilePath=", string.Concat("\"", string.Concat(keyFilePath, "\""))));
            return strPath;
        }
        
        private void InitializeObject(ManagementScope mgmtScope, ManagementPath path, ObjectGetOptions getOptions) {
            this.Initialize();
            if ((path != null)) {
                if ((this.CheckIfProperClass(mgmtScope, path, getOptions) != true)) {
                    throw new System.ArgumentException("クラス名が一致しません。");
                }
            }
            this.PrivateLateBoundObject = new ManagementObject(mgmtScope, path, getOptions);
            this.PrivateSystemProperties = new ManagementSystemProperties(this.PrivateLateBoundObject);
            this.curObj = this.PrivateLateBoundObject;
        }
        
        // WMI クラスのインスタンスを列挙する GetInstances() ヘルプのオーバーロードです。
        public static BcdStoreCollection GetInstances() {
            return GetInstances(null, null, null);
        }
        
        public static BcdStoreCollection GetInstances(string condition) {
            return GetInstances(null, condition, null);
        }
        
        public static BcdStoreCollection GetInstances(string[] selectedProperties) {
            return GetInstances(null, null, selectedProperties);
        }
        
        public static BcdStoreCollection GetInstances(string condition, string[] selectedProperties) {
            return GetInstances(null, condition, selectedProperties);
        }
        
        public static BcdStoreCollection GetInstances(ManagementScope mgmtScope, EnumerationOptions enumOptions) {
            if ((mgmtScope == null)) {
                if ((statMgmtScope == null)) {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\WMI";
                }
                else {
                    mgmtScope = statMgmtScope;
                }
            }
            ManagementPath pathObj = new ManagementPath();
            pathObj.ClassName = "BcdStore";
            pathObj.NamespacePath = "root\\WMI";
            ManagementClass clsObject = new ManagementClass(mgmtScope, pathObj, null);
            if ((enumOptions == null)) {
                enumOptions = new EnumerationOptions();
                enumOptions.EnsureLocatable = true;
            }
            return new BcdStoreCollection(clsObject.GetInstances(enumOptions));
        }
        
        public static BcdStoreCollection GetInstances(ManagementScope mgmtScope, string condition) {
            return GetInstances(mgmtScope, condition, null);
        }
        
        public static BcdStoreCollection GetInstances(ManagementScope mgmtScope, string[] selectedProperties) {
            return GetInstances(mgmtScope, null, selectedProperties);
        }
        
        public static BcdStoreCollection GetInstances(ManagementScope mgmtScope, string condition, string[] selectedProperties) {
            if ((mgmtScope == null)) {
                if ((statMgmtScope == null)) {
                    mgmtScope = new ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\WMI";
                }
                else {
                    mgmtScope = statMgmtScope;
                }
            }
            ManagementObjectSearcher ObjectSearcher = new ManagementObjectSearcher(mgmtScope, new SelectQuery("BcdStore", condition, selectedProperties));
            EnumerationOptions enumOptions = new EnumerationOptions();
            enumOptions.EnsureLocatable = true;
            ObjectSearcher.Options = enumOptions;
            return new BcdStoreCollection(ObjectSearcher.Get());
        }
        
        [Browsable(true)]
        public static BcdStore CreateInstance() {
            ManagementScope mgmtScope = null;
            if ((statMgmtScope == null)) {
                mgmtScope = new ManagementScope();
                mgmtScope.Path.NamespacePath = CreatedWmiNamespace;
            }
            else {
                mgmtScope = statMgmtScope;
            }
            ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
            ManagementClass tmpMgmtClass = new ManagementClass(mgmtScope, mgmtPath, null);
            return new BcdStore(tmpMgmtClass.CreateInstance());
        }
        
        [Browsable(true)]
        public void Delete() {
            this.PrivateLateBoundObject.Delete();
        }
        
        public bool CopyObject(uint Flags, string SourceId, string SourceStoreFile, out ManagementBaseObject Object) {
            if ((this.isEmbedded == false)) {
                ManagementBaseObject inParams = null;
                inParams = this.PrivateLateBoundObject.GetMethodParameters("CopyObject");
                inParams["Flags"] = Flags;
                inParams["SourceId"] = SourceId;
                inParams["SourceStoreFile"] = SourceStoreFile;
                ManagementBaseObject outParams = this.PrivateLateBoundObject.InvokeMethod("CopyObject", inParams, null);
                Object = ((ManagementBaseObject)(outParams.Properties["Object"].Value));
                return System.Convert.ToBoolean(outParams.Properties["ReturnValue"].Value);
            }
            Object = null;
            return System.Convert.ToBoolean(0);
        }
        
        public bool CopyObjects(uint Flags, string SourceStoreFile, uint Type)
        {
            if ((this.isEmbedded == false)) {
                ManagementBaseObject inParams = null;
                inParams = this.PrivateLateBoundObject.GetMethodParameters("CopyObjects");
                inParams["Flags"] = Flags;
                inParams["SourceStoreFile"] = SourceStoreFile;
                inParams["Type"] = Type;
                ManagementBaseObject outParams = this.PrivateLateBoundObject.InvokeMethod("CopyObjects", inParams, null);
                return System.Convert.ToBoolean(outParams.Properties["ReturnValue"].Value);
            }
            return System.Convert.ToBoolean(0);
        }

        public bool CreateObject(string Id, uint Type, out ManagementBaseObject Object) {
            if ((this.isEmbedded == false)) {
                ManagementBaseObject inParams = null;
                inParams = this.PrivateLateBoundObject.GetMethodParameters("CreateObject");
                inParams["Id"] = Id;
                inParams["Type"] = Type;
                ManagementBaseObject outParams = this.PrivateLateBoundObject.InvokeMethod("CreateObject", inParams, null);
                Object = ((ManagementBaseObject)(outParams.Properties["Object"].Value));
                return System.Convert.ToBoolean(outParams.Properties["ReturnValue"].Value);
            }
            Object = null;
            return System.Convert.ToBoolean(0);
        }
        
        public static bool CreateStore(string File, out ManagementBaseObject Store) {
            bool IsMethodStatic = true;
            if (IsMethodStatic) {
                ManagementBaseObject inParams = null;
                ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
                ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("CreateStore");
                inParams["File"] = File;
                ManagementBaseObject outParams = classObj.InvokeMethod("CreateStore", inParams, null);
                Store = ((ManagementBaseObject)(outParams.Properties["Store"].Value));
                return System.Convert.ToBoolean(outParams.Properties["ReturnValue"].Value);
            }
            Store = null;
            return System.Convert.ToBoolean(0);
        }
        
        public bool DeleteObject(string Id)
        {
            if ((this.isEmbedded == false)) {
                ManagementBaseObject inParams = null;
                inParams = this.PrivateLateBoundObject.GetMethodParameters("DeleteObject");
                inParams["Id"] = Id;
                ManagementBaseObject outParams = this.PrivateLateBoundObject.InvokeMethod("DeleteObject", inParams, null);
                return System.Convert.ToBoolean(outParams.Properties["ReturnValue"].Value);
            }
            return System.Convert.ToBoolean(0);
        }

        public static bool DeleteSystemStore() {
            bool IsMethodStatic = true;
            if (IsMethodStatic) {
                ManagementBaseObject inParams = null;
                ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
                ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
                ManagementBaseObject outParams = classObj.InvokeMethod("DeleteSystemStore", inParams, null);
                return System.Convert.ToBoolean(outParams.Properties["ReturnValue"].Value);
            }
            return System.Convert.ToBoolean(0);
        }
        
        public bool EnumerateObjects(uint Type, out ManagementBaseObject[] Objects) {
            if ((this.isEmbedded == false)) {
                ManagementBaseObject inParams = null;
                inParams = this.PrivateLateBoundObject.GetMethodParameters("EnumerateObjects");
                inParams["Type"] = Type;
                ManagementBaseObject outParams = this.PrivateLateBoundObject.InvokeMethod("EnumerateObjects", inParams, null);
                Objects = ((ManagementBaseObject[])(outParams.Properties["Objects"].Value));
                return System.Convert.ToBoolean(outParams.Properties["ReturnValue"].Value);
            }
            Objects = null;
            return System.Convert.ToBoolean(0);
        }
        
        public static bool ExportStore(string File) {
            bool IsMethodStatic = true;
            if (IsMethodStatic) {
                ManagementBaseObject inParams = null;
                ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
                ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("ExportStore");
                inParams["File"] = File;
                ManagementBaseObject outParams = classObj.InvokeMethod("ExportStore", inParams, null);
                return System.Convert.ToBoolean(outParams.Properties["ReturnValue"].Value);
            }
            return System.Convert.ToBoolean(0);
        }
        
        public static bool GetSystemDisk(out string Disk) {
            bool IsMethodStatic = true;
            if (IsMethodStatic) {
                ManagementBaseObject inParams = null;
                ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
                ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
                ManagementBaseObject outParams = classObj.InvokeMethod("GetSystemDisk", inParams, null);
                Disk = System.Convert.ToString(outParams.Properties["Disk"].Value);
                return System.Convert.ToBoolean(outParams.Properties["ReturnValue"].Value);
            }
            Disk = null;
            return System.Convert.ToBoolean(0);
        }
        
        public static bool GetSystemPartition(out string Partition) {
            bool IsMethodStatic = true;
            if (IsMethodStatic) {
                ManagementBaseObject inParams = null;
                ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
                ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
                ManagementBaseObject outParams = classObj.InvokeMethod("GetSystemPartition", inParams, null);
                Partition = System.Convert.ToString(outParams.Properties["Partition"].Value);
                return System.Convert.ToBoolean(outParams.Properties["ReturnValue"].Value);
            }
            Partition = null;
            return System.Convert.ToBoolean(0);
        }
        
        public static bool ImportStore(string File) {
            bool IsMethodStatic = true;
            if (IsMethodStatic) {
                ManagementBaseObject inParams = null;
                ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
                ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("ImportStore");
                inParams["File"] = File;
                ManagementBaseObject outParams = classObj.InvokeMethod("ImportStore", inParams, null);
                return System.Convert.ToBoolean(outParams.Properties["ReturnValue"].Value);
            }
            return System.Convert.ToBoolean(0);
        }
        
        public static bool ImportStoreWithFlags(string File, uint Flags) {
            bool IsMethodStatic = true;
            if (IsMethodStatic) {
                ManagementBaseObject inParams = null;
                ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
                ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("ImportStoreWithFlags");
                inParams["File"] = File;
                inParams["Flags"] = Flags;
                ManagementBaseObject outParams = classObj.InvokeMethod("ImportStoreWithFlags", inParams, null);
                return System.Convert.ToBoolean(outParams.Properties["ReturnValue"].Value);
            }
            return System.Convert.ToBoolean(0);
        }
        
        public bool OpenObject(string Id, out ManagementBaseObject Object) {
            if ((this.isEmbedded == false)) {
                ManagementBaseObject inParams = null;
                inParams = this.PrivateLateBoundObject.GetMethodParameters("OpenObject");
                inParams["Id"] = Id;
                ManagementBaseObject outParams = this.PrivateLateBoundObject.InvokeMethod("OpenObject", inParams, null);
                Object = ((ManagementBaseObject)(outParams.Properties["Object"].Value));
                return System.Convert.ToBoolean(outParams.Properties["ReturnValue"].Value);
            }
            Object = null;
            return System.Convert.ToBoolean(0);
        }
        
        public static bool OpenStore(string File, out ManagementBaseObject Store) {
            bool IsMethodStatic = true;
            if (IsMethodStatic) {
                ManagementBaseObject inParams = null;
                ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
                ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("OpenStore");
                inParams["File"] = File;
                ManagementBaseObject outParams = classObj.InvokeMethod("OpenStore", inParams, null);
                Store = ((ManagementBaseObject)(outParams.Properties["Store"].Value));
                return System.Convert.ToBoolean(outParams.Properties["ReturnValue"].Value);
            }
            Store = null;
            return System.Convert.ToBoolean(0);
        }
        
        public static bool SetSystemStoreDevice(string Partition) {
            bool IsMethodStatic = true;
            if (IsMethodStatic) {
                ManagementBaseObject inParams = null;
                ManagementPath mgmtPath = new ManagementPath(CreatedClassName);
                ManagementClass classObj = new ManagementClass(statMgmtScope, mgmtPath, null);
                inParams = classObj.GetMethodParameters("SetSystemStoreDevice");
                inParams["Partition"] = Partition;
                ManagementBaseObject outParams = classObj.InvokeMethod("SetSystemStoreDevice", inParams, null);
                return System.Convert.ToBoolean(outParams.Properties["ReturnValue"].Value);
            }
            return System.Convert.ToBoolean(0);
        }
        
        // クラスのインスタンスを列挙する列挙子の実装です。
        internal class BcdStoreCollection : object, ICollection {
            
            private ManagementObjectCollection privColObj;
            
            public BcdStoreCollection(ManagementObjectCollection objCollection) {
                this.privColObj = objCollection;
            }
            
            public virtual int Count {
                get {
                    return this.privColObj.Count;
                }
            }
            
            public virtual bool IsSynchronized {
                get {
                    return this.privColObj.IsSynchronized;
                }
            }
            
            public virtual object SyncRoot {
                get {
                    return this;
                }
            }
            
            public virtual void CopyTo(System.Array array, int index) {
                this.privColObj.CopyTo(array, index);
                int nCtr;
                for (nCtr = 0; (nCtr < array.Length); nCtr = (nCtr + 1)) {
                    array.SetValue(new BcdStore(((ManagementObject)(array.GetValue(nCtr)))), nCtr);
                }
            }
            
            public virtual IEnumerator GetEnumerator() {
                return new BcdStoreEnumerator(this.privColObj.GetEnumerator());
            }
            
            internal class BcdStoreEnumerator : object, IEnumerator {
                
                private ManagementObjectCollection.ManagementObjectEnumerator privObjEnum;
                
                public BcdStoreEnumerator(ManagementObjectCollection.ManagementObjectEnumerator objEnum) {
                    this.privObjEnum = objEnum;
                }
                
                public virtual object Current {
                    get {
                        return new BcdStore(((ManagementObject)(this.privObjEnum.Current)));
                    }
                }
                
                public virtual bool MoveNext() {
                    return this.privObjEnum.MoveNext();
                }
                
                public virtual void Reset() {
                    this.privObjEnum.Reset();
                }
            }
        }
        
        // ValueType プロパティの NULL 値を扱う TypeConverter です。
        internal class WMIValueTypeConverter : TypeConverter {
            
            private TypeConverter baseConverter;
            
            private System.Type baseType;
            
            public WMIValueTypeConverter(System.Type inBaseType) {
                this.baseConverter = TypeDescriptor.GetConverter(inBaseType);
                this.baseType = inBaseType;
            }
            
            public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type srcType) {
                return this.baseConverter.CanConvertFrom(context, srcType);
            }
            
            public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType) {
                return this.baseConverter.CanConvertTo(context, destinationType);
            }
            
            public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value) {
                return this.baseConverter.ConvertFrom(context, culture, value);
            }
            
            public override object CreateInstance(ITypeDescriptorContext context, IDictionary dictionary) {
                return this.baseConverter.CreateInstance(context, dictionary);
            }
            
            public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) {
                return this.baseConverter.GetCreateInstanceSupported(context);
            }
            
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, System.Attribute[] attributeVar) {
                return this.baseConverter.GetProperties(context, value, attributeVar);
            }
            
            public override bool GetPropertiesSupported(ITypeDescriptorContext context) {
                return this.baseConverter.GetPropertiesSupported(context);
            }
            
            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) {
                return this.baseConverter.GetStandardValues(context);
            }
            
            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) {
                return this.baseConverter.GetStandardValuesExclusive(context);
            }
            
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) {
                return this.baseConverter.GetStandardValuesSupported(context);
            }
            
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType) {
                if ((this.baseType.BaseType == typeof(System.Enum))) {
                    if ((value.GetType() == destinationType)) {
                        return value;
                    }
                    if ((((value == null) 
                                && (context != null)) 
                                && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))) {
                        return  "NULL_ENUM_VALUE" ;
                    }
                    return this.baseConverter.ConvertTo(context, culture, value, destinationType);
                }
                if (((this.baseType == typeof(bool)) 
                            && (this.baseType.BaseType == typeof(System.ValueType)))) {
                    if ((((value == null) 
                                && (context != null)) 
                                && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))) {
                        return "";
                    }
                    return this.baseConverter.ConvertTo(context, culture, value, destinationType);
                }
                if (((context != null) 
                            && (context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false))) {
                    return "";
                }
                return this.baseConverter.ConvertTo(context, culture, value, destinationType);
            }
        }
        
        // WMI システム プロパティを表す埋め込みクラスです。
        [TypeConverter(typeof(ExpandableObjectConverter))]
        internal class ManagementSystemProperties {
            
            private ManagementBaseObject PrivateLateBoundObject;
            
            public ManagementSystemProperties(ManagementBaseObject ManagedObject) {
                this.PrivateLateBoundObject = ManagedObject;
            }
            
            [Browsable(true)]
            public int GENUS {
                get {
                    return ((int)(this.PrivateLateBoundObject["__GENUS"]));
                }
            }
            
            [Browsable(true)]
            public string CLASS {
                get {
                    return ((string)(this.PrivateLateBoundObject["__CLASS"]));
                }
            }
            
            [Browsable(true)]
            public string SUPERCLASS {
                get {
                    return ((string)(this.PrivateLateBoundObject["__SUPERCLASS"]));
                }
            }
            
            [Browsable(true)]
            public string DYNASTY {
                get {
                    return ((string)(this.PrivateLateBoundObject["__DYNASTY"]));
                }
            }
            
            [Browsable(true)]
            public string RELPATH {
                get {
                    return ((string)(this.PrivateLateBoundObject["__RELPATH"]));
                }
            }
            
            [Browsable(true)]
            public int PROPERTY_COUNT {
                get {
                    return ((int)(this.PrivateLateBoundObject["__PROPERTY_COUNT"]));
                }
            }
            
            [Browsable(true)]
            public string[] DERIVATION {
                get {
                    return ((string[])(this.PrivateLateBoundObject["__DERIVATION"]));
                }
            }
            
            [Browsable(true)]
            public string SERVER {
                get {
                    return ((string)(this.PrivateLateBoundObject["__SERVER"]));
                }
            }
            
            [Browsable(true)]
            public string NAMESPACE {
                get {
                    return ((string)(this.PrivateLateBoundObject["__NAMESPACE"]));
                }
            }
            
            [Browsable(true)]
            public string PATH {
                get {
                    return ((string)(this.PrivateLateBoundObject["__PATH"]));
                }
            }
        }
    }
}
