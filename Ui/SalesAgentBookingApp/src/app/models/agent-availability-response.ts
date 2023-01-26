export interface AgentAvailabiltyResponse
{
    availableDateTime: Date;
    agentName: string;
    agentId: number;
    dayValue: string;
    timeSlotId: number;
    startTime: string;
    endTime: string;
    availableDate: Date
}