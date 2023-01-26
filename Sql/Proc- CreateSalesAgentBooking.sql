CREATE OR ALTER PROCEDURE [dbo].[CreateSalesAgentBooking] 
	@BookingDate DATE, 
	@BookedBy NVARCHAR(50),
	@AgentId INT,
	@TimeSlotId INT,
	@BookingMessage NVARCHAR(50)
AS
	INSERT INTO Booking (BookingDate, BookedBy, AgentId, TimeSlotId, BookingMessage) 
	VALUES (@BookingDate, @BookedBy, @AgentId, @TimeSlotId, @BookingMessage);
    SELECT CAST(SCOPE_IDENTITY() as int)