using System;

#if ZwCAD
using CADRuntime = ZwSoft.ZwCAD.Runtime;
using ZwSoft.ZwCAD.Runtime;
#endif

#if AutoCAD
using CADRuntime = Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Runtime;
#endif

namespace CADKit.Runtime
{
    public sealed class CommandMethodAttribute : Attribute, ICommandLineCallable
    {
        private readonly CADRuntime.CommandMethodAttribute proxy;

        public CommandMethodAttribute(string globalName)
        {
            proxy = new CADRuntime.CommandMethodAttribute(globalName);
        }
        public CommandMethodAttribute(string globalName, CommandFlags flags)
        {
            proxy = new CADRuntime.CommandMethodAttribute(globalName, flags);
        }
        public CommandMethodAttribute(string groupName, string globalName, CommandFlags flags)
        {
            proxy = new CADRuntime.CommandMethodAttribute(groupName, globalName, flags);
        }
        public CommandMethodAttribute(string groupName, string globalName, string localizedNameId, CommandFlags flags)
        {
            proxy = new CADRuntime.CommandMethodAttribute(groupName, globalName, localizedNameId, flags);
        }
        public CommandMethodAttribute(string groupName, string globalName, string localizedNameId, CommandFlags flags, Type contextMenuExtensionType)
        {
            proxy = new CADRuntime.CommandMethodAttribute(groupName, globalName, localizedNameId, flags, contextMenuExtensionType);
        }
        public CommandMethodAttribute(string groupName, string globalName, string localizedNameId, CommandFlags flags, string helpTopic)
        {
            proxy = new CADRuntime.CommandMethodAttribute(groupName, globalName, localizedNameId, flags, helpTopic);
        }
        public CommandMethodAttribute(string groupName, string globalName, string localizedNameId, CommandFlags flags, Type contextMenuExtensionType, string helpFileName, string helpTopic)
        {
            proxy = new CADRuntime.CommandMethodAttribute(groupName, globalName, localizedNameId, flags, contextMenuExtensionType, helpFileName, helpTopic);
        }

        public string LocalizedNameId { get { return proxy.LocalizedNameId; } }
        public string HelpTopic { get { return proxy.HelpTopic; } }
        public string HelpFileName { get { return proxy.HelpFileName; } }
        public string GroupName { get { return proxy.GroupName; } }
        public string GlobalName { get { return proxy.GlobalName; } }
        public CommandFlags Flags { get { return proxy.Flags; } }
        public Type ContextMenuExtensionType { get { return proxy.ContextMenuExtensionType; } }
    }
}
