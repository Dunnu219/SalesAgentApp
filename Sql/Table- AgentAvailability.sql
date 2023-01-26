CREATE TABLE [dbo].[AgentAvailability](
	[AgentId] [int] NOT NULL,
	[TimeSlotId] [int] NOT NULL,
	[Day] [nvarchar](10) NOT NULL,
	CONSTRAINT [UC_AgentAvailability] UNIQUE ([AgentId], [TimeSlotId], [Day])
)