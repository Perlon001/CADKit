using System;
#if ZwCAD
using CADRuntime = ZwSoft.ZwCAD.Runtime;
using ZwSoft.ZwCAD.Runtime;
#endif

#if AutoCAD
using CADRuntime = Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Runtime;
#endif

namespace CADProxy.Runtime
{
    public sealed class CommandMethodAttribute : Attribute, ICommandLineCallable
    {
        private readonly CADRuntime.CommandMethodAttribute proxy;
        public CommandMethodAttribute(string globalName)
        {
            this.GlobalName = globalName;
        }
        public CommandMethodAttribute(string globalName, CommandFlags flags)
        {
            this.GlobalName = globalName;
            this.Flags = flags;
        }
        public CommandMethodAttribute(string groupName, string globalName, CommandFlags flags)
        {
            this.GroupName = groupName;
            this.GlobalName = globalName;
            this.Flags = flags;
        }
        public CommandMethodAttribute(string groupName, string globalName, string localizedNameId, CommandFlags flags)
        {
            this.GroupName = groupName;
            this.GlobalName = globalName;
            this.LocalizedNameId = localizedNameId;
            this.Flags = flags;
        }
        public CommandMethodAttribute(string groupName, string globalName, string localizedNameId, CommandFlags flags, Type contextMenuExtensionType)
        {
            this.GroupName = groupName;
            this.GlobalName = globalName;
            this.LocalizedNameId = localizedNameId;
            this.Flags = flags;
            this.ContextMenuExtensionType = contextMenuExtensionType;
        }
        public CommandMethodAttribute(string groupName, string globalName, string localizedNameId, CommandFlags flags, string helpTopic)
        {
            this.GroupName = groupName;
            this.GlobalName = globalName;
            this.LocalizedNameId = localizedNameId;
            this.Flags = flags;
            this.HelpTopic = helpTopic;
        }
        public CommandMethodAttribute(string groupName, string globalName, string localizedNameId, CommandFlags flags, Type contextMenuExtensionType, string helpFileName, string helpTopic)
        {
            this.GroupName = groupName;
            this.GlobalName = globalName;
            this.LocalizedNameId = localizedNameId;
            this.Flags = flags;
            this.ContextMenuExtensionType = contextMenuExtensionType;
            this.HelpFileName = helpFileName;
            this.HelpTopic = helpTopic;
        }

        public string LocalizedNameId { get; }
        public string HelpTopic { get; }
        public string HelpFileName { get; }
        public string GroupName { get; }
        public string GlobalName { get; }
        public CommandFlags Flags { get; }
        public Type ContextMenuExtensionType { get; }
    }
}
