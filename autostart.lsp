(setq 
	MyPath "D:\\_dev\\CSharp\\Workspace\\CADKit\\CADKitZwCAD\\bin\\Debug"
	dllFile (strcat MyPath "\\CADKitZwCAD.dll")
)
(if (findfile dllFile)(vl-cmdf "netload" dllFile))
;(command "ZPap")
(princ)

