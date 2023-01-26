import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { AgentAvailabiltyRequest } from 'src/app/models/agent-availability-request';
import { AgentAvailabiltyResponse } from 'src/app/models/agent-availability-response';
import { AgentBooking } from 'src/app/models/agent-booking';
import { Agent } from 'src/app/models/agent';
import { AgentDataService } from 'src/app/services/agentData/agent-data.service';
import { MatDialog } from '@angular/material/dialog';
import { AgentBookingComponent } from '../agent-booking/agent-booking.component';

@Component({
  selector: 'app-agent-availability',
  templateUrl: './agent-availability.component.html',
  styleUrls: ['./agent-availability.component.css']
})
export class AgentAvailabilityComponent implements OnInit{
  agents: Agent[] = [];
  bookingId: number= 0;
  displayStyle = "none";
  messageRequired = 'Required Field. Please enter a value.'
  agentAvailabiltyResponse: AgentAvailabiltyResponse[] = [];
  salesAgentAvailabilityForm = new FormGroup({
    startDate: new FormControl(null, Validators.required),
    endDate: new FormControl(null, Validators.required),
    agentName: new FormControl(null)
  });

  constructor( private agentDataService: AgentDataService, private bokingMatDialog: MatDialog){
  }

  ngOnInit(): void {
    console.log('AgentAvailability');
    this.agentDataService.GetSalesAgents().subscribe((response: Agent[]) => {
      this.agents = response;
      console.log('SalesAgentAvailability: Get sales agents from web api');
    });
  }

  onSubmit() {
    if (this.salesAgentAvailabilityForm.valid) 
    {
      this.getSalesAgentVailability();
      this.displayStyle = "none";
    }
  }

  openBooking(index: number) {
    let agentBooking: AgentBooking ={
      bookingDateTime: this.agentAvailabiltyResponse[index].availableDateTime,
      bookingDate: this.agentAvailabiltyResponse[index].availableDate,
      agentName: this.agentAvailabiltyResponse[index].agentName,
      agentId: this.agentAvailabiltyResponse[index].agentId,
      day: this.agentAvailabiltyResponse[index].dayValue,
      timeSlotId: this.agentAvailabiltyResponse[index].timeSlotId,
      startTime: this.agentAvailabiltyResponse[index].startTime,
      endTime: this.agentAvailabiltyResponse[index].endTime
    };

    const bookingPopUpId = this.bokingMatDialog.open(AgentBookingComponent,
      {
        width:'40%',
        height:'30%',
        enterAnimationDuration: '200ms',
        exitAnimationDuration: '200ms',
        data: agentBooking
      }
    );

    bookingPopUpId.afterClosed().subscribe((item: number)=>
      {
        this.bookingId = item;
        this.displayStyle = item== undefined || item ==null ? "none" :"block";
        this.getSalesAgentVailability();
      });
  }

  getSalesAgentVailability()
  {
    let agentAvailabiltyRequest : AgentAvailabiltyRequest = {
      startDate: this.salesAgentAvailabilityForm.get("startDate")!.value,
      endDate: this.salesAgentAvailabilityForm.get("endDate")!.value,
      agentId: this.salesAgentAvailabilityForm.get("agentName")!.value == null ? 0 :this.salesAgentAvailabilityForm.get("agentName")!.value
    };

    this.agentDataService.GetSalesAgentAvailability(agentAvailabiltyRequest).subscribe((response: AgentAvailabiltyResponse[]) => {
      this.agentAvailabiltyResponse = response;
      console.log('SalesAgentAvailability: Get response from web api');
    });
  }
}
