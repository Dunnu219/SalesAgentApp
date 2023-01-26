CREATE TABLE [dbo].[Booking](
	[BookingId] [int] IDENTITY(1,1) NOT NULL,
	[BookingDate] [date] NOT NULL,
	[AgentId] [int] NOT NULL,
	[TimeSlotId] [int] NOT NULL,
	[BookingMessage] [nvarchar](50) NOT NULL,
	[BookedBy] [nvarchar](50) NULL,
	CONSTRAINT [PK_Booking] PRIMARY KEY ([BookingId]),
	CONSTRAINT [UC_AgentAvailability] UNIQUE ([BookingDate], [AgentId], [TimeSlotId])
)