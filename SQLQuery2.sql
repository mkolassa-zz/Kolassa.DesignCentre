select r.ReportDescription, r.name as ReportName, r.code as ReportCode, c.ControlName, c.ControlFieldName,c.ControlFieldDescription,c.ControlType,f.tablename, f.sortorder,f.ReportControl

from tblReportDescriptions r inner join tblreportfields f on f.reportid = r.code
inner join tblReportControls c on f.reportcontrol = c.controlID
Where r.code = 30

SELECT  D.DepositTypeName, F.Name as FloorName, Tier.Name as TierName, T.UnitTypeName, 
                U.* 
FROM (((( tblUnits  U  left join tblquote  q  on u.id= q.unitid              
  INNER JOIN tblUnitTypes  T         ON U.UnitTypeID = T.ID        )         
  LEFT JOIN tblDepositConditions  D ON U.DepositTypeID = D.ID )  
  LEFT JOIN tblFloors  F            ON U.FloorID = F.ID             )  
  LEFT JOIN tblUnitTiers  Tier      ON U.TierID = Tier.ID        )  
WHERE  U.NodeID=1 

 and  u.ID = '0b41ee62-fdcd-477e-9d84-3f416677eb29' 