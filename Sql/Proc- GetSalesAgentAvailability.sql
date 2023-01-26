CREATE OR ALTER PROCEDURE [dbo].[GetSalesAgentAvailability] 
	@StartDate DATE,
	@EndDate DATE, 
	@AgentId INT
AS
	WITH ListDates(AllDates) AS
	(    SELECT @StartDate AS DATE
		UNION ALL
		SELECT DATEADD(DAY,1,AllDates)
		FROM ListDates 
		WHERE AllDates < @EndDate
	)
	SELECT CAST(D.AllDates as DATETIME) + CAST(TS.StartTime as DATETIME)  AS AvailableDateTime, 
	D.Day, SA.FirstName + ' ' + SA.LastName AS AgentName, SA.AgentId, AA.TimeSlotId
	FROM AgentAvailability AA
	INNER JOIN
	TimeSlots TS
	ON AA.TimeSlotId = TS.TimeSlotId
	INNER JOIN SalesAgent SA
	ON AA.AgentId =SA.AgentId
	INNER JOIN
	(SELECT AllDates, DATENAME(weekday, AllDates) Day FROM ListDates) D
	ON AA.Day = D.Day
	LEFT JOIN Booking B
	ON  B.BookingDate = D.AllDates
	AND B.AgentId = SA.AgentId
	AND B.TimeSlotId = AA.TimeSlotId
	WHERE B.BookingDate IS NULL
	AND (@AgentId = 0 OR AA.AgentId = @AgentId )
	ORDER BY D.AllDates, AA.TimeSlotId, AgentName