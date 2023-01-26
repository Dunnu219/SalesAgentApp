import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AgentAvailabiltyRequest } from 'src/app/models/agent-availability-request';
import { AgentAvailabiltyResponse } from 'src/app/models/agent-availability-response';
import { AgentBookingRequest } from 'src/app/models/agent-booking-request';
import { Agent } from 'src/app/models/agent';

@Injectable({
  providedIn: 'root'
})
export class AgentDataService {

  baseUrl: string = "https://localhost:7103/api/salesagentbooking/";
  constructor(private http: HttpClient) { }

  public GetSalesAgents(): Observable<Agent[]> {

    let salesAgentApiUrl = this.baseUrl + "getsalesagents";
    return this.http.get<Agent[]>(salesAgentApiUrl);
  }

  public GetSalesAgentAvailability(agentAvailabiltyRequest: AgentAvailabiltyRequest): Observable<AgentAvailabiltyResponse[]> {

    let salesAgentApiUrl = this.baseUrl + "getsalesagentavailability";
    const params = new HttpParams()
    .set("startDate", agentAvailabiltyRequest.startDate!)
    .set("endDate", agentAvailabiltyRequest.endDate!)
    .set("agentId", agentAvailabiltyRequest.agentId!);
    return this.http.get<AgentAvailabiltyResponse[]>(salesAgentApiUrl, {params});
  }

  public CreateSalesAgentBooking(agentBookingRequest: AgentBookingRequest): Observable<number> {

    let createBookingApiUrl = this.baseUrl + "createsalesagentbooking";
    return this.http.post<number>(createBookingApiUrl, agentBookingRequest);
  }
}