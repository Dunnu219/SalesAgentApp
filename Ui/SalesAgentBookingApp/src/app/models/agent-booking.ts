export interface AgentBooking
{
    bookingDateTime: Date;
    bookingDate: Date;
    agentName: string;
    agentId: number;
    day: string;
    timeSlotId: number;
    startTime: string;
    endTime: string;
}