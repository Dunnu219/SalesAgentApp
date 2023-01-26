export interface AgentBookingRequest
{
    bookingDateTime: Date;
    bookedBy: string | null
    agentId: number;
    timeSlotId: number;
    bookingMessage: string | null
}