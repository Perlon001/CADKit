(vl-load-com)
(cond
	((= (getvar "PRODUCT") "ZWCAD") (vl-cmdf "netload" "CADKitZwCAD.dll"))
	((= (getvar "PRODUCT") "AutoCAD") (vl-cmdf "netload" "CADKitAutoCAD.dll"))
	(T (progn
			(princ "\nNieznana platforma CAD. CADKit nie mo¿e byæ wczytany.")
		)
	)
)

(princ)
