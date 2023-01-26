CREATE TABLE [dbo].[TimeSlots](
	[TimeSlotId] [int] NOT NULL,
	[StartTime] [time](7) NULL,
	CONSTRAINT [PK_TimeSlots] PRIMARY KEY ([TimeSlotId])
)