import { Component,Input, OnInit, Inject} from '@angular/core';
import { AgentBooking } from 'src/app/models/agent-booking';
import { AgentBookingRequest } from 'src/app/models/agent-booking-request';
import { AgentDataService } from 'src/app/services/agentData/agent-data.service';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-agent-booking',
  templateUrl: './agent-booking.component.html',
  styleUrls: ['./agent-booking.component.css']
})
export class AgentBookingComponent implements OnInit {
  agentBooking!: AgentBooking;
  bookingId: number= 0;
  title = "Agent Booking";
  messageRequired = 'Required Field. Please enter a value.'

  bookingForm = new FormGroup({
    bookingBy: new FormControl(null, Validators.required),
    bookingMessage: new FormControl(null, Validators.required)
  });
  constructor( private agentDataService: AgentDataService, @Inject(MAT_DIALOG_DATA) public data: AgentBooking, 
    private Ref: MatDialogRef<AgentBookingComponent>){
    this.agentBooking = data;
  }

  ngOnInit(): void {
    console.log('Agent Booking');
  }

  onSubmit() {
    let agentBookingRequest : AgentBookingRequest = {
      bookingDateTime: this.agentBooking.bookingDateTime,
      bookedBy: this.bookingForm.get("bookingBy")!.value,
      agentId: this.agentBooking.agentId,
      timeSlotId: this.agentBooking.timeSlotId,
      bookingMessage: this.bookingForm.get("bookingMessage")!.value
    };

    this.agentDataService.CreateSalesAgentBooking(agentBookingRequest).subscribe((response: number) => {
      this.bookingId = response;
      console.log('CreateSalesAgentBooking: Get booking id from api');
      this.Ref.close(this.bookingId);
    });
    
  }

  onClose()
  {
    this.Ref.close();
  }
}
