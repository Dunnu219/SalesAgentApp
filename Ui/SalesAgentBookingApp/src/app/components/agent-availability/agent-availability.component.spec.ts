import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgentAvailabilityComponent } from './agent-availability.component';

describe('AgentAvailabilityComponent', () => {
  let component: AgentAvailabilityComponent;
  let fixture: ComponentFixture<AgentAvailabilityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgentAvailabilityComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AgentAvailabilityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
