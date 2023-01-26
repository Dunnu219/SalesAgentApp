import { TestBed } from '@angular/core/testing';

import { AgentDataService } from './agent-data.service';

describe('AgentDataService', () => {
  let service: AgentDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AgentDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
