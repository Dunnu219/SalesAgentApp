import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentBookingComponent } from './agent-booking.component';

describe('AgentBookingComponent', () => {
  let component: AgentBookingComponent;
  let fixture: ComponentFixture<AgentBookingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentBookingComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AgentBookingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
