CREATE TABLE [dbo].[SalesAgent](
	[AgentId] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	CONSTRAINT [PK_SalesAgent] PRIMARY KEY ([AgentId])
)