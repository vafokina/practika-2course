﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeliveryServiceWebDBServer.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>Зарегистрировано</string>
  <string>Ожидается отправка</string>
  <string>Отправлено в пункт назначения</string>
  <string>Прибыло в пункт назначения</string>
  <string>Доставлено в пункт выдачи</string>
  <string>Передано курьеру</string>
  <string>Доставлено</string>
  <string>Будет возвращено отправителю</string>
  <string>Прибыло для возврата отправителю</string>
  <string>Возвращено отправителю</string>
  <string>Отменено</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection Statuses {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["Statuses"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public int DaysForBack {
            get {
                return ((int)(this["DaysForBack"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>Товары народного потребления (без ГСМ и АКБ)</string>
  <string>Опасные грузы</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection Descriptions {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["Descriptions"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string CompanyName {
            get {
                return ((string)(this["CompanyName"]));
            }
        }
    }
}
